using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmailNotificationTypeMap : EntityTypeConfiguration<EmailNotificationType>
    {
        public EmailNotificationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailNotificationTypeID);

            // Properties
            this.Property(t => t.EmailNotificationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.EmailTemplateFilePath)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.TimeStamp)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.ModifiedBy)
                .HasMaxLength(100);

            this.Property(t => t.EmailSubject)
                .HasMaxLength(500);

            this.Property(t => t.ToCCEmailID)
                .HasMaxLength(200);

            this.Property(t => t.ToBCCEmailID)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("EmailNotificationTypes", "notification");
            this.Property(t => t.EmailNotificationTypeID).HasColumnName("EmailNotificationTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.EmailTemplateFilePath).HasColumnName("EmailTemplateFilePath");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.TimeStamp).HasColumnName("TimeStamp");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.EmailSubject).HasColumnName("EmailSubject");
            this.Property(t => t.ToCCEmailID).HasColumnName("ToCCEmailID");
            this.Property(t => t.ToBCCEmailID).HasColumnName("ToBCCEmailID");
        }
    }
}
