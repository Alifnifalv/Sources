using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentLeaveApplicationSearch
    {
        public long StudentLeaveApplicationIID { get; set; }
        [Unicode(false)]
        public string LeaveApplicationNo { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string AppliedDate { get; set; }
        public long? StudentID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FromDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ToDate { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public byte? FromSessionID { get; set; }
        public byte? ToSessionID { get; set; }
        public byte? LeaveStatusID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [Column("class")]
        [StringLength(50)]
        public string _class { get; set; }
        [StringLength(50)]
        public string LeaveStatus { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
