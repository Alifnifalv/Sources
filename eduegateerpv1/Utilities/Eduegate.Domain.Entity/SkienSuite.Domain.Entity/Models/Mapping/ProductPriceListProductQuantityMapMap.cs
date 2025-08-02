using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListProductQuantityMapMap : EntityTypeConfiguration<ProductPriceListProductQuantityMap>
    {
        public ProductPriceListProductQuantityMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListProductQuantityMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListProductQuantityMaps", "catalog");
            this.Property(t => t.ProductPriceListProductQuantityMapIID).HasColumnName("ProductPriceListProductQuantityMapIID");
            this.Property(t => t.ProductPriceListProductMapID).HasColumnName("ProductPriceListProductMapID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductPriceListProductMap)
                .WithMany(t => t.ProductPriceListProductQuantityMaps)
                .HasForeignKey(d => d.ProductPriceListProductMapID);
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductPriceListProductQuantityMaps)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
