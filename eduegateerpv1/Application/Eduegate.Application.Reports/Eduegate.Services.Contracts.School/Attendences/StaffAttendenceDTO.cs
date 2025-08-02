using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Attendences
{
    [DataContract]
    public class StaffAttendenceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  StaffAttendenceIID { get; set; }

        [DataMember]
        public DateTime?  AttendenceDate { get; set; }

        [DataMember]
        public long?  EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public byte?  PresentStatusID { get; set; }

        [DataMember]
        public int? AttendenceReasonID { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public TimeSpan?  StartTime { get; set; }

        [DataMember]
        public TimeSpan?  EndTime { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long StaffPresentCount { get; set; }

        [DataMember]
        public long StaffAbsentCount { get; set; }

        [DataMember]
        public long WorkingDayCount { get; set; }

        [DataMember]
        public string PresentStatusTitle { get; set; }

        [DataMember]
        public string PresentStatus { get; set; }
    }
}