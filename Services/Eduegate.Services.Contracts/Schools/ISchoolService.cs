    using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Services.Contracts.Schedulers;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Accounts;
using Eduegate.Services.Contracts.School.Attendences;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Fines;
using Eduegate.Services.Contracts.School.Galleries;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.Settings;
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.School.Payment;
using Eduegate.Services.Contracts.School.CounselorHub;
namespace Eduegate.Services.Contracts.Schools
{
    public interface ISchoolService
    {
        List<StudentApplicationDTO> GetStudentApplication(long loginID);

        OperationResult DeleteApplication(long applicationId);

        OperationResult DeleteSiblingApplication(long applicationId);

        StudentApplicationDTO GetApplication(long applicationId);

        StudentAttendenceDTO GetStudentAttendence(long studentID, DateTime date);

        StaffAttendenceDTO GetStaffAttendence(long staffID, DateTime date);

        string SaveStudentAttendence(StudentAttendenceDTO attendence);

        string SaveAcademicCalendar(AcadamicCalendarDTO acadamic);

        string SaveStaffAttendance(StaffAttendenceDTO attendence);

        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId);

        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId);

        List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year);

        List<PresentStatusDTO> GetPresentStatuses();

        //List<ClassSubjectMapDTO> GetSubjectByClassID(int classID);

        string SaveTimeTable(TimeTableAllocationDTO timeTableAlloc);

        string SaveTimeTableLog(TimeTableLogDTO timeTableAlloc);

        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByClassID(int classID, int tableMasterId);

        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate);

        void DeleteTimeTableEntry(long timeTableAllocationID);

        void DeleteDailyTimeTableEntry(long timeTableLogID);

        List<SubjectTeacherMapDTO> GetTeacherBySubject(int classId, int SectionId, int subjectId);

        string SaveStudentFeeDue(StudentFeeDueDTO feeDueInfo);

        string SaveStudentApplication(StudentApplicationDTO studentApplication);

        string SendTodayAttendancePushNotification(int classId, int sectionId);

        string ResentFromLeadLoginCredentials(StudentApplicationDTO studentApplication);

        OperationResultDTO GenerateTimeTable(TimeTableAllocationDTO timeTableLog);

        List<StudentFeeDueDTO> FillFeeDue(int classId, long studentId);

        List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId);

        List<KeyValueDTO> GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID);

        List<FeeDueMonthlySplitDTO> GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID);

        List<StudentFeeDueDTO> GetFeesByInvoiceNo(long studentFeeDueID);

        List<KeyValueDTO> GetClassStudents(int classId, int sectionId);

        List<KeyValueDTO> GetClassStudentsAll(int academicYearID, List<int> classList, List<int> sectionList);

        List<AcademicYearDTO> GetAcademicYearDataByCalendarID(long calendarID);

        List<AcademicYearDTO> GetAcademicYear(int academicYearID);

        List<KeyValueDTO> GetAdvancedAcademicYearBySchool(int schoolID);

        List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId);

        List<RemarksEntryStudentsDTO> GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID);

        List<HealthEntryStudentMapDTO> GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID);

        List<EventTransportAllocationMapDTO> GetStudentandStaffsByRouteIDforEvent(EventTransportAllocationDTO eventDto);

        EventTransportAllocationMapDTO GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType);

        EventTransportAllocationMapDTO GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType);

        List<RouteShiftingStudentMapDTO> GetStudentDatasFromRouteID(int routeId);

        List<ClassSectionSubjectListMapDTO> FillClassandSectionWiseSubjects(int classID, int sectionID, int IID);

        List<RouteShiftingStaffMapDTO> GetStaffDatasFromRouteID(int routeId);

        List<CampusTransferMapDTO> GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId);

        List<StudentDTO> GetStudentsSiblings(long parentId);

        List<AgeCriteriaMapDTO> GetAgeCriteriaByClassID(int classId, int academicYearID);

        LibraryBookDTO GetBooKCategoryName(long bookCategoryCodeId);

        LibraryTransactionDTO GetBookQuantityDetails(string CallAccNo, int? bookMapID);

        LibraryTransactionDTO GetIssuedBookDetails(string CallAccN, int? bookMapID);

        List<CircularListDTO> GetCircularList(long parentId);

        List<CounselorHubListDTO> GetCounselorList(long parentId);
        List<CircularListDTO> GetCircularListByStudentID(long studentID);
        List<GalleryDTO> GetGalleryView(long academicYearID);

        List<AgendaDTO> GetAgendaList(long loginID);

        List<LessonPlanDTO> GetLessonPlanList(long studentID);

        List<FeeCollectionDTO> GetStudentFeeCollection(long studentId);

        StudentRouteStopMapDTO GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID);

        StudentRouteStopMapDTO GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID);

        StaffRouteStopMapDTO GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID);

        StaffRouteStopMapDTO GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID);

        List<AssignmentDTO> GetAssignmentStudentwise(long studentId, int? SubjectID);

        List<MarkListViewDTO> GetMarkListStudentwise(long studentId);

        ExamDTO GetClassByExam(int examId);

        List<FeePeriodsDTO> GetFeePeriodMonthlySplit(List<int> feePeriodIds);

        List<MarkRegisterDetailsSplitDTO> GetExamSubjectsMarks(long studentId, long examId, int ClassId);

        List<MarkGradeMapDTO> GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId);

        List<CollectFeeAccountDetailDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID);

        List<StudentDTO> GetStudentDetails(long studentId);

        List<StudentTransportDetailDTO> GetStudentTransportDetails(long studentID);
        TransportCancellationDTO GetStudentTransportCancellationDetails(long studentID);
        OperationResultDTO RevertTransportCancellation(long RequestIID);
        List<StudentBehavioralRemarksDTO> GetStudentBehavioralRemarks(StudentBehavioralRemarksDTO studentBehavioralRemarksDTO);

        TransportApplicationDTO GetTransportStudentDetailsByParentLoginID(long id);

        GuardianDTO GetGuardianDetails(long studentId);

        string FeeAccountPosting(DateTime fromDate, DateTime toDate, long cashierID);

        StudentLeaveApplicationDTO GetLeaveApplication(long leaveapplicationId);

        OperationResult DeleteLeaveApplication(long leaveapplicationId);

        List<StudentLeaveApplicationDTO> GetStudentLeaveApplication(long leaveapplicationId);

        List<TimeTableAllocationDTO> GetClassTimeTable(long studentId);

        List<ExamDTO> GetExamLists(long studentId);

        StudentTransferRequestDTO GetTransferApplication(long studentId);

        OperationResult DeleteTransferApplication(long studentId);

        List<StudentTransferRequestDTO> GetStudentTransferApplication(long studentId);

        StudentApplicationDTO GetApplicationByLoginID(long loginID);

        List<NotificationAlertsDTO> GetNotificationAlerts(long loginID , int page = 1, int pageSize = 100);

        List<NotificationAlertsDTO> GetAllNotificationAlerts(long loginID);

        List<MailBoxDTO> GetSendMailFromParent(long loginID);

        int GetNotificationAlertsCount(long loginID);

        string MarkNotificationAsRead(long loginID, long notificationAlertIID);

        List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentId);

        List<FeeCollectionDTO> GetSiblingDueDetailsFromStudentID(long StudentID);

        List<StudentFeeDueDTO> GetFineCollected(long studentId);

        List<StudentFeeDueDTO> FillFineDue(int classId, long studentId);

        List<StudentSkillRegisterSplitDTO> GetStudentSkills(long skillId);

        List<MarkGradeMapDTO> GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID);

        ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID);

        ProductBundleDTO GetProductDetails(long productId);

        EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate);

        ProductBundleDTO GetProductBundleData(long productskuId);

        FineMasterStudentMapDTO GetFineAmount(int fineMasterID);

        List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId);

        List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId, int academicId);


        List<FeeCollectionDTO> FillFeeDueDataForSettlement(long studentId, int academicId);

        List<FeeCollectionPreviousFeesDTO> GetIssuedCreditNotesForCollectedFee(long studentId);

        EmployeesDTO GetEmployeeFromEmployeeID(long employeeID);

        AccountsGroupDTO GetGroupCodeByParentGroup(long parentGroupID);

        AccountsDTO GetAccountCodeByGroup(long groupID);

        StudentDTO GetStudentDetailFromStudentID(long StudentID);

        List<CurrencyDTO> GetCurrencyDetails();

        List<FeeCollectionDTO> AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative);

        string DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID);

        SchoolCreditNoteDTO AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type);

        GuardianDTO GetParentDetailFromParentID(long ParentID);

        List<KeyValueDTO> GetFeeStructure(int academicYearID);

        List<KeyValueDTO> GetSubGroup(int mainGroupID);

        List<KeyValueDTO> GetAccountCodeByLedger(int ledgerGroupID);

        List<KeyValueDTO> GetAccountGroup(int subGroupID);

        List<KeyValueDTO> GetAccountByGroupID(int groupID);

        List<KeyValueDTO> GetCostCenterByAccount(long accountID);

        List<KeyValueDTO> GetAccountByPayementModeID(long paymentModeID);

        List<KeyValueDTO> GetAccountGroupByAccountID(long accountID);

        List<KeyValueDTO> GetFeePeriod(int academicYearID, long studentID, int? feeMasterID);

        List<KeyValueDTO> GetTransportFeePeriod(int academicYearID);

        List<AcademicCalenderEventDateDTO> GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID);

        List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year);

        void DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID);

        int GetStudentsCount();

        List<KeyValueDTO> GetCastByRelegion(int relegionID);

        List<KeyValueDTO> GetToNotificationUsersByUser(int userID, int branchID, string user);

        List<KeyValueDTO> GetStreamByStreamGroup(byte? streamGroupID);

        List<KeyValueDTO> GetStreamCompulsorySubjects(byte? streamGroupID);

        List<KeyValueDTO> GetStreamOptionalSubjects(byte? streamGroupID);

        List<StreamDTO> GetFullStreamListDatas();

        List<KeyValueDTO> GetSubSkillByGroup(int skillGroupID);

        List<KeyValueDTO> GetAcademicYearBySchool(int schoolID, bool? isActive);
        List<KeyValueDTO> GetClassesByAcademicYearID(int academicYearID);

        List<KeyValueDTO> GetClasseByAcademicyear(int academicyearID);

        List<KeyValueDTO> GetClassesBySchool(byte schoolID);

        List<KeyValueDTO> GetSectionsBySchool(byte schoolID);

        List<ClassDTO> GetClassListBySchool(byte schoolID);

        string GetProgressReportName(long schoolID, int? classID);

        List<KeyValueDTO> GetRouteStopsByRoute(int routeID);

        List<KeyValueDTO> GetSubjectByClass(int classID);

        List<KeyValueDTO> GetSubjectbyQuestionGroup(long questionGroupID);

        OnlineExamQuestionDTO GetQuestionDetailsByQuestionID(long questionID);

        StaticContentDataDTO GetAboutandContactDetails(long contentID);

        LibraryTransactionDTO GetLibraryStudentFromStudentID(long studentID);

        LibraryTransactionDTO GetLibraryStaffFromEmployeeID(long employeeID);

        TransportApplicationDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID);

        List<SubjectMarkEntryDetailDTO> FillClassStudents(long classID, long sectionID);

        List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examID, int classID, int sectionID, int subjectID);

        ClassSubjectSkillGroupMapDTO GetExamMarkDetails(long subjectID, long examID);

        List<KeyValueDTO> GetTeacherEmailByParentLoginID(long loginID);

        CandidateDTO GetCandidateDetails(string username, string password);

        string ShiftStudentSection(ClassSectionShiftingDTO classSectionShiftingDTO);

        ClassSectionShiftingDTO GetStudentsForShifting(int classID, int sectionID);

        List<KeyValueDTO> GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID);

        List<KeyValueDTO> GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID);

        List<KeyValueDTO> GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID);

        List<KeyValueDTO> GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID);

        List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);


        List<MarkRegisterDetailsDTO> GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);

        string SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO);

        List<MarkRegisterDetailsDTO> GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);

        string SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO);

        string UpdateMarkEntryStatus(MarkRegisterDTO dto);

        List<StudentMarkEntryDTO> GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);

        string SaveMarkEntry(MarkRegisterDTO markRegisterDTO);

        List<KeyValueDTO> GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO);

        List<KeyValueDTO> GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO);

        List<KeyValueDTO> GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO);

        List<KeyValueDTO> GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID);
        List<KeyValueDTO> GetExamGroupsByAcademicYearID(int? academicYearID);

        List<KeyValueDTO> GetSubjectByType(byte subjectTypeID);

        List<KeyValueDTO> GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID);

        List<MarkRegisterDetailsDTO> GetSubjectsAndMarksToPublish(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);

        string GetAcademicYearNameByID(int academicYearID);

        string GetSchoolNameByID(int schoolID);

        List<TransportApplicationStudentMapDTO> GetTransportApplication(long loginID);

        List<SchoolGeoLocationDTO> GetGeoSchoolSetting(long schoolID);

        void SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dto);

        void ClearGeoSchoolSetting(int schoolID);

        List<AcademicClassMapWorkingDayDTO> GetMonthAndYearByAcademicYearID(int? academicYearID);

        List<AcademicCalenderEventDateDTO> GetAcademicMonthAndYearByCalendarID(long? calendarID);

        List<FeeCollectionDTO> FillCollectedFeesDetails(long studentId, int academicId);

        string CheckAndInsertCalendarEntries(long calendarID);

        List<KeyValueDTO> GetCalendarByTypeID(byte calendarTypeID);

        List<KeyValueDTO> GetBookDetailsByCallNo(string CallAccNo);

        List<AcademicCalenderEventDateDTO> GetCalendarEventsByCalendarID(long calendarID);

        List<EmployeesDTO> GetEmployeesByCalendarID(long calendarID);

        List<PresentStatusDTO> GetStaffPresentStatuses();

        //LibraryBookDTO GetBookDetailsChange(long BookID);

        List<SchoolCreditNoteDTO> GetCreditNoteNumber(long? headID, long studentID);

        List<KeyValueDTO> GetSchoolsByParentLoginID(long? loginID);

        AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID);

        List<OnlineExamQuestionDTO> GetQuestionsByCandidateID(long candidateID);

        StudentApplicationDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob);

        List<KeyValueDTO> GetUnitByUnitGroup(int groupID);

        List<UnitDTO> GetUnitDataByUnitGroup(int groupID);

        List<OnlineExamResultDTO> GetOnlineExamsResultByCandidateID(long candidateID);

        string SubmitAmountAsLog(decimal? totalAmount);

        PaymentLogDTO GetLastLogData();

        PaymentMasterVisaDTO GetPaymentMasterVisaData();
        PaymentMasterVisaDTO GetPaymentMasterVisaDataByTrackID(long trackID);
        PaymentMasterVisaDTO UpdatePaymentMasterVisa(PaymentMasterVisaDTO paymentMasterVisaDTO);

        string SaveQPayPayment(PaymentQPAYDTO paymentQPAYDTO);

        OperationResultDTO FeeCollectionEntry(List<FeeCollectionDTO> feeCollectionList);

        List<FeeCollectionDTO> UpdateStudentsFeePaymentStatus(string transactionNo, long? parentLoginID);

        List<FeeCollectionDTO> GetStudentFeeCollectionsHistory(StudentDTO studentData, byte? schoolID, int? academicYearID);

        List<AcademicYearDTO> GetCurrentAcademicYearsData();

        List<KeyValueDTO> GetAllAcademicYearBySchoolID(int schoolID);

        List<FeeCollectionDTO> GetFeeCollectionHistories(List<StudentDTO> studentDatas, byte? schoolID, int? academicYearID);

        PaymentLogDTO GetAndInsertLogDataByTransactionID(string transID);

        string CheckFeeCollectionStatusByTransactionNumber(string transactionNumber);

        List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber, string mailID, string feeReceiptNo = null);

        OperationResult DeleteTransportApplication(long transportapplicationId);

        List<KeyValueDTO> GetRoutesByRouteGroupID(int? routeGroupID);

        AcademicYearDTO GetAcademicYearDataByGroupID(int? routeGroupID);

        List<KeyValueDTO> GetPickupStopMapsByRouteGroupID(int? routeGroupID);

        List<KeyValueDTO> GetDropStopMapsByRouteGroupID(int? routeGroupID);

        List<StudentPickupRequestDTO> GetStudentPickupRequestsByLoginID(long loginID);

        List<KeyValueDTO> GetSubLedgerByAccount(long accountID);

        List<ProgressReportNewDTO> SaveProgressReportData(List<ProgressReportNewDTO> toDtoList);

        string UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList);

        List<ProgressReportNewDTO> GetProgressReportData(int classID, int? sectionID, int academicYearID);

        List<ProgressReportNewDTO> GetProgressReportList(long studentID, int classID, int sectionID, int academicYearID);

        string CancelStudentPickupRequestByID(long pickupRequestID);

        string CancelTransportApplication(long mapIID);

        TransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID);

        List<KeyValueDTO> GetStudentDetailsByStaff(long staffID);

        List<KeyValueDTO> GetStaffDetailsByStudentID(int studentID);

        List<KeyValueDTO> GetParentDetailsByStudentID(int studentID);

        decimal? GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID);

        string SendAttendanceNotificationsToParents(int classId, int sectionId);

        DashBoardChartDTO GetCountsForDashBoardMenuCards(int chartID);
        DashBoardChartDTO GetTwinViewData(int chartID,string referenceID);

        List<ClassClassTeacherMapDTO> FillEditDatasAndSubjects(int IID, int classID, int sectionID);

        List<StudentFeeConcessionDetailDTO> GetFeeDueForConcession(long studentID, int academicYearID);

        List<ClassCoordinatorsDTO> FillClassSectionWiseCoordinators(int classID, int sectionID);

        List<MailFeeDueStatementReportDTO> GetFeeDueDatasForReportMail(DateTime asOnDate, int classID, int? sectionID);

        DirectorsDashBoardDTO GetTeacherRelatedDataForDirectorsDashBoard();

        List<KeyValueDTO> GetVehiclesByRoute(int routeID);

        MailFeeDueStatementReportDTO SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData);

        EmployeeDTO UserProfileForDashBoard();

        Eduegate.Services.Contracts.Notifications.PushNotificationDTO GetNotificationsForDashBoard();

        TimeTableDTO GetWeeklyTimeTableForDashBoard(int weekDayID);

        List<FeeDueCancellationDetailDTO> GetFeeDueForDueCancellation(long studentID, int academicYearID);

        StudentFeeDueDTO GetGridLookUpsForSchoolCreditNote(long studentId);

        List<EmployeePromotionDTO> GetEmployeeDetailsByEmployeeID(long employeeID);

        string UpdateTCStatus(long? StudentTransferRequestIID, long TCContentID);

        string UpdateTCStatusToComplete(long? StudentTransferRequestIID);

        List<KeyValueDTO> Getstudentsubjectlist(long studentId);

        bool InsertProgressReportEntries(List<ProgressReportDTO> progressReportListDTOs, List<SettingDTO> settings);

        bool UpdateStudentProgressReportStatusID(MarkRegisterDTO dto);

        List<ProgressReportDTO> GetStudentProgressReports(ProgressReportDTO progressReport);

        List<ProgressReportDTO> GetStudentPublishedProgressReports(long studentID, long? examID);

        BankAccountDTO GetBankDetailsByBankID(long bankID);

        List<SchoolPayerBankDTO> FillPayerBanksBySchoolID(long schoolID);

        public StudentFeeDueDTO GetStudentFeeDueDetailsByID(long studentFeeDueID);

        public List<FeeCollectionFeeTypeDTO> GetFeeDuesForCampusTransfer(long studentId, int toSchoolID, int toAcademicYearID, int toClassID);

        StudentDTO GetStudentDetailsByStudentID(long studentID);

        List<KeyValueDTO> GetStudentsByParameters(int academicYearID, int? classID, int? sectionID);

        public AccountsGroupDTO GetAccountGroupDataByID(int groupID);

        List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID);

        string UpdateScheduleLogStatus(ScheduleLogDTO dto);

        FeePaymentDTO GetStudentFeeDetails(long studentID);

        StudentTransferRequestDTO FillStudentTransferData(long StudentID);
        List<FeePaymentHistoryDTO> GetFeeCollectionHistory(long studentID);
        List<KeyValueDTO> GetProgressReportIDsByStudPromStatus(int classID, int sectionID, long academicYearID, byte statusID, int examID, int examGroupID);

        List<KeyValueDTO> GetSchoolsByLoginIDActiveStuds(long? loginID);

        string GetPassageQuestionDetails(long passageQuestionID);

        public KeyValueDTO GetClassHeadTeacher(int classID, int sectionID, int academicYearID);

        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID);

        public List<KeyValueDTO> GetAssociateTeachers(int classID, int sectionID, int academicYearID);

        public List<KeyValueDTO> GetOtherTeachersByClass(int classID, int sectionID, int academicYearID);

        CandidateDTO GetCandidateDetailsByCandidateID(long candidateID);

        List<DaysDTO> GetWeekDays();

        List<TimeTableAllocInfoHeaderDTO> GetSmartTimeTableByDate(int tableMasterId, DateTime timeTableDate, int classID);

        List<TimeTableListDTO> GetTeacherSummary(int tableMasterID);

        List<TimeTableListDTO> GetClassSummary(int tableMasterID);

        List<TimeTableAllocationDTO> GetClassSectionTimeTableSummary(int tableMasterID);

        List<TimeTableListDTO> GetTeacherSummaryByTeacherID(long employeeID, int tableMasterID);

        List<TimeTableListDTO> GetClassSummaryDetails(long classID, long sectionID, int tableMasterID);

        string GenerateSmartTimeTable(int tableMasterId, string timeTableDate);

        OperationResultDTO ReAssignTimeTable(int tableMasterId, DateTime timeTableDate, int classID);

        List<SubjectDTO> GetSubjectDetailsByClassID(int classID);

        List<KeyValueDTO> GetAcademicYearBySchoolForApplications(int schoolID, bool? isActive);

        List<KeyValueDTO> GetWeekDayByDate(string date);

        List<KeyValueDTO> GetSetupScreens();

        KeyValueDTO GetCurrentAcademicYearBySchoolID(long schoolID);

        List<KeyValueDTO> GetSubjectBySubjectID(byte subjectTypeID);

        List<KeyValueDTO> GetDashboardCounts(long candidateID);

        List<CandidateOnlineExamMapDTO> GetExamDetailsByCandidateID(long candidateID);

        List<LessonPlanDTO> ExtractUploadedFiles();

        List<KeyValueDTO> GetStudentsByParent(long parentID);

        List<KeyValueDTO> GetActiveStudentsByParent(long parentID);

        public List<StudentDTO> GetStudentsDetailsByParent(long parentID);

        public List<StudentDTO> GetActiveStudentsDetailsByParent(long parentID);

        public List<StudentAttendenceDTO> GetClassWiseAttendanceDatas(int classId, int sectionId);

        public string SendNotificationsToParentsByAttendance(List<StudentAttendenceDTO> studAttendance);

        List<KeyValueDTO> GetSubjectBySubjectTypeID(byte subjectTypeID);

        List<KeyValueDTO> GetTeacherByClassAndSubject(string classIDs, string subjectIDs);

        public string SaveAllStudentAttendances(int classID, int sectionID, string attendanceDateString, byte? attendanceStatus);

        List<AgendaDTO> GetStudentSubjectWiseAgendas(long loginID, long subjectID, string date);
    }
}