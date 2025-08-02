using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SKUPaymentMethodExceptionMaps
    { 
        public int SKUPaymentMethodExceptionMapIID { get; set; } 
        public Nullable<long> SKUID { get; set; } 
        public Nullable<short> PaymentMethodID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<int> CompnayID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ProductSKUMap ProductSKUMaps { get; set; } 
    }
}
