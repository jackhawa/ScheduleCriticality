using System;
using System.Collections.Generic;
using System.Linq;
using SchedulePath.Services;

namespace SchedulePath.Models
{
    public class ProcessorResult
    {
        public IEnumerable<Activity> Activities { get; set; }
        public CriticalPath CriticalPath { get; set; }
        public List<FeedingBuffer> FeedingBuffers { get; set; }
        public ProcessorResult AddCriticalPath(CriticalPath criticalPath)
        {
            CriticalPath = criticalPath;
            return this;
        }

        internal ProcessorResult AddFeedingBuffers(List<FeedingBuffer> feedingBuffers)
        {
            FeedingBuffers = feedingBuffers;
            return this;
        }

        public ProcessorResult AddActivities(IEnumerable<Activity> activities)
        {
            Activities = activities;
            return this;
        }
    }
}
