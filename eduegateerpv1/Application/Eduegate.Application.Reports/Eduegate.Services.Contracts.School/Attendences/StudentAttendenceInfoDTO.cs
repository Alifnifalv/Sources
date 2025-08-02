using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Attendences
{
    [DataContract]
    public class StudentAttendenceInfoDTO : BaseMasterDTO
    {
        [DataMember]
        public long StudentID { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string AttendanceDateString { get; set; }

        [DataMember]
        public byte? PresentStatusID { get; set; }

        [DataMember]
        public int? AttendanceReasonID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }
    }
}


