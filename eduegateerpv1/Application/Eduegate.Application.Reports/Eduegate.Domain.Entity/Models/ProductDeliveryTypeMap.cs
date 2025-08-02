using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDeliveryTypeMap
    {
        public long ProductDeliveryTypeMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<int> CreatedBy { get; set; } 
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
        public virtual Branch Branch { get; set; } 
    }
}
