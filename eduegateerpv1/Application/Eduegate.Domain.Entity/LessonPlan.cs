namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.LessonPlans")]
    public partial class LessonPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonPlan()
        {
            LessonPlanAttachmentMaps = new HashSet<LessonPlanAttachmentMap>();
            LessonPlanClassSectionMaps = new HashSet<LessonPlanClassSectionMap>();
            LessonPlanTaskMaps = new HashSet<LessonPlanTaskMap>();
            LessonPlanTopicMaps = new HashSet<LessonPlanTopicMap>();
        }

        [Key]
        public long LessonPlanIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectID { get; set; }

        public decimal? TotalHours { get; set; }

        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }

        public DateTime? Date3 { get; set; }

        public byte? LessonPlanStatusID { get; set; }

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

        [StringLength(20)]
        public string LessonPlanCode { get; set; }

        [StringLength(500)]
        public string LearningExperiences { get; set; }

        public byte? NumberOfPeriods { get; set; }

        public byte? NumberOfClassTests { get; set; }

        public byte? NumberOfActivityCompleted { get; set; }

        [StringLength(500)]
        public string Activity { get; set; }

        [StringLength(500)]
        public string HomeWorks { get; set; }

        public byte? TeachingAidID { get; set; }

        public byte? MonthID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ReferenceID { get; set; }

        [StringLength(250)]
        public string Resourses { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        [StringLength(500)]
        public string SkillFocused { get; set; }

        [StringLength(500)]
        public string AllignmentToVisionAndMission { get; set; }

        [StringLength(500)]
        public string Connectivity { get; set; }

        [StringLength(500)]
        public string CrossDisciplinaryConnection { get; set; }

        public string Introduction { get; set; }

        [StringLength(500)]
        public string TeachingMethodology { get; set; }

        [StringLength(500)]
        public string Closure { get; set; }

        [StringLength(500)]
        public string SEN { get; set; }

        [StringLength(500)]
        public string HighAchievers { get; set; }

        [StringLength(500)]
        public string StudentsWhoNeedImprovement { get; set; }

        [StringLength(500)]
        public string PostLessonEvaluation { get; set; }

        [StringLength(500)]
        public string Reflections { get; set; }

        public byte? ExpectedLearningOutcomeID { get; set; }

        public bool? IsSyllabusCompleted { get; set; }

        public string ActionPlan { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual ExamType ExamType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanAttachmentMap> LessonPlanAttachmentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanClassSectionMap> LessonPlanClassSectionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }

        public virtual LessonPlanStatus LessonPlanStatus { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual TeachingAid TeachingAid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTopicMap> LessonPlanTopicMaps { get; set; }
    }
}
