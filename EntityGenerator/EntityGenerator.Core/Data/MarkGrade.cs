using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkGrades", Schema = "schools")]
    public partial class MarkGrade
    {
        public MarkGrade()
        {
            ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            ExamSubjectMaps = new HashSet<ExamSubjectMap>();
            Exams = new HashSet<Exam>();
            MarkGradeMaps = new HashSet<MarkGradeMap>();
            SkillGroupSubjectMaps = new HashSet<SkillGroupSubjectMap>();
        }

        [Key]
        public int MarkGradeIID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("MarkGrades")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("MarkGrades")]
        public virtual School School { get; set; }
        [InverseProperty("MarkGrade")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        [InverseProperty("MarkGrade")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }
        [InverseProperty("MarkGrade")]
        public virtual ICollection<ExamSubjectMap> ExamSubjectMaps { get; set; }
        [InverseProperty("MarkGrade")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("MarkGrade")]
        public virtual ICollection<MarkGradeMap> MarkGradeMaps { get; set; }
        [InverseProperty("MarkGrade")]
        public virtual ICollection<SkillGroupSubjectMap> SkillGroupSubjectMaps { get; set; }
    }
}
