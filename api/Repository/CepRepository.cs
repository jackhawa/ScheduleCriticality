using System;
using System.Collections.Generic;
using System.Linq;
using SchedulePath.Models;

namespace SchedulePath.Repository
{
    public class CepRepository : ICepRepository
    {
        CepContext _context;
        public CepRepository(CepContext context)
        {
            _context = context;
        }

        public void AddActivity(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public void AddProcess(Process process)
        {
            _context.Processes.Add(process);
            _context.SaveChanges();
        }

        public IEnumerable<Activity> GetActivities()
        {
            var activities = _context.Activities.ToList();
            var processes = _context.Processes.ToList();
            foreach (var activity in activities)
            {
                var dependencies = activity.Dependencies.Split(',');
                dependencies.Where(d => !string.IsNullOrEmpty(d)).ToList().ForEach(d =>
                {
                    activity.ActivityDependencies
                        .Add(activities.First(a => a.Id == Convert.ToInt32(d)));
                });
                var process = processes.FirstOrDefault(p => p.Id == activity.ProcessId);
                if(process != null)
                {
                    activity.ProcessName = process.Name;
                }
            }
            return activities;
        }

        public IEnumerable<Process> GetProcesses()
        {
            return _context.Processes.ToList();
        }

        public void UpdateActivity(Activity activity)
        {
            _context.Activities.Update(activity);
            _context.SaveChanges();
        }

        public void DeleteActivity(int id)
        {
            var activity = _context.Activities.First(a => a.Id == id);
            _context.Activities.Remove(activity);
            _context.SaveChanges();
        }

        public void UpdateProcess(Process process)
        {
            _context.Processes.Update(process);
            _context.SaveChanges();
        }

        public void DeleteProcess(int id)
        {
            var process = _context.Processes.First(a => a.Id == id);
            _context.Processes.Remove(process);
            _context.SaveChanges();
        }

        public Link GetLink()
        {
            var links = _context.Links;
            if (links.Any())
                return links.Single();
            return null;
        }

        public void AddLink(Link link)
        {
            _context.Links.Add(link);
            _context.SaveChanges();
        }

        public void UpdateLink(Link link)
        {
            _context.Links.RemoveRange(_context.Links);
            _context.Links.Add(link);
            _context.SaveChanges();
        }

        public void DeleteLink(int id)
        {
            var link = _context.Links.Single();
            _context.Links.Remove(link);
            _context.SaveChanges();
        }
    }
}
