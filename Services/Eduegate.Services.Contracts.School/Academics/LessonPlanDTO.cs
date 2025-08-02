using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class LessonPlanDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LessonPlanDTO()
        {
            Class = new List<KeyValueDTO>();
            Section = new List<KeyValueDTO>();
            Subject = new KeyValueDTO();
            SubjectUnit = new KeyValueDTO();
            LessonPlanStatus = new KeyValueDTO();
            LessonPlanAttachmentMap = new List<LessonPlanAttachmentMapDTO>();
            LessonPlanClassSectionMaps = new List<LessonPlanClassSectionMapDTO>();
            LessonPlanTopicMap = new List<LessonPlanTopicMapDTO>();
            LessonPlanTask = new List<LessonPlanTaskMapDTO>();
            LessonPlanLearningOutcomeMap = new List<LessonPlanOutcomeMapDTO>();
            LessonLearningOutcome = new List<KeyValueDTO>();
            TeachingAids = new List<KeyValueDTO>();
        }

        [DataMember]
        public long LessonPlanIID { get; set; }

        [DataMember]
        public int LessonPlanID { get; set; }

        [DataMember]
        public byte? LessonPlanStatusID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public System.DateTime? Date1 { get; set; }

        [DataMember]
        public System.DateTime? Date2 { get; set; }

        [DataMember]
        public System.DateTime? Date3 { get; set; }

        [DataMember]
        public decimal? TotalHours { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string LessonPlanCode { get; set; }

        [DataMember]
        public string LearningExperiences { get; set; }

        [DataMember]
        public byte? NumberOfPeriods { get; set; }

        [DataMember]
        public byte? NumberOfClassTests { get; set; }

        [DataMember]
        public byte? NumberOfActivityCompleted { get; set; }

        [DataMember]
        public string Activity { get; set; }

        [DataMember]
        public string HomeWorks { get; set; }

        [DataMember]
        public byte? TeachingAidID { get; set; }

        [DataMember]
        public byte? MonthID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string MonthName { get; set; }

        [DataMember]
        public List<KeyValueDTO> Class { get; set; }

        [DataMember]
        public List<KeyValueDTO> Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO LessonPlanStatus { get; set; }

        [DataMember]
        public string Resourses { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string SkillFocused { get; set; }
        [DataMember]
        public string AllignmentToVisionAndMission { get; set; }
        [DataMember]
        public string Connectivity { get; set; }
        [DataMember]
        public string CrossDisciplinaryConnection { get; set; }
        [DataMember]
        public string Introduction { get; set; }
        [DataMember]
        public string TeachingMethodology { get; set; }
        [DataMember]
        public string Closure { get; set; }
        [DataMember]
        public string SEN { get; set; }
        [DataMember]
        public string HighAchievers { get; set; }
        [DataMember]
        public string StudentsWhoNeedImprovement { get; set; }
        [DataMember]
        public string PostLessonEvaluation { get; set; }
        [DataMember]
        public string Reflections { get; set; }
        [DataMember]
        public byte? ExpectedLearningOutcomeID { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string ExpectedLearningOutcomeString { get; set; }

        [DataMember]
        public string MonthNameString { get; set; }

        [DataMember]
        public List<LessonPlanAttachmentMapDTO> LessonPlanAttachmentMap { get; set; }

        [DataMember]
        public List<LessonPlanClassSectionMapDTO> LessonPlanClassSectionMaps { get; set; }

        [DataMember]
        public bool? IsSendPushNotification { get; set; }

        [DataMember]
        public bool? IsSyllabusCompleted { get; set; }

        [DataMember]
        public string ActionPlan { get; set; }


        [DataMember]
        public List<LessonPlanTopicMapDTO> LessonPlanTopicMap { get; set; }

        [DataMember]
        public List<LessonPlanTaskMapDTO> LessonPlanTask { get; set; }

        [DataMember]
        public List<KeyValueDTO> TeachingAids { get; set; }

        [DataMember]
        public List<LessonPlanOutcomeMapDTO> LessonPlanLearningOutcomeMap { get; set; }

        [DataMember]
        public List<LessonPlanObjectiveMapDTO> LessonPlanLearningObjectiveMap { get; set; }

        [DataMember]
        public List<KeyValueDTO> LessonLearningOutcome { get; set; }

        

        [DataMember]
        public KeyValueDTO SubjectUnit { get; set; }

        [DataMember]
        public long? UnitID { get; set; }
    }
}