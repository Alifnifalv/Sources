namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ClaimSetLoginMaps", Schema = "admin")]
    public partial class ClaimSetLoginMap
    {
        [Key]
        public long ClaimSetLoginMapIID { get; set; }
        public int? CompanyID { get; set; }
        public long? LoginID { get; set; }
        public long? ClaimSetID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }

        //[ForeignKey("ClaimSetID")]
        //[InverseProperty("ClaimSetLoginMaps")]
        public virtual ClaimSet ClaimSet { get; set; }
        //[ForeignKey("CompanyID")]
        //[InverseProperty("ClaimSetLoginMaps")]
        public virtual Company Company { get; set; }
        //[ForeignKey("LoginID")]
        //[InverseProperty("ClaimSetLoginMaps")]
        public virtual Login Login { get; set; }
    }
}
