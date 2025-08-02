using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryTagMap : EntityTypeConfiguration<CategoryTag>
    {
        public CategoryTagMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryTagIID);

            // Properties
            this.Property(t => t.TagName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CategoryTags", "catalog");
            this.Property(t => t.CategoryTagIID).HasColumnName("CategoryTagIID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.CategoryTags)
                .HasForeignKey(d => d.CategoryID);

        }
    }
}
