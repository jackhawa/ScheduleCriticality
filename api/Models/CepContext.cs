using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchedulePath.Services;

namespace SchedulePath.Models
{
    public class CepContext: DbContext
    {
        private IConfigurationRoot _config;
        private ILoggingManager _logger;
        public CepContext(IConfigurationRoot config, DbContextOptions options, ILoggingManager logger): base(options)
        {
            _config = config;
            _logger = logger;
        }

        public DbSet<Process> Processes { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Link> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Logging:ConnectionStrings:CepContextConnection"]);
            _logger.Log("CepContextConnection: " + _config["Logging:ConnectionStrings:CepContextConnection"]);
        }
    }
}
