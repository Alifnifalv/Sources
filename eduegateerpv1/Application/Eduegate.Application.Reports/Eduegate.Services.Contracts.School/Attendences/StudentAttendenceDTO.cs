using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Attendences
{
    [DataContract]
    public class StudentAttendenceDTO : BaseMasterDTO
    {
        [DataMember]
        public long  StudentAttendenceIID { get; set; }

        [DataMember]
        public long?  StudentID { get; set; }

        [DataMember]
        public DateTime?  AttendenceDate { get; set; }

        [DataMember]
        public byte?  PresentStatusID { get; set; }

        [DataMember]
        public int? AttendenceReasonID { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string PresentStatus { get; set; }

        [DataMember]
        public TimeSpan?  StartTime { get; set; }

        [DataMember]
        public TimeSpan?  EndTime { get; set; }

        [DataMember]
        public int?  ClassID { get; set; }

        [DataMember]
        public int?  SectionID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string PresentStatusTitle { get; set; }

        [DataMember]
        public DateTime? AdmissionDate { get; set; }

        [DataMember]
        public DateTime? FeeStartDate { get; set; }

        [DataMember]
        public long StudentPresentCount { get; set; }

        [DataMember]
        public long StudentAbsentCount { get; set; }

        [DataMember]
        public long WorkingDayCount { get; set; }
    }
}


