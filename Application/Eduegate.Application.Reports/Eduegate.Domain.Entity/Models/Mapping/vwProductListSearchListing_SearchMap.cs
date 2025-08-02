using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductListSearchListing_SearchMap : EntityTypeConfiguration<vwProductListSearchListing_Search>
    {
        public vwProductListSearchListing_SearchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName, t.BrandNameEn, t.ProductPrice, t.ProductDiscountPrice, t.ProductColor, t.BrandID, t.BrandCode, t.BrandPosition, t.ProductAvailableQuantity, t.ProductSoldQty, t.ProductNameBrand });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductCode)
                .HasMaxLength(150);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.BrandNameEn)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductDiscountPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductColor)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BrandID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandCode)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.ProductCategoryAll)
                .HasMaxLength(100);

            this.Property(t => t.BrandPosition)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductAvailableQuantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductSoldQty)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductNameBrand)
                .IsRequired()
                .HasMaxLength(123);

            this.Property(t => t.ProductMadeIn)
                .HasMaxLength(100);

            this.Property(t => t.ProductModel)
                .HasMaxLength(50);

            this.Property(t => t.ProductWarranty)
                .HasMaxLength(50);

            this.Property(t => t.CategoryKeywords)
                .HasMaxLength(3000);

            this.Property(t => t.BrandKeywordsEn)
                .HasMaxLength(300);

            this.Property(t => t.ProductKeywordsEn)
                .HasMaxLength(900);

            // Table & Column Mappings
            this.ToTable("vwProductListSearchListing_Search");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BrandNameEn).HasColumnName("BrandNameEn");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductColor).HasColumnName("ProductColor");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.BrandPosition).HasColumnName("BrandPosition");
            this.Property(t => t.ProductWeight).HasColumnName("ProductWeight");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.ProductSoldQty).HasColumnName("ProductSoldQty");
            this.Property(t => t.ProductNameBrand).HasColumnName("ProductNameBrand");
            this.Property(t => t.ProductMadeIn).HasColumnName("ProductMadeIn");
            this.Property(t => t.ProductModel).HasColumnName("ProductModel");
            this.Property(t => t.ProductWarranty).HasColumnName("ProductWarranty");
            this.Property(t => t.CategoryKeywords).HasColumnName("CategoryKeywords");
            this.Property(t => t.BrandKeywordsEn).HasColumnName("BrandKeywordsEn");
            this.Property(t => t.ProductKeywordsEn).HasColumnName("ProductKeywordsEn");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.DisableListing).HasColumnName("DisableListing");
        }
    }
}
