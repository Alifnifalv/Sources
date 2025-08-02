using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassSearch
    {
        public int ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public byte? ShiftID { get; set; }
        public byte? SchoolID { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(100)]
        public string CostCenterName { get; set; }
        [StringLength(50)]
        public string CostCenterCode { get; set; }
        public int? ORDERNO { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
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
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string IsActive { get; set; }
    }
}
