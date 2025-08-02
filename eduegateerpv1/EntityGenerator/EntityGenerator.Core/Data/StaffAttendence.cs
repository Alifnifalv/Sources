using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StaffAttendences", Schema = "schools")]
    [Index("SchoolID", "AttendenceDate", Name = "IDX_StaffAttendences_SchoolIDAttendenceDate_PresentStatusID")]
    [Index("EmployeeID", Name = "idx_StaffAttendencesEmployeeID")]
    public partial class StaffAttendence
    {
        [Key]
        public long StaffAttendenceIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendenceDate { get; set; }
        public long? EmployeeID { get; set; }
        public byte? PresentStatusID { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? AttendenceReasonID { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StaffAttendences")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AttendenceReasonID")]
        [InverseProperty("StaffAttendences")]
        public virtual AttendenceReason AttendenceReason { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("StaffAttendences")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("PresentStatusID")]
        [InverseProperty("StaffAttendences")]
        public virtual StaffPresentStatus PresentStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StaffAttendences")]
        public virtual School School { get; set; }
    }
}
