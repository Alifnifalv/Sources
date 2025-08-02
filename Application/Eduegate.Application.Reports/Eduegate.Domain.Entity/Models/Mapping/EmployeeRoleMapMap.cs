using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmployeeRoleMapMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.EmployeeRoleMap>
    {
        public EmployeeRoleMapMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeRoleMapIID);

            // Properties
            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EmployeeRoleMaps", "payroll");
            this.Property(t => t.EmployeeRoleMapIID).HasColumnName("EmployeeRoleMapIID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.EmployeeRoleID).HasColumnName("EmployeeRoleID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.EmployeeRole)
                .WithMany(t => t.EmployeeRoleMaps)
                .HasForeignKey(d => d.EmployeeRoleID);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.EmployeeRoleMaps)
                .HasForeignKey(d => d.EmployeeID);

        }
    }
}
