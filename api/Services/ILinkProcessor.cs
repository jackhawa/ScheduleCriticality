using SchedulePath.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePath.Services
{
    public interface ILinkProcessor
    {
        void Process(IEnumerable<Activity> activities, Link link, 
            ref Schedule upwardProcessorResult, ref Schedule downwardProcessorResult);
    }
}
