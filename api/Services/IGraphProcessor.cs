using SchedulePath.Models;

namespace SchedulePath.Services
{
    public interface IGraphProcessor
    {
        GraphConfig ProcessGraph(bool withCriticalPath, Schedule upperResult, Schedule lowerResult);
    }
}
