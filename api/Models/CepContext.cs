using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SchedulePath.Models
{
    public class CepContext: DbContext
    {
        private IConfigurationRoot _config;
        public CepContext(IConfigurationRoot config, DbContextOptions options): base(options)
        {
            _config = config;
        }

        public DbSet<Process> Processes { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Link> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Logging:ConnectionStrings:CepContextConnection"]);
        }
    }
}
