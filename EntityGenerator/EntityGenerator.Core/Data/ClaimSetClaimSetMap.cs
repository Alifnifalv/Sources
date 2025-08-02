using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClaimSetClaimSetMaps", Schema = "admin")]
    public partial class ClaimSetClaimSetMap
    {
        [Key]
        public long ClaimSetClaimSetMapIID { get; set; }
        public long? ClaimSetID { get; set; }
        public long? LinkedClaimSetID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClaimSetID")]
        [InverseProperty("ClaimSetClaimSetMapClaimSets")]
        public virtual ClaimSet ClaimSet { get; set; }
        [ForeignKey("LinkedClaimSetID")]
        [InverseProperty("ClaimSetClaimSetMapLinkedClaimSets")]
        public virtual ClaimSet LinkedClaimSet { get; set; }
    }
}
