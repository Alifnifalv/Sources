using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClaimLoginMaps", Schema = "admin")]
    public partial class ClaimLoginMap
    {
        [Key]
        public long ClaimLoginMapIID { get; set; }
        public int? CompanyID { get; set; }
        public long? LoginID { get; set; }
        public long? ClaimID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClaimID")]
        [InverseProperty("ClaimLoginMaps")]
        public virtual Claim Claim { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("ClaimLoginMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("ClaimLoginMaps")]
        public virtual Login Login { get; set; }
    }
}
