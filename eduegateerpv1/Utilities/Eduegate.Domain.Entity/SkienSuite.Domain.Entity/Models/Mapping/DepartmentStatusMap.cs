using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DepartmentStatusMap : EntityTypeConfiguration<DepartmentStatus>
    {
        public DepartmentStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DepartmentStatuses", "mutual");
            this.Property(t => t.DepartmentStatusID).HasColumnName("DepartmentStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
