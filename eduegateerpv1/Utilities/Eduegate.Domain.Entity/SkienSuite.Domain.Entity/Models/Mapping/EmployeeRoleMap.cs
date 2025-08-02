using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmployeeRoleMap : EntityTypeConfiguration<EmployeeRole>
    {
        public EmployeeRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeRoleID);

            // Properties
            this.Property(t => t.EmployeeRoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmployeeRoleName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EmployeeRoles", "payroll");
            this.Property(t => t.EmployeeRoleID).HasColumnName("EmployeeRoleID");
            this.Property(t => t.EmployeeRoleName).HasColumnName("EmployeeRoleName");
        }
    }
}
