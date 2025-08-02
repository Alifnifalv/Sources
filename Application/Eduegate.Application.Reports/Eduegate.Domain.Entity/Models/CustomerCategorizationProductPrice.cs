using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerCategorizationProductPrice
    {
        public decimal ProductPriceID { get; set; }
        public int RefProductID { get; set; }
        public int RefSlabID { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> OriginalPrice { get; set; }
        public Nullable<decimal> AppliedDiscount { get; set; }
    }
}
