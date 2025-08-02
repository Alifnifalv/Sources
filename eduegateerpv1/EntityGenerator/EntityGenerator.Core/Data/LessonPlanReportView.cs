using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LessonPlanReportView
    {
        [StringLength(15)]
        public string MonthName { get; set; }
        public byte? SchoolID { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string ExpectedLearningOutcome { get; set; }
        [StringLength(500)]
        public string LearningExperiences { get; set; }
        public int? ClassID { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(123)]
        public string AcademicYearName { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
        public long LessonPlanIID { get; set; }
        [StringLength(50)]
        public string LessonPlanStatus { get; set; }
        public byte? LessonPlanStatusID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? MonthID { get; set; }
        public byte? NumberOfPeriods { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public byte? ExpectedLearningOutcomeID { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
        [StringLength(250)]
        public string Resourses { get; set; }
        [StringLength(500)]
        public string HomeWorks { get; set; }
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
        public string Activity { get; set; }
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
    }
}
