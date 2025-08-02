using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductListHomePageProduct
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
        public byte Position { get; set; }
        public string ProductNameBrand { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
    }
}
