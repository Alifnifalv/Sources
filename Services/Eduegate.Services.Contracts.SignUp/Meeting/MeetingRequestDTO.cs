using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.Meeting
{
    [DataContract]
    public class MeetingRequestDTO : BaseMasterDTO
    {
        public MeetingRequestDTO()
        {
            Student = new KeyValueDTO();
            Faculty = new KeyValueDTO();
        }

        [DataMember]
        public long MeetingRequestIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public long? FacultyID { get; set; }

        [DataMember]
        public KeyValueDTO Faculty { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public long? RequestedSignupSlotMapID { get; set; }

        [DataMember]
        public string RequestedSignupSlot { get; set; }

        [DataMember]
        public long? ApprovedSignupSlotMapID { get; set; }

        [DataMember]
        public string ApprovedSignupSlot { get; set; }

        [DataMember]
        public byte? MeetingRequestStatusID { get; set; }

        [DataMember]
        public string MeetingRequestStatusName { get; set; }
        
        [DataMember]
        public DateTime? RequestedDate { get; set; }

        [DataMember]
        public string RequestedDateString { get; set; }

        [DataMember]
        public DateTime? ApprovedDate { get; set; }

        [DataMember]
        public string ApprovedDateString { get; set; }

        [DataMember]
        public string GuardianEmailID { get; set; }

        [DataMember]
        public string RequestedSlotTime { get; set; }

        [DataMember]
        public string ApprovedSlotTime { get; set; }

        [DataMember]
        public byte? OldMeetingRequestStatusID { get; set; }

        [DataMember]
        public string RequesterRemark { get; set; }

        [DataMember]
        public string FacultyRemark { get; set; }

        [DataMember]
        public bool? IsSendNotification { get; set; }

        [DataMember]
        public long? ParentLoginID { get; set; }

        [DataMember]
        public long? FacultyLoginID { get; set; }

        [DataMember]
        public string FacultyEmailID { get; set; }
    }
}