namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonPlans", Schema = "schools")]
    public partial class LessonPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonPlan()
        {
            LessonPlanAttachmentMaps = new HashSet<LessonPlanAttachmentMap>();
            LessonPlanTaskMaps = new HashSet<LessonPlanTaskMap>();
            LessonPlanTopicMaps = new HashSet<LessonPlanTopicMap>();
            LessonPlanClassSectionMaps = new HashSet<LessonPlanClassSectionMap>();
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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(20)]
        public string LessonPlanCode { get; set; }

        [StringLength(500)]
        public string LearningExperiences { get; set; }

        public byte? NumberOfPeriods { get; set; }

        public byte? NumberOfClassTests { get; set; }

        public byte? NumberOfActivityCompleted { get; set; }

        public string Activity { get; set; }

        public string HomeWorks { get; set; }

        public byte? TeachingAidID { get; set; }

        public byte? MonthID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ReferenceID { get; set; }

        public string Resourses { get; set; }

        public string Content { get; set; }

        public string SkillFocused { get; set; }

        public string AllignmentToVisionAndMission { get; set; }

        public string Connectivity { get; set; }

        public string CrossDisciplinaryConnection { get; set; }

        public string Introduction { get; set; }

        public string TeachingMethodology { get; set; }

        public string Closure { get; set; }

        public string SEN { get; set; }

        public string HighAchievers { get; set; }

        public string StudentsWhoNeedImprovement { get; set; }

        public string PostLessonEvaluation { get; set; }

        public string Reflections { get; set; }

        public byte? ExpectedLearningOutcomeID { get; set; }

        public bool? IsSyllabusCompleted { get; set; }
        public string ActionPlan { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        public virtual ExamType ExamType { get; set; }
        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanAttachmentMap> LessonPlanAttachmentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }

        public virtual LessonPlanStatus LessonPlanStatus { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual TeachingAid TeachingAid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanTopicMap> LessonPlanTopicMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlanClassSectionMap> LessonPlanClassSectionMaps { get; set; }
    }
}