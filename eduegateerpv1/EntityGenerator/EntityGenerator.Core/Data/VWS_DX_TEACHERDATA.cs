using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_TEACHERDATA
    {
        public long EmployeeIID { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public int? EmployeeRoleID { get; set; }
        public int? DesignationID { get; set; }
        public long? BranchID { get; set; }
        public int? JobTypeID { get; set; }
        public byte? GenderID { get; set; }
        public long? DepartmentID { get; set; }
        public int? MaritalStatusID { get; set; }
        public long? ReportingEmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string PermenentAddress { get; set; }
        [StringLength(250)]
        public string PresentAddress { get; set; }
        public byte? CastID { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(30)]
        public string TeacherCode { get; set; }
        public byte? CategoryID { get; set; }
        public long? AcademicCalendarID { get; set; }
        public byte? CalendarTypeID { get; set; }
        [StringLength(50)]
        public string Grades { get; set; }
        public int? AccomodationTypeID { get; set; }
        public byte SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(50)]
        public string SchoolCode { get; set; }
        [StringLength(100)]
        public string Place { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [StringLength(50)]
        public string DesignationCode { get; set; }
        public bool? IsTransportNotification { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [StringLength(4)]
        public string DepartmentNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Gender { get; set; }
        [Required]
        [StringLength(50)]
        public string MaritalStatus { get; set; }
        [StringLength(50)]
        public string CalenderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
    }
}
