using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryBlocksBrandMap : EntityTypeConfiguration<CategoryBlocksBrand>
    {
        public CategoryBlocksBrandMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategoryBlocksBrand");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.RefBlockID).HasColumnName("RefBlockID");
            this.Property(t => t.RefBrandID).HasColumnName("RefBrandID");
        }
    }
}
