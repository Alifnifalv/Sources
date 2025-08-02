namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.CostCenterAccountMaps")]
    public partial class CostCenterAccountMap
    {
        [Key]
        [Column(Order = 0)]
        public long CostCenterAccountMapIID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CostCenterID { get; set; }

        public long? AccountID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? GroupID { get; set; }

        public bool? IsAffect_A { get; set; }

        public bool? IsAffect_L { get; set; }

        public bool? IsAffect_C { get; set; }

        public bool? IsAffect_E { get; set; }

        public bool? IsAffect_I { get; set; }

        public virtual CostCenter CostCenter { get; set; }
    }
}
