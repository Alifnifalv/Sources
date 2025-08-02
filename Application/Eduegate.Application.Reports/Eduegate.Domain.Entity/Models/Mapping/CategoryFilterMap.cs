using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryFilterMap : EntityTypeConfiguration<CategoryFilter>
    {
        public CategoryFilterMap()
        {
            // Primary Key
            this.HasKey(t => t.FilterID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategoryFilters");
            this.Property(t => t.FilterID).HasColumnName("FilterID");
            this.Property(t => t.RefCategoryColumnID).HasColumnName("RefCategoryColumnID");
            this.Property(t => t.Position).HasColumnName("Position");

            // Relationships
            this.HasRequired(t => t.CategoryColumn)
                .WithMany(t => t.CategoryFilters)
                .HasForeignKey(d => d.RefCategoryColumnID);

        }
    }
}
