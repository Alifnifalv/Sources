using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandStatusMap : EntityTypeConfiguration<BrandStatus>
    {
        public BrandStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BrandStatuses", "catalog");
            this.Property(t => t.BrandStatusID).HasColumnName("BrandStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
