using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwBrandListMap : EntityTypeConfiguration<vwBrandList>
    {
        public vwBrandListMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BrandID, t.BrandCode, t.BrandNameEn, t.BrandPosition, t.BrandActive });

            // Properties
            this.Property(t => t.BrandID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BrandCode)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.BrandNameEn)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.BrandPosition)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandLogo)
                .HasMaxLength(30);

            this.Property(t => t.BrandKeywordsEn)
                .HasMaxLength(300);

            this.Property(t => t.BrandNameAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("vwBrandList");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.BrandNameEn).HasColumnName("BrandNameEn");
            this.Property(t => t.BrandPosition).HasColumnName("BrandPosition");
            this.Property(t => t.BrandLogo).HasColumnName("BrandLogo");
            this.Property(t => t.BrandKeywordsEn).HasColumnName("BrandKeywordsEn");
            this.Property(t => t.BrandDescriptionEn).HasColumnName("BrandDescriptionEn");
            this.Property(t => t.BrandActive).HasColumnName("BrandActive");
            this.Property(t => t.BrandNameAr).HasColumnName("BrandNameAr");
            this.Property(t => t.BrandKeywordsAr).HasColumnName("BrandKeywordsAr");
            this.Property(t => t.BrandDescriptionAr).HasColumnName("BrandDescriptionAr");
        }
    }
}
