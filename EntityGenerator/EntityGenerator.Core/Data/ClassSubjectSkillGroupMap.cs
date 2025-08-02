using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassSubjectSkillGroupMaps", Schema = "schools")]
    public partial class ClassSubjectSkillGroupMap
    {
        public ClassSubjectSkillGroupMap()
        {
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            ExamSkillMaps = new HashSet<ExamSkillMap>();
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            SkillGroupSubjectMaps = new HashSet<SkillGroupSubjectMap>();
        }

        [Key]
        public long ClassSubjectSkillGroupMapID { get; set; }
        public long? ExamID { get; set; }
        public int? ClassID { get; set; }
        public int? SubjectID { get; set; }
        public int? MarkGradeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalMarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumMarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }
        public string Description { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? ISScholastic { get; set; }
        public string ProgressCardHeader { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassSubjectSkillGroupMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassSubjectSkillGroupMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamID")]
        [InverseProperty("ClassSubjectSkillGroupMaps")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("MarkGradeID")]
        [InverseProperty("ClassSubjectSkillGroupMaps")]
        public virtual MarkGrade MarkGrade { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassSubjectSkillGroupMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassSubjectSkillGroupMaps")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("ClassSubjectSkillGroupMap")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }
        [InverseProperty("ClassSubjectSkillGroupMap")]
        public virtual ICollection<ExamSkillMap> ExamSkillMaps { get; set; }
        [InverseProperty("ClassSubjectSkillGroupMap")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        [InverseProperty("ClassSubjectSkillGroupMap")]
        public virtual ICollection<SkillGroupSubjectMap> SkillGroupSubjectMaps { get; set; }
    }
}
