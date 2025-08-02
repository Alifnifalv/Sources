using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StudentLeaveApplications", Schema = "schools")]
    public partial class StudentLeaveApplication
    {
        [Key]
        public long StudentLeaveApplicationIID { get; set; }

        public int? ClassID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public byte? FromSessionID { get; set; }

        public byte? ToSessionID { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        public byte? LeaveStatusID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public string Remarks { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        public virtual LeaveStatus LeaveStatus { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Class Class { get; set; }

        public virtual Student Student { get; set; }
    }
}