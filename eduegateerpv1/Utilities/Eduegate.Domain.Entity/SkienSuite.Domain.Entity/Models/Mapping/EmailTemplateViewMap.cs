using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmailTemplateViewMap : EntityTypeConfiguration<EmailTemplateView>
    {
        public EmailTemplateViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EmailNotificationTypeID, t.Name });

            // Properties
            this.Property(t => t.EmailNotificationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.EmailSubject)
                .HasMaxLength(500);

            this.Property(t => t.ToCCEmailID)
                .HasMaxLength(200);

            this.Property(t => t.ToBCCEmailID)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("EmailTemplateView", "notification");
            this.Property(t => t.EmailNotificationTypeID).HasColumnName("EmailNotificationTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.EmailSubject).HasColumnName("EmailSubject");
            this.Property(t => t.ToCCEmailID).HasColumnName("ToCCEmailID");
            this.Property(t => t.ToBCCEmailID).HasColumnName("ToBCCEmailID");
        }
    }
}
