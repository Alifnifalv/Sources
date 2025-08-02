using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationAlertViewMap : EntityTypeConfiguration<NotificationAlertView>
    {
        public NotificationAlertViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NotificationAlertIID, t.Name, t.actionrequired, t.Viewdocuments });

            // Properties
            this.Property(t => t.NotificationAlertIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Message)
                .HasMaxLength(1000);

            this.Property(t => t.actionrequired)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Viewdocuments)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NotificationAlertView", "notification");
            this.Property(t => t.NotificationAlertIID).HasColumnName("NotificationAlertIID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.FromLoginID).HasColumnName("FromLoginID");
            this.Property(t => t.ToLoginID).HasColumnName("ToLoginID");
            this.Property(t => t.actionrequired).HasColumnName("actionrequired");
            this.Property(t => t.Viewdocuments).HasColumnName("Viewdocuments");
        }
    }
}
