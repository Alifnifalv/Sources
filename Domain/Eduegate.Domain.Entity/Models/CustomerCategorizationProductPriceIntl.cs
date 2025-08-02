using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerCategorizationProductPriceIntl
    {
        [Key]
        public decimal ProductPriceID { get; set; }
        public int RefProductID { get; set; }
        public int RefSlabID { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> OriginalPrice { get; set; }
        public Nullable<decimal> AppliedDiscount { get; set; }
        public Nullable<short> RefCountryID { get; set; }
    }
}
