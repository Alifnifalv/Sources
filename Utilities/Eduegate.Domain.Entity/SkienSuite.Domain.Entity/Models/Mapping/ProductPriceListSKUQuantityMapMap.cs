using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListSKUQuantityMapMap : EntityTypeConfiguration<ProductPriceListSKUQuantityMap>
    {
        public ProductPriceListSKUQuantityMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListSKUQuantityMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListSKUQuantityMaps", "catalog");
            this.Property(t => t.ProductPriceListSKUQuantityMapIID).HasColumnName("ProductPriceListSKUQuantityMapIID");
            this.Property(t => t.ProductPriceListSKUMapID).HasColumnName("ProductPriceListSKUMapID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductPriceListSKUMap)
                .WithMany(t => t.ProductPriceListSKUQuantityMaps)
                .HasForeignKey(d => d.ProductPriceListSKUMapID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductPriceListSKUQuantityMaps)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
