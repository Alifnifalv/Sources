using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_STUDENT_MARKS
    {
        public byte SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int AcademicYearID { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        public int ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        public int SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public int ExamGroupID { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        public long ExamIID { get; set; }
        [StringLength(100)]
        public string ExamName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CF { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TotalMarks { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? TotalMarks_100 { get; set; }
        public int CF_100 { get; set; }
    }
}
