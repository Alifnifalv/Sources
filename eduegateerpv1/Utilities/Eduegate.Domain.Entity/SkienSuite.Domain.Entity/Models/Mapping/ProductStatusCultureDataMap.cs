using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStatusCultureDataMap : EntityTypeConfiguration<ProductStatusCultureData>
    {
        public ProductStatusCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CoultureID, t.ProductStatusID });

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductStatusCultureDatas", "catalog");
            this.Property(t => t.CoultureID).HasColumnName("CoultureID");
            this.Property(t => t.ProductStatusID).HasColumnName("ProductStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
