using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServicePricing
    {
        public long ServicePricingIID { get; set; }
        public long ServiceID { get; set; }
        public Nullable<decimal> Duration { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public string Caption { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Service Service { get; set; }
    }
}
