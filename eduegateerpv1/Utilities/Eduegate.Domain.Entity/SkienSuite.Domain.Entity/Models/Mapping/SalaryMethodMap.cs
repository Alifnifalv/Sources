using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SalaryMethodMap : EntityTypeConfiguration<SalaryMethod>
    {
        public SalaryMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.SalaryMethodID);

            // Properties
            this.Property(t => t.SalaryMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SalaryMethodName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("SalaryMethod", "payroll");
            this.Property(t => t.SalaryMethodID).HasColumnName("SalaryMethodID");
            this.Property(t => t.SalaryMethodName).HasColumnName("SalaryMethodName");
        }
    }
}
