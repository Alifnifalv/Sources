using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMaster
    {
        public ProductMaster()
        {
            this.BlinkPoOrderDetails = new List<BlinkPoOrderDetail>();
            this.OrderDetailsLoyaltyPoints = new List<OrderDetailsLoyaltyPoint>();
            this.OrderItems = new List<OrderItem>();
            this.OrderItems1 = new List<OrderItem>();
            this.ProductCategories = new List<ProductCategory>();
            this.ProductDetails1 = new List<ProductDetail>();
            this.ProductDigitals = new List<ProductDigital>();
            this.ProductGalleries = new List<ProductGallery>();
            this.ProductHomePages = new List<ProductHomePage>();
            this.ProductNewArrivals = new List<ProductNewArrival>();
            this.ProductOffers = new List<ProductOffer>();
            this.ProductPointsMasters = new List<ProductPointsMaster>();
            this.ProductRecommends = new List<ProductRecommend>();
            this.ProductRecommends1 = new List<ProductRecommend>();
            this.ProductUseds = new List<ProductUsed>();
            this.CategoryMasters = new List<CategoryMaster>();
        }

        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public Nullable<System.DateTime> ProductCreatedOn { get; set; }
        public string ProductGroup { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDetails { get; set; }
        public int RefBrandID { get; set; }
        public string ProductColor { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public string ProductWarranty { get; set; }
        public string ProductMadeIn { get; set; }
        public string ProductModel { get; set; }
        public Nullable<long> ProductWeight { get; set; }
        public int ProductReOrderLevel { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public bool ProductActive { get; set; }
        public string ProductCategoryAll { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductPartNo { get; set; }
        public Nullable<decimal> ProductDiscountPercent { get; set; }
        public string ProductKeywordsEn { get; set; }
        public Nullable<bool> ExpressDelivery { get; set; }
        public Nullable<bool> PickUpShowroom { get; set; }
        public bool IsProductVoucher { get; set; }
        public bool MultiPrice { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<bool> SentToEmail { get; set; }
        public bool IsVertual { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsNextDay { get; set; }
        public Nullable<bool> IsDigital { get; set; }
        public Nullable<int> MaxOrderQty { get; set; }
        public Nullable<int> MaxCustomerQty { get; set; }
        public Nullable<int> MaxCustomerQtyDuration { get; set; }
        public Nullable<bool> IntlShipping { get; set; }
        public Nullable<bool> UsedProduct { get; set; }
        public string UsedProductCategory { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> MaxOrderQtyVerified { get; set; }
        public Nullable<decimal> ExpressDeliveryCost { get; set; }
        public Nullable<decimal> NextDayDeliveryCost { get; set; }
        public string ProductNameAr { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
        public Nullable<bool> DisableListing { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string ProductDescriptionAr { get; set; }
        public string ProductDetailsAr { get; set; }
        public string ProductKeywordsAr { get; set; }
        public string ProductColorAr { get; set; }
        public string ProductMadeInAr { get; set; }
        public string ProductWarrantyAr { get; set; }
        public string Location { get; set; }
        public Nullable<bool> MultiPriceIgnore { get; set; }
        public Nullable<bool> ProductActiveAr { get; set; }
        public Nullable<decimal> ProductCostPrice { get; set; }
        public Nullable<long> ProductManagerID { get; set; }
        public Nullable<decimal> CashBack { get; set; }
        public string ProductHSCode { get; set; }
        public string ProductListingImageMaster { get; set; }
        public string ProductThumbnailMaster { get; set; }
        public Nullable<decimal> ProductLength { get; set; }
        public Nullable<decimal> ProductWidth { get; set; }
        public Nullable<decimal> ProductHeight { get; set; }
        public virtual ICollection<BlinkPoOrderDetail> BlinkPoOrderDetails { get; set; }
        public virtual ICollection<OrderDetailsLoyaltyPoint> OrderDetailsLoyaltyPoints { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems1 { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ProductCoutureNewArrival ProductCoutureNewArrival { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails1 { get; set; }
        public virtual ICollection<ProductDigital> ProductDigitals { get; set; }
        public virtual ICollection<ProductGallery> ProductGalleries { get; set; }
        public virtual ICollection<ProductHomePage> ProductHomePages { get; set; }
        public virtual ICollection<ProductNewArrival> ProductNewArrivals { get; set; }
        public virtual ICollection<ProductOffer> ProductOffers { get; set; }
        public virtual ICollection<ProductPointsMaster> ProductPointsMasters { get; set; }
        public virtual ICollection<ProductRecommend> ProductRecommends { get; set; }
        public virtual ICollection<ProductRecommend> ProductRecommends1 { get; set; }
        public virtual ICollection<ProductUsed> ProductUseds { get; set; }
        public virtual ICollection<CategoryMaster> CategoryMasters { get; set; }
    }
}
