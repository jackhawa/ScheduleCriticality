using System;
using System.Collections.Generic;
using System.Linq;
using SchedulePath.Services;

namespace SchedulePath.Models
{
    public class Schedule
    {
        public IEnumerable<Activity> Activities { get; set; }
        public CriticalPath CriticalPath { get; set; }
        public List<FeedingBuffer> FeedingBuffers { get; set; }

        public Schedule AddCriticalPath(CriticalPath criticalPath)
        {
            CriticalPath = criticalPath;
            return this;
        }

        internal Schedule AddFeedingBuffers(List<FeedingBuffer> feedingBuffers)
        {
            FeedingBuffers = feedingBuffers;
            return this;
        }

        public Schedule AddActivities(IEnumerable<Activity> activities)
        {
            Activities = activities;
            return this;
        }

        public void ShiftSchedule(float delta)
        {
            Activities.ToList().ForEach(a => a.LinkShift = delta);
            CriticalPath.ActivityDirections.ToList()
                .ForEach(a => a.LinkShift = delta);
            CriticalPath.ProjectBuffer.ControllingLinkShift = delta;
            FeedingBuffers.ToList().ForEach(f => f.ControllingLinkShift = delta);
        }
    }
}
