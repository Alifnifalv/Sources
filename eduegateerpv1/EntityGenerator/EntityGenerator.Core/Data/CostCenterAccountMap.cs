using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("CostCenterAccountMaps", Schema = "account")]
    public partial class CostCenterAccountMap
    {
        public long CostCenterAccountMapIID { get; set; }
        public int CostCenterID { get; set; }
        public long? AccountID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? GroupID { get; set; }
        public bool? IsAffect_A { get; set; }
        public bool? IsAffect_L { get; set; }
        public bool? IsAffect_C { get; set; }
        public bool? IsAffect_E { get; set; }
        public bool? IsAffect_I { get; set; }

        [ForeignKey("CostCenterID")]
        public virtual CostCenter CostCenter { get; set; }
    }
}
