using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MeetingRequestSearchView
    {
        public long MeetingRequestIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string StudentName { get; set; }
        public long? ParentID { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        [StringLength(302)]
        public string MotherName { get; set; }
        public long? FacultyID { get; set; }
        [StringLength(555)]
        public string FacultyName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string School { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public long? RequestedSignupSlotMapID { get; set; }
        [StringLength(33)]
        [Unicode(false)]
        public string RequestedTime { get; set; }
        public long? ApprovedSignupSlotMapID { get; set; }
        [StringLength(33)]
        [Unicode(false)]
        public string ApprovedTime { get; set; }
        public byte? MeetingRequestStatusID { get; set; }
        [StringLength(100)]
        public string RequestStatusName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovedDate { get; set; }
        public string RequesterRemark { get; set; }
        public string FacultyRemark { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
