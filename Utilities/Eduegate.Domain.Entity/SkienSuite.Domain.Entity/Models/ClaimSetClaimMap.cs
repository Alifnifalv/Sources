using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ClaimSetClaimMap
    {
        public long ClaimSetClaimMapIID { get; set; }
        public Nullable<long> ClaimSetID { get; set; }
        public Nullable<long> ClaimID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Claim Claim { get; set; }
        public virtual ClaimSet ClaimSet { get; set; }
    }
}
