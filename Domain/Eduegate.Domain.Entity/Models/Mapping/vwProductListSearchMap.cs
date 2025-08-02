using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductListSearchMap : EntityTypeConfiguration<vwProductListSearch>
    {
        public vwProductListSearchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName, t.BrandNameEn, t.ProductPrice, t.ProductDiscountPrice, t.prodname, t.ProductColor, t.BrandID, t.BrandCode, t.BrandPosition, t.ProductAvailableQuantity, t.ProductDescription, t.ProductDetails, t.DisableListing, t.ProductActive, t.ProductActiveAr });

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

            this.Property(t => t.ProductThumbnail)
                .HasMaxLength(81);

            this.Property(t => t.ProductListingImage)
                .HasMaxLength(81);

            this.Property(t => t.prodname)
                .IsRequired()
                .HasMaxLength(123);

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

            this.Property(t => t.ProductDescription)
                .IsRequired();

            this.Property(t => t.ProductDetails)
                .IsRequired();

            this.Property(t => t.ProductGroup)
                .HasMaxLength(100);

            this.Property(t => t.ProductWarranty)
                .HasMaxLength(50);

            this.Property(t => t.ProductMadeIn)
                .HasMaxLength(100);

            this.Property(t => t.ProductModel)
                .HasMaxLength(50);

            this.Property(t => t.CategoryKeywords)
                .HasMaxLength(3000);

            this.Property(t => t.BrandKeywordsEn)
                .HasMaxLength(300);

            this.Property(t => t.ProductKeywordsEn)
                .HasMaxLength(900);

            this.Property(t => t.ProductNameAr)
                .HasMaxLength(555);

            this.Property(t => t.BrandNameAr)
                .HasMaxLength(255);

            this.Property(t => t.prodnameAr)
                .HasMaxLength(813);

            this.Property(t => t.ProductColorAr)
                .HasMaxLength(50);

            this.Property(t => t.ProductWarrantyAr)
                .HasMaxLength(50);

            this.Property(t => t.ProductMadeInAr)
                .HasMaxLength(100);

            this.Property(t => t.ProductKeywordsAr)
                .HasMaxLength(900);

            this.Property(t => t.CategoryKeywordsAr)
                .HasMaxLength(3000);

            // Table & Column Mappings
            this.ToTable("vwProductListSearch");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BrandNameEn).HasColumnName("BrandNameEn");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductThumbnail).HasColumnName("ProductThumbnail");
            this.Property(t => t.ProductListingImage).HasColumnName("ProductListingImage");
            this.Property(t => t.prodname).HasColumnName("prodname");
            this.Property(t => t.ProductColor).HasColumnName("ProductColor");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.BrandPosition).HasColumnName("BrandPosition");
            this.Property(t => t.ProductWeight).HasColumnName("ProductWeight");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.ProductDescription).HasColumnName("ProductDescription");
            this.Property(t => t.ProductDetails).HasColumnName("ProductDetails");
            this.Property(t => t.ProductGroup).HasColumnName("ProductGroup");
            this.Property(t => t.ProductWarranty).HasColumnName("ProductWarranty");
            this.Property(t => t.ProductMadeIn).HasColumnName("ProductMadeIn");
            this.Property(t => t.ProductModel).HasColumnName("ProductModel");
            this.Property(t => t.ProductDiscountPercent).HasColumnName("ProductDiscountPercent");
            this.Property(t => t.CategoryKeywords).HasColumnName("CategoryKeywords");
            this.Property(t => t.BrandKeywordsEn).HasColumnName("BrandKeywordsEn");
            this.Property(t => t.ProductKeywordsEn).HasColumnName("ProductKeywordsEn");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.DisableListing).HasColumnName("DisableListing");
            this.Property(t => t.ProductNameAr).HasColumnName("ProductNameAr");
            this.Property(t => t.BrandNameAr).HasColumnName("BrandNameAr");
            this.Property(t => t.prodnameAr).HasColumnName("prodnameAr");
            this.Property(t => t.ProductColorAr).HasColumnName("ProductColorAr");
            this.Property(t => t.ProductDescriptionAr).HasColumnName("ProductDescriptionAr");
            this.Property(t => t.ProductWarrantyAr).HasColumnName("ProductWarrantyAr");
            this.Property(t => t.ProductMadeInAr).HasColumnName("ProductMadeInAr");
            this.Property(t => t.ProductDetailsAr).HasColumnName("ProductDetailsAr");
            this.Property(t => t.BrandKeywordsAr).HasColumnName("BrandKeywordsAr");
            this.Property(t => t.ProductKeywordsAr).HasColumnName("ProductKeywordsAr");
            this.Property(t => t.CategoryKeywordsAr).HasColumnName("CategoryKeywordsAr");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductActiveAr).HasColumnName("ProductActiveAr");
        }
    }
}
