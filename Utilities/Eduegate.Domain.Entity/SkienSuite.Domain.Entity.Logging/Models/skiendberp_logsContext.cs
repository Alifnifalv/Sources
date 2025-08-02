using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Eduegate.Domain.Entity.Logging.Models.Mapping;

namespace Eduegate.Domain.Entity.Logging.Models
{
    public partial class skiendberp_logsContext : DbContext
    {
        static skiendberp_logsContext()
        {
            Database.SetInitializer<skiendberp_logsContext>(null);
        }

        public skiendberp_logsContext()
            : base("Name=skiendberp_logsContext")
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActivityMap());
            modelBuilder.Configurations.Add(new ActivityTypeMap());
        }
    }
}
