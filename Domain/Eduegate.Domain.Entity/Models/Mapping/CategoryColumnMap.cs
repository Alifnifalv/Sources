using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryColumnMap : EntityTypeConfiguration<CategoryColumn>
    {
        public CategoryColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryColumnID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategoryColumns");
            this.Property(t => t.CategoryColumnID).HasColumnName("CategoryColumnID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefColumnID).HasColumnName("RefColumnID");

            // Relationships
            //this.HasRequired(t => t.CategoryMaster)
            //    .WithMany(t => t.CategoryColumns)
            //    .HasForeignKey(d => d.RefCategoryID);
            this.HasRequired(t => t.ColumnMaster)
                .WithMany(t => t.CategoryColumns)
                .HasForeignKey(d => d.RefColumnID);

        }
    }
}
