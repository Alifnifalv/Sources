using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductCategoryID);

            // Properties
            this.Property(t => t.RefCategoryBreadcrumbs)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductCategory");
            this.Property(t => t.ProductCategoryID).HasColumnName("ProductCategoryID");
            this.Property(t => t.RefProductCategoryProductID).HasColumnName("RefProductCategoryProductID");
            this.Property(t => t.RefProductCategoryCategoryID).HasColumnName("RefProductCategoryCategoryID");
            this.Property(t => t.RefCategoryBreadcrumbs).HasColumnName("RefCategoryBreadcrumbs");

            // Relationships
            //this.HasRequired(t => t.CategoryMaster)
            //    .WithMany(t => t.ProductCategories)
            //    .HasForeignKey(d => d.RefProductCategoryCategoryID);
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.ProductCategories)
            //    .HasForeignKey(d => d.RefProductCategoryProductID);

        }
    }
}
