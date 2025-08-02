using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? SlotDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal? Duration { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? SignupID { get; set; }
        public byte? SlotMapStatusID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SignupSlotMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SignupSlotMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SignupID")]
        [InverseProperty("SignupSlotMaps")]
        public virtual Signup Signup { get; set; }
        [ForeignKey("SignupSlotTypeID")]
        [InverseProperty("SignupSlotMaps")]
        public virtual SignupSlotType SignupSlotType { get; set; }
        [ForeignKey("SlotMapStatusID")]
        [InverseProperty("SignupSlotMaps")]
        public virtual SlotMapStatus SlotMapStatus { get; set; }
        [InverseProperty("ApprovedSignupSlotMap")]
        public virtual ICollection<MeetingRequest> MeetingRequestApprovedSignupSlotMaps { get; set; }
        [InverseProperty("RequestedSignupSlotMap")]
        public virtual ICollection<MeetingRequest> MeetingRequestRequestedSignupSlotMaps { get; set; }
        [InverseProperty("SignupSlotMap")]
        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        [InverseProperty("SignupSlotMap")]
        public virtual ICollection<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }
    }
}
