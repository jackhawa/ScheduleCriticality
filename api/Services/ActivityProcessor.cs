using System;
using System.Collections.Generic;
using System.Linq;
using SchedulePath.Models;
using SchedulePath.Helper;

namespace SchedulePath.Services
{
    public class ActivityProcessor : IActivityProcessor
    {
        public Schedule Calculate(IEnumerable<Activity> activities)
        {
            if (!activities.Any()) return null;

            CalculateActivities(activities);
            return BuildProcessorResult()
                    .AddActivities(activities);
        }

        public Schedule Process(IEnumerable<Activity> activities, 
            Activity startingPoint, Activity endingActivity)
        {
            if (!activities.Any()) return null;
            
            var criticalPaths = CalculateCriticalPath(activities, startingPoint, endingActivity, ActivityDirection.Normal);
			if(!criticalPaths.Any())
				criticalPaths.AddRange(CalculateCriticalPath(activities, startingPoint, endingActivity, ActivityDirection.Reverse));

			var maxCriticalPath = GetMaxProjectBuffer(criticalPaths);
            var feedingBuffers = CalculateFeedingBuffers(activities, maxCriticalPath, startingPoint, endingActivity);

            return BuildProcessorResult()
                .AddActivities(activities)
                .AddCriticalPath(maxCriticalPath)
                .AddFeedingBuffers(feedingBuffers);
        }
        
        private List<FeedingBuffer> CalculateFeedingBuffers(IEnumerable<Activity> activities, CriticalPath maxCriticalPath,
            Activity startingActivity, Activity endingActivity)
        {
            var feedingBuffers = new List<FeedingBuffer>();

            if (maxCriticalPath == null) return feedingBuffers;

            var processFeedingBuffer = new List<KeyValuePair<int, float>>();
            var orderedProcesses = new List<OrderedProcess>();
            var orderedIndex = 0;
            var startDurProcWithStartAct = activities.Where(a => a.ProcessId == startingActivity.ProcessId).Select(x => x.FromDuration).Min();
            var startDurProcWithEndAct = activities.Where(a => a.ProcessId == endingActivity.ProcessId).Select(x => x.FromDuration).Min();

            activities.GroupBy(a => a.ProcessId)
                .Where(g => g.Select(x => x.FromDuration).Min() >= startDurProcWithStartAct &&
                g.Select(x => x.FromDuration).Min() <= startDurProcWithEndAct)
                .OrderBy(g => g.Min(a => a.FromDuration)).ToList()
                .ForEach(g => orderedProcesses.Add(new OrderedProcess
                {
                    order = orderedIndex++,
                    elements = g
                }));

            foreach (var proc in orderedProcesses)
            {
                float sumPreviousFbs = 0;
                var criticalActivitiesInProc = proc.elements.Intersect(maxCriticalPath.ActivityDirections.Select(ad => ad.Activity));
                var nonCriticalActivities = criticalActivitiesInProc.Any() ? proc.elements
                    .Where(act => act.FromDuration < criticalActivitiesInProc.Select(c => c.FromDuration).Min())
                    : proc.elements;

                if (!criticalActivitiesInProc.Any()) continue;

                var feedingBuffer = (float)Math.Sqrt(nonCriticalActivities
                    .Sum(a => Math.Pow(a.Duration - a.AggressiveDuration, 2)));

                processFeedingBuffer.Add(new KeyValuePair<int, float>(proc.order, feedingBuffer));

                if (processFeedingBuffer.Any(pfb => pfb.Key == proc.order - 1))
                {
                    sumPreviousFbs = processFeedingBuffer.Where(pfb => pfb.Key < proc.order).Sum(r => r.Value);

                    SetPreviousFeedingBufferShiftingToElements(proc.elements.ToList(), sumPreviousFbs);
                    SetPreviousFeedingBufferShiftingToElements(maxCriticalPath.ActivityDirections.Select(ad => ad.Activity)
                        .Intersect(proc.elements).ToList(), sumPreviousFbs);

                    
                }

                var link = GetLinkInCriticalPathForCurrentProcess(maxCriticalPath, proc);
                if (link != null)
                {
                    link.FeedingBuffer = new FeedingBuffer
                    {
                        StartingDuration = link.LinkDistance.StartingDuration,
                        PreviousFeedingBuffers = sumPreviousFbs,
                        Buffer = feedingBuffer,
                        StartingUnit = criticalActivitiesInProc.Max(a => link.Direction == ActivityDirection.Normal ?
                        a.StartingUnit + a.Units : a.StartingUnit)
                    };
                    link.LinkDistance.PreviousFeedingBuffers = sumPreviousFbs;
                    link.LinkDistance.FeedingBuffer = feedingBuffer;
                }                

                var maxDuration = proc.elements.Max(a => a.ToDuration);
                var lastProc = proc.elements.First(a => a.ToDuration == maxDuration);
                feedingBuffers.Add(new FeedingBuffer
                {
                    StartingDuration = lastProc.ToDuration,
                    PreviousFeedingBuffers = sumPreviousFbs,
                    Buffer = feedingBuffer,
                    StartingUnit = lastProc.ToUnit
                });
            }

            maxCriticalPath.ProjectBuffer = new ProjectBuffer
            {
                StartingDuration = maxCriticalPath.ProjectBuffer.StartingDuration + processFeedingBuffer.Sum(r => r.Value),
                StartingUnit = maxCriticalPath.ProjectBuffer.StartingUnit,
                Buffer = maxCriticalPath.ProjectBuffer.Buffer
            };

            return feedingBuffers;
        }

        private static ActivityWithDirection GetLinkInCriticalPathForCurrentProcess(CriticalPath maxCriticalPath, OrderedProcess proc)
        {
            return maxCriticalPath.ActivityDirections.Where(ad => ad.LinkDistance != null).Skip(proc.order)
                                    .FirstOrDefault();
        }

        private static void SetPreviousFeedingBufferShiftingToElements(IList<Activity> elements, float sumPreviousFbs)
        {
            elements.ToList().ForEach(a => a.ShiftDueToPreviousFeedingBuffers = sumPreviousFbs);
        }

        public CriticalPath GetMaxProjectBuffer(List<List<ActivityWithDirection>> criticalPaths)
        {
            if (!criticalPaths.Any()) return null;
            var criticalPath = new CriticalPath();
            var maxProjBuf = 0.0;

            criticalPaths.ForEach(cp =>
            {
                var projBuf = Math.Sqrt(cp.Where(a => a.LinkDistance == null)
                    .Sum(a => Math.Pow(a.Activity.Duration - a.Activity.AggressiveDuration, 2)));
                if (projBuf > maxProjBuf)
                {
                    maxProjBuf = projBuf;
                    criticalPath = new CriticalPath { ActivityDirections = cp.CloneLists() };
                }
            });
            var maxDuration = criticalPaths.First().Where(a => a.LinkDistance == null).Max(a => a.Activity.ToDuration);
            var maxUnit = criticalPaths.First().Where(a => a.LinkDistance == null && a.Activity.ToDuration == maxDuration).First().Activity.ToUnit;
            criticalPath.ProjectBuffer = new ProjectBuffer
            {
                StartingDuration = maxDuration,
                StartingUnit = maxUnit,
                Buffer = maxProjBuf
            };
            return criticalPath;
        }

        public List<List<ActivityWithDirection>> CalculateCriticalPath(IEnumerable<Activity> activities, 
            Activity startingActivity, Activity endingActivity, ActivityDirection direction)
        {
            var results = new List<List<ActivityWithDirection>>();
            var tempResults = new List<ActivityWithDirection>();
            var result = "";
            var visited = new Dictionary<int, bool>();
            activities.ToList().ForEach(l => visited.Add(l.Id, false));
            CalculateCriticalPath(activities, startingActivity, result, endingActivity, null, visited,
                tempResults.CloneLists(), results, direction, ActivityDirection.Normal);
            return results;
        }

