using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.SignUps
{
    [DataContract]
    public class SignupSlotMapDTO : BaseMasterDTO
    {
        public SignupSlotMapDTO()
        {
            SignupSlotAllocationMaps = new List<SignupSlotAllocationMapDTO>();
            SignupSlotMapTimes = new List<SignupSlotMapDTO>();
            SignupStudent = new SignupStudentDTO();
            SignupSlotAllocationRemarkMap = new SignupSlotRemarkMapDTO();
        }

        [DataMember]
        public long SignupSlotMapIID { get; set; }

        [DataMember]
        public byte SignupSlotTypeID { get; set; }

        [DataMember]
        public string SignupSlotType { get; set; }

        [DataMember]
        public DateTime? SlotDate { get; set; }

        [DataMember]
        public string SlotDateString { get; set; }

        [DataMember]
        public TimeSpan? StartTime { get; set; }

        [DataMember]
        public string StartTimeString { get; set; }

        [DataMember]
        public TimeSpan? EndTime { get; set; }

        [DataMember]
        public string EndTimeString { get; set; }

        [DataMember]
        public decimal? Duration { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public long? SignupID { get; set; }

        [DataMember]
        public byte? SlotMapStatusID { get; set; }

        [DataMember]
        public string SlotMapStatusName { get; set; }

        [DataMember]
        public List<SignupSlotAllocationMapDTO> SignupSlotAllocationMaps { get; set; }

        [DataMember]
        public List<SignupSlotMapDTO> SignupSlotMapTimes { get; set; }

        [DataMember]
        public int? TimeMapsCount { get; set; }

        [DataMember]
        public bool? IsSlotAllocated { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string Student { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string Employee { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public string Parent { get; set; }

        [DataMember]
        public bool? IsSlotActive { get; set; }

        [DataMember]
        public SignupStudentDTO SignupStudent { get; set; }

        [DataMember]
        public SignupSlotRemarkMapDTO SignupSlotAllocationRemarkMap { get; set; }

        [DataMember]
        public bool? IsSlotDateCurrentDate { get; set; }

        [DataMember]
        public bool? IsTimeExpired { get; set; }

        [DataMember]
        public byte? OldSlotMapStatusID { get; set; }

        [DataMember]
        public long? SignupOrganizerEmployeeID { get; set; }

        [DataMember]
        public string SignupOrganizerEmployeeName { get; set; }
    }
}