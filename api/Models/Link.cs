using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePath.Models
{
    public class Link
    {
        public int Id { get; set; }
        public int UpwardActivity { get; set; }
        public int DownwardActivity { get; set; }
        public float TimePeriod { get; set; }
    }
}
