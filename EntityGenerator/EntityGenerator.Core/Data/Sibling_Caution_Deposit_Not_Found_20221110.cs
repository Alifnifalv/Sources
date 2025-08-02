using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Sibling_Caution_Deposit_Not_Found_20221110
    {
        public double? StudentIID { get; set; }
        public double? SchoolID { get; set; }
        public double? AcademicYearID { get; set; }
        [StringLength(255)]
        public string School { get; set; }
        [StringLength(255)]
        public string AcademicYear { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(255)]
        public string Section { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(255)]
        public string ParentCode { get; set; }
        [StringLength(255)]
        public string ParentName { get; set; }
        public double? SecurityDeposit { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
