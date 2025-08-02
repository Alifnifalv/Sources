using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUSiteMapMap : EntityTypeConfiguration<ProductSKUSiteMap>
    {
        public ProductSKUSiteMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUSiteMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductSKUSiteMap", "catalog");
            this.Property(t => t.ProductSKUSiteMapIID).HasColumnName("ProductSKUSiteMapIID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductSKUSiteMaps)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
