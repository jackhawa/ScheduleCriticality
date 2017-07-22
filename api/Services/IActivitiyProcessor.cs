using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchedulePath.Models;

namespace SchedulePath.Services
{
    public interface IActivityProcessor
    {
        Schedule Calculate(IEnumerable<Activity> activities);
        Schedule Process(IEnumerable<Activity> activities, 
            Activity startingPoint, Activity endingActivity);
        List<List<ActivityWithDirection>> CalculateCriticalPath(IEnumerable<Activity> activities, 
            Activity startingPoint, Activity endingActivity);
        CriticalPath GetMaxProjectBuffer(List<List<ActivityWithDirection>> criticalPaths);
    }
}
