using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentLeaveApplications", Schema = "schools")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_StudentLeaveApplications_SchoolID__AcademicYearID_")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_StudentLeaveApplications_SchoolID__AcademicYearID_ClassID__StudentID")]
    [Index("StudentID", "FromDate", "ToDate", Name = "IDX_StudentLeaveApplications_StudentIDFromDate__ToDate_")]
    [Index("StudentID", Name = "IDX_StudentLeaveApplications_StudentID_")]
    public partial class StudentLeaveApplication
    {
        [Key]
        public long StudentLeaveApplicationIID { get; set; }
        public int? ClassID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public byte? FromSessionID { get; set; }
        public byte? ToSessionID { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public byte? LeaveStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentLeaveApplications")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentLeaveApplications")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentLeaveApplications")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentLeaveApplications")]
        public virtual Student Student { get; set; }
    }
}
