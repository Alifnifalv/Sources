using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryBrandMap : EntityTypeConfiguration<CategoryBrand>
    {
        public CategoryBrandMap()
        {
            // Primary Key
            this.HasKey(t => t.KeyCode);

            // Properties
            this.Property(t => t.KeyCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.KeyType)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CategoryBrands");
            this.Property(t => t.KeyCode).HasColumnName("KeyCode");
            this.Property(t => t.KeyType).HasColumnName("KeyType");
            this.Property(t => t.CategoryLevel).HasColumnName("CategoryLevel");
        }
    }
}
