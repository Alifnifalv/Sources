using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductListNewPreview
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string BrandNameEn { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public string ProductThumbnail { get; set; }
        public string ProductListingImage { get; set; }
        public string prodname { get; set; }
        public string ProductColor { get; set; }
        public int BrandID { get; set; }
        public string BrandCode { get; set; }
        public string ProductCategoryAll { get; set; }
        public short BrandPosition { get; set; }
        public Nullable<long> ProductWeight { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDetails { get; set; }
        public string ProductGroup { get; set; }
        public string ProductWarranty { get; set; }
        public string ProductMadeIn { get; set; }
        public string ProductModel { get; set; }
        public Nullable<decimal> ProductDiscountPercent { get; set; }
        public string ProductKeywordsEn { get; set; }
        public Nullable<bool> ExpressDelivery { get; set; }
        public Nullable<bool> PickUpShowroom { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<bool> SentToEmail { get; set; }
        public bool IsVertual { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsNextDay { get; set; }
        public bool IsProductVoucher { get; set; }
        public Nullable<bool> IntlShipping { get; set; }
        public Nullable<bool> UsedProduct { get; set; }
        public string UsedProductCategory { get; set; }
        public Nullable<bool> IsDigital { get; set; }
        public Nullable<int> MaxOrderQty { get; set; }
        public Nullable<int> MaxCustomerQty { get; set; }
        public Nullable<int> MaxCustomerQtyDuration { get; set; }
        public string ProductPartNo { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
        public bool MultiPrice { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductDetailsAr { get; set; }
        public string ProductDescriptionAr { get; set; }
        public string ProductKeywordsAr { get; set; }
        public string BrandNameAr { get; set; }
        public string ProductColorAr { get; set; }
        public string ProductWarrantyAr { get; set; }
        public string ProductMadeInAr { get; set; }
        public string ProductManager { get; set; }
        public string SupplierName { get; set; }
    }
}
