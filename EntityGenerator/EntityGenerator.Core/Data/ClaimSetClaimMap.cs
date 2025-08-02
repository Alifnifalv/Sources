using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClaimSetClaimMaps", Schema = "admin")]
    public partial class ClaimSetClaimMap
    {
        [Key]
        public long ClaimSetClaimMapIID { get; set; }
        public long? ClaimSetID { get; set; }
        public long? ClaimID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClaimID")]
        [InverseProperty("ClaimSetClaimMaps")]
        public virtual Claim Claim { get; set; }
        [ForeignKey("ClaimSetID")]
        [InverseProperty("ClaimSetClaimMaps")]
        public virtual ClaimSet ClaimSet { get; set; }
    }
}
