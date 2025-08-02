using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class LessonPlanDTO_OLD : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
       
        [DataMember]
        public long LessonPlanIID { get; set; }

        [DataMember]
        public int LessonPlanID { get; set; }
       
        [DataMember]
        public byte? LessonPlanStatusID { get; set; }
        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

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
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public List<KeyValueDTO> TeachingAid { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO LessonPlanStatus { get; set; }

        [DataMember]
        public List<LessonPlanTopicMapDTO> LessonPlanTopicMap { get; set; }

        [DataMember]
        public List<LessonPlanTaskMapDTO> LessonPlanTask { get; set; }

    }
}
