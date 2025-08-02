namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.Exams")]
    public partial class Exam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? ExamGroupID { get; set; }

        public decimal? ConversionFactor { get; set; }

        public bool? IsProgressCard { get; set; }

        public bool? IsAnnualExam { get; set; }

        [StringLength(100)]
        public string ProgressCardHeader { get; set; }

        public bool? IsAssessment { get; set; }

        public bool? IsConverted { get; set; }

        public int? OrderBy { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }

        public virtual ExamGroup ExamGroup { get; set; }

        public virtual ExamType ExamType { get; set; }

        public virtual MarkGrade MarkGrade { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSchedule> ExamSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSkillMap> ExamSkillMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSubjectMap> ExamSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
    }
}
