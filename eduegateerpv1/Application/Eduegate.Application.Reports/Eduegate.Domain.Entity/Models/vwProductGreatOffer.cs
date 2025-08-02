using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductGreatOffer
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string BrandNameEn { get; set; }
        public byte Position { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public string ProductThumbnail { get; set; }
        public string prodname { get; set; }
        public string ProductCategoryAll { get; set; }
        public string BrandCode { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public string prodnameAr { get; set; }
        public bool ProductActive { get; set; }
        public bool ProductActiveAr { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
    }
}
