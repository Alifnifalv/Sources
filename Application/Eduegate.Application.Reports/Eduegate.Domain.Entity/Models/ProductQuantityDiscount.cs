using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductQuantityDiscount
    {
        public int DiscountID { get; set; }
        public Nullable<long> RefProductID { get; set; }
        public Nullable<short> Quantity { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
