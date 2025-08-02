namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentAttendences", Schema = "schools")]
    public partial class StudentAttendence
    {
        [Key]
        public long StudentAttendenceIID { get; set; }

        public long? StudentID { get; set; }

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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AttendenceReason AttendenceReason { get; set; }

        public virtual Class Class { get; set; }

        public virtual PresentStatus PresentStatus { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }
    }
}
