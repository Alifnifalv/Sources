using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationViewMap : EntityTypeConfiguration<NotificationView>
    {
        public NotificationViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NotificationQueueID, t.Name });

            // Properties
            this.Property(t => t.NotificationQueueID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.NotificationStatusName)
                .HasMaxLength(50);

            this.Property(t => t.ToEmailID)
                .HasMaxLength(1000);

            this.Property(t => t.FromEmailID)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("NotificationView", "notification");
            this.Property(t => t.NotificationQueueID).HasColumnName("NotificationQueueID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NotificationStatusName).HasColumnName("NotificationStatusName");
            this.Property(t => t.ToEmailID).HasColumnName("ToEmailID");
            this.Property(t => t.FromEmailID).HasColumnName("FromEmailID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
