using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductInventorySerialMap
    {
        public long ProductSKUMapID { get; set; }
        public long ProductInventorySerialMapIID { get; set; }
        public long Batch { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public long BranchID { get; set; }
        public string SerialNo { get; set; }
        public Nullable<bool> Used { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
    }
}
