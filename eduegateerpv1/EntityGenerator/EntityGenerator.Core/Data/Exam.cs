using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Exams", Schema = "schools")]
    public partial class Exam
    {
        public Exam()
        {
            ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            ExamClassMaps = new HashSet<ExamClassMap>();
            ExamSchedules = new HashSet<ExamSchedule>();
            ExamSkillMaps = new HashSet<ExamSkillMap>();
            ExamSubjectMaps = new HashSet<ExamSubjectMap>();
            MarkRegisters = new HashSet<MarkRegister>();
            ProgressReports = new HashSet<ProgressReport>();
            RemarksEntryExamMaps = new HashSet<RemarksEntryExamMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
        }

        [Key]
        public long ExamIID { get; set; }
        [StringLength(100)]
        public string ExamDescription { get; set; }
        [StringLength(100)]
        public string ExamNumber { get; set; }
        public byte? ExamTypeID { get; set; }
        public int? MarkGradeID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ExamGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }
        public bool? IsProgressCard { get; set; }
        public bool? IsAnnualExam { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ProgressCardHeader { get; set; }
        public bool? IsAssessment { get; set; }
        public bool? IsConverted { get; set; }
        public int? OrderBy { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Exams")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ExamGroupID")]
        [InverseProperty("Exams")]
        public virtual ExamGroup ExamGroup { get; set; }
        [ForeignKey("ExamTypeID")]
        [InverseProperty("Exams")]
        public virtual ExamType ExamType { get; set; }
        [ForeignKey("MarkGradeID")]
        [InverseProperty("Exams")]
        public virtual MarkGrade MarkGrade { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Exams")]
        public virtual School School { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<ExamSchedule> ExamSchedules { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<ExamSkillMap> ExamSkillMaps { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<ExamSubjectMap> ExamSubjectMaps { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
    }
}
