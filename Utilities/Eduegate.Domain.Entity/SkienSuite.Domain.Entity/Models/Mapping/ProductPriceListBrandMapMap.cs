using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListBrandMapMap : EntityTypeConfiguration<ProductPriceListBrandMap>
    {
        public ProductPriceListBrandMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListBrandMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListBrandMaps", "catalog");
            this.Property(t => t.ProductPriceListBrandMapIID).HasColumnName("ProductPriceListBrandMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.Price).HasColumnName("Price");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.ProductPriceListBrandMaps)
                .HasForeignKey(d => d.BrandID);
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListBrandMaps)
                .HasForeignKey(d => d.ProductPriceListID);

        }
    }
}
