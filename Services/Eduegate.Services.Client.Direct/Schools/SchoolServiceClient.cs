using Eduegate.Domain.Entity.School.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts;
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
using Eduegate.Services.Contracts.School.CounselorHub;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Fines;
using Eduegate.Services.Contracts.School.Galleries;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.Schools;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Services.Schools;
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.School.Payment;
namespace Eduegate.Services.Client.Direct.Schools
{
    public class SchoolServiceClient : BaseClient, ISchoolService
    {
        SchoolService service = new SchoolService();

        public SchoolServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public OperationResult DeleteApplication(long applicationId)
        {
            return service.DeleteApplication(applicationId);
        }

        public OperationResult DeleteSiblingApplication(long applicationId)
        {
            return service.DeleteSiblingApplication(applicationId);
        }

        public StudentApplicationDTO GetApplication(long applicationId)
        {
            return service.GetApplication(applicationId);
        }

        public List<StudentApplicationDTO> GetStudentApplication(long loginID)
        {
            return service.GetStudentApplication(loginID);
        }

        public StaffAttendenceDTO GetStaffAttendence(long staffID, DateTime date)
        {
            return service.GetStaffAttendence(staffID, date);
        }

        public StudentAttendenceDTO GetStudentAttendence(long studentID, DateTime date)
        {
            return service.GetStudentAttendence(studentID, date);
        }

        public string SaveStudentAttendence(StudentAttendenceDTO attendence)
        {
            return service.SaveStudentAttendence(attendence);
        }

        public string SaveAcademicCalendar(AcadamicCalendarDTO acadamic)
        {
            return service.SaveAcademicCalendar(acadamic);
        }

        public string SaveStaffAttendance(StaffAttendenceDTO attendence)
        {
            return service.SaveStaffAttendance(attendence);
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId)
        {
            return service.GetStudentAttendenceByYearMonth(month, year, classId, sectionId);
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            return service.GetStudentAttendenceByYearMonthStudentId(month, year, studentId);
        }

        public List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year)
        {
            return service.GetStaffAttendanceByMonthYear(month, year);
        }

        public List<PresentStatusDTO> GetPresentStatuses()
        {
            return service.GetPresentStatuses();
        }

        //public List<ClassSubjectMapDTO> GetSubjectByClassID(int classID)
        //{
        //    return service.GetSubjectByClassID(classID);
        //}

        public string SaveTimeTable(TimeTableAllocationDTO timeTableAlloc)
        {
            return service.SaveTimeTable(timeTableAlloc);
        }
        public string SaveTimeTableLog(TimeTableLogDTO timeTableLog)
        {
            return service.SaveTimeTableLog(timeTableLog);
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByClassID(int classID, int tableMasterId)
        {
            return service.GetTimeTableByClassID(classID, tableMasterId);
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate)
        {
            return service.GetTimeTableByDate(classID, tableMasterId, timeTableDate);
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID)
        {
            return service.GetAcademicCalenderByAcademicYear(academicYearID, year, academicCalendarStatusID, academicCalendarID);
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year)
        {
            return service.GetAcademicCalenderByMonthYear(month, year);
        }
        public void DeleteTimeTableEntry(long timeTableAllocationID)
        {
            service.DeleteTimeTableEntry(timeTableAllocationID);
        }

        public void DeleteDailyTimeTableEntry(long timeTableLogID)
        {
            service.DeleteDailyTimeTableEntry(timeTableLogID);
        }
        public List<SubjectTeacherMapDTO> GetTeacherBySubject(int classId, int SectionId, int subjectId)
        {
            return service.GetTeacherBySubject(classId, SectionId, subjectId);
        }

        public string SaveStudentFeeDue(StudentFeeDueDTO feeDueInfo)
        {
            return service.SaveStudentFeeDue(feeDueInfo);
        }

        public string SaveStudentApplication(StudentApplicationDTO studentApplication)
        {
            return service.SaveStudentApplication(studentApplication);
        }

        public string SendTodayAttendancePushNotification(int classId, int sectionId)
        {
            return service.SendTodayAttendancePushNotification(classId, sectionId);
        }

        public string ResentFromLeadLoginCredentials(StudentApplicationDTO studentApplication)
        {
            return service.ResentFromLeadLoginCredentials(studentApplication);
        }

        public OperationResultDTO GenerateTimeTable(TimeTableAllocationDTO timeTableLog)
        {
            return service.GenerateTimeTable(timeTableLog);
        }

        public List<StudentFeeDueDTO> FillFeeDue(int classId, long studentId)
        {
            return service.FillFeeDue(classId, studentId);
        }

        public List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId)
        {
            return service.FillFeePaymentDetails(loginId);
        }

