using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUSiteMapMap : EntityTypeConfiguration<ProductSKUSiteMap>
    {
        public ProductSKUSiteMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUSiteMapIID);

            // Table & Column Mappings
            this.ToTable("ProductSKUSiteMap", "catalog");
            this.Property(t => t.ProductSKUSiteMapIID).HasColumnName("ProductSKUSiteMapIID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductSKUSiteMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.Sites)
                .WithMany(t => t.ProductSKUSiteMaps)
                .HasForeignKey(d => d.SiteID);

        }
    }
}
