namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin.ClaimSetClaimMaps")]
    public partial class ClaimSetClaimMap
    {
        [Key]
        public long ClaimSetClaimMapIID { get; set; }

        public long? ClaimSetID { get; set; }

        public long? ClaimID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Claim Claim { get; set; }

        public virtual ClaimSet ClaimSet { get; set; }
    }
}
