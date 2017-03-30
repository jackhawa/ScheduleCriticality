using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchedulePath.Models;

namespace SchedulePath.Services
{
    public interface IActivityService
    {
        IEnumerable<Activity> GetActivities();
        IEnumerable<Process> GetProcesses();
        GraphConfig Process(bool withCriticalPath);
        void AddActivity(ActivityRequest activity);
        void UpdateActivity(ActivityRequest request);
        void DeleteActivity(int id);
    }
}
