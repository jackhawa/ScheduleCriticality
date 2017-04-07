using SchedulePath.Models;
using SchedulePath.Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SchedulePath.Services
{
    public class ActivityService : IActivityService
    {
        private ICepRepository _repository;
        private IActivityProcessor _activityProcessor;
        private ILinkProcessor _linkProcessor;
        private IGraphProcessor _graphProcessor;
        public ActivityService(ICepRepository repository, 
            IActivityProcessor processor,
            ILinkProcessor linkProcessor,
            IGraphProcessor graphProcessor)
        {
            _repository = repository;
            _activityProcessor = processor;
            _linkProcessor = linkProcessor;
            _graphProcessor = graphProcessor;
        }


        public GraphConfig Process(bool withCriticalPath)
        {
            var activities = _repository.GetActivities();
            var link = _repository.GetLink();

            var upwardActivities = activities.Where(a => a.Section == ActivitySection.UPWARD);
            var downwardActivities = activities.Where(a => a.Section == ActivitySection.DOWNWARD);

            var upperSectionResult = _activityProcessor.Process(withCriticalPath, upwardActivities,
                upwardActivities.First(a => string.IsNullOrEmpty(a.Dependencies)));
            var lowerSectionResult = _activityProcessor.Process(withCriticalPath, downwardActivities,
                downwardActivities.First(a => string.IsNullOrEmpty(a.Dependencies)));

            if(withCriticalPath)
                _linkProcessor.Process(activities, link, ref upperSectionResult, ref lowerSectionResult);

            return _graphProcessor.ProcessGraph(withCriticalPath, upperSectionResult, lowerSectionResult);
        }
        
        public IEnumerable<Activity> GetActivities()
        {
            var activities = _repository.GetActivities();
            activities.ToList().ForEach(item =>

            {
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
            });
            return activities;
        }

        public IEnumerable<Process> GetProcesses()
        {
            return _repository.GetProcesses();
        }

        public void AddActivity(ActivityRequest request)
        {
            _repository.AddActivity(new Activity
            {
                Name = request.Name,
                ProcessId = request.ProcessId,
                Units = request.Units,
                SafeProductivityRate = request.SafeProductivityRate,
                AggressiveProductivityRate = request.AggressiveProductivityRate,
                DurationFunction = request.DurationFunction,
                UnitDelta = request.UnitDelta,
                StartToFinish = request.StartToFinish,
                Dependencies = request.Dependencies,
                Duration = request.Duration,
                AggressiveDuration = request.AggressiveDuration,
                inputProdRate = request.InputProdRate,
                Section = request.Section
            });
        }

        public void UpdateActivity(ActivityRequest request)
        {
            var activity = GetActivities().First(a => a.Id == request.Id);
            activity.Name = request.Name;
            activity.ProcessId = request.ProcessId;
            activity.Units = request.Units;
            activity.SafeProductivityRate = request.SafeProductivityRate;
            activity.AggressiveProductivityRate = request.AggressiveProductivityRate;
            activity.DurationFunction = request.DurationFunction;
            activity.UnitDelta = request.UnitDelta;
            activity.StartToFinish = request.StartToFinish;
            activity.Dependencies = request.Dependencies;
            activity.Duration = request.Duration;
            activity.AggressiveDuration = request.AggressiveDuration;
            activity.inputProdRate = request.InputProdRate;
            activity.Section = request.Section;
            _repository.UpdateActivity(activity);
        }

        public void DeleteActivity(int id)
        {
            if(_repository.GetActivities().Any(a => a.ActivityDependencies.Any(d => d.Id == id)))
            {
                throw new Exception("Cannot delete activity. It is a dependency for another.");
            }
            _repository.DeleteActivity(id);
        }
    }
}
