using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategorySortDetailMap : EntityTypeConfiguration<CategorySortDetail>
    {
        public CategorySortDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.CategorySortDetailsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategorySortDetails");
            this.Property(t => t.CategorySortDetailsID).HasColumnName("CategorySortDetailsID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefCategorySortID).HasColumnName("RefCategorySortID");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
