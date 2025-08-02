using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ClaimSet
    {
        public ClaimSet()
        {
            this.ClaimSetClaimMaps = new List<ClaimSetClaimMap>();
            this.ClaimSetClaimSetMaps = new List<ClaimSetClaimSetMap>();
            this.ClaimSetClaimSetMaps1 = new List<ClaimSetClaimSetMap>();
            this.ClaimSetLoginMaps = new List<ClaimSetLoginMap>();
        }

        public long ClaimSetIID { get; set; }
        public string ClaimSetName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID{ get; set; }
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMaps { get; set; }
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMaps1 { get; set; }
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
    }
}
