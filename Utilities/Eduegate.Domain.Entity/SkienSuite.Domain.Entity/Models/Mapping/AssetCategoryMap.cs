using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AssetCategoryMap : EntityTypeConfiguration<AssetCategory>
    {
        public AssetCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.AssetCategoryID);

            // Properties
            this.Property(t => t.CategoryName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("AssetCategories", "asset");
            this.Property(t => t.AssetCategoryID).HasColumnName("AssetCategoryID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
        }
    }
}
