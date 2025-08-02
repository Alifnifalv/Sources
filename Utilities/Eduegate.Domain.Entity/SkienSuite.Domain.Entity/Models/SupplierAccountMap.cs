using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierAccountMap
    {
        public long SupplierAccountMapIID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<short> EntitlementID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
