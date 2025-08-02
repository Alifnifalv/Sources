using Eduegate.Domain.Entity.Schedule.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Schedule
{
    public partial class EduegatedERP_ScheduleContext : DbContext
    {
        public EduegatedERP_ScheduleContext()
        {
        }

        public EduegatedERP_ScheduleContext(DbContextOptions<EduegatedERP_ScheduleContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public DbSet<vDriverSchedule> vDriverSchedules { get; set; }
        public DbSet<vMaidSchedule> vMaidSchedules { get; set; }
        public DbSet<DriverSchedule> DriverSchedules { get; set; }
        public DbSet<Despatch> Despatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new vDriverScheduleMap());
            //modelBuilder.Configurations.Add(new vMaidScheduleMap());

            //modelBuilder.Configurations.Add(new DriverSchedulesMap());
            //modelBuilder.Configurations.Add(new DespatchMap());
            //base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}