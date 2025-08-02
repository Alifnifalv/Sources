using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassSubjectSkillGroupMapSearchView
    {
        public long ClassSubjectSkillGroupMapID { get; set; }
        [StringLength(4000)]
        public string SkillGroupMaster { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SkillSetName { get; set; }
        public int? MarkGradeID { get; set; }
        [StringLength(100)]
        public string MarkGrade { get; set; }
        public long? ExamID { get; set; }
        [StringLength(100)]
        public string ExamName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumMarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalMarks { get; set; }
    }
}
