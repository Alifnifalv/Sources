using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DesignationMap : EntityTypeConfiguration<Designation>
    {
        public DesignationMap()
        {
            // Primary Key
            this.HasKey(t => t.DesignationID);

            // Properties
            this.Property(t => t.DesignationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DesignationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Designations", "payroll");
            this.Property(t => t.DesignationID).HasColumnName("DesignationID");
            this.Property(t => t.DesignationName).HasColumnName("DesignationName");
        }
    }
}
