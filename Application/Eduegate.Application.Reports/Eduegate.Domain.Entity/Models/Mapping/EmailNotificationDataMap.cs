using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmailNotificationDataMap : EntityTypeConfiguration<EmailNotificationData>
    {
        public EmailNotificationDataMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailMetaDataIID);

            // Properties
            this.Property(t => t.EmailData);
                //.IsRequired();

            this.Property(t => t.TemplateName)
                .HasMaxLength(50);

            this.Property(t => t.Subject)
                .HasMaxLength(500);

            this.Property(t => t.FromEmailID)
                .HasMaxLength(1000);

            this.Property(t => t.ToEmailID)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EmailNotificationData", "notification");
            this.Property(t => t.EmailMetaDataIID).HasColumnName("EmailMetaDataIID");
            this.Property(t => t.NotificationQueueID).HasColumnName("NotificationQueueID");
            this.Property(t => t.EmailNotificationTypeID).HasColumnName("EmailNotificationTypeID");
            this.Property(t => t.EmailData).HasColumnName("EmailData");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.FromEmailID).HasColumnName("FromEmailID");
            this.Property(t => t.ToEmailID).HasColumnName("ToEmailID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.SerializedJsonParameters).HasColumnName("SerializedJsonParameters");

            // Relationships
            this.HasRequired(t => t.EmailNotificationType)
                .WithMany(t => t.EmailNotificationDatas)
                .HasForeignKey(d => d.EmailNotificationTypeID);

        }
    }
}
