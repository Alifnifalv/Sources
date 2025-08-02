using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListProductMapMap : EntityTypeConfiguration<ProductPriceListProductMap>
    {
        public ProductPriceListProductMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListProductMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListProductMaps", "catalog");
            this.Property(t => t.ProductPriceListProductMapIID).HasColumnName("ProductPriceListProductMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListProductMaps)
                .HasForeignKey(d => d.ProductPriceListID);
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductPriceListProductMaps)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
