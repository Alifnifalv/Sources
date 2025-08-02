using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductCategoryBrandListSearchListing
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string BrandNameEn { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public string ProductColor { get; set; }
        public int BrandID { get; set; }
        public string BrandCode { get; set; }
        public string ProductCategoryAll { get; set; }
        public short BrandPosition { get; set; }
        public Nullable<long> ProductWeight { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public int ProductSoldQty { get; set; }
        public string ProductNameBrand { get; set; }
        public string ProductMadeIn { get; set; }
        public string ProductModel { get; set; }
        public string ProductWarranty { get; set; }
        public string BrandKeywordsEn { get; set; }
        public string ProductKeywordsEn { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
        public Nullable<bool> DisableListing { get; set; }
    }
}
