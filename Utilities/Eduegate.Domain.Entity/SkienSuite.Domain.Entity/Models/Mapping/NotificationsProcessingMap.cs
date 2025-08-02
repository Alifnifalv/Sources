using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationsProcessingMap : EntityTypeConfiguration<NotificationsProcessing>
    {
        public NotificationsProcessingMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationProcessingIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("NotificationsProcessing", "notification");
            this.Property(t => t.NotificationProcessingIID).HasColumnName("NotificationProcessingIID");
            this.Property(t => t.NotificationTypeID).HasColumnName("NotificationTypeID");
            this.Property(t => t.NotificationQueueID).HasColumnName("NotificationQueueID");
            this.Property(t => t.IsReprocess).HasColumnName("IsReprocess");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.NotificationType)
                .WithMany(t => t.NotificationsProcessings)
                .HasForeignKey(d => d.NotificationTypeID);

        }
    }
}
