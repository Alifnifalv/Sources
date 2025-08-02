using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TrackerStatusMap : EntityTypeConfiguration<TrackerStatus>
    {
        public TrackerStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.TrackerStatusID);

            // Properties
            this.Property(t => t.TrackerStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TrackerStatuses", "sync");
            this.Property(t => t.TrackerStatusID).HasColumnName("TrackerStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
