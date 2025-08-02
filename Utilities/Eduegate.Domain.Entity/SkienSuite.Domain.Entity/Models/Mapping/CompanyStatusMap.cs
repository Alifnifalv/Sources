using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CompanyStatusMap : EntityTypeConfiguration<CompanyStatus>
    {
        public CompanyStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CompanyStatuses", "mutual");
            this.Property(t => t.CompanyStatusID).HasColumnName("CompanyStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
