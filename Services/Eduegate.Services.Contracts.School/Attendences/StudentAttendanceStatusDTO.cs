using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Attendences
{
    [DataContract]
    public class StudentAttendanceStatusDTO : BaseMasterDTO
    {
        [DataMember]
        public long StudentIID { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string StudentFullName { get; set; }

        [DataMember]
        public string StudentProfile { get; set; } // The ID or path to the profile image

        [DataMember]
        public string AttendanceStatus { get; set; }

        [DataMember]
        public int? PresentStatusID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

    
    }
}


