namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Claims", Schema = "admin")]
    public partial class Claim
    {
        public Claim()
        {
            ClaimLoginMaps = new HashSet<ClaimLoginMap>();
            ClaimSetClaimMaps = new HashSet<ClaimSetClaimMap>();
        }

        [Key]
        public long ClaimIID { get; set; }
        [StringLength(500)]
        public string ClaimName { get; set; }
        [StringLength(50)]
        public string ResourceName { get; set; }
        public int? ClaimTypeID { get; set; }
        [StringLength(50)]
        public string Rights { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        //[ForeignKey("ClaimTypeID")]
        //[InverseProperty("Claims")]
        public virtual ClaimType ClaimType { get; set; }
        //[InverseProperty("Claim")]
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
        //[InverseProperty("Claim")]
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
    }
}
