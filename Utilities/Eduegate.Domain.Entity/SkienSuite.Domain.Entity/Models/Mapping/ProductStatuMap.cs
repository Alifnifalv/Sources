using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductStatuMap : EntityTypeConfiguration<ProductStatu>
    {
        public ProductStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductStatus", "catalog");
            this.Property(t => t.ProductStatusID).HasColumnName("ProductStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
