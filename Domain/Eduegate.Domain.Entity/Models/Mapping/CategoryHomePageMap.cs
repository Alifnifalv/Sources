using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryHomePageMap : EntityTypeConfiguration<CategoryHomePage>
    {
        public CategoryHomePageMap()
        {
            // Primary Key
            this.HasKey(t => t.RefCategoryID);

            // Properties
            this.Property(t => t.RefCategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CategoryHomePage");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
        }
    }
}
