using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TimeSlotMasterMap : EntityTypeConfiguration<TimeSlotMaster>
    {
        public TimeSlotMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.TimeSlotID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TimeSlotMaster", "cms");
            this.Property(t => t.TimeSlotID).HasColumnName("TimeSlotID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.Cutoff).HasColumnName("Cutoff");
            this.Property(t => t.Monday).HasColumnName("Monday");
            this.Property(t => t.Tuesday).HasColumnName("Tuesday");
            this.Property(t => t.Wednesday).HasColumnName("Wednesday");
            this.Property(t => t.Thursday).HasColumnName("Thursday");
            this.Property(t => t.Friday).HasColumnName("Friday");
            this.Property(t => t.Saturday).HasColumnName("Saturday");
            this.Property(t => t.Sunday).HasColumnName("Sunday");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
        }
    }
}
