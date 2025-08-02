using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("SignupSlotMaps", Schema = "signup")]
    public partial class SignupSlotMap
    {
        public SignupSlotMap()
        {
            MeetingRequestApprovedSignupSlotMaps = new HashSet<MeetingRequest>();
            MeetingRequestRequestedSignupSlotMaps = new HashSet<MeetingRequest>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            SignupSlotRemarkMaps = new HashSet<SignupSlotRemarkMap>();
        }

        [Key]
        public long SignupSlotMapIID { get; set; }

        public byte SignupSlotTypeID { get; set; }

        public DateTime? SlotDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public decimal? Duration { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? SignupID { get; set; }

        public byte? SlotMapStatusID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual Signup Signup { get; set; }

        public virtual SignupSlotType SignupSlotType { get; set; }

        public virtual SlotMapStatus SlotMapStatus { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequestApprovedSignupSlotMaps { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequestRequestedSignupSlotMaps { get; set; }

        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }

        public virtual ICollection<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }

    }
}