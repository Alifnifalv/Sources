using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUMapSKUNamesViewMap : EntityTypeConfiguration<ProductSKUMapSKUNamesView>
    {
        public ProductSKUMapSKUNamesViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductSKUMapIID, t.SKU });

            // Properties
            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SKU)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProductSKUMapSKUNamesView", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.SKU).HasColumnName("SKU");
        }
    }
}
