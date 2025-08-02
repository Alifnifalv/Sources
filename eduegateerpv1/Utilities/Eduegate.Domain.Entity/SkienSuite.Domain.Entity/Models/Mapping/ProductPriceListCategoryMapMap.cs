using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListCategoryMapMap : EntityTypeConfiguration<ProductPriceListCategoryMap>
    {
        public ProductPriceListCategoryMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListCategoryMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListCategoryMaps", "catalog");
            this.Property(t => t.ProductPriceListCategoryMapIID).HasColumnName("ProductPriceListCategoryMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.Price).HasColumnName("Price");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.ProductPriceListCategoryMaps)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListCategoryMaps)
                .HasForeignKey(d => d.ProductPriceListID);

        }
    }
}
