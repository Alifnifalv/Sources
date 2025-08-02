using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CostCenterSearch
    {
        public int CostCenterID { get; set; }
        [StringLength(100)]
        public string CostCenterName { get; set; }
        [StringLength(50)]
        public string CostCenterCode { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string IsAffect_A { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string IsAffect_L { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string IsAffect_C { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string IsAffect_E { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string IsAffect_I { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string IsFixed { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Balance { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string IsActive { get; set; }
    }
}
