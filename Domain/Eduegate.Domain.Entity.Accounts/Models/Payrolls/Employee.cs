using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Jobs;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Payrolls
{
    [Table("Employees", Schema = "payroll")]
    [Index("AcademicCalendarID", Name = "IDX_Employees_AcademicCalendarID_")]
    [Index("BranchID", Name = "IDX_Employees_BranchID_EmployeeCode__FirstName__MiddleName__LastName")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_DepartmentID")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_DesignationID")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_DesignationID__EmployeePhoto__DateOfJoining__EmployeeCode__FirstNa")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_EmployeeCode__FirstName__MiddleName__LastName")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_GenderID")]
    [Index("BranchID", "IsActive", Name = "IDX_Employees_BranchID__IsActive_JobTypeID")]
    [Index("BranchID", "JobTypeID", "IsActive", Name = "IDX_Employees_BranchID__JobTypeID__IsActive_")]
    [Index("DepartmentID", Name = "IDX_Employees_DepartmentID_")]
    [Index("DesignationID", Name = "IDX_Employees_DesignationID_")]
    [Index("DesignationID", Name = "IDX_Employees_DesignationID_DepartmentID")]
    [Index("DesignationID", Name = "IDX_Employees_DesignationID_EmployeeAlias__BranchID__WorkEmail__WorkPhone__WorkMobileNo__DateOfJoin")]
    [Index("DesignationID", "BranchID", "IsActive", Name = "IDX_Employees_DesignationID__BranchID__IsActive_")]
    [Index("DesignationID", "IsActive", Name = "IDX_Employees_DesignationID__IsActive_BranchID")]
    [Index("DesignationID", "IsActive", Name = "IDX_Employees_DesignationID__IsActive_BranchID__EmployeePhoto__DateOfJoining__EmployeeCode__FirstNa")]
    [Index("EmployeeCode", Name = "IDX_Employees_EmployeeCode_")]
    [Index("IsActive", "DesignationID", Name = "IDX_Employees_IsActiveDesignationID_EmployeeCode__FirstName__MiddleName__LastName")]
    [Index("IsActive", Name = "IDX_Employees_IsActive_")]
    [Index("IsActive", Name = "IDX_Employees_IsActive_DesignationID__BranchID")]
    [Index("IsActive", Name = "IDX_Employees_IsActive_DesignationID__BranchID__EmployeePhoto__DateOfJoining__EmployeeCode__FirstNa")]
    [Index("IsActive", Name = "IDX_Employees_IsActive_DesignationID__BranchID__WorkEmail__WorkMobileNo__DateOfJoining__DateOfBirth")]
    [Index("IsActive", Name = "IDX_Employees_IsActive_DesignationID__EmployeeCode__FirstName__MiddleName__LastName")]
    [Index("IsActive", Name = "IDX_Employees_IsActive_EmployeeCode__FirstName__MiddleName__LastName")]
    [Index("LoginID", Name = "idx_EmployeesLoginID")]
    public partial class Employee
    {
        public Employee()
        {
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            InverseReportingEmployee = new HashSet<Employee>();
            JobEntryHeads = new HashSet<JobEntryHead>();
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
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

        public DateTime? DateOfJoining { get; set; }

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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string PersonalMobileNo { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string CivilIDNumber { get; set; }

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

        public DateTime? LastWorkingDate { get; set; }

        public byte? LeavingTypeID { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public int? LeaveGroupID { get; set; }

        public int? PassageTypeID { get; set; }

        public int? AccomodationTypeID { get; set; }

        public bool? IsOverrideLeaveGroup { get; set; }

        [StringLength(15)]
        public string CBSEID { get; set; }

        public DateTime? InActiveDate { get; set; }

        public int? Grade { get; set; }

        [StringLength(20)]
        public string MOIID { get; set; }

        public byte? TotalYearsofExperience { get; set; }

        [StringLength(25)]
        public string LabourCardNo { get; set; }

        [StringLength(25)]
        public string HealthCardNo { get; set; }

        [StringLength(50)]
        public string Grades { get; set; }

        public DateTime? ResignationDate { get; set; }

        public byte? StatusID { get; set; }

        public bool? EnableCommunication { get; set; }

        public DateTime? TicketEligibleFromDate { get; set; }

        public long? EmployeeCountryAirportID { get; set; }

        public bool? ISTicketEligible { get; set; }

        public long? EmployeeNearestAirportID { get; set; }

        public long? TicketEntitlementID { get; set; }

        [StringLength(50)]
        public string GenerateTravelSector { get; set; }

        public int? TicketEntitilementID { get; set; }

        public DateTime? LastTicketGivenDate { get; set; }

        public short? FlightClassID { get; set; }

        public bool? IsTwoWay { get; set; }

        public long? JobInterviewMapID { get; set; }

        public bool? IsLeaveSalaryEligible { get; set; }

        public bool? IsEoSBEligible { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Department1 Department { get; set; }

        public virtual LeaveGroup LeaveGroup { get; set; }

        public virtual Employee ReportingEmployee { get; set; }

        public virtual Company ResidencyCompany { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        public virtual ICollection<Employee> InverseReportingEmployee { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
