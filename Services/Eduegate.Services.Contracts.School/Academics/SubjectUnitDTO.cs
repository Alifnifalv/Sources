using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SubjectUnitDTO : BaseMasterDTO
    {
        public SubjectUnitDTO()
        {
            Class = new KeyValueDTO();
            Subject = new KeyValueDTO();
            Chapter = new KeyValueDTO();
            SubjectUnit = new KeyValueDTO();
            Lessons = new List<LessonPlanDTO>();

            LessonLearningObjectiveName = new List<string>();
            LessonLearningOutcomeName = new List<string>();
            SkillDevolepment = new List<string>();
            TeachingMethodology = new List<LessonPlanTeachingMethodologyDTO>();
            Assessment = new List<LessonPlanAssessmentMapDTO>();
            DifferentiatedLearning = new LessonPlanDifferentiatedLearningMapDTO();
        }

        [DataMember]
        public long UnitIID { get; set; } // Primary Key

        [DataMember]
        public long? ParentSubjectUnitID { get; set; }

        [DataMember]
        public long? ChapterID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }


        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string UnitTitle { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long? TotalPeriods { get; set; }

        [DataMember]
        public long? TotalHours { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Chapter { get; set; }

        [DataMember]
        public KeyValueDTO SubjectUnit { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public KeyValueDTO ParentSubjectUnit { get; set; }


        [DataMember]
        public List<LessonPlanDTO> Lessons { get; set; }



        //New For AI Lesson Plannar
        [DataMember]
        public string LessonName { get; set; } //UnitTitle

        [DataMember]
        public List<string> LessonLearningObjectiveName { get; set; }

        [DataMember]
        public List<string> LessonLearningOutcomeName { get; set; }

        [DataMember]
        public List<string> SkillDevolepment { get; set; }


        [DataMember]
        public string AllignmentToVisionAndMission { get; set; }

        [DataMember]
        public string CrossDisciplinaryConnection { get; set; }

        [DataMember]
        public string HomeWorks { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string LocalAndGlobalConnection { get; set; }

        [DataMember]
        public string Resources { get; set; }

        [DataMember]
        public string ReferenceBook { get; set; }

        [DataMember]
        public List<LessonPlanAssessmentMapDTO> Assessment { get; set; } 

        [DataMember]
        public List<LessonPlanTeachingMethodologyDTO> TeachingMethodology { get; set; }

        [DataMember]
        public LessonPlanDifferentiatedLearningMapDTO DifferentiatedLearning { get; set; } 

    }

    public class LessonPlanTeachingMethodologyDTO : BaseMasterDTO
    {
        [DataMember]
        public long TeachingMethodologyMapIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public string ActivityName { get; set; }

        [DataMember]
        public string ActivityDescription { get; set; }

        [DataMember]
        public string EstimatedDuration { get; set; } //For Duration
    }

    public class LessonPlanAssessmentMapDTO : BaseMasterDTO 
    {
        [DataMember]
        public long AssessmentMapIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public string AssessmentName { get; set; }

        [DataMember]
        public string AssessmentDescription { get; set; } //For Description
    }

    public class LessonPlanDifferentiatedLearningMapDTO : BaseMasterDTO  
    {
        [DataMember]
        public long DifferentiatedLearningIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public string SEN { get; set; }

        [DataMember]
        public string HighAchievers { get; set; }

        [DataMember]
        public string StudentsWhoNeedImprovement { get; set; } 
    }
}
