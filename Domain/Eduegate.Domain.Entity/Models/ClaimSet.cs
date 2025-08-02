using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ClaimSets", Schema = "admin")]
    public partial class ClaimSet
    {
        public ClaimSet()
        {
            this.ClaimSetClaimMaps = new List<ClaimSetClaimMap>();
            this.ClaimSetClaimSetMapClaimSets = new List<ClaimSetClaimSetMap>();
            this.ClaimSetClaimSetMapLinkedClaimSets = new List<ClaimSetClaimSetMap>();
            this.ClaimSetLoginMaps = new List<ClaimSetLoginMap>();
        }

        [Key]
        public long ClaimSetIID { get; set; }
        public string ClaimSetName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID{ get; set; }
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMapClaimSets { get; set; }
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMapLinkedClaimSets { get; set; }
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
    }
}
