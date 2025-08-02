using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductNewArrivalsIntl
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string BrandNameEn { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public string prodname { get; set; }
        public byte DeliveryDays { get; set; }
        public string ProductThumbnail { get; set; }
        public int NewArrival { get; set; }
        public string ProductCategoryAll { get; set; }
        public string BrandCode { get; set; }
        public System.DateTime ProductCreatedOn { get; set; }
        public int BrandID { get; set; }
        public short BrandPosition { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public string ProductNameBrand { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
        public string ProductNameAr { get; set; }
        public string prodnameAr { get; set; }
        public string ProductNameBrandAr { get; set; }
        public string BrandNameAr { get; set; }
        public bool ProductActive { get; set; }
        public bool ProductActiveAr { get; set; }
        public short RefCountryID { get; set; }
        public Nullable<bool> MultiPrice { get; set; }
    }
}
