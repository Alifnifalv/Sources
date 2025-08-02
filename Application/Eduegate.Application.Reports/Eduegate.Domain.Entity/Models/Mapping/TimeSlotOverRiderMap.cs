using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TimeSlotOverRiderMap : EntityTypeConfiguration<TimeSlotOverRider>
    {
        public TimeSlotOverRiderMap()
        {
            // Primary Key
            this.HasKey(t => t.OverrideID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TimeSlotOverRider", "cms");
            this.Property(t => t.OverrideID).HasColumnName("OverrideID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.Cutoff).HasColumnName("Cutoff");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Exclude).HasColumnName("Exclude");

            // Relationships
            this.HasOptional(t => t.SupplierMaster)
                .WithMany(t => t.TimeSlotOverRiders)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
