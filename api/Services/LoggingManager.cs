using Microsoft.Extensions.Logging;

namespace SchedulePath.Services
{
    public class LoggingManager : ILoggingManager
    {
        private ILogger _logger;

        public LoggingManager(ILoggerFactory loggerFactory){ 
            loggerFactory.AddConsole();
            _logger = loggerFactory.CreateLogger("MyLogger");
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}