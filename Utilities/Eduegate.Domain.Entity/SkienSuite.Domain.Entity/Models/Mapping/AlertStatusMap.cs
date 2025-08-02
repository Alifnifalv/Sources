using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AlertStatusMap : EntityTypeConfiguration<AlertStatus>
    {
        public AlertStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.AlertStatusID);

            // Properties
            this.Property(t => t.AlertStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AlertStatuses", "notification");
            this.Property(t => t.AlertStatusID).HasColumnName("AlertStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
