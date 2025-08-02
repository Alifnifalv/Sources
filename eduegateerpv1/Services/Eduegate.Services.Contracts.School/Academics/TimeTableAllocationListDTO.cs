using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class TimeTableAllocationListDTO : BaseMasterDTO
    {
        public TimeTableAllocationListDTO()
        {
            WeekDay = new KeyValueDTO();
            ClassTiming = new KeyValueDTO();
            MapDetails = new List<TimeTableAllocationListDTO>();
        }

        [DataMember]
        public int? WeekDayID { get; set; }

        [DataMember]
        public int? ClassTimingID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? ClassId { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO ClassTiming { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO WeekDay { get; set; }

        [DataMember]
        public List<TimeTableAllocationListDTO> MapDetails { get; set; }

    }

}