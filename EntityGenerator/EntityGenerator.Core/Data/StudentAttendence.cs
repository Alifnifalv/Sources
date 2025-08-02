using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentAttendences", Schema = "schools")]
    [Index("StudentID", "PresentStatusID", Name = "IDX_StudentAttendences_AttendenceDate")]
    [Index("SchoolID", "AttendenceDate", Name = "IDX_StudentAttendences_SchoolIDAttendenceDate_StudentID")]
    [Index("PresentStatusID", Name = "IDX_StudentAttendences_StudentID_AttendenceDate")]
    [Index("StudentID", "AcademicYearID", Name = "IDX_StudentAttendences_StudentID__AcademicYearID_AttendenceDate__PresentStatusID__StartTime__EndTim")]
    [Index("StudentID", Name = "IDX_studentID")]
    [Index("ClassID", "SectionID", Name = "StudentAttendences_ClassID_SectionID")]
    [Index("AttendenceDate", Name = "idx_StudentAttendencesAttendenceDate")]
    [Index("PresentStatusID", "AcademicYearID", Name = "idx_StudentAttendencesPresentStatusIDAcademicYearID")]
    [Index("SchoolID", "AcademicYearID", Name = "idx_StudentAttendencesSchoolIDAcademicYearID")]
    public partial class StudentAttendence
    {
        [Key]
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

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentAttendences")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AttendenceReasonID")]
        [InverseProperty("StudentAttendences")]
        public virtual AttendenceReason AttendenceReason { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentAttendences")]
        public virtual Class Class { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("StudentAttendences")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("PresentStatusID")]
        [InverseProperty("StudentAttendences")]
        public virtual PresentStatus PresentStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentAttendences")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentAttendences")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentAttendences")]
        public virtual Student Student { get; set; }
    }
}
