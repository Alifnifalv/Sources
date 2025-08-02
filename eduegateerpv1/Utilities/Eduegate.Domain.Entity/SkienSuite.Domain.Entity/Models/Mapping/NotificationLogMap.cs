using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationLogMap : EntityTypeConfiguration<NotificationLog>
    {
        public NotificationLogMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationLogsIID);

            // Properties
            this.Property(t => t.Result)
                .HasMaxLength(100);

            this.Property(t => t.Reason)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("NotificationLogs", "notification");
            this.Property(t => t.NotificationLogsIID).HasColumnName("NotificationLogsIID");
            this.Property(t => t.NotificationTypeID).HasColumnName("NotificationTypeID");
            this.Property(t => t.NotificationQueueID).HasColumnName("NotificationQueueID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Result).HasColumnName("Result");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.NotificationStatusID).HasColumnName("NotificationStatusID");

            // Relationships
            this.HasRequired(t => t.NotificationStatus)
                .WithMany(t => t.NotificationLogs)
                .HasForeignKey(d => d.NotificationStatusID);
            this.HasRequired(t => t.NotificationType)
                .WithMany(t => t.NotificationLogs)
                .HasForeignKey(d => d.NotificationTypeID);

        }
    }
}
