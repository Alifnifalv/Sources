using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeesView
    {
        public long EmployeeIID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        public int? JobTypeID { get; set; }
        [StringLength(50)]
        public string JobTypeName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string EmployeeAlias { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(50)]
        public string GenderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(250)]
        public string PresentAddress { get; set; }
        [StringLength(250)]
        public string PermenentAddress { get; set; }
        public int? Age { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }
        [StringLength(50)]
        public string EmergencyContactNo { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(50)]
        public string NationalityName { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string AdhaarCardNo { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(50)]
        public string RelegionName { get; set; }
        public byte? CastID { get; set; }
        [StringLength(50)]
        public string CastDescription { get; set; }
        public byte? CommunityID { get; set; }
        [StringLength(50)]
        public string CommunityDescription { get; set; }
        public int? MaritalStatusID { get; set; }
        [StringLength(50)]
        public string MaritalStatus { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string WorkPhone { get; set; }
        [StringLength(25)]
        public string WorkMobileNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkEmail { get; set; }
        [StringLength(4000)]
        public string Roles { get; set; }
        public long? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? DesignationID { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        public byte? LicenseTypeID { get; set; }
        [StringLength(250)]
        public string LicenseTypeName { get; set; }
        [StringLength(250)]
        public string LicenseNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
        public long? DepartmentID { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public long? ReportingEmployeeID { get; set; }
        [StringLength(502)]
        public string ReportingEmployee { get; set; }
        [StringLength(20)]
        public string PassportNo { get; set; }
        [StringLength(20)]
        public string PassportIssueCoutryCode { get; set; }
        [StringLength(50)]
        public string PassportIssueCountry { get; set; }
        [StringLength(100)]
        public string PassportIssuePlace { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoExpiry { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDNoExpiry { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaExpiry { get; set; }
        [StringLength(250)]
        public string SponceredBy { get; set; }
        [StringLength(500)]
        public string SponsorName { get; set; }
        public byte? CalendarTypeID { get; set; }
        [StringLength(50)]
        public string CalendarType { get; set; }
        public long? AcademicCalendarID { get; set; }
        [StringLength(50)]
        public string CalenderName { get; set; }
        [Required]
        [StringLength(25)]
        [Unicode(false)]
        public string IsOTEligible { get; set; }
        public long? BankID { get; set; }
        [StringLength(10)]
        public string BankCode { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string BankShortName { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }
        [StringLength(20)]
        public string AccountNo { get; set; }
        [StringLength(50)]
        public string IBAN { get; set; }
        [StringLength(20)]
        public string BankSwiftCode { get; set; }
        public long? LoginID { get; set; }
        [StringLength(100)]
        public string LoginUserID { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActiveLogin { get; set; }
        [StringLength(1000)]
        public string HighestAcademicQualitication { get; set; }
        [StringLength(1000)]
        public string HighestPrefessionalQualitication { get; set; }
        [StringLength(1000)]
        public string ClassessTaught { get; set; }
        [StringLength(1000)]
        public string AppointedSubject { get; set; }
        [StringLength(1000)]
        public string MainSubjectTought { get; set; }
        [StringLength(1000)]
        public string AdditioanalSubjectTought { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string IsComputerTrained { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        public int? ResidencyCompanyId { get; set; }
        [StringLength(100)]
        public string ResidenceCompany { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
