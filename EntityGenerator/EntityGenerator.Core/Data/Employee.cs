using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
            Appointments = new HashSet<Appointment>();
            AssignVehicleAttendantMaps = new HashSet<AssignVehicleAttendantMap>();
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            Attendences = new HashSet<Attendence>();
            CampaignEmployeeMaps = new HashSet<CampaignEmployeeMap>();
            ClassAssociateTeacherMaps = new HashSet<ClassAssociateTeacherMap>();
            ClassClassTeacherMapCoordinators = new HashSet<ClassClassTeacherMap>();
            ClassClassTeacherMapTeachers = new HashSet<ClassClassTeacherMap>();
            ClassCoordinatorCoordinators = new HashSet<ClassCoordinator>();
            ClassCoordinatorHeadMasters = new HashSet<ClassCoordinator>();
            ClassCoordinatorMaps = new HashSet<ClassCoordinatorMap>();
            ClassGroupTeacherMaps = new HashSet<ClassGroupTeacherMap>();
            ClassGroups = new HashSet<ClassGroup>();
            ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            ClassTeacherMapClassTeachers = new HashSet<ClassTeacherMap>();
            ClassTeacherMapEmployees = new HashSet<ClassTeacherMap>();
            ClassTeacherMapTeachers = new HashSet<ClassTeacherMap>();
            DocumentFiles = new HashSet<DocumentFile>();
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            EmployeeAdditionalInfoes = new HashSet<EmployeeAdditionalInfo>();
            EmployeeBankDetails = new HashSet<EmployeeBankDetail>();
            EmployeeESBProvisionDetails = new HashSet<EmployeeESBProvisionDetail>();
            EmployeeESBProvisions = new HashSet<EmployeeESBProvision>();
            EmployeeExperienceDetails = new HashSet<EmployeeExperienceDetail>();
            EmployeeJobDescriptionEmployees = new HashSet<EmployeeJobDescription>();
            EmployeeJobDescriptionReportingToEmployees = new HashSet<EmployeeJobDescription>();
            EmployeeLSProvisionDetails = new HashSet<EmployeeLSProvisionDetail>();
            EmployeeLSProvisions = new HashSet<EmployeeLSProvision>();
            EmployeeLeaveAllocations = new HashSet<EmployeeLeaveAllocation>();
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeQualificationMaps = new HashSet<EmployeeQualificationMap>();
            EmployeeRelationsDetails = new HashSet<EmployeeRelationsDetail>();
            EmployeeRoleMaps = new HashSet<EmployeeRoleMap>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            EmployeeTimeSheetApprovals = new HashSet<EmployeeTimeSheetApproval>();
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
            EventTransportAllocationAttendars = new HashSet<EventTransportAllocation>();
            EventTransportAllocationDrivers = new HashSet<EventTransportAllocation>();
            FeeCollections = new HashSet<FeeCollection>();
            HealthEntries = new HashSet<HealthEntry>();
            InventoryVerifications = new HashSet<InventoryVerification>();
            InverseReportingEmployee = new HashSet<Employee>();
            JobDescriptions = new HashSet<JobDescription>();
            JobEntryHeads = new HashSet<JobEntryHead>();
            JobInterviews = new HashSet<JobInterview>();
            LeaveApplicationApprovers = new HashSet<LeaveApplicationApprover>();
            LeaveApplications = new HashSet<LeaveApplication>();
            LeaveBlockListApprovers = new HashSet<LeaveBlockListApprover>();
            LibraryStaffRegisters = new HashSet<LibraryStaffRegister>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
            LoanHeads = new HashSet<LoanHead>();
            LoanRequests = new HashSet<LoanRequest>();
            MeetingRequests = new HashSet<MeetingRequest>();
            PassportVisaDetails = new HashSet<PassportVisaDetail>();
            RemarksEntries = new HashSet<RemarksEntry>();
            SalarySlips = new HashSet<SalarySlip>();
            SalesPromotionLogs = new HashSet<SalesPromotionLog>();
            SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
            ServiceEmployeeMaps = new HashSet<ServiceEmployeeMap>();
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            Signups = new HashSet<Signup>();
            StaffAttendences = new HashSet<StaffAttendence>();
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentMiscDetails = new HashSet<StudentMiscDetail>();
            StudentStaffMaps = new HashSet<StudentStaffMap>();
            SubjectInchargerClassMaps = new HashSet<SubjectInchargerClassMap>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            SubjectTopics = new HashSet<SubjectTopic>();
            Suppliers = new HashSet<Supplier>();
            TeacherActivities = new HashSet<TeacherActivity>();
            TenderAuthentications = new HashSet<TenderAuthentication>();
            TicketAssignedEmployees = new HashSet<Ticket>();
            TicketEmployees = new HashSet<Ticket>();
            TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableExtTeachers = new HashSet<TimeTableExtTeacher>();
            TimeTableLogs = new HashSet<TimeTableLog>();
            TimesheetEntryLogs = new HashSet<TimesheetEntryLog>();
            TransactionHeadApprovedByNavigations = new HashSet<TransactionHead>();
            TransactionHeadEmployees = new HashSet<TransactionHead>();
            TransactionHeadStaffs = new HashSet<TransactionHead>();
            VehicleTrackings = new HashSet<VehicleTracking>();
            WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
            WorkflowRuleApprovers = new HashSet<WorkflowRuleApprover>();
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
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
        [StringLength(20)]
        public string MOIID { get; set; }
        public byte? TotalYearsofExperience { get; set; }
        [StringLength(25)]
        public string LabourCardNo { get; set; }
        [StringLength(25)]
        public string HealthCardNo { get; set; }
        [StringLength(50)]
        public string Grades { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResignationDate { get; set; }
        public byte? StatusID { get; set; }
        public bool? EnableCommunication { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TicketEligibleFromDate { get; set; }
        public long? EmployeeCountryAirportID { get; set; }
        public bool? ISTicketEligible { get; set; }
        public long? EmployeeNearestAirportID { get; set; }
        public long? TicketEntitlementID { get; set; }
        [StringLength(50)]
        public string GenerateTravelSector { get; set; }
        public int? TicketEntitilementID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastTicketGivenDate { get; set; }
        public short? FlightClassID { get; set; }
        public bool? IsTwoWay { get; set; }
        public long? JobInterviewMapID { get; set; }
        public bool? IsLeaveSalaryEligible { get; set; }
        public bool? IsEoSBEligible { get; set; }

        [ForeignKey("AcademicCalendarID")]
        [InverseProperty("Employees")]
        public virtual AcadamicCalendar AcademicCalendar { get; set; }
        [ForeignKey("AccomodationTypeID")]
        [InverseProperty("Employees")]
        public virtual AccomodationType AccomodationType { get; set; }
        [ForeignKey("BloodGroupID")]
        [InverseProperty("Employees")]
        public virtual BloodGroup BloodGroup { get; set; }
        [ForeignKey("BranchID")]
        [InverseProperty("Employees")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CalendarTypeID")]
        [InverseProperty("Employees")]
        public virtual CalendarType CalendarType { get; set; }
        [ForeignKey("CastID")]
        [InverseProperty("Employees")]
        public virtual Cast Cast { get; set; }
        [ForeignKey("CategoryID")]
        [InverseProperty("Employees")]
        public virtual Catogory Category { get; set; }
        [ForeignKey("CommunityID")]
        [InverseProperty("Employees")]
        public virtual Community Community { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("Employees")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("DesignationID")]
        [InverseProperty("Employees")]
        public virtual Designation Designation { get; set; }
        [ForeignKey("EmployeeCountryAirportID")]
        [InverseProperty("EmployeeEmployeeCountryAirports")]
        public virtual Airport EmployeeCountryAirport { get; set; }
        [ForeignKey("EmployeeNearestAirportID")]
        [InverseProperty("EmployeeEmployeeNearestAirports")]
        public virtual Airport EmployeeNearestAirport { get; set; }
        [ForeignKey("EmployeeRoleID")]
        [InverseProperty("Employees")]
        public virtual EmployeeRole EmployeeRole { get; set; }
        [ForeignKey("FlightClassID")]
        [InverseProperty("Employees")]
        public virtual FlightClass FlightClass { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("Employees")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("JobTypeID")]
        [InverseProperty("Employees")]
        public virtual JobType JobType { get; set; }
        [ForeignKey("LeaveGroupID")]
        [InverseProperty("Employees")]
        public virtual LeaveGroup LeaveGroup { get; set; }
        [ForeignKey("LeavingTypeID")]
        [InverseProperty("Employees")]
        public virtual EmployeeLeavingType LeavingType { get; set; }
        [ForeignKey("LicenseTypeID")]
        [InverseProperty("Employees")]
        public virtual LicenseType LicenseType { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("Employees")]
        public virtual Login Login { get; set; }
        [ForeignKey("MaritalStatusID")]
        [InverseProperty("Employees")]
        public virtual MaritalStatus1 MaritalStatus { get; set; }
        [ForeignKey("NationalityID")]
        [InverseProperty("Employees")]
        public virtual Nationality Nationality { get; set; }
        [ForeignKey("PassageTypeID")]
        [InverseProperty("Employees")]
        public virtual PassageType PassageType { get; set; }
        [ForeignKey("RelegionID")]
        [InverseProperty("Employees")]
        public virtual Relegion Relegion { get; set; }
        [ForeignKey("ReportingEmployeeID")]
        [InverseProperty("InverseReportingEmployee")]
        public virtual Employee ReportingEmployee { get; set; }
        [ForeignKey("ResidencyCompanyId")]
        [InverseProperty("Employees")]
        public virtual Company ResidencyCompany { get; set; }
        [ForeignKey("SalaryMethodID")]
        [InverseProperty("Employees")]
        public virtual SalaryMethod SalaryMethod { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Employees")]
        public virtual EmployeeStatu StatusNavigation { get; set; }
        [ForeignKey("TicketEntitilementID")]
        [InverseProperty("Employees")]
        public virtual TicketEntitilement TicketEntitilement { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<AssignVehicleAttendantMap> AssignVehicleAttendantMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Attendence> Attendences { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<CampaignEmployeeMap> CampaignEmployeeMaps { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<ClassAssociateTeacherMap> ClassAssociateTeacherMaps { get; set; }
        [InverseProperty("Coordinator")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMapCoordinators { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMapTeachers { get; set; }
        [InverseProperty("Coordinator")]
        public virtual ICollection<ClassCoordinator> ClassCoordinatorCoordinators { get; set; }
        [InverseProperty("HeadMaster")]
        public virtual ICollection<ClassCoordinator> ClassCoordinatorHeadMasters { get; set; }
        [InverseProperty("Coordinator")]
        public virtual ICollection<ClassCoordinatorMap> ClassCoordinatorMaps { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<ClassGroupTeacherMap> ClassGroupTeacherMaps { get; set; }
        [InverseProperty("HeadTeacher")]
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }
        [InverseProperty("ClassTeacher")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMapClassTeachers { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMapEmployees { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMapTeachers { get; set; }
        [InverseProperty("OwnerEmployee")]
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeAdditionalInfo> EmployeeAdditionalInfoes { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeESBProvisionDetail> EmployeeESBProvisionDetails { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeESBProvision> EmployeeESBProvisions { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeExperienceDetail> EmployeeExperienceDetails { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeJobDescription> EmployeeJobDescriptionEmployees { get; set; }
        [InverseProperty("ReportingToEmployee")]
        public virtual ICollection<EmployeeJobDescription> EmployeeJobDescriptionReportingToEmployees { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeLSProvisionDetail> EmployeeLSProvisionDetails { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeLSProvision> EmployeeLSProvisions { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeQualificationMap> EmployeeQualificationMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
        [InverseProperty("Attendar")]
        public virtual ICollection<EventTransportAllocation> EventTransportAllocationAttendars { get; set; }
        [InverseProperty("Driver")]
        public virtual ICollection<EventTransportAllocation> EventTransportAllocationDrivers { get; set; }
        [InverseProperty("Cashier")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<HealthEntry> HealthEntries { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        [InverseProperty("ReportingEmployee")]
        public virtual ICollection<Employee> InverseReportingEmployee { get; set; }
        [InverseProperty("ReportingToEmployee")]
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        [InverseProperty("Interviewer")]
        public virtual ICollection<JobInterview> JobInterviews { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LibraryStaffRegister> LibraryStaffRegisters { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LoanHead> LoanHeads { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
        [InverseProperty("Faculty")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
        [InverseProperty("Reference")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SalesPromotionLog> SalesPromotionLogs { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        [InverseProperty("OrganizerEmployee")]
        public virtual ICollection<Signup> Signups { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<StudentMiscDetail> StudentMiscDetails { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<StudentStaffMap> StudentStaffMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SubjectInchargerClassMap> SubjectInchargerClassMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<TenderAuthentication> TenderAuthentications { get; set; }
        [InverseProperty("AssignedEmployee")]
        public virtual ICollection<Ticket> TicketAssignedEmployees { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Ticket> TicketEmployees { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<TimeTableExtTeacher> TimeTableExtTeachers { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<TimesheetEntryLog> TimesheetEntryLogs { get; set; }
        [InverseProperty("ApprovedByNavigation")]
        public virtual ICollection<TransactionHead> TransactionHeadApprovedByNavigations { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<TransactionHead> TransactionHeadEmployees { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<TransactionHead> TransactionHeadStaffs { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<VehicleTracking> VehicleTrackings { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}
