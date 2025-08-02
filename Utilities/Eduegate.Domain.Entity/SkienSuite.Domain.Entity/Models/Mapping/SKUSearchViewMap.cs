using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SKUSearchViewMap : EntityTypeConfiguration<SKUSearchView>
    {
        public SKUSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUMapIID);

            // Properties
            this.Property(t => t.ProductSKUMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SKUName)
                .HasMaxLength(1000);

            this.Property(t => t.ProductSKUCode)
                .HasMaxLength(150);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            this.Property(t => t.RowCategory)
                .HasMaxLength(6);

            this.Property(t => t.ProductName)
                .HasMaxLength(1000);

            this.Property(t => t.partno)
                .HasMaxLength(50);

            this.Property(t => t.barcode)
                .HasMaxLength(50);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SKUSearchView", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.SKUName).HasColumnName("SKUName");
            this.Property(t => t.ProductSKUCode).HasColumnName("ProductSKUCode");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.partno).HasColumnName("partno");
            this.Property(t => t.barcode).HasColumnName("barcode");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.SkuTag).HasColumnName("SkuTag");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
