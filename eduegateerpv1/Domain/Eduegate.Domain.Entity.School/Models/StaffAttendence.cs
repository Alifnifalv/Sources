using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StaffAttendences", Schema = "schools")]
    public partial class StaffAttendence
    {
        [Key]
        public long StaffAttendenceIID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AttendenceDate { get; set; }

        public long? EmployeeID { get; set; }

        public byte? PresentStatusID { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public int? AttendenceReasonID { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AttendenceReason AttendenceReason { get; set; }

        public virtual Schools School { get; set; }

        public virtual StaffPresentStatus StaffPresentStatus { get; set; }
    }
}