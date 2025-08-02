using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerSlabMultiprice
    {
        public long CustomerSlabMultipriceID { get; set; }
        public int RefCategoryID { get; set; }
        public int RefSlabID { get; set; }
        public decimal DiscountPer { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public decimal DiscountPerUnBranded { get; set; }
    }
}