        public List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId)
        {
            return service.FillPendingFees(classId, studentId);
        }
        public List<KeyValueDTO> GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID)
        {
            return service.GetInvoiceForCreditNote(classId, studentId, feeMasterID, feePeriodID);
        }
        public List<FeeDueMonthlySplitDTO> GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID)
        {
            return service.GetFeeDueMonthlyDetails(studentFeeDueID, feeMasterID, feePeriodID);
        }

        public List<StudentFeeDueDTO> GetFeesByInvoiceNo(long studentFeeDueID)
        {
            return service.GetFeesByInvoiceNo(studentFeeDueID);
        }

        public List<StudentDTO> GetStudentsSiblings(long parentId)
        {
            return service.GetStudentsSiblings(parentId);
        }

        public List<AgeCriteriaMapDTO> GetAgeCriteriaByClassID(int classId, int academicYearID)
        {
            return service.GetAgeCriteriaByClassID(classId, academicYearID);
        }

        public LibraryBookDTO GetBooKCategoryName(long bookCategoryCodeId)
        {
            return service.GetBooKCategoryName(bookCategoryCodeId);
        }

        public LibraryTransactionDTO GetBookQuantityDetails(string CallAccNo, int? bookMapID)
        {
            return service.GetBookQuantityDetails(CallAccNo, bookMapID);
        }

        public LibraryTransactionDTO GetIssuedBookDetails(string CallAccNo, int? bookMapID)
        {
            return service.GetIssuedBookDetails(CallAccNo, bookMapID);
        }

        public List<KeyValueDTO> GetBookDetailsByCallNo(string CallAccNo)
        {
            return service.GetBookDetailsByCallNo(CallAccNo);
        }

        public List<CircularListDTO> GetCircularList(long parentId)
        {
            return service.GetCircularList(parentId);
        }

        public List<CounselorHubListDTO> GetCounselorList(long parentId)
        {
            return service.GetCounselorList(parentId);
        }
        public List<CircularListDTO> GetCircularListByStudentID(long studentID)
        {
            return service.GetCircularListByStudentID(studentID);
        }

     
        public List<GalleryDTO> GetGalleryView(long academicYearID)
        {
            return service.GetGalleryView(academicYearID);
        }

        public List<AgendaDTO> GetAgendaList(long loginID)
        {
            return service.GetAgendaList(loginID);
        }

        public List<LessonPlanDTO> GetLessonPlanList(long StudentID)
        {
            return service.GetLessonPlanList(StudentID);
        }

        public List<FeeCollectionDTO> GetStudentFeeCollection(long studentId)
        {
            return service.GetStudentFeeCollection(studentId);
        }
        public StudentRouteStopMapDTO GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return service.GetPickUpBusSeatAvailabilty(RouteStopMapId, academicYearID);
        }
        public StudentRouteStopMapDTO GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return service.GetDropBusSeatAvailabilty(RouteStopMapId, academicYearID);
        }

        public StaffRouteStopMapDTO GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return service.GetPickUpBusSeatStaffAvailabilty(RouteStopMapId, academicYearID);
        }
        public StaffRouteStopMapDTO GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return service.GetDropBusSeatStaffAvailabilty(RouteStopMapId, academicYearID);
        }

        public List<AssignmentDTO> GetAssignmentStudentwise(long studentId, int? SubjectID)
        {
            return service.GetAssignmentStudentwise(studentId, SubjectID);
        }

        public List<MarkListViewDTO> GetMarkListStudentwise(long studentId)
        {
            return service.GetMarkListStudentwise(studentId);
        }
        public ExamDTO GetClassByExam(int examId)
        {
            return service.GetClassByExam(examId);
        }
        public List<FeePeriodsDTO> GetFeePeriodMonthlySplit(List<int> feePeriodIds)
        {
            return service.GetFeePeriodMonthlySplit(feePeriodIds);
        }
        public List<KeyValueDTO> GetClassStudents(int classId, int sectionId)
        {
            return service.GetClassStudents(classId, sectionId);
        }

        public List<KeyValueDTO> GetClassStudentsAll(int academicYearID,List<int> classList, List<int> sectionList)
        {
            return service.GetClassStudentsAll(academicYearID, classList, sectionList);
        }

        public List<AcademicYearDTO> GetAcademicYearDataByCalendarID(long calendarID)
        {
            return service.GetAcademicYearDataByCalendarID(calendarID);
        }
        public List<AcademicYearDTO> GetAcademicYear(int academicYearID)
        {
            return service.GetAcademicYear(academicYearID);
        }
        public List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId)
        {
            return service.GetClasswiseStudentData(classId, sectionId);
        }

        public List<RouteShiftingStudentMapDTO> GetStudentDatasFromRouteID(int routeId)
        {
            return service.GetStudentDatasFromRouteID(routeId);
        }

        public List<ClassSectionSubjectListMapDTO> FillClassandSectionWiseSubjects(int classID, int sectionID, int IID)
        {
            return service.FillClassandSectionWiseSubjects(classID, sectionID, IID);
        }

        public List<RouteShiftingStaffMapDTO> GetStaffDatasFromRouteID(int routeId)
        {
            return service.GetStaffDatasFromRouteID(routeId);
        }

        public List<RemarksEntryStudentsDTO> GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID)
        {
            return service.GetClasswiseRemarksEntryStudentData(classId, sectionId, examGroupID);
        }

        public List<HealthEntryStudentMapDTO> GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID)
        {
            return service.GetHealthEntryStudentData(classId, sectionId, academicYearID, examGroupID);
        }

        public List<EventTransportAllocationMapDTO> GetStudentandStaffsByRouteIDforEvent(EventTransportAllocationDTO eventDto)
        {
            return service.GetStudentandStaffsByRouteIDforEvent(eventDto);
        }

        public EventTransportAllocationMapDTO GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType)
        {
            return service.GetStudentTransportDetailsByStudentID(studentID, IsRouteType);
        }
      
        public EventTransportAllocationMapDTO GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType)
        {
            return service.GetStaffTransportDetailsByStaffID(staffID, IsRouteType);
        }

        public List<CampusTransferMapDTO> GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId)
        {
            return service.GetClasswiseStudentDataForCampusTransfer(classId, sectionId);
        }

        public List<KeyValueDTO> GetAdvancedAcademicYearBySchool(int schoolID)
        {
            return service.GetAdvancedAcademicYearBySchool(schoolID);
        }

        public List<MarkRegisterDetailsSplitDTO> GetExamSubjectsMarks(long studentId, long examId, int ClassId)
        {
            return service.GetExamSubjectsMarks(studentId, examId, ClassId);
        }

        public List<MarkGradeMapDTO> GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId)
        {
            return service.GetGradeByExamSubjects(examId, classId, subjectID, typeId);
        }

        public List<CollectFeeAccountDetailDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID)
        {
            return service.GetCollectFeeAccountData(fromDate, toDate, CashierID);
        }

        public List<StudentDTO> GetStudentDetails(long studentId)
        {
            return service.GetStudentDetails(studentId);
        }

        public List<StudentTransportDetailDTO>  GetStudentTransportDetails(long studentID)
        {
            return service.GetStudentTransportDetails(studentID);
        } 
        
        public TransportCancellationDTO GetStudentTransportCancellationDetails(long studentID)
        {
            return service.GetStudentTransportCancellationDetails(studentID);
        }
        
        public OperationResultDTO RevertTransportCancellation(long RequestIID)
        {
            return service.RevertTransportCancellation(RequestIID);
        }


        public List<StudentBehavioralRemarksDTO> GetStudentBehavioralRemarks(StudentBehavioralRemarksDTO studentBehavioralRemarksDTO)
        {
            return service.GetStudentBehavioralRemarks(studentBehavioralRemarksDTO);
        }
        public TransportApplicationDTO GetTransportStudentDetailsByParentLoginID(long id)
        {
            return service.GetTransportStudentDetailsByParentLoginID(id);
        }

        public GuardianDTO GetGuardianDetails(long studentId)
        {
            return service.GetGuardianDetails(studentId);
        }
        public string FeeAccountPosting(DateTime fromDate, DateTime toDate, long cashierID)
        {
            return service.FeeAccountPosting(fromDate, toDate, cashierID);
        }

        public List<StudentLeaveApplicationDTO> GetStudentLeaveApplication(long leaveapplicationId)
        {
            return service.GetStudentLeaveApplication(leaveapplicationId);
        }

        public StudentLeaveApplicationDTO GetLeaveApplication(long leaveapplicationId)
        {
            return service.GetLeaveApplication(leaveapplicationId);
        }

        public OperationResult DeleteLeaveApplication(long leaveapplicationId)
        {
            return service.DeleteLeaveApplication(leaveapplicationId);
        }

        public List<TimeTableAllocationDTO> GetClassTimeTable(long studentId)
        {
            return service.GetClassTimeTable(studentId);
        }

        public List<ExamDTO> GetExamLists(long studentId)
        {
            return service.GetExamLists(studentId);
        }

        public List<StudentTransferRequestDTO> GetStudentTransferApplication(long studentId)
        {
            return service.GetStudentTransferApplication(studentId);
        }

        public StudentTransferRequestDTO GetTransferApplication(long studentId)
        {
            return service.GetTransferApplication(studentId);
        }

        public OperationResult DeleteTransferApplication(long studentId)
        {
            return service.DeleteTransferApplication(studentId);
        }

        public StudentApplicationDTO GetApplicationByLoginID(long loginID)
        {
            return service.GetApplicationByLoginID(loginID);
        }

        public List<NotificationAlertsDTO> GetNotificationAlerts(long loginID, int page, int pageSize)
        {
            return ((ISchoolService)service).GetNotificationAlerts(loginID ,  page,  pageSize);
        }

        public List<NotificationAlertsDTO> GetAllNotificationAlerts(long loginID)
        {
            return ((ISchoolService)service).GetAllNotificationAlerts(loginID);
        }

        public List<MailBoxDTO> GetSendMailFromParent(long loginID)
        {
            return ((ISchoolService)service).GetSendMailFromParent(loginID);
        }

        public int GetNotificationAlertsCount(long loginID)
        {
            return ((ISchoolService)service).GetNotificationAlertsCount(loginID);
        }

        public string MarkNotificationAsRead(long loginID, long notificationAlertIID)
        {
            return ((ISchoolService)service).MarkNotificationAsRead(loginID, notificationAlertIID);
        }

        public List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentId)
        {
            return service.GetTeacherDetails(studentId);
        }


        public List<FeeCollectionDTO> GetSiblingDueDetailsFromStudentID(long StudentID)
        {
            return service.GetSiblingDueDetailsFromStudentID(StudentID);
        }

        public List<StudentFeeDueDTO> GetFineCollected(long studentId)
        {
            return service.GetFineCollected(studentId);
        }

        public List<StudentFeeDueDTO> FillFineDue(int classId, long studentId)
        {
            return service.FillFineDue(classId, studentId);
        }


        //public List<StudentFeeDueDTO> FillFineDue(int classId, long studentId)
        //{
        //    return service.FillFineDue(classId, studentId);
        //}

        //public List<StudentFeeDueDTO> GetFineCollected(long studentId)
        //{
        //    return service.GetFineCollected(studentId);
        //}
        public List<StudentSkillRegisterSplitDTO> GetStudentSkills(long skillId)
        {
            return service.GetStudentSkills(skillId);
        }

        public List<MarkGradeMapDTO> GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID)
        {
            return service.GetGradeByExamSkill(examId, skillId, subjectId, classId, markGradeID);
        }
        public ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID)
        {
            return ((ISchoolService)service).GetStudentSkillRegister(studentId, ClassID);
        }

        public ProductBundleDTO GetProductDetails(long productId)
        {
            return service.GetProductDetails(productId);
        }
        public ProductBundleDTO GetProductBundleData(long productskuId)
        {
            return service.GetProductBundleData(productskuId);
        }

        public EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate)
        {
            return service.GetCollectTimeSheetsData(employeeID, fromDate, toDate);
        }

        public FineMasterStudentMapDTO GetFineAmount(int fineMasterID)
        {
            return service.GetFineAmount(fineMasterID);
        }

        public List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId, int academicId)
        {
            return service.FillFeeDueForSettlement(studentId, academicId);
        }

        public List<FeeCollectionDTO> FillFeeDueDataForSettlement(long studentId, int academicId)
        {
            return service.FillFeeDueDataForSettlement(studentId, academicId);
        }
        public List<FeeCollectionDTO> FillCollectedFeesDetails(long studentId, int academicId)
        {
            return service.FillCollectedFeesDetails(studentId, academicId);
        }

        public List<FeeCollectionPreviousFeesDTO> GetIssuedCreditNotesForCollectedFee(long studentId)
        {
            throw new NotImplementedException();
        }

        public EmployeesDTO GetEmployeeFromEmployeeID(long employeeID)
        {
            return service.GetEmployeeFromEmployeeID(employeeID);
        }

        public AccountsGroupDTO GetGroupCodeByParentGroup(long parentGroupID)
        {
            return service.GetGroupCodeByParentGroup(parentGroupID);
        }

        public AccountsDTO GetAccountCodeByGroup(long groupID)
        {
            return service.GetAccountCodeByGroup(groupID);
        }

        public StudentDTO GetStudentDetailFromStudentID(long StudentID)
        {
            return service.GetStudentDetailFromStudentID(StudentID);
        }
        public List<CurrencyDTO> GetCurrencyDetails()
        {
            return service.GetCurrencyDetails();
        }
        public List<FeeCollectionDTO> AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative)
        {
            return service.AutoReceiptAccountTransactionSync(accountTransactionHeadIID, referenceID, loginId, type, isDueAsNegative);
        }
        public SchoolCreditNoteDTO AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type)
        {
            return service.AutoCreditNoteAccountTransactionSync(accountTransactionHeadIID, studentID, loginId, type);
        }
        public GuardianDTO GetParentDetailFromParentID(long ParentID)
        {
            return service.GetParentDetailFromParentID(ParentID);
        }

        public List<KeyValueDTO> GetFeeStructure(int academicYearID)
        {
            return service.GetFeeStructure(academicYearID);
        }

        public List<KeyValueDTO> GetSubGroup(int mainGroupID)
        {
            return service.GetSubGroup(mainGroupID);
        }

        public List<KeyValueDTO> GetAccountCodeByLedger(int ledgerGroupID)
        {
            return service.GetAccountCodeByLedger(ledgerGroupID);
        }

        public List<KeyValueDTO> GetAccountGroup(int subGroupID)
        {
            return service.GetAccountGroup(subGroupID);
        }

        public List<KeyValueDTO> GetAccountByGroupID(int groupID)
        {
            return service.GetAccountByGroupID(groupID);
        }

        public List<KeyValueDTO> GetCostCenterByAccount(long accountID)
        {
            return service.GetCostCenterByAccount(accountID);
        }

        public List<KeyValueDTO> GetAccountByPayementModeID(long paymentModeID)
        {
            return service.GetAccountByPayementModeID(paymentModeID);
        }

        public List<KeyValueDTO> GetAccountGroupByAccountID(long accountID)
        {
            return service.GetAccountGroupByAccountID(accountID);
        }

        public List<KeyValueDTO> GetFeePeriod(int academicYearID, long studentID, int? feeMasterID)
        {
            return service.GetFeePeriod(academicYearID, studentID, feeMasterID);
        }
        public List<KeyValueDTO> GetTransportFeePeriod(int academicYearID)
        {
            return service.GetTransportFeePeriod(academicYearID);
        }

        public void DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID)
        {
            service.DeleteAcademicCalendarEvent(academicYearCalendarEventIID, academicYearCalendarID);
        }
        public int GetStudentsCount()
        {
            return service.GetStudentsCount();
        }

        public List<KeyValueDTO> GetCastByRelegion(int relegionID)
        {
            return service.GetCastByRelegion(relegionID);
        }

        public List<KeyValueDTO> GetToNotificationUsersByUser(int userID, int branchID, string user)
        {
            return service.GetToNotificationUsersByUser(userID, branchID, user);
        }

        public List<KeyValueDTO> GetStreamByStreamGroup(byte? streamGroupID)
        {
            return service.GetStreamByStreamGroup(streamGroupID);
        }

        public List<KeyValueDTO> GetStreamCompulsorySubjects(byte? streamID)
        {
            return service.GetStreamCompulsorySubjects(streamID);
        }

        public List<KeyValueDTO> GetStreamOptionalSubjects(byte? streamID)
        {
            return service.GetStreamOptionalSubjects(streamID);
        }

        public List<StreamDTO> GetFullStreamListDatas()
        {
            return service.GetFullStreamListDatas();
        }

        public List<KeyValueDTO> GetSubSkillByGroup(int skillGroupID)
        {
            return service.GetSubSkillByGroup(skillGroupID);
        }

        public List<KeyValueDTO> GetAcademicYearBySchool(int schoolID, bool? isActive)
        {
            return service.GetAcademicYearBySchool(schoolID, isActive);
        }

        public List<KeyValueDTO> GetClassesByAcademicYearID(int academicyearID)
        {
            return service.GetClassesByAcademicYearID(academicyearID);
        }
        public List<KeyValueDTO> GetClassesBySchool(byte schoolID)
        {
            return service.GetClassesBySchool(schoolID);
        }
        public List<KeyValueDTO> GetSectionsBySchool(byte schoolID)
        {
            return service.GetSectionsBySchool(schoolID);
        } 
        
        public List<ClassDTO> GetClassListBySchool(byte schoolID)
        {
            return service.GetClassListBySchool(schoolID);
        }

        public string GetProgressReportName(long schoolID, int? classID)
        {
            return service.GetProgressReportName(schoolID, classID);
        }

        public List<KeyValueDTO> GetRouteStopsByRoute(int routeID)
        {
            return service.GetRouteStopsByRoute(routeID);
        }

        public List<KeyValueDTO> GetSubjectByClass(int classID)
        {
            return service.GetSubjectByClass(classID);
        }

        public List<KeyValueDTO> GetSubjectbyQuestionGroup(long questionGroupID)
        {
            return service.GetSubjectbyQuestionGroup(questionGroupID);
        }

        public OnlineExamQuestionDTO GetQuestionDetailsByQuestionID(long questionID)
        {
            return service.GetQuestionDetailsByQuestionID(questionID);
        }

        public StaticContentDataDTO GetAboutandContactDetails(long contentID)
        {
            return service.GetAboutandContactDetails(contentID);
        }

        public LibraryTransactionDTO GetLibraryStudentFromStudentID(long studentID)
        {
            return service.GetLibraryStudentFromStudentID(studentID);
        }

        public LibraryTransactionDTO GetLibraryStaffFromEmployeeID(long employeeID)
        {
            return service.GetLibraryStaffFromEmployeeID(employeeID);
        }

        public TransportApplicationDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            return service.GetStudentTransportApplication(TransportApplctnStudentMapIID);
        }

        public List<SubjectMarkEntryDetailDTO> FillClassStudents(long classID, long sectionID)
        {
            return service.FillClassStudents(classID, sectionID);
        }

        public List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examId, int classId, int sectionID, int subjectID)
        {
            return service.GetSubjectsMarkData(examId, classId, sectionID, subjectID);
        }

        public ClassSubjectSkillGroupMapDTO GetExamMarkDetails(long subjectID, long examID)
        {
            return service.GetExamMarkDetails(subjectID, examID);
        }

        public List<KeyValueDTO> GetTeacherEmailByParentLoginID(long loginID)
        {
            return service.GetTeacherEmailByParentLoginID(loginID);
        }

        public CandidateDTO GetCandidateDetails(string username, string password)
        {
            return service.GetCandidateDetails(username, password);
        }

        public string ShiftStudentSection(ClassSectionShiftingDTO toDto)
        {
            return service.ShiftStudentSection(toDto);
        }

        public ClassSectionShiftingDTO GetStudentsForShifting(int classID, int sectionID)
        {
            return service.GetStudentsForShifting(classID, sectionID);
        }

        List<KeyValueDTO> GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            return service.GetClassWiseExamGroup(classID, sectionID, academicYearID);
        }
        List<KeyValueDTO> GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            return service.GetSkillSetByClassExam(classID, sectionID, examID, academicYearID, languageTypeID);
        }
        public List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return service.GetSubjectsBySkillset(markEntrySearchArgsDTO);
        }
        List<KeyValueDTO> GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            return service.GetSkillGroupByClassExam(classID, examGroupID, skillSetID, academicYearID);
        }
        List<KeyValueDTO> GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            return service.GetSkillsByClassExam(classID, skillGroupID, skillSetID, academicYearID);
        }

        List<KeyValueDTO> ISchoolService.GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            return ((ISchoolService)service).GetSkillsByClassExam(classID, skillGroupID, skillSetID, academicYearID);
        }

        List<KeyValueDTO> ISchoolService.GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            return ((ISchoolService)service).GetSkillGroupByClassExam(classID, examGroupID, skillSetID, academicYearID);
        }

        List<KeyValueDTO> ISchoolService.GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            return ((ISchoolService)service).GetSkillSetByClassExam(classID, sectionID, examID, academicYearID, languageTypeID);
        }

        List<KeyValueDTO> ISchoolService.GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            return ((ISchoolService)service).GetClassWiseExamGroup(classID, sectionID, academicYearID);
        }


        List<MarkRegisterDetailsDTO> ISchoolService.GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetCoScholasticEntry(markEntrySearchArgsDTO);
        }

        string SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO)
        {
            return ((ISchoolService)service).SaveCoScholasticEntry(markRegisterDTO);
        }

        string ISchoolService.SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO)
        {
            return ((ISchoolService)service).SaveCoScholasticEntry(markRegisterDTO);
        }
        List<MarkRegisterDetailsDTO> ISchoolService.GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetScholasticInternalEntry(markEntrySearchArgsDTO);
        }

        string SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO)
        {
            return ((ISchoolService)service).SaveScholasticInternalEntry(markRegisterDTO);
        }

        string ISchoolService.SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO)
        {
            return ((ISchoolService)service).SaveScholasticInternalEntry(markRegisterDTO);
        }
        string SaveMarkEntry(MarkRegisterDTO markRegisterDTO)
        {
            return ((ISchoolService)service).SaveMarkEntry(markRegisterDTO);
        }

        string ISchoolService.SaveMarkEntry(MarkRegisterDTO markRegisterDTO)
        {
            return ((ISchoolService)service).SaveMarkEntry(markRegisterDTO);
        }

        public List<StudentMarkEntryDTO> GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetMarkEntry(markEntrySearchArgsDTO);
        }


        public List<MarkRegisterDetailsDTO> GetSubjectsAndMarksToPublish(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetSubjectsAndMarksToPublish(markEntrySearchArgsDTO);
        }

        List<KeyValueDTO> ISchoolService.GetSubjectsByClassID(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetSubjectsByClassID(markEntrySearchArgsDTO);
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsBySubjectType(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetSubjectsBySubjectType(markEntrySearchArgsDTO);
        }

        List<KeyValueDTO> ISchoolService.GetExamsByTermID(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return ((ISchoolService)service).GetExamsByTermID(markEntrySearchArgsDTO);
        }

        List<KeyValueDTO> ISchoolService.GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID)
        {
            return service.GetExamsByClassAndGroup(classID, sectionID, examGroupID, academicYearID);
        }   
        
        List<KeyValueDTO> ISchoolService.GetExamGroupsByAcademicYearID(int? academicYearID)
        {
            return service.GetExamGroupsByAcademicYearID(academicYearID);
        }

        List<KeyValueDTO> ISchoolService.GetSubjectByType(byte subjectTypeID)
        {
            return service.GetSubjectByType(subjectTypeID);
        }

        List<KeyValueDTO> ISchoolService.GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID)
        {
            return service.GetSkillByExamAndClass(classID, sectionID, examID, academicYearID, termID);
        }

        string ISchoolService.UpdateMarkEntryStatus(MarkRegisterDTO dto)
        {
            return service.UpdateMarkEntryStatus(dto);
        }

        public string GetAcademicYearNameByID(int academicYearID)
        {
            return service.GetAcademicYearNameByID(academicYearID);
        }

        public string GetSchoolNameByID(int schoolID)
        {
            return service.GetSchoolNameByID(schoolID);
        }

        public List<TransportApplicationStudentMapDTO> GetTransportApplication(long loginID)
        {
            return service.GetTransportApplication(loginID);
        }

        public List<SchoolGeoLocationDTO> GetGeoSchoolSetting(long schoolID)
        {
            return service.GetGeoSchoolSetting(schoolID);
        }

        public void ClearGeoSchoolSetting(int schoolID)
        {
            ((ISchoolService)service).ClearGeoSchoolSetting(schoolID);
        }

        public void SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dto)
        {
            ((ISchoolService)service).SaveGeoSchoolSetting(dto);
        }

        public List<AcademicClassMapWorkingDayDTO> GetMonthAndYearByAcademicYearID(int? academicYearID)
        {
            return service.GetMonthAndYearByAcademicYearID(academicYearID);
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicMonthAndYearByCalendarID(long? calendarID)
        {
            return service.GetAcademicMonthAndYearByCalendarID(calendarID);
        }

        public string CheckAndInsertCalendarEntries(long calendarID)
        {
            return service.CheckAndInsertCalendarEntries(calendarID);
        }

        public List<KeyValueDTO> GetCalendarByTypeID(byte calendarTypeID)
        {
            return service.GetCalendarByTypeID(calendarTypeID);
        }

        public List<AcademicCalenderEventDateDTO> GetCalendarEventsByCalendarID(long calendarID)
        {
            return service.GetCalendarEventsByCalendarID(calendarID);
        }

        public List<EmployeesDTO> GetEmployeesByCalendarID(long calendarID)
        {
            return service.GetEmployeesByCalendarID(calendarID);
        }

        public List<PresentStatusDTO> GetStaffPresentStatuses()
        {
            return service.GetStaffPresentStatuses();
        }

        //public LibraryBookDTO GetBookDetailsChange(long BookID)
        //{
        //    return service.GetBookDetailsChange(BookID);
        //}

        public List<SchoolCreditNoteDTO> GetCreditNoteNumber(long? headID, long studentID)
        {
            return service.GetCreditNoteNumber(headID, studentID);
        }

        public string DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID)
        {
            return ((ISchoolService)service).DueFees(feemasterID, invoiceDate, amount, studentID, creditNoteID);
        }

        public List<KeyValueDTO> GetSchoolsByParentLoginID(long? loginID)
        {
            return service.GetSchoolsByParentLoginID(loginID);
        }

        public AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID)
        {
            return service.GetAcademicYearDataByAcademicYearID(academicYearID);
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByCandidateID(long candidateID)
        {
            return service.GetQuestionsByCandidateID(candidateID);
        }

        public StudentApplicationDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob)
        {
            return service.GetAgeCriteriaDetails(classID, academicID, schoolID, dob);
        }

        public List<KeyValueDTO> GetUnitByUnitGroup(int groupID)
        {
            return service.GetUnitByUnitGroup(groupID);
        }

        public List<UnitDTO> GetUnitDataByUnitGroup(int groupID)
        {
            return service.GetUnitDataByUnitGroup(groupID);
        }

        public List<OnlineExamResultDTO> GetOnlineExamsResultByCandidateID(long candidateID)
        {
            return service.GetOnlineExamsResultByCandidateID(candidateID);
        }

        public string SubmitAmountAsLog(decimal? totalAmount)
        {
            return service.SubmitAmountAsLog(totalAmount);
        }

        public PaymentLogDTO GetLastLogData()
        {
            return service.GetLastLogData();
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaData()
        {
            return service.GetPaymentMasterVisaData();
        }

        public PaymentMasterVisaDTO UpdatePaymentMasterVisa(PaymentMasterVisaDTO paymentMasterVisaDTO)
        {
            return service.UpdatePaymentMasterVisa(paymentMasterVisaDTO);

        }
        public OperationResultDTO FeeCollectionEntry(List<FeeCollectionDTO> feeCollectionList)
        {
            return service.FeeCollectionEntry(feeCollectionList);
        }

        public List<FeeCollectionDTO> UpdateStudentsFeePaymentStatus(string transactionNo, long? parentLoginID)
        {
            return service.UpdateStudentsFeePaymentStatus(transactionNo, parentLoginID);
        }

        public List<FeeCollectionDTO> GetStudentFeeCollectionsHistory(StudentDTO studentData, byte? schoolID, int? academicYearID)
        {
            return service.GetStudentFeeCollectionsHistory(studentData, schoolID, academicYearID);
        }

        public List<AcademicYearDTO> GetCurrentAcademicYearsData()
        {
            return service.GetCurrentAcademicYearsData();
        }

        public List<KeyValueDTO> GetAllAcademicYearBySchoolID(int schoolID)
        {
            return service.GetAllAcademicYearBySchoolID(schoolID);
        }

        public List<FeeCollectionDTO> GetFeeCollectionHistories(List<StudentDTO> studentDatas, byte? schoolID, int? academicYearID)
        {
            return service.GetFeeCollectionHistories(studentDatas, schoolID, academicYearID);
        }

        public PaymentLogDTO GetAndInsertLogDataByTransactionID(string transID)
        {
            return service.GetAndInsertLogDataByTransactionID(transID);
        }

        public string CheckFeeCollectionStatusByTransactionNumber(string transactionNumber)
        {
            return service.CheckFeeCollectionStatusByTransactionNumber(transactionNumber);
        }

        public List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber, string mailID, string feeReceiptNo)
        {
            return service.GetFeeCollectionDetailsByTransactionNumber(transactionNumber, mailID, feeReceiptNo);
        }

        public OperationResult DeleteTransportApplication(long transportapplicationId)
        {
            return service.DeleteTransportApplication(transportapplicationId);
        }

        public List<KeyValueDTO> GetRoutesByRouteGroupID(int? routeGroupID)
        {
            return service.GetRoutesByRouteGroupID(routeGroupID);
        }

        public AcademicYearDTO GetAcademicYearDataByGroupID(int? routeGroupID)
        {
            return service.GetAcademicYearDataByGroupID(routeGroupID);
        }

        public List<KeyValueDTO> GetPickupStopMapsByRouteGroupID(int? routeGroupID)
        {
            return service.GetPickupStopMapsByRouteGroupID(routeGroupID);
        }

        public List<KeyValueDTO> GetDropStopMapsByRouteGroupID(int? routeGroupID)
        {
            return service.GetDropStopMapsByRouteGroupID(routeGroupID);
        }

        public List<StudentPickupRequestDTO> GetStudentPickupRequestsByLoginID(long loginID)
        {
            return service.GetStudentPickupRequestsByLoginID(loginID);
        }

        public List<KeyValueDTO> GetSubLedgerByAccount(long accountID)
        {
            return service.GetSubLedgerByAccount(accountID);
        }

        public List<ProgressReportNewDTO> SaveProgressReportData(List<ProgressReportNewDTO> toDtoList)
        {
            throw new NotImplementedException();
        }

        public string UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList)
        {
            throw new NotImplementedException();
        }

        public List<ProgressReportNewDTO> GetProgressReportData(int classID, int? sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<ProgressReportNewDTO> GetProgressReportList(long studentID, int classID, int sectionID, int academicYearID)
        {
            return service.GetProgressReportList(studentID, classID, sectionID, academicYearID);
        }

        public string CancelStudentPickupRequestByID(long pickupRequestID)
        {
            return service.CancelStudentPickupRequestByID(pickupRequestID);
        }

        public string CancelTransportApplication(long mapIID)
        {
            return service.CancelTransportApplication(mapIID);
        }

        public TransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID)
        {
            return service.GetCountByDocumentTypeID(docTypeID);
        }

        public List<KeyValueDTO> GetStaffDetailsByStudentID(int studentID)
        {
            return service.GetStaffDetailsByStudentID(studentID);
        }

        public List<KeyValueDTO> GetParentDetailsByStudentID(int studentID)
        {
            return service.GetParentDetailsByStudentID(studentID);
        }

        public List<KeyValueDTO> GetStudentDetailsByStaff(long staffID)
        {
            return service.GetStudentDetailsByStaff(staffID);
        }

        public decimal? GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID)
        {
            return service.GetFeeAmount(studentID, academicYearID, feeMasterID, feePeriodID);
        }
        public List<StudentFeeConcessionDetailDTO> GetFeeDueForConcession(long studentID, int academicYearID)
        {
            return service.GetFeeDueForConcession(studentID, academicYearID);
        }
        public string SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            return service.SendAttendanceNotificationsToParents(classId, sectionId);
        }

        public DashBoardChartDTO GetCountsForDashBoardMenuCards(int chartID)
        {
            return service.GetCountsForDashBoardMenuCards(chartID);
        }
        public DashBoardChartDTO GetTwinViewData(int chartID, string referenceID)
        {
            return service.GetTwinViewData(chartID, referenceID);
        }        
        public List<ClassClassTeacherMapDTO> FillEditDatasAndSubjects(int IID, int classID, int sectionID)
        {
            return service.FillEditDatasAndSubjects(IID, classID, sectionID);
        }

        public List<ClassCoordinatorsDTO> FillClassSectionWiseCoordinators(int classID, int sectionID)
        {
            return service.FillClassSectionWiseCoordinators(classID, sectionID);
        }

        public List<MailFeeDueStatementReportDTO> GetFeeDueDatasForReportMail(DateTime asOnDate, int classID, int? sectionID)
        {
            return service.GetFeeDueDatasForReportMail(asOnDate, classID, sectionID);
        }

        public DirectorsDashBoardDTO GetTeacherRelatedDataForDirectorsDashBoard()
        {
            return service.GetTeacherRelatedDataForDirectorsDashBoard();
        }

        public List<KeyValueDTO> GetVehiclesByRoute(int routeID)
        {
            return service.GetVehiclesByRoute(routeID);
        }

        public MailFeeDueStatementReportDTO SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            return service.SendFeeDueMailReportToParent(gridData);
        }

        public EmployeeDTO UserProfileForDashBoard()
        {
            return service.UserProfileForDashBoard();
        }

        public Services.Contracts.Notifications.PushNotificationDTO GetNotificationsForDashBoard()
        {
            return service.GetNotificationsForDashBoard();
        }

        public TimeTableDTO GetWeeklyTimeTableForDashBoard(int weekDayID)
        {
            return service.GetWeeklyTimeTableForDashBoard(weekDayID);
        }


        public List<FeeDueCancellationDetailDTO> GetFeeDueForDueCancellation(long studentID, int academicYearID)
        {
            return service.GetFeeDueForDueCancellation(studentID, academicYearID);
        }

        public StudentFeeDueDTO GetGridLookUpsForSchoolCreditNote(long studentId)
        {
            return service.GetGridLookUpsForSchoolCreditNote(studentId);
        }
        public List<EmployeePromotionDTO> GetEmployeeDetailsByEmployeeID(long employeeID)
        {
            return service.GetEmployeeDetailsByEmployeeID(employeeID);
        }

        public string UpdateTCStatus(long? studentTransferRequestID, long TCContentID)
        {
            return service.UpdateTCStatus(studentTransferRequestID, TCContentID);
        }

        public string UpdateTCStatusToComplete(long? studentTransferRequestID)
        {
            return service.UpdateTCStatusToComplete(studentTransferRequestID);
        }

        public List<KeyValueDTO> Getstudentsubjectlist(long studentID)
        {
            return service.Getstudentsubjectlist(studentID);
        }

        public bool InsertProgressReportEntries(List<ProgressReportDTO> progressReportListDTOs, List<SettingDTO> settings)
        {
            return service.InsertProgressReportEntries(progressReportListDTOs, settings);
        } 
        
        public bool UpdateStudentProgressReportStatusID(MarkRegisterDTO dto)
        {
            return service.UpdateStudentProgressReportStatusID(dto);
        }

        public List<ProgressReportDTO> GetStudentProgressReports(ProgressReportDTO progressReport)
        {
            return service.GetStudentProgressReports(progressReport);
        }

        public List<ProgressReportDTO> GetStudentPublishedProgressReports(long studentID, long? examID)
        {
            return service.GetStudentPublishedProgressReports(studentID, examID);
        }

        public BankAccountDTO GetBankDetailsByBankID(long bankID)
        {
            return service.GetBankDetailsByBankID(bankID);
        }

        public List<SchoolPayerBankDTO> FillPayerBanksBySchoolID(long schoolID)
        {
            return service.FillPayerBanksBySchoolID(schoolID);
        }

        public string SaveQPayPayment(PaymentQPAYDTO paymentQPAYDTO)
        {
            return service.SaveQPayPayment(paymentQPAYDTO);
        }

        public StudentFeeDueDTO GetStudentFeeDueDetailsByID(long studentFeeDueID)
        {
            return service.GetStudentFeeDueDetailsByID(studentFeeDueID);
        }
        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByTrackID(long trackID)
        {
            return service.GetPaymentMasterVisaDataByTrackID(trackID);
        }

        public List<FeeCollectionFeeTypeDTO> GetFeeDuesForCampusTransfer(long studentId, int toSchoolID, int toAcademicYearID, int toClassID)
        {
            return service.GetFeeDuesForCampusTransfer(studentId, toSchoolID, toAcademicYearID, toClassID); ;
        }

        public StudentDTO GetStudentDetailsByStudentID(long studentID)
        {
            return service.GetStudentDetailsByStudentID(studentID);
        }

        public List<KeyValueDTO> GetStudentsByParameters(int academicYearID, int? classID, int? sectionID)
        {
            return service.GetStudentsByParameters(academicYearID,classID,sectionID);
        }

        public AccountsGroupDTO GetAccountGroupDataByID(int groupID)
        {
            return service.GetAccountGroupDataByID(groupID);
        }

        public List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID)
        {
            return service.GetAcademicYearByProgressReport(studentID);
        }

        public string UpdateScheduleLogStatus(ScheduleLogDTO dto)
        {
            return service.UpdateScheduleLogStatus(dto);
        }

        public FeePaymentDTO GetStudentFeeDetails(long studentID)
        {
            return service.GetStudentFeeDetails(studentID);
        }
        public List<FeePaymentHistoryDTO> GetFeeCollectionHistory(long studentID)
        {
            return service.GetFeeCollectionHistory(studentID);
        }

        public List<KeyValueDTO> GetProgressReportIDsByStudPromStatus(int classID, int sectionID, long academicYearID, byte statusID, int examID, int examGroupID)
        {
            return service.GetProgressReportIDsByStudPromStatus(classID, sectionID, academicYearID, statusID, examID, examGroupID);
        }

        public List<KeyValueDTO> GetSchoolsByLoginIDActiveStuds(long? loginID)
        {
            return service.GetSchoolsByLoginIDActiveStuds(loginID);
        }

        public StudentTransferRequestDTO FillStudentTransferData(long StudentID)
        {
            return service.FillStudentTransferData(StudentID);
        }

        public string GetPassageQuestionDetails(long passageQuestionID)
        {
            return service.GetPassageQuestionDetails(passageQuestionID);
        }

        public KeyValueDTO GetClassHeadTeacher(int classID, int sectionID, int academicYearID)
        {
            return service.GetClassHeadTeacher(classID, sectionID, academicYearID);
        }

        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            return service.GetClassCoordinator(classID, sectionID, academicYearID);
        }

        public List<KeyValueDTO> GetAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            return service.GetAssociateTeachers(classID, sectionID, academicYearID);
        }

        public List<KeyValueDTO> GetOtherTeachersByClass(int classID, int sectionID, int academicYearID)
        {
            return service.GetOtherTeachersByClass(classID, sectionID, academicYearID);
        }

        public CandidateDTO GetCandidateDetailsByCandidateID(long candidateID)
        {
            return service.GetCandidateDetailsByCandidateID(candidateID);
        }

        public List<DaysDTO> GetWeekDays()
        {
            return service.GetWeekDays();
        }

        public List<TimeTableAllocInfoHeaderDTO> GetSmartTimeTableByDate(int tableMasterId, DateTime timeTableDate, int classID)
        {
            return service.GetSmartTimeTableByDate(tableMasterId, timeTableDate, classID);
        }

        public List<TimeTableListDTO> GetTeacherSummary(int tableMasterID)
        {
            return service.GetTeacherSummary(tableMasterID);
        }

        public List<TimeTableListDTO> GetClassSummary(int tableMasterID)
        {
            return service.GetClassSummary(tableMasterID);
        }

        public List<TimeTableAllocationDTO> GetClassSectionTimeTableSummary(int tableMasterID)
        {
            return service.GetClassSectionTimeTableSummary(tableMasterID);
        }

        public List<TimeTableListDTO> GetTeacherSummaryByTeacherID(long employeeID, int tableMasterID)
        {
            return service.GetTeacherSummaryByTeacherID(employeeID, tableMasterID);
        }

        public List<TimeTableListDTO> GetClassSummaryDetails(long classID, long sectionID, int tableMasterID)
        {
            return service.GetClassSummaryDetails(classID,sectionID, tableMasterID);
        }

        public string GenerateSmartTimeTable(int tableMasterId, string timeTableDate)
        {
            return service.GenerateSmartTimeTable(tableMasterId, timeTableDate);
        }

        public OperationResultDTO ReAssignTimeTable(int tableMasterId, DateTime timeTableDate, int classID)
        {
            return service.ReAssignTimeTable(tableMasterId, timeTableDate, classID);
        }

        public List<KeyValueDTO> GetClasseByAcademicyear(int academicyearID)
        {
            throw new NotImplementedException();
        }
        public List<SubjectDTO> GetSubjectDetailsByClassID(int classID)
        {
            return service.GetSubjectDetailsByClassID(classID);
        }

        public List<KeyValueDTO> GetAcademicYearBySchoolForApplications(int schoolID, bool? isActive)
        {
            return service.GetAcademicYearBySchool(schoolID, isActive);
        }

        public List<KeyValueDTO> GetWeekDayByDate(string date)
        {
            return service.GetWeekDayByDate(date);
        }

        public List<KeyValueDTO> GetSetupScreens()
        {
            return service.GetSetupScreens();
        }

        public KeyValueDTO GetCurrentAcademicYearBySchoolID(long schoolID)
        {
            return service.GetCurrentAcademicYearBySchoolID(schoolID);
        }

        List<KeyValueDTO> ISchoolService.GetSubjectBySubjectID(byte subjectTypeID)
        {
            return service.GetSubjectBySubjectID(subjectTypeID);
        }

        List<KeyValueDTO> ISchoolService.GetDashboardCounts(long candidateID)
        {
            return service.GetDashboardCounts(candidateID);
        }

        List<CandidateOnlineExamMapDTO> ISchoolService.GetExamDetailsByCandidateID(long candidateID)
        {
            return service.GetExamDetailsByCandidateID(candidateID);
        }

        List<LessonPlanDTO> ISchoolService.ExtractUploadedFiles()
        {
            return service.ExtractUploadedFiles();
        }

        public List<KeyValueDTO> GetStudentsByParent(long parentID)
        {
            return service.GetStudentsByParent(parentID);
        }

        public List<KeyValueDTO> GetActiveStudentsByParent(long parentID)
        {
            return service.GetActiveStudentsByParent(parentID);
        }

        public List<StudentDTO> GetStudentsDetailsByParent(long parentID)
        {
            return service.GetStudentsDetailsByParent(parentID);
        }

        public List<StudentDTO> GetActiveStudentsDetailsByParent(long parentID)
        {
            return service.GetActiveStudentsDetailsByParent(parentID);
        }

        public List<StudentAttendenceDTO> GetClassWiseAttendanceDatas(int classId, int sectionId)
        {
            return service.GetClassWiseAttendanceDatas(classId, sectionId);
        }

        public string SendNotificationsToParentsByAttendance(List<StudentAttendenceDTO> studAttendance)
        {
            return service.SendNotificationsToParentsByAttendance(studAttendance);
        }

        List<KeyValueDTO> ISchoolService.GetSubjectBySubjectTypeID(byte subjectTypeID)
        {
            return service.GetSubjectBySubjectTypeID(subjectTypeID);
        }

        List<KeyValueDTO> ISchoolService.GetTeacherByClassAndSubject(string classIDs, string subjectIDs)
        {
            return service.GetTeacherByClassAndSubject(classIDs, subjectIDs);
        }

        public string SaveAllStudentAttendances(int classID, int sectionID, string attendanceDateString, byte? attendanceStatus)
        {
            return service.SaveAllStudentAttendances(classID, sectionID, attendanceDateString, attendanceStatus);
        }

        public List<AgendaDTO> GetStudentSubjectWiseAgendas(long loginID, long subjectID, string date)
        {
            return service.GetStudentSubjectWiseAgendas(loginID, subjectID, date);
        }

    }
}