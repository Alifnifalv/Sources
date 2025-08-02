using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MarkRegisterReportView
    {
        public long MarkRegisterIID { get; set; }
        public long? ExamID { get; set; }
        [StringLength(100)]
        public string ExamDescription { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? StudentId { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DateOfBirth { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(115)]
        public string AcademicYearName { get; set; }
        public byte? MarkEntryStatusID { get; set; }
        [StringLength(50)]
        public string MarkEntryStatusName { get; set; }
        public int? ExamGroupID { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        public byte? PresentStatusID { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
        [StringLength(10)]
        public string StatusTitle { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public long? MarksGradeMapID { get; set; }
        [StringLength(50)]
        public string GradeName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public bool? IsAbsent { get; set; }
        public bool? IsPassed { get; set; }
        [Required]
        [StringLength(302)]
        public string GuardianName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }
        [StringLength(63)]
        [Unicode(false)]
        public string Academic { get; set; }
    }
}
