using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulePath.Models
{
    [NotMapped]
    public class ActivityGraph
    {
        public ActivityGraph()
        {
            Descendents = new List<ActivityGraph>();
        }
        public Activity ParentNode { get; set; }
        public Activity CurrentNode { get; set; }
        public ICollection<ActivityGraph> Descendents { get; set; }
    }
}
