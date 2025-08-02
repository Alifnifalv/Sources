using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ClaimSetClaimSetMaps", Schema = "admin")]
    public partial class ClaimSetClaimSetMap
    {
        [Key]
        public long ClaimSetClaimSetMapIID { get; set; }
        public Nullable<long> ClaimSetID { get; set; }
        public Nullable<long> LinkedClaimSetID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public virtual ClaimSet ClaimSet { get; set; }
        public virtual ClaimSet LinkedClaimSet { get; set; }
    }
}
