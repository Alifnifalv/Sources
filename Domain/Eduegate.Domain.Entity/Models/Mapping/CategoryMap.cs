using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryIID);

            // Properties
            this.Property(t => t.CategoryCode)
                .HasMaxLength(50);

            this.Property(t => t.CategoryName)
                .HasMaxLength(100);

            this.Property(t => t.ImageName)
                .HasMaxLength(500);

            this.Property(t => t.ThumbnailImageName)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Categories", "catalog");
            this.Property(t => t.CategoryIID).HasColumnName("CategoryIID");
            this.Property(t => t.ParentCategoryID).HasColumnName("ParentCategoryID");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.ThumbnailImageName).HasColumnName("ThumbnailImageName");
            this.Property(t => t.SeoMetadataID).HasColumnName("SeoMetadataID");
            this.Property(t => t.IsInNavigationMenu).HasColumnName("IsInNavigationMenu");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.Profit).HasColumnName("Profit");
            this.Property(t => t.IsReporting).HasColumnName("IsReporting");
            
            // Relationships
            this.HasOptional(t => t.Category1)
                .WithMany(t => t.Categories1)
                .HasForeignKey(d => d.ParentCategoryID);
            this.HasOptional(t => t.SeoMetadata)
                .WithMany(t => t.Categories)
                .HasForeignKey(d => d.SeoMetadataID);
            this.HasOptional(t => t.Culture)
                .WithMany(t => t.Categories)
                .HasForeignKey(d => d.CultureID);
        }
    }
}
