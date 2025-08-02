using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class TimeTableExtensionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TimeTableExtensionDTO()
        {
            Class = new List<KeyValueDTO>();
            Section = new List<KeyValueDTO>();
            Subject = new List<KeyValueDTO>();
            Teacher = new List<KeyValueDTO>();
            WeekDay = new List<KeyValueDTO>();
            WeekDayOperator = new KeyValueDTO();
            ClassTiming = new List<KeyValueDTO>();
            ClassTimingOperator = new KeyValueDTO();
        }

        [DataMember]
        public long TimeTableExtIID { get; set; }

        [DataMember]
        public string TimeTableExtName { get; set; }

        [DataMember]
        public string TimeTableExtRemarks { get; set; }

        [DataMember]
        public int TimeTableID { get; set; }

        [DataMember]
        public byte? SubjectTypeID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? TimeTableExtParentID { get; set; }

        [DataMember]
        public string MinPeriodCountDay { get; set; }

        [DataMember]
        public string MaxPeriodCountDay { get; set; }

        [DataMember]
        public string PeriodCountWeek { get; set; }

        [DataMember]
        public bool? IsPeriodContinues { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string SubjectTypeName { get; set; }

        [DataMember]
        public string TimeTableName { get; set; }

        [DataMember]
        public string AcademicYearName { get; set; }

        [DataMember]
        public List<KeyValueDTO> Class { get; set; }

        [DataMember]
        public List<KeyValueDTO> Section { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subject { get; set; }

        [DataMember]
        public List<KeyValueDTO> Teacher { get; set; }

        [DataMember]
        public List<KeyValueDTO> WeekDay { get; set; }

        [DataMember]
        public KeyValueDTO WeekDayOperator { get; set; }

        [DataMember]
        public List<KeyValueDTO> ClassTiming { get; set; }

        [DataMember]
        public KeyValueDTO ClassTimingOperator { get; set; }

    }
}