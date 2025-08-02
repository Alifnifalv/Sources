using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerGroupDeliveryTypeMap
    {
        public long CustomerGroupDeliveryTypeMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
