namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ClaimSetClaimSetMaps", Schema = "admin")]
    public partial class ClaimSetClaimSetMap
    {
        [Key]
        public long ClaimSetClaimSetMapIID { get; set; }

        public long? ClaimSetID { get; set; }

        public long? LinkedClaimSetID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        //[ForeignKey("ClaimSetID")]
        //[InverseProperty("ClaimSetClaimSetMapClaimSets")]
        public virtual ClaimSet ClaimSet { get; set; }
        //[ForeignKey("LinkedClaimSetID")]
        //[InverseProperty("ClaimSetClaimSetMapLinkedClaimSets")]
        public virtual ClaimSet LinkedClaimSet { get; set; }
    }
}
