using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ChapterDTO : BaseMasterDTO
    {
        public ChapterDTO()
        {
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Subject = new KeyValueDTO();
        }

        [DataMember]
        public long ChapterIID { get; set; } // Primary Key

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string ChapterTitle { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? TotalPeriods { get; set; }

        [DataMember]
        public long? TotalHours { get; set; }


        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]

        public string SubjectName { get; set; }

        [DataMember]

        public string ClassName { get; set; }

        [DataMember]
        public List<LessonPlanDTO> Lessons { get; set; } = new List<LessonPlanDTO>();

        [DataMember]
        public List<SubjectUnitDTO> SubjectUnits { get; set; } = new List<SubjectUnitDTO>();

    }

    public class LessonPlanChapterDTO : BaseMasterDTO
    {
        public LessonPlanChapterDTO()
        {
            SubjectUnits = new List<SubjectUnitDTO>();
        }

        [DataMember]
        public long ChapterIID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string ChapterTitle { get; set; }

        [DataMember]
        public long? TotalPeriods { get; set; }

        [DataMember]
        public List<SubjectUnitDTO> SubjectUnits { get; set; } 

    }
}
