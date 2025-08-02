using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Schools", Schema = "schools")]
    public partial class School
    {
        public School()
        {
            AcadamicCalendars = new HashSet<AcadamicCalendar>();
            AcademicClassMaps = new HashSet<AcademicClassMap>();
            AcademicSchoolMaps = new HashSet<AcademicSchoolMap>();
            AcademicYears = new HashSet<AcademicYear>();
            AdmissionEnquiries = new HashSet<AdmissionEnquiry>();
            AgeCriterias = new HashSet<AgeCriteria>();
            Agenda = new HashSet<Agenda>();
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            Assignments = new HashSet<Assignment>();
            CampusTransferFromSchools = new HashSet<CampusTransfer>();
            CampusTransferToSchools = new HashSet<CampusTransfer>();
            Circulars = new HashSet<Circular>();
            ClassClassGroupMaps = new HashSet<ClassClassGroupMap>();
            ClassClassTeacherMaps = new HashSet<ClassClassTeacherMap>();
            ClassCoordinatorMaps = new HashSet<ClassCoordinatorMap>();
            ClassCoordinators = new HashSet<ClassCoordinator>();
            ClassFeeMasters = new HashSet<ClassFeeMaster>();
            ClassFeeStructureMaps = new HashSet<ClassFeeStructureMap>();
            ClassGroups = new HashSet<ClassGroup>();
            ClassSectionMaps = new HashSet<ClassSectionMap>();
            ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            ClassTimings = new HashSet<ClassTiming>();
            Classes = new HashSet<Class>();
            CostCenters = new HashSet<CostCenter>();
            CounselorHubs = new HashSet<CounselorHub>();
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            Department1 = new HashSet<Department1>();
            ExamGroups = new HashSet<ExamGroup>();
            ExamTypes = new HashSet<ExamType>();
            Exams = new HashSet<Exam>();
            FeeCollections = new HashSet<FeeCollection>();
            FeeMasters = new HashSet<FeeMaster>();
            FeePeriods = new HashSet<FeePeriod>();
            FeeStructures = new HashSet<FeeStructure>();
            FeeTypes = new HashSet<FeeType>();
            FinalSettlements = new HashSet<FinalSettlement>();
            FineMasterStudentMaps = new HashSet<FineMasterStudentMap>();
            FineMasters = new HashSet<FineMaster>();
            FunctionalPeriods = new HashSet<FunctionalPeriod>();
            Galleries = new HashSet<Gallery>();
            HealthEntries = new HashSet<HealthEntry>();
            Leads = new HashSet<Lead>();
            LessonPlans = new HashSet<LessonPlan>();
            LibraryBooks = new HashSet<LibraryBook>();
            LibraryStaffRegisters = new HashSet<LibraryStaffRegister>();
            LibraryStudentRegisters = new HashSet<LibraryStudentRegister>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
            MarkGradeMaps = new HashSet<MarkGradeMap>();
            MarkGrades = new HashSet<MarkGrade>();
            MarkRegisters = new HashSet<MarkRegister>();
            Mediums = new HashSet<Medium>();
            MeetingRequests = new HashSet<MeetingRequest>();
            OnlineExamQuestionMaps = new HashSet<OnlineExamQuestionMap>();
            OnlineExamQuestions = new HashSet<OnlineExamQuestion>();
            OnlineExamResults = new HashSet<OnlineExamResult>();
            OnlineExams = new HashSet<OnlineExam>();
            PackageConfigs = new HashSet<PackageConfig>();
            Parents = new HashSet<Parent>();
            ProgressReports = new HashSet<ProgressReport>();
            Refunds = new HashSet<Refund>();
            RemarksEntries = new HashSet<RemarksEntry>();
            Route1 = new HashSet<Route1>();
            RouteGroups = new HashSet<RouteGroup>();
            RouteTypes = new HashSet<RouteType>();
            RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            SchoolCalenders = new HashSet<SchoolCalender>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            SchoolDateSettings = new HashSet<SchoolDateSetting>();
            SchoolPayerBankDetailMaps = new HashSet<SchoolPayerBankDetailMap>();
            Sections = new HashSet<Section>();
            Shifts = new HashSet<Shift>();
            ShoppingCart1 = new HashSet<ShoppingCart1>();
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            SignupSlotMaps = new HashSet<SignupSlotMap>();
            Signups = new HashSet<Signup>();
            StaffAttendences = new HashSet<StaffAttendence>();
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            Stops = new HashSet<Stop>();
            StreamSubjectMaps = new HashSet<StreamSubjectMap>();
            Streams = new HashSet<Stream>();
            StudentAchievements = new HashSet<StudentAchievement>();
            StudentApplications = new HashSet<StudentApplication>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentFeeDues = new HashSet<StudentFeeDue>();
            StudentGroupMaps = new HashSet<StudentGroupMap>();
            StudentGroups = new HashSet<StudentGroup>();
            StudentHous = new HashSet<StudentHous>();
            StudentLeaveApplications = new HashSet<StudentLeaveApplication>();
            StudentPickers = new HashSet<StudentPicker>();
            StudentPromotionLogSchools = new HashSet<StudentPromotionLog>();
            StudentPromotionLogShiftFromSchools = new HashSet<StudentPromotionLog>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentTransferRequestReasons = new HashSet<StudentTransferRequestReason>();
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
            Students = new HashSet<Student>();
            SubjectGroupSubjectMaps = new HashSet<SubjectGroupSubjectMap>();
            SubjectInchargerClassMaps = new HashSet<SubjectInchargerClassMap>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            Subjects = new HashSet<Subject>();
            Syllabus = new HashSet<Syllabu>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableLogs = new HashSet<TimeTableLog>();
            TimeTables = new HashSet<TimeTable>();
            TransactionHeads = new HashSet<TransactionHead>();
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
            TransportApplications = new HashSet<TransportApplication>();
            VehicleDetailMaps = new HashSet<VehicleDetailMap>();
            VehicleTrackings = new HashSet<VehicleTracking>();
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        public byte SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string RegistrationID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(50)]
        public string SchoolCode { get; set; }
        [StringLength(100)]
        public string Place { get; set; }
        public string EmployerEID { get; set; }
        public string PayerEID { get; set; }
        public string PayerQID { get; set; }
        [StringLength(10)]
        public string SchoolShortName { get; set; }
        public long? SchoolProfileID { get; set; }
        public long? SchoolSealID { get; set; }
        public long? SponsorID { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("Schools")]
        public virtual Company Company { get; set; }
        [ForeignKey("SponsorID")]
        [InverseProperty("Schools")]
        public virtual Sponsor Sponsor { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AcademicClassMap> AcademicClassMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AcademicSchoolMap> AcademicSchoolMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AdmissionEnquiry> AdmissionEnquiries { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AgeCriteria> AgeCriterias { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [InverseProperty("FromSchool")]
        public virtual ICollection<CampusTransfer> CampusTransferFromSchools { get; set; }
        [InverseProperty("ToSchool")]
        public virtual ICollection<CampusTransfer> CampusTransferToSchools { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Circular> Circulars { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassClassGroupMap> ClassClassGroupMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassCoordinatorMap> ClassCoordinatorMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassCoordinator> ClassCoordinators { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassFeeMaster> ClassFeeMasters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassSectionMap> ClassSectionMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ClassTiming> ClassTimings { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<CostCenter> CostCenters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<CounselorHub> CounselorHubs { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Department1> Department1 { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ExamGroup> ExamGroups { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ExamType> ExamTypes { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FeeMaster> FeeMasters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FeePeriod> FeePeriods { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FeeStructure> FeeStructures { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FeeType> FeeTypes { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FineMasterStudentMap> FineMasterStudentMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FineMaster> FineMasters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<FunctionalPeriod> FunctionalPeriods { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Gallery> Galleries { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<HealthEntry> HealthEntries { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<LibraryStaffRegister> LibraryStaffRegisters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<LibraryStudentRegister> LibraryStudentRegisters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<MarkGradeMap> MarkGradeMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<MarkGrade> MarkGrades { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Medium> Mediums { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<OnlineExamQuestion> OnlineExamQuestions { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<OnlineExam> OnlineExams { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<PackageConfig> PackageConfigs { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Parent> Parents { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Refund> Refunds { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Route1> Route1 { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<RouteGroup> RouteGroups { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<RouteType> RouteTypes { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SchoolCalender> SchoolCalenders { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SchoolDateSetting> SchoolDateSettings { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SchoolPayerBankDetailMap> SchoolPayerBankDetailMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Section> Sections { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Shift> Shifts { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<ShoppingCart1> ShoppingCart1 { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Signup> Signups { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Stop> Stops { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StreamSubjectMap> StreamSubjectMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Stream> Streams { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentGroupMap> StudentGroupMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentGroup> StudentGroups { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentHous> StudentHous { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentLeaveApplication> StudentLeaveApplications { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentPicker> StudentPickers { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogSchools { get; set; }
        [InverseProperty("ShiftFromSchool")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogShiftFromSchools { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentTransferRequestReason> StudentTransferRequestReasons { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SubjectGroupSubjectMap> SubjectGroupSubjectMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SubjectInchargerClassMap> SubjectInchargerClassMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Subject> Subjects { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Syllabu> Syllabus { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<TimeTable> TimeTables { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<TransportApplication> TransportApplications { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<VehicleDetailMap> VehicleDetailMaps { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<VehicleTracking> VehicleTrackings { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
