using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryMasterMap : EntityTypeConfiguration<CategoryMaster>
    {
        public CategoryMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryID);

            // Properties
            this.Property(t => t.CategoryCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryNameEn)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryBreadCrumbs)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryLogo)
                .HasMaxLength(70);

            this.Property(t => t.CategoryKeywordsEn)
                .HasMaxLength(300);

            this.Property(t => t.CategoryDescriptionEn)
                .HasMaxLength(1000);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(1000);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(1000);

            this.Property(t => t.CategoryNameAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("CategoryMaster");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.CategoryNameEn).HasColumnName("CategoryNameEn");
            this.Property(t => t.CategoryPosition).HasColumnName("CategoryPosition");
            this.Property(t => t.CategoryLevel).HasColumnName("CategoryLevel");
            this.Property(t => t.CategoryBreadCrumbs).HasColumnName("CategoryBreadCrumbs");
            this.Property(t => t.CategoryLogo).HasColumnName("CategoryLogo");
            this.Property(t => t.CategoryKeywordsEn).HasColumnName("CategoryKeywordsEn");
            this.Property(t => t.CategoryDescriptionEn).HasColumnName("CategoryDescriptionEn");
            this.Property(t => t.CategoryActive).HasColumnName("CategoryActive");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.CategoryNameAr).HasColumnName("CategoryNameAr");
            this.Property(t => t.CategoryKeywordsAr).HasColumnName("CategoryKeywordsAr");
            this.Property(t => t.CategoryDescriptionAr).HasColumnName("CategoryDescriptionAr");
            this.Property(t => t.SeoKeywordsAr).HasColumnName("SeoKeywordsAr");
            this.Property(t => t.SeoDescriptionAr).HasColumnName("SeoDescriptionAr");
            this.Property(t => t.CategoryActiveKSA).HasColumnName("CategoryActiveKSA");

            // Relationships
            
            this.HasMany(t => t.ProductMasters)
                .WithMany(t => t.CategoryMasters)
                .Map(m =>
                {
                    m.ToTable("ProductCategoryLevel");
                    m.MapLeftKey("RefCategoryID");
                    m.MapRightKey("RefProductID");
                });


        }
    }
}
