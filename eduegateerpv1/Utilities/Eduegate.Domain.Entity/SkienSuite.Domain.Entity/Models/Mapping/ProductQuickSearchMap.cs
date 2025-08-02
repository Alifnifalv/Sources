using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductQuickSearchMap : EntityTypeConfiguration<ProductQuickSearch>
    {
        public ProductQuickSearchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductSKUMapIID, t.SKU });

            // Properties
            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SKU)
                .IsRequired();

            this.Property(t => t.ProductSKUCode)
                .HasMaxLength(150);

            this.Property(t => t.PartNo)
                .HasMaxLength(50);

            this.Property(t => t.BarCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductQuickSearch", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.SKU).HasColumnName("SKU");
            this.Property(t => t.ProductSKUCode).HasColumnName("ProductSKUCode");
            this.Property(t => t.PartNo).HasColumnName("PartNo");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
        }
    }
}
