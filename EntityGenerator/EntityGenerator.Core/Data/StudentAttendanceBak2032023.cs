using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StudentAttendanceBak2032023", Schema = "schools")]
    public partial class StudentAttendanceBak2032023
    {
        public long StudentAttendenceIID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendenceDate { get; set; }
        public byte? PresentStatusID { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? AttendenceReasonID { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? EmployeeID { get; set; }
    }
}
