using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUVariantMap : EntityTypeConfiguration<ProductSKUVariant>
    {
        public ProductSKUVariantMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductSKUMapIID, t.ProductIID, t.CultureID, t.Expression });

            // Properties
            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CultureID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Expression)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ProductSKUVariants", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.Expression).HasColumnName("Expression");
        }
    }
}
