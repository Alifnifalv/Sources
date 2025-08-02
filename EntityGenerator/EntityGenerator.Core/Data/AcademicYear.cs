using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AcademicYears", Schema = "schools")]
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            AcadamicCalendars = new HashSet<AcadamicCalendar>();
            AcademicClassMaps = new HashSet<AcademicClassMap>();
            AcademicSchoolMaps = new HashSet<AcademicSchoolMap>();
            AgeCriterias = new HashSet<AgeCriteria>();
            Agenda = new HashSet<Agenda>();
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            Assignments = new HashSet<Assignment>();
            CampusTransferFromAcademicYears = new HashSet<CampusTransfer>();
            CampusTransferToAcademicYears = new HashSet<CampusTransfer>();
            Chapters = new HashSet<Chapter>();
            Circulars = new HashSet<Circular>();
            ClassClassGroupMaps = new HashSet<ClassClassGroupMap>();
            ClassClassTeacherMaps = new HashSet<ClassClassTeacherMap>();
            ClassCoordinatorMaps = new HashSet<ClassCoordinatorMap>();
            ClassCoordinators = new HashSet<ClassCoordinator>();
            ClassFeeMasters = new HashSet<ClassFeeMaster>();
            ClassFeeStructureMaps = new HashSet<ClassFeeStructureMap>();
            ClassGroups = new HashSet<ClassGroup>();
            ClassSectionMaps = new HashSet<ClassSectionMap>();
            ClassSectionSubjectPeriodMaps = new HashSet<ClassSectionSubjectPeriodMap>();
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
            FeeDueCancellations = new HashSet<FeeDueCancellation>();
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
            ProductClassMaps = new HashSet<ProductClassMap>();
            ProductStudentMaps = new HashSet<ProductStudentMap>();
            ProgressReports = new HashSet<ProgressReport>();
            Refunds = new HashSet<Refund>();
            RemarksEntries = new HashSet<RemarksEntry>();
            Route1 = new HashSet<Route1>();
            RouteGroups = new HashSet<RouteGroup>();
            RouteStopMaps = new HashSet<RouteStopMap>();
            RouteTypes = new HashSet<RouteType>();
            RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            SchoolCalenders = new HashSet<SchoolCalender>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            SchoolDateSettings = new HashSet<SchoolDateSetting>();
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
            StreamGroups = new HashSet<StreamGroup>();
            StreamSubjectMaps = new HashSet<StreamSubjectMap>();
            Streams = new HashSet<Stream>();
            StudentAcademicYears = new HashSet<Student>();
            StudentAchievements = new HashSet<StudentAchievement>();
            StudentApplications = new HashSet<StudentApplication>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            StudentFeeDues = new HashSet<StudentFeeDue>();
            StudentGroupFeeMasters = new HashSet<StudentGroupFeeMaster>();
            StudentGroupMaps = new HashSet<StudentGroupMap>();
            StudentGroups = new HashSet<StudentGroup>();
            StudentHous = new HashSet<StudentHous>();
            StudentLeaveApplications = new HashSet<StudentLeaveApplication>();
            StudentPickers = new HashSet<StudentPicker>();
            StudentPromotionLogAcademicYears = new HashSet<StudentPromotionLog>();
            StudentPromotionLogShiftFromAcademicYears = new HashSet<StudentPromotionLog>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentSchoolAcademicyears = new HashSet<Student>();
            StudentTransferRequestReasons = new HashSet<StudentTransferRequestReason>();
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
            SubjectGroupSubjectMaps = new HashSet<SubjectGroupSubjectMap>();
            SubjectInchargerClassMaps = new HashSet<SubjectInchargerClassMap>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            SubjectUnits = new HashSet<SubjectUnit>();
            Subjects = new HashSet<Subject>();
            Syllabus = new HashSet<Syllabu>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableExtensions = new HashSet<TimeTableExtension>();
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
        public int AcademicYearID { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? AcademicYearStatusID { get; set; }
        public int? ORDERNO { get; set; }
        public bool? IsUsedForApplication { get; set; }

        [ForeignKey("AcademicYearStatusID")]
        [InverseProperty("AcademicYears")]
        public virtual AcademicYearStatu AcademicYearStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AcademicYears")]
        public virtual School School { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<AcademicClassMap> AcademicClassMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<AcademicSchoolMap> AcademicSchoolMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<AgeCriteria> AgeCriterias { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [InverseProperty("FromAcademicYear")]
        public virtual ICollection<CampusTransfer> CampusTransferFromAcademicYears { get; set; }
        [InverseProperty("ToAcademicYear")]
        public virtual ICollection<CampusTransfer> CampusTransferToAcademicYears { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Chapter> Chapters { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<Circular> Circulars { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassClassGroupMap> ClassClassGroupMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassCoordinatorMap> ClassCoordinatorMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassCoordinator> ClassCoordinators { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<ClassFeeMaster> ClassFeeMasters { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassSectionMap> ClassSectionMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassSectionSubjectPeriodMap> ClassSectionSubjectPeriodMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ClassTiming> ClassTimings { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<CostCenter> CostCenters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<CounselorHub> CounselorHubs { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Department1> Department1 { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ExamGroup> ExamGroups { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ExamType> ExamTypes { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FeeDueCancellation> FeeDueCancellations { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FeeMaster> FeeMasters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FeePeriod> FeePeriods { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<FeeStructure> FeeStructures { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FeeType> FeeTypes { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FineMasterStudentMap> FineMasterStudentMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FineMaster> FineMasters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<FunctionalPeriod> FunctionalPeriods { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Gallery> Galleries { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<HealthEntry> HealthEntries { get; set; }
        [InverseProperty("AcademicYearNavigation")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<LibraryStaffRegister> LibraryStaffRegisters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<LibraryStudentRegister> LibraryStudentRegisters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<MarkGradeMap> MarkGradeMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<MarkGrade> MarkGrades { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Medium> Mediums { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<OnlineExamQuestion> OnlineExamQuestions { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<OnlineExamResult> OnlineExamResults { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<OnlineExam> OnlineExams { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<PackageConfig> PackageConfigs { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Parent> Parents { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ProductClassMap> ProductClassMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ProductStudentMap> ProductStudentMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<Refund> Refunds { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Route1> Route1 { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<RouteGroup> RouteGroups { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<RouteStopMap> RouteStopMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<RouteType> RouteTypes { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SchoolCalender> SchoolCalenders { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SchoolDateSetting> SchoolDateSettings { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Section> Sections { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Shift> Shifts { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<ShoppingCart1> ShoppingCart1 { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Signup> Signups { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Stop> Stops { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StreamGroup> StreamGroups { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StreamSubjectMap> StreamSubjectMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Stream> Streams { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Student> StudentAcademicYears { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
        [InverseProperty("SchoolAcademicyear")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }
        [InverseProperty("AcadamicYear")]
        public virtual ICollection<StudentGroupFeeMaster> StudentGroupFeeMasters { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentGroupMap> StudentGroupMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentGroup> StudentGroups { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentHous> StudentHous { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentLeaveApplication> StudentLeaveApplications { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentPicker> StudentPickers { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogAcademicYears { get; set; }
        [InverseProperty("ShiftFromAcademicYear")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogShiftFromAcademicYears { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        [InverseProperty("SchoolAcademicyear")]
        public virtual ICollection<Student> StudentSchoolAcademicyears { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentTransferRequestReason> StudentTransferRequestReasons { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SubjectGroupSubjectMap> SubjectGroupSubjectMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SubjectInchargerClassMap> SubjectInchargerClassMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<SubjectUnit> SubjectUnits { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Subject> Subjects { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Syllabu> Syllabus { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TimeTableExtension> TimeTableExtensions { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TimeTable> TimeTables { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<TransportApplication> TransportApplications { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<VehicleDetailMap> VehicleDetailMaps { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<VehicleTracking> VehicleTrackings { get; set; }
        [InverseProperty("AcademicYear")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
