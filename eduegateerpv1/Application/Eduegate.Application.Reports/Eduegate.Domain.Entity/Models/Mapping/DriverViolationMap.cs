using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DriverViolationMap : EntityTypeConfiguration<DriverViolation>
    {
        public DriverViolationMap()
        {
            // Primary Key
            this.HasKey(t => t.ViolationID);

            // Properties
            this.Property(t => t.ViolationRemark)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("DriverViolation");
            this.Property(t => t.ViolationID).HasColumnName("ViolationID");
            this.Property(t => t.RefDriverID).HasColumnName("RefDriverID");
            this.Property(t => t.ViolationRemark).HasColumnName("ViolationRemark");
            this.Property(t => t.ViolationHistoryDate).HasColumnName("ViolationHistoryDate");
        }
    }
}
