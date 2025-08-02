using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDayDealLog
    {
        public int ProductDayDealLogID { get; set; }
        public int RefDealID { get; set; }
        public int RefProductID { get; set; }
        public int ProductAvailableQtyLog { get; set; }
        public bool PickUpShowroomLog { get; set; }
        public int MaxCustomerQtyLog { get; set; }
        public int MaxOrderQtyLog { get; set; }
        public int MaxCustomerQtyDurationLog { get; set; }
        public int MaxOrderQtyVerifiedLog { get; set; }
        public bool MultiPrice { get; set; }
        public bool QuantityDiscount { get; set; }
        public decimal RefProductDiscountPrice { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
