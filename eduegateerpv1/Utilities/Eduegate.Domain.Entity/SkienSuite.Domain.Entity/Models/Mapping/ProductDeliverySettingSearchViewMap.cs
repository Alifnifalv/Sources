using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDeliverySettingSearchViewMap : EntityTypeConfiguration<ProductDeliverySettingSearchView>
    {
        public ProductDeliverySettingSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductIID, t.PartNumber, t.ProductCode });

            // Properties
            this.Property(t => t.ProductIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartNumber)
                .IsRequired();

            this.Property(t => t.RowCategory)
                .HasMaxLength(6);

            this.Property(t => t.ProductCode)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.ProductName)
                .HasMaxLength(1000);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            this.Property(t => t.BrandCode)
                .HasMaxLength(50);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductDeliverySettingSearchView", "catalog");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.PartNumber).HasColumnName("PartNumber");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Weight).HasColumnName("Weight");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
