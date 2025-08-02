using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{

    [Table("Employees", Schema = "payroll")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            //Tickets = new HashSet<Ticket>();
            //Tickets1 = new HashSet<Ticket>();
            //Tickets2 = new HashSet<Ticket>();
            //DocumentFiles = new HashSet<DocumentFile>();
            //InventoryVerifications = new HashSet<InventoryVerification>();
            //TransactionHeads = new HashSet<TransactionHead>();
            //JobEntryHeads = new HashSet<JobEntryHead>();
            //AssignVehicleAttendantMaps = new HashSet<AssignVehicleAttendantMap>();
            //AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            //Suppliers = new HashSet<Supplier>();
            //Attendences = new HashSet<Attendence>();
            EmployeeAdditionalInfos = new HashSet<EmployeeAdditionalInfo>();
            EmployeeBankDetails = new HashSet<EmployeeBankDetail>();
            EmployeeRoleMaps = new HashSet<EmployeeRoleMap>();
            //Appointments = new HashSet<Appointment>();
            //ClassClassTeacherMaps = new HashSet<ClassClassTeacherMap>();
            //ClassGroups = new HashSet<ClassGroup>();
            //ClassGroupTeacherMaps = new HashSet<ClassGroupTeacherMap>();
            //ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            //ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            //ClassTeacherMaps1 = new HashSet<ClassTeacherMap>();
            //ClassTeacherMaps2 = new HashSet<ClassTeacherMap>();
            Employees1 = new HashSet<Employee>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
            LeaveApplicationApprovers = new HashSet<LeaveApplicationApprover>();
            LeaveApplications = new HashSet<LeaveApplication>();
            LeaveBlockListApprovers = new HashSet<LeaveBlockListApprover>();

            LoanHeads = new HashSet<LoanHead>();
            LoanRequests = new HashSet<LoanRequest>();
            //FeeCollections = new HashSet<FeeCollection>();
            //LibraryStaffRegisters = new HashSet<LibraryStaffRegister>();
            //LibraryTransactions = new HashSet<LibraryTransaction>();
            //PassportVisaDetails = new HashSet<PassportVisaDetail>();
            //RemarksEntries = new HashSet<RemarksEntry>();
            SalarySlips = new HashSet<SalarySlip>();
            //SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
            //ServiceEmployeeMaps = new HashSet<ServiceEmployeeMap>();
            //StaffAttendences = new HashSet<StaffAttendence>();
            //StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            //StudentAttendences = new HashSet<StudentAttendence>();
            //StudentMiscDetails = new HashSet<StudentMiscDetail>();
            //SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            //SubjectTopics = new HashSet<SubjectTopic>();
            //TeacherActivities = new HashSet<TeacherActivity>();
            //TimeTableAllocations = new HashSet<TimeTableAllocation>();
            //TimeTableLogs = new HashSet<TimeTableLog>();
            //WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
            //WorkflowRuleApprovers = new HashSet<WorkflowRuleApprover>();
            //WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
            EmployeeTimeSheetApprovals = new HashSet<EmployeeTimeSheetApproval>();
            EmployeeLeaveAllocations = new HashSet<EmployeeLeaveAllocation>();
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
        }

        [Key]
        public long EmployeeIID { get; set; }

        [StringLength(50)]
        public string EmployeeAlias { get; set; }

        [StringLength(100)]
        public string EmployeeName { get; set; }

        public int? EmployeeRoleID { get; set; }

        public int? DesignationID { get; set; }

        public int? LeaveGroupID { get; set; }

        public long? BranchID { get; set; }

        [StringLength(500)]
        public string EmployeePhoto { get; set; }

        [StringLength(50)]
        public string WorkEmail { get; set; }

        [StringLength(20)]
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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(20)]
        public string PersonalMobileNo { get; set; }

        [StringLength(20)]
        public string CivilIDNumber { get; set; }

        public DateTime? CivilIDValidity { get; set; }

        [StringLength(50)]
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
        public string AdhaarCardNo { get; set; }

        public int? NationalityID { get; set; }

        [StringLength(30)]
        public string TeacherCode { get; set; }

        public byte? LicenseTypeID { get; set; }

        [StringLength(250)]
        public string LicenseNumber { get; set; }

        public byte? CategoryID { get; set; }

        public bool? IsActive { get; set; }

        public long? AcademicCalendarID { get; set; }

       

        public int? PassageTypeID { get; set; }

        public int? AccomodationTypeID { get; set; }

        public bool? IsOverrideLeaveGroup { get; set; }

        public byte? CalendarTypeID { get; set; }

        public virtual Login Login { get; set; }

        public virtual LeaveGroup LeaveGroup { get; set; }

        public virtual PassageType PassageType { get; set; }

        public virtual AccomodationType AccomodationType { get; set; }

        public virtual ICollection<LoanHead> LoanHeads { get; set; }
    
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Tickets { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Tickets1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Tickets2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DocumentFile> DocumentFiles { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AssignVehicleAttendantMap> AssignVehicleAttendantMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }

        public virtual Branch Branch { get; set; }

        //public virtual Catogory Catogory { get; set; }

        public virtual Company Company { get; set; }

        public virtual Departments1 Departments1 { get; set; }

        public virtual Gender Gender { get; set; }

        //public virtual MaritalStatuses1 MaritalStatuses1 { get; set; }

        public virtual Nationality Nationality { get; set; }

        public virtual Relegion Relegion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Supplier> Suppliers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Attendence> Attendences { get; set; }

        public virtual Designation Designation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeAdditionalInfo> EmployeeAdditionalInfos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeBankDetail> EmployeeBankDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Appointment> Appointments { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassGroup> ClassGroups { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassGroupTeacherMap> ClassGroupTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTeacherMap> ClassTeacherMaps1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTeacherMap> ClassTeacherMaps2 { get; set; }

        //public virtual LicenseType LicenseType { get; set; }

        public virtual Cast Cast { get; set; }

        public virtual Community Community { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual JobType JobType { get; set; }

        public virtual SalaryMethod SalaryMethod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeCollection> FeeCollections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryStaffRegister> LibraryStaffRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentMiscDetail> StudentMiscDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }

        public virtual CalendarType CalendarType { get; set; }

        public virtual AcadamicCalendar AcademicCalendar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
    }
}