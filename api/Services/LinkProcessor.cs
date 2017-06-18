using System;
using System.Collections.Generic;
using System.Linq;
using SchedulePath.Models;

namespace SchedulePath.Services
{
    public class LinkProcessor : ILinkProcessor
    {
        public IActivityProcessor _activityProcessor;

        public LinkProcessor(IActivityProcessor activityProcessor)
        {
            _activityProcessor = activityProcessor;
        }
        public void Process(IEnumerable<Activity> activities, LinkWithActivity link,
            ref Schedule upwardProcessorResult, ref Schedule downwardProcessorResult)
        {
            if (!activities.Any() || link.UpwardAct == null || link.DownwardAct == null) return;

            var delta = link.UpwardAct.ToDuration + link.UpwardAct.FeedingBuffer +
                link.TimePeriod - link.DownwardAct.FromDuration - link.DownwardAct.FeedingBuffer;

            downwardProcessorResult.ShiftSchedule(delta);

            //Add controlling link
            var lastActivityInUpward = upwardProcessorResult.CriticalPath.ActivityDirections.Last();
            upwardProcessorResult.CriticalPath.ActivityDirections.Add(new ActivityWithDirection
            {
                FeedingBuffer = new FeedingBuffer
                {
                    StartingDuration = link.UpwardAct.ToDuration,
                    Buffer = link.UpwardAct.FeedingBuffer,
                    TimePeriod = link.TimePeriod,
                    StartingUnit = link.UpwardAct.ToUnit
                },
                LinkDistance = new LinkDistance {
                    StartingDuration = link.UpwardAct.ToDuration,
                    TimePeriod = link.TimePeriod,
                    FeedingBuffer = link.UpwardAct.FeedingBuffer,
                    StartingUnit = link.DownwardAct.FromUnit
                },
                Flip = -1
            });

            var totalProjectBuffer = Math.Sqrt(upwardProcessorResult.CriticalPath.ActivityDirections.Where(a => a.LinkDistance == null)
                    .Sum(a => Math.Pow(a.Activity.AggressiveDuration - a.Activity.Duration, 2)) + 
                    downwardProcessorResult.CriticalPath.ActivityDirections.Where(a => a.LinkDistance == null)
                    .Sum(a => Math.Pow(a.Activity.AggressiveDuration - a.Activity.Duration, 2)));

            upwardProcessorResult.CriticalPath.ProjectBuffer.Buffer = 0;
            downwardProcessorResult.CriticalPath.ProjectBuffer.Buffer = totalProjectBuffer;            
        }
    }
}
