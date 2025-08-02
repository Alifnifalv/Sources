using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryCultureMap : EntityTypeConfiguration<CategoryCulture>
    {
        public CategoryCultureMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryIID);

            // Properties
            this.Property(t => t.CategoryIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CategoryCode)
                .HasMaxLength(50);

            this.Property(t => t.CategoryName)
                .HasMaxLength(100);

            this.Property(t => t.ImageName)
                .HasMaxLength(500);

            this.Property(t => t.ThumbnailImageName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CategoryCultures", "catalog");
            this.Property(t => t.CategoryIID).HasColumnName("CategoryIID");
            this.Property(t => t.ParentCategoryID).HasColumnName("ParentCategoryID");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.ThumbnailImageName).HasColumnName("ThumbnailImageName");
            this.Property(t => t.SeoMetadataID).HasColumnName("SeoMetadataID");
            this.Property(t => t.IsInNavigationMenu).HasColumnName("IsInNavigationMenu");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
        }
    }
}
