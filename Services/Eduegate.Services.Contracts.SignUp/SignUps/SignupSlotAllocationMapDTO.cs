using Eduegate.Framework.Contracts.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.SignUps
{
    [DataContract]
    public class SignupSlotAllocationMapDTO : BaseMasterDTO
    {
        public SignupSlotAllocationMapDTO()
        {
            SignupSlotRemarkMap = new SignupSlotRemarkMapDTO();
        }

        [DataMember]
        public long SignupSlotAllocationMapIID { get; set; }

        [DataMember]
        public long? SignupSlotMapID { get; set; }

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
        public byte? SchoolID { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public byte? SlotMapStatusID { get; set; }

        [DataMember]
        public string SlotMapStatus { get; set; }

        [DataMember]
        [StringLength(50)]
        public string SignupName { get; set; }

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
        public string SlotTimeString { get; set; }

        [DataMember]
        public SignupSlotRemarkMapDTO SignupSlotRemarkMap { get; set; }

        [DataMember]
        public byte? OldSlotMapStatusID { get; set; }

        [DataMember]
        public string GuardianEmailID { get; set; }

        [DataMember]
        public long? OrganizerEmployeeID { get; set; }

        [DataMember]
        public string OrganizerEmployeeName { get; set; }

        [DataMember]
        public long? OrganizerEmployeeLoginID { get; set; }

        [DataMember]
        public string OrganizerEmployeeEmailID { get; set; }

        [DataMember]
        public long? ParentLoginID { get; set; }

        [DataMember]
        public string SignupGroupTitle { get; set; }

        [DataMember]
        public string SignupGroupDescription { get; set; }

    }
}