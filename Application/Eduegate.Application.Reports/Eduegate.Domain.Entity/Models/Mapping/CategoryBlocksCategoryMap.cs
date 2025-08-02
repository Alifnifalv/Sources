using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryBlocksCategoryMap : EntityTypeConfiguration<CategoryBlocksCategory>
    {
        public CategoryBlocksCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategoryBlocksCategory");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.RefBlockID).HasColumnName("RefBlockID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
        }
    }
}
