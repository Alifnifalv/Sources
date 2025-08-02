using Eduegate.Domain.Entity.Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Logging
{
    public partial class EduegatedERP_LoggerContext : DbContext
    {

        public EduegatedERP_LoggerContext()
        {
        }

        public EduegatedERP_LoggerContext(DbContextOptions<EduegatedERP_LoggerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultLoggerConnectionString());
            }
        }

        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<CatalogLogger> CatalogLoggers { get; set; }

        public virtual DbSet<ActionStatus> ActionStatuses { get; set; }
        public virtual DbSet<ActionType> ActionTypes { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }

        public virtual DbSet<Exceptions> Exceptions { get; set; }
        public virtual DbSet<ExceptionType> ExceptionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionStatus>(entity =>
            {
                entity.Property(e => e.ActionStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.Property(e => e.ActionTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasOne(d => d.ActionStatus)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ActionStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activities_ActionStatuses");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ActionTypeID)
                    .HasConstraintName("FK_Activities_ActionTypes");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ActivityTypeID)
                    .HasConstraintName("FK_Activities_ActivityTypes");
            });

            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.Property(e => e.ActivityTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Exceptions>(entity =>
            {
                entity.HasOne(d => d.ExceptionType)
                    .WithMany(p => p.Exceptions)
                    .HasForeignKey(d => d.ExceptionTypeID)
                    .HasConstraintName("FK_Exceptions_ExceptionTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}