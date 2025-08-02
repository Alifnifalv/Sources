using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCategoryWithChildCountMap : EntityTypeConfiguration<vwCategoryWithChildCount>
    {
        public vwCategoryWithChildCountMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CategoryID, t.CategoryCode, t.CategoryNameEn });

            // Properties
            this.Property(t => t.CategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CategoryCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryNameEn)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryNameAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("vwCategoryWithChildCount");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.CategoryNameEn).HasColumnName("CategoryNameEn");
            this.Property(t => t.CategoryNameAr).HasColumnName("CategoryNameAr");
        }
    }
}
