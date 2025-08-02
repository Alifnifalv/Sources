using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntitlementMap
    {
        public long EntitlementMapIID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public Nullable<decimal> EntitlementAmount { get; set; }
        public Nullable<short> EntitlementDays { get; set; }
        public Nullable<short> EntitlementID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
