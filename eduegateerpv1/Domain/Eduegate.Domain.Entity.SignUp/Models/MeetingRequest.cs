using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.SignUp.Models
{
    [Table("MeetingRequests", Schema = "signup")]
    public partial class MeetingRequest
    {
        [Key]
        public long MeetingRequestIID { get; set; }

        public long? StudentID { get; set; }

        public long? ParentID { get; set; }

        public long? FacultyID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public long? RequestedSignupSlotMapID { get; set; }

        public long? ApprovedSignupSlotMapID { get; set; }

        public byte? MeetingRequestStatusID { get; set; }

        public DateTime? RequestedDate { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string RequesterRemark { get; set; }

        public string FacultyRemark { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual SignupSlotMap ApprovedSignupSlotMap { get; set; }

        public virtual Class Class { get; set; }

        public virtual Employee Faculty { get; set; }

        public virtual MeetingRequestStatus MeetingRequestStatus { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual SignupSlotMap RequestedSignupSlotMap { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }
    }
}
