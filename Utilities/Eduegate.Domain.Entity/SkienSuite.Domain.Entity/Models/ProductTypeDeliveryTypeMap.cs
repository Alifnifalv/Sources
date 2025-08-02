using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductTypeDeliveryTypeMap
    {
        public long ProductTypeDeliveryTypeMapIID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
        public virtual Company Company { get; set; }
    }
}
