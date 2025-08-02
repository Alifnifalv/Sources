using Eduegate.Domain.Entity.Models.HR;
using Eduegate.Domain.Entity.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Employees", Schema = "payroll")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Tickets = new HashSet<Ticket>();
            Tickets1 = new HashSet<Ticket>();
            Tickets2 = new HashSet<Ticket>();
            DocumentFiles = new HashSet<DocumentFile>();
            InventoryVerifications = new HashSet<InventoryVerification>();
            TransactionHeads = new HashSet<TransactionHead>();
            TransactionHeads1 = new HashSet<TransactionHead>();
            JobEntryHeads = new HashSet<JobEntryHead>();
            //Attendences = new HashSet<Attendence>();
            EmployeeRoleMaps = new HashSet<EmployeeRoleMap>();
            //Appointments = new HashSet<Appointment>();
            //ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            //ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            Employees1 = new HashSet<Employee>();
            //EmployeeSalaries = new HashSet<EmployeeSalary>();
            //LeaveApplicationApprovers = new HashSet<LeaveApplicationApprover>();
            //LeaveApplications = new HashSet<LeaveApplication>();
            //LeaveBlockListApprovers = new HashSet<LeaveBlockListApprover>();
            //LibraryStaffRegisters = new HashSet<LibraryStaffRegister>();
            //LibraryTransactions = new HashSet<LibraryTransaction>();
            //SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
            //ServiceEmployeeMaps = new HashSet<ServiceEmployeeMap>();
            //StaffAttendences = new HashSet<StaffAttendence>();
            //SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            //SubjectTopics = new HashSet<SubjectTopic>();
            //TeacherActivities = new HashSet<TeacherActivity>();
            //TimeTableAllocations = new HashSet<TimeTableAllocation>();
            WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
            WorkflowRuleApprovers = new HashSet<WorkflowRuleApprover>();
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
            EmployeeAdditionalInfos = new HashSet<EmployeeAdditionalInfo>();
            PassportVisaDetails = new HashSet<PassportVisaDetail>();
            EmployeeBankDetails = new HashSet<EmployeeBankDetail>();
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            EmployeeLeaveAllocations = new HashSet<EmployeeLeaveAllocation>();
            TransactionHeadApprovedByNavigations = new HashSet<TransactionHead>();
            EmployeeQualificationMaps = new HashSet<EmployeeQualificationMap>();
            EmployeeExperienceDetails = new HashSet<EmployeeExperienceDetail>();
            TenderAuthentications = new HashSet<TenderAuthentication>();
            EmployeeRelationsDetails = new HashSet<EmployeeRelationsDetail>();
            //TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
            JobInterviews = new HashSet<JobInterview>();
            EmployeeJobDescriptionEmployees = new HashSet<EmployeeJobDescription>();
            EmployeeJobDescriptionReportingToEmployees = new HashSet<EmployeeJobDescription>();
            JobDescriptions = new HashSet<JobDescription>();
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
        public string WorkEmail { get; set; }

        [StringLength(20)]
        public string WorkPhone { get; set; }

        [StringLength(20)]
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

        public byte? LicenseTypeID { get; set; }

        [StringLength(250)]
        public string LicenseNumber { get; set; }
        public byte? CategoryID { get; set; }

        public bool? IsActive { get; set; }
        public int? BloodGroupID { get; set; }
        public string EmergencyContactNo { get; set; }

        public bool? IsOTEligible { get; set; }
        public bool? IsLeaveSalaryEligible { get; set; }
        public bool? IsEoSBEligible { get; set; }

        public long? AcademicCalendarID { get; set; }

        public byte? CalendarTypeID { get; set; }

        public long? SignatureContentID { get; set; }

        public DateTime? LastWorkingDate { get; set; }

        public byte? LeavingTypeID { get; set; }

        public DateTime? ConfirmationDate { get; set; }


        public DateTime? ResignationDate { get; set; }

        public int? Grade { get; set; }

        public int? PassageTypeID { get; set; }

        public int? AccomodationTypeID { get; set; }

        public int? LeaveGroupID { get; set; }

        public string CBSEID { get; set; }

        public byte? TotalYearsofExperience { get; set; }

        public DateTime? InActiveDate { get; set; }

        public string MOIID { get; set; }
        public string LabourCardNo { get; set; }
        public string HealthCardNo { get; set; }
        public string Grades { get; set; }
        public byte? StatusID { get; set; }

        public bool? EnableCommunication { get; set; }
        public long? JobInterviewMapID { get; set; }

        public virtual Login Login { get; set; }

        public virtual LicenseType LicenseType { get; set; }

        public virtual Catogory Catogory { get; set; }

        public virtual AcadamicCalendar AcadamicCalendar { get; set; }

        public virtual CalendarType CalendarType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeQualificationMap> EmployeeQualificationMaps { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual Company Company { get; set; }

        public virtual Department1 Department { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Cast Cast { get; set; }

        public virtual Community Community { get; set; }

        public virtual Relegion Relegion { get; set; }

        public virtual Nationality Nationality { get; set; }

        //public virtual Country Country { get; set; }

        //public virtual MaritalStatuses1 MaritalStatuses1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Attendence> Attendences { get; set; }

        public virtual Designation Designation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Appointment> Appointments { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual JobType JobType { get; set; }

        public virtual SalaryMethod SalaryMethod { get; set; }

        public virtual PassageType PassageType { get; set; }

        public virtual AccomodationType AccomodationType { get; set; }

        public virtual LeaveGroup LeaveGroup { get; set; }

        public bool? IsOverrideLeaveGroup { get; set; }

        public DateTime? TicketEligibleFromDate { get; set; }
        public long? EmployeeCountryAirportID { get; set; }
        public bool? ISTicketEligible { get; set; }
        public long? EmployeeNearestAirportID { get; set; }
        public int? TicketEntitilementID { get; set; }
        public string GenerateTravelSector { get; set; }
        public DateTime? LastTicketGivenDate { get; set; }
        public bool? IsTwoWay { get; set; }
        public short? FlightClassID { get; set; }
        public virtual FlightClass FlightClass { get; set; }
        public virtual Airport EmployeeNearestAirport { get; set; }
        public virtual Airport EmployeeCountryAirport { get; set; }
        public virtual TicketEntitilement TicketEntitilement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeBankDetail> EmployeeBankDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeExperienceDetail> EmployeeExperienceDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryStaffRegister> LibraryStaffRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier> Suppliers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeAdditionalInfo> EmployeeAdditionalInfos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }

        public virtual ICollection<TransactionHead> TransactionHeadApprovedByNavigations { get; set; }
        public virtual ICollection<TenderAuthentication> TenderAuthentications { get; set; }
        public virtual ICollection<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }
        public virtual ICollection<JobInterview> JobInterviews { get; set; }
        public virtual ICollection<EmployeeJobDescription> EmployeeJobDescriptionEmployees { get; set; }
        public virtual ICollection<EmployeeJobDescription> EmployeeJobDescriptionReportingToEmployees { get; set; }
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}