using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("account.CostCenterAccountMaps")]

    public partial class CostCenterAccountMap
    {
        [Key]
        public long CostCenterAccountMapIID { get; set; }

        public int? CostCenterID { get; set; }

        public long? AccountID { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        public bool? IsAffect_A { get; set; }
        public bool? IsAffect_L { get; set; }
        public bool? IsAffect_C { get; set; }
        public bool? IsAffect_E { get; set; }
        public bool? IsAffect_I { get; set; }
    }
}
