using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Lms.Lms;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Lms
{
    [DataContract]
    public class LmsSlotMapDTO : BaseMasterDTO
    {
        public LmsSlotMapDTO()
        {
            SignupSlotAllocationMaps = new List<LmsSlotAllocationMapDTO>();
            SignupSlotMapTimes = new List<LmsSlotMapDTO>();
            SignupStudent = new LmsStudentDTO();
            SignupSlotAllocationRemarkMap = new LmsSlotRemarkMapDTO();
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
        public List<LmsSlotAllocationMapDTO> SignupSlotAllocationMaps { get; set; }

        [DataMember]
        public List<LmsSlotMapDTO> SignupSlotMapTimes { get; set; }

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
        public LmsStudentDTO SignupStudent { get; set; }

        [DataMember]
        public LmsSlotRemarkMapDTO SignupSlotAllocationRemarkMap { get; set; }

        [DataMember]
        public bool? IsSlotDateCurrentDate { get; set; }

        [DataMember]
        public bool? IsTimeExpired { get; set; }

        [DataMember]
        public byte? OldSlotMapStatusID { get; set; }

        [DataMember]
        public string SignupOrganizerEmployeeName { get; set; }
    }
}