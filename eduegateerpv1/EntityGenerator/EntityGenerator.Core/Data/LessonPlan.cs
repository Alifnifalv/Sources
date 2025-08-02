using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonPlans", Schema = "schools")]
    [Index("LessonPlanIID", "ClassID", "SubjectID", "Date1", "SectionID", "TotalHours", "Date2", "Date3", "LessonPlanStatusID", "CreatedBy", "UpdatedBy", "CreatedDate", "UpdatedDate", "TimeStamps", "SchoolID", "AcademicYearID", Name = "_dta_index_LessonPlans_7_79391402__K1_K2_K4_K6_K3_K5_K7_K8_K9_K10_K11_K12_K13_K14_K15_K16_17_18_19_20_21_22_23_24_25_26_27_28_")]
    public partial class LessonPlan
    {
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalHours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date3 { get; set; }
        public byte? LessonPlanStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
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

        [ForeignKey("AcademicYearID")]
        [InverseProperty("LessonPlans")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("LessonPlans")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExpectedLearningOutcomeID")]
        [InverseProperty("LessonPlans")]
        public virtual ExamType ExpectedLearningOutcome { get; set; }
        [ForeignKey("LessonPlanStatusID")]
        [InverseProperty("LessonPlans")]
        public virtual LessonPlanStatus LessonPlanStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("LessonPlans")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("LessonPlans")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("LessonPlans")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TeachingAidID")]
        [InverseProperty("LessonPlans")]
        public virtual TeachingAid TeachingAid { get; set; }
        [InverseProperty("LessonPlan")]
        public virtual ICollection<LessonPlanAttachmentMap> LessonPlanAttachmentMaps { get; set; }
        [InverseProperty("LessonPlan")]
        public virtual ICollection<LessonPlanClassSectionMap> LessonPlanClassSectionMaps { get; set; }
        [InverseProperty("LessonPlan")]
        public virtual ICollection<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }
        [InverseProperty("LessonPlan")]
        public virtual ICollection<LessonPlanTopicMap> LessonPlanTopicMaps { get; set; }
    }
}
