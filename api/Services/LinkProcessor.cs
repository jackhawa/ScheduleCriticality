using System.Collections.Generic;
using System.Linq;
using SchedulePath.Models;
using SchedulePath.Helper;

namespace SchedulePath.Services
{
    public class LinkProcessor : ILinkProcessor
    {
        public IActivityProcessor _activityProcessor;

        public LinkProcessor(IActivityProcessor activityProcessor)
        {
            _activityProcessor = activityProcessor;
        }
        public void Process(IEnumerable<Activity> activities, Link link,
            ref ProcessorResult upwardProcessorResult, ref ProcessorResult downwardProcessorResult)
        {
            if(!activities.Any()) return;

            var upperActivity = activities.SingleOrDefault(a => link.UpwardActivity == a.Id);
            var lowerActivity = activities.SingleOrDefault(a => link.DownwardActivity == a.Id);

            if(upperActivity == null || lowerActivity == null) return;

            //Determine controlling link shift
            var delta = upperActivity.ToDuration + upperActivity.FeedingBuffer +
                link.TimePeriod - lowerActivity.FromDuration - lowerActivity.FeedingBuffer;

            //Perform shifting according to controlling link
            downwardProcessorResult.Activities.ToList().ForEach(a => a.LinkShift = delta);
            downwardProcessorResult.CriticalPath.ActivityDirections.ToList()
                .ForEach(a => a.LinkShift = delta);
            downwardProcessorResult.CriticalPath.ProjectBuffer.ToList().ForEach(a => a[0] += delta);
            downwardProcessorResult.FeedingBuffers.ToList().ForEach(f => f.ToList().ForEach(b => b[0] += delta));

            //Add controlling link
            var lastActivityInUpward = upwardProcessorResult.CriticalPath.ActivityDirections.Last();
            upwardProcessorResult.CriticalPath.ActivityDirections.Add(new ActivityWithDirection
            {
                FeedingBuffer = new float?[] { upperActivity.ToDuration + link.TimePeriod + upperActivity.FeedingBuffer,
                upperActivity.ToUnit },
                LinkDistance = new float?[] { upperActivity.ToDuration + link.TimePeriod + upperActivity.FeedingBuffer,
                lowerActivity.FromUnit },
                Flip = -1
            });

            /*
            //Recalculate critical path of right side of controlling link
            var continuationActivities = activities.Where(a => a.Section == ActivitySection.DOWNWARD
            && (a.FromDuration + a.FeedingBuffer + a.LinkShift) > (upperActivity.ToDuration + link.TimePeriod)).Select(a => a.Clone());

            var contProcessorResult = _activityProcessor.Process(true, activities.Where(a => a.Section == ActivitySection.DOWNWARD), 
                continuationActivities.Single(a => a.Id == link.DownwardActivity));

            contProcessorResult.CriticalPath.ActivityDirections.Where(ad => ad.Activity != null).ToList().ForEach(ad => {
                ad.Activity.Flip = -1;
            });

            contProcessorResult.CriticalPath.ActivityDirections.Where(ad => ad.Activity == null).ToList().ForEach(ad => {
                ad.LinkShift = delta;
                ad.Flip = -1;
            });

            //Append right side critical path to complete upper section critical path
            upwardProcessorResult.CriticalPath.ActivityDirections.AddRange(contProcessorResult.CriticalPath.ActivityDirections);*/
        }
    }
}