        private void CalculateCriticalPath(IEnumerable<Activity> activities, Activity currentAct, string result,
            Activity endingActivity, float?[] deltaLink, Dictionary<int, bool> visited, List<ActivityWithDirection> tempResults,
            List<List<ActivityWithDirection>> results, ActivityDirection direction, ActivityDirection previousDirection)
        {
            if (visited[currentAct.Id]) return;

            visited[currentAct.Id] = true;

            if (deltaLink != null) tempResults.Add(new ActivityWithDirection
            {
                LinkDistance = new LinkDistance
                {
                    StartingDuration = deltaLink[0],
                    StartingUnit = deltaLink[1]
                },
                Direction = previousDirection
            });

            tempResults.Add(new ActivityWithDirection { Activity = currentAct, Direction = direction });

            if (currentAct.Id == endingActivity.Id)
            {
                results.Add(tempResults);
                return;
            }

            if (direction == ActivityDirection.Normal)
            {
                foreach (var next in activities.Where(l => Math.Abs(currentAct.ToDuration - l.FromDuration) < 0.02))
                {
                    var delta = currentAct.ToUnit - next.FromUnit;
                    if (delta >= 0 && !visited[next.Id])
                    {
                        float?[] dLink = null;
                        if (delta > 0) dLink = new float?[] { next.FromDuration, next.FromUnit };

                        CalculateCriticalPath(activities, next, result, endingActivity, dLink,
                            visited.ToDictionary(entry => entry.Key, entry => entry.Value),
                            tempResults.CloneLists(), results, ActivityDirection.Normal, ActivityDirection.Normal);
                    }
                }

                foreach (var next in activities.Where(l => Math.Abs(currentAct.ToDuration - l.ToDuration) < 0.02))
                {
                    var delta = currentAct.ToUnit - next.ToUnit;
                    if (delta >= 0 && !visited[next.Id])
                    {
                        float?[] dLink = null;
                        if (delta > 0) dLink = new float?[] { next.ToDuration, next.ToUnit };

                        CalculateCriticalPath(activities, next, result, endingActivity, dLink,
                            visited.ToDictionary(entry => entry.Key, entry => entry.Value),
                            tempResults.CloneLists(), results, ActivityDirection.Reverse, ActivityDirection.Normal);
                    }
                }
            }

            if (direction == ActivityDirection.Reverse)
            {
                foreach (var next in activities.Where(l => Math.Abs(currentAct.FromDuration - l.FromDuration) < 0.02))
                {
                    var delta = currentAct.FromUnit - next.FromUnit;
                    if (delta >= 0 && !visited[next.Id])
                    {
                        float?[] dLink = null;
                        if (delta > 0) dLink = new float?[] { next.FromDuration, next.FromUnit };

                        CalculateCriticalPath(activities, next, result, endingActivity, dLink,
                            visited.ToDictionary(entry => entry.Key, entry => entry.Value),
                            tempResults.CloneLists(), results, ActivityDirection.Normal, ActivityDirection.Reverse);
                    }
                }

                foreach (var next in activities.Where(l => Math.Abs(currentAct.FromDuration - l.ToDuration) < 0.02))
                {
                    var delta = currentAct.FromUnit - next.ToUnit;
                    if (delta >= 0 && !visited[next.Id])
                    {

                        float?[] dLink = null;
                        if (delta > 0) dLink = new float?[] { next.FromDuration, next.FromUnit };

                        CalculateCriticalPath(activities, next, result, endingActivity, dLink,
                            visited.ToDictionary(entry => entry.Key, entry => entry.Value),
                            tempResults.CloneLists(), results, ActivityDirection.Reverse, ActivityDirection.Reverse);
                    }
                }
            }
        }

        private void CalculateActivities(IEnumerable<Activity> activities)
        {
            var queue = new Queue<Activity>();
            var visited = new Dictionary<int, bool>();

            activities.ToList().ForEach(a =>
            {
                queue.Enqueue(a);
                visited.Add(a.Id, false);
            });

            while (queue.Count != 0)
            {
                var item = queue.Dequeue();

                if (item.ActivityDependencies.Any(d => !visited[d.Id]))
                {
                    queue.Enqueue(item);
                    continue;
                }

                if (item.inputProdRate)
                {
                    item.Duration = item.Units / item.SafeProductivityRate;
                    item.AggressiveDuration = item.Units / item.AggressiveProductivityRate;
                }
                else
                {
                    item.SafeProductivityRate = item.Units / item.Duration;
                    item.AggressiveProductivityRate = item.Units / item.AggressiveDuration;
                }

                item.StartingDuration = 0;
                item.StartingUnit = 0;

                var itemWithMax = item.ActivityDependencies.Any() ? item.ActivityDependencies
                    .OrderByDescending(d => d.StartingDuration + d.DurationDefault).ToList()
                    : new List<Activity>();

                if (itemWithMax.Any())
                {
                    item.StartingDuration = itemWithMax.First().StartingDuration + itemWithMax.First().DurationDefault;
                    item.StartingUnit = itemWithMax.First().StartingUnit + itemWithMax.First().Units;
                }

                item.StartingDuration += item.StartToFinish;
                item.StartingUnit += item.UnitDelta;
                visited[item.Id] = true;
            }
        }

        private Schedule BuildProcessorResult()
        {
            return new Schedule();
        }
    }
}
