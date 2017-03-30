using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePath.Models
{
    public class LinkPaths
    {
        public double FromLink { get; set; }
        public double ToLink { get; set; }
        public List<string> Paths { get; set; }
        public bool Reverse { get; set; }
    }

    public class LinkGroup
    {
        public double Duration { get; set;}
        public List<Activity> Activities { get; set; }
    }
}
