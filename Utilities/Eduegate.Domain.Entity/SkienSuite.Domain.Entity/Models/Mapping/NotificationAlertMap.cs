using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotificationAlertMap : EntityTypeConfiguration<NotificationAlert>
    {
        public NotificationAlertMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationAlertIID);

            // Properties
            this.Property(t => t.Message)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("NotificationAlerts", "notification");
            this.Property(t => t.NotificationAlertIID).HasColumnName("NotificationAlertIID");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.FromLoginID).HasColumnName("FromLoginID");
            this.Property(t => t.ToLoginID).HasColumnName("ToLoginID");
            this.Property(t => t.NotificationDate).HasColumnName("NotificationDate");
            this.Property(t => t.AlertStatusID).HasColumnName("AlertStatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.AlertTypeID).HasColumnName("AlertTypeID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.NotificationAlerts)
                .HasForeignKey(d => d.FromLoginID);
            this.HasOptional(t => t.Login1)
                .WithMany(t => t.NotificationAlerts1)
                .HasForeignKey(d => d.ToLoginID);
            this.HasOptional(t => t.AlertStatus)
                .WithMany(t => t.NotificationAlerts)
                .HasForeignKey(d => d.AlertStatusID);

        }
    }
}
