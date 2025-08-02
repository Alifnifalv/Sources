using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? RequestedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string RequesterRemark { get; set; }
        public string FacultyRemark { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("MeetingRequests")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ApprovedSignupSlotMapID")]
        [InverseProperty("MeetingRequestApprovedSignupSlotMaps")]
        public virtual SignupSlotMap ApprovedSignupSlotMap { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("MeetingRequests")]
        public virtual Class Class { get; set; }
        [ForeignKey("FacultyID")]
        [InverseProperty("MeetingRequests")]
        public virtual Employee Faculty { get; set; }
        [ForeignKey("MeetingRequestStatusID")]
        [InverseProperty("MeetingRequests")]
        public virtual MeetingRequestStatus MeetingRequestStatus { get; set; }
        [ForeignKey("ParentID")]
        [InverseProperty("MeetingRequests")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("RequestedSignupSlotMapID")]
        [InverseProperty("MeetingRequestRequestedSignupSlotMaps")]
        public virtual SignupSlotMap RequestedSignupSlotMap { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("MeetingRequests")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("MeetingRequests")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("MeetingRequests")]
        public virtual Student Student { get; set; }
    }
}
