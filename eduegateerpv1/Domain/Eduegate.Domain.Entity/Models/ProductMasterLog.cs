using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterLog
    {
        [Key]
        public int LogID { get; set; }
        public long ProductID { get; set; }
        public long UserID { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<decimal> ProductPrice_N { get; set; }
        public Nullable<decimal> ProductDiscountPrice { get; set; }
        public Nullable<decimal> ProductDiscountPrice_N { get; set; }
        public Nullable<int> ProductAvailableQuantity { get; set; }
        public Nullable<int> ProductAvailableQuantity_N { get; set; }
        public Nullable<bool> ProductActive { get; set; }
        public Nullable<bool> ProductActive_N { get; set; }
        public Nullable<int> MaxOrderQty { get; set; }
        public Nullable<int> MaxOrderQty_N { get; set; }
        public Nullable<int> MaxCustomerQty { get; set; }
        public Nullable<int> MaxCustomerQty_N { get; set; }
        public Nullable<int> MaxCustomerQtyDuration { get; set; }
        public Nullable<int> MaxCustomerQtyDuration_N { get; set; }
        public Nullable<int> MaxOrderQtyVerified { get; set; }
        public Nullable<int> MaxOrderQtyVerified_N { get; set; }
        public string UpdateAction { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductBarCode_N { get; set; }
        public string Location { get; set; }
        public string Location_N { get; set; }
        public string ProductCategoryAll { get; set; }
        public string ProductCategoryAll_N { get; set; }
        public Nullable<decimal> ProductCostPrice { get; set; }
        public Nullable<decimal> ProductCostPrice_N { get; set; }
        public Nullable<decimal> CashBack { get; set; }
        public Nullable<decimal> CashBack_N { get; set; }
        public string ProductHSCode { get; set; }
        public string ProductHSCode_N { get; set; }
    }
}
