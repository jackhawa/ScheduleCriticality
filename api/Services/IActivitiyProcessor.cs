using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchedulePath.Models;

namespace SchedulePath.Services
{
    public interface IActivityProcessor
    {
        ProcessorResult Process(bool withCriticalPath, IEnumerable<Activity> activities, Activity startingPoint);
        List<List<ActivityWithDirection>> CalculateCriticalPath(IEnumerable<Activity> activities, Activity startingPoint);
        CriticalPath GetMaxProjectBuffer(List<List<ActivityWithDirection>> criticalPaths);
    }
}
