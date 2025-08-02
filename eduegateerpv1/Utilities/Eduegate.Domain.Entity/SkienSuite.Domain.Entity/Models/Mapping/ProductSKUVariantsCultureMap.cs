using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUVariantsCultureMap : EntityTypeConfiguration<ProductSKUVariantsCulture>
    {
        public ProductSKUVariantsCultureMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductSKUMapIID, t.ProductIID });

            // Properties
            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductSKUVariantsCulture", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
        }
    }
}
