using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHeadEntitlementMap
    {
        public long TransactionHeadEntitlementMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public byte EntitlementID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ReferenceNo { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
