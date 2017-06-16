using System.Collections.Generic;

namespace SchedulePath.Models
{
    public class CriticalPath
    {
        public List<ActivityWithDirection> ActivityDirections { get; set; }
        public ProjectBuffer ProjectBuffer { get; set; }
    }
}
