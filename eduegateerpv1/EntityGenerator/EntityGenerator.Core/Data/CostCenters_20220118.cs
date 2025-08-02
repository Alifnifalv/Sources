using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CostCenters_20220118
    {
        public int CostCenterID { get; set; }
        [StringLength(50)]
        public string CostCenterCode { get; set; }
        [StringLength(100)]
        public string CostCenterName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAffect_A { get; set; }
        public bool? IsAffect_L { get; set; }
        public bool? IsAffect_C { get; set; }
        public bool? IsAffect_E { get; set; }
        public bool? IsAffect_I { get; set; }
        public bool? IsFixed { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
    }
}
