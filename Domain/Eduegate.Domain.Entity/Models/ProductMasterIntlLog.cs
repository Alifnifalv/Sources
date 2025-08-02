using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterIntlLog
    {
        [Key]
        public int ProductMasterIntlLogID { get; set; }
        public short RefCountryID { get; set; }
        public int RefProductID { get; set; }
        public string ProductType { get; set; }
        public string ProductType_N { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductPrice_N { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public decimal ProductDiscountPrice_N { get; set; }
        public bool ProductActive { get; set; }
        public bool ProductActive_N { get; set; }
        public bool ProductActiveCountry { get; set; }
        public bool ProductActiveCountry_N { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<byte> DeliveryDays_N { get; set; }
        public Nullable<bool> MultiPrice { get; set; }
        public Nullable<bool> MultiPrice_N { get; set; }
        public Nullable<bool> MultiPriceIgnore { get; set; }
        public Nullable<bool> MultiPriceIgnore_N { get; set; }
    }
}
