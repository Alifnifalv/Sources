using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierProductPriceSettingSearchViewMap : EntityTypeConfiguration<SupplierProductPriceSettingSearchView>
    {
        public SupplierProductPriceSettingSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductIID);

            // Properties
            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            this.Property(t => t.ProductName)
                .HasMaxLength(1000);

            this.Property(t => t.BranchName)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("SupplierProductPriceSettingSearchView", "catalog");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
        }
    }
}
