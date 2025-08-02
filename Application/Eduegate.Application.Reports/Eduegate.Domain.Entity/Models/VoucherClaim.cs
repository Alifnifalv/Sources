using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherClaim
    {
        public long ClaimId { get; set; }
        public Nullable<long> RefCustomerID { get; set; }
        public Nullable<long> RefOrderID { get; set; }
        public Nullable<System.DateTime> ClaimDate { get; set; }
        public Nullable<decimal> ClaimAmount { get; set; }
        public Nullable<decimal> ClaimPoint { get; set; }
        public virtual CustomerMaster CustomerMaster { get; set; }
        public virtual OrderMaster OrderMaster { get; set; }
    }
}
