using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmployeeAccountMapMap : EntityTypeConfiguration<EmployeeAccountMap>
    {
        public EmployeeAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeAccountMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("EmployeeAccountMaps", "payroll");
            this.Property(t => t.EmployeeAccountMapIID).HasColumnName("EmployeeAccountMapIID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.EmployeeAccountMaps)
                .HasForeignKey(d => d.AccountID);
            //this.HasOptional(t => t.Employee)
            //    .WithMany(t => t.EmployeeAccountMaps)
            //    .HasForeignKey(d => d.EmployeeID);
        }
    }
}
