using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHeadEntitlementMap
    {
        public long TransactionHeadEntitlementMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public short EntitlementID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
