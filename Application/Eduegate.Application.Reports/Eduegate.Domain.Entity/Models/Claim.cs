using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Claim
    {
        public Claim()
        {
            this.ClaimSetClaimMaps = new List<ClaimSetClaimMap>();
        }

        public long ClaimIID { get; set; }
        public string ClaimName { get; set; }
        public string ResourceName { get; set; }
        public Nullable<int> ClaimTypeID { get; set; }
        public string Rights { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ClaimType ClaimType { get; set; }
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
    }
}
