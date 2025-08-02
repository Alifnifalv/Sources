using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterMap : EntityTypeConfiguration<ProductMaster>
    {
        public ProductMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

            // Properties
            this.Property(t => t.ProductCode)
                .HasMaxLength(150);

            this.Property(t => t.ProductGroup)
                .HasMaxLength(100);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ProductDescription)
                .IsRequired();

            this.Property(t => t.ProductDetails)
                .IsRequired();

            this.Property(t => t.ProductColor)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProductWarranty)
                .HasMaxLength(50);

            this.Property(t => t.ProductMadeIn)
                .HasMaxLength(100);

            this.Property(t => t.ProductModel)
                .HasMaxLength(50);

            this.Property(t => t.ProductCategoryAll)
                .HasMaxLength(100);

            this.Property(t => t.ProductBarCode)
                .HasMaxLength(100);

            this.Property(t => t.ProductPartNo)
                .HasMaxLength(20);

            this.Property(t => t.ProductKeywordsEn)
                .HasMaxLength(900);

            this.Property(t => t.UsedProductCategory)
                .HasMaxLength(1);

            this.Property(t => t.ProductNameAr)
                .HasMaxLength(555);

            this.Property(t => t.ProductKeywordsAr)
                .HasMaxLength(900);

            this.Property(t => t.ProductColorAr)
                .HasMaxLength(50);

            this.Property(t => t.ProductMadeInAr)
                .HasMaxLength(100);

            this.Property(t => t.ProductWarrantyAr)
                .HasMaxLength(50);

            this.Property(t => t.Location)
                .HasMaxLength(50);

            this.Property(t => t.ProductHSCode)
                .HasMaxLength(30);

            this.Property(t => t.ProductListingImageMaster)
                .HasMaxLength(50);

            this.Property(t => t.ProductThumbnailMaster)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductMaster");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductCreatedOn).HasColumnName("ProductCreatedOn");
            this.Property(t => t.ProductGroup).HasColumnName("ProductGroup");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductDescription).HasColumnName("ProductDescription");
            this.Property(t => t.ProductDetails).HasColumnName("ProductDetails");
            this.Property(t => t.RefBrandID).HasColumnName("RefBrandID");
            this.Property(t => t.ProductColor).HasColumnName("ProductColor");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductWarranty).HasColumnName("ProductWarranty");
            this.Property(t => t.ProductMadeIn).HasColumnName("ProductMadeIn");
            this.Property(t => t.ProductModel).HasColumnName("ProductModel");
            this.Property(t => t.ProductWeight).HasColumnName("ProductWeight");
            this.Property(t => t.ProductReOrderLevel).HasColumnName("ProductReOrderLevel");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.ProductBarCode).HasColumnName("ProductBarCode");
            this.Property(t => t.ProductPartNo).HasColumnName("ProductPartNo");
            this.Property(t => t.ProductDiscountPercent).HasColumnName("ProductDiscountPercent");
            this.Property(t => t.ProductKeywordsEn).HasColumnName("ProductKeywordsEn");
            this.Property(t => t.ExpressDelivery).HasColumnName("ExpressDelivery");
            this.Property(t => t.PickUpShowroom).HasColumnName("PickUpShowroom");
            this.Property(t => t.IsProductVoucher).HasColumnName("IsProductVoucher");
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.SentToEmail).HasColumnName("SentToEmail");
            this.Property(t => t.IsVertual).HasColumnName("IsVertual");
            this.Property(t => t.IsPhysical).HasColumnName("IsPhysical");
            this.Property(t => t.IsNextDay).HasColumnName("IsNextDay");
            this.Property(t => t.IsDigital).HasColumnName("IsDigital");
            this.Property(t => t.MaxOrderQty).HasColumnName("MaxOrderQty");
            this.Property(t => t.MaxCustomerQty).HasColumnName("MaxCustomerQty");
            this.Property(t => t.MaxCustomerQtyDuration).HasColumnName("MaxCustomerQtyDuration");
            this.Property(t => t.IntlShipping).HasColumnName("IntlShipping");
            this.Property(t => t.UsedProduct).HasColumnName("UsedProduct");
            this.Property(t => t.UsedProductCategory).HasColumnName("UsedProductCategory");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.MaxOrderQtyVerified).HasColumnName("MaxOrderQtyVerified");
            this.Property(t => t.ExpressDeliveryCost).HasColumnName("ExpressDeliveryCost");
            this.Property(t => t.NextDayDeliveryCost).HasColumnName("NextDayDeliveryCost");
            this.Property(t => t.ProductNameAr).HasColumnName("ProductNameAr");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.DisableListing).HasColumnName("DisableListing");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.ProductDescriptionAr).HasColumnName("ProductDescriptionAr");
            this.Property(t => t.ProductDetailsAr).HasColumnName("ProductDetailsAr");
            this.Property(t => t.ProductKeywordsAr).HasColumnName("ProductKeywordsAr");
            this.Property(t => t.ProductColorAr).HasColumnName("ProductColorAr");
            this.Property(t => t.ProductMadeInAr).HasColumnName("ProductMadeInAr");
            this.Property(t => t.ProductWarrantyAr).HasColumnName("ProductWarrantyAr");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.MultiPriceIgnore).HasColumnName("MultiPriceIgnore");
            this.Property(t => t.ProductActiveAr).HasColumnName("ProductActiveAr");
            this.Property(t => t.ProductCostPrice).HasColumnName("ProductCostPrice");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
            this.Property(t => t.CashBack).HasColumnName("CashBack");
            this.Property(t => t.ProductHSCode).HasColumnName("ProductHSCode");
            this.Property(t => t.ProductListingImageMaster).HasColumnName("ProductListingImageMaster");
            this.Property(t => t.ProductThumbnailMaster).HasColumnName("ProductThumbnailMaster");
            this.Property(t => t.ProductLength).HasColumnName("ProductLength");
            this.Property(t => t.ProductWidth).HasColumnName("ProductWidth");
            this.Property(t => t.ProductHeight).HasColumnName("ProductHeight");
        }
    }
}
