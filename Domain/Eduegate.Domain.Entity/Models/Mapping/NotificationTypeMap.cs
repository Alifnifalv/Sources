using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationTypeMap : EntityTypeConfiguration<NotificationType>
    {
        public NotificationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationTypeID);

            // Properties
            this.Property(t => t.NotificationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("NotificationTypes", "notification");
            this.Property(t => t.NotificationTypeID).HasColumnName("NotificationTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
