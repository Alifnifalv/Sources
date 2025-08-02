using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategorySortMasterMap : EntityTypeConfiguration<CategorySortMaster>
    {
        public CategorySortMasterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CategorySortID, t.CategoryText, t.CategoryValue });

            // Properties
            this.Property(t => t.CategoryText)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryValue)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.CategoryTextCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CategorySortMaster");
            this.Property(t => t.CategorySortID).HasColumnName("CategorySortID");
            this.Property(t => t.CategoryText).HasColumnName("CategoryText");
            this.Property(t => t.CategoryValue).HasColumnName("CategoryValue");
            this.Property(t => t.CategoryTextCode).HasColumnName("CategoryTextCode");
        }
    }
}
