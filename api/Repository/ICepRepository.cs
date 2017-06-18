using SchedulePath.Models;
using System.Collections.Generic;

namespace SchedulePath.Repository
{
    public interface ICepRepository
    {
        IEnumerable<Activity> GetActivities();
        void AddActivity(Activity activity);
        void UpdateActivity(Activity activity);
        void DeleteActivity(int id);
        IEnumerable<Process> GetProcesses();
        void AddProcess(Process process);
        void UpdateProcess(Process process);
        void DeleteProcess(int id);
        LinkWithActivity GetLink();
        void AddLink(Link request);
        void UpdateLink(Link request);
        void DeleteLink(int id);
    }
}
