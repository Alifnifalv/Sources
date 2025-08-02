using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationsQueueMap : EntityTypeConfiguration<NotificationsQueue>
    {
        public NotificationsQueueMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationQueueIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("NotificationsQueue", "notification");
            this.Property(t => t.NotificationQueueIID).HasColumnName("NotificationQueueIID");
            this.Property(t => t.NotificationTypeID).HasColumnName("NotificationTypeID");
            this.Property(t => t.IsReprocess).HasColumnName("IsReprocess");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.NotificationType)
                .WithMany(t => t.NotificationsQueues)
                .HasForeignKey(d => d.NotificationTypeID);

        }
    }
}
