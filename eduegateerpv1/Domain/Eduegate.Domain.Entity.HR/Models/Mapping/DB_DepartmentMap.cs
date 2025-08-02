using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.HR.Models.Mapping
{
    public class DB_DepartmentMap : EntityTypeConfiguration<DB_Department>
    {
        public DB_DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentID);

            // Properties
            this.Property(t => t.DepartmentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DepartmentName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Departments", "hr");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.Logo).HasColumnName("Logo");
        }
    }
}
