using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSkuSearchViewMap : EntityTypeConfiguration<ProductSkuSearchView>
    {
        public ProductSkuSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductIID, t.ProductSKUMapIID });

            // Properties
            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SKU)
                .HasMaxLength(1000);

            this.Property(t => t.Barcode)
                .HasMaxLength(50);

            this.Property(t => t.PartNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductSkuSearchView", "catalog");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.SKU).HasColumnName("SKU");
            this.Property(t => t.Barcode).HasColumnName("Barcode");
            this.Property(t => t.PartNo).HasColumnName("PartNo");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
        }
    }
}
