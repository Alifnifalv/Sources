using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationStatusMap : EntityTypeConfiguration<NotificationStatus>
    {
        public NotificationStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationStatusID);

            // Properties
            this.Property(t => t.NotificationStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NotificationStatuses", "notification");
            this.Property(t => t.NotificationStatusID).HasColumnName("NotificationStatusID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
