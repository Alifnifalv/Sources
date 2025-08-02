using EntityGenerator.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models.School
{
    public partial class dbEduegateSchoolContext : DbContext
    {
        public dbEduegateSchoolContext()
        {
        }

        public dbEduegateSchoolContext(DbContextOptions<dbEduegateSchoolContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetSchoolConnectionString());
            }
        }

        public virtual DbSet<VolunteerType> VolunteerTypes { get; set; }
        public virtual DbSet<VisaDetailMap> VisaDetailMaps { get; set; }
        public virtual DbSet<PassportDetailMap> PassportDetailMaps { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UnitGroup> UnitGroups { get; set; }
        //public virtual DbSet<ProductSKUMap> ProductSKUMaps { get; set; }
        public virtual DbSet<ProductSKURackMap> ProductSKURackMaps { get; set; }
        public virtual DbSet<Rack> Racks { get; set; }
        public virtual DbSet<ClassSectionMap> ClassSectionMaps { get; set; }
        public virtual DbSet<LoginRoleMap> LoginRoleMaps { get; set; }
        public virtual DbSet<AssignVehicleAttendantMap> AssignVehicleAttendantMaps { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<FeePaymentMode> FeePaymentModes { get; set; }
        public virtual DbSet<StudentPassportDetail> StudentPassportDetails { get; set; }
        public virtual DbSet<StudentAssignmentMap> StudentAssignmentMaps { get; set; }

        public virtual DbSet<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }
        public virtual DbSet<StudentGroupFeeMaster> StudentGroupFeeMasters { get; set; }
        public virtual DbSet<StudentGroupMap> StudentGroupMaps { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudentGroupType> StudentGroupTypes { get; set; }
        public virtual DbSet<RouteVehicleMap> RouteVehicleMaps { get; set; }
        public virtual DbSet<StaffRouteStopMap> StaffRouteStopMaps { get; set; }
        public virtual DbSet<VehicleDetailMap> VehicleDetailMaps { get; set; }
        public virtual DbSet<AssignVehicleMap> AssignVehicleMaps { get; set; }
        public virtual DbSet<VehicleTransmission> VehicleTransmissions { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<ClassFeeMaster> ClassFeeMasters { get; set; }
        public virtual DbSet<FeeMasterClassMap> FeeMasterClassMaps { get; set; }
        public virtual DbSet<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }
        public virtual DbSet<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }

        public virtual DbSet<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        public virtual DbSet<FeeCollection> FeeCollections { get; set; }
        public virtual DbSet<FeeCollectionStatus> FeeCollectionStatuses { get; set; }

        public virtual DbSet<HolidayList> HolidayLists { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HolidayType> HolidayTypes { get; set; }

        public virtual DbSet<AcademicClassMap> AcademicClassMaps { get; set; }

        public virtual DbSet<SchoolCalenderHolidayMap> SchoolCalenderHolidayMaps { get; set; }
        public virtual DbSet<SchoolCalender> SchoolCalenders { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<AccountBehavoir> AccountBehavoirs { get; set; }
        public virtual DbSet<StudentTransferRequest> StudentTransferRequest { get; set; }
        public virtual DbSet<TransferRequestStatus> TransferRequestStatuses { get; set; }
        public virtual DbSet<StudentTransferRequestReason> StudentTransferRequestReasons { get; set; }
        //public virtual DbSet<FeePaymentModes> FeePaymentModes { get; set; }
        public virtual DbSet<FeeCycle> FeeCycles { get; set; }

        public virtual DbSet<FeePeriod> FeePeriods { get; set; }
        public virtual DbSet<FeePeriodType> FeePeriodTypes { get; set; }
        public virtual DbSet<BuildingClassRoomMap> BuildingClassRoomMaps { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }

        public virtual DbSet<StudentApplication> StudentApplications { get; set; }
        public virtual DbSet<StudentApplicationSiblingMap> StudentApplicationSiblingMaps { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeAdditionalInfo> EmployeeAdditionalInfos { get; set; }
        public virtual DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }
        public virtual DbSet<Catogory> Catogories { get; set; }
        public virtual DbSet<Relegion> Relegions { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }

        public virtual DbSet<StudentMiscDetail> StudentMiscDetails { get; set; }
        public virtual DbSet<StudentAttendence> StudentAttendences { get; set; }
        public virtual DbSet<StudentCategory> StudentCategories { get; set; }
        public virtual DbSet<StudentHouse> StudentHouses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentSiblingMap> StudentSiblingMaps { get; set; }
        public virtual DbSet<StudentLeaveApplication> StudentLeaveApplications { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectType> SubjectTypes { get; set; }
        public virtual DbSet<ClassTeacherMap> ClassTeacherMaps { get; set; }
        public virtual DbSet<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }
        public virtual DbSet<ClassClassGroupMap> ClassClassGroupMaps { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassSubjectMap> ClassSubjectMaps { get; set; }
        public virtual DbSet<ExamClassMap> ExamClassMaps { get; set; }
        public virtual DbSet<ExamSubjectMap> ExamSubjectMaps { get; set; }
        public virtual DbSet<ExamGroupCategoryMap> ExamGroupCategoryMaps { get; set; }
        public virtual DbSet<ExamGroup> ExamGroups { get; set; }
        public virtual DbSet<ExamGroupSubjectMap> ExamGroupSubjectMaps { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamSchedule> ExamSchedules { get; set; }
        public virtual DbSet<ExamType> ExamTypes { get; set; }
        public virtual DbSet<FeeDiscount> FeeDiscounts { get; set; }
        public virtual DbSet<FeeGroup> FeeGroups { get; set; }
        public virtual DbSet<FeeMaster> FeeMasters { get; set; }
        public virtual DbSet<FeeType> FeeTypes { get; set; }
        public virtual DbSet<GuardianType> GuardianTypes { get; set; }
        public virtual DbSet<MarkGrade> MarkGrades { get; set; }
        public virtual DbSet<MarkGradeMap> MarkGradeMaps { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<PresentStatus> PresentStatuses { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<StudentSkillRegister> StudentSkillRegisters { get; set; }
        public virtual DbSet<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }
        public virtual DbSet<StaffRouteMonthlySplit> StaffRouteMonthlySplits { get; set; }
        public virtual DbSet<HostelRoom> HostelRooms { get; set; }
        public virtual DbSet<Hostel> Hostels { get; set; }
        public virtual DbSet<HostelType> HostelTypes { get; set; }
        public virtual DbSet<LibraryBook> LibraryBooks { get; set; }
        public virtual DbSet<LibraryStaffRegister> LibraryStaffRegisters { get; set; }
        public virtual DbSet<LibraryStudentRegister> LibraryStudentRegisters { get; set; }
        public virtual DbSet<LibraryTransaction> LibraryTransactions { get; set; }
        public virtual DbSet<LibraryTransactionType> LibraryTransactionTypes { get; set; }
        public virtual DbSet<LibraryBookMap> LibraryBookMaps { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        public virtual DbSet<StaffAttendence> StaffAttendences { get; set; }
        public virtual DbSet<StaffPresentStatus> StaffPresentStatuses { get; set; }

        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Medium> Mediums { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AcademicYearStatu> AcademicYearStatus { get; set; }
        public virtual DbSet<AdmissionEnquiry> AdmissionEnquiries { get; set; }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyGroup> CompanyGroups { get; set; }
        public virtual DbSet<Schools> School { get; set; }
        public virtual DbSet<ClassRoomType> ClassRoomTypes { get; set; }
        public virtual DbSet<LibraryBookCategory> LibraryBookCategories { get; set; }
        public virtual DbSet<LibraryBookCategoryMap> LibraryBookCategoryMaps { get; set; }
        public virtual DbSet<LibraryBookCondition> LibraryBookConditions { get; set; }
        public virtual DbSet<LibraryBookStatus> LibraryBookStatuses { get; set; }
        public virtual DbSet<LibraryBookType> LibraryBookTypes { get; set; }
        public virtual DbSet<VehicleOwnershipType> VehicleOwnershipTypes { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<ClassGroup> ClassGroups { get; set; }
        public virtual DbSet<ClassGroupTeacherMap> ClassGroupTeacherMaps { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public virtual DbSet<WeekDay> WeekDays { get; set; }
        public virtual DbSet<TimeTableAllocation> TimeTableAllocations { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }

        public virtual DbSet<AlbumImageMap> AlbumImageMaps { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AlbumType> AlbumTypes { get; set; }
        public virtual DbSet<EventAudienceMap> EventAudienceMaps { get; set; }
        public virtual DbSet<EventAudienceType> EventAudienceTypes { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<PollAnswerMap> PollAnswerMaps { get; set; }
        public virtual DbSet<Poll> Polls { get; set; }
        public virtual DbSet<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
        public virtual DbSet<QuestionnaireAnswerType> QuestionnaireAnswerTypes { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireSet> QuestionnaireSets { get; set; }
        public virtual DbSet<QuestionnaireType> QuestionnaireTypes { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelType> ChannelTypes { get; set; }

        public virtual DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public virtual DbSet<LeaveApplication> LeaveApplications { get; set; }
        public virtual DbSet<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }
        public virtual DbSet<LeaveSession> LeaveSessions { get; set; }
        public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        public virtual DbSet<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }
        public virtual DbSet<StudentFeeDue> StudentFeeDues { get; set; }
        public virtual DbSet<SubjectTeacherMap> SubjectTeacherMaps { get; set; }

        public virtual DbSet<AssignmentAttachmentMap> AssignmentAttachmentMaps { get; set; }
        public virtual DbSet<AssignmentDocument> AssignmentDocuments { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentSectionMap> AssignmentSectionMaps { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        public virtual DbSet<AssignmentType> AssignmentTypes { get; set; }

        public virtual DbSet<Accounts_SubLedger> Accounts_SubLedger { get; set; }
        public virtual DbSet<Accounts_SubLedger_Relation> Accounts_SubLedger_Relation { get; set; }

        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonPlanStatus> LessonPlanStatuses { get; set; }
        public virtual DbSet<LessonPlanTopicMap> LessonPlanTopicMaps { get; set; }

        public virtual DbSet<ClassAgeLimit> ClassAgeLimits { get; set; }

        public virtual DbSet<Routes1> Routes1 { get; set; }
        public virtual DbSet<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        public virtual DbSet<RouteStopMap> RouteStopMaps { get; set; }
        public virtual DbSet<RouteType> RouteTypes { get; set; }
        public virtual DbSet<RouteGroup> RouteGroups { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<MarkRegister> MarkRegisters { get; set; }
        public virtual DbSet<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }

        public virtual DbSet<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        public virtual DbSet<MarkRegisterSkill> MarkRegisterSkills { get; set; }
        public virtual DbSet<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual DbSet<ClassTiming> ClassTimings { get; set; }
        public virtual DbSet<ClassTimingSet> ClassTimingSets { get; set; }
        public virtual DbSet<Day> Days { get; set; }

        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<CostCenterAccountMap> CostCenterAccountMaps { get; set; }
        public virtual DbSet<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
        public virtual DbSet<NotificationAlert> NotificationAlerts { get; set; }
        public virtual DbSet<CareerNotificationAlert> CareerNotificationAlerts { get; set; } 
        public virtual DbSet<AlertStatus> AlertStatuses { get; set; }
        public virtual DbSet<AlertType> AlertTypes { get; set; }

        public virtual DbSet<FineMaster> FineMasters { get; set; }
        public virtual DbSet<FineMasterStudentMap> FineMasterStudentMaps { get; set; }
        public virtual DbSet<SkillGroupMaster> SkillGroupMasters { get; set; }
        public virtual DbSet<SkillMaster> SkillMasters { get; set; }

        public virtual DbSet<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        public virtual DbSet<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }

        public virtual DbSet<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }
        public virtual DbSet<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }

        public virtual DbSet<PackageConfig> PackageConfigs { get; set; }
        public virtual DbSet<PackageConfigClassMap> PackageConfigClassMaps { get; set; }
        public virtual DbSet<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }
        public virtual DbSet<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }

        public virtual DbSet<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }
        public virtual DbSet<CategoryFeeMap> CategoryFeeMaps { get; set; }
        public virtual DbSet<FeeStructureMontlySplitMap> FeeStructureMontlySplitMaps { get; set; }
        public virtual DbSet<FeeStructure> FeeStructures { get; set; }
        public virtual DbSet<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }

        public virtual DbSet<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }
        public virtual DbSet<SchoolCreditNote> SchoolCreditNotes { get; set; }
        public virtual DbSet<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        public virtual DbSet<AcadamicCalendar> AcadamicCalendars { get; set; }
        public virtual DbSet<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
        public virtual DbSet<AcademicYearCalendarStatus> AcademicYearCalendarStatus { get; set; }
        public virtual DbSet<CalendarEntry> CalendarEntries { get; set; }

        public virtual DbSet<FinalSettlement> FinalSettlements { get; set; }
        public virtual DbSet<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        public virtual DbSet<FinalSettlementPaymentModeMap> FinalSettlementPaymentModeMaps { get; set; }
        public virtual DbSet<StudentPromotionLog> StudentPromotionLogs { get; set; }
        public virtual DbSet<PassportVisaDetail> PassportVisaDetails { get; set; }
        public virtual DbSet<ApplicationSubmitType> ApplicationSubmitTypes { get; set; }
        public virtual DbSet<Departments1> Departments1 { get; set; }
        public virtual DbSet<DepartmentStatus> DepartmentStatuses { get; set; }
        public virtual DbSet<TeachingAid> TeachingAids { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<LessonPlanAttachmentMap> LessonPlanAttachmentMaps { get; set; }
        public virtual DbSet<LessonPlanTopicAttachmentMap> LessonPlanTopicAttachmentMaps { get; set; }
        public virtual DbSet<LessonPlanTaskMap> LessonPlanTaskMaps { get; set; }
        public virtual DbSet<LessonPlanTaskAttachmentMap> LessonPlanTaskAttachmentMaps { get; set; }
        public virtual DbSet<LessonPlanClassSectionMap> LessonPlanClassSectionMaps { get; set; }
        public virtual DbSet<LessonPlanLearningObjectiveMap> LessonPlanLearningObjectiveMaps { get; set; }
        public virtual DbSet<LessonPlanLearningOutcomeMap> LessonPlanLearningOutcomeMaps { get; set; }
        public virtual DbSet<RemarksEntry> RemarksEntries { get; set; }
        public virtual DbSet<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }

        public virtual DbSet<CircularMap> CircularMaps { get; set; }
        public virtual DbSet<CircularPriority> CircularPriorities { get; set; }
        public virtual DbSet<Circular> Circulars { get; set; }
        public virtual DbSet<CircularStatus> CircularStatuses { get; set; }
        public virtual DbSet<CircularType> CircularTypes { get; set; }
        public virtual DbSet<CircularUserTypeMap> CircularUserTypeMaps { get; set; }
        public virtual DbSet<CircularUserType> CircularUserTypes { get; set; }
        public virtual DbSet<CircularAttachmentMap> CircularAttachmentMaps { get; set; }
        public virtual DbSet<CounselorHub> CounselorHubs { get; set; }
        public virtual DbSet<CounselorHubMap> CounselorHubMaps { get; set; }
        public virtual DbSet<CounselorHubAttachmentMap> CounselorHubAttachmentMaps { get; set; }
        public virtual DbSet<CounselorHubStatus> CounselorHubStatuses { get; set; }



        public virtual DbSet<ExamSkillMap> ExamSkillMaps { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<TransportStatus> TransportStatuses { get; set; }
        public virtual DbSet<ParentUploadDocumentMap> ParentUploadDocumentMaps { get; set; }
        public virtual DbSet<UploadDocument> UploadDocuments { get; set; }
        public virtual DbSet<UploadDocumentType> UploadDocumentTypes { get; set; }

        public virtual DbSet<AgeCriteria> AgeCriterias { get; set; }

        public virtual DbSet<Agenda> Agendas { get; set; }
        public virtual DbSet<AgendaStatus> AgendaStatuses { get; set; }
        public virtual DbSet<AgendaSectionMap> AgendaSectionMaps { get; set; }
        public virtual DbSet<AgendaAttachmentMap> AgendaAttachmentMaps { get; set; }
        public virtual DbSet<AgendaTaskAttachmentMap> AgendaTaskAttachmentMaps { get; set; }
        public virtual DbSet<AgendaTaskMap> AgendaTaskMaps { get; set; }
        public virtual DbSet<AgendaTopicAttachmentMap> AgendaTopicAttachmentMaps { get; set; }
        public virtual DbSet<AgendaTopicMap> AgendaTopicMaps { get; set; }

        public virtual DbSet<HealthEntry> HealthEntries { get; set; }
        public virtual DbSet<HealthEntryStudentMap> HealthEntryStudentMaps { get; set; }

        public virtual DbSet<StudentApplicationDocumentMap> StudentApplicationDocumentMaps { get; set; }

        public virtual DbSet<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
        public virtual DbSet<TransportApplication> TransportApplications { get; set; }
        public virtual DbSet<TransportApplicationStatus> TransportApplicationStatuses { get; set; }
        public virtual DbSet<Street> Streets { get; set; }

        public virtual DbSet<StreamGroup> StreamGroups { get; set; }
        public virtual DbSet<Stream> Streams { get; set; }
        public virtual DbSet<StreamSubjectMap> StreamSubjectMaps { get; set; }
        public virtual DbSet<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }
        public virtual DbSet<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }
        public virtual DbSet<StreamOptionalSubjectMap> StreamOptionalSubjectMaps { get; set; }

        public virtual DbSet<CampusTransfers> CampusTransfers { get; set; }
        public virtual DbSet<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
        public virtual DbSet<CampusTransferMonthlySplit> CampusTransferMonthlySplits { get; set; }


        public virtual DbSet<WorkflowEntity> WorkflowEntitys { get; set; }

        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<ClassSubjectWorkflowEntityMap> ClassSubjectWorkflowEntityMaps { get; set; }

        public virtual DbSet<SchoolGeoMap> SchoolGeoMaps { get; set; }
        public virtual DbSet<GeoLocationLog> GeoLocationLogs { get; set; }

        public virtual DbSet<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        public virtual DbSet<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
        public virtual DbSet<RemarksEntryStudentMap> RemarksEntryStudentMaps { get; set; }

        public virtual DbSet<AcademicYearCalendarEventType> AcademicYearCalendarEventTypes { get; set; }

        public virtual DbSet<Month> Months { get; set; }

        public virtual DbSet<ClassWorkFlowMap> ClassWorkFlowMaps { get; set; }

        public virtual DbSet<AcademicSchoolMap> AcademicSchoolMaps { get; set; }
        public virtual DbSet<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }

        public virtual DbSet<DriverScheduleLog> DriverScheduleLogs { get; set; }
        public virtual DbSet<ScheduleLogStatus> ScheduleLogStatuses { get; set; }
        public virtual DbSet<StopEntryStatus> StopEntryStatuses { get; set; }
        public virtual DbSet<FunctionalPeriod> FunctionalPeriods { get; set; }
        public virtual DbSet<Refund> Refunds { get; set; }
        public virtual DbSet<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
        public virtual DbSet<RefundMonthlySplit> RefundMonthlySplits { get; set; }
        public virtual DbSet<RefundPaymentModeMap> RefundPaymentModeMaps { get; set; }

        public virtual DbSet<StudentRoutePeriodMap> StudentRoutePeriodMaps { get; set; }

        public virtual DbSet<ClassAssociateTeacherMap> ClassAssociateTeacherMaps { get; set; }

        public virtual DbSet<ClassCoordinatorClassMap> ClassCoordinatorClassMaps { get; set; }
        public virtual DbSet<ClassCoordinatorMap> ClassCoordinatorMaps { get; set; }
        public virtual DbSet<ClassCoordinator> ClassCoordinators { get; set; }

        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<GalleryAttachmentMap> GalleryAttachmentMaps { get; set; }

        public virtual DbSet<SchoolEvent> SchoolEvents { get; set; }
        public virtual DbSet<EventTransportAllocation> EventTransportAllocations { get; set; }
        public virtual DbSet<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }
        public virtual DbSet<AdditionalExpens> AdditionalExpenses { get; set; }
        public virtual DbSet<ContentFile> ContentFiles { get; set; }
        public virtual DbSet<StudentPickedBy> StudentPickedBies { get; set; }
        public virtual DbSet<StudentPickupRequestStatus> StudentPickupRequestStatuses { get; set; }
        public virtual DbSet<StudentPickupRequest> StudentPickupRequests { get; set; }

        public virtual DbSet<StudentPicker> StudentPickers { get; set; }
        public virtual DbSet<StudentPickerStudentMap> StudentPickerStudentMaps { get; set; }
        public virtual DbSet<StudentPickLog> StudentPickLogs { get; set; }

        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<ProgressReport> ProgressReports { get; set; }
        public virtual DbSet<ProgressReportPublishStatus> ProgressReportPublishStatuses { get; set; }
        public virtual DbSet<StudentStaffMap> StudentStaffMaps { get; set; }
        public virtual DbSet<StudentFeeConcession> StudentFeeConcessions { get; set; }
        public virtual DbSet<FeeConcessionApprovalType> FeeConcessionApprovalTypes { get; set; }

        public virtual DbSet<FormField> FormFields { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<FormValue> FormValues { get; set; }

        public virtual DbSet<FeeDueCancellation> FeeDueCancellations { get; set; }
        public virtual DbSet<SkillGroupSubjectMap> SkillGroupSubjectMaps { get; set; }

        public virtual DbSet<AcademicNote> AcademicNotes { get; set; }
        public virtual DbSet<EmailTemplates2> EmailTemplates2 { get; set; }
        public virtual DbSet<EmailTemplateParameterMap> EmailTemplateParameterMaps { get; set; }

        public virtual DbSet<SignupAudienceMap> SignupAudienceMaps { get; set; }
        public virtual DbSet<SignupCategory> SignupCategories { get; set; }
        public virtual DbSet<Signup> Signups { get; set; }
        public virtual DbSet<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        public virtual DbSet<SignupSlotMap> SignupSlotMaps { get; set; }
        public virtual DbSet<SignupSlotType> SignupSlotTypes { get; set; }
        public virtual DbSet<SignupType> SignupTypes { get; set; }

        public virtual DbSet<StudentStatus> StudentStatuses { get; set; }
        public virtual DbSet<SchoolDateSettingMap> SchoolDateSettingMaps { get; set; }
        public virtual DbSet<SchoolDateSetting> SchoolDateSettings { get; set; }

        public virtual DbSet<Complain> Complains { get; set; }
        public virtual DbSet<ComplainType> ComplainTypes { get; set; }
        public virtual DbSet<ComplaintSourceType> ComplaintSourceTypes { get; set; }
        public virtual DbSet<Allergy> Allergies { get; set; }
        public virtual DbSet<AllergyStudentMap> AllergyStudentMaps { get; set; }

        public virtual DbSet<AchievementCategory> AchievementCategories { get; set; }
        public virtual DbSet<AchievementRanking> AchievementRankings { get; set; }
        public virtual DbSet<AchievementType> AchievementTypes { get; set; }
        public virtual DbSet<StudentAchievement> StudentAchievements { get; set; }

        public virtual DbSet<VisitorAttachmentMap> VisitorAttachmentMaps { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<SubjectTopic> SubjectTopics { get; set; }

        public virtual DbSet<TeacherActivity> TeacherActivities { get; set; }
        public virtual DbSet<TimeTableLog> TimeTableLogs { get; set; }

        public virtual DbSet<ContentType> ContentTypes { get; set; }

        public virtual DbSet<StaticContentData> StaticContentDatas { get; set; }
        public virtual DbSet<StaticContentType> StaticContentTypes { get; set; }

        public virtual DbSet<EnquiryReferenceType> EnquiryReferenceTypes { get; set; }
        public virtual DbSet<EnquirySource> EnquirySources { get; set; }

        public virtual DbSet<Sequence> Sequences { get; set; }

        public virtual DbSet<VisitorBook> VisitorBooks { get; set; }

        public virtual DbSet<SchoolPayerBankDetailMap> SchoolPayerBankDetailMaps { get; set; }

        public virtual DbSet<WPSDetail> WPSDetails { get; set; }

        public virtual DbSet<VehicleTracking> VehicleTrackings { get; set; }

        public virtual DbSet<CustomerFeedBacks> CustomerFeedBacks { get; set; }
        public virtual DbSet<CustomerFeedbackType> CustomerFeedbackTypes { get; set; }

        public virtual DbSet<ClassSectionSubjectPeriodMap> ClassSectionSubjectPeriodMaps { get; set; }

        public virtual DbSet<ParentPortalSchoolAcademicClassMap> ParentPortalSchoolAcademicClassMaps { get; set; }

        public virtual DbSet<LessonLearningOutcome> LessonLearningOutcomes { get; set; }

        public virtual DbSet<Chapter> Chapters { get; set; }

        public virtual DbSet<SubjectUnit> SubjectUnits { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<LessonReferenceBook> LessonReferenceBooks { get; set; }

        public virtual DbSet<LessonResource> LessonResources { get; set; }

        public virtual DbSet<LessonLearningObjective> LessonLearningObjectives { get; set; }

        public virtual DbSet<StudentFeeRelationMap> StudentFeeRelationMaps { get; set; }

        public virtual DbSet<TimeTableExtClass> TimeTableExtClasses { get; set; }
        public virtual DbSet<TimeTableExtClassTiming> TimeTableExtClassTimings { get; set; }
        public virtual DbSet<TimeTableExtSection> TimeTableExtSections { get; set; }
        public virtual DbSet<TimeTableExtSubject> TimeTableExtSubjects { get; set; }
        public virtual DbSet<TimeTableExtTeacher> TimeTableExtTeachers { get; set; }
        public virtual DbSet<TimeTableExtWeekDay> TimeTableExtWeekDays { get; set; }
        public virtual DbSet<TimeTableExtension> TimeTableExtensions { get; set; }

        public virtual DbSet<TransportCancelRequest> TransportCancelRequests { get; set; }
        public virtual DbSet<TransportCancellationStatus> TransportCancellationStatuses { get; set; }
        public virtual DbSet<LessonPlanSkillDevelopmentMap> LessonPlanSkillDevelopmentMaps { get; set; }
        public virtual DbSet<LessonPlanAssessmentMap> LessonPlanAssessmentMaps { get; set; }
        public virtual DbSet<LessonPlanTeachingMethodologyMap> LessonPlanTeachingMethodologyMaps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcadamicCalendar>(entity =>
            {
                entity.HasKey(e => e.AcademicCalendarID)
                    .HasName("PK_AcadamicCalendar");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYearCalendarStatus)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.AcademicCalendarStatusID)
                    .HasConstraintName("FK_AcadamicCalenders_AcademicYearCalenderStatus");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_AcadamicCalenders_AcademicYears");

                entity.HasOne(d => d.CalendarType)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.CalendarTypeID)
                    .HasConstraintName("FK_Calendar_type");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcadamicCalendars_School");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

         

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.Comments1)
                    .HasForeignKey(d => d.ParentCommentID)
                    .HasConstraintName("FK_Comments_Comments");
                entity.HasOne(c => c.FromEmployee)
                    .WithMany()
                    .HasForeignKey(c => c.FromLoginID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Comments_Employees_FromLoginID"); // Ensure foreign key constraint name is correct
            });
            modelBuilder.Entity<AcademicClassMap>(entity =>
            {
                entity.HasKey(e => e.AcademicClassMapIID)
                    .HasName("PK_AcademicClasses");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.AcademicClassMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_AcademicClassMaps_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.AcademicClassMaps)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_AcademicClassMaps_Classes");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.AcademicClassMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_AcademicClassMaps_Schools");
            });

            modelBuilder.Entity<AcademicNote>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.AcademicNotes)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_AcademicNotes_Classes");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.AcademicNotes)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_AcademicNotes_Sections");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.AcademicNotes)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_AcademicNotes_Subjects");
            });

            modelBuilder.Entity<AcademicSchoolMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AcademicSchoolMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_AcademicSchoolMaps_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcademicSchoolMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcademicSchoolMaps_Schools");
            });

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.Property(e => e.AcademicYearID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYearStatu)
                    .WithMany(p => p.AcademicYears)
                    .HasForeignKey(d => d.AcademicYearStatusID)
                    .HasConstraintName("FK_AcademicYears_AcademicYearStatus");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcademicYears)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcademicYears_Schools");
            });

            modelBuilder.Entity<AcademicYearCalendarEvent>(entity =>
            {
                entity.HasKey(e => e.AcademicYearCalendarEventIID)
                    .HasName("PK_Events");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcadamicCalendar)
                    .WithMany(p => p.AcademicYearCalendarEvents)
                    .HasForeignKey(d => d.AcademicCalendarID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcademicYearCalendarEvents_AcadamicCalenders");

                entity.HasOne(d => d.AcademicYearCalendarEventType)
                    .WithMany(p => p.AcademicYearCalendarEvents)
                    .HasForeignKey(d => d.AcademicYearCalendarEventTypeID)
                    .HasConstraintName("FK_AcademicYearCalendarEvents_AcademicYearCalendarEventType");
            });

            modelBuilder.Entity<AcademicYearCalendarEventType>(entity =>
            {
                entity.Property(e => e.AcademicYearCalendarEventTypeID).ValueGeneratedNever();
            });


            modelBuilder.Entity<AcademicYearCalendarStatus>(entity =>
            {
                entity.HasKey(e => e.AcademicYearCalendarStatusID)
                    .HasName("PK_AcademicYearStatus");
            });

            modelBuilder.Entity<AcademicYearStatu>(entity =>
            {
                entity.HasKey(e => e.AcademicYearStatusID)
                    .HasName("PK_AcademYearStatus");
            });

            //modelBuilder.Entity<AccomodationType>(entity =>
            //{
            //    entity.Property(e => e.AccomodationTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Account>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountBehavoir)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountBehavoirID)
                    .HasConstraintName("FK_Accounts_AccountBehavoirs");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_Accounts_Groups");

                entity.HasOne(d => d.Account1)
                    .WithMany(p => p.Accounts1)
                    .HasForeignKey(d => d.ParentAccountID)
                    .HasConstraintName("FK_Accounts_Accounts");
            });

            //modelBuilder.Entity<AccountTaxTransaction>(entity =>
            //{
            //    entity.HasOne(d => d.Accound)
            //        .WithMany(p => p.AccountTaxTransactions)
            //        .HasForeignKey(d => d.AccoundID)
            //        .HasConstraintName("FK_AccountTaxTransactions_Accounts");

            //    entity.HasOne(d => d.Head)
            //        .WithMany(p => p.AccountTaxTransactions)
            //        .HasForeignKey(d => d.HeadID)
            //        .HasConstraintName("FK_AccountTaxTransactions_TransactionHead");

            //    entity.HasOne(d => d.TaxTemplate)
            //        .WithMany(p => p.AccountTaxTransactions)
            //        .HasForeignKey(d => d.TaxTemplateID)
            //        .HasConstraintName("FK_AccountTaxTransactions_TaxTemplates");

            //    entity.HasOne(d => d.TaxTemplateItem)
            //        .WithMany(p => p.AccountTaxTransactions)
            //        .HasForeignKey(d => d.TaxTemplateItemID)
            //        .HasConstraintName("FK_AccountTaxTransactions_TaxTemplateItems");

            //    entity.HasOne(d => d.TaxType)
            //        .WithMany(p => p.AccountTaxTransactions)
            //        .HasForeignKey(d => d.TaxTypeID)
            //        .HasConstraintName("FK_AccountTaxTransactions_TaxTypes");
            //});

            modelBuilder.Entity<AccountTransaction>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactions_Accounts");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.BudgetID)
                    .HasConstraintName("FK_AccountTran_Budgets");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_AccountTransactions_CostCenters");

                //entity.HasOne(d => d.DocumentType)
                //    .WithMany(p => p.AccountTransactions)
                //    .HasForeignKey(d => d.DocumentTypeID)
                //    .HasConstraintName("FK_AccountTransactions_DocumentTypes");
            });

            modelBuilder.Entity<AccountTransactionDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactionDetails_Accounts");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_AccountTransactionDetails_AccountTransactionHeads");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.BudgetID)
                    .HasConstraintName("FK_AccountTransac_Budgets");

                //entity.HasOne(d => d.Department)
                //    .WithMany(p => p.AccountTransactionDetails)
                //    .HasForeignKey(d => d.DepartmentID)
                //    .HasConstraintName("FK_AccountTransactionDet_Departments");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.ProductSKUId)
                    .HasConstraintName("FK_AccountTransactionDetails_ProductSKUMaps");

                //entity.HasOne(d => d.SubLedger)
                //    .WithMany(p => p.AccountTransactionDetails)
                //    .HasForeignKey(d => d.SubLedgerID)
                //    .HasConstraintName("FK_AccountTransactionDetails_Accounts_SubLedger");
            });

            modelBuilder.Entity<AccountTransactionHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactionHeads_Accounts");

                //entity.HasOne(d => d.Branch)
                //    .WithMany(p => p.AccountTransactionHeads)
                //    .HasForeignKey(d => d.BranchID)
                //    .HasConstraintName("FK_AccountTransactionHeads_Branches");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_AccountTransactionHeads_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AccountTransactionHeads_DocumentTypes");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_AccountTransactionHeads_TransactionStatuses");
            });

            modelBuilder.Entity<AccountTransactionHeadAccountMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionHeadAccountMaps_AccountTransactionHeads");

                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.AccountTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionHeadAccountMaps_AccountTransactions");
            });

            modelBuilder.Entity<Accounts_SubLedger>(entity =>
            {
                entity.Property(e => e.AllowUserDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllowUserEdit).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllowUserRename).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsHidden).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });


            modelBuilder.Entity<Accounts_SubLedger_Relation>(entity =>
            {
                entity.HasOne(d => d.Accounts_SubLedger)
                    .WithMany(p => p.Accounts_SubLedger_Relation)
                    .HasForeignKey(d => d.SL_AccountID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_SubLedger_Relation_Accounts_SubLedger");
            });

            //modelBuilder.Entity<ActionLinkType>(entity =>
            //{
            //    entity.Property(e => e.ActionLinkTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<ActionStatus>(entity =>
            //{
            //    entity.Property(e => e.ActionStatusID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<ActionType>(entity =>
            //{
            //    entity.Property(e => e.ActionTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Activity>(entity =>
            //{
            //    entity.HasOne(d => d.ActionStatus)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.ActionStatusID)
            //        .HasConstraintName("FK_Activities_ActionStatuses");

            //    entity.HasOne(d => d.ActionType)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.ActionTypeID)
            //        .HasConstraintName("FK_Activities_ActionTypes");

            //    entity.HasOne(d => d.ActivityType)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.ActivityTypeID)
            //        .HasConstraintName("FK_Activities_ActivityTypes");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.UserID)
            //        .HasConstraintName("FK_Activities_UserReferences");
            //});

            //modelBuilder.Entity<ActivityType>(entity =>
            //{
            //    entity.Property(e => e.ActivityTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<AdditionalExpens>(entity =>
            {
                entity.Property(e => e.AdditionalExpenseID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<AdditionalExpenseProvisionalAccountMap>(entity =>
            //{
            //    entity.HasKey(e => e.AdditionalExpProvAccountMapIID)
            //        .HasName("PK__Addition__92AA8BBD7AB4F805");

            //    entity.Property(e => e.Isdefault).HasDefaultValueSql("((0))");
            //});

            //modelBuilder.Entity<AdditionalExpensesTransactionsMap>(entity =>
            //{
            //    entity.HasKey(e => e.AdditionalExpensesTransactionsMapIID)
            //        .HasName("PK__Addition__E3CF711C3A8C47D5");

            //    entity.Property(e => e.ISAffectSupplier).HasDefaultValueSql("((0))");
            //});

            modelBuilder.Entity<AdmissionEnquiry>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AdmissionEnquiries)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AdmissionEnquiries_Schools");
            });

            modelBuilder.Entity<AgeCriteria>(entity =>
            {
                entity.HasKey(e => e.AgeCriteriaIID)
                    .HasName("PK_AgeCriterias");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AgeCriterias)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_AgeCriteria_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.AgeCriterias)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_AgeCriteria_classes");

                entity.HasOne(d => d.Syllabu)
                    .WithMany(p => p.AgeCriterias)
                    .HasForeignKey(d => d.CurriculumID)
                    .HasConstraintName("FK_AgeCriteria_Syllabus");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AgeCriterias)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AgeCriteria_School");
            });

            modelBuilder.Entity<Agenda>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Agendas_AcademicYear");

                entity.HasOne(d => d.AgendaStatus)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.AgendaStatusID)
                    .HasConstraintName("FK_Agendas_AgendaStatuses");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Agendas_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Agendas_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Agendas_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_Agendas_Subjects");

                entity.HasOne(d => d.TeachingAid)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.TeachingAidID)
                    .HasConstraintName("FK_Agendas_TeachingAid");
            });

            modelBuilder.Entity<AgendaAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Agenda)
                //    .WithMany(p => p.AgendaAttachmentMaps)
                //    .HasForeignKey(d => d.AgendaID)
                //    .HasConstraintName("FK_AgendaAttachmentMaps_Agenda");
            });

            modelBuilder.Entity<AgendaSectionMap>(entity =>
            {
                entity.HasKey(e => e.AgendaSectionMapIID)
                    .HasName("PK_AgendaSectionMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Agenda)
                    .WithMany(p => p.AgendaSectionMaps)
                    .HasForeignKey(d => d.AgendaID)
                    .HasConstraintName("FK_AgendaSectionMap_Agenda");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.AgendaSectionMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_AgendaSectionMap_Section");
            });

            modelBuilder.Entity<AgendaTaskAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AgendaTaskMap)
                    .WithMany(p => p.AgendaTaskAttachmentMaps)
                    .HasForeignKey(d => d.AgendaTaskMapID)
                    .HasConstraintName("FK_AgendaAttachment_AgendaTask");
            });

            modelBuilder.Entity<AgendaTaskMap>(entity =>
            {
                entity.HasKey(e => e.AgendaTaskMapIID)
                    .HasName("PK_AgendaTopicTaskMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Agenda)
                    .WithMany(p => p.AgendaTaskMaps)
                    .HasForeignKey(d => d.AgendaID)
                    .HasConstraintName("FK_Agendas");

                entity.HasOne(d => d.AgendaTopicMap)
                    .WithMany(p => p.AgendaTaskMaps)
                    .HasForeignKey(d => d.AgendaTopicMapID)
                    .HasConstraintName("FK_AgendaTaskTopicMaps");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.AgendaTaskMaps)
                    .HasForeignKey(d => d.TaskTypeID)
                    .HasConstraintName("FK_AgendaTaskTaskTypes");
            });

            modelBuilder.Entity<AgendaTopicAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AgendaTopicMap)
                    .WithMany(p => p.AgendaTopicAttachmentMaps)
                    .HasForeignKey(d => d.AgendaTopicMapID)
                    .HasConstraintName("FK_AgendaAttachmentMaps_AgendaTopic");
            });

            modelBuilder.Entity<AgendaTopicMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Agenda)
                    .WithMany(p => p.AgendaTopicMaps)
                    .HasForeignKey(d => d.AgendaID)
                    .HasConstraintName("FK_AgendaTopicMaps_Agendas");
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumID).ValueGeneratedNever();

                entity.HasOne(d => d.AlbumType)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.AlbumTypeID)
                    .HasConstraintName("FK_Albums_AlbumTypes");
            });

            modelBuilder.Entity<AlbumImageMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AlbumImageMaps)
                    .HasForeignKey(d => d.AlbumID)
                    .HasConstraintName("FK_AlbumImageMaps_Albums");
            });


            modelBuilder.Entity<AlertStatus>(entity =>
            {
                entity.Property(e => e.AlertStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AlertType>(entity =>
            {
                entity.Property(e => e.AlertTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.Property(e => e.AllergyID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<AllergyStudentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.AllergyStudentMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_AllergyStudentMaps_AcademicYear");

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.AllergyStudentMaps)
                    .HasForeignKey(d => d.AllergyID)
                    .HasConstraintName("FK_AllergyStudentMaps_Allergy");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.AllergyStudentMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_AllergyStudentMaps_School");

                //entity.HasOne(d => d.Severity)
                //    .WithMany(p => p.AllergyStudentMaps)
                //    .HasForeignKey(d => d.SeverityID)
                //    .HasConstraintName("FK_AllergyStudentMaps_Severities");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AllergyStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_AllergyStudentMaps_Students");
            });

            //modelBuilder.Entity<ApplicationForm>(entity =>
            //{
            //    entity.HasKey(e => e.ApplicationFormIID)
            //        .HasName("PK_ApplicationForm");
            //});

            modelBuilder.Entity<ApplicationSubmitType>(entity =>
            {
                entity.HasKey(e => e.SubmitTypeID)
                    .HasName("PK_SubmitTypeName");

                entity.Property(e => e.SubmitTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Appointment>(entity =>
            //{
            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_Appointments_Customers");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_Appointments_Employees");

            //    entity.HasOne(d => d.FrequencyType)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.FrequencyTypeID)
            //        .HasConstraintName("FK_Appointments_Appointments");

            //    entity.HasOne(d => d.Service)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.ServiceID)
            //        .HasConstraintName("FK_Appointments_Services");
            //});

            //modelBuilder.Entity<Area>(entity =>
            //{
            //    entity.Property(e => e.AreaID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ParentArea)
            //        .WithMany(p => p.InverseParentArea)
            //        .HasForeignKey(d => d.ParentAreaID)
            //        .HasConstraintName("FK_Areas_Areas");
            //});

            //modelBuilder.Entity<AreaCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.AreaID });

            //    entity.HasOne(d => d.Area)
            //        .WithMany(p => p.AreaCultureDatas)
            //        .HasForeignKey(d => d.AreaID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_AreaCultureDatas_Areas");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.AreaCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_AreaCultureDatas_Cultures");
            //});

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasOne(d => d.Account1)
                    .WithMany(p => p.Assets1)
                    .HasForeignKey(d => d.AccumulatedDepGLAccID)
                    .HasConstraintName("FK_Assets_Accounts1");

                //entity.HasOne(d => d.AssetCategory)
                //    .WithMany(p => p.Assets)
                //    .HasForeignKey(d => d.AssetCategoryID)
                //    .HasConstraintName("FK_Assets_AssetCategories");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetGlAccID)
                    .HasConstraintName("FK_Assets_Accounts");

                entity.HasOne(d => d.Account2)
                    .WithMany(p => p.Assets2)
                    .HasForeignKey(d => d.DepreciationExpGLAccId)
                    .HasConstraintName("FK_Assets_Accounts2");
            });

            modelBuilder.Entity<AssetTransactionDetail>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AssetTransactionDetails_Account");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetTransactionDetails_Assets");

                entity.HasOne(d => d.AssetTransactionHead)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_AssetTransactionDetails_AssetTransactionHead");
            });

            modelBuilder.Entity<AssetTransactionHead>(entity =>
            {
                //entity.HasOne(d => d.DocumentStatus)
                //    .WithMany(p => p.AssetTransactionHeads)
                //    .HasForeignKey(d => d.DocumentStatusID)
                //    .HasConstraintName("FK_AssetTransactionHead_DocumentReferenceStatusMap");

                //entity.HasOne(d => d.DocumentType)
                //    .WithMany(p => p.AssetTransactionHeads)
                //    .HasForeignKey(d => d.DocumentTypeID)
                //    .HasConstraintName("FK_AssetTransactionHead_DocumentTypes");

                //entity.HasOne(d => d.ProcessingStatus)
                //    .WithMany(p => p.AssetTransactionHeads)
                //    .HasForeignKey(d => d.ProcessingStatusID)
                //    .HasConstraintName("FK_AssetTransactionHead_TransactionStatuses");
            });

            modelBuilder.Entity<AssetTransactionHeadAccountMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.AssetTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransactionHeadAccountMaps_AccountTransactions");

                entity.HasOne(d => d.AssetTransactionHead)
                    .WithMany(p => p.AssetTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AssetTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransactionHeadAccountMaps_AssetTransactionHead");
            });


            modelBuilder.Entity<AssignVehicleAttendantMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AssignVehicleMap)
                    .WithMany(p => p.AssignVehicleAttendantMaps)
                    .HasForeignKey(d => d.AssignVehicleMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignVehicleAttendantMaps_AssignVehicleMap");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignVehicleAttendantMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_AssignVehicleAttendantMaps_Employees");
            });

            modelBuilder.Entity<AssignVehicleMap>(entity =>
            {
                entity.HasKey(e => e.AssignVehicleMapIID)
                    .HasName("PK_AssignVehicleMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_AssignVehicleMap_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_AssignVehicleMaps_Employees");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.RouteID)
                    .HasConstraintName("FK_AssignVehicleMap_Route");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AssignVehicleMap_School");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_AssignVehicleMaps_Vehicles");
            });


            modelBuilder.Entity<Assignment>(entity =>
            {
                //entity.Property(e => e.TimeStamaps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Assignments_AcademicYears");

                entity.HasOne(d => d.AssignmentStatus)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.AssignmentStatusID)
                    .HasConstraintName("FK_Assignments_AssignmentStatuses");

                entity.HasOne(d => d.AssignmentType)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.AssignmentTypeID)
                    .HasConstraintName("FK_Assignments_AssignmentTypes");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Assignments_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Assignments_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Assignments_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_Assignments_Subjects");
            });

            modelBuilder.Entity<AssignmentAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentAttachmentMaps)
                    .HasForeignKey(d => d.AssignmentID)
                    .HasConstraintName("FK_AssignmentAttachmentMaps_Assignments");
            });

            modelBuilder.Entity<AssignmentDocument>(entity =>
            {
                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentDocuments)
                    .HasForeignKey(d => d.AssignmentID)
                    .HasConstraintName("FK_AssignmentDocument_Assignments");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AssignmentDocuments)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_AssignmentDocument_Students");
            });

            modelBuilder.Entity<AssignmentSectionMap>(entity =>
            {
                entity.HasKey(e => e.AssignmentSectionMapIID)
                    .HasName("PK_AssignmentSectionMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentSectionMaps)
                    .HasForeignKey(d => d.AssignmentID)
                    .HasConstraintName("FK_AssignmentSectionMap_Assignment");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.AssignmentSectionMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_AssignmentSectionMap_Section");
            });

            //modelBuilder.Entity<Attachment>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EntityType)
            //        .WithMany(p => p.Attachments)
            //        .HasForeignKey(d => d.EntityTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Attachments_EntityType");
            //});

            modelBuilder.Entity<Attendence>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AttendenceStatus)
                    .WithMany(p => p.Attendences)
                    .HasForeignKey(d => d.AttendenceStatusID)
                    .HasConstraintName("FK_Attendences_AttendenceStatuses");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Attendences)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Attendences_Companies");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendences)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Attendences_Employees");
            });

            modelBuilder.Entity<AttendenceReason>(entity =>
            {
                entity.Property(e => e.AttendenceReasonID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.BankIID)
                    .HasName("PK_BanksNames");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<Banner>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.BannerType)
            //        .WithMany(p => p.Banners)
            //        .HasForeignKey(d => d.BannerTypeID)
            //        .HasConstraintName("FK_Banners_Banners");

            //    entity.HasOne(d => d.Status)
            //        .WithMany(p => p.Banners)
            //        .HasForeignKey(d => d.StatusID)
            //        .HasConstraintName("FK_Banners_BannerStatuses");
            //});

            //modelBuilder.Entity<BannerType>(entity =>
            //{
            //    entity.Property(e => e.BannerTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Basket>(entity =>
            //{
            //    entity.Property(e => e.BasketID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<BloodGroup>(entity =>
            {
                entity.Property(e => e.BloodGroupID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<BoilerPlate>(entity =>
            //{
            //    entity.Property(e => e.BoilerPlateID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<BoilerPlateParameter>(entity =>
            //{
            //    entity.Property(e => e.BoilerPlateParameterID).ValueGeneratedNever();

            //    entity.HasOne(d => d.BoilerPlate)
            //        .WithMany(p => p.BoilerPlateParameters)
            //        .HasForeignKey(d => d.BoilerPlateID)
            //        .HasConstraintName("FK_BoilerPlateParameters_BoilerPlates");
            //});

            //modelBuilder.Entity<Branch>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.BranchGroup)
            //        .WithMany(p => p.Branches)
            //        .HasForeignKey(d => d.BranchGroupID)
            //        .HasConstraintName("FK_Branches_BranchGroups");

            //    entity.HasOne(d => d.BranchStatuses)
            //        .WithMany(p => p.Branches)
            //        .HasForeignKey(d => d.StatusID)
            //        .HasConstraintName("FK_Branches_BranchStatuses");

            //    entity.HasOne(d => d.Warehouse)
            //        .WithMany(p => p.Branches)
            //        .HasForeignKey(d => d.WarehouseID)
            //        .HasConstraintName("FK_Branches_Warehouses");
            //});

            //modelBuilder.Entity<BranchCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.BranchID });

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.BranchCultureDatas)
            //        .HasForeignKey(d => d.BranchID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_BranchCultureDatas_Branches");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.BranchCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_BranchCultureDatas_Cultures");
            //});

            //modelBuilder.Entity<BranchDocumentTypeMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.BranchDocumentTypeMaps)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_BranchDocumentMaps_Branches");

            //    entity.HasOne(d => d.DocumentType)
            //        .WithMany(p => p.BranchDocumentTypeMaps)
            //        .HasForeignKey(d => d.DocumentTypeID)
            //        .HasConstraintName("FK_BranchDocumentMaps_DocumentTypes");
            //});

            //modelBuilder.Entity<BranchGroup>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Status)
            //        .WithMany(p => p.BranchGroups)
            //        .HasForeignKey(d => d.StatusID)
            //        .HasConstraintName("FK_BranchGroups_BranchGroupStatuses");
            //});

            //modelBuilder.Entity<Brand>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.SEOMetadata)
            //        .WithMany(p => p.Brands)
            //        .HasForeignKey(d => d.SEOMetadataID)
            //        .HasConstraintName("FK_Brands_SEOMetaDataID");

            //    entity.HasOne(d => d.Status)
            //        .WithMany(p => p.Brands)
            //        .HasForeignKey(d => d.StatusID)
            //        .HasConstraintName("FK_Brands_BrandStatuses");
            //});

            //modelBuilder.Entity<BrandCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.BrandID });

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Brand)
            //        .WithMany(p => p.BrandCultureDatas)
            //        .HasForeignKey(d => d.BrandID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_BrandCultureDatas_Brands");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.BrandCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_BrandCultureDatas_Cultures");
            //});

            //modelBuilder.Entity<BrandImageMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Brand)
            //        .WithMany(p => p.BrandImageMaps)
            //        .HasForeignKey(d => d.BrandID)
            //        .HasConstraintName("FK_BrandImageMap_Brands");
            //});

            //modelBuilder.Entity<BrandTag>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Brand)
            //        .WithMany(p => p.BrandTags)
            //        .HasForeignKey(d => d.BrandID)
            //        .HasConstraintName("FK_BrandTags_Brands");
            //});

            //modelBuilder.Entity<BrandTagMap>(entity =>
            //{
            //    entity.HasOne(d => d.Brand)
            //        .WithMany(p => p.BrandTagMaps)
            //        .HasForeignKey(d => d.BrandID)
            //        .HasConstraintName("FK_BrandTagMaps_Brands");

            //    entity.HasOne(d => d.BrandTag)
            //        .WithMany(p => p.BrandTagMaps)
            //        .HasForeignKey(d => d.BrandTagID)
            //        .HasConstraintName("FK_BrandTagMaps_BrandTags");
            //});

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.BudgetID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.BuildingID).ValueGeneratedNever();
            });

            modelBuilder.Entity<BuildingClassRoomMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.BuildingClassRoomMaps)
                    .HasForeignKey(d => d.BuildingID)
                    .HasConstraintName("FK_BuildingClassRoomMaps_Buildings");
            });

            //modelBuilder.Entity<CRMCompany>(entity =>
            //{
            //    entity.Property(e => e.CompanyID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<CalendarEntry>(entity =>
            {
                entity.Property(e => e.CalendarEntryID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicCalendar)
                //    .WithMany(p => p.CalendarEntries)
                //    .HasForeignKey(d => d.AcademicCalendarID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_CalendarEntries_AcadamicCalenders");
            });

            //modelBuilder.Entity<Campaign>(entity =>
            //{
            //    entity.HasOne(d => d.CampaignType)
            //        .WithMany(p => p.Campaigns)
            //        .HasForeignKey(d => d.CampaignTypeID)
            //        .HasConstraintName("FK_Campaigns_CampaignTypes");
            //});

            //modelBuilder.Entity<CampaignEmployeeMap>(entity =>
            //{
            //    entity.HasKey(e => e.CompaignEmployeeMapIID)
            //        .HasName("PK_CompaignEmployeeMaps");

            //    entity.HasOne(d => d.Compaign)
            //        .WithMany(p => p.CampaignEmployeeMaps)
            //        .HasForeignKey(d => d.CompaignID)
            //        .HasConstraintName("FK_CompaignEmployeeMaps_SocailMediaCampaigns");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.CampaignEmployeeMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_CompaignEmployeeMaps_Employees");
            //});

            //modelBuilder.Entity<CampaignStatSummaryMap>(entity =>
            //{
            //    entity.HasOne(d => d.Campaign)
            //        .WithMany(p => p.CampaignStatSummaryMaps)
            //        .HasForeignKey(d => d.CampaignID)
            //        .HasConstraintName("FK_CampaignStatSummaryMaps_SocailMediaCampaigns");

            //    entity.HasOne(d => d.CampaignStatType)
            //        .WithMany(p => p.CampaignStatSummaryMaps)
            //        .HasForeignKey(d => d.CampaignStatTypeID)
            //        .HasConstraintName("FK_CampaignStatSummaryMaps_CampaignStatTypes");
            //});

            //modelBuilder.Entity<CampaignStatType>(entity =>
            //{
            //    entity.Property(e => e.CampaignStatTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CampaignTag>(entity =>
            //{
            //    entity.Property(e => e.CampaignTagIID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CampaignType>(entity =>
            //{
            //    entity.Property(e => e.CampaignTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<CampusTransfers>(entity =>
            {
                entity.HasOne(d => d.FromAcademicYear)
                    .WithMany(p => p.CampusTransferFromAcademicYears)
                    .HasForeignKey(d => d.FromAcademicYearID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_FrmAcademicYears");

                entity.HasOne(d => d.FromClass)
                    .WithMany(p => p.CampusTransferFromClasses)
                    .HasForeignKey(d => d.FromClassID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_FromClass");

                entity.HasOne(d => d.FromSchool)
                    .WithMany(p => p.CampusTransferFromSchools)
                    .HasForeignKey(d => d.FromSchoolID)
                    .HasConstraintName("FK_CampusTransfers_FromSchoolID");

                entity.HasOne(d => d.ToSchool)
                    .WithMany(p => p.CampusTransferToSchools)
                    .HasForeignKey(d => d.ToSchoolID)
                    .HasConstraintName("FK_CampusTransfers_ToSchoolID");

                entity.HasOne(d => d.FromSection)
                    .WithMany(p => p.CampusTransferFromSections)
                    .HasForeignKey(d => d.FromSectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_FromSection");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CampusTransfers)
                    .HasForeignKey(d => d.StudentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_Students");

                entity.HasOne(d => d.ToAcademicYear)
                    .WithMany(p => p.CampusTransferToAcademicYears)
                    .HasForeignKey(d => d.ToAcademicYearID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_ToAcademicYears");

                entity.HasOne(d => d.ToClass)
                    .WithMany(p => p.CampusTransferToClasses)
                    .HasForeignKey(d => d.ToClassID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_ToClass");

                entity.HasOne(d => d.ToSection)
                    .WithMany(p => p.CampusTransferToSections)
                    .HasForeignKey(d => d.ToSectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransfers_ToSection");
            });

            modelBuilder.Entity<CampusTransferFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.CampusTransferFeeTypeMapsIID)
                    .HasName("PK_CampusTransferFeeTypeMapsIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CampusTransfer)
                    .WithMany(p => p.CampusTransferFeeTypeMaps)
                    .HasForeignKey(d => d.CampusTransferID)
                    .HasConstraintName("FK_CampusTransferFeeTypeMaps_CampusTransfer");

                entity.HasOne(d => d.FeeDueFeeTypeMaps)
                    .WithMany(p => p.CampusTransferFeeTypeMaps)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .HasConstraintName("FK_CampusTransferFeeTypeMaps_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.CampusTransferFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_CampusTransferFeeTypeMaps_FeeMasters");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.CampusTransferFeeTypeMaps)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_CampusTransferFeeTypeMaps_FeePeriods");
            });

            modelBuilder.Entity<CampusTransferMonthlySplit>(entity =>
            {
                entity.HasKey(e => e.CampusTransferMonthlySplitIID)
                    .HasName("PK_CampusTransferMonthlySplitIID");

                entity.HasOne(d => d.CampusTransferFeeTypeMaps)
                    .WithMany(p => p.CampusTransferMonthlySplits)
                    .HasForeignKey(d => d.CampusTransferFeeTypeMapsID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampusTransferMonthlySplit_CampusTransferFeeType");

                entity.HasOne(d => d.FeeDueMonthlySplit)
                    .WithMany(p => p.CampusTransferMonthlySplits)
                    .HasForeignKey(d => d.FeeDueMonthlySplitID)
                    .HasConstraintName("FK_CampusTransferMonthlySplit_FeeDueMonthlySplit");
            });

            //modelBuilder.Entity<Candidate>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CandidateStatus)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.CandidateStatusID)
            //        .HasConstraintName("FK_Candidate_CandidateStatus");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_Candidate_Class");

            //    entity.HasOne(d => d.StudentApplication)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.StudentApplicationID)
            //        .HasConstraintName("FK_Candidate_StudentApplication");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_Candidates_Students");
            //});

            //modelBuilder.Entity<CandidateAnswer>(entity =>
            //{
            //    entity.HasOne(d => d.Candidate)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.CandidateID)
            //        .HasConstraintName("FK_CandidateAnswers_Candidate");

            //    entity.HasOne(d => d.CandidateOnlineExamMap)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.CandidateOnlineExamMapID)
            //        .HasConstraintName("FK_CandidateAnswers_CandidateOnlineExamMap");

            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.QuestionID)
            //        .HasConstraintName("FK_CandidateAnswers_Question");

            //    entity.HasOne(d => d.QuestionOptionMap)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.QuestionOptionMapID)
            //        .HasConstraintName("FK_CandidateAnswers_QuestionOptionMap");
            //});

            //modelBuilder.Entity<CandidateAssesment>(entity =>
            //{
            //    entity.HasOne(d => d.AnswerQuestionOptionMap)
            //        .WithMany(p => p.CandidateAssesmentAnswerQuestionOptionMaps)
            //        .HasForeignKey(d => d.AnswerQuestionOptionMapID)
            //        .HasConstraintName("FK_CandidateAssesments_QuestionOptionMaps1");

            //    entity.HasOne(d => d.CandidateOnlinExamMap)
            //        .WithMany(p => p.CandidateAssesments)
            //        .HasForeignKey(d => d.CandidateOnlinExamMapID)
            //        .HasConstraintName("FK_CandidateAssesments_CandidateOnlineExamMaps");

            //    entity.HasOne(d => d.SelectedQuestionOptionMap)
            //        .WithMany(p => p.CandidateAssesmentSelectedQuestionOptionMaps)
            //        .HasForeignKey(d => d.SelectedQuestionOptionMapID)
            //        .HasConstraintName("FK_CandidateAssesments_QuestionOptionMaps");
            //});

            //modelBuilder.Entity<CandidateOnlineExamMap>(entity =>
            //{
            //    entity.HasKey(e => e.CandidateOnlinExamMapIID)
            //        .HasName("PK_CandidateOnlinExamMaps");

            //    entity.HasOne(d => d.Candidate)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.CandidateID)
            //        .HasConstraintName("FK_CandidateOnlinExamMaps_Candidates");

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_CandidateOnlinExamMaps_OnlineExams");

            //    entity.HasOne(d => d.OnlineExamOperationStatus)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.OnlineExamOperationStatusID)
            //        .HasConstraintName("FK_CandidateOnlineExamMaps_OnlineExamOperationStatuses");

            //    entity.HasOne(d => d.OnlineExamStatus)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.OnlineExamStatusID)
            //        .HasConstraintName("FK_CandidateOnlineExamMaps_OnlineExamStatuses");
            //});

            //modelBuilder.Entity<CardType>(entity =>
            //{
            //    entity.Property(e => e.CardTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CartCharge>(entity =>
            //{
            //    entity.Property(e => e.CartChargeID).ValueGeneratedNever();

            //    entity.HasOne(d => d.CartChargeType)
            //        .WithMany(p => p.CartCharges)
            //        .HasForeignKey(d => d.CartChargeTypeID)
            //        .HasConstraintName("FK_CartCharges_CartChargeTypes");
            //});

            //modelBuilder.Entity<CartChargeCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CartChargeID, e.CultureID });
            //});

            //modelBuilder.Entity<CashChangeCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CashChangeID, e.CultureID });

            //    entity.HasOne(d => d.CashChange)
            //        .WithMany(p => p.CashChangeCultureDatas)
            //        .HasForeignKey(d => d.CashChangeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CashChangeCultureDatas_CashChanges");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.CashChangeCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CashChangeCultureDatas_Cultures");
            //});

            //modelBuilder.Entity<CashChanx>(entity =>
            //{
            //    entity.Property(e => e.CashChangeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.HasOne(d => d.Relegion)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.RelegionID)
                    .HasConstraintName("FK_Casts_Relegions");
            });

            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.Categories)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_Category_Class");

            //    entity.HasOne(d => d.SeoMetadata)
            //        .WithMany(p => p.Categories)
            //        .HasForeignKey(d => d.SeoMetadataID)
            //        .HasConstraintName("FK_Categories_SeoMetadatas");
            //});

            //modelBuilder.Entity<CategoryCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.CategoryID });

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.CategoryCultureDatas)
            //        .HasForeignKey(d => d.CategoryID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CategoryCultureDatas_Categories");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.CategoryCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CategoryCultureDatas_Cultures");
            //});

            modelBuilder.Entity<CategoryFeeMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<CategoryImageMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.CategoryImageMaps)
            //        .HasForeignKey(d => d.CategoryID)
            //        .HasConstraintName("FK_CategoryImageMaps_Categories");

            //    entity.HasOne(d => d.ImageType)
            //        .WithMany(p => p.CategoryImageMaps)
            //        .HasForeignKey(d => d.ImageTypeID)
            //        .HasConstraintName("FK_CategoryImageMaps_ImageTypes");
            //});

            //modelBuilder.Entity<CategoryPageBoilerPlatMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PageBoilerplateMap)
            //        .WithMany(p => p.CategoryPageBoilerPlatMaps)
            //        .HasForeignKey(d => d.PageBoilerplateMapID)
            //        .HasConstraintName("FK_CategoryPageBoilerPlatMaps_PageBoilerplateMaps");
            //});

            //modelBuilder.Entity<CategorySetting>(entity =>
            //{
            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.CategorySettings)
            //        .HasForeignKey(d => d.CategoryID)
            //        .HasConstraintName("FK_CategorySettings_Categories");

            //    entity.HasOne(d => d.UIControlType)
            //        .WithMany(p => p.CategorySettings)
            //        .HasForeignKey(d => d.UIControlTypeID)
            //        .HasConstraintName("FK_CategorySettings_UIControlTypes");
            //});

            //modelBuilder.Entity<CategoryTag>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.CategoryTags)
            //        .HasForeignKey(d => d.CategoryID)
            //        .HasConstraintName("FK_CategoryTags_Categories");
            //});

            //modelBuilder.Entity<CategoryTagMap>(entity =>
            //{
            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.CategoryTagMaps)
            //        .HasForeignKey(d => d.CategoryID)
            //        .HasConstraintName("FK_CategoryTagMaps_Categories");

            //    entity.HasOne(d => d.CategoryTag)
            //        .WithMany(p => p.CategoryTagMaps)
            //        .HasForeignKey(d => d.CategoryTagID)
            //        .HasConstraintName("FK_CategoryTagMaps_CategoryTags");
            //});

            //modelBuilder.Entity<CategoryTree>(entity =>
            //{
            //    entity.ToView("CategoryTree", "catalog");
            //});

            //modelBuilder.Entity<CategoryTree1>(entity =>
            //{
            //    entity.HasKey(e => e.CategoryIID)
            //        .HasName("PK__Category__68320745B6A3AF09");

            //    entity.Property(e => e.CategoryIID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Catogory>(entity =>
            {
                entity.HasKey(e => e.CategoryID)
                    .HasName("PK_Category");
            });

            //modelBuilder.Entity<CertificateLog>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CertificateTemplateI)
            //        .WithMany(p => p.CertificateLogs)
            //        .HasForeignKey(d => d.CertificateTemplateIID)
            //        .HasConstraintName("FK_CertificateTemplates_CertificateTemplateIID");
            //});

            //modelBuilder.Entity<CertificateTemplate>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<CertificateTemplateParameter>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ChannelType>(entity =>
            {
                entity.Property(e => e.ChannelTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<ChartMetadata>(entity =>
            //{
            //    entity.Property(e => e.ChartMetadataID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<ChartOfAccount>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ChartOfAccountMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.ChartOfAccountMaps)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_ChartOfAccountMaps_Accounts");

            //    entity.HasOne(d => d.ChartOfAccount)
            //        .WithMany(p => p.ChartOfAccountMaps)
            //        .HasForeignKey(d => d.ChartOfAccountID)
            //        .HasConstraintName("FK_ChartOfAccountMaps_ChartOfAccounts");

            //    entity.HasOne(d => d.ChartRowType)
            //        .WithMany(p => p.ChartOfAccountMaps)
            //        .HasForeignKey(d => d.ChartRowTypeID)
            //        .HasConstraintName("FK_ChartOfAccountMaps_ChartRowTypes");
            //});

            //modelBuilder.Entity<ChartRowType>(entity =>
            //{
            //    entity.Property(e => e.ChartRowTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Circular>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Circulars)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_Circulars_AcademicYears");

                entity.HasOne(d => d.CircularPriority)
                    .WithMany(p => p.Circulars)
                    .HasForeignKey(d => d.CircularPriorityID)
                    .HasConstraintName("FK_Circulars_CircularPriorities");

                entity.HasOne(d => d.CircularStatus)
                    .WithMany(p => p.Circulars)
                    .HasForeignKey(d => d.CircularStatusID)
                    .HasConstraintName("FK_Circulars_CircularStatuses");

                entity.HasOne(d => d.CircularType)
                    .WithMany(p => p.Circulars)
                    .HasForeignKey(d => d.CircularTypeID)
                    .HasConstraintName("FK_Circulars_CirculateTypes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Circulars)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Circulars_Circulars");
            });

            modelBuilder.Entity<CircularAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Circular)
                    .WithMany(p => p.CircularAttachmentMaps)
                    .HasForeignKey(d => d.CircularID)
                    .HasConstraintName("FK_CircularAttachmentMaps_Circular");
            });

            modelBuilder.Entity<CircularMap>(entity =>
            {
                entity.HasOne(d => d.Circular)
                    .WithMany(p => p.CircularMaps)
                    .HasForeignKey(d => d.CircularID)
                    .HasConstraintName("FK_CircularMaps_Circulars");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.CircularMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_CircularMaps_Classes");

                entity.HasOne(d => d.Departments1)
                    .WithMany(p => p.CircularMaps)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_CircularMaps_Departments");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.CircularMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_CircularMaps_Sections");
            });

            modelBuilder.Entity<CircularType>(entity =>
            {
                entity.HasKey(e => e.CirculateTypeID)
                    .HasName("PK_CirculateTypes");
            });

            modelBuilder.Entity<CircularUserTypeMap>(entity =>
            {
                entity.HasOne(d => d.Circular)
                    .WithMany(p => p.CircularUserTypeMaps)
                    .HasForeignKey(d => d.CircularID)
                    .HasConstraintName("FK_CircularUserTypeMaps_Circulars");

                entity.HasOne(d => d.CircularUserType)
                    .WithMany(p => p.CircularUserTypeMaps)
                    .HasForeignKey(d => d.CircularUserTypeID)
                    .HasConstraintName("FK_CircularUserTypeMaps_CircularUserTypes");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassID).ValueGeneratedNever();

                entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Classes_AcademicYear");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_Classes_CostCenters");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Classes_Schools");
            });

            modelBuilder.Entity<ClassAssociateTeacherMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassClassTeacherMap)
                    .WithMany(p => p.ClassAssociateTeacherMaps)
                    .HasForeignKey(d => d.ClassClassTeacherMapID)
                    .HasConstraintName("FK_ClassssociateClassTeacherMap");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassAssociateTeacherMaps)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_ClassAssociateTeacherMaps_Employees1");
            });

            modelBuilder.Entity<ClassClassGroupMap>(entity =>
            {
                entity.HasKey(e => e.ClassClassGroupMapIID)
                    .HasName("PK_[ClassClassGroupMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.ClassClassGroupMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_ClassGrpMaps_AcademicYearID");

                entity.HasOne(d => d.ClassGroup)
                    .WithMany(p => p.ClassClassGroupMaps)
                    .HasForeignKey(d => d.ClassGroupID)
                    .HasConstraintName("FK_ClassClassGroupMaps_ClassGrps");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassClassGroupMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassClassGroupMaps_class");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.ClassClassGroupMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_ClassClassGroupMaps_School");
            });

            modelBuilder.Entity<ClassClassTeacherMap>(entity =>
            {
                entity.HasKey(e => e.ClassClassTeacherMapIID)
                    .HasName("PK_ClassClassTeacherMapss");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassClassTeacherMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassClassTeacherMaps_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassClassTeacherMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassClassTeacherMaps_Class");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassClassTeacherMaps)
                    .HasForeignKey(d => d.CoordinatorID)
                    .HasConstraintName("FK_ClassClassTeacherMaps_Coordinator");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassClassTeacherMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassClassTeacherMaps_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassClassTeacherMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassClassTeacherMaps_Section");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.ClassClassTeacherMaps)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_ClassClassTeacherMaps_Parent");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.ClassClassTeacherMaps1)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_ClassClassTeacherMaps_Employees");
            });

            modelBuilder.Entity<ClassCoordinator>(entity =>
            {
                entity.Property(e => e.ISACTIVE).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.ClassCoordinators)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_ClassCoordinator_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassCoordinators)
                    .HasForeignKey(d => d.CoordinatorID)
                    .HasConstraintName("FK_ClassCoordinator_Coordinator");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.HeadMasters)
                    .HasForeignKey(d => d.HeadMasterID)
                    .HasConstraintName("FK_ClassCoordinator_HeadMaster");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.ClassCoordinators)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_ClassCoordinator_School");
            });

            modelBuilder.Entity<ClassCoordinatorClassMap>(entity =>
            {
                entity.HasOne(d => d.ClassCoordinator)
                    .WithMany(p => p.ClassCoordinatorClassMaps)
                    .HasForeignKey(d => d.ClassCoordinatorID)
                    .HasConstraintName("FK_ClassCoordinatorClassMaps_Coordinators");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassCoordinatorClassMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassCoordinatorClassMaps_Classes");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassCoordinatorClassMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassCoordinatorClassMaps_Sections");
            });

            modelBuilder.Entity<ClassCoordinatorMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.ClassCoordinatorMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_ClassCoordinatorMap_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.ClassCoordinatorMaps)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_ClassCoordinatorMap_Class");

                //entity.HasOne(d => d.Coordinator)
                //    .WithMany(p => p.ClassCoordinatorMaps)
                //    .HasForeignKey(d => d.CoordinatorID)
                //    .HasConstraintName("FK_ClassCoordinatorMap_Coordinator");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.ClassCoordinatorMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_ClassCoordinatorMap_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassCoordinatorMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassCoordinatorMap_Section");
            });

            //modelBuilder.Entity<ClassCoordinatorsView>(entity =>
            //{
            //    entity.ToView("ClassCoordinatorsViews", "schools");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<ClassFeeMaster>(entity =>
            {
                entity.HasKey(e => e.ClassFeeMasterIID)
                    .HasName("PK_FeeAssigns");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassFeeMasters)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_FeeAssigns_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassFeeMasters)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_FeeAssigns_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassFeeMasters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassFeeMasters_School");
            });

            modelBuilder.Entity<ClassFeeStructureMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassFeeStructureMaps)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_ClassFeeStructureMaps_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassFeeStructureMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassFeeStructureMaps_Classes");

                entity.HasOne(d => d.FeeStructure)
                    .WithMany(p => p.ClassFeeStructureMaps)
                    .HasForeignKey(d => d.FeeStructureID)
                    .HasConstraintName("FK_ClassFeeStructureMaps_FeeStructures");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassFeeStructureMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassFeeStructureMaps_School");
            });

            modelBuilder.Entity<ClassGroup>(entity =>
            {
                entity.HasKey(e => e.ClassGroupID)
                    .IsClustered(false);

                entity.Property(e => e.ClassGroupID).ValueGeneratedNever();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassGroups)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassGroups_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassGroups)
                    .HasForeignKey(d => d.HeadTeacherID)
                    .HasConstraintName("FK_ClassGroups_HeadTeacher");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassGroups)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassGroups_School");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassGroups)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_ClassGroups_SubjectMap");
            });

            modelBuilder.Entity<ClassGroupTeacherMap>(entity =>
            {
                //entity.Property(e => e.IsHeadTeacher).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassGroup)
                    .WithMany(p => p.ClassGroupTeacherMaps)
                    .HasForeignKey(d => d.ClassGroupID)
                    .HasConstraintName("FK_ClassGrpTeacherMaps_ClassGrps");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassGroupTeacherMaps)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_ClassGroupTeacherMaps_Teacher");
            });

            modelBuilder.Entity<ClassSectionMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassSectionMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassSectionMaps_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassSectionMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassSectionMaps_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassSectionMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassSectionMaps_Schools");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassSectionMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassSectionMaps_Sections");
            });


            modelBuilder.Entity<ClassSubjectMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassSubjectMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassSubjectMaps_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassSubjectMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassSubjectMaps_Classes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassSubjectMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_ClassSubjectMaps_Employees");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassSubjectMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassSubjectMaps_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassSubjectMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassSubjectMaps_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_ClassSubjectMaps_Subjects");

                entity.Property(e => e.WeekPeriods).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ClassSubjectSkillGroupMap>(entity =>
            {
                entity.Property(e => e.ClassSubjectSkillGroupMapID).ValueGeneratedNever();

                entity.Property(e => e.ISScholastic).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassSubjectSkillGroupMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupMaps_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassSubjectSkillGroupMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupMaps_Classes");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ClassSubjectSkillGroupMaps)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupMaps_Exams");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.ClassSubjectSkillGroupMaps)
                    .HasForeignKey(d => d.MarkGradeID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupMaps_MarkGrades");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassSubjectSkillGroupMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupMaps_School");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSubjectSkillGroupMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupMaps_Subject");
            });

            modelBuilder.Entity<ClassSubjectSkillGroupSkillMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassSubjectSkillGroupMap)
                    .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
                    .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_ClassSubjectSkillGroupMaps");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
                    .HasForeignKey(d => d.MarkGradeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_MarkGrades");

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_SkillGroups");

                entity.HasOne(d => d.SkillMaster)
                    .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
                    .HasForeignKey(d => d.SkillMasterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_SkillMaster");
            });

            modelBuilder.Entity<ClassSubjectWorkflowEntityMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassSubjectMap)
                    .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
                    .HasForeignKey(d => d.ClassSubjectMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassSubjectMaps_ClassSubjectWorkflowEntity");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_ClassSubjectWorkflowEntityMaps_Subject");

                entity.HasOne(d => d.WorkflowEntity)
                    .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
                    .HasForeignKey(d => d.WorkflowEntityID)
                    .HasConstraintName("FK_ClassSubjectWorkflowEntity_WorkflowEntity");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
                    .HasForeignKey(d => d.workflowID)
                    .HasConstraintName("FK_ClassSubjectWorkflowEntity_Workflow");
            });

            modelBuilder.Entity<ClassTeacherMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassTeacherMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassTeacherMaps_AcademicYear");

                entity.HasOne(d => d.ClassClassTeacherMap)
                    .WithMany(p => p.ClassTeacherMaps)
                    .HasForeignKey(d => d.ClassClassTeacherMapID)
                    .HasConstraintName("FK_ClassGroupTeacherMaps_ClassTeacherMap");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassTeacherMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassTeacherMaps_Classes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassTeacherMaps)
                    .HasForeignKey(d => d.ClassTeacherID)
                    .HasConstraintName("FK_ClassTeacherMaps_ClassTeacher");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.ClassTeacherMaps1)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_ClassTeacherMaps_Employees");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ClassTeacherMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassTeacherMaps_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassTeacherMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassTeacherMaps_Sections");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.ClassTeacherMaps)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_OtherTeacherMaps_Subject");

                entity.HasOne(d => d.Employee2)
                    .WithMany(p => p.ClassTeacherMaps2)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_ClassTeacherMaps_Employees1");
            });

            modelBuilder.Entity<ClassTiming>(entity =>
            {
                entity.Property(e => e.ClassTimingID).ValueGeneratedNever();

                entity.Property(e => e.IsBreakTime).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassTimings)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassTimings_AcademicYear");

                entity.HasOne(d => d.BreakType)
                    .WithMany(p => p.ClassTimings)
                    .HasForeignKey(d => d.BreakTypeID)
                    .HasConstraintName("FK_ClassTimings_BreakTypes");

                entity.HasOne(d => d.ClassTimingSet)
                    .WithMany(p => p.ClassTimings)
                    .HasForeignKey(d => d.ClassTimingSetID)
                    .HasConstraintName("FK_ClassTimings_ClassTimingSets");

                entity.HasOne(d => d.Schools)
                    .WithMany(p => p.ClassTimings)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ClassTimings_School");
            });

            modelBuilder.Entity<ClassWorkFlowMap>(entity =>
            {
                entity.HasKey(e => e.ClassWorkFlowIID)
                    .HasName("PK_ClassWorkFlowMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassWorkFlowMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassWorkFlowMaps_Class");

                entity.HasOne(d => d.WorkflowEntity)
                    .WithMany(p => p.ClassWorkFlowMaps)
                    .HasForeignKey(d => d.WorkflowEntityID)
                    .HasConstraintName("FK_ClassWorkFlowMaps_WorkflowEntity");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.ClassWorkFlowMaps)
                    .HasForeignKey(d => d.WorkflowID)
                    .HasConstraintName("FK_ClassWorkFlowMaps_Workflow");
            });

            //modelBuilder.Entity<Comment>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EntityType)
            //        .WithMany(p => p.Comments)
            //        .HasForeignKey(d => d.EntityTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Comments_EntityType");

            //    entity.HasOne(d => d.ParentComment)
            //        .WithMany(p => p.InverseParentComment)
            //        .HasForeignKey(d => d.ParentCommentID)
            //        .HasConstraintName("FK_Comments_Comments");
            //});

            //modelBuilder.Entity<Communication>(entity =>
            //{
            //    entity.HasKey(e => e.CommunicationIID)
            //        .HasName("PK_LeadCommunications");

            //    entity.HasOne(d => d.CommunicationType)
            //        .WithMany(p => p.Communications)
            //        .HasForeignKey(d => d.CommunicationTypeID)
            //        .HasConstraintName("FK_LeadCommunications_CommunicationTypes");

            //    entity.HasOne(d => d.EmailTemplate)
            //        .WithMany(p => p.Communications)
            //        .HasForeignKey(d => d.EmailTemplateID)
            //        .HasConstraintName("FK_LeadCommunications_EmailTemplates");

            //    entity.HasOne(d => d.Lead)
            //        .WithMany(p => p.Communications)
            //        .HasForeignKey(d => d.LeadID)
            //        .HasConstraintName("FK_Communications_Leads");
            //});

            //modelBuilder.Entity<CommunicationLog>(entity =>
            //{
            //    entity.HasOne(d => d.CommunicationStatus)
            //        .WithMany(p => p.CommunicationLogs)
            //        .HasForeignKey(d => d.CommunicationStatusID)
            //        .HasConstraintName("FK_CommunicationLogs_CommunicationStatuses");

            //    entity.HasOne(d => d.CommunicationType)
            //        .WithMany(p => p.CommunicationLogs)
            //        .HasForeignKey(d => d.CommunicationTypeID)
            //        .HasConstraintName("FK_CommunicationLogs_CommunicationTypes");

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.CommunicationLogs)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_CommunicationLogs_Logins");
            //});

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Currency)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.BaseCurrencyID)
                //    .HasConstraintName("FK_Companies_Currencies");

                //entity.HasOne(d => d.CompanyGroup)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.CompanyGroupID)
                //    .HasConstraintName("FK_Companies_CompanyGroups");

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.CountryID)
                //    .HasConstraintName("FK_Companies_Companies");

                //entity.HasOne(d => d.CompanyStatuses)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Companies_CompanyStatuses");
            });

            //modelBuilder.Entity<CompanyCurrencyMap>(entity =>
            //{
            //    entity.HasKey(e => new { e.CompanyID, e.CurrencyID });

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.CompanyCurrencyMaps)
            //        .HasForeignKey(d => d.CompanyID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CompanyCurrencyMaps_Companies");

            //    entity.HasOne(d => d.Currency)
            //        .WithMany(p => p.CompanyCurrencyMaps)
            //        .HasForeignKey(d => d.CurrencyID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CompanyCurrencyMaps_Currencies");
            //});

            modelBuilder.Entity<CompanyGroup>(entity =>
            {
                entity.Property(e => e.CompanyGroupID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Company_FiscalYear_Close>(entity =>
            //{
            //    entity.HasKey(e => e.CFC_ID)
            //        .IsClustered(false);
            //});

            modelBuilder.Entity<Complain>(entity =>
            {
                entity.Property(e => e.ComplainIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<Contact>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<ContentFile>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.ContentFiles)
                    .HasForeignKey(d => d.ContentTypeID)
                    .HasConstraintName("FK_ContentFiles_ContentTypes");

            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.Property(e => e.ContentTypeID).ValueGeneratedNever();
            });


            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.Property(e => e.CostCenterID).ValueGeneratedNever();

                entity.Property(e => e.IsAffect_A).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsAffect_C).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsAffect_E).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAffect_I).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAffect_L).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsFixed).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.CostCenters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_CostCenters_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.CostCenters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_CostCenters_School");
            });

            modelBuilder.Entity<CostCenterAccountMap>(entity =>
            {
                entity.Property(e => e.CostCenterAccountMapIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.CostCenterAccountMaps)
                    .HasForeignKey(d => d.CostCenterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostCentersAccountMaps_CostCenter");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryID).ValueGeneratedNever();

                //entity.HasOne(d => d.Currency)
                //    .WithMany(p => p.Countries)
                //    .HasForeignKey(d => d.CurrencyID)
                //    .HasConstraintName("FK_Countries_Currencies");
            });

            modelBuilder.Entity<CreditNoteFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.CreditNoteFeeTypeMapIID)
                    .HasName("PK_CreditNoteFeeTypeMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_AcademicYear");

                entity.HasOne(d => d.FeeDueFeeTypeMap)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.FeeDueMonthlySplit)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.FeeDueMonthlySplitID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeeDueMonthlySplits");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeeMasters");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.PeriodID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeePeriods");

                entity.HasOne(d => d.SchoolCreditNote)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.SchoolCreditNoteID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_SchoolCreditNote");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.CreditNoteFeeTypeMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_CreditNoteFeeTypeMaps_School");
            });

            //modelBuilder.Entity<Currency>(entity =>
            //{
            //    entity.Property(e => e.CurrencyID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.Currencies)
            //        .HasForeignKey(d => d.CompanyID)
            //        .HasConstraintName("FK_Currencies_Companies");
            //});

            //modelBuilder.Entity<Customer>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DefaultStudent)
            //        .WithMany(p => p.Customers)
            //        .HasForeignKey(d => d.DefaultStudentID)
            //        .HasConstraintName("FK_customers_Students");

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.Customers)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_Customers_Logins");
            //});

            //modelBuilder.Entity<CustomerAccountMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.CustomerAccountMaps)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_CustomerAccountMaps_Accounts");

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.CustomerAccountMaps)
            //        .HasForeignKey(d => d.CustomerID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CustomerAccountMaps_Customers");

            //    entity.HasOne(d => d.Entitlement)
            //        .WithMany(p => p.CustomerAccountMaps)
            //        .HasForeignKey(d => d.EntitlementID)
            //        .HasConstraintName("FK_CustomerAccountMaps_EntityTypeEntitlements");
            //});

            //modelBuilder.Entity<CustomerCard>(entity =>
            //{
            //    entity.HasOne(d => d.CardType)
            //        .WithMany(p => p.CustomerCards)
            //        .HasForeignKey(d => d.CardTypeID)
            //        .HasConstraintName("FK_CustomerCards_CardTypes");

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.CustomerCards)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_CustomerCards_Customers");

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.CustomerCards)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_CustomerCards_Logins");
            //});

            modelBuilder.Entity<CustomerFeedBacks>(entity =>
            {
                entity.HasOne(d => d.CustomerFeedbackType)
                    .WithMany(p => p.CustomerFeedBacks)
                    .HasForeignKey(d => d.CustomerFeedbackTypeID)
                    .HasConstraintName("FK_CustomerFeedBacks_CustomerFeedbackTypes");
            });


            //modelBuilder.Entity<CustomerGroup>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<CustomerGroupDeliveryTypeMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CustomerGroup)
            //        .WithMany(p => p.CustomerGroupDeliveryTypeMaps)
            //        .HasForeignKey(d => d.CustomerGroupID)
            //        .HasConstraintName("FK_CustomerGroupDeliveryTypeMaps_CustomerGroups");

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.CustomerGroupDeliveryTypeMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_CustomerGroupDeliveryTypeMaps_DeliveryTypes");
            //});

            //modelBuilder.Entity<CustomerGroupLoginMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<CustomerJustAsk>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.CustomerJustAsks)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CustomerJustAsk_Cultures");
            //});

            //modelBuilder.Entity<CustomerProductReference>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.CustomerProductReferences)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_CustomerProductReferences_Customers");

            //    entity.HasOne(d => d.ProductSKUMap)
            //        .WithMany(p => p.CustomerProductReferences)
            //        .HasForeignKey(d => d.ProductSKUMapID)
            //        .HasConstraintName("FK_CustomerProductReferences_ProductSKUMaps");
            //});

            //modelBuilder.Entity<CustomerSetting>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.CustomerSettings)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_CustomerSettings_Customers");
            //});

            //modelBuilder.Entity<CustomerSupplierMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.CustomerSupplierMaps)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_CustomerSupplierMaps_Customers");

            //    entity.HasOne(d => d.Supplier)
            //        .WithMany(p => p.CustomerSupplierMaps)
            //        .HasForeignKey(d => d.SupplierID)
            //        .HasConstraintName("FK_CustomerSupplierMap_Suppliers");
            //});

            //modelBuilder.Entity<CustomerSupportTicket>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.CustomerSupportTickets)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CustomerSupportTicket_Cultures");
            //});

            //modelBuilder.Entity<DataFeedLog>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DataFeedStatus)
            //        .WithMany(p => p.DataFeedLogs)
            //        .HasForeignKey(d => d.DataFeedStatusID)
            //        .HasConstraintName("FK_DataFeedLogs_DataFeedStatuses");

            //    entity.HasOne(d => d.DataFeedType)
            //        .WithMany(p => p.DataFeedLogs)
            //        .HasForeignKey(d => d.DataFeedTypeID)
            //        .HasConstraintName("FK_DataFeedLogs_DataFeedTypes");
            //});

            //modelBuilder.Entity<DataFeedStatus>(entity =>
            //{
            //    entity.Property(e => e.DataFeedStatusID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<DataFeedTable>(entity =>
            //{
            //    entity.Property(e => e.DataFeedTableID).ValueGeneratedNever();

            //    entity.HasOne(d => d.DataFeedType)
            //        .WithMany(p => p.DataFeedTables)
            //        .HasForeignKey(d => d.DataFeedTypeID)
            //        .HasConstraintName("FK_DataFeedTables_DataFeedTypes");
            //});

            //modelBuilder.Entity<DataFeedTableColumn>(entity =>
            //{
            //    entity.Property(e => e.DataFeedTableColumnID).ValueGeneratedNever();

            //    entity.HasOne(d => d.DataFeedTable)
            //        .WithMany(p => p.DataFeedTableColumns)
            //        .HasForeignKey(d => d.DataFeedTableID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DataFeedTableColumns_DataFeedTables");
            //});

            //modelBuilder.Entity<DataFeedType>(entity =>
            //{
            //    entity.Property(e => e.DataFeedTypeID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Operation)
            //        .WithMany(p => p.DataFeedTypes)
            //        .HasForeignKey(d => d.OperationID)
            //        .HasConstraintName("FK_DataFeedTypes_DataFeedOperations");
            //});

            //modelBuilder.Entity<DataFormat>(entity =>
            //{
            //    entity.Property(e => e.DataFormatID).ValueGeneratedNever();

            //    entity.HasOne(d => d.DataFormatType)
            //        .WithMany(p => p.DataFormats)
            //        .HasForeignKey(d => d.DataFormatTypeID)
            //        .HasConstraintName("FK_DataFormats_DataFormatTypes");
            //});

            //modelBuilder.Entity<DataFormatType>(entity =>
            //{
            //    entity.Property(e => e.DataFormatTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<DataHistoryEntity>(entity =>
            //{
            //    entity.Property(e => e.DataHistoryEntityID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<DeliveryCharge>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CountryGroup)
            //        .WithMany(p => p.DeliveryCharges)
            //        .HasForeignKey(d => d.CountryGroupID)
            //        .HasConstraintName("FK_DeliveryCharges_ServiceProviderCountryGroups");

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.DeliveryCharges)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DeliveryCharges_DeliveryTypes");

            //    entity.HasOne(d => d.FromCountry)
            //        .WithMany(p => p.DeliveryChargeFromCountries)
            //        .HasForeignKey(d => d.FromCountryID)
            //        .HasConstraintName("FK_DeliveryCharges_Countries1");

            //    entity.HasOne(d => d.ServiceProvider)
            //        .WithMany(p => p.DeliveryCharges)
            //        .HasForeignKey(d => d.ServiceProviderID)
            //        .HasConstraintName("FK_DeliveryCharges_ServiceProviders");

            //    entity.HasOne(d => d.ToCountry)
            //        .WithMany(p => p.DeliveryChargeToCountries)
            //        .HasForeignKey(d => d.ToCountryID)
            //        .HasConstraintName("FK_DeliveryCharges_Countries11");
            //});

            //modelBuilder.Entity<DeliveryTimeSlot>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<DeliveryTimeSlotBranchMap>(entity =>
            //{
            //    entity.Property(e => e.DeliveryTimeSlotBranchMapID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.DeliveryTimeSlotBranchMaps)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_DeliveryTimeSlotBranchMaps_DeliveryTimeSlotBranchMaps");

            //    entity.HasOne(d => d.DeliveryTimeSlot)
            //        .WithMany(p => p.DeliveryTimeSlotBranchMaps)
            //        .HasForeignKey(d => d.DeliveryTimeSlotID)
            //        .HasConstraintName("FK_DeliveryTimeSlotBranchMaps_DeliveryTimeSlots");
            //});

            //modelBuilder.Entity<DeliveryType>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<DeliveryType1>(entity =>
            //{
            //    entity.HasKey(e => e.DeliveryTypeID)
            //        .HasName("PK_DeliveryTypes_1");

            //    entity.Property(e => e.DeliveryTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Status)
            //        .WithMany(p => p.DeliveryType1)
            //        .HasForeignKey(d => d.StatusID)
            //        .HasConstraintName("FK_DeliveryTypes_DeliveryTypeStatuses");
            //});

            //modelBuilder.Entity<DeliveryTypeAllowedAreaMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Area)
            //        .WithMany(p => p.DeliveryTypeAllowedAreaMaps)
            //        .HasForeignKey(d => d.AreaID)
            //        .HasConstraintName("FK_DeliveryTypeAllowedAreaMaps_Areas");

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.DeliveryTypeAllowedAreaMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DeliveryTypeAllowedAreaMaps_DeliveryTypes");
            //});

            //modelBuilder.Entity<DeliveryTypeAllowedCountryMap>(entity =>
            //{
            //    entity.HasKey(e => new { e.DeliveryTypeID, e.FromCountryID, e.ToCountryID })
            //        .HasName("PK_DeliveryTypeAllowedCountryMaps_1");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DeliveryTypes1)
            //        .WithMany(p => p.DeliveryTypeAllowedCountryMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeAllowedCountryMaps_DeliveryTypes");

            //    entity.HasOne(d => d.Country)
            //        .WithMany(p => p.DeliveryTypeAllowedCountryMaps)
            //        .HasForeignKey(d => d.FromCountryID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeAllowedCountryMaps_Countries1");

            //    entity.HasOne(d => d.Country1)
            //        .WithMany(p => p.DeliveryTypeAllowedCountryMaps1)
            //        .HasForeignKey(d => d.ToCountryID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeAllowedCountryMaps_Countries11");
            //});

            //modelBuilder.Entity<DeliveryTypeAllowedZoneMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DeliveryTypes1)
            //        .WithMany(p => p.DeliveryTypeAllowedZoneMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DelivertyTypeAllowedZoneMaps_DeliveryTypes");

            //    entity.HasOne(d => d.Zone)
            //        .WithMany(p => p.DeliveryTypeAllowedZoneMaps)
            //        .HasForeignKey(d => d.ZoneID)
            //        .HasConstraintName("FK_DelivertyTypeAllowedZoneMaps_Zones");
            //});

            //modelBuilder.Entity<DeliveryTypeCategoryMaster>(entity =>
            //{
            //    entity.HasOne(d => d.RefDeliveryType)
            //        .WithMany()
            //        .HasForeignKey(d => d.RefDeliveryTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeCategoryMaster_DeliveryTypeMaster");
            //});

            //modelBuilder.Entity<DeliveryTypeCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.DeliveryTypeID, e.CultureID });

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.DeliveryTypeCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeCultureDatas_Cultures");

            //    entity.HasOne(d => d.DeliveryTypes1)
            //        .WithMany(p => p.DeliveryTypeCultureDatas)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeCultureDatas_DeliveryTypes");
            //});

            //modelBuilder.Entity<DeliveryTypeCutOffSlot>(entity =>
            //{
            //    entity.Property(e => e.DeliveryTypeCutOffSlotID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.DeliveryTypeCutOffSlots)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DeliveryTypeCutOffSlots_DeliveryTypes");

            //    entity.HasOne(d => d.TimeSlot)
            //        .WithMany(p => p.DeliveryTypeCutOffSlots)
            //        .HasForeignKey(d => d.TimeSlotID)
            //        .HasConstraintName("FK_DeliveryTypeCutOffSlots_DeliveryTimeSlots");
            //});

            //modelBuilder.Entity<DeliveryTypeCutOffSlotCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.DeliveryTypeCutOffSlotID, e.CultureID });

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.DeliveryTypeCutOffSlotCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeCutOffSlotCultureDatas_Cultures");

            //    entity.HasOne(d => d.DeliveryTypeCutOffSlot)
            //        .WithMany(p => p.DeliveryTypeCutOffSlotCultureDatas)
            //        .HasForeignKey(d => d.DeliveryTypeCutOffSlotID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeCutOffSlotCultureDatas_DeliveryTypeCutOffSlots");
            //});

            //modelBuilder.Entity<DeliveryTypeGeoMap>(entity =>
            //{
            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.DeliveryTypeGeoMaps)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_DeliveryTypeGeoMaps_Branches");

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.DeliveryTypeGeoMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DeliveryTypeGeoMaps_DeliveryTypeGeoMaps");
            //});

            //modelBuilder.Entity<DeliveryTypeTimeSlotMap>(entity =>
            //{
            //    entity.HasKey(e => e.DeliveryTypeTimeSlotMapIID)
            //        .HasName("PK_DeliveryTypeTimeSlotMaps_1");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.DeliveryTypeTimeSlotMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DeliveryTypeTimeSlotMaps_DeliveryTypes");
            //});

            //modelBuilder.Entity<DeliveryTypeTimeSlotMapsCulture>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.DeliveryTypeTimeSlotMapID });

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.DeliveryTypeTimeSlotMapsCultures)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeTimeSlotMapsCulture_Cultures");

            //    entity.HasOne(d => d.DeliveryTypeTimeSlotMap)
            //        .WithMany(p => p.DeliveryTypeTimeSlotMapsCultures)
            //        .HasForeignKey(d => d.DeliveryTypeTimeSlotMapID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeTimeSlotMapsCulture_DeliveryTypeTimeSlotMaps");
            //});

            //modelBuilder.Entity<Department>(entity =>
            //{
            //    entity.Property(e => e.DepartmentID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Departments1>(entity =>
            {
                entity.Property(e => e.DepartmentID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();


                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Departments1)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Departments_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Department1)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Departments_School");

                entity.HasOne(d => d.DepartmentStatus)
                .WithMany(p => p.Departments1)
                .HasForeignKey(d => d.StatusID);

            });


            //modelBuilder.Entity<DepartmentCostCenterMap>(entity =>
            //{
            //    entity.Property(e => e.DepartmentCostCenterMapIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CostCenter)
            //        .WithMany()
            //        .HasForeignKey(d => d.CostCenterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DepartmentCostCenterMap_CostCenter");

            //    entity.HasOne(d => d.Department)
            //        .WithMany()
            //        .HasForeignKey(d => d.DepartmentID)
            //        .HasConstraintName("FK_DepartmentCostCenterMap_Department");
            //});

            //modelBuilder.Entity<DepartmentTag>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Department)
            //        .WithMany(p => p.DepartmentTags)
            //        .HasForeignKey(d => d.DepartmentID)
            //        .HasConstraintName("FK_DepartmentTags_Departments");
            //});

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.DesignationID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<Despatch>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<DocFileType>(entity =>
            //{
            //    entity.Property(e => e.DocFileTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<DocumentFile>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DocFileType)
            //        .WithMany(p => p.DocumentFiles)
            //        .HasForeignKey(d => d.DocFileTypeID)
            //        .HasConstraintName("FK_DocumentFiles_DocFileTypes");

            //    entity.HasOne(d => d.DocumentStatus)
            //        .WithMany(p => p.DocumentFiles)
            //        .HasForeignKey(d => d.DocumentStatusID)
            //        .HasConstraintName("FK_DocumentFiles_DocumentFileStatuses");

            //    entity.HasOne(d => d.OwnerEmployee)
            //        .WithMany(p => p.DocumentFiles)
            //        .HasForeignKey(d => d.OwnerEmployeeID)
            //        .HasConstraintName("FK_DocumentFiles_Employees");
            //});

            //modelBuilder.Entity<DocumentFileStatus>(entity =>
            //{
            //    entity.Property(e => e.DocumentStatusID).ValueGeneratedNever();

            //    entity.Property(e => e.StatusName).IsFixedLength();
            //});

            modelBuilder.Entity<DocumentReferenceStatusMap>(entity =>
            {
                entity.Property(e => e.DocumentReferenceStatusMapID).ValueGeneratedNever();

                //entity.HasOne(d => d.DocumentStatus)
                //    .WithMany(p => p.DocumentReferenceStatusMaps)
                //    .HasForeignKey(d => d.DocumentStatusID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_DocumentReferenceStatusMap_DocumentStatuses");

                //entity.HasOne(d => d.ReferenceType)
                //    .WithMany(p => p.DocumentReferenceStatusMaps)
                //    .HasForeignKey(d => d.ReferenceTypeID)
                //    .HasConstraintName("FK_DocumentReferenceStatusMap_DocumentReferenceTypes");
            });

            //modelBuilder.Entity<DocumentReferenceTicketStatusMap>(entity =>
            //{
            //    entity.HasOne(d => d.ReferenceType)
            //        .WithMany(p => p.DocumentReferenceTicketStatusMaps)
            //        .HasForeignKey(d => d.ReferenceTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DocumentReferenceTicketStatusMap_DocumentReferenceTypes");

            //    entity.HasOne(d => d.TicketStatus)
            //        .WithMany(p => p.DocumentReferenceTicketStatusMaps)
            //        .HasForeignKey(d => d.TicketStatusID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DocumentReferenceTicketStatusMap_TicketStatuses");
            //});

            modelBuilder.Entity<DocumentReferenceType>(entity =>
            {
                entity.HasKey(e => e.ReferenceTypeID)
                    .HasName("PK_InventoryTypes");

                entity.Property(e => e.ReferenceTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.Property(e => e.DocumentStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DocumentTypeID).ValueGeneratedNever();

                //entity.Property(e => e.IsExternal).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.DocumentReferenceType)
                //    .WithMany(p => p.DocumentTypes)
                //    .HasForeignKey(d => d.ReferenceTypeID)
                //    .HasConstraintName("FK_DocumentTypes_DocumentReferenceTypes");

                //entity.HasOne(d => d.TaxTemplate)
                //    .WithMany(p => p.DocumentTypes)
                //    .HasForeignKey(d => d.TaxTemplateID)
                //    .HasConstraintName("FK_DocumentTypes_TaxTemplates");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.DocumentTypes)
                    .HasForeignKey(d => d.WorkflowID)
                    .HasConstraintName("FK_DocumentTypes_Workflows");
            });

            //modelBuilder.Entity<DocumentTypeType>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DocumentType)
            //        .WithMany(p => p.DocumentTypeType)
            //        .HasForeignKey(d => d.DocumentTypeID)
            //        .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes");

            //    entity.HasOne(d => d.DocumentTypeType2)
            //        .WithMany(p => p.DocumentTypeType2)
            //        .HasForeignKey(d => d.DocumentTypeMapID)
            //        .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes1");
            //});

            //modelBuilder.Entity<DocumentTypeTransactionNumber>(entity =>
            //{
            //    entity.HasKey(e => new { e.DocumentTypeID, e.Year, e.Month });

            //    entity.HasOne(d => d.DocumentType)
            //        .WithMany(p => p.DocumentTypeTransactionNumbers)
            //        .HasForeignKey(d => d.DocumentTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DocumentTypeTransactionNumbers_DocumentTypes");
            //});

            //modelBuilder.Entity<DocumentTypeTypeMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DocumentType)
            //        .WithMany(p => p.DocumentTypeTypeMapDocumentTypes)
            //        .HasForeignKey(d => d.DocumentTypeID)
            //        .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes");

            //    entity.HasOne(d => d.DocumentTypeMap)
            //        .WithMany(p => p.DocumentTypeTypeMapDocumentTypeMaps)
            //        .HasForeignKey(d => d.DocumentTypeMapID)
            //        .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes1");
            //});

            modelBuilder.Entity<DriverScheduleLog>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_DriverScheduleLogs_Employee");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.RouteID)
                    .HasConstraintName("FK_DriverScheduleLogs_Route");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.RouteStopMapID)
                    .HasConstraintName("FK_DriverScheduleLogs_RouteStopMap");

                entity.HasOne(d => d.ScheduleLogStatus)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.SheduleLogStatusID)
                    .HasConstraintName("FK_DriverScheduleLogs_ScheduleLogStatus");

                entity.HasOne(d => d.StopEntryStatus)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.StopEntryStatusID)
                    .HasConstraintName("FK_DriverScheduleLogs_StopEntryStatus");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_DriverScheduleLogs_Student");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.DriverScheduleLogs)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_DriverScheduleLogs_Vehicle");
            });

            //modelBuilder.Entity<DuplicateTuitionFee>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<EducationDetail>(entity =>
            //{
            //    entity.Property(e => e.EducationDetailIID).ValueGeneratedNever();

            //    entity.HasOne(d => d.EducationType)
            //        .WithMany(p => p.EducationDetails)
            //        .HasForeignKey(d => d.EducationTypeID)
            //        .HasConstraintName("FK_EducationDetails_EducationDetails");

            //    entity.HasOne(d => d.Member)
            //        .WithMany(p => p.EducationDetails)
            //        .HasForeignKey(d => d.MemberID)
            //        .HasConstraintName("FK_EducationDetails_Members");
            //});

            //modelBuilder.Entity<EmailCampaign>(entity =>
            //{
            //    entity.HasOne(d => d.Campaign)
            //        .WithMany(p => p.EmailCampaigns)
            //        .HasForeignKey(d => d.CampaignID)
            //        .HasConstraintName("FK_EmailCampaigns_Campaigns");

            //    entity.HasOne(d => d.EmailTemplate)
            //        .WithMany(p => p.EmailCampaigns)
            //        .HasForeignKey(d => d.EmailTemplateID)
            //        .HasConstraintName("FK_EmailCampaigns_EmailTemplates");

            //    entity.HasOne(d => d.Segment)
            //        .WithMany(p => p.EmailCampaigns)
            //        .HasForeignKey(d => d.SegmentID)
            //        .HasConstraintName("FK_EmailCampaigns_Segments");
            //});

            //modelBuilder.Entity<EmailNotificationData>(entity =>
            //{
            //    entity.HasKey(e => e.EmailMetaDataIID)
            //        .HasName("PK_EmailMetaData");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EmailNotificationType)
            //        .WithMany(p => p.EmailNotificationDatas)
            //        .HasForeignKey(d => d.EmailNotificationTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmailNotificationData_EmailNotificationTypes");
            //});

            //modelBuilder.Entity<EmailNotificationType>(entity =>
            //{
            //    entity.Property(e => e.EmailNotificationTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamp)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<EmailTemplate>(entity =>
            //{
            //    entity.Property(e => e.EmailTemplateID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EmailTemplate2>(entity =>
            //{
            //    entity.Property(e => e.EmailTemplateID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<EmailTemplateParameterMap>(entity =>
            {
                entity.Property(e => e.EmailTemplateParameterMapID).ValueGeneratedNever();

                //entity.HasOne(d => d.EmailTemplate)
                //    .WithMany(p => p.EmailTemplateParameterMaps)
                //    .HasForeignKey(d => d.EmailTemplateID)
                //    .HasConstraintName("FK_EmailTemplateParameterMaps_EmailTemplates");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                //entity.Property(e => e.IsOTEligible).HasDefaultValueSql("((0))");

                //entity.Property(e => e.IsOverrideLeaveGroup).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Departments1)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_Employees_Departments");

                //entity.HasOne(d => d.AcadamicCalendar)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.AcademicCalendarID)
                //    .HasConstraintName("FK_Employees_AcadamicCalendar");

                //entity.HasOne(d => d.AccomodationType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.AccomodationTypeID)
                //    .HasConstraintName("FK_Employee_AccomodationType");

                //entity.HasOne(d => d.BloodGroup)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.BloodGroupID)
                //    .HasConstraintName("FK_Employee_BloodGroup");

                //entity.HasOne(d => d.Branch)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.BranchID)
                //    .HasConstraintName("FK_Employees_Branches");

                //entity.HasOne(d => d.CalendarType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CalendarTypeID)
                //    .HasConstraintName("FK_Emp_Calendar_type");

                //entity.HasOne(d => d.Cast)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CastID)
                //    .HasConstraintName("FK_Employees_Casts");

                entity.HasOne(d => d.Catogory)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_Employee_Category");

                //entity.HasOne(d => d.Community)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CommunityID)
                //    .HasConstraintName("FK_Employees_Communitys");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationID)
                    .HasConstraintName("FK_Employees_Designations");

                //entity.HasOne(d => d.EmployeeRole)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.EmployeeRoleID)
                //    .HasConstraintName("FK_Employees_EmployeeRoles");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_Employees_Genders");

                //entity.HasOne(d => d.JobType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.JobTypeID)
                //    .HasConstraintName("FK_Employees_JobTypes");

                //entity.HasOne(d => d.LeaveGroup)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.LeaveGroupID)
                //    .HasConstraintName("FK_emp_LeaveGroup");

                //entity.HasOne(d => d.LeavingType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.LeavingTypeID)
                //    .HasConstraintName("FK_employees_LeavingType");

                entity.HasOne(d => d.LicenseType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LicenseTypeID)
                    .HasConstraintName("FK_Employee_LicenseType");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Employees_Logins");

                //entity.HasOne(d => d.MaritalStatus)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.MaritalStatusID)
                //    .HasConstraintName("FK_Employees_MaritalStatuses");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_Employees_Nationality");

                //entity.HasOne(d => d.PassageType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.PassageTypeID)
                //    .HasConstraintName("FK_Employee_PassageType");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_Employees_Relegions");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.Employees1)
                    .HasForeignKey(d => d.ReportingEmployeeID)
                    .HasConstraintName("FK_Employees_Employees");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ResidencyCompanyId)
                    .HasConstraintName("FK_Employees_Companies");

                //entity.HasOne(d => d.SalaryMethod)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.SalaryMethodID)
                //    .HasConstraintName("FK_Employees_SalaryMethod");
            });

            //modelBuilder.Entity<EmployeeAdditionalInfo>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeAdditionalInfoes)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeAdditionalInfos_Employees");
            //});

            modelBuilder.Entity<EmployeeBankDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeBankIID)
                    .HasName("PK_EmployeeBankDetail");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_Employee_BankName");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Employee_BankDetail");
            });

            //modelBuilder.Entity<EmployeeCatalogRelation>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.RelationType)
            //        .WithMany(p => p.EmployeeCatalogRelations)
            //        .HasForeignKey(d => d.RelationTypeID)
            //        .HasConstraintName("FK_EmployeeCatalogRelations_RelationTypes");
            //});

            //modelBuilder.Entity<EmployeeGrade>(entity =>
            //{
            //    entity.Property(e => e.EmployeeGradeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EmployeeLeaveAllocation>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeLeaveAllocations)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmpLeaveAllocations_Employee");

            //    entity.HasOne(d => d.LeaveType)
            //        .WithMany(p => p.EmployeeLeaveAllocations)
            //        .HasForeignKey(d => d.LeaveTypeID)
            //        .HasConstraintName("FK_EmployeeLeaveAllocations_LeaveTypes");
            //});

            //modelBuilder.Entity<EmployeeLeavingType>(entity =>
            //{
            //    entity.HasKey(e => e.LeavingTypeID)
            //        .HasName("PK_LeadTypes");
            //});

            //modelBuilder.Entity<EmployeeLevel>(entity =>
            //{
            //    entity.Property(e => e.EmployeeLevelID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EmployeePromotion>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_EmployeePromotions_Accounts");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeePromotions_Employees");

            //    entity.HasOne(d => d.NewBranch)
            //        .WithMany(p => p.EmployeePromotionNewBranches)
            //        .HasForeignKey(d => d.NewBranchID)
            //        .HasConstraintName("FK_EmployeePromotions_NewBranches");

            //    entity.HasOne(d => d.NewDesignation)
            //        .WithMany(p => p.EmployeePromotionNewDesignations)
            //        .HasForeignKey(d => d.NewDesignationID)
            //        .HasConstraintName("FK_EmployeePromotions_NewDesignations");

            //    entity.HasOne(d => d.NewLeaveGroup)
            //        .WithMany(p => p.EmployeePromotionNewLeaveGroups)
            //        .HasForeignKey(d => d.NewLeaveGroupID)
            //        .HasConstraintName("FK_EmployeePromotions_LeaveGroups");

            //    entity.HasOne(d => d.NewRole)
            //        .WithMany(p => p.EmployeePromotionNewRoles)
            //        .HasForeignKey(d => d.NewRoleID)
            //        .HasConstraintName("FK_EmployeePromotions_NewRoles");

            //    entity.HasOne(d => d.NewSalaryStructure)
            //        .WithMany(p => p.EmployeePromotionNewSalaryStructures)
            //        .HasForeignKey(d => d.NewSalaryStructureID)
            //        .HasConstraintName("FK_EmployeePromotions_NewSalaryStructure");

            //    entity.HasOne(d => d.OldBranch)
            //        .WithMany(p => p.EmployeePromotionOldBranches)
            //        .HasForeignKey(d => d.OldBranchID)
            //        .HasConstraintName("FK_EmployeePromotions_OldBranches");

            //    entity.HasOne(d => d.OldDesignation)
            //        .WithMany(p => p.EmployeePromotionOldDesignations)
            //        .HasForeignKey(d => d.OldDesignationID)
            //        .HasConstraintName("FK_EmployeePromotions_OldDesignations");

            //    entity.HasOne(d => d.OldLeaveGroup)
            //        .WithMany(p => p.EmployeePromotionOldLeaveGroups)
            //        .HasForeignKey(d => d.OldLeaveGroupID)
            //        .HasConstraintName("FK_EmployeePromotions_OldLeaveGroups");

            //    entity.HasOne(d => d.OldRole)
            //        .WithMany(p => p.EmployeePromotionOldRoles)
            //        .HasForeignKey(d => d.OldRoleID)
            //        .HasConstraintName("FK_EmployeePromotions_OldRoles");

            //    entity.HasOne(d => d.OldSalaryStructure)
            //        .WithMany(p => p.EmployeePromotionOldSalaryStructures)
            //        .HasForeignKey(d => d.OldSalaryStructureID)
            //        .HasConstraintName("FK_EmployeePromotions_OldSalaryStructure");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_EmployeePromotions_SalaryPaymentModes");

            //    entity.HasOne(d => d.PayrollFrequency)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.PayrollFrequencyID)
            //        .HasConstraintName("FK_EmployeePromotions_PayrollFrequencies");

            //    entity.HasOne(d => d.SalaryStructure)
            //        .WithMany(p => p.EmployeePromotionSalaryStructures)
            //        .HasForeignKey(d => d.SalaryStructureID)
            //        .HasConstraintName("FK_EmployeePromotions_SalaryStructure");

            //    entity.HasOne(d => d.TimeSheetSalaryComponent)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.TimeSheetSalaryComponentID)
            //        .HasConstraintName("FK_EmployeePromotions_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeePromotionLeaveAllocation>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EmployeePromotion)
            //        .WithMany(p => p.EmployeePromotionLeaveAllocations)
            //        .HasForeignKey(d => d.EmployeePromotionID)
            //        .HasConstraintName("FK_EmpLeavePromoLeavAllocations_LeaveTypes");

            //    entity.HasOne(d => d.LeaveType)
            //        .WithMany(p => p.EmployeePromotionLeaveAllocations)
            //        .HasForeignKey(d => d.LeaveTypeID)
            //        .HasConstraintName("FK_EmployeePromoLeaveAllocons_LeaveTypes");
            //});

            //modelBuilder.Entity<EmployeePromotionSalaryComponentMap>(entity =>
            //{
            //    entity.HasOne(d => d.EmployeePromotion)
            //        .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
            //        .HasForeignKey(d => d.EmployeePromotionID)
            //        .HasConstraintName("FK_EmployeePromotionSalaryComp_Promotion");

            //    entity.HasOne(d => d.EmployeeSalaryStructureComponentMap)
            //        .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
            //        .HasForeignKey(d => d.EmployeeSalaryStructureComponentMapID)
            //        .HasConstraintName("FK_EmployeePromotion_SalaryComponentMaps");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_EmployeePromotionComp_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeeRelationsDetail>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CountryofIssue)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.CountryofIssueID)
            //        .HasConstraintName("FK_EmployeeRelations_CountryOfIssue");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeRelations_Employee");

            //    entity.HasOne(d => d.EmployeeRelationType)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.EmployeeRelationTypeID)
            //        .HasConstraintName("FK_EmployeeRelations_Ralation");

            //    entity.HasOne(d => d.Sponsor)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.SponsorID)
            //        .HasConstraintName("FK_EmployeeRelations_Sponsor");
            //});

            //modelBuilder.Entity<EmployeeRole>(entity =>
            //{
            //    entity.Property(e => e.EmployeeRoleID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EmployeeRoleMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeRoleMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeRoleMaps_Employees");

            //    entity.HasOne(d => d.EmployeeRole)
            //        .WithMany(p => p.EmployeeRoleMaps)
            //        .HasForeignKey(d => d.EmployeeRoleID)
            //        .HasConstraintName("FK_EmployeeRoleMaps_EmployeeRoles");
            //});

            //modelBuilder.Entity<EmployeeSalary>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.EmployeeSalaries)
            //        .HasForeignKey(d => d.CompanyID)
            //        .HasConstraintName("FK_EmployeeSalaries_Companies");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeSalaries)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeSalaries_Employees");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.EmployeeSalaries)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_EmployeeSalaries_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeeSalaryStructure>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_Accounts");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_Employees");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_SalaryPaymentModes");

            //    entity.HasOne(d => d.PayrollFrequency)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.PayrollFrequencyID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_PayrollFrequencies");

            //    entity.HasOne(d => d.SalaryStructure)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.SalaryStructureID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_SalaryStructure");

            //    entity.HasOne(d => d.TimeSheetSalaryComponent)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.TimeSheetSalaryComponentID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeeSalaryStructureComponentMap>(entity =>
            //{
            //    entity.HasOne(d => d.EmployeeSalaryStructure)
            //        .WithMany(p => p.EmployeeSalaryStructureComponentMaps)
            //        .HasForeignKey(d => d.EmployeeSalaryStructureID)
            //        .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_EmployeeSalaryStructures");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.EmployeeSalaryStructureComponentMaps)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeeTimeSheet>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.Property(e => e.TimesheetEntryStatusID).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmployeeTimeSheets_Employees");

            //    entity.HasOne(d => d.Task)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.TaskID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmployeeTimeSheets_EmployeeTimeSheets");

            //    entity.HasOne(d => d.TimesheetEntryStatus)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.TimesheetEntryStatusID)
            //        .HasConstraintName("FK_EmployeeTimeSheet_Satus");

            //    entity.HasOne(d => d.TimesheetTimeType)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.TimesheetTimeTypeID)
            //        .HasConstraintName("FK_EmployeeTimeSheet_TimesheetTimeType");
            //});

            //modelBuilder.Entity<EmployeeTimeSheetApproval>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeTimeSheetApprovals)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmployeeTimeSheetAs_Employees");

            //    entity.HasOne(d => d.TimesheetApprovalStatus)
            //        .WithMany(p => p.EmployeeTimeSheetApprovals)
            //        .HasForeignKey(d => d.TimesheetApprovalStatusID)
            //        .HasConstraintName("FK_EmployeeTimesheetApproval_ApprovalStatuses");

            //    entity.HasOne(d => d.TimesheetTimeType)
            //        .WithMany(p => p.EmployeeTimeSheetApprovals)
            //        .HasForeignKey(d => d.TimesheetTimeTypeID)
            //        .HasConstraintName("FK_EmployeeTimeSheets_TimesheetTimeType");
            //});

            //modelBuilder.Entity<EmployeeTimesheetApprovalMap>(entity =>
            //{
            //    entity.Property(e => e.EmployeeTimesheetApprovalMapIID).ValueGeneratedOnAdd();

            //    entity.HasOne(d => d.EmployeeTimeSheet)
            //        .WithMany()
            //        .HasForeignKey(d => d.EmployeeTimeSheetID)
            //        .HasConstraintName("FK_EmployeeTimesheetApprovalMaps_EmployeeTimeSheets");

            //    entity.HasOne(d => d.EmployeeTimesheetApproval)
            //        .WithMany()
            //        .HasForeignKey(d => d.EmployeeTimesheetApprovalID)
            //        .HasConstraintName("FK_EmployeeTimesheetApprovalMaps_EmployeeTimeSheetApprovals");
            //});

            //modelBuilder.Entity<EntitlementMap>(entity =>
            //{
            //    entity.HasKey(e => e.EntitlementMapIID)
            //        .HasName("PK_EntityTypeEntitlementMaps");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Entitlement)
            //        .WithMany(p => p.EntitlementMaps)
            //        .HasForeignKey(d => d.EntitlementID)
            //        .HasConstraintName("FK_EntitlementMaps_EntityTypeEntitlements");
            //});

            //modelBuilder.Entity<EntityChangeTracker>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Entity)
            //        .WithMany(p => p.EntityChangeTrackers)
            //        .HasForeignKey(d => d.EntityID)
            //        .HasConstraintName("FK_EntityChangeTracker_Entities");

            //    entity.HasOne(d => d.OperationType)
            //        .WithMany(p => p.EntityChangeTrackers)
            //        .HasForeignKey(d => d.OperationTypeID)
            //        .HasConstraintName("FK_EntityChangeTracker_OperationTypes");

            //    entity.HasOne(d => d.TrackerStatus)
            //        .WithMany(p => p.EntityChangeTrackers)
            //        .HasForeignKey(d => d.TrackerStatusID)
            //        .HasConstraintName("FK_EntityChangeTracker_TrackerStatuses");
            //});

            //modelBuilder.Entity<EntityChangeTrackerLog>(entity =>
            //{
            //    entity.Property(e => e.EntityChangeTrackerType).HasComment("0 - Category\r\n1 - Brand\r\n2 - Supplier");
            //});

            //modelBuilder.Entity<EntityChangeTrackersInProcess>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EntityChangeTracker)
            //        .WithMany(p => p.EntityChangeTrackersInProcesses)
            //        .HasForeignKey(d => d.EntityChangeTrackerID)
            //        .HasConstraintName("FK_EntityChangeTrackersInProcess_EntityChangeTracker");
            //});

            //modelBuilder.Entity<EntityChangeTrackersQueue>(entity =>
            //{
            //    entity.HasKey(e => e.EntityChangeTrackerQueueIID)
            //        .HasName("PK_EntityChangeTrackerQueues");

            //    entity.HasOne(d => d.EntityChangeTracke)
            //        .WithMany(p => p.EntityChangeTrackersQueues)
            //        .HasForeignKey(d => d.EntityChangeTrackeID)
            //        .HasConstraintName("FK_EntityChangeTrackerQueues_EntityChangeTracker");
            //});

            //modelBuilder.Entity<EntityProperty>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EntityPropertyType)
            //        .WithMany(p => p.EntityProperties)
            //        .HasForeignKey(d => d.EntityPropertyTypeID)
            //        .HasConstraintName("FK_EntityProperties_EntityPropertyTypes");
            //});

            //modelBuilder.Entity<EntityPropertyMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<EntityPropertyType>(entity =>
            //{
            //    entity.Property(e => e.EntityPropertyTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EntityScheduler>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.SchedulerEntityType)
            //        .WithMany(p => p.EntitySchedulers)
            //        .HasForeignKey(d => d.SchedulerEntityTypeID)
            //        .HasConstraintName("FK_EntitySchedulers_SchedulerEntityTypes");

            //    entity.HasOne(d => d.SchedulerType)
            //        .WithMany(p => p.EntitySchedulers)
            //        .HasForeignKey(d => d.SchedulerTypeID)
            //        .HasConstraintName("FK_EntitySchedulers_SchedulerTypes");
            //});

            //modelBuilder.Entity<EntityType>(entity =>
            //{
            //    entity.Property(e => e.EntityTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EntityTypeEntitlement>(entity =>
            //{
            //    entity.HasKey(e => e.EntitlementID)
            //        .HasName("PK_Entitlements");

            //    entity.HasOne(d => d.EntityType)
            //        .WithMany(p => p.EntityTypeEntitlements)
            //        .HasForeignKey(d => d.EntityTypeID)
            //        .HasConstraintName("FK_Entitlements_EntityTypes");
            //});

            //modelBuilder.Entity<EntityTypePaymentMethodMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EntityPropertyType)
            //        .WithMany(p => p.EntityTypePaymentMethodMaps)
            //        .HasForeignKey(d => d.EntityPropertyTypeID)
            //        .HasConstraintName("FK_EntityTypePaymentMethodMaps_EntityPropertyTypes");

            //    entity.HasOne(d => d.EntityType)
            //        .WithMany(p => p.EntityTypePaymentMethodMaps)
            //        .HasForeignKey(d => d.EntityTypeID)
            //        .HasConstraintName("FK_EntityTypePaymentMethodMaps_EntityTypes");

            //    entity.HasOne(d => d.PaymentMethod)
            //        .WithMany(p => p.EntityTypePaymentMethodMaps)
            //        .HasForeignKey(d => d.PaymentMethodID)
            //        .HasConstraintName("FK_EntityTypePaymentMethodMaps_PaymentMethods");
            //});

            //modelBuilder.Entity<EntityTypeRelationMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EntityType)
            //        .WithMany(p => p.EntityTypeRelationMaps)
            //        .HasForeignKey(d => d.FromEntityTypeID)
            //        .HasConstraintName("FK_EntityTypeRelationMaps_EntityTypes");

            //    entity.HasOne(d => d.EntityType1)
            //        .WithMany(p => p.EntityTypeRelationMaps1)
            //        .HasForeignKey(d => d.ToEntityTypeID)
            //        .HasConstraintName("FK_EntityTypeRelationMaps_EntityTypes1");
            //});

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<EventAudienceMap>(entity =>
            {
                entity.Property(e => e.EventAudienceMapIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.EventAudienceMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_EventAudienceMaps_Classes");

                entity.HasOne(d => d.EventAudienceType)
                    .WithMany(p => p.EventAudienceMaps)
                    .HasForeignKey(d => d.EventAudienceTypeID)
                    .HasConstraintName("FK_EventAudienceMaps_EventAudienceTypes");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventAudienceMaps)
                    .HasForeignKey(d => d.EventID)
                    .HasConstraintName("FK_EventAudienceMaps_EventAudienceMaps");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.EventAudienceMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_EventAudienceMaps_Sections");

                //entity.HasOne(d => d.StudentCategory)
                //    .WithMany(p => p.EventAudienceMaps)
                //    .HasForeignKey(d => d.StudentCategoryID)
                //    .HasConstraintName("FK_EventAudienceMaps_StudentCategories");
            });

            modelBuilder.Entity<EventTransportAllocation>(entity =>
            {
                entity.HasKey(e => e.EventTransportAllocationIID)
                    .HasName("PK__EventTra__006FE7BC1DA92FB0");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EventTransportAllocations)
                    .HasForeignKey(d => d.AttendarID)
                    .HasConstraintName("FK_EventTransportAllocation_Attendar");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.EventTransportAllocations1)
                    .HasForeignKey(d => d.DriverID)
                    .HasConstraintName("FK_EventTransportAllocation_Driver");

                entity.HasOne(d => d.SchoolEvent)
                    .WithMany(p => p.EventTransportAllocations)
                    .HasForeignKey(d => d.EventID)
                    .HasConstraintName("FK_EventTransportAllocation_Event");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.EventTransportAllocations)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_EventTransportAllocation_Vehicle");
            });

            modelBuilder.Entity<EventTransportAllocationMap>(entity =>
            {
                entity.HasKey(e => e.EventTransportAllocationMapIID)
                    .HasName("PK__EventTra__11DA3B17439210AF");

                entity.HasOne(d => d.EventTransportAllocation)
                    .WithMany(p => p.EventTransportAllocationMaps)
                    .HasForeignKey(d => d.EventTransportAllocationID)
                    .HasConstraintName("FK_EventTransportAllocation_Master");

                entity.HasOne(d => d.StaffRouteStopMap)
                    .WithMany(p => p.EventTransportAllocationMaps)
                    .HasForeignKey(d => d.StaffRouteStopMapID)
                    .HasConstraintName("FK_EventTransportAllocation_Staff");

                entity.HasOne(d => d.StudentRouteStopMap)
                    .WithMany(p => p.EventTransportAllocationMaps)
                    .HasForeignKey(d => d.StudentRouteStopMapID)
                    .HasConstraintName("FK_EventTransportAllocation_Student");

                entity.HasOne(d => d.ToRoute)
                    .WithMany(p => p.EventTransportAllocationMaps)
                    .HasForeignKey(d => d.ToRouteID)
                    .HasConstraintName("FK_EventTransportAllocation_ToRoute");
            });



            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.IsAnnualExam).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsProgressCard).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProgressCardHeader).HasDefaultValueSql("('')");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Exams_AcademicYear");

                entity.HasOne(d => d.ExamGroup)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.ExamGroupID)
                    .HasConstraintName("FK_Exams_ExamGroup");

                entity.HasOne(d => d.ExamType)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.ExamTypeID)
                    .HasConstraintName("FK_Exams_ExamTypes");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.MarkGradeID)
                    .HasConstraintName("FK_Exams_MarkGrades");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Exams_School");
            });

            modelBuilder.Entity<ExamClassMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ExamClassMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ExamClassMaps_Classes");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamClassMaps)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_ExamClassMaps_Exams");

                entity.HasOne(d => d.ExamSchedule)
                    .WithMany(p => p.ExamClassMaps)
                    .HasForeignKey(d => d.ExamScheduleID)
                    .HasConstraintName("FK_ExamClassMaps_ExamSchedules");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ExamClassMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ExamClassMaps_Sections");
            });

            modelBuilder.Entity<ExamGroup>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ExamGroups)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_examgroups_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ExamGroups)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_examgroups_School");
            });

            //modelBuilder.Entity<ExamQuestionGroupMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.ExamQuestionGroupMaps)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_ExamQuestionGroupMap_Exam");

            //    entity.HasOne(d => d.QuestionGroup)
            //        .WithMany(p => p.ExamQuestionGroupMaps)
            //        .HasForeignKey(d => d.QuestionGroupID)
            //        .HasConstraintName("FK_ExamQuestionGroupMap_QuestionGroup");
            //});

            modelBuilder.Entity<ExamSchedule>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamSchedules)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_ExamSchedules_Exams");
            });

            modelBuilder.Entity<ExamSkillMap>(entity =>
            {
                entity.HasOne(d => d.ClassSubjectSkillGroupMap)
                    .WithMany(p => p.ExamSkillMaps)
                    .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
                    .HasConstraintName("FK_ExamSkillMaps_ClassSubjectSkillGroupMaps");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamSkillMaps)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_ExamSkillMaps_Exams");

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.ExamSkillMaps)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_ExamSkillMaps_SkillGroupMasters");
            });

            modelBuilder.Entity<ExamSubjectMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamSubjectMaps)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_ExamSubjectMaps_Exams");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.ExamSubjectMaps)
                    .HasForeignKey(d => d.MarkGradeID)
                    .HasConstraintName("FK_ExamSubjectMaps_ExamSubjectMaps");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ExamSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_ExamSubjectMaps_Subjects");
            });

            modelBuilder.Entity<ExamType>(entity =>
            {
                entity.Property(e => e.ExamTypeID).ValueGeneratedOnAdd();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ExamTypes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ExamTypes_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ExamTypes)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_ExamTypes_School");
            });

            //modelBuilder.Entity<ExtraTimeType>(entity =>
            //{
            //    entity.Property(e => e.ExtraTimeTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Family>(entity =>
            //{
            //    entity.Property(e => e.HouseOwnerShipType).IsFixedLength();

            //    entity.Property(e => e.HouseType).IsFixedLength();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<FeeCollection>(entity =>
            {
                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_FeeCollections_AcademicYears");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FeeCollections_AccountTransactionHeads");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.CashierID)
                    .HasConstraintName("FK_FeeCollections_Employees");

                entity.HasOne(d => d.ClassFeeMaster)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.ClassFeeMasterId)
                    .HasConstraintName("FK_classfeemaster");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_FeeCollections_Classes");

                entity.HasOne(d => d.FeeCollectionStatus)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.FeeCollectionStatusID)
                    .HasConstraintName("FK_FeeCollections_FeeCollectionStatus");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_FeeMasters_PeriodId");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FeeCollections_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_FeeCollections_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.FeeCollections)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_FeeCollections_Students");
            });

            modelBuilder.Entity<FeeCollectionFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.FeeCollectionFeeTypeMapsIID)
                    .HasName("PK_FeeClassMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FeeCollectionFeeTypeMaps_AccountTransactionHeads");

                entity.HasOne(d => d.FeeCollection)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.FeeCollectionID)
                    .HasConstraintName("FK_FeecollectionClassMaps_Collection");

                entity.HasOne(d => d.FeeDueFeeTypeMap)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FeeMasters");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_FeecollectionClassMaps_FeePeriods");

                entity.HasOne(d => d.FineMaster)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.FineMasterID)
                    .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FineMasters");

                entity.HasOne(d => d.FineMasterStudentMap)
                    .WithMany(p => p.FeeCollectionFeeTypeMaps)
                    .HasForeignKey(d => d.FineMasterStudentMapID)
                    .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FineMasterStudentMaps");
            });

            modelBuilder.Entity<FeeCollectionMonthlySplit>(entity =>
            {
                entity.HasKey(e => e.FeeCollectionMonthlySplitIID)
                    .HasName("PK_FeeAssignMonthlySplitId");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FeeCollectionMonthlySplits)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FeeCollectionMonthlySplit_AccountTransactionHead");

                entity.HasOne(d => d.FeeCollectionFeeTypeMap)
                    .WithMany(p => p.FeeCollectionMonthlySplits)
                    .HasForeignKey(d => d.FeeCollectionFeeTypeMapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeeCollectionMonthlySplit_FeeCollectionClassMaps");

                entity.HasOne(d => d.FeeDueMonthlySplit)
                    .WithMany(p => p.FeeCollectionMonthlySplits)
                    .HasForeignKey(d => d.FeeDueMonthlySplitID)
                    .HasConstraintName("FK_FeeCollectionMonthlySplit_FeeDueMonthlySplit");
            });

            modelBuilder.Entity<FeeCollectionPaymentModeMap>(entity =>
            {
                entity.HasKey(e => e.FeeCollectionPaymentModeMapIID)
                    .HasName("PK_FeeCollectionPaymentModeMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountTransactionDetail)
                    .WithMany(p => p.FeeCollectionPaymentModeMaps)
                    .HasForeignKey(d => d.AccountTransactionDetailID)
                    .HasConstraintName("FK_FeeCollectionPaymentModeMaps_AccountTransactionDetails");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.FeeCollectionPaymentModeMaps)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_FeeCollections_Banks");

                entity.HasOne(d => d.FeeCollection)
                    .WithMany(p => p.FeeCollectionPaymentModeMaps)
                    .HasForeignKey(d => d.FeeCollectionID)
                    .HasConstraintName("FK_FeeCollectionPaymentModeMap_FeeCollections");

                entity.HasOne(d => d.PaymentMode)
                    .WithMany(p => p.FeeCollectionPaymentModeMaps)
                    .HasForeignKey(d => d.PaymentModeID)
                    .HasConstraintName("FK_FeeCollectionPaymentModeMaps_PaymentModes");
            });

            modelBuilder.Entity<FeeCollectionStatus>(entity =>
            {
                entity.Property(e => e.FeeCollectionStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<FeeConcessionApprovalType>(entity =>
            {
                entity.HasKey(e => e.ConcessionApprovalTypeID)
                    .HasName("PK__FeeConce__9A15E8131C1E4131");

                entity.Property(e => e.ConcessionApprovalTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<FeeConcessionType>(entity =>
            //{
            //    entity.HasKey(e => e.ConcessionTypeID)
            //        .HasName("PK__Concessi__D97C9C76A6E139BB");

            //    entity.Property(e => e.ConcessionTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<FeeCycle>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<FeeDiscount>(entity =>
            {
                entity.Property(e => e.FeeDiscountID).ValueGeneratedNever();
            });

            modelBuilder.Entity<FeeDueCancellation>(entity =>
            {
                entity.HasKey(e => e.FeeDueCancellationIID)
                    .HasName("PK_FeeDuecancelations");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.FeeDueCancellations)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_FeeDuecancelations_AcademicYear");

                entity.HasOne(d => d.FeeDueFeeTypeMap)
                    .WithMany(p => p.FeeDueCancellations)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .HasConstraintName("FK_FeeDuecancelation_DueFeeType");

                entity.HasOne(d => d.StudentFeeDue)
                    .WithMany(p => p.FeeDueCancellations)
                    .HasForeignKey(d => d.StudentFeeDueID)
                    .HasConstraintName("FK_FeeDuecancelation_Due");
            });

            modelBuilder.Entity<FeeDueFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.FeeDueFeeTypeMapsIID)
                    .HasName("PK_FeeDueFeeTypeMapsIID");

                entity.Property(e => e.CollectedAmount).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FeeDueFeeTypeMaps_AccountTransactionHeads");

                entity.HasOne(d => d.ClassFeeMaster)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.ClassFeeMasterID)
                    .HasConstraintName("FK_FeeDueFeeTypeMaps_ClassFeeMasters");

                entity.HasOne(d => d.FeeMasterClassMap)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterClassMapID)
                    .HasConstraintName("FK_feemasterClassMap_ClassFeeMasters");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_Feemaster_FeeDueFeeTypeMaps");

                //entity.HasOne(d => d.FeePeriod)
                //    .WithMany(p => p.FeeDueFeeTypeMaps)
                //    .HasForeignKey(d => d.FeePeriodID)
                //    .HasConstraintName("FK_FeeDueFeeTypeMaps_FeePeriods");

                entity.HasOne(d => d.FeeStructureFeeMap)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.FeeStructureFeeMapID)
                    .HasConstraintName("FK_FeeDueFeeTypeMaps_FeeStructureFeeMaps");

                entity.HasOne(d => d.FineMaster)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.FineMasterID)
                    .HasConstraintName("FK_FeeDueFeeTypeMaps_FineMasters");

                entity.HasOne(d => d.FineMasterStudentMap)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.FineMasterStudentMapID)
                    .HasConstraintName("FK_FeeDueFeeTypeMaps_FineMasterStudentMaps");

                entity.HasOne(d => d.StudentFeeDue)
                    .WithMany(p => p.FeeDueFeeTypeMaps)
                    .HasForeignKey(d => d.StudentFeeDueID)
                    .HasConstraintName("FK_StudentFeeDue_FeeDueFeeTypeMaps1");
            });

            //modelBuilder.Entity<FeeDueInventoryMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeDueInventoryMapIID)
            //        .HasName("PK_FeeDueInventoryMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.FeeDueInventoryMaps)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_FeeDueInventory_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FeeDueInventoryMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_Feemaster_FeeDueInventoryMaps");

            //    entity.HasOne(d => d.ProductCategoryMap)
            //        .WithMany(p => p.FeeDueInventoryMaps)
            //        .HasForeignKey(d => d.ProductCategoryMapID)
            //        .HasConstraintName("FK_ProductCategory_FeeDueInventory");

            //    entity.HasOne(d => d.StudentFeeDue)
            //        .WithMany(p => p.FeeDueInventoryMaps)
            //        .HasForeignKey(d => d.StudentFeeDueID)
            //        .HasConstraintName("FK_StudentFeeDue_FeeDueInventory");

            //    entity.HasOne(d => d.TransactionHead)
            //        .WithMany(p => p.FeeDueInventoryMaps)
            //        .HasForeignKey(d => d.TransactionHeadID)
            //        .HasConstraintName("FK_FeeDueInventoryMap_TransactionHeads");
            //});

            modelBuilder.Entity<FeeDueMonthlySplit>(entity =>
            {
                entity.HasKey(e => e.FeeDueMonthlySplitIID)
                    .HasName("PK_FeeDueMonthlySplitIID");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FeeDueMonthlySplits)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FeeDueMonthlySplit_AccountTransactionHeads");

                entity.HasOne(d => d.FeeDueFeeTypeMap)
                    .WithMany(p => p.FeeDueMonthlySplits)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeeDueMonthlySplit_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.FeeDueMonthlySplits)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_FeeDueMonthlySplit_FeePeriods");

                entity.HasOne(d => d.FeeStructureMontlySplitMap)
                    .WithMany(p => p.FeeDueMonthlySplits)
                    .HasForeignKey(d => d.FeeStructureMontlySplitMapID)
                    .HasConstraintName("FK_FeeDueMonthlySplit_FeeStructureMontlySplitMaps");
            });

            modelBuilder.Entity<FeeFineType>(entity =>
            {
                entity.Property(e => e.FeeFineTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.FeeType)
                    .WithMany(p => p.FeeFineTypes)
                    .HasForeignKey(d => d.FeeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeeFineTypes_FeeTypes");
            });

            modelBuilder.Entity<FeeGroup>(entity =>
            {
                entity.Property(e => e.FeeGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<FeeMaster>(entity =>
            {
                entity.Property(e => e.FeeMasterID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FeeMasters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_FeeMasters_AcademicYear");

                entity.HasOne(d => d.Account3)
                    .WithMany(p => p.FeeMasters3)
                    .HasForeignKey(d => d.AdvanceAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts3");

                entity.HasOne(d => d.Account5)
                    .WithMany(p => p.FeeMasters5)
                    .HasForeignKey(d => d.AdvanceTaxAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts5");

                entity.HasOne(d => d.FeeCycle)
                    .WithMany(p => p.FeeMasters)
                    .HasForeignKey(d => d.FeeCycleID)
                    .HasConstraintName("FK__FeeMaster__FeeCy__1A9FBED1");

                entity.HasOne(d => d.FeeType)
                    .WithMany(p => p.FeeMasters)
                    .HasForeignKey(d => d.FeeTypeID)
                    .HasConstraintName("FK_FeeMasters_FeeTypes");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FeeMasters)
                    .HasForeignKey(d => d.LedgerAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts");

                entity.HasOne(d => d.Account4)
                    .WithMany(p => p.FeeMasters4)
                    .HasForeignKey(d => d.OSTaxAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts4");

                entity.HasOne(d => d.Account2)
                    .WithMany(p => p.FeeMasters2)
                    .HasForeignKey(d => d.OutstandingAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts2");

                entity.HasOne(d => d.Account6)
                    .WithMany(p => p.FeeMasters6)
                    .HasForeignKey(d => d.ProvisionforAdvanceAccountID)
                    .HasConstraintName("FK_FeeMasters_ProvisforAdvanAcc");

                entity.HasOne(d => d.Account7)
                    .WithMany(p => p.FeeMasters7)
                    .HasForeignKey(d => d.ProvisionforOutstandingAccountID)
                    .HasConstraintName("FK_FeeMasters_ProvisforOutSAcc");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FeeMasters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FeeMasters_School");

                entity.HasOne(d => d.Account1)
                    .WithMany(p => p.FeeMasters1)
                    .HasForeignKey(d => d.TaxLedgerAccountID)
                    .HasConstraintName("FK_FeeMasters_Accounts1");
            });

            modelBuilder.Entity<FeeMasterClassMap>(entity =>
            {
                entity.HasKey(e => e.FeeMasterClassMapIID)
                    .HasName("PK_FeeMasterClassMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassFeeMaster)
                    .WithMany(p => p.FeeMasterClassMaps)
                    .HasForeignKey(d => d.ClassFeeMasterID)
                    .HasConstraintName("FK_FeeMasterClassMaps_ClassFeeMasters");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FeeMasterClassMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_FeeMasterClassMaps_FeeMasters");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.FeeMasterClassMaps)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_FeeMasterClassMaps_FeePeriods");
            });

            modelBuilder.Entity<FeeMasterClassMontlySplitMap>(entity =>
            {
                entity.HasOne(d => d.FeeMasterClassMap)
                    .WithMany(p => p.FeeMasterClassMontlySplitMaps)
                    .HasForeignKey(d => d.FeeMasterClassMapID)
                    .HasConstraintName("FK_FeeMasterClassMontlySplitMaps_FeeMasterClassMaps");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.FeeMasterClassMontlySplitMaps)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_FeeMasterClassMontlySplitMaps_FeePeriods");
            });

            modelBuilder.Entity<FeePaymentMode>(entity =>
            {
                entity.Property(e => e.PaymentModeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<FeePeriod>(entity =>
            {
                entity.Property(e => e.FeePeriodID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FeePeriods)
                    .HasForeignKey(d => d.AcademicYearId)
                    .HasConstraintName("FK_FeePeriods_AcademicYears");

                entity.HasOne(d => d.FeePeriodType)
                    .WithMany(p => p.FeePeriods)
                    .HasForeignKey(d => d.FeePeriodTypeID)
                    .HasConstraintName("FK_FeeTypes_FeePeriodType");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FeePeriods)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FeePeriods_School");
            });

            modelBuilder.Entity<FeePeriodType>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<FeeStructure>(entity =>
            {
                entity.HasKey(e => e.FeeStructureIID)
                    .HasName("PK_ClassPackageMasters");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FeeStructures)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_FeeStructures_AcademicYears");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FeeStructures)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FeeStructures_School");
            });

            modelBuilder.Entity<FeeStructureFeeMap>(entity =>
            {
                entity.HasKey(e => e.FeeStructureFeeMapIID)
                    .HasName("PK_[FeeStructureFeeMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FeeStructureFeeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_FeeStructureFeeMaps_FeeMasters");

                //entity.HasOne(d => d.FeePeriod)
                //    .WithMany(p => p.FeeStructureFeeMaps)
                //    .HasForeignKey(d => d.FeePeriodID)
                //    .HasConstraintName("FK_FeeStructureFeeMaps_FeePeriods");

                entity.HasOne(d => d.FeeStructure)
                    .WithMany(p => p.FeeStructureFeeMaps)
                    .HasForeignKey(d => d.FeeStructureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeeStructureFeeMaps_FeeStructures");
            });

            modelBuilder.Entity<FeeStructureMontlySplitMap>(entity =>
            {
                entity.HasKey(e => e.FeeStructureMontlySplitMapIID)
                    .HasName("PK_FeeStructureMontlySplitMapMaps");

                entity.HasOne(d => d.FeeStructureFeeMap)
                    .WithMany(p => p.FeeStructureMontlySplitMaps)
                    .HasForeignKey(d => d.FeeStructureFeeMapID)
                    .HasConstraintName("FK_FeeStructureMontlySplitMaps_FeeStructureMontlySplitMaps");
            });

            modelBuilder.Entity<FeeType>(entity =>
            {
                entity.Property(e => e.FeeTypeID).ValueGeneratedNever();

                entity.Property(e => e.IsRefundable).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FeeTypes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_FeeTypes_AcademicYear");

                entity.HasOne(d => d.FeeCycle)
                    .WithMany(p => p.FeeTypes)
                    .HasForeignKey(d => d.FeeCycleId)
                    .HasConstraintName("FK_FeeTypes_FeeCycles");

                entity.HasOne(d => d.FeeGroup)
                    .WithMany(p => p.FeeTypes)
                    .HasForeignKey(d => d.FeeGroupId)
                    .HasConstraintName("FK_FeeTypes_FeeGroups");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FeeTypes)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FeeTypes_School");
            });

            modelBuilder.Entity<FinalSettlement>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FinalSettlements)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_FinalSettlement_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.FinalSettlements)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_FinalSettlement_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FinalSettlements)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FinalSettlement_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.FinalSettlements)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_FinalSettlement_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.FinalSettlements)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_FinalSettlement_Students");
            });

            modelBuilder.Entity<FinalSettlementFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.FinalSettlementFeeTypeMapsIID)
                    .HasName("PK_FinalSettlementFeeTypeMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FinalSettlementFeeTypeMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FinalSettlementFeeTypeMaps_AccountTransactionHeads");

                entity.HasOne(d => d.FeeCollectionFeeTypeMap)
                    .WithMany(p => p.FinalSettlementFeeTypeMaps)
                    .HasForeignKey(d => d.FeeCollectionFeeTypeMapsID)
                    .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeeCollectionFeeTypeMaps");

                entity.HasOne(d => d.FeeDueFeeTypeMap)
                    .WithMany(p => p.FinalSettlementFeeTypeMaps)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.FinalSettlementFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeeMasters");

                //entity.HasOne(d => d.FeePeriod)
                //    .WithMany(p => p.FinalSettlementFeeTypeMaps)
                //    .HasForeignKey(d => d.FeePeriodID)
                //    .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeePeriods");

                entity.HasOne(d => d.FinalSettlement)
                    .WithMany(p => p.FinalSettlementFeeTypeMaps)
                    .HasForeignKey(d => d.FinalSettlementID)
                    .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FinalSettlement");
            });

            modelBuilder.Entity<FinalSettlementMonthlySplit>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.FinalSettlementMonthlySplits)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_FinalSettlementMonthlySplit_AccountTransactionHead");

                entity.HasOne(d => d.FeeDueMonthlySplit)
                    .WithMany(p => p.FinalSettlementMonthlySplits)
                    .HasForeignKey(d => d.FeeDueMonthlySplitID)
                    .HasConstraintName("FK_FinalSettlementMonthlySplit_FeeDueMonthlySplit");

                entity.HasOne(d => d.FinalSettlementFeeTypeMap)
                    .WithMany(p => p.FinalSettlementMonthlySplits)
                    .HasForeignKey(d => d.FinalSettlementFeeTypeMapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinalSettlementMonthlySplit_FinalSettlementFeeTypeMaps");
            });

            modelBuilder.Entity<FinalSettlementPaymentModeMap>(entity =>
            {
                entity.HasKey(e => e.FinalSettlementPaymentModeMapIID)
                    .HasName("PK_FinalSettlementPaymentModeMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.FinalSettlement)
                    .WithMany(p => p.FinalSettlementPaymentModeMaps)
                    .HasForeignKey(d => d.FinalSettlementID)
                    .HasConstraintName("FK_FinalSettlementPaymentModeMap_FinalSettlement");

                entity.HasOne(d => d.PaymentMode)
                    .WithMany(p => p.FinalSettlementPaymentModeMaps)
                    .HasForeignKey(d => d.PaymentModeID)
                    .HasConstraintName("FK_FinalSettlementPaymentModeMaps_PaymentModes");
            });

            modelBuilder.Entity<FineMaster>(entity =>
            {
                entity.Property(e => e.FineMasterID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FineMasters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_FineMasters_AcademicYear");

                entity.HasOne(d => d.FeeFineType)
                    .WithMany(p => p.FineMasters)
                    .HasForeignKey(d => d.FeeFineTypeID)
                    .HasConstraintName("FK_FineMasters_FeeFineTypes");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FineMasters)
                    .HasForeignKey(d => d.LedgerAccountID)
                    .HasConstraintName("FK_FineMasters_Accounts");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FineMasters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FineMasters_School");
            });

            modelBuilder.Entity<FineMasterStudentMap>(entity =>
            {
                entity.HasKey(e => e.FineMasterStudentMapIID)
                    .HasName("PK_FineMasterStudentMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FineMasterStudentMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_FineMasterStudentMaps_AcademicYear");

                entity.HasOne(d => d.FineMaster)
                    .WithMany(p => p.FineMasterStudentMaps)
                    .HasForeignKey(d => d.FineMasterID)
                    .HasConstraintName("FK_FineMasterStudentMaps_FineMasters");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FineMasterStudentMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FineMasterStudentMaps_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.FineMasterStudentMaps)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_FineMasterStudentMaps_Students");
            });

            //modelBuilder.Entity<FiscalYear>(entity =>
            //{
            //    entity.HasKey(e => e.FiscalYear_ID)
            //        .HasName("PK_FISCYEAR")
            //        .IsClustered(false);
            //});

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.FormID).ValueGeneratedNever();
            });

            modelBuilder.Entity<FormField>(entity =>
            {
                entity.Property(e => e.FormFieldID).ValueGeneratedNever();

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormFields)
                    .HasForeignKey(d => d.FormID)
                    .HasConstraintName("FK_FormFields_Form");
            });

            modelBuilder.Entity<FormValue>(entity =>
            {
                entity.HasOne(d => d.FormField)
                    .WithMany(p => p.FormValues)
                    .HasForeignKey(d => d.FormFieldID)
                    .HasConstraintName("FK_FormValues_FormField");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormValues)
                    .HasForeignKey(d => d.FormID)
                    .HasConstraintName("FK_FormValues_Form");
            });

            //modelBuilder.Entity<FrequencyType>(entity =>
            //{
            //    entity.Property(e => e.FrequencyTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<FunctionalPeriod>(entity =>
            {
                entity.Property(e => e.FunctionalPeriodID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.FunctionalPeriods)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_FunctionalPeriods_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.FunctionalPeriods)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_FunctionalPeriods_School");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.Property(e => e.ISActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Galleries)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Galleries_AcademicYears");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Galleries)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Galleries_Schools");
            });

            modelBuilder.Entity<GalleryAttachmentMap>(entity =>
            {

                entity.HasOne(d => d.AttachmentContent)
                    .WithMany(p => p.GalleryAttachmentMaps)
                    .HasForeignKey(d => d.AttachmentContentID)
                    .HasConstraintName("FK_GalleryAttachmentMap_Content");

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.GalleryAttachmentMaps)
                    .HasForeignKey(d => d.GalleryID)
                    .HasConstraintName("FK_GalleryAttachmentMap_Gallery");
            });

            modelBuilder.Entity<GeoLocationLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<GlobalSetting>(entity =>
            //{
            //    entity.HasKey(e => e.GlobalSettingIID)
            //        .HasName("PK_setting.GlobalSettings");
            //});

            //modelBuilder.Entity<GradeMapsForReport>(entity =>
            //{
            //    entity.HasKey(e => e.ReportGradeMapIID)
            //        .HasName("PK_GradeMapsReport");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Group>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<HealthEntry>(entity =>
            {
                entity.HasKey(e => e.HealthEntryIID)
                    .HasName("PK_HealthEntry");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.HealthEntries)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_HealthEntries_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.HealthEntries)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_HealthEntries_Classes");

                entity.HasOne(d => d.ExamGroup)
                    .WithMany(p => p.HealthEntries)
                    .HasForeignKey(d => d.ExamGroupID)
                    .HasConstraintName("FK_HealthEntries_ExamGroup");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.HealthEntries)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_HealthEntries_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.HealthEntries)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_HealthEntries_Sections");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.HealthEntries)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_HealthEntries_Teacher");
            });

            modelBuilder.Entity<HealthEntryStudentMap>(entity =>
            {
                entity.HasOne(d => d.HealthEntry)
                    .WithMany(p => p.HealthEntryStudentMaps)
                    .HasForeignKey(d => d.HealthEntryID)
                    .HasConstraintName("FK_HealthEntryStudentMaps_HealthEntry");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.HealthEntryStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_HealthEntryStudentMaps_Students");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.Property(e => e.HolidayIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.HolidayList)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.HolidayListID)
                    .HasConstraintName("FK_Holidays_Holidays");

                entity.HasOne(d => d.HolidayType)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.HolidayTypeID)
                    .HasConstraintName("FK_Holidays_HolidayTypes");
            });

            modelBuilder.Entity<Hostel>(entity =>
            {
                entity.Property(e => e.HostelID).ValueGeneratedNever();

                entity.HasOne(d => d.HostelType)
                    .WithMany(p => p.Hostels)
                    .HasForeignKey(d => d.HostelTypeID)
                    .HasConstraintName("FK_Hostels_HostelTypes");
            });

            modelBuilder.Entity<HostelRoom>(entity =>
            {
                entity.Property(e => e.HostelRoomIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Hostel)
                    .WithMany(p => p.HostelRooms)
                    .HasForeignKey(d => d.HostelID)
                    .HasConstraintName("FK_HostelRooms_Hostels");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.HostelRooms)
                    .HasForeignKey(d => d.RoomTypeID)
                    .HasConstraintName("FK_HostelRooms_RoomTypes");
            });

            //modelBuilder.Entity<IntegrationParameter>(entity =>
            //{
            //    entity.Property(e => e.IntegrationParameterId).ValueGeneratedNever();
            //});

            modelBuilder.Entity<InvetoryTransaction>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Branch)
                //    .WithMany(p => p.InvetoryTransactions)
                //    .HasForeignKey(d => d.BranchID)
                //    .HasConstraintName("FK_InvetoryTransactions_Branches");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.InvetoryTransactions)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_InvetoryTransactions_Companies");

                //entity.HasOne(d => d.Currency)
                //    .WithMany(p => p.InvetoryTransactions)
                //    .HasForeignKey(d => d.CurrencyID)
                //    .HasConstraintName("FK_InvetoryTransactions_Currencies");

                //entity.HasOne(d => d.DocumentType)
                //    .WithMany(p => p.InvetoryTransactions)
                //    .HasForeignKey(d => d.DocumentTypeID)
                //    .HasConstraintName("FK_InvetoryTransactions_DocumentTypes");

                //entity.HasOne(d => d.TransactionHead)
                //    .WithMany(p => p.InvetoryTransactions)
                //    .HasForeignKey(d => d.HeadID)
                //    .HasConstraintName("FK_InvetoryTransactions_TransactionHead");

                //entity.HasOne(d => d.InvetoryTransaction1)
                //    .WithMany(p => p.InvetoryTransactions1)
                //    .HasForeignKey(d => d.LinkDocumentID)
                //    .HasConstraintName("FK_InvetoryTransactions_InvetoryTransactions");

                //entity.HasOne(d => d.ProductSKUMap)
                //    .WithMany(p => p.InvetoryTransactions)
                //    .HasForeignKey(d => d.ProductSKUMapID)
                //    .HasConstraintName("FK_InvetoryTransactions_ProductSKUMaps");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_InvetoryTransactions_Units");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                //entity.HasOne(d => d.AcademicYearNavigation)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Leads_AcademicYears");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Leads_Classes");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Leads_CRMCompanies");

                //entity.HasOne(d => d.Contact)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.ContactID)
                //    .HasConstraintName("FK_Leads_Contacts");

                //entity.HasOne(d => d.Curriculam)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.CurriculamID)
                //    .HasConstraintName("FK_Leads_Syllabus");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_Leads_Genders");

                //entity.HasOne(d => d.IndustryType)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.IndustryTypeID)
                //    .HasConstraintName("FK_Leads_IndustryTypes");

                //entity.HasOne(d => d.LeadSource)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.LeadSourceID)
                //    .HasConstraintName("FK_Leads_LeadSources");

                //entity.HasOne(d => d.LeadStatus)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.LeadStatusID)
                //    .HasConstraintName("FK_Leads_LeadStatus");

                //entity.HasOne(d => d.LeadType)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.LeadTypeID)
                //    .HasConstraintName("FK_Leads_LeadTypes");

                //entity.HasOne(d => d.MarketSegment)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.MarketSegmentID)
                //    .HasConstraintName("FK_Leads_MarketSegments");

                //entity.HasOne(d => d.Nationality)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.NationalityID)
                //    .HasConstraintName("FK_Lead_Nationality");

                //entity.HasOne(d => d.RequestType)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.RequestTypeID)
                //    .HasConstraintName("FK_Leads_RequestTypes");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_leads_School");
            });

            //modelBuilder.Entity<LeadEmailMap>(entity =>
            //{
            //    entity.HasOne(d => d.EmailTemplate)
            //        .WithMany(p => p.LeadEmailMaps)
            //        .HasForeignKey(d => d.EmailTemplateID)
            //        .HasConstraintName("FK_LeadEmailMaps_EmailTemplates");

            //    entity.HasOne(d => d.Lead)
            //        .WithMany(p => p.LeadEmailMaps)
            //        .HasForeignKey(d => d.LeadID)
            //        .HasConstraintName("FK_LeadEmailMaps_Leads");
            //});

            modelBuilder.Entity<LeaveAllocation>(entity =>
            {
                entity.Property(e => e.LeaveAllocationIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.LeaveGroup)
                //    .WithMany()
                //    .HasForeignKey(d => d.LeaveGroupID)
                //    .HasConstraintName("FK_LeaveAllocations_LeaveGroups");

                entity.HasOne(d => d.LeaveType)
                    .WithMany()
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_LeaveAllocations_LeaveTypes");
            });

            modelBuilder.Entity<LeaveApplication>(entity =>
            {
                //entity.Property(e => e.IsLeaveWithoutPay).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LeaveApplications_Employees");

                entity.HasOne(d => d.LeaveSession)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.FromSessionID)
                    .HasConstraintName("FK_LeaveApplications_LeaveApplications");

                entity.HasOne(d => d.LeaveStatus)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveStatusID)
                    .HasConstraintName("FK_LeaveApplications_LeaveStatuses");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveApplications)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_LeaveApplications_LeaveTypes");

                //entity.HasOne(d => d.StaffLeaveReason)
                //    .WithMany(p => p.LeaveApplications)
                //    .HasForeignKey(d => d.StaffLeaveReasonID)
                //    .HasConstraintName("FK_LeaveApplications_LeaveApplications1");

                entity.HasOne(d => d.LeaveSession1)
                    .WithMany(p => p.LeaveApplications1)
                    .HasForeignKey(d => d.ToSessionID)
                    .HasConstraintName("FK_LeaveApplications_LeaveSessions");
            });

            modelBuilder.Entity<LeaveApplicationApprover>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveApplicationApprovers)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LeaveApplicationApprovers_Employees");

                entity.HasOne(d => d.LeaveApplication)
                    .WithMany(p => p.LeaveApplicationApprovers)
                    .HasForeignKey(d => d.LeaveApplicationID)
                    .HasConstraintName("FK_LeaveApplicationApprovers_LeaveApplications");
            });

            //modelBuilder.Entity<LeaveBlockListApprover>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.LeaveBlockListApprovers)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_LeaveBlockListApprovers_Employees");

            //    entity.HasOne(d => d.LeaveBlockList)
            //        .WithMany(p => p.LeaveBlockListApprovers)
            //        .HasForeignKey(d => d.LeaveBlockListID)
            //        .HasConstraintName("FK_LeaveBlockListApprovers_LeaveBlockLists");
            //});

            //modelBuilder.Entity<LeaveBlockListEntry>(entity =>
            //{
            //    entity.HasOne(d => d.LeaveBlockList)
            //        .WithMany(p => p.LeaveBlockListEntries)
            //        .HasForeignKey(d => d.LeaveBlockListID)
            //        .HasConstraintName("FK_LeaveBlockListEntries_LeaveBlockLists");
            //});

            //modelBuilder.Entity<LeaveGroup>(entity =>
            //{
            //    entity.Property(e => e.LeaveGroupID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.LeaveTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<LessonClassRoomComment>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<LessonLearningOutcome>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<LessonPlan>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_LessonPlans_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_LessonPlans_Classes");

                entity.HasOne(d => d.ExamType)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.ExpectedLearningOutcomeID)
                    .HasConstraintName("FK_LessonPlans_Scholastic");

                entity.HasOne(d => d.LessonPlanStatus)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.LessonPlanStatusID)
                    .HasConstraintName("FK_LessonPlans_LessonPlanStatuses");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_LessonPlans_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_LessonPlans_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_LessonPlans_Subjects");

                entity.HasOne(d => d.TeachingAid)
                    .WithMany(p => p.LessonPlans)
                    .HasForeignKey(d => d.TeachingAidID)
                    .HasConstraintName("FK_LessonPlans_TeachingAid");
                entity.HasOne(d => d.Unit)
                   .WithMany(p => p.LessonPlans)
                   .HasForeignKey(d => d.UnitID)
                   .HasConstraintName("FK_LessonPlans_SubjectUnits");

            });
            modelBuilder.Entity<LessonReferenceBook>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LessonResource>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LessonPlanAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanAttachmentMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanAttachmentMaps_LessonPlan");
            });

            modelBuilder.Entity<LessonPlanClassSectionMap>(entity =>
            {
                entity.HasKey(e => e.LessonPlanClassSectionMapIID)
                    .HasName("PK_LessonPlanClassSectionMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.LessonPlanClassSectionMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_LessonPlanClassSectionMap_Class");

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanClassSectionMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanClassSectionMaps_Maping");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.LessonPlanClassSectionMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_LessonPlanClassSectionMap_Section");
            });

            modelBuilder.Entity<LessonPlanLearningOutcomeMap>(entity =>
            {
                entity.HasKey(e => e.LessonPlanLearningOutcomeMapIID)
                    .HasName("PK_LessonPlanLearningOutcomeMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.LessonLearningOutcome)
                //    .WithMany(p => p.LessonPlanLearningOutcomeMaps)
                //    .HasForeignKey(d => d.LessonLearningOutcomeID)
                //    .HasConstraintName("FK_LessonPlanLearningOutcomeMap_LessonLearningOutcomeID");

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanLearningOutcomeMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanLearningOutcomeMap_LessonPlanIID");
            });

            modelBuilder.Entity<LessonPlanLearningObjectiveMap>(entity =>
            {
                entity.HasKey(e => e.LessonPlanLearningObjectiveMapIID)
                    .HasName("PK_LessonPlanLearningObjectiveMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.LessonLearningObjective)
                //    .WithMany(p => p.LessonPlanLearningObjectiveMaps)
                //    .HasForeignKey(d => d.LessonLearningObjectiveID)
                //    .HasConstraintName("FK_LessonPlanLearningObjectiveMap_LessonLearningObjectiveID");

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanLearningObjectiveMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanLearningObjectiveMap_LessonPlanIID");
            });

            modelBuilder.Entity<LessonPlanTaskAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.LessonPlanTaskMap)
                    .WithMany(p => p.LessonPlanTaskAttachmentMaps)
                    .HasForeignKey(d => d.LessonPlanTaskMapID)
                    .HasConstraintName("FK_LessonPlanAttachment_LessonPlanTask");
            });

            modelBuilder.Entity<LessonPlanTaskMap>(entity =>
            {
                entity.HasKey(e => e.LessonPlanTaskMapIID)
                    .HasName("PK_LessonPlanTopicTaskMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanTaskMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlans");

                entity.HasOne(d => d.LessonPlanTopicMap)
                    .WithMany(p => p.LessonPlanTaskMaps)
                    .HasForeignKey(d => d.LessonPlanTopicMapID)
                    .HasConstraintName("FK_LessonPlanTaskTopicMaps");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.LessonPlanTaskMaps)
                    .HasForeignKey(d => d.TaskTypeID)
                    .HasConstraintName("FK_LessonPlanTaskTaskTypes");
            });

            modelBuilder.Entity<LessonPlanTopicAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.LessonPlanTopicMap)
                    .WithMany(p => p.LessonPlanTopicAttachmentMaps)
                    .HasForeignKey(d => d.LessonPlanTopicMapID)
                    .HasConstraintName("FK_LessonPlanAttachmentMaps_LessonPlanTopic");
            });

            modelBuilder.Entity<LessonPlanTopicMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanTopicMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanTopicMaps_LessonPlans");
            });

            //modelBuilder.Entity<LessonStudentEngagement>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<LessonSubjectKnowledge>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<LibraryBook>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.LibraryBooks)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_LibraryBooks_AcademicYear");

                entity.HasOne(d => d.LibraryBookCategory)
                    .WithMany(p => p.LibraryBooks)
                    .HasForeignKey(d => d.BookCategoryCodeID)
                    .HasConstraintName("FK_LibraryBooks_BookCategoryCode");

                entity.HasOne(d => d.LibraryBookCondition)
                    .WithMany(p => p.LibraryBooks)
                    .HasForeignKey(d => d.BookConditionID)
                    .HasConstraintName("FK_LibraryBooks_LibraryBookConditions");

                entity.HasOne(d => d.LibraryBookStatus)
                    .WithMany(p => p.LibraryBooks)
                    .HasForeignKey(d => d.BookStatusID)
                    .HasConstraintName("FK_LibraryBooks_LibraryBookStatuses");

                entity.HasOne(d => d.LibraryBookTypes)
                    .WithMany(p => p.LibraryBooks)
                    .HasForeignKey(d => d.LibraryBookTypeID)
                    .HasConstraintName("FK_LibraryBooks_LibraryBookTypes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.LibraryBooks)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_LibraryBooks_School");
            });

            modelBuilder.Entity<LibraryBookCategory>(entity =>
            {
                entity.Property(e => e.LibraryBookCategoryID).ValueGeneratedNever();
            });

            modelBuilder.Entity<LibraryBookCategoryMap>(entity =>
            {
                entity.HasOne(d => d.LibraryBookCategory)
                    .WithMany(p => p.LibraryBookCategoryMaps)
                    .HasForeignKey(d => d.BookCategoryID)
                    .HasConstraintName("FK_LibraryBookCategoryMaps_LibraryBookCategories");

                entity.HasOne(d => d.LibraryBook)
                    .WithMany(p => p.LibraryBookCategoryMaps)
                    .HasForeignKey(d => d.LibraryBookID)
                    .HasConstraintName("FK_LibraryBookCategoryMaps_LibraryBooks");
            });

            modelBuilder.Entity<LibraryBookCondition>(entity =>
            {
                entity.HasKey(e => e.BookConditionID)
                    .HasName("PK_BookConditions");
            });

            modelBuilder.Entity<LibraryBookMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.LibraryBook)
                    .WithMany(p => p.LibraryBookMaps)
                    .HasForeignKey(d => d.LibraryBookID)
                    .HasConstraintName("FK_LibraryBookMap_LibraryBook");
            });

            modelBuilder.Entity<LibraryStaffRegister>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.LibraryStaffRegisters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_LibraryStaffRegisters_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LibraryStaffRegisters)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LibraryStaffRegisters_LibraryStaffRegisters");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.LibraryStaffRegisters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_LibraryStaffRegisters_School");
            });

            modelBuilder.Entity<LibraryStudentRegister>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.LibraryStudentRegisters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_LibraryStudentRegisters_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.LibraryStudentRegisters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_LibraryStudentRegisters_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LibraryStudentRegisters)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_LibraryStudentRegisters_Students");
            });

            modelBuilder.Entity<LibraryTransaction>(entity =>
            {
                entity.Property(e => e.IsCollected).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_LibraryTransactions_AcademicYear");

                entity.HasOne(d => d.LibraryBookCondition)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.BookCondionID)
                    .HasConstraintName("FK_LibraryTransactions_LibraryBookConditions");

                entity.HasOne(d => d.LibraryBook)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.BookID)
                    .HasConstraintName("FK_LibraryTransactions_LibraryBooks");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_LibraryTransactions_Employees");

                entity.HasOne(d => d.LibraryBookMap)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.LibraryBookMapID)
                    .HasConstraintName("FK_LibraryTransaction_LibraryBookMap");

                entity.HasOne(d => d.LibraryTransactionType)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.LibraryTransactionTypeID)
                    .HasConstraintName("FK_LibraryTransactions_LibraryTransactionTypes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_LibraryTransactions_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LibraryTransactions)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_LibraryTransactions_Students");
            });

            //modelBuilder.Entity<Location>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.Locations)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_Locations_Branches");

            //    entity.HasOne(d => d.LocationType)
            //        .WithMany(p => p.Locations)
            //        .HasForeignKey(d => d.LocationTypeID)
            //        .HasConstraintName("FK_Locations_LocationTypes");
            //});

            //modelBuilder.Entity<Location1>(entity =>
            //{
            //    entity.HasKey(e => e.LocationID)
            //        .HasName("PK_Locations_1");

            //    entity.Property(e => e.LocationID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Area)
            //        .WithMany(p => p.Location1)
            //        .HasForeignKey(d => d.AreaID)
            //        .HasConstraintName("FK_Locations_Areas");

            //    entity.HasOne(d => d.ParentLocation)
            //        .WithMany(p => p.InverseParentLocation)
            //        .HasForeignKey(d => d.ParentLocationID)
            //        .HasConstraintName("FK_Locations_Locations");
            //});

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.RequirePasswordReset).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                        .WithMany(p => p.Logins)
                        .HasForeignKey(d => d.RegisteredCountryID);
            });

            //modelBuilder.Entity<LoginImageMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.LoginImageMaps)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_LoginImageMaps_Logins");
            //});

            modelBuilder.Entity<LoginRoleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.LoginRoleMaps)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_LoginRoleMaps_Logins");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LoginRoleMaps)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_LoginRoleMaps_Roles");
            });

            //modelBuilder.Entity<LoginUserGroup>(entity =>
            //{
            //    entity.Property(e => e.LoginUserGroupID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Lookup>(entity =>
            //{
            //    entity.Property(e => e.LookupID).ValueGeneratedNever();

            //    entity.Property(e => e.LookupType).IsFixedLength();
            //});

            //modelBuilder.Entity<LookupColumnConditionMap>(entity =>
            //{
            //    entity.Property(e => e.LookupColumnConditionMapID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Designation)
            //        .WithMany(p => p.LookupColumnConditionMaps)
            //        .HasForeignKey(d => d.DesignationID)
            //        .HasConstraintName("FK_LookupColumnConditionMaps_Designations");

            //    entity.HasOne(d => d.ScreenLookupMap)
            //        .WithMany(p => p.LookupColumnConditionMaps)
            //        .HasForeignKey(d => d.ScreenLookupMapID)
            //        .HasConstraintName("FK_LookupColumnConditionMaps_ScreenLookupMaps");
            //});

            //modelBuilder.Entity<MaritalStatus1>(entity =>
            //{
            //    entity.Property(e => e.MaritalStatusID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<MarkEntryStatus>(entity =>
            {
                entity.Property(e => e.MarkEntryStatusID).ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MarkGrade>(entity =>
            {
                entity.HasKey(e => e.MarkGradeIID)
                    .HasName("PK_MarkGradeGroups");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.MarkGrades)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_MarkGrades_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.MarkGrades)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_MarkGrades_School");
            });

            modelBuilder.Entity<MarkGradeMap>(entity =>
            {
                entity.HasKey(e => e.MarksGradeMapIID)
                    .HasName("PK_MarkGrades");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.MarkGradeMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_MarkGradeMaps_AcademicYear");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.MarkGradeMaps)
                    .HasForeignKey(d => d.MarkGradeID)
                    .HasConstraintName("FK_MarkGrades_MarkGradeGroups");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.MarkGradeMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_MarkGradeMaps_School");
            });

            modelBuilder.Entity<MarkRegister>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_MarkRegisters_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_MarkRegisters_Classes");

                entity.HasOne(d => d.ExamGroup)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.ExamGroupID)
                    .HasConstraintName("FK_MarkRegister_ExamGroup");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_MarkRegisters_Exams");

                entity.HasOne(d => d.MarkEntryStatus)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.MarkEntryStatusID)
                    .HasConstraintName("FK_MarkRegisters_Status");

                entity.HasOne(d => d.PresentStatus)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.PresentStatusID)
                    .HasConstraintName("FK_MarkRegister_PresentStatusID");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_MarkRegisters_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_MarkRegisters_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_MarkRegisters_Students");
            });

            modelBuilder.Entity<MarkRegisterSkill>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.MarkGradeMap)
                    .WithMany(p => p.MarkRegisterSkills)
                    .HasForeignKey(d => d.MarksGradeMapID)
                    .HasConstraintName("FK_MarkRegisterSkills_MarkGradeMaps");

                entity.HasOne(d => d.MarkEntryStatus)
                    .WithMany(p => p.MarkRegisterSkills)
                    .HasForeignKey(d => d.MarkEntryStatusID)
                    .HasConstraintName("FK_MarkRegisterSkills_ MarkEntryStatusID");

                entity.HasOne(d => d.MarkRegisterSkillGroup)
                    .WithMany(p => p.MarkRegisterSkills)
                    .HasForeignKey(d => d.MarkRegisterSkillGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MarkRegisterSkills_MarkRegisterSkillGroups");

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.MarkRegisterSkills)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_MarkRegisterSkills_SkillGroupMasters");

                entity.HasOne(d => d.SkillMaster)
                    .WithMany(p => p.MarkRegisterSkills)
                    .HasForeignKey(d => d.SkillMasterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MarkRegisterSkills_SkillMasters");
            });

            modelBuilder.Entity<MarkRegisterSkillGroup>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassSubjectSkillGroupMap)
                    .WithMany(p => p.MarkRegisterSkillGroups)
                    .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
                    .HasConstraintName("FK_MarkRegisterSkillGroups_SkillSet");

                entity.HasOne(d => d.MarkRegister)
                    .WithMany(p => p.MarkRegisterSkillGroups)
                    .HasForeignKey(d => d.MarkRegisterID)
                    .HasConstraintName("FK_MarkRegisterSkillGroups_MarkRegister");

                entity.HasOne(d => d.MarkRegisterSubjectMap)
                    .WithMany(p => p.MarkRegisterSkillGroups)
                    .HasForeignKey(d => d.MarkRegisterSubjectMapID)
                    .HasConstraintName("FK_MarkRegisterSkillGroups_MarkRegisterSubjectMaps");

                entity.HasOne(d => d.MarkGradeMap)
                    .WithMany(p => p.MarkRegisterSkillGroups)
                    .HasForeignKey(d => d.MarksGradeMapID)
                    .HasConstraintName("FK_MarkRegisterSkillGroups_MarkGradeMaps");

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.MarkRegisterSkillGroups)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_MarkRegisterSkillGroups_SkillGroupMasters");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.MarkRegisterSkillGroups)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_MarkRegisterSkillMaps_Subjects");
            });

            modelBuilder.Entity<MarkRegisterSubjectMap>(entity =>
            {
                entity.HasKey(e => e.MarkRegisterSubjectMapIID)
                    .HasName("PK_MarkRegisterSubjectMap");

                entity.Property(e => e.IsAbsent).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.MarkEntryStatus)
                    .WithMany(p => p.MarkRegisterSubjectMaps)
                    .HasForeignKey(d => d.MarkEntryStatusID)
                    .HasConstraintName("FK_MarkRegisterSubjectMaps_ MarkEntryStatusID");

                entity.HasOne(d => d.MarkRegister)
                    .WithMany(p => p.MarkRegisterSubjectMaps)
                    .HasForeignKey(d => d.MarkRegisterID)
                    .HasConstraintName("FK_MarkRegisterSubjectMaps_MarkRegisters");

                entity.HasOne(d => d.MarkGradeMap)
                    .WithMany(p => p.MarkRegisterSubjectMaps)
                    .HasForeignKey(d => d.MarksGradeMapID)
                    .HasConstraintName("FK_MarkRegisterSubjectMaps_MarkRegisterGrade");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.MarkRegisterSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_MarkRegisterSubjectMaps_Subjects");
            });

            //modelBuilder.Entity<MarkStatus>(entity =>
            //{
            //    entity.Property(e => e.MarkStatusID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Mediums)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Mediums_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Mediums)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Mediums_School");
            });

            modelBuilder.Entity<MemberQuestionnaireAnswerMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Member)
                //    .WithMany(p => p.MemberQuestionnaireAnswerMaps)
                //    .HasForeignKey(d => d.MemberID)
                //    .HasConstraintName("FK_MemberQuestionnaireAnswerMaps_MemberQuestionnaireAnswerMaps");

                entity.HasOne(d => d.QuestionnaireAnswer)
                    .WithMany(p => p.MemberQuestionnaireAnswerMaps)
                    .HasForeignKey(d => d.QuestionnaireAnswerID)
                    .HasConstraintName("FK_MemberQuestionnaireAnswerMaps_QuestionnaireAnswers");

                entity.HasOne(d => d.Questionnaire)
                    .WithMany(p => p.MemberQuestionnaireAnswerMaps)
                    .HasForeignKey(d => d.QuestionnaireID)
                    .HasConstraintName("FK_MemberQuestionnaireAnswerMaps_Questionnaires");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            

            modelBuilder.Entity<NotificationAlert>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AlertStatus)
                    .WithMany(p => p.NotificationAlerts)
                    .HasForeignKey(d => d.AlertStatusID)
                    .HasConstraintName("FK_NotificationAlerts_AlertStatuses");

                entity.HasOne(d => d.AlertType)
                    .WithMany(p => p.NotificationAlerts)
                    .HasForeignKey(d => d.AlertTypeID)
                    .HasConstraintName("FK_NotificationAlerts_AlertTypes");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.NotificationAlerts)
                    .HasForeignKey(d => d.FromLoginID)
                    .HasConstraintName("FK_NotificationAlerts_Logins");

                entity.HasOne(d => d.Login1)
                    .WithMany(p => p.NotificationAlerts1)
                    .HasForeignKey(d => d.ToLoginID)
                    .HasConstraintName("FK_NotificationAlerts_Logins1");
            });

            modelBuilder.Entity<PackageConfig>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.PackageConfigs)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_PackageConfig_AcademicYears");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.PackageConfigs)
                    .HasForeignKey(d => d.CreditNoteAccountID)
                    .HasConstraintName("FK_PackageConfig_Account");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.PackageConfigs)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_PackageConfig_School");
            });

            modelBuilder.Entity<PackageConfigClassMap>(entity =>
            {
                entity.HasKey(e => e.PackageConfigClassMapIID)
                    .HasName("PK_PackageConfigClassMap");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.PackageConfigClassMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_PackageConfigClassMaps_Classes");

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigClassMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigClassMaps_PackageConfig");
            });

            modelBuilder.Entity<PackageConfigFeeStructureMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.FeeStructure)
                    .WithMany(p => p.PackageConfigFeeStructureMaps)
                    .HasForeignKey(d => d.FeeStructureID)
                    .HasConstraintName("FK_PackageConfigFeeStructureMaps_FeeStructures");

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigFeeStructureMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigFeeStructureMaps_PackageConfig");
            });

            modelBuilder.Entity<PackageConfigStudentGroupMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigStudentGroupMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigStudentGroupMaps_PackageConfig");

                entity.HasOne(d => d.StudentGroup)
                    .WithMany(p => p.PackageConfigStudentGroupMaps)
                    .HasForeignKey(d => d.StudentGroupID)
                    .HasConstraintName("FK_PackageConfigStudentGroupMaps_StudentGroups");
            });

            modelBuilder.Entity<PackageConfigStudentMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PackageConfig)
                    .WithMany(p => p.PackageConfigStudentMaps)
                    .HasForeignKey(d => d.PackageConfigID)
                    .HasConstraintName("FK_PackageConfigStudentMaps_PackageConfig");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.PackageConfigStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_PackageConfigStudentMaps_Students");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Parents_AcademicYear");

                entity.HasOne(d => d.VolunteerType)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.CanYouVolunteerToHelpOneID)
                    .HasConstraintName("FK_Parents_VolunteerType");

                entity.HasOne(d => d.VolunteerType1)
                    .WithMany(p => p.Parents1)
                    .HasForeignKey(d => d.CanYouVolunteerToHelpTwoID)
                    .HasConstraintName("FK_Parents_VolunteerType1");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Parents_Countries");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.FatherCountryID)
                    .HasConstraintName("FK_Parents_FatherNat");

                entity.HasOne(d => d.Country1)
                    .WithMany(p => p.Parents1)
                    .HasForeignKey(d => d.FatherPassportCountryofIssueID)
                    .HasConstraintName("FK_Parents_Countries2");

                entity.HasOne(d => d.Country3)
                    .WithMany(p => p.Parents3)
                    .HasForeignKey(d => d.GuardianCountryofIssueID)
                    .HasConstraintName("FK_Parents_Guradian_PassportCountryofIssueID");

                entity.HasOne(d => d.Nationality1)
                    .WithMany(p => p.Parents1)
                    .HasForeignKey(d => d.GuardianNationalityID)
                    .HasConstraintName("FK_Parents_Guradian_Nationality");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Parents_Logins");

                entity.HasOne(d => d.Nationality2)
                    .WithMany(p => p.Parents2)
                    .HasForeignKey(d => d.MotherCountryID)
                    .HasConstraintName("FK_Parents_MotherNat");

                entity.HasOne(d => d.Country2)
                    .WithMany(p => p.Parents2)
                    .HasForeignKey(d => d.MotherPassportCountryofIssueID)
                    .HasConstraintName("FK_Parents_Countries4");

                entity.HasOne(d => d.GuardianType)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.MotherStudentRelationShipID)
                    .HasConstraintName("FK_Parents_GuardianTypes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Parents_School");

            });

            modelBuilder.Entity<ParentStudentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ParentStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_ParentStudentMaps_Students");
            });

            modelBuilder.Entity<ParentUploadDocumentMap>(entity =>
            {
                entity.HasKey(e => e.ParentUploadDocumentMapIID)
                    .HasName("PK_ContentFiles");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ParentUploadDocumentMaps)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_ParentUploadDocument_Parent");

                entity.HasOne(d => d.UploadDocument)
                    .WithMany(p => p.ParentUploadDocumentMaps)
                    .HasForeignKey(d => d.UploadDocumentID)
                    .HasConstraintName("FK_ParentUploadDocumentMaps_UploadDocument");

                entity.HasOne(d => d.UploadDocumentType)
                    .WithMany(p => p.ParentUploadDocumentMaps)
                    .HasForeignKey(d => d.UploadDocumentTypeID)
                    .HasConstraintName("FK_ParentUploadDocumentMaps_UploadDocumentTypes");
            });

            modelBuilder.Entity<PassportDetailMap>(entity =>
            {
                entity.HasKey(e => e.PassportDetailsIID)
                    .HasName("PK_PassportDetails");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.CountryofIssue)
                //    .WithMany(p => p.PassportDetailMaps)
                //    .HasForeignKey(d => d.CountryofIssueID)
                //    .HasConstraintName("FK_PassportDetailMaps_Countries");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PassportDetailMaps)
                    .HasForeignKey(d => d.CountryofIssueID);
            });

            modelBuilder.Entity<PassportVisaDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.CountryofIssueID)
                    .HasConstraintName("FK_PassportVisa_CountryOfIssue");

                //entity.HasOne(d => d.Reference)
                //    .WithMany(p => p.PassportVisaDetails)
                //    .HasForeignKey(d => d.ReferenceID)
                //    .HasConstraintName("FK_PassportVisa_Employee");

                //entity.HasOne(d => d.Sponsor)
                //    .WithMany(p => p.PassportVisaDetails)
                //    .HasForeignKey(d => d.SponsorID)
                //    .HasConstraintName("FK_PassportVisa_Sponsor");
            });

            modelBuilder.Entity<PaymentMode>(entity =>
            {
                entity.Property(e => e.PaymentModeID).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.PaymentModes)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_PaymentModes_Accounts");

                entity.HasOne(d => d.TenderType)
                    .WithMany(p => p.PaymentModes)
                    .HasForeignKey(d => d.TenderTypeID)
                    .HasConstraintName("FK_PaymentModes_TenderTypes");
            });

            modelBuilder.Entity<Poll>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<PollAnswerMap>(entity =>
            {
                entity.Property(e => e.PollAnswerMapIID).ValueGeneratedNever();

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.PollAnswerMaps)
                    .HasForeignKey(d => d.PollID)
                    .HasConstraintName("FK_PollAnswerMaps_Polls");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Brand)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.BrandID)
                //    .HasConstraintName("FK_Products_Brands");

                //entity.HasOne(d => d.GLAccount)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.GLAccountID)
                //    .HasConstraintName("FK_Products_GLAccount");

                //entity.HasOne(d => d.ProductFamily)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.ProductFamilyID)
                //    .HasConstraintName("FK_Products_ProductFamilies");

                entity.HasOne(d => d.UnitGroup)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PurchaseUnitGroupID)
                    .HasConstraintName("FK_Products_PurchaseUnitGroup");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PurchaseUnitID)
                    .HasConstraintName("FK_Products_PurchaseUnit");

                entity.HasOne(d => d.UnitGroup1)
                    .WithMany(p => p.Products1)
                    .HasForeignKey(d => d.SellingUnitGroupID)
                    .HasConstraintName("FK_Products_SellingUnitGroup");

                entity.HasOne(d => d.Unit1)
                    .WithMany(p => p.Products1)
                    .HasForeignKey(d => d.SellingUnitID)
                    .HasConstraintName("FK_Products_SellingUnit");

                //entity.HasOne(d => d.SeoMetadata)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.SeoMetadataIID)
                //    .HasConstraintName("FK_Products_SeoMetadatas");

                //entity.HasOne(d => d.ProductStatu)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_Products_ProductStatus");

                //entity.HasOne(d => d.TaxTemplate)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.TaxTemplateID)
                //    .HasConstraintName("FK_Products_TaxTemplates");

                entity.HasOne(d => d.UnitGroup2)
                    .WithMany(p => p.Products2)
                    .HasForeignKey(d => d.UnitGroupID)
                    .HasConstraintName("FK_Products_UnitGroups");

                entity.HasOne(d => d.Unit2)
                    .WithMany(p => p.Products2)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_Products_Units");
            });

            modelBuilder.Entity<ProductSKUMap>(entity =>
            {
                entity.Property(e => e.ProductSKUMapIIDTEXT).HasComputedColumnSql("(CONVERT([varchar](20),[ProductSKUMapIID],(0)))", true);

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.ProductSKUMaps)
                //    .HasForeignKey(d => d.ProductID)
                //    .HasConstraintName("FK_ProductSKUMaps_ProductSKUMaps");

                //entity.HasOne(d => d.SeoMetadata)
                //    .WithMany(p => p.ProductSKUMaps)
                //    .HasForeignKey(d => d.SeoMetadataID)
                //    .HasConstraintName("FK_ProductSKUMaps_SEOMetaDataID");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.ProductSKUMaps)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_ProductSKUMaps_ProductStatus");
            });

            modelBuilder.Entity<ProductSKURackMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.ProductSKURackMaps)
                //    .HasForeignKey(d => d.ProductID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_ProductSKURackMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductSKURackMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSKURackMaps_ProductSKUMaps");

                entity.HasOne(d => d.Rack)
                    .WithMany(p => p.ProductSKURackMaps)
                    .HasForeignKey(d => d.RackID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSKURackMaps_Racks");
            });

            modelBuilder.Entity<ProgressReport>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.ProgressReports)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_ProgressReports_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.ProgressReports)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_ProgressReports_Classes");

                //entity.HasOne(d => d.ExamGroup)
                //    .WithMany(p => p.ProgressReports)
                //    .HasForeignKey(d => d.ExamGroupID)
                //    .HasConstraintName("FK_ProgressReport_ExamGroup");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ProgressReports)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_ProgressReport_Exam");

                entity.HasOne(d => d.ProgressReportPublishStatus)
                    .WithMany(p => p.ProgressReports)
                    .HasForeignKey(d => d.PublishStatusID)
                    .HasConstraintName("FK_ProgressReport_PublishStatusID");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.ProgressReports)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_ProgressReports_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ProgressReports)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ProgressReports_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ProgressReports)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_ProgressReports_Students");
            });

            modelBuilder.Entity<ProgressReportPublishStatus>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasOne(d => d.QuestionnaireAnswerType)
                    .WithMany(p => p.Questionnaires)
                    .HasForeignKey(d => d.QuestionnaireAnswerTypeID)
                    .HasConstraintName("FK_Questionnaires_QuestionnaireAnswerTypes");
            });

            modelBuilder.Entity<QuestionnaireAnswer>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.QuestionnaireAnswerType)
                    .WithMany(p => p.QuestionnaireAnswers)
                    .HasForeignKey(d => d.QuestionnaireAnswerTypeID)
                    .HasConstraintName("FK_QuestionnaireAnswers_QuestionnaireAnswerTypes");

                entity.HasOne(d => d.Questionnaire)
                    .WithMany(p => p.QuestionnaireAnswers)
                    .HasForeignKey(d => d.QuestionnaireID)
                    .HasConstraintName("FK_QuestionnaireAnswers_Questionnaires");
            });

            modelBuilder.Entity<QuestionnaireAnswerType>(entity =>
            {
                entity.Property(e => e.QuestionnaireAnswerTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<QuestionnaireSet>(entity =>
            {
                entity.Property(e => e.QuestionnaireSetID).ValueGeneratedNever();
            });

            modelBuilder.Entity<QuestionnaireType>(entity =>
            {
                entity.Property(e => e.QuestionnaireTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Rack>(entity =>
            {
                entity.Property(e => e.RackIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Refund>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_Refund_AcademicYears");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Refund_School");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Refund_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Refund_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Refund_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_Refund_Students");
            });

            modelBuilder.Entity<RefundFeeTypeMap>(entity =>
            {
                entity.HasKey(e => e.RefundFeeTypeMapsIID)
                    .HasName("PK_RefundFeeTypeMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.RefundFeeTypeMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_RefundFeeTypeMaps_AccountTransactionHeads");

                entity.HasOne(d => d.FeeCollectionFeeTypeMap)
                    .WithMany(p => p.RefundFeeTypeMaps)
                    .HasForeignKey(d => d.FeeCollectionFeeTypeMapsID)
                    .HasConstraintName("FK_RefundFeeTypeMaps_FeeCollectionFeeTypeMaps");

                entity.HasOne(d => d.FeeDueFeeTypeMap)
                    .WithMany(p => p.RefundFeeTypeMaps)
                    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                    .HasConstraintName("FK_RefundFeeTypeMaps_FeeDueFeeTypeMaps");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.RefundFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_RefundFeeTypeMaps_FeeMasters");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.RefundFeeTypeMaps)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_RefundFeeTypeMaps_FeePeriods");

                entity.HasOne(d => d.Refund)
                    .WithMany(p => p.RefundFeeTypeMaps)
                    .HasForeignKey(d => d.RefundID)
                    .HasConstraintName("FK_RefundFeeTypeMaps_Refund");
            });

            modelBuilder.Entity<RefundMonthlySplit>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.RefundMonthlySplits)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_RefundMonthlySplit_AccountTransactionHead");

                entity.HasOne(d => d.FeeDueMonthlySplit)
                    .WithMany(p => p.RefundMonthlySplits)
                    .HasForeignKey(d => d.FeeDueMonthlySplitID)
                    .HasConstraintName("FK_RefundMonthlySplit_FeeDueMonthlySplit");

                entity.HasOne(d => d.RefundFeeTypeMap)
                    .WithMany(p => p.RefundMonthlySplits)
                    .HasForeignKey(d => d.RefundFeeTypeMapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefundMonthlySplit_RefundFeeTypeMaps");
            });

            modelBuilder.Entity<RefundPaymentModeMap>(entity =>
            {
                entity.HasKey(e => e.RefundPaymentModeMapIID)
                    .HasName("PK_RefundPaymentModeMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PaymentMode)
                    .WithMany(p => p.RefundPaymentModeMaps)
                    .HasForeignKey(d => d.PaymentModeID)
                    .HasConstraintName("FK_RefundPaymentModeMaps_PaymentModes");

                entity.HasOne(d => d.Refund)
                    .WithMany(p => p.RefundPaymentModeMaps)
                    .HasForeignKey(d => d.RefundID)
                    .HasConstraintName("FK_RefundPaymentModeMap_Refund");
            });

            modelBuilder.Entity<RemarksEntry>(entity =>
            {
                entity.HasKey(e => e.RemarksEntryIID)
                    .HasName("PK_RemarksEntry");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.RemarksEntries)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_RemarksEntries_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.RemarksEntries)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_RemarksEntries_Classes");

                entity.HasOne(d => d.ExamGroup)
                    .WithMany(p => p.RemarksEntries)
                    .HasForeignKey(d => d.ExamGroupID)
                    .HasConstraintName("FK_RemarksEntries_ExamGroup");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.RemarksEntries)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_RemarksEntries_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.RemarksEntries)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_RemarksEntries_Sections");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RemarksEntries)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_RemarksEntries_Teacher");
            });

            modelBuilder.Entity<RemarksEntryExamMap>(entity =>
            {
                entity.HasKey(e => e.RemarksEntryExamMapIID)
                    .HasName("PK_RemarksEntryExam");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.RemarksEntryExamMaps)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_RemarksEntries_Exams");

                entity.HasOne(d => d.RemarksEntryStudentMap)
                    .WithMany(p => p.RemarksEntryExamMaps)
                    .HasForeignKey(d => d.RemarksEntryStudentMapID)
                    .HasConstraintName("FK_RemarksEntryExamMap_RemarksEntryStudentMaps");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.RemarksEntryExamMaps)
                    .HasForeignKey(d => d.subjectID)
                    .HasConstraintName("FK_RemarksEntries_Subjects");
            });

            modelBuilder.Entity<RemarksEntryStudentMap>(entity =>
            {
                entity.HasOne(d => d.RemarksEntry)
                    .WithMany(p => p.RemarksEntryStudentMaps)
                    .HasForeignKey(d => d.RemarksEntryID)
                    .HasConstraintName("FK_RemarksEntryStudentMaps_RemarksEntry");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RemarksEntryStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_RemarksEntryStudentMaps_Students");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID).ValueGeneratedNever();
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.RoomTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Routes1>(entity =>
            {
                entity.HasKey(e => e.RouteID)
                    .HasName("PK_Routes_1");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Routes1)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Routes_AcademicYear");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Routes1)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_Routes_CostCenters");

                entity.HasOne(d => d.RouteGroup)
                    .WithMany(p => p.Routes1)
                    .HasForeignKey(d => d.RouteGroupID)
                    .HasConstraintName("FK_Routes_RouteGroup");

                entity.HasOne(d => d.RouteType)
                    .WithMany(p => p.Routes1)
                    .HasForeignKey(d => d.RouteTypeID)
                    .HasConstraintName("FK_Routes_RouteTypes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Routes1)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Routes_School");
            });

            modelBuilder.Entity<RouteGroup>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.RouteGroups)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_RouteGroups_AcademicYear");

                entity.HasOne(d => d.Schools)
                    .WithMany(p => p.RouteGroups)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_RouteGroups_School");
            });

            modelBuilder.Entity<RouteStopMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.RouteStopMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_RouteStopMaps_AcademicYear");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.RouteStopMaps)
                    .HasForeignKey(d => d.RouteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RouteStopMaps_Routes");
            });

            modelBuilder.Entity<RouteType>(entity =>
            {
                //entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.RouteTypes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_RouteTypes_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.RouteTypes)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_RouteTypes_School");
            });

            modelBuilder.Entity<RouteVehicleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.RouteVehicleMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_RouteVehicleMaps_AcademicYear");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.RouteVehicleMaps)
                    .HasForeignKey(d => d.RouteID)
                    .HasConstraintName("FK_RouteVehicleMaps_Routes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.RouteVehicleMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_RouteVehicleMaps_School");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.RouteVehicleMaps)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_RouteVehicleMaps_Vehicles");
            });

            modelBuilder.Entity<ScheduleLogStatus>(entity =>
            {
                entity.Property(e => e.ScheduleLogStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Schools_Companies");
            });

            modelBuilder.Entity<SchoolCalender>(entity =>
            {
                entity.Property(e => e.SchoolCalenderID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SchoolCalenders)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_SchoolCalenders_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolCalenders)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_SchoolCalenders_School");
            });

            modelBuilder.Entity<SchoolCalenderHolidayMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Holiday)
                    .WithMany(p => p.SchoolCalenderHolidayMaps)
                    .HasForeignKey(d => d.HolidayID)
                    .HasConstraintName("FK_SchoolCalenderHolidayMaps_SchoolCalenderHolidayMaps");
            });

            modelBuilder.Entity<SchoolCreditNote>(entity =>
            {
                entity.HasKey(e => e.SchoolCreditNoteIID)
                    .HasName("PK_SchoolCreditNoteIID");

                entity.Property(e => e.IsDebitNote).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SchoolCreditNotes)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_SchoolCreditNote_AcademicYear");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.SchoolCreditNotes)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_SchoolCreditNote_AccountTransactionHeads");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SchoolCreditNotes)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_SchoolCreditNote_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolCreditNotes)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_SchoolCreditNote_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SchoolCreditNotes)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_SchoolCreditNote_Section");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SchoolCreditNotes)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_SchoolCreditNote_Students");
            });

            modelBuilder.Entity<SchoolDateSetting>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SchoolDateSettings)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_SchoolDateSettings_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolDateSettings)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_SchoolDateSettings_Schools");
            });

            modelBuilder.Entity<SchoolDateSettingMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SchoolDateSetting)
                    .WithMany(p => p.SchoolDateSettingMaps)
                    .HasForeignKey(d => d.SchoolDateSettingID)
                    .HasConstraintName("FK_SchoolDateSettingMaps_DateSetting");
            });

            modelBuilder.Entity<SchoolEvent>(entity =>
            {
                entity.HasKey(e => e.SchoolEventIID)
                    .HasName("PK__SchoolEv__BAFAED4B2AD60482");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SchoolPollAnswerLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PollAnswerMap)
                    .WithMany(p => p.SchoolPollAnswerLogs)
                    .HasForeignKey(d => d.PollAnswerMapID)
                    .HasConstraintName("FK_SchoolPollAnswerLogs_PollAnswerMaps");

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.SchoolPollAnswerLogs)
                    .HasForeignKey(d => d.PollID)
                    .HasConstraintName("FK_SchoolPollAnswerLogs_Polls");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SchoolPollAnswerLogs)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_SchoolPollAnswerLogs_Employees");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SchoolPollAnswerLogs)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_SchoolPollAnswerLogs_Students");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionID).ValueGeneratedNever();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Sections_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Sections_School");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => new { e.SettingCode, e.CompanyID })
                    .HasName("PK_TransactionHistoryArchive_TransactionID");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Settings)
                //    .HasForeignKey(d => d.CompanyID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Settings_Companies");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Shifts_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Shifts_School");
            });

            modelBuilder.Entity<Signup>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Signups)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Signups_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.Signups)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Signups_Class");

                //entity.HasOne(d => d.OrganizerEmployee)
                //    .WithMany(p => p.Signups)
                //    .HasForeignKey(d => d.OrganizerEmployeeID)
                //    .HasConstraintName("FK_MeetingSlotMaps_Employee");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Signups)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Signups_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Signups_Section");

                entity.HasOne(d => d.SignupCategory)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SignupCategoryID)
                    .HasConstraintName("FK_Signups_Category");

                entity.HasOne(d => d.SignupType)
                    .WithMany(p => p.Signups)
                    .HasForeignKey(d => d.SignupTypeID)
                    .HasConstraintName("FK_Signups_SignupType");
            });

            modelBuilder.Entity<SignupAudienceMap>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.SignupAudienceMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_TeacherSignupAudience_AcademicYear");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.SignupAudienceMaps)
                //    .HasForeignKey(d => d.EmployeeID)
                //    .HasConstraintName("FK_SignupAudience_EmployeeIID");

                //entity.HasOne(d => d.Parent)
                //    .WithMany(p => p.SignupAudienceMaps)
                //    .HasForeignKey(d => d.ParentID)
                //    .HasConstraintName("FK_SignupAudience_Parent");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.SignupAudienceMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_TeacherSignupAudience_School");

                entity.HasOne(d => d.Signup)
                    .WithMany(p => p.SignupAudienceMaps)
                    .HasForeignKey(d => d.SignupID)
                    .HasConstraintName("FK_SignupAudience_Signup");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.SignupAudienceMaps)
                //    .HasForeignKey(d => d.StudentID)
                //    .HasConstraintName("FK_SignupAudience_Student");
            });

            modelBuilder.Entity<SignupCategory>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SignupSlotAllocationMap>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.SignupSlotAllocationMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_TeacherSignupSlotAllocation_AcademicYear");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.SignupSlotAllocationMaps)
                //    .HasForeignKey(d => d.EmployeeID)
                //    .HasConstraintName("FK_SignupSlotAllocation_EmployeeIID");

                //entity.HasOne(d => d.Parent)
                //    .WithMany(p => p.SignupSlotAllocationMaps)
                //    .HasForeignKey(d => d.ParentID)
                //    .HasConstraintName("FK_SignupSlotAllocation_Parent");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.SignupSlotAllocationMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_TeacherSignupSlotAllocation_School");

                entity.HasOne(d => d.SignupSlotMap)
                    .WithMany(p => p.SignupSlotAllocationMaps)
                    .HasForeignKey(d => d.SignupSlotMapID)
                    .HasConstraintName("FK_SignupSlotAllocation_Signup");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.SignupSlotAllocationMaps)
                //    .HasForeignKey(d => d.StudentID)
                //    .HasConstraintName("FK_SignupSlotAllocation_Student");
            });

            modelBuilder.Entity<SignupSlotMap>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.SignupSlotMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_SignupSlotMaps_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.SignupSlotMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_SignupSlotMap_School");

                entity.HasOne(d => d.Signup)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.SignupID)
                    .HasConstraintName("FK_SignupSlotMaps_Signups");

                entity.HasOne(d => d.SignupSlotType)
                    .WithMany(p => p.SignupSlotMaps)
                    .HasForeignKey(d => d.SignupSlotTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SignupSlotMap_SlotType");
            });

            modelBuilder.Entity<SignupSlotType>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SignupType>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SkillGroupMaster>(entity =>
            {
                entity.Property(e => e.SkillGroupMasterID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SkillGroupSubjectMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClassSubjectSkillGroupMap)
                    .WithMany(p => p.SkillGroupSubjectMaps)
                    .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkillGroupSkillMaps_SubjectSkillGroupMaps");

                entity.HasOne(d => d.MarkGrade)
                    .WithMany(p => p.SkillGroupSubjectMaps)
                    .HasForeignKey(d => d.MarkGradeID)
                    .HasConstraintName("FK_SkillGroupSkillMaps_MarkGrades");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.SkillGroupSubjectMaps)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_SkillGroupSkillMaps_Subjects");
            });

            modelBuilder.Entity<SkillMaster>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.SkillMasters)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_SkillMasters_SkillGroups");
            });

            modelBuilder.Entity<StaffAttendence>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StaffAttendences)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StaffAttendences_AcademicYear");

                entity.HasOne(d => d.AttendenceReason)
                    .WithMany(p => p.StaffAttendences)
                    .HasForeignKey(d => d.AttendenceReasonID)
                    .HasConstraintName("FK_StaffAttendences_AttendenceReasons");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StaffAttendences)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_StaffAttendences_Employees");

                entity.HasOne(d => d.StaffPresentStatus)
                    .WithMany(p => p.StaffAttendences)
                    .HasForeignKey(d => d.PresentStatusID)
                    .HasConstraintName("FK_StaffAttendences_StaffPresentStatuses");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StaffAttendences)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StaffAttendences_School");
            });

            modelBuilder.Entity<StaffRouteMonthlySplit>(entity =>
            {
                entity.HasOne(d => d.StaffRouteStopMap)
                    .WithMany(p => p.StaffRouteMonthlySplits)
                    .HasForeignKey(d => d.StaffRouteStopMapID)
                    .HasConstraintName("FK_StaffRouteMonthlySplit_StaffRouteStopMaps");
            });

            modelBuilder.Entity<StaffRouteShiftMapLog>(entity =>
            {
                entity.HasKey(e => e.StaffRouteStopMapLogIID)
                    .HasName("PK_StaffRouteStopMapLogs");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_AcademicYear");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.DropStopMapID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_DropStop");

                entity.HasOne(d => d.RouteStopMap1)
                    .WithMany(p => p.StaffRouteShiftMapLogs1)
                    .HasForeignKey(d => d.PickupStopMapID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_PickUpStop");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_School");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.ShiftFromRouteID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_Main_Route");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_Staff");

                entity.HasOne(d => d.StaffRouteStopMap)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.StaffRouteStopMapID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_StaffRouteStopmaps");

                entity.HasOne(d => d.TransportStatus)
                    .WithMany(p => p.StaffRouteShiftMapLogs)
                    .HasForeignKey(d => d.TransportStatusID)
                    .HasConstraintName("FK_StaffRouteStopMapLogs_TransportStatus");
            });

            modelBuilder.Entity<StaffRouteStopMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StaffRouteStopMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StaffRouteStopMaps_AcademicYear");

                entity.HasOne(d => d.RouteStopMap2)
                    .WithMany(p => p.StaffRouteStopMaps2)
                    .HasForeignKey(d => d.DropStopMapID)
                    .HasConstraintName("FK_StaffRouteStopMaps_RouteStopMaps2");

                entity.HasOne(d => d.Routes11)
                    .WithMany(p => p.StaffRouteStopMaps1)
                    .HasForeignKey(d => d.DropStopRouteID)
                    .HasConstraintName("FK_StaffRouteStopMaps_Routes1");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.StaffRouteStopMaps)
                    .HasForeignKey(d => d.PickupRouteID)
                    .HasConstraintName("FK_StaffRouteStopMaps_Routes");

                entity.HasOne(d => d.RouteStopMap1)
                    .WithMany(p => p.StaffRouteStopMaps1)
                    .HasForeignKey(d => d.PickupStopMapID)
                    .HasConstraintName("FK_StaffRouteStopMaps_RouteStopMaps1");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.StaffRouteStopMaps)
                    .HasForeignKey(d => d.RouteStopMapID)
                    .HasConstraintName("FK_StaffRouteStopMaps_RouteStopMaps");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StaffRouteStopMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StaffRouteStopMaps_School");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StaffRouteStopMaps)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_StaffRouteStopMaps_Employees");

                entity.HasOne(d => d.TransportStatus)
                    .WithMany(p => p.StaffRouteStopMaps)
                    .HasForeignKey(d => d.TransportStatusID)
                    .HasConstraintName("FK_StaffRouteStopMaps_TransportStatus");
            });

            modelBuilder.Entity<Stop>(entity =>
            {
                entity.Property(e => e.StopID).ValueGeneratedNever();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Stops)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Stops_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Stops)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Stops_School");
            });

            modelBuilder.Entity<StopEntryStatus>(entity =>
            {
                entity.Property(e => e.StopEntryStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Stream>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Streams)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Streams_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Streams)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Streams_Schools");

                entity.HasOne(d => d.StreamGroup)
                    .WithMany(p => p.Streams)
                    .HasForeignKey(d => d.StreamGroupID)
                    .HasConstraintName("FK_Streams_StreamGroups");
            });

            modelBuilder.Entity<StreamGroup>(entity =>
            {
                entity.HasKey(e => e.StreamGroupID)
                    .IsClustered(false);

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StreamGroups)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StreamGroups_AcademicYear");
            });

            modelBuilder.Entity<StreamOptionalSubjectMap>(entity =>
            {
                entity.HasKey(e => e.StreamOptionalSubjectIID)
                    .HasName("PK_StreamOptionalSubjectMap");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.StreamOptionalSubjectMaps)
                    .HasForeignKey(d => d.StreamID)
                    .HasConstraintName("FK_OptionalSubjectMap_Stream");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StreamOptionalSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_OptionalSubjectMap_Subject");
            });

            modelBuilder.Entity<StreamSubjectMap>(entity =>
            {
                entity.Property(e => e.IsOptionalSubject).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StreamSubjectMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StreamSubjectMaps_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StreamSubjectMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StreamSubjectMaps_School");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.StreamSubjectMaps)
                    .HasForeignKey(d => d.StreamID)
                    .HasConstraintName("FK_StreamSubjectMaps_Streams");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StreamSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_StreamSubjectMaps_Subjects");
            });


            modelBuilder.Entity<Street>(entity =>
            {
                entity.Property(e => e.StreetID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.Streets)
                //    .HasForeignKey(d => d.CountryID)
                //    .HasConstraintName("FK_Streets_Countries");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1-Active\r\n2-Transferred\r\n3-Discontinue\r\n");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Students_AcademicYear");

                entity.HasOne(d => d.StudentApplication)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ApplicationID)
                    .HasConstraintName("FK_Students_StudentApplications");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.BloodGroupID)
                    .HasConstraintName("FK_Students_BloodGroups");

                entity.HasOne(d => d.Cast)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CastID)
                    .HasConstraintName("FK_Students_Casts");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Students_Classes");

                entity.HasOne(d => d.Community)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CommunityID)
                    .HasConstraintName("FK_Students_Communitys");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CurrentCountryID)
                    .HasConstraintName("FK_Students_Countries");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GenderID)
                    .HasConstraintName("FK_Students_Genders");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GradeID)
                    .HasConstraintName("FK_Students_Grades");

                //entity.HasOne(d => d.Hostel)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.HostelID)
                //    .HasConstraintName("FK_Students_Hostels");

                entity.HasOne(d => d.HostelRoom)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.HostelRoomID)
                    .HasConstraintName("FK_Students_HostelRooms");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Students_Logins");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_Students_Parents");

                entity.HasOne(d => d.Country1)
                    .WithMany(p => p.Students1)
                    .HasForeignKey(d => d.PermenentCountryID)
                    .HasConstraintName("FK_Students_Countries1");

                entity.HasOne(d => d.Class1)
                    .WithMany(p => p.Students1)
                    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                    .HasConstraintName("FK_Students_Classes1");

                entity.HasOne(d => d.Syllabu)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.PreviousSchoolSyllabusID)
                    .HasConstraintName("FK_Students_Syllabus");

                entity.HasOne(d => d.GuardianType)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.PrimaryContactID)
                    .HasConstraintName("FK_Students_GuardianTypes");

                entity.HasOne(d => d.Relegion)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.RelegionID)
                    .HasConstraintName("FK_Students_Relegions");

                entity.HasOne(d => d.AcademicYear1)
                    .WithMany(p => p.Students1)
                    .HasForeignKey(d => d.SchoolAcademicyearID)
                    .HasConstraintName("FK_Students_SchoolAcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Students_School");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SecondLangID)
                    .HasConstraintName("FK_Students_N_SecondLanguage");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SecoundLanguageID)
                    .HasConstraintName("FK_Students_SecondLang");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Students_Sections");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StreamID)
                    .HasConstraintName("FK_Students_Streams");

                entity.HasOne(d => d.StudentCategory)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentCategoryID)
                    .HasConstraintName("FK_Students_StudentCategories");

                entity.HasOne(d => d.StudentHouse)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentHouseID)
                    .HasConstraintName("FK_Students_StudentHouses");

                entity.HasOne(d => d.Subject2)
                    .WithMany(p => p.Students2)
                    .HasForeignKey(d => d.SubjectMapID)
                    .HasConstraintName("FK_Students_SubjectMap");

                entity.HasOne(d => d.Subject1)
                    .WithMany(p => p.Students1)
                    .HasForeignKey(d => d.ThirdLangID)
                    .HasConstraintName("FK_Students_N_ThirdLanguage");

                entity.HasOne(d => d.Language1)
                    .WithMany(p => p.Students1)
                    .HasForeignKey(d => d.ThridLanguageID)
                    .HasConstraintName("FK_Students_ThirdLang");
            });

            modelBuilder.Entity<StudentAchievement>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentAchievements)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentAchievement_AcademicYear");

                entity.HasOne(d => d.AchievementCategory)
                    .WithMany(p => p.StudentAchievements)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_StudentAchievement_Category");

                entity.HasOne(d => d.AchievementRanking)
                    .WithMany(p => p.StudentAchievements)
                    .HasForeignKey(d => d.RankingID)
                    .HasConstraintName("FK_StudentAchievement_Ranking");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentAchievements)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentAchievement_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAchievements)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentAchievement_Student");

                entity.HasOne(d => d.AchievementType)
                    .WithMany(p => p.StudentAchievements)
                    .HasForeignKey(d => d.TypeID)
                    .HasConstraintName("FK_StudentAchievement_Type");
            });

            modelBuilder.Entity<StudentApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationIID)
                    .HasName("PK_ApplicationIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.ApplicationStatusID)
                    .HasConstraintName("FK_StudentApplications_ApplicationStatuses");

                entity.HasOne(d => d.ApplicationSubmitType)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.ApplicationTypeID)
                    .HasConstraintName("FK_StudentApplications_SubmitType");

                entity.HasOne(d => d.VolunteerType)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.CanYouVolunteerToHelpOneID)
                    .HasConstraintName("FK_StudentApplications_VolunteerType");

                entity.HasOne(d => d.VolunteerType1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.CanYouVolunteerToHelpTwoID)
                    .HasConstraintName("FK_StudentApplications_VolunteerType1");

                entity.HasOne(d => d.Cast)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.CastID)
                    .HasConstraintName("FK_StudentApplications_Casts");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_StudentApplications_Classes");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_StudentApplications_Countries");

                entity.HasOne(d => d.Syllabu1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.CurriculamID)
                    .HasConstraintName("FK_StudentApplications_Syllabus");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.FatherCountryID)
                    .HasConstraintName("FK_StudentApplications_FatherNat");

                entity.HasOne(d => d.PassportDetailMap1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.FatherPassportDetailNoID)
                    .HasConstraintName("FK_StudentApplications_PassportDetailMaps");

                entity.HasOne(d => d.GuardianType)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.FatherStudentRelationShipID)
                    .HasConstraintName("FK_StudentApplications_GuardianTypes");

                entity.HasOne(d => d.VisaDetailMap2)
                    .WithMany(p => p.StudentApplications2)
                    .HasForeignKey(d => d.FatherVisaDetailNoID)
                    .HasConstraintName("FK_StudentApplications_VisaDetailMaps1");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_StudentApplications_Genders");

                entity.HasOne(d => d.Nationality1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.GuardianNationalityID)
                    .HasConstraintName("FK_Studentapplications_Guradian_Nationality");

                entity.HasOne(d => d.PassportDetailMap)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.GuardianPassportDetailNoID)
                    .HasConstraintName("FK_Studentapplications_Guradian_PassportDetail");

                entity.HasOne(d => d.GuardianType3)
                    .WithMany(p => p.StudentApplications3)
                    .HasForeignKey(d => d.GuardianStudentRelationShipID)
                    .HasConstraintName("FK_Studentapplications_Guradian_RelationShip");

                entity.HasOne(d => d.VisaDetailMap)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.GuardianVisaDetailNoID)
                    .HasConstraintName("FK_Studentapplications_Guradian_VisaDetail");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_StudentApplications_Logins");

                entity.HasOne(d => d.Nationality2)
                    .WithMany(p => p.StudentApplications2)
                    .HasForeignKey(d => d.MotherCountryID)
                    .HasConstraintName("FK_StudentApplications_MotherNat");

                entity.HasOne(d => d.PassportDetailMap2)
                    .WithMany(p => p.StudentApplications2)
                    .HasForeignKey(d => d.MotherPassportDetailNoID)
                    .HasConstraintName("FK_StudentApplications_PassportDetailMaps1");

                entity.HasOne(d => d.GuardianType1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.MotherStudentRelationShipID)
                    .HasConstraintName("FK_StudentApplications_GuardianTypes1");

                entity.HasOne(d => d.VisaDetailMap3)
                    .WithMany(p => p.StudentApplications3)
                    .HasForeignKey(d => d.MotherVisaDetailNoID)
                    .HasConstraintName("FK_StudentApplications_VisaDetailMaps2");

                entity.HasOne(d => d.Nationality3)
                    .WithMany(p => p.StudentApplications3)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_StudentApplications_Nationalities");

                entity.HasOne(d => d.Class1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                    .HasConstraintName("FK_StudentApplications_Classes1");

                entity.HasOne(d => d.Syllabu)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.PreviousSchoolSyllabusID)
                    .HasConstraintName("FK_StudentApplications_StudentApplications");

                entity.HasOne(d => d.GuardianType2)
                    .WithMany(p => p.StudentApplications2)
                    .HasForeignKey(d => d.PrimaryContactID)
                    .HasConstraintName("FK_StudentApplications_GuardianTypes2");

                entity.HasOne(d => d.Relegion)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.RelegionID)
                    .HasConstraintName("FK_StudentApplications_Relegions");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.SchoolAcademicyearID)
                    .HasConstraintName("FK_StudentApplications_AcademicYears1");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentApplications_School");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.SecondLangID)
                    .HasConstraintName("FK_StudentApplications_N_SecondLanguage");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.SecoundLanguageID)
                    .HasConstraintName("FK_StudentApplications_SecondLang");

                //entity.HasOne(d => d.StreamGroup)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StreamGroupID)
                //    .HasConstraintName("FK_Studentapplications_StreamGroup");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.StudentApplications)
                    .HasForeignKey(d => d.StreamID)
                    .HasConstraintName("FK_Studentapplications_Stream");

                //entity.HasOne(d => d.StudentCategory)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StudentCategoryID)
                //    .HasConstraintName("FK_StudentApplications_StudentCategories");

                entity.HasOne(d => d.Country1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.StudentCoutryOfBrithID)
                    .HasConstraintName("FK_StudentApplications_Countries4");

                entity.HasOne(d => d.PassportDetailMap3)
                    .WithMany(p => p.StudentApplications3)
                    .HasForeignKey(d => d.StudentPassportDetailNoID)
                    .HasConstraintName("FK_StudentApplications_PassportDetailMaps2");

                entity.HasOne(d => d.VisaDetailMap1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.StudentVisaDetailNoID)
                    .HasConstraintName("FK_StudentApplications_VisaDetailMaps");

                entity.HasOne(d => d.Subject1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.ThirdLangID)
                    .HasConstraintName("FK_StudentApplications_N_ThirdLanguage");

                entity.HasOne(d => d.Language1)
                    .WithMany(p => p.StudentApplications1)
                    .HasForeignKey(d => d.ThridLanguageID)
                    .HasConstraintName("FK_StudentApplications_ThirdLang");
            });

            modelBuilder.Entity<StudentApplicationDocumentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.StudentApplication)
                    .WithMany(p => p.StudentApplicationDocumentMaps)
                    .HasForeignKey(d => d.ApplicationID)
                    .HasConstraintName("FK_StudentApplicationDocumentMaps_DocAttach");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentApplicationDocumentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentApplicationDocumentMaps_Student");
            });

            modelBuilder.Entity<StudentApplicationOptionalSubjectMap>(entity =>
            {
                entity.HasOne(d => d.StudentApplication)
                    .WithMany(p => p.StudentApplicationOptionalSubjectMaps)
                    .HasForeignKey(d => d.ApplicationID)
                    .HasConstraintName("FK_StudentApplicationOptionalSubjectMaps_ApplicationID");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.StudentApplicationOptionalSubjectMaps)
                    .HasForeignKey(d => d.StreamID)
                    .HasConstraintName("FK_StudentApplicationOptionalSubjectMaps_Stream");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentApplicationOptionalSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_StudentApplicationOptionalSubjectMaps_Subject");
            });


            modelBuilder.Entity<StudentApplicationSiblingMap>(entity =>
            {
                entity.HasKey(e => e.StudentApplicationSiblingMapIID)
                    .HasName("PK_StudentSiblingAppMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.StudentApplicationSiblingMaps)
                    .HasForeignKey(d => d.ApplicationID)
                    .HasConstraintName("FK_StudentApplicationSiblingMaps_StudentApplications");

                entity.HasOne(d => d.Sibling)
                    .WithMany(p => p.StudentApplicationSiblingMaps)
                    .HasForeignKey(d => d.SiblingID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentApplicationSiblingMaps_Students");
            });

            modelBuilder.Entity<StudentAssignmentMap>(entity =>
            {
                entity.Property(e => e.StudentAssignmentMapIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamaps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.StudentAssignmentMaps)
                    .HasForeignKey(d => d.AssignmentID)
                    .HasConstraintName("FK_StudentAssignmentMaps_Assignments1");

                //entity.HasOne(d => d.AssignmentStatus)
                //    .WithMany(p => p.StudentAssignmentMaps)
                //    .HasForeignKey(d => d.AssignmentStatusID)
                //    .HasConstraintName("FK_StudentAssignmentMaps_AssignmentStatuses1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAssignmentMaps)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentAssignmentMaps_Students");
            });

           modelBuilder.Entity<StudentAttendence>(entity =>
           {
               //entity.Property(e => e.TimeStamps)
               //    .IsRowVersion()
               //    .IsConcurrencyToken();

               entity.HasOne(d => d.AcademicYear)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.AcademicYearID)
                   .HasConstraintName("FK_StudentAttendences_AcademicYear");

               entity.HasOne(d => d.AttendenceReason)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.AttendenceReasonID)
                   .HasConstraintName("FK_StudentAttendences_AttendenceReasons");

               entity.HasOne(d => d.Class)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.ClassID)
                   .HasConstraintName("FK_StudentAttendences_Classes");

               entity.HasOne(d => d.Employee)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.EmployeeID)
                   .HasConstraintName("FK_StudentAttendence_Employees");

               entity.HasOne(d => d.PresentStatus)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.PresentStatusID)
                   .HasConstraintName("FK_StudentAttendences_PresentStatuses");

               entity.HasOne(d => d.School)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.SchoolID)
                   .HasConstraintName("FK_StudentAttendences_School");

               entity.HasOne(d => d.Section)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.SectionID)
                   .HasConstraintName("FK_StudentAttendences_Sections");

               entity.HasOne(d => d.Student)
                   .WithMany(p => p.StudentAttendences)
                   .HasForeignKey(d => d.StudentID)
                   .HasConstraintName("FK_StudentAttendences_Students");
           });

            modelBuilder.Entity<StudentCategory>(entity =>
            {
                entity.Property(e => e.StudentCategoryID).ValueGeneratedNever();
            });

            modelBuilder.Entity<StudentClassHistoryMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentClassHistoryMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_StudentClassHistoryMaps_Classes");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentClassHistoryMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_StudentClassHistoryMaps_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentClassHistoryMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentClassHistoryMaps_StudentClassHistoryMaps");
            });

            modelBuilder.Entity<StudentFeeConcession>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                   //.IsRowVersion()
                   // .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentFeeConcessions_AcademicYear");

                entity.HasOne(d => d.FeeConcessionApprovalType)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.ConcessionApprovalTypeID)
                    .HasConstraintName("FK_StudentFeeConcessions_ApprovalType");

                //entity.HasOne(d => d.CreditNoteFeeTypeMap)
                //    .WithMany(p => p.StudentFeeConcessions)
                //    .HasForeignKey(d => d.CreditNoteFeeTypeMapID)
                //    .HasConstraintName("FK_StudFeeCon_CreditNoteFee");

                //entity.HasOne(d => d.FeeDueFeeTypeMaps)
                //    .WithMany(p => p.StudentFeeConcessions)
                //    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                //    .HasConstraintName("FK_StudentFeeConcession_DueFeeType");

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_StudentFeeConcessions_FeeMasters");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_StudentFeeConcessions_FeePeriods");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_StudentFeeConcession_Parent");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_StudentFeeConcession_Staff");

                //entity.HasOne(d => d.StudentFeeDue)
                //    .WithMany(p => p.StudentFeeConcessions)
                //    .HasForeignKey(d => d.StudentFeeDueID)
                //    .HasConstraintName("FK_StudentFeeConcession_Due");

                entity.HasOne(d => d.StudentGroup)
                    .WithMany(p => p.StudentFeeConcessions)
                    .HasForeignKey(d => d.StudentGroupID)
                    .HasConstraintName("FK_StudentFeeConcessions_StudentGroup");
            });

            modelBuilder.Entity<StudentFeeDue>(entity =>
            {
                entity.HasKey(e => e.StudentFeeDueIID)
                    .HasName("PK_FeeDueStudentMapIID");

                entity.Property(e => e.IsAccountPost).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentFeeDues)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_StudentFeeDues_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentFeeDues)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_StudentFeeDues_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentFeeDues)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentFeeDues_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentFeeDues)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_StudentFeeDues_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentFeeDues)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentFeeDues_Students");
            });

            modelBuilder.Entity<StudentGroup>(entity =>
            {
                entity.Property(e => e.StudentGroupID).ValueGeneratedNever();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentGroups)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentGroups_AcademicYear");

                entity.HasOne(d => d.StudentGroupType)
                    .WithMany(p => p.StudentGroups)
                    .HasForeignKey(d => d.GroupTypeID)
                    .HasConstraintName("FK_StudentGroups_StudentGroupType");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentGroups)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentGroups_School");
            });

            modelBuilder.Entity<StudentGroupFeeMaster>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentGroupFeeMasters)
                    .HasForeignKey(d => d.AcadamicYearID)
                    .HasConstraintName("FK_StudentGroupFeeMasters_AcademicYears");
            });

            modelBuilder.Entity<StudentGroupFeeTypeMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.FeeMaster)
                    .WithMany(p => p.StudentGroupFeeTypeMaps)
                    .HasForeignKey(d => d.FeeMasterID)
                    .HasConstraintName("FK_StudentGroupFeeTypeMaps_FeeMasters");

                //entity.HasOne(d => d.FeePeriod)
                //    .WithMany(p => p.StudentGroupFeeTypeMaps)
                //    .HasForeignKey(d => d.FeePeriodID)
                //    .HasConstraintName("FK_StudentGroupFeeTypeMaps_FeePeriods");

                entity.HasOne(d => d.StudentGroupFeeMaster)
                    .WithMany(p => p.StudentGroupFeeTypeMaps)
                    .HasForeignKey(d => d.StudentGroupFeeMasterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGroupFeeTypeMaps_StudentGroupFeeMasters");
            });

            modelBuilder.Entity<StudentGroupMap>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("((0))")
                    .HasComment("");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentGroupMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentGroupMaps_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentGroupMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentGroupMaps_School");

                entity.HasOne(d => d.StudentGroup)
                    .WithMany(p => p.StudentGroupMaps)
                    .HasForeignKey(d => d.StudentGroupID)
                    .HasConstraintName("FK_StudentGroupMaps_StudentGroups");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentGroupMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentGroupMaps_Students");
            });

            modelBuilder.Entity<StudentGroupType>(entity =>
            {
                entity.Property(e => e.GroupTypeIID).ValueGeneratedNever();
            });

            modelBuilder.Entity<StudentHouse>(entity =>
            {
                entity.Property(e => e.StudentHouseID).ValueGeneratedNever();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.StudentHouse)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_StudentHouses_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.StudentHouse)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_StudentHouses_School");
            });

            modelBuilder.Entity<StudentLeaveApplication>(entity =>
            {
                entity.Property(e => e.LeaveStatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentLeaveApplications)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentLeaveApplications_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentLeaveApplications)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_StudentLeaveApplications_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentLeaveApplications)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentLeaveApplications_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentLeaveApplications)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentLeaveApplications_Students");
            });


            modelBuilder.Entity<StudentMiscDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StudentMiscDetails)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_StudentMiscDetails_Employees");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentMiscDetails)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentMiscDetails_Students");
            });

            modelBuilder.Entity<StudentPassportDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country1)
                    .WithMany(p => p.StudentPassportDetails1)
                    .HasForeignKey(d => d.CountryofBirthID)
                    .HasConstraintName("FK_StudentPassportDetails_Countries2");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StudentPassportDetails)
                    .HasForeignKey(d => d.CountryofIssueID)
                    .HasConstraintName("FK_StudentPassportDetails_Countries1");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.StudentPassportDetails)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_StudentPassportDetails_StudentNat");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPassportDetails)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentPassportDetails_Students");
            });

            modelBuilder.Entity<StudentPickLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPickLogs)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentPickLogs_Student");

                entity.HasOne(d => d.StudentPicker)
                    .WithMany(p => p.StudentPickLogs)
                    .HasForeignKey(d => d.StudentPickerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPickLogs_picker");
            });

            modelBuilder.Entity<StudentPicker>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentPickers)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentPickers_AcademicYear");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.StudentPickers)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_StudentPickers_Parent");

                entity.HasOne(d => d.StudentPickedBy)
                    .WithMany(p => p.StudentPickers)
                    .HasForeignKey(d => d.PickedByID)
                    .HasConstraintName("FK_StudentPickers_PickedBy");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentPickers)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentPickers_School");
            });

            modelBuilder.Entity<StudentPickerStudentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPickerStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentPickers_Student");

                entity.HasOne(d => d.StudentPicker)
                    .WithMany(p => p.StudentPickerStudentMaps)
                    .HasForeignKey(d => d.StudentPickerID)
                    .HasConstraintName("FK_StudentPickers_picker");
            });

            modelBuilder.Entity<StudentPickupRequest>(entity =>
            {
                entity.Property(e => e.StudentPickupRequestIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.StudentPickupRequests)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_StudentPickupRequests_AcademicYear");

                entity.HasOne(d => d.StudentPickedBy)
                    .WithMany(p => p.StudentPickupRequests)
                    .HasForeignKey(d => d.PickedByID)
                    .HasConstraintName("FK_StudentPickupRequests_PickedBy");

                entity.HasOne(d => d.StudentPickupRequestStatus)
                    .WithMany(p => p.StudentPickupRequests)
                    .HasForeignKey(d => d.RequestStatusID)
                    .HasConstraintName("FK_StudentPickupRequests_StudentPickupRequestStatus");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.StudentPickupRequests)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_StudentPickupRequests_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPickupRequests)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentPickupRequests_Student");

                entity.HasOne(d => d.StudentPicker)
                    .WithMany(p => p.StudentPickupRequests)
                    .HasForeignKey(d => d.StudentPickerID)
                    .HasConstraintName("FK_StudentPickersreq_picker");
            });

            modelBuilder.Entity<StudentPromotionLog>(entity =>
            {
                //entity.Property(e => e.IsPromoted).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear1)
                    .WithMany(p => p.StudentPromotionLogs1)
                    .HasForeignKey(d => d.AcademicYearID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_AcademicYears1");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentPromotionLogs)
                    .HasForeignKey(d => d.ClassID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_Classes");

                //entity.HasOne(d => d.PromotionStatus)
                //    .WithMany(p => p.StudentPromotionLogs)
                //    .HasForeignKey(d => d.PromotionStatusID)
                //    .HasConstraintName("FK_Promotion_PromotionStatus");

                entity.HasOne(d => d.School1)
                    .WithMany(p => p.StudentPromotionLogs1)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentPromotionLogs_Schools");

                entity.HasOne(d => d.Section1)
                    .WithMany(p => p.StudentPromotionLogs1)
                    .HasForeignKey(d => d.SectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_Sections1");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentPromotionLogs)
                    .HasForeignKey(d => d.ShiftFromAcademicYearID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_AcademicYears");

                entity.HasOne(d => d.Class1)
                    .WithMany(p => p.StudentPromotionLogs1)
                    .HasForeignKey(d => d.ShiftFromClassID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_Classes1");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentPromotionLogs)
                    .HasForeignKey(d => d.ShiftFromSchoolID)
                    .HasConstraintName("FK_StudentPromotionLogs_FromSchools");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentPromotionLogs)
                    .HasForeignKey(d => d.ShiftFromSectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPromotionLogs)
                    .HasForeignKey(d => d.StudentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPromotionLogs_Students");
            });


            modelBuilder.Entity<StudentRouteMonthlySplit>(entity =>
            {
                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.StudentRouteMonthlySplits)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_StudentRouteMonthlySplit_FeePeriods");

                entity.HasOne(d => d.StudentRouteStopMap)
                    .WithMany(p => p.StudentRouteMonthlySplits)
                    .HasForeignKey(d => d.StudentRouteStopMapID)
                    .HasConstraintName("FK_StudentRouteMonthlySplit_StudentRouteStopMaps");
            });

            modelBuilder.Entity<StudentRoutePeriodMap>(entity =>
            {
                entity.HasKey(e => e.StudentRoutePeriodMapIID)
                    .HasName("PK_StudentRoutePeriodMap");

                entity.HasOne(d => d.FeePeriod)
                    .WithMany(p => p.StudentRoutePeriodMaps)
                    .HasForeignKey(d => d.FeePeriodID)
                    .HasConstraintName("FK_StudentRoutePeriodMap_FeePeriods");

                entity.HasOne(d => d.StudentRouteStopMap)
                    .WithMany(p => p.StudentRoutePeriodMaps)
                    .HasForeignKey(d => d.StudentRouteStopMapID)
                    .HasConstraintName("FK_StudeRouteStoPeriodpMaps_RouteStopMap");
            });

            modelBuilder.Entity<StudentRouteStopMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.IsFromPromotion).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentRouteStopMaps_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_StudentRouteStopMaps_Class");

                entity.HasOne(d => d.RouteStopMap2)
                    .WithMany(p => p.StudentRouteStopMaps2)
                    .HasForeignKey(d => d.DropStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMaps_RouteStopMaps2");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.DropStopRouteID)
                    .HasConstraintName("FK_StudentRouteStopMaps_DropRoutes");

                //entity.HasOne(d => d.FeePeriod)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.FeePeriodID)
                //    .HasConstraintName("FK_StudentRouteStopMap_FeePeriods");

                entity.HasOne(d => d.Routes11)
                    .WithMany(p => p.StudentRouteStopMaps1)
                    .HasForeignKey(d => d.PickupRouteID)
                    .HasConstraintName("FK_StudentRouteStopMaps_PickUpRoutes");

                entity.HasOne(d => d.RouteStopMap1)
                    .WithMany(p => p.StudentRouteStopMaps1)
                    .HasForeignKey(d => d.PickupStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMaps_RouteStopMaps1");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.RouteStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMaps_RouteStopMaps");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentRouteStopMaps_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_StudentRouteStopMaps_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentRouteStopMaps_Students");

                entity.HasOne(d => d.TransportStatus)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.TransportStatusID)
                    .HasConstraintName("FK_StudentRouteStopMaps_TransportStatus");
            });

            modelBuilder.Entity<StudentRouteStopMapLog>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_Class");

                entity.HasOne(d => d.RouteStopMap2)
                    .WithMany(p => p.StudentRouteStopMapLogs2)
                    .HasForeignKey(d => d.DropStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_RouteStopMaps2");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.DropStopRouteID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_DropRoutes");

                entity.HasOne(d => d.Routes12)
                    .WithMany(p => p.StudentRouteStopMapLogs2)
                    .HasForeignKey(d => d.PickupRouteID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_PickUpRoutes");

                entity.HasOne(d => d.RouteStopMap1)
                    .WithMany(p => p.StudentRouteStopMapLogs1)
                    .HasForeignKey(d => d.PickupStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_RouteStopMaps1");

                entity.HasOne(d => d.Routes11)
                    .WithMany(p => p.StudentRouteStopMapLogs1)
                    .HasForeignKey(d => d.RouteID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_Main_Route");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.RouteStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_RouteStopMaps");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_Students");

                entity.HasOne(d => d.StudentRouteStopMap)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.StudentRouteStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_StudRouteStopmaps");

                entity.HasOne(d => d.TransportStatus)
                    .WithMany(p => p.StudentRouteStopMapLogs)
                    .HasForeignKey(d => d.TransportStatusID)
                    .HasConstraintName("FK_StudentRouteStopMapLogs_TransportStatus");
            });

            modelBuilder.Entity<StudentSiblingMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Parent)
                //    .WithMany(p => p.StudentSiblingMaps)
                //    .HasForeignKey(d => d.ParentID)
                //    .HasConstraintName("FK_StudentSiblingMap_Parent");

                entity.HasOne(d => d.Student1)
                    .WithMany(p => p.StudentSiblingMaps1)
                    .HasForeignKey(d => d.SiblingID)
                    .HasConstraintName("FK_StudentSiblingMaps_Students1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentSiblingMaps)
                    .HasForeignKey(d => d.StudentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSiblingMaps_Students");
            });

            modelBuilder.Entity<StudentSkillGroupMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.MarkGradeMap)
                    .WithMany(p => p.StudentSkillGroupMaps)
                    .HasForeignKey(d => d.MarksGradeMapID)
                    .HasConstraintName("FK_StudentSkillGroupMaps_MarkGradeMaps");

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.StudentSkillGroupMaps)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_StudentSkillGroupMaps_SkillGroupMasters");

                entity.HasOne(d => d.StudentSkillRegister)
                    .WithMany(p => p.StudentSkillGroupMaps)
                    .HasForeignKey(d => d.StudentSkillRegisterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSkillGroupMaps_StudentSkillRegisters");
            });

            modelBuilder.Entity<StudentSkillMasterMap>(entity =>
            {
                entity.HasKey(e => e.StudentSkillMasterMapIID)
                    .HasName("PK_StudentSkillMasterMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.MarkGradeMap)
                    .WithMany(p => p.StudentSkillMasterMaps)
                    .HasForeignKey(d => d.MarksGradeMapID)
                    .HasConstraintName("FK_StudentSkillMasterMaps_MarkGradeMaps");

                entity.HasOne(d => d.SkillGroupMaster)
                    .WithMany(p => p.StudentSkillMasterMaps)
                    .HasForeignKey(d => d.SkillGroupMasterID)
                    .HasConstraintName("FK_StudentSkillMasterMaps_SkillGroupMasters");

                entity.HasOne(d => d.SkillMaster)
                    .WithMany(p => p.StudentSkillMasterMaps)
                    .HasForeignKey(d => d.SkillMasterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSkillMasterMaps_SkillMasters");

                entity.HasOne(d => d.StudentSkillGroupMap)
                    .WithMany(p => p.StudentSkillMasterMaps)
                    .HasForeignKey(d => d.StudentSkillGroupMapsID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSkillMasterMaps_StudentSkillGroupMaps");
            });

            modelBuilder.Entity<StudentSkillRegister>(entity =>
            {
                entity.HasKey(e => e.StudentSkillRegisterIID)
                    .HasName("PK_StudentSkillRegister");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentSkillRegisters)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_StudentSkillRegisters_Classes");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.StudentSkillRegisters)
                    .HasForeignKey(d => d.ExamID)
                    .HasConstraintName("FK_StudentSkillRegisters_Exams");

                entity.HasOne(d => d.MarkGradeMap)
                    .WithMany(p => p.StudentSkillRegisters)
                    .HasForeignKey(d => d.MarksGradeMapID)
                    .HasConstraintName("FK_StudentSkillRegisters_MarkGradeMaps");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentSkillRegisters)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_StudentSkillRegisters_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentSkillRegisters)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentSkillRegisters_Students");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentSkillRegisters)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_StudentSkillRegisters_Subjects");
            });

            modelBuilder.Entity<StudentStaffMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StudentStaffMaps)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_StudentStaffMap_Staff");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentStaffMaps)
                    .HasForeignKey(d => d.StudentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentStaffMap_Students");
            });

            modelBuilder.Entity<StudentStreamOptionalSubjectMap>(entity =>
            {
                entity.HasKey(e => e.StudentStreamOptionalSubjectMapIID)
                    .HasName("PK_StreamOptionalSubjectMaps");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.StudentStreamOptionalSubjectMaps)
                    .HasForeignKey(d => d.StreamID)
                    .HasConstraintName("FK_StudentOptionalSubjectMap_Stream");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentStreamOptionalSubjectMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentOptionalSubjectMap_StudentID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentStreamOptionalSubjectMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_StudentOptionalSubjectMap_Subject");
            });

            modelBuilder.Entity<StudentTransferRequest>(entity =>
            {
                entity.Property(e => e.StudentTransferRequestIID).ValueGeneratedNever();

                entity.Property(e => e.IsMailSent).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTransferRequested).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentTransferRequests)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_studenttransferRequests_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentTransferRequests)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_studenttransferRequests_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTransferRequests)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentTransferRequest_Students");

                entity.HasOne(d => d.StudentTransferRequestReason)
                    .WithMany(p => p.StudentTransferRequests)
                    .HasForeignKey(d => d.TransferRequestReasonID)
                    .HasConstraintName("FK_StudentTransferRequests_StudentTransferRequests");

                entity.HasOne(d => d.TransferRequestStatus)
                    .WithMany(p => p.StudentTransferRequests)
                    .HasForeignKey(d => d.TransferRequestStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTransferRequests_TransferRequestStatuses");
            });

            modelBuilder.Entity<StudentTransferRequestReason>(entity =>
            {
                entity.HasKey(e => e.TransferRequestReasonIID)
                    .HasName("PK_TCReasonss");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentTransferRequestReasons)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentTransferRequestReasons_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentTransferRequestReasons)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_StudentTransferRequestReasons_School");
            });


            modelBuilder.Entity<StudentVehicleAssign>(entity =>
            {
                entity.Property(e => e.StudentAssignId).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.StudentVehicleAssigns)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentVehicleAssign_Routes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.StudentVehicleAssigns)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentVehicleAssign_Vehicles");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectID).ValueGeneratedNever();

                entity.Property(e => e.IsLanguage).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Subjects_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Subjects_School");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.SubjectTypeID)
                    .HasConstraintName("FK_Subjects_SubjectTypes");
            });

            modelBuilder.Entity<SubjectTeacherMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SubjectTeacherMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_SubjectTeacherMaps_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SubjectTeacherMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_SubjectTeacherMaps_Classes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SubjectTeacherMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_SubjectTeacherMaps_Employees");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SubjectTeacherMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_SubjectTeacherMaps_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SubjectTeacherMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_SubjectTeacherMaps_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectTeacherMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_SubjectTeacherMaps_Subjects");
            });


            modelBuilder.Entity<SubjectTopic>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SubjectTopics)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_SubjectTopics_Classes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SubjectTopics)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_SubjectTopics_Employees");

                //entity.HasOne(d => d.ParentTopic)
                //    .WithMany(p => p.InverseParentTopic)
                //    .HasForeignKey(d => d.ParentTopicID)
                //    .HasConstraintName("FK_SubjectTopics_SubjectTopics");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SubjectTopics)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_SubjectTopics_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectTopics)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_SubjectTopics_Subjects");
            });

            modelBuilder.Entity<Syllabu>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Syllabus)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Syllabus_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Syllabus)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Syllabus_School");
            });

            modelBuilder.Entity<TeacherActivity>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TeacherActivities)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_TeacherActivities_Classes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TeacherActivities)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_TeacherActivities_Employees");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TeacherActivities)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_TeacherActivities_Sections");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TeacherActivities)
                    .HasForeignKey(d => d.ShiftID)
                    .HasConstraintName("FK_TeacherActivities_Shifts");

                entity.HasOne(d => d.SubjectTopic1)
                    .WithMany(p => p.TeacherActivities1)
                    .HasForeignKey(d => d.SubTopicID)
                    .HasConstraintName("FK_TeacherActivities_SubjectTopics1");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TeacherActivities)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_TeacherActivities_Subjects");

                entity.HasOne(d => d.SubjectTopic)
                    .WithMany(p => p.TeacherActivities)
                    .HasForeignKey(d => d.TopicID)
                    .HasConstraintName("FK_TeacherActivities_SubjectTopics");
            });

            modelBuilder.Entity<TeachingAid>(entity =>
            {
                entity.Property(e => e.TeachingAidID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TenderType>(entity =>
            {
                entity.Property(e => e.TenderTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<TimeTable>(entity =>
            {
                entity.Property(e => e.TimeTableID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasComment("1-Active, 2-Inactive");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TimeTables)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TimeTables_TimeTables");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.TimeTables)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_TimeTables_School");
            });

            modelBuilder.Entity<TimeTableAllocation>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TimeTableAllocations_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_TimeTableAllocations_Classes");

                entity.HasOne(d => d.ClassTiming)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.ClassTimingID)
                    .HasConstraintName("FK_TimeTableAllocations_ClassTimings");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.TimeTableAllocations)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_TimeTableAllocations_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_TimeTableAllocations_Sections");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_TimeTableAllocations_Employees");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_TimeTableAllocations_Subjects");

                entity.HasOne(d => d.TimeTable)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.TimeTableID)
                    .HasConstraintName("FK_TimeTableAllocations_TimeTables");

                entity.HasOne(d => d.WeekDay)
                    .WithMany(p => p.TimeTableAllocations)
                    .HasForeignKey(d => d.WeekDayID)
                    .HasConstraintName("FK_TimeTableAllocations_WeekDays");
            });

            modelBuilder.Entity<TimeTableLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TimeTableLogs_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_TimeTableLogs_Classes");

                entity.HasOne(d => d.ClassTiming)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.ClassTimingID)
                    .HasConstraintName("FK_TimeTableLogs_ClassTimings");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.TimeTableLogs)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_TimeTableLogs_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_TimeTableLogs_Sections");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_TimeTableLogs_Employees");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_TimeTableLogs_Subjects");

                //entity.HasOne(d => d.TimeTable)
                //    .WithMany(p => p.TimeTableLogs)
                //    .HasForeignKey(d => d.TimeTableID)
                //    .HasConstraintName("FK_TimeTableLogs_TimeTables");

                //entity.HasOne(d => d.WeekDay)
                //    .WithMany(p => p.TimeTableLogs)
                //    .HasForeignKey(d => d.WeekDayID)
                //    .HasConstraintName("FK_TimeTableLogs_WeekDays");

                entity.HasOne(d => d.TimeTableAllocation)
                    .WithMany(p => p.TimeTableLogs)
                    .HasForeignKey(d => d.TimeTableAllocationID)
                    .HasConstraintName("FK_TimeTableLog_TimeTableAllocation");
            });

            modelBuilder.Entity<TransactionHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_TransactionHead_AcademicYear");

                //entity.HasOne(d => d.Branch)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.BranchID)
                //    .HasConstraintName("FK_TransactionHead_Branches");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_TransactionHead_Companies");

                //entity.HasOne(d => d.Currency)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.CurrencyID)
                //    .HasConstraintName("FK_TransactionHead_Currencies");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.CustomerID)
                //    .HasConstraintName("FK_TransactionHead_Customers");

                //entity.HasOne(d => d.DeliveryMethod)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.DeliveryMethodID)
                //    .HasConstraintName("FK_TransactionHead_DeliveryTypes");

                //entity.HasOne(d => d.DeliveryType)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.DeliveryTypeID)
                //    .HasConstraintName("FK_TransactionHead_DeliveryTypeID");

                //entity.HasOne(d => d.DocumentStatus)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.DocumentStatusID)
                //    .HasConstraintName("FK_TransactionHead_DocumentReferenceStatusMap");

                //entity.HasOne(d => d.DocumentType)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.DocumentTypeID)
                //    .HasConstraintName("FK_TransactionHead_DocumentTypes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_TransactionHead_Employees");

                //entity.HasOne(d => d.Entitlement)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.EntitlementID)
                //    .HasConstraintName("FK_TransactionHead_EntityTypeEntitlements");

                //entity.HasOne(d => d.ReceivingMethod)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.ReceivingMethodID)
                //    .HasConstraintName("FK_TransactionHead_ReceivingMethods");

                //entity.HasOne(d => d.ReferenceHead)
                //    .WithMany(p => p.InverseReferenceHead)
                //    .HasForeignKey(d => d.ReferenceHeadID)
                //    .HasConstraintName("FK_TransactionHead_TransactionHead");

                //entity.HasOne(d => d.ReturnMethod)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.ReturnMethodID)
                //    .HasConstraintName("FK_TransactionHead_ReturnMethods");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_TransactionHead_School");

                //entity.HasOne(d => d.Employee1)
                //    .WithMany(p => p.TransactionHeads1)
                //    .HasForeignKey(d => d.StaffID)
                //    .HasConstraintName("FK_TransactionHead_Staff");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.StudentID)
                //    .HasConstraintName("FK_TransactionHead_Students");

                //entity.HasOne(d => d.Supplier)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.SupplierID)
                //    .HasConstraintName("FK_TransactionHead_Suppliers");

                //entity.HasOne(d => d.Branch1)
                //    .WithMany(p => p.TransactionHeads1)
                //    .HasForeignKey(d => d.ToBranchID)
                //    .HasConstraintName("FK_TransactionHead_Branches1");

                //entity.HasOne(d => d.TransactionRoleNavigation)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.TransactionRole)
                //    .HasConstraintName("FK_TransactionHead_Roles");

                //entity.HasOne(d => d.TransactionStatus)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.TransactionStatusID)
                //    .HasConstraintName("FK_TransactionHead_TransactionStatuses");
            });

            modelBuilder.Entity<TransportApplctnStudentMap>(entity =>
            {
                entity.HasKey(e => e.TransportApplctnStudentMapIID)
                    .HasName("PK_TransportApplctnStudentMapIID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsNewRider).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYears)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TransportApplctnStudentMaps_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_TransportApplicationstud_Classes1");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.GenderID)
                    .HasConstraintName("FK_TransportApplicationstud_Genders");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_TransportApplctnStudentMaps_Schools");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_TransportApplicationstud_Students");

                entity.HasOne(d => d.TransportApplicationStatus)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.TransportApplcnStatusID)
                    .HasConstraintName("FK_StudentApplicStud_TransportApplcnStatus");

                entity.HasOne(d => d.TransportApplication)
                    .WithMany(p => p.TransportApplctnStudentMaps)
                    .HasForeignKey(d => d.TransportApplicationID)
                    .HasConstraintName("FK_TransportApplctnStudentMaps_TransportApplications");
            });

            modelBuilder.Entity<TransportApplication>(entity =>
            {
                entity.HasKey(e => e.TransportApplicationIID)
                    .HasName("PK_TransportApplicationIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TransportApplications)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TransportApplications_AcademicYears1");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.TransportApplications)
                    .HasForeignKey(d => d.DropStopMapID)
                    .HasConstraintName("FK_TransportApplications_DropStopMap");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.TransportApplications)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_TransportApplications_Parents");

                entity.HasOne(d => d.RouteStopMap1)
                    .WithMany(p => p.TransportApplications1)
                    .HasForeignKey(d => d.PickupStopMapID)
                    .HasConstraintName("FK_TransportApplications_PickUpStop");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.TransportApplications)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_TransportApplications_Schools");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.TransportApplications)
                    .HasForeignKey(d => d.StreetID)
                    .HasConstraintName("FK_StudentAppli_Street");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.UnitGroup)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.UnitGroupID)
                    .HasConstraintName("FK_Units_UnitGroup");
            });

            modelBuilder.Entity<UnitGroup>(entity =>
            {
                entity.Property(e => e.UnitGroupID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });


            modelBuilder.Entity<UploadDocument>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.ContentType)
                //    .WithMany(p => p.UploadDocuments)
                //    .HasForeignKey(d => d.ContentTypeID)
                //    .HasConstraintName("FK_UploadDocument_ContentTypes");

                entity.HasOne(d => d.UploadDocumentType)
                    .WithMany(p => p.UploadDocuments)
                    .HasForeignKey(d => d.UploadDocumentTypeID)
                    .HasConstraintName("FK_UploadDocuments_UploadDocumentTypes");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Vehicles_AcademicYear");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.RigistrationCityID)
                    .HasConstraintName("FK_Vehicles_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.RigistrationCountryID)
                    .HasConstraintName("FK_Vehicles_Countries");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Vehicles_School");

                entity.HasOne(d => d.VehicleTransmission)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.TransmissionID)
                    .HasConstraintName("FK_Vehicles_VehicleTransmissions");

                entity.HasOne(d => d.VehicleOwnershipType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleOwnershipTypeID)
                    .HasConstraintName("FK_Vehicles_VehicleOwnershipTypes");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleTypeID)
                    .HasConstraintName("FK_Vehicles_VehicleTypes");
            });

            modelBuilder.Entity<VehicleDetailMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.VehicleDetailMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_VehicleDetailMaps_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.VehicleDetailMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_VehicleDetailMaps_School");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleDetailMaps)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_VehicleDetailMaps_Vehicles");
            });

            modelBuilder.Entity<VehicleOwnershipType>(entity =>
            {
                entity.Property(e => e.VehicleOwnershipTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });


            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.VehicleTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<VisaDetailMap>(entity =>
            {
                entity.HasKey(e => e.VisaDetailsIID)
                    .HasName("PK_VisaDetails");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Visitor_Login");
            });

            modelBuilder.Entity<VisitorAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitorAttachmentMaps)
                    .HasForeignKey(d => d.VisitorID)
                    .HasConstraintName("FK_VisitorAttachmentMap_Visitor");
            });

            modelBuilder.Entity<VisitorBook>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.VisitingPurpose)
                    .WithMany(p => p.VisitorBooks)
                    .HasForeignKey(d => d.VisitingPurposeID)
                    .HasConstraintName("FK_VisitorBooks_VisitingPurposes");
            });

            modelBuilder.Entity<VolunteerType>(entity =>
            {
                entity.Property(e => e.VolunteerTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<WeekDay>(entity =>
            {
                entity.Property(e => e.WeekDayID).ValueGeneratedNever();

                entity.HasOne(d => d.ClassTimingSet)
                    .WithMany(p => p.WeekDays)
                    .HasForeignKey(d => d.ClassTimingSetID)
                    .HasConstraintName("FK_WeekDays_WeekDays");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.WeekDays)
                    .HasForeignKey(d => d.DayID)
                    .HasConstraintName("FK_WeekDays_Days");
            });

            modelBuilder.Entity<Workflow>(entity =>
            {
                //entity.HasOne(d => d.LinkedEntityType)
                //    .WithMany(p => p.Workflows)
                //    .HasForeignKey(d => d.LinkedEntityTypeID)
                //    .HasConstraintName("FK_Workflows_EntityTypes");

                //entity.HasOne(d => d.WorkflowApplyField)
                //    .WithMany(p => p.Workflows)
                //    .HasForeignKey(d => d.WorkflowApplyFieldID)
                //    .HasConstraintName("FK_Workflows_WorkflowFileds");

                //entity.HasOne(d => d.WorkflowType)
                //    .WithMany(p => p.Workflows)
                //    .HasForeignKey(d => d.WorkflowTypeID)
                //    .HasConstraintName("FK_Workflows_WorkflowTypes");
            });

            //modelBuilder.Entity<WorkflowCondition>(entity =>
            //{
            //    entity.Property(e => e.WorkflowConditionID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<WorkflowEntity>(entity =>
            {
                entity.Property(e => e.WorkflowEntityID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<WorkflowFiled>(entity =>
            //{
            //    entity.Property(e => e.WorkflowFieldID).ValueGeneratedNever();

            //    entity.HasOne(d => d.WorkflowType)
            //        .WithMany(p => p.WorkflowFileds)
            //        .HasForeignKey(d => d.WorkflowTypeID)
            //        .HasConstraintName("FK_WorkflowFileds_WorkflowTypes");
            //});

            //modelBuilder.Entity<WorkflowLogMap>(entity =>
            //{
            //    entity.HasOne(d => d.Workflow)
            //        .WithMany(p => p.WorkflowLogMaps)
            //        .HasForeignKey(d => d.WorkflowID)
            //        .HasConstraintName("w");

            //    entity.HasOne(d => d.WorkflowStatus)
            //        .WithMany(p => p.WorkflowLogMaps)
            //        .HasForeignKey(d => d.WorkflowStatusID)
            //        .HasConstraintName("FK_WorkflowLogMaps_WorkflowStatuses");
            //});

            //modelBuilder.Entity<WorkflowLogMapRuleApproverMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_Employees");

            //    entity.HasOne(d => d.WorkflowCondition)
            //        .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowConditionID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowConditions");

            //    entity.HasOne(d => d.WorkflowLogMapRuleMap)
            //        .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowLogMapRuleMapID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowLogMapRuleMaps");

            //    entity.HasOne(d => d.WorkflowRuleCondition)
            //        .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowRuleConditionID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowRuleConditions");

            //    entity.HasOne(d => d.WorkflowStatus)
            //        .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowStatusID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowStatuses");
            //});

            //modelBuilder.Entity<WorkflowLogMapRuleMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.WorkflowCondition)
            //        .WithMany(p => p.WorkflowLogMapRuleMaps)
            //        .HasForeignKey(d => d.WorkflowConditionID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowConditions");

            //    entity.HasOne(d => d.WorkflowLogMap)
            //        .WithMany(p => p.WorkflowLogMapRuleMaps)
            //        .HasForeignKey(d => d.WorkflowLogMapID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowLogMaps");

            //    entity.HasOne(d => d.WorkflowRule)
            //        .WithMany(p => p.WorkflowLogMapRuleMaps)
            //        .HasForeignKey(d => d.WorkflowRuleID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowRules");

            //    entity.HasOne(d => d.WorkflowStatus)
            //        .WithMany(p => p.WorkflowLogMapRuleMaps)
            //        .HasForeignKey(d => d.WorkflowStatusID)
            //        .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowStatuses");
            //});

            //modelBuilder.Entity<WorkflowRule>(entity =>
            //{
            //    entity.HasOne(d => d.Condition)
            //        .WithMany(p => p.WorkflowRules)
            //        .HasForeignKey(d => d.ConditionID)
            //        .HasConstraintName("FK_WorkflowRules_WorkflowConditions");

            //    entity.HasOne(d => d.Workflow)
            //        .WithMany(p => p.WorkflowRules)
            //        .HasForeignKey(d => d.WorkflowID)
            //        .HasConstraintName("FK_WorkflowRules_Workflows");
            //});

            //modelBuilder.Entity<WorkflowRuleApprover>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.WorkflowRuleApprovers)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_WorkflowRuleApprovers_Employees");

            //    entity.HasOne(d => d.WorkflowRuleCondition)
            //        .WithMany(p => p.WorkflowRuleApprovers)
            //        .HasForeignKey(d => d.WorkflowRuleConditionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_WorkflowRuleApprovers_WorkflowRuleConditions");
            //});

            //modelBuilder.Entity<WorkflowRuleCondition>(entity =>
            //{
            //    entity.HasOne(d => d.Condition)
            //        .WithMany(p => p.WorkflowRuleConditions)
            //        .HasForeignKey(d => d.ConditionID)
            //        .HasConstraintName("FK_WorkflowRuleConditions_WorkflowConditions");

            //    entity.HasOne(d => d.WorkflowRule)
            //        .WithMany(p => p.WorkflowRuleConditions)
            //        .HasForeignKey(d => d.WorkflowRuleID)
            //        .HasConstraintName("FK_WorkflowRuleConditions_WorkflowRules");
            //});

            //modelBuilder.Entity<WorkflowStatus>(entity =>
            //{
            //    entity.HasOne(d => d.WorkflowType)
            //        .WithMany(p => p.WorkflowStatus)
            //        .HasForeignKey(d => d.WorkflowTypeID)
            //        .HasConstraintName("FK_WorkflowStatuses_WorkflowTypes");
            //});

            //modelBuilder.Entity<WorkflowTransactionHeadMap>(entity =>
            //{
            //    entity.HasOne(d => d.TransactionHead)
            //        .WithMany(p => p.WorkflowTransactionHeadMaps)
            //        .HasForeignKey(d => d.TransactionHeadID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadMaps_TransactionHead");

            //    entity.HasOne(d => d.Workflow)
            //        .WithMany(p => p.WorkflowTransactionHeadMaps)
            //        .HasForeignKey(d => d.WorkflowID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadMaps_Workflows");

            //    entity.HasOne(d => d.WorkflowStatus)
            //        .WithMany(p => p.WorkflowTransactionHeadMaps)
            //        .HasForeignKey(d => d.WorkflowStatusID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadMaps_WorkflowStatuses");
            //});

            //modelBuilder.Entity<WorkflowTransactionHeadRuleMap>(entity =>
            //{
            //    entity.HasOne(d => d.WorkflowCondition)
            //        .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
            //        .HasForeignKey(d => d.WorkflowConditionID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowConditions");

            //    entity.HasOne(d => d.WorkflowRule)
            //        .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
            //        .HasForeignKey(d => d.WorkflowRuleID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowRules");

            //    entity.HasOne(d => d.WorkflowStatus)
            //        .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
            //        .HasForeignKey(d => d.WorkflowStatusID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowStatuses");

            //    entity.HasOne(d => d.WorkflowTransactionHeadMap)
            //        .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
            //        .HasForeignKey(d => d.WorkflowTransactionHeadMapID)
            //        .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowTransactionHeadMaps");
            //});

            //modelBuilder.Entity<WorkflowTransactionRuleApproverMap>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_Employees");

            //    entity.HasOne(d => d.WorkflowCondition)
            //        .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowConditionID)
            //        .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowConditions");

            //    entity.HasOne(d => d.WorkflowRuleCondition)
            //        .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowRuleConditionID)
            //        .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowRuleConditions");

            //    entity.HasOne(d => d.WorkflowStatus)
            //        .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowStatusID)
            //        .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowStatuses");

            //    entity.HasOne(d => d.WorkflowTransactionHeadRuleMap)
            //        .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
            //        .HasForeignKey(d => d.WorkflowTransactionHeadRuleMapID)
            //        .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowTransactionHeadRuleMaps");
            //});

            //modelBuilder.Entity<WorkflowType>(entity =>
            //{
            //    entity.Property(e => e.WorkflowTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Zone>(entity =>
            //{
            //    entity.Property(e => e.ZoneID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Country)
            //        .WithMany(p => p.Zones)
            //        .HasForeignKey(d => d.CountryID)
            //        .HasConstraintName("FK_Zones_Countries");
            //});

            modelBuilder.Entity<StaticContentData>(entity =>
            {
                entity.Property(e => e.ContentDataIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.StaticContentType)
                    .WithMany(p => p.StaticContentDatas)
                    .HasForeignKey(d => d.ContentTypeID)
                    .HasConstraintName("FK_StaticContentDatas_StaticContentTypes");
            });

            modelBuilder.Entity<StaticContentType>(entity =>
            {
                entity.Property(e => e.ContentTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<VisitorBook>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.VisitingPurpose)
                    .WithMany(p => p.VisitorBooks)
                    .HasForeignKey(d => d.VisitingPurposeID)
                    .HasConstraintName("FK_VisitorBooks_VisitingPurposes");
            });

            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.Property(e => e.SequenceID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SchoolPayerBankDetailMap>(entity =>
            {
                entity.HasKey(e => e.PayerBankDetailIID)
                    .HasName("PK_PayerBankDetails");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.SchoolPayerBankDetailMaps)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_PayerBankDetails_Bank");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolPayerBankDetailMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_PayerBankDetails_School");
            });
            modelBuilder.Entity<CounselorHubMap>(entity =>
            {
                entity.HasOne(d => d.Class)
                    .WithMany(p => p.CounselorHubMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_CounselorHubMaps_Classes");

                entity.HasOne(d => d.CounselorHub)
                    .WithMany(p => p.CounselorHubMaps)
                    .HasForeignKey(d => d.CounselorHubID)
                    .HasConstraintName("FK_CounselorHubMaps_CounselorHub");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.CounselorHubMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_CounselorHubMaps_Sections");


                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CounselorHubMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_CounselorHubMaps_Student");
            });

            modelBuilder.Entity<VehicleTracking>(entity =>
            {
                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.VehicleTrackings)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_VehicleTracking_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.VehicleTrackings)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_VehicleTrackings_Employee");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.VehicleTrackings)
                    .HasForeignKey(d => d.RouteID)
                    .HasConstraintName("FK_VehicleTracking_Route");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.VehicleTrackings)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_VehicleTracking_School");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleTrackings)
                    .HasForeignKey(d => d.VehicleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleTrackings_Vehicles");
            });

            modelBuilder.Entity<CounselorHubAttachmentMap>(entity =>
            {
                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CounselorHub)
                    .WithMany(p => p.CounselorHubAttachmentMaps)
                    .HasForeignKey(d => d.CounselorHubID)
                    .HasConstraintName("FK_CounselorHubAttachmentMaps_CounselorHub");
            });

            modelBuilder.Entity<CounselorHub>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.CounselorHubs)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Couns_AcademicYear");

                entity.HasOne(d => d.CounselorHubStatus)
                    .WithMany(p => p.CounselorHubs)
                    .HasForeignKey(d => d.CounselorHubStatusID)
                    .HasConstraintName("FK_CounselorHubs_CounselorHubStatus");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.CounselorHubs)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Couns_School");
            });

            modelBuilder.Entity<ClassSectionSubjectPeriodMap>(entity =>
            {
                entity.HasKey(e => e.PeriodMapIID)
                    .HasName("PK__ClassSec__38D6C0497785412E");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.ClassSectionSubjectPeriodMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_ClassSectionSubjectPeriodMaps_AcademicYears");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassSectionSubjectPeriodMaps)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_ClassSectionSubjectPeriodMaps_Class");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClassSectionSubjectPeriodMaps)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_ClassSectionSubjectPeriodMaps_Section");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSectionSubjectPeriodMaps)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_ClassSectionSubjectPeriodMaps_Subject");
            });

            modelBuilder.Entity<ParentPortalSchoolAcademicClassMap>(entity =>
            {
                entity.HasKey(e => e.ClassMapIID)
                    .HasName("PK_ClassMapIID");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasKey(e => e.ChapterIID)
                    .HasName("PK_Chapter");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Chapters_AcademicYear");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Chapters_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Chapters_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Chapters_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_Chapters_Subjects");
            });

            modelBuilder.Entity<SubjectUnit>(entity =>
            {
                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.SubjectUnits)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_SubjectUnits_AcademicYear");

                entity.HasOne(d => d.Chapter)
                .WithMany(p => p.SubjectUnits)
                .HasForeignKey(d => d.ChapterID)
                .HasConstraintName("FK_SubjectUnits_Chapters");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SubjectUnits)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_SubjectUnits_Classes");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SubjectUnits)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_SubjectUnits_School");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SubjectUnits)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_SubjectUnits_Sections");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectUnits)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_SubjectUnits_Subjects");
                entity.HasOne(d => d.ParentSubjectUnit)
                  .WithMany(p => p.InverseParentSubjectUnit)
                  .HasForeignKey(d => d.ParentSubjectUnitID)
                  .HasConstraintName("FK_SubjectUnits_SubjectUnits");
            });

            modelBuilder.Entity<LessonLearningObjective>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<StudentFeeRelationMap>(entity =>
            {
                entity.HasKey(e => e.SFRM_IID)
                    .HasName("PK__StudentF__5988874F73B58985");
            });

            modelBuilder.Entity<TimeTableExtClass>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtClassIID)
                    .HasName("TimeTableExtClassIID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TimeTableExtClasses)
                    .HasForeignKey(d => d.ClassID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtClass_Classes");

                entity.HasOne(d => d.TimeTableExt)
                    .WithMany(p => p.TimeTableExtClasses)
                    .HasForeignKey(d => d.TimeTableExtID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtClass_TimeTableExt");
            });

            modelBuilder.Entity<TimeTableExtClassTiming>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtClassTimingIID)
                    .HasName("TimeTableExtClassTimingIID");

                entity.HasOne(d => d.ClassTiming)
                    .WithMany(p => p.TimeTableExtClassTimings)
                    .HasForeignKey(d => d.ClassTimingID)
                    .HasConstraintName("FK_TimeTableExtClassTiming_ClassTiming");

                entity.HasOne(d => d.LogicalOperator)
                    .WithMany(p => p.TimeTableExtClassTimings)
                    .HasForeignKey(d => d.LogicalOperatorID)
                    .HasConstraintName("FK_TimeTableExtClassTiming_LogicalOperator");

                entity.HasOne(d => d.TimeTableExt)
                    .WithMany(p => p.TimeTableExtClassTimings)
                    .HasForeignKey(d => d.TimeTableExtID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtClassTiming_TimeTableExt");
            });

            modelBuilder.Entity<TimeTableExtSection>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtSectionIID)
                    .HasName("TimeTableExtSectionIID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TimeTableExtSections)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_TimeTableExtSection_Classes");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TimeTableExtSections)
                    .HasForeignKey(d => d.SectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtSection_Sections");

                entity.HasOne(d => d.TimeTableExt)
                    .WithMany(p => p.TimeTableExtSections)
                    .HasForeignKey(d => d.TimeTableExtID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtSection_TimeTableExt");
            });

            modelBuilder.Entity<TimeTableExtSubject>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtSubjectIID)
                    .HasName("TimeTableExtSubjectIID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TimeTableExtSubjects)
                    .HasForeignKey(d => d.SubjectID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtSubject_Subjects");

                entity.HasOne(d => d.TimeTableExt)
                    .WithMany(p => p.TimeTableExtSubjects)
                    .HasForeignKey(d => d.TimeTableExtID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtSubject_TimeTableExt");
            });

            modelBuilder.Entity<TimeTableExtTeacher>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtTeacherIID)
                    .HasName("TimeTableExtTeacherIID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TimeTableExtTeachers)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_TimeTableExtTeacher_Subjects");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TimeTableExtTeachers)
                    .HasForeignKey(d => d.TeacherID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtTeacher_Teacher");

                entity.HasOne(d => d.TimeTableExt)
                    .WithMany(p => p.TimeTableExtTeachers)
                    .HasForeignKey(d => d.TimeTableExtID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtTeacher_TimeTableExt");
            });

            modelBuilder.Entity<TimeTableExtWeekDay>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtWeekDayIID)
                    .HasName("TimeTableExtWeekDayIID");

                entity.HasOne(d => d.LogicalOperator)
                    .WithMany(p => p.TimeTableExtWeekDays)
                    .HasForeignKey(d => d.LogicalOperatorID)
                    .HasConstraintName("FK_TimeTableExtWeekDay_LogicalOperator");

                entity.HasOne(d => d.TimeTableExt)
                    .WithMany(p => p.TimeTableExtWeekDays)
                    .HasForeignKey(d => d.TimeTableExtID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtWeekDay_TimeTableExt");

                entity.HasOne(d => d.WeekDay)
                    .WithMany(p => p.TimeTableExtWeekDays)
                    .HasForeignKey(d => d.WeekDayID)
                    .HasConstraintName("FK_TimeTableExtWeekDay_WeekDays");
            });

            modelBuilder.Entity<TimeTableExtension>(entity =>
            {
                entity.HasKey(e => e.TimeTableExtIID)
                    .HasName("PK_TimeTableExtIID");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TimeTableExtensions)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_TimeTableExtension_AcademicYears");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.TimeTableExtensions)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_TimeTableExtension_Schools");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.TimeTableExtensions)
                    .HasForeignKey(d => d.SubjectTypeID)
                    .HasConstraintName("FK_TimeTableExtension_SubjectTypes");

                entity.HasOne(d => d.TimeTable)
                    .WithMany(p => p.TimeTableExtensions)
                    .HasForeignKey(d => d.TimeTableID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeTableExtension_TimeTables");
            });

            modelBuilder.Entity<TransportCancelRequest>(entity =>
            {
                entity.HasKey(e => e.RequestIID)
                    .HasName("PK_RequestIID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TransportCancelRequests)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_TransportCancelRequests_Status");

                entity.HasOne(d => d.StudentRouteStopMap)
                    .WithMany(p => p.TransportCancelRequests)
                    .HasForeignKey(d => d.StudentRouteStopMapID)
                    .HasConstraintName("FK_TransportCancelRequests_StudentTransport");
            });

            modelBuilder.Entity<LessonPlanSkillDevelopmentMap>(entity =>
            {
                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanSkillDevelopmentMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanSkillDevelopmentMap_LessonPlan");
            });

            modelBuilder.Entity<LessonPlanAssessmentMap>(entity =>
            {
                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanAssessmentMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_LessonPlanAssessmentMap_LessonPlan");
            });

            modelBuilder.Entity<LessonPlanTeachingMethodologyMap>(entity =>
            {
                entity.HasKey(e => e.TeachingMethodologyMapIID)
                    .HasName("PK_TeachingMethodologyMaps");

                entity.HasOne(d => d.LessonPlan)
                    .WithMany(p => p.LessonPlanTeachingMethodologyMaps)
                    .HasForeignKey(d => d.LessonPlanID)
                    .HasConstraintName("FK_TeachingMethodologyMap_LessonPlan");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}