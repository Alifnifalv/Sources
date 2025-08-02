using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Employees_21112023", Schema = "payroll")]
    public partial class Employees_21112023
    {
        public long EmployeeIID { get; set; }
        [StringLength(50)]
        public string EmployeeAlias { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public int? EmployeeRoleID { get; set; }
        public int? DesignationID { get; set; }
        public long? BranchID { get; set; }
        [StringLength(500)]
        public string EmployeePhoto { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkEmail { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string WorkPhone { get; set; }
        [StringLength(25)]
        public string WorkMobileNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public int? JobTypeID { get; set; }
        public byte? GenderID { get; set; }
        public long? DepartmentID { get; set; }
        public int? MaritalStatusID { get; set; }
        public long? ReportingEmployeeID { get; set; }
        public long? LoginID { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string PersonalMobileNo { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string CivilIDNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CivilIDValidity { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SponsorDetails { get; set; }
        public int? SalaryMethodID { get; set; }
        public long? EmployeeBankID { get; set; }
        public int? AliasID { get; set; }
        public int? PassportStatus { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public int? ResidencyCompanyId { get; set; }
        public int? Status { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        public byte? CastID { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(250)]
        public string PermenentAddress { get; set; }
        [StringLength(250)]
        public string PresentAddress { get; set; }
        public byte? CommunityID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string AdhaarCardNo { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(30)]
        public string TeacherCode { get; set; }
        public byte? LicenseTypeID { get; set; }
        [StringLength(250)]
        public string LicenseNumber { get; set; }
        public byte? CategoryID { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(20)]
        public string OldEmployeeCode { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(50)]
        public string EmergencyContactNo { get; set; }
        public bool? IsOTEligible { get; set; }
        public long? AcademicCalendarID { get; set; }
        public byte? CalendarTypeID { get; set; }
        public long? SignatureContentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastWorkingDate { get; set; }
        public byte? LeavingTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }
        public int? LeaveGroupID { get; set; }
        public int? PassageTypeID { get; set; }
        public int? AccomodationTypeID { get; set; }
        public bool? IsOverrideLeaveGroup { get; set; }
        [StringLength(15)]
        public string CBSEID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InActiveDate { get; set; }
        public int? Grade { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Grades { get; set; }
        [StringLength(20)]
        public string MOIID { get; set; }
        public byte? TotalYearsofExperience { get; set; }
        [StringLength(25)]
        public string LabourCardNo { get; set; }
        [StringLength(25)]
        public string HealthCardNo { get; set; }
    }
}
