using System.Linq;

namespace SchedulePath.Models
{
    internal class OrderedProcess
    {
        public IGrouping<int, Activity> elements { get; set; }
        public int order { get; set; }
    }
}