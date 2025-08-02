using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Tuition_Fee_OS_Opening_2022_Final
    {
        public long StudentIID { get; set; }
        public byte? SchoolID { get; set; }
        public int AcademicYearID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(63)]
        [Unicode(false)]
        public string AcademicYearCode { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Tuition_Fee_Op_2022 { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [Required]
        [StringLength(302)]
        public string ParentName { get; set; }
    }
}
