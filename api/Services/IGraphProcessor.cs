using SchedulePath.Models;

namespace SchedulePath.Services
{
    public interface IGraphProcessor
    {
        GraphConfig ProcessGraph(bool withCriticalPath, ProcessorResult upperResult, ProcessorResult lowerResult);
    }
}
