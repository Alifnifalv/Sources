using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductBundleMap : EntityTypeConfiguration<ProductBundle>
    {
        public ProductBundleMap()
        {
            // Primary Key
            this.HasKey(t => t.BundleIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductBundles", "catalog");
            this.Property(t => t.BundleIID).HasColumnName("BundleIID");
            this.Property(t => t.FromProductID).HasColumnName("FromProductID");
            this.Property(t => t.ToProductID).HasColumnName("ToProductID");
            this.Property(t => t.FromProductSKUMapID).HasColumnName("FromProductSKUMapID");
            this.Property(t => t.ToProductSKUMapID).HasColumnName("ToProductSKUMapID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductBundles)
                .HasForeignKey(d => d.FromProductID);
            this.HasOptional(t => t.Product1)
                .WithMany(t => t.ProductBundles1)
                .HasForeignKey(d => d.ToProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductBundles)
                .HasForeignKey(d => d.ToProductSKUMapID);

        }
    }
}
