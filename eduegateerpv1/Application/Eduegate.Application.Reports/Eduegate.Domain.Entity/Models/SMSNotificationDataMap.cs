using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SMSNotificationDataMap : EntityTypeConfiguration<SMSNotificationData>
    {
        public SMSNotificationDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SMSNotificationDataIID, t.NotificationQueueID, t.SMSNotificationTypeID });

            // Properties
            this.Property(t => t.SMSNotificationDataIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NotificationQueueID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SMSNotificationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TemplateName)
                .HasMaxLength(50);

            this.Property(t => t.Subject)
                .HasMaxLength(500);

            this.Property(t => t.FromMobileNumber)
                .HasMaxLength(1000);

            this.Property(t => t.ToMobileNumber)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SMSNotificationDatas", "notification");
            this.Property(t => t.SMSNotificationDataIID).HasColumnName("SMSNotificationDataIID");
            this.Property(t => t.NotificationQueueID).HasColumnName("NotificationQueueID");
            this.Property(t => t.SMSNotificationTypeID).HasColumnName("SMSNotificationTypeID");
            this.Property(t => t.SMSContent).HasColumnName("SMSContent");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.FromMobileNumber).HasColumnName("FromMobileNumber");
            this.Property(t => t.ToMobileNumber).HasColumnName("ToMobileNumber");
            this.Property(t => t.SerializedJsonParameters).HasColumnName("SerializedJsonParameters");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.SMSNotificationType)
                .WithMany(t => t.SMSNotificationDatas)
                .HasForeignKey(d => d.SMSNotificationTypeID);

        }
    }
}
