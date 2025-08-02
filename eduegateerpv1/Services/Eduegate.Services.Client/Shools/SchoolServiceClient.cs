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
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.School.Payment;
namespace Eduegate.Services.Client.Shools
{
    public class SchoolServiceClient : BaseClient, ISchoolService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string productService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.BANNER_SERVICE_NAME);
        public SchoolServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public List<StudentApplicationDTO> GetStudentApplication(long loginID)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteApplication(long applicationId)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteSiblingApplication(long applicationId)
        {
            throw new NotImplementedException();
        }

        public StudentApplicationDTO GetApplication(long applicationId)
        {
            throw new NotImplementedException();
        }

        public StudentAttendenceDTO GetStudentAttendence(long studentID, DateTime date)
        {
            throw new NotImplementedException();
        }

        public StaffAttendenceDTO GetStaffAttendence(long staffID, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<GalleryDTO> GetGalleryView(long academicYearID)
        {
            throw new NotImplementedException();
        }

        public string SaveStudentAttendence(StudentAttendenceDTO attendence)
        {
            throw new NotImplementedException();
        }

        public string SaveAcademicCalendar(AcadamicCalendarDTO acadamic)
        {
            throw new NotImplementedException();
        }

        public string SaveStaffAttendance(StaffAttendenceDTO attendence)
        {
            throw new NotImplementedException();
        }

        public List<PresentStatusDTO> GetPresentStatuses()
        {
            throw new NotImplementedException();
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            throw new NotImplementedException();
        }
        public List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year)
        {
            throw new NotImplementedException();
        }

        //public List<ClassSubjectMapDTO> GetSubjectByClassID(int classID)
        //{
        //    throw new NotImplementedException();
        //}

        List<StudentApplicationDTO> ISchoolService.GetStudentApplication(long loginID)
        {
            throw new NotImplementedException();
        }

        OperationResult ISchoolService.DeleteApplication(long applicationId)
        {
            throw new NotImplementedException();
        }

        StudentApplicationDTO ISchoolService.GetApplication(long applicationId)
        {
            throw new NotImplementedException();
        }

        StudentAttendenceDTO ISchoolService.GetStudentAttendence(long studentID, DateTime date)
        {
            throw new NotImplementedException();
        }

        StaffAttendenceDTO ISchoolService.GetStaffAttendence(long staffID, DateTime date)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveStudentAttendence(StudentAttendenceDTO attendence)
        {
            throw new NotImplementedException();
        }
        string ISchoolService.SaveAcademicCalendar(AcadamicCalendarDTO acadamic)
        {
            throw new NotImplementedException();
        }

        List<StudentAttendenceDTO> ISchoolService.GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId)
        {
            throw new NotImplementedException();
        }
        List<PresentStatusDTO> ISchoolService.GetPresentStatuses()
        {
            throw new NotImplementedException();
        }
        List<StaffAttendenceDTO> ISchoolService.GetStaffAttendanceByMonthYear(int month, int year)
        {
            throw new NotImplementedException();
        }

        //List<ClassSubjectMapDTO> ISchoolService.GetSubjectByClassID(int classID)
        //{
        //    throw new NotImplementedException();
        //}

        string ISchoolService.SaveTimeTable(TimeTableAllocationDTO timeTableAlloc)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveTimeTableLog(TimeTableLogDTO timeTableLog)
        {
            throw new NotImplementedException();
        }

        List<TimeTableAllocInfoHeaderDTO> ISchoolService.GetTimeTableByClassID(int classID, int GetTimeTableByClassID)
        {
            throw new NotImplementedException();
        }

        void ISchoolService.DeleteTimeTableEntry(string timeTableAllocationID)
        {
            throw new NotImplementedException();
        }

        public List<SubjectTeacherMapDTO> GetTeacherBySubject(int classId, int SectionId, int subjectId)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveStudentFeeDue(StudentFeeDueDTO feeDueInfo)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveStudentApplication(StudentApplicationDTO studentApplication)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SendTodayAttendancePushNotification(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.ResentFromLeadLoginCredentials(StudentApplicationDTO studentApplication)
        {
            throw new NotImplementedException();
        }

        public string GenerateTimeTable(TimeTableAllocationDTO timeTableLog)
        {
            throw new NotImplementedException();
        }

        public List<StudentFeeDueDTO> FillFeeDue(int classId, long studentId)
        {
            throw new NotImplementedException();
        }

        public List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId)
        {
            throw new NotImplementedException();
        }
        public List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId)
        {
            throw new NotImplementedException();
        }
        public List<StudentFeeDueDTO> GetFeesByInvoiceNo(long studentFeeDueID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetClassStudents(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetClassStudentsAll(int academicYearID, List<int> classList, List<int> sectionList)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetClasswiseStudentData(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID)
        {
            throw new NotImplementedException();
        }

        public List<HealthEntryStudentMapDTO> GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStudentDatasFromRouteID(int routeId)
        {
            throw new NotImplementedException();
        }


        public List<CampusTransferMapDTO> GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAdvancedAcademicYearBySchool(int schoolID)
        {
            throw new NotImplementedException();
        }

        public ExamDTO GetClassByExam(int examId)
        {
            throw new NotImplementedException();
        }
        public List<FeePeriodsDTO> GetFeePeriodMonthlySplit(List<int> feePeriodIds)
        {
            throw new NotImplementedException();
        }


        public List<FeeCollectionDTO> GetStudentFeeCollection(long studentId)
        {
            throw new NotImplementedException();
        }

        public StudentRouteStopMapDTO GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public StudentRouteStopMapDTO GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public StaffRouteStopMapDTO GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public StaffRouteStopMapDTO GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<AssignmentDTO> GetAssignmentStudentwise(long studentId)
        {
            throw new NotImplementedException();
        }
        public List<MarkListViewDTO> GetMarkListStudentwise(long studentId)
        {
            throw new NotImplementedException();
        }
        public List<StudentDTO> GetStudentsSiblings(long parentId)
        {
            throw new NotImplementedException();
        }

        public List<AgeCriteriaMapDTO> GetAgeCriteriaByClassID(int classId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public LibraryBookDTO GetBooKCategoryName(long bookCategoryCodeId)
        {
            throw new NotImplementedException();
        }

        public LibraryTransactionDTO GetBookQuantityDetails(string CallAccNo)
        {
            throw new NotImplementedException();
        }
        public LibraryTransactionDTO GetIssuedBookDetails(string CallAccNo)
        {
            throw new NotImplementedException();
        }


        public List<CircularListDTO> GetCircularList(long parentId)
        {
            throw new NotImplementedException();
        }
        public List<CounselorHubListDTO> GetCounselorList(long parentId)
        {
            throw new NotImplementedException();
        }
        public List<CircularListDTO> GetCircularListByStudentID(long studentID)
        {
            throw new NotImplementedException();
        }
        public List<MarkRegisterDetailsSplitDTO> GetExamSubjectsMarks(long studentId, long examId, int ClassId)
        {
            throw new NotImplementedException();
        }
        public List<MarkGradeMapDTO> GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId)
        {
            throw new NotImplementedException();
        }

        public List<CollectFeeAccountDetailDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID)
        {
            throw new NotImplementedException();
        }

        public List<StudentDTO> GetStudentDetails(long studentId)
        {
            throw new NotImplementedException();
        }
        public List<StudentTransportDetailDTO> GetStudentTransportDetails(long studentID)
        {
            throw new NotImplementedException();
        }
        public TransportApplicationDTO GetTransportStudentDetailsByParentLoginID(long id)
        {
            throw new NotImplementedException();
        }

        public List<StudentDTO> GuardianInfo(long studentId)
        {
            throw new NotImplementedException();
        }

        public string FeeAccountPosting(DateTime fromDate, DateTime toDate, long cashierID)
        {
            throw new NotImplementedException();
        }


        public List<StudentLeaveApplicationDTO> GetLeaveApplication(long leaveapplicationId)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteLeaveApplication(long leaveapplicationId)
        {
            throw new NotImplementedException();
        }

        StudentLeaveApplicationDTO ISchoolService.GetLeaveApplication(long leaveapplicationId)
        {
            throw new NotImplementedException();
        }

        public List<StudentLeaveApplicationDTO> GetStudentLeaveApplication(long leaveapplicationId)
        {
            throw new NotImplementedException();
        }

        public List<TimeTableAllocationDTO> GetClassTimeTable(long studentId)
        {
            throw new NotImplementedException();
        }

        public List<ExamDTO> GetExamLists(long studentId)
        {
            throw new NotImplementedException();
        }

        public StudentTransferRequestDTO GetTransferApplication(long studentId)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteTransferApplication(long studentId)
        {
            throw new NotImplementedException();
        }

        public List<StudentTransferRequestDTO> GetStudentTransferApplication(long studentId)
        {
            throw new NotImplementedException();
        }

        public StudentApplicationDTO GetApplicationByLoginID(long loginID)
        {
            throw new NotImplementedException();
        }

        List<SubjectTeacherMapDTO> ISchoolService.GetTeacherBySubject(int classId, int SectionId, int subjectId)
        {
            throw new NotImplementedException();
        }



        List<StudentFeeDueDTO> ISchoolService.FillPendingFees(int classId, long studentId)
        {
            throw new NotImplementedException();
        }

        List<StudentFeeDueDTO> ISchoolService.GetFeesByInvoiceNo(long studentFeeDueID)
        {
            throw new NotImplementedException();
        }
        //List<StudentFeeDueDTO> ISchoolService.GetClasswiseStudentData(int classId, int sectionId)
        //{
        //    throw new NotImplementedException();
        //}
        List<KeyValueDTO> ISchoolService.GetClassStudents(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        List<AcademicYearDTO> ISchoolService.GetAcademicYearDataByCalendarID(long calendarID)
        {
            throw new NotImplementedException();
        }

        List<StudentDTO> ISchoolService.GetStudentsSiblings(long parentId)
        {
            throw new NotImplementedException();
        }

        List<AgeCriteriaMapDTO> ISchoolService.GetAgeCriteriaByClassID(int classId, int academicYearID)
        {
            throw new NotImplementedException();
        }


        List<FeeCollectionDTO> ISchoolService.GetStudentFeeCollection(long studentId)
        {
            throw new NotImplementedException();
        }

        StudentRouteStopMapDTO ISchoolService.GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        StudentRouteStopMapDTO ISchoolService.GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        StaffRouteStopMapDTO ISchoolService.GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        StaffRouteStopMapDTO ISchoolService.GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<AssignmentDTO> ISchoolService.GetAssignmentStudentwise(long studentId, int? SubjectID)
        {
            throw new NotImplementedException();
        }

        List<MarkListViewDTO> ISchoolService.GetMarkListStudentwise(long studentId)
        {
            throw new NotImplementedException();
        }

        ExamDTO ISchoolService.GetClassByExam(int examId)
        {
            throw new NotImplementedException();
        }

        List<FeePeriodsDTO> ISchoolService.GetFeePeriodMonthlySplit(List<int> feePeriodIds)
        {
            throw new NotImplementedException();
        }
        List<MarkRegisterDetailsSplitDTO> ISchoolService.GetExamSubjectsMarks(long studentId, long examId, int ClassId)
        {
            throw new NotImplementedException();
        }

        List<MarkGradeMapDTO> ISchoolService.GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId)
        {
            throw new NotImplementedException();
        }

        List<CollectFeeAccountDetailDTO> ISchoolService.GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID)
        {
            throw new NotImplementedException();
        }

        List<StudentDTO> ISchoolService.GetStudentDetails(long studentId)
        {
            throw new NotImplementedException();
        }
        List<StudentTransportDetailDTO> ISchoolService.GetStudentTransportDetails(long studentID)
        {
            throw new NotImplementedException();
        }

        public GuardianDTO GetGuardianDetails(long studentId)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.FeeAccountPosting(DateTime fromDate, DateTime toDate, long cashierID)
        {
            throw new NotImplementedException();
        }

        OperationResult ISchoolService.DeleteLeaveApplication(long leaveapplicationId)
        {
            throw new NotImplementedException();
        }

        List<StudentLeaveApplicationDTO> ISchoolService.GetStudentLeaveApplication(long leaveapplicationId)
        {
            throw new NotImplementedException();
        }

        List<TimeTableAllocationDTO> ISchoolService.GetClassTimeTable(long studentId)
        {
            throw new NotImplementedException();
        }

        List<ExamDTO> ISchoolService.GetExamLists(long studentId)
        {
            throw new NotImplementedException();
        }

        StudentTransferRequestDTO ISchoolService.GetTransferApplication(long studentId)
        {
            throw new NotImplementedException();
        }

        OperationResult ISchoolService.DeleteTransferApplication(long studentId)
        {
            throw new NotImplementedException();
        }

        List<StudentTransferRequestDTO> ISchoolService.GetStudentTransferApplication(long studentId)
        {
            throw new NotImplementedException();
        }

        StudentApplicationDTO ISchoolService.GetApplicationByLoginID(long loginID)
        {
            throw new NotImplementedException();
        }

        List<NotificationAlertsDTO> ISchoolService.GetNotificationAlerts(long loginID)
        {
            throw new NotImplementedException();
        }

        List<NotificationAlertsDTO> ISchoolService.GetAllNotificationAlerts(long loginID)
        {
            throw new NotImplementedException();
        }

        List<MailBoxDTO> ISchoolService.GetSendMailFromParent(long loginID)
        {
            throw new NotImplementedException();
        }

        int ISchoolService.GetNotificationAlertsCount(long loginID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.MarkNotificationAsRead(long loginID, long notificationAlertIID)
        {
            throw new NotImplementedException();
        }

        public List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentId)
        {
            throw new NotImplementedException();
        }


        public List<FeeCollectionDTO> GetSiblingDueDetailsFromStudentID(long StudentID)
        {
            throw new NotImplementedException();
        }

        public List<StudentFeeDueDTO> GetFineCollected(long studentId)
        {
            throw new NotImplementedException();
        }

        public List<StudentFeeDueDTO> FillFineDue(int classId, long studentId)
        {
            throw new NotImplementedException();
        }

        public List<StudentSkillRegisterSplitDTO> GetStudentSkills(long skillId)
        {
            throw new NotImplementedException();
        }

        public List<MarkGradeMapDTO> GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID)
        {
            throw new NotImplementedException();
        }
        public ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID)
        {
            throw new NotImplementedException();
        }

        public ProductBundleDTO GetProductDetails(long productId)
        {
            throw new NotImplementedException();
        }
        public ProductBundleDTO GetProductBundleData(long productskuId)
        {
            throw new NotImplementedException();
        }

        public EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public FineMasterStudentMapDTO GetFineAmount(int fineMasterID)
        {
            throw new NotImplementedException();
        }

        public List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId, int academicId)
        {
            throw new NotImplementedException();
        }

        public List<FeeCollectionDTO> FillFeeDueDataForSettlement(long studentId, int academicId)
        {
            throw new NotImplementedException();
        }
        public List<FeeCollectionPreviousFeesDTO> GetIssuedCreditNotesForCollectedFee(long studentId)
        {
            throw new NotImplementedException();
        }

        public EmployeesDTO GetEmployeeFromEmployeeID(long employeeID)
        {
            throw new NotImplementedException();
        }

        public AccountsGroupDTO GetGroupCodeByParentGroup(long parentGroupID)
        {
            throw new NotImplementedException();
        }

        public AccountsDTO GetAccountCodeByGroup(long groupID)
        {
            throw new NotImplementedException();
        }

        public StudentDTO GetStudentDetailFromStudentID(long StudentID)
        {
            throw new NotImplementedException();
        }
        public List<FeeCollectionDTO> AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative)
        {
            throw new NotImplementedException();
        }
        public string DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetFeeStructure(int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubGroup(int mainGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAccountCodeByLedger(int ledgerGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAccountGroup(int subGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAccountByGroupID(int groupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAccountByPayementModeID(long paymentModeID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAccountGroupByAccountID(long accountID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetFeePeriod(int academicYearID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetTransportFeePeriod(int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID)
        {
            throw new NotImplementedException();
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year)
        {
            throw new NotImplementedException();
        }

        public void DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID)
        {
            throw new NotImplementedException();
        }

        int GetStudentsCount()
        {
            throw new NotImplementedException();
        }
        int ISchoolService.GetStudentsCount()
        {
            throw new NotImplementedException();
        }

        OperationResult ISchoolService.DeleteSiblingApplication(long applicationId)
        {
            throw new NotImplementedException();
        }

        List<StudentFeeDueDTO> ISchoolService.FillFeeDue(int classId, long studentId)
        {
            throw new NotImplementedException();
        }

        List<StudentDTO> ISchoolService.GetClasswiseStudentData(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        List<ClassClassTeacherMapDTO> ISchoolService.GetTeacherDetails(long studentId)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.GetSiblingDueDetailsFromStudentID(long StudentID)
        {
            throw new NotImplementedException();
        }

        List<StudentFeeDueDTO> ISchoolService.GetFineCollected(long studentId)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetBookDetailsByCallNo(string CallAccNo)
        {
            throw new NotImplementedException();
        }

        List<StudentFeeDueDTO> ISchoolService.FillFineDue(int classId, long studentId)
        {
            throw new NotImplementedException();
        }

        List<StudentSkillRegisterSplitDTO> ISchoolService.GetStudentSkills(long skillId)
        {
            throw new NotImplementedException();
        }

        List<MarkGradeMapDTO> ISchoolService.GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID)
        {
            throw new NotImplementedException();
        }

        ProgressReportDTO ISchoolService.GetStudentSkillRegister(long studentId, int ClassID)
        {
            throw new NotImplementedException();
        }

        ProductBundleDTO ISchoolService.GetProductDetails(long productId)
        {
            throw new NotImplementedException();
        }

        EmployeeTimeSheetsWeeklyDTO ISchoolService.GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        ProductBundleDTO ISchoolService.GetProductBundleData(long productskuId)
        {
            throw new NotImplementedException();
        }

        FineMasterStudentMapDTO ISchoolService.GetFineAmount(int fineMasterID)
        {
            throw new NotImplementedException();
        }

        List<FeeDuePaymentDTO> ISchoolService.FillFeePaymentDetails(long loginId)
        {
            throw new NotImplementedException();
        }

        List<FeeDueFeeTypeMapDTO> ISchoolService.FillFeeDueForSettlement(long studentId, int academicId)
        {
            throw new NotImplementedException();
        }


        List<FeeCollectionDTO> ISchoolService.FillFeeDueDataForSettlement(long studentId, int academicId)
        {
            throw new NotImplementedException();
        }
        List<FeeCollectionPreviousFeesDTO> ISchoolService.GetIssuedCreditNotesForCollectedFee(long studentId)
        {
            throw new NotImplementedException();
        }

        EmployeesDTO ISchoolService.GetEmployeeFromEmployeeID(long employeeID)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative)
        {
            throw new NotImplementedException();
        }
        string ISchoolService.DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID)
        {
            throw new NotImplementedException();
        }
        StudentDTO ISchoolService.GetStudentDetailFromStudentID(long StudentID)
        {
            throw new NotImplementedException();
        }

        GuardianDTO ISchoolService.GetParentDetailFromParentID(long ParentID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetFeeStructure(int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetFeePeriod(int academicYearID, long studentID, int? feeMasterID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetTransportFeePeriod(int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<AcademicCalenderEventDateDTO> ISchoolService.GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID)
        {
            throw new NotImplementedException();
        }
        List<AcademicCalenderEventDateDTO> ISchoolService.GetAcademicCalenderByMonthYear(int month, int year)
        {
            throw new NotImplementedException();
        }

        void ISchoolService.DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetCastByRelegion(int relegionID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetToNotificationUsersByUser(int userID, int branchID, string user)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStreamByStreamGroup(byte? streamGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStreamCompulsorySubjects(byte? streamID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStreamOptionalSubjects(byte? streamID)
        {
            throw new NotImplementedException();
        }

        public List<StreamDTO> GetFullStreamListDatas()
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubSkillByGroup(int skillGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAcademicYearBySchool(int schoolID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetClasseByAcademicyear(int academicyearID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetClassesBySchool(byte schoolID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSectionsBySchool(byte schoolID)
        {
            throw new NotImplementedException();
        }

        public string GetProgressReportName(long schoolID, int? classID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetRouteStopsByRoute(int routeID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubjectByClass(int classID)
        {
            throw new NotImplementedException();
        }

        public StaticContentDataDTO GetAboutandContactDetails(long contentID)
        {
            throw new NotImplementedException();
        }

        public LibraryTransactionDTO GetLibraryStudentFromStudentID(long studentID)
        {
            throw new NotImplementedException();
        }

        public LibraryTransactionDTO GetLibraryStaffFromEmployeeID(long employeeID)
        {
            throw new NotImplementedException();
        }

        public TransportApplicationDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            throw new NotImplementedException();
        }
        public List<SubjectMarkEntryDetailDTO> FillClassStudents(long classID, long sectionID)
        {
            throw new NotImplementedException();
        }

        public List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examId, int classId, int sectionID, int subjectID)
        {
            throw new NotImplementedException();
        }

        public ClassSubjectSkillGroupMapDTO GetExamMarkDetails(long subjectID, long examID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetTeacherEmailByParentLoginID(long loginID)
        {
            throw new NotImplementedException();
        }

        public CandidateDTO GetCandidateDetails(string username, string password)
        {
            throw new NotImplementedException();
        }

        List<RemarksEntryStudentsDTO> ISchoolService.GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID)
        {
            throw new NotImplementedException();
        }

        public string ShiftStudentSection(ClassSectionShiftingDTO classSectionShiftingDTO)
        {
            throw new NotImplementedException();
        }

        public ClassSectionShiftingDTO GetStudentsForShifting(int classID, int sectionID)
        {
            throw new NotImplementedException();
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate)
        {
            throw new NotImplementedException();
        }

        public void DeleteDailyTimeTableEntry(long timeTableLogID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            throw new NotImplementedException();
        }


        public List<KeyValueDTO> GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }
        List<MarkRegisterDetailsDTO> ISchoolService.GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }

        public string SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO)
        {
            throw new NotImplementedException();
        }

        List<MarkRegisterDetailsDTO> ISchoolService.GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }

        public string SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.UpdateMarkEntryStatus(MarkRegisterDTO dto)
        {
            throw new NotImplementedException();
        }
        public string SaveMarkEntry(MarkRegisterDTO markRegisterDTO)
        {
            throw new NotImplementedException();
        }

        List<StudentMarkEntryDTO> ISchoolService.GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }

        public List<StudentMarkEntryDTO> GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO)
        {
            throw new NotImplementedException();
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO)
        {
            throw new NotImplementedException();
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO)
        {
            throw new NotImplementedException();
        }
        List<KeyValueDTO> ISchoolService.GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubjectByType(byte subjectTypeID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID)
        {
            throw new NotImplementedException();
        }

        List<MarkRegisterDetailsDTO> ISchoolService.GetSubjectsAndMarksToPublish(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }
        public string GetAcademicYearNameByID(int academicYearID)
        {
            throw new NotImplementedException();
        }

        public string GetSchoolNameByID(int schoolID)
        {
            throw new NotImplementedException();
        }

        public List<AcademicYearDTO> GetAcademicYear(int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<AgendaDTO> GetAgendaList(long loginID)
        {
            throw new NotImplementedException();
        }

        public List<LessonPlanDTO> GetLessonPlanList(long studentID)
        {
            throw new NotImplementedException();
        }

        public List<TransportApplicationStudentMapDTO> GetTransportApplication(long loginID)
        {
            throw new NotImplementedException();
        }

        public List<SchoolGeoLocationDTO> GetGeoSchoolSetting(long schoolID)
        {
            throw new NotImplementedException();
        }

        public void SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dto)
        {
            throw new NotImplementedException();
        }

        public void ClearGeoSchoolSetting(int schoolID)
        {
            throw new NotImplementedException();
        }

        List<RouteShiftingStudentMapDTO> ISchoolService.GetStudentDatasFromRouteID(int routeId)
        {
            throw new NotImplementedException();
        }

        List<ClassSectionSubjectListMapDTO> ISchoolService.FillClassandSectionWiseSubjects(int classID, int sectionID, int IID)
        {
            throw new NotImplementedException();
        }

        List<RouteShiftingStaffMapDTO> ISchoolService.GetStaffDatasFromRouteID(int routeId)
        {
            throw new NotImplementedException();
        }

        public List<AcademicClassMapWorkingDayDTO> GetMonthAndYearByAcademicYearID(int? academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicMonthAndYearByCalendarID(long? calendarID)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.FillCollectedFeesDetails(long studentId, int academicId)
        {
            throw new NotImplementedException();
        }
        public List<FeeCollectionDTO> FillCollectedFeesDetails(long studentId, int academicId)
        {
            throw new NotImplementedException();
        }

        public string CheckAndInsertCalendarEntries(long calendarID)
        {
            throw new NotImplementedException();
        }



        public List<KeyValueDTO> GetCalendarByTypeID(byte calendarTypeID)
        {
            throw new NotImplementedException();
        }

        List<AcademicCalenderEventDateDTO> ISchoolService.GetCalendarEventsByCalendarID(long calendarID)
        {
            throw new NotImplementedException();
        }

        List<EmployeesDTO> ISchoolService.GetEmployeesByCalendarID(long calendarID)
        {
            throw new NotImplementedException();
        }

        List<PresentStatusDTO> ISchoolService.GetStaffPresentStatuses()
        {
            throw new NotImplementedException();
        }

        //LibraryBookDTO ISchoolService.GetBookDetailsChange(long BookID)
        //{
        //    throw new NotImplementedException();
        //}

        public List<LibraryTransactionDTO> GetBookDetailsForIssue(string CallAccNo)
        {
            throw new NotImplementedException();
        }

        public SchoolCreditNoteDTO AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type)
        {
            throw new NotImplementedException();
        }

        SchoolCreditNoteDTO ISchoolService.AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type)
        {
            throw new NotImplementedException();
        }

        public List<SchoolCreditNoteDTO> GetCreditNoteNumber(long? headID, long studentID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSchoolsByParentLoginID(long? loginID)
        {
            throw new NotImplementedException();
        }

        public AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByCandidateID(long candidateID)
        {
            throw new NotImplementedException();
        }

        public StudentApplicationDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveStaffAttendance(StaffAttendenceDTO attendence)
        {
            throw new NotImplementedException();
        }

        List<StudentAttendenceDTO> ISchoolService.GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            throw new NotImplementedException();
        }

        List<TimeTableAllocInfoHeaderDTO> ISchoolService.GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate)
        {
            throw new NotImplementedException();
        }

        void ISchoolService.DeleteDailyTimeTableEntry(long timeTableLogID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.GenerateTimeTable(TimeTableAllocationDTO timeTableLog)
        {
            throw new NotImplementedException();
        }

        List<AcademicYearDTO> ISchoolService.GetAcademicYear(int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAdvancedAcademicYearBySchool(int schoolID)
        {
            throw new NotImplementedException();
        }

        List<HealthEntryStudentMapDTO> ISchoolService.GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID)
        {
            throw new NotImplementedException();
        }

        List<EventTransportAllocationMapDTO> ISchoolService.GetStudentandStaffsByRouteIDforEvent(EventTransportAllocationDTO eventDto)
        {
            throw new NotImplementedException();
        }

        EventTransportAllocationMapDTO ISchoolService.GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType)
        {
            throw new NotImplementedException();
        }

        EventTransportAllocationMapDTO ISchoolService.GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType)
        {
            throw new NotImplementedException();
        }

        List<CampusTransferMapDTO> ISchoolService.GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        LibraryBookDTO ISchoolService.GetBooKCategoryName(long bookCategoryCodeId)
        {
            throw new NotImplementedException();
        }

        LibraryTransactionDTO ISchoolService.GetBookQuantityDetails(string CallAccNo, int? bookMapID)
        {
            throw new NotImplementedException();
        }

        LibraryTransactionDTO ISchoolService.GetIssuedBookDetails(string CallAccNo, int? bookMapID)
        {
            throw new NotImplementedException();
        }

        List<CircularListDTO> ISchoolService.GetCircularList(long parentId)
        {
            throw new NotImplementedException();
        }
        List<CounselorHubListDTO> ISchoolService.GetCounselorList(long parentId)
        {
            throw new NotImplementedException();
        }

        List<CircularListDTO> ISchoolService.GetCircularListByStudentID(long studentID)
        { throw new NotImplementedException(); }
        List<GalleryDTO> ISchoolService.GetGalleryView(long academicYearID)
        {
            throw new NotImplementedException();
        }

        List<AgendaDTO> ISchoolService.GetAgendaList(long loginID)
        {
            throw new NotImplementedException();
        }

        List<LessonPlanDTO> ISchoolService.GetLessonPlanList(long studentID)
        {
            throw new NotImplementedException();
        }

        TransportApplicationDTO ISchoolService.GetTransportStudentDetailsByParentLoginID(long id)
        {
            throw new NotImplementedException();
        }

        AccountsGroupDTO ISchoolService.GetGroupCodeByParentGroup(long parentGroupID)
        {
            throw new NotImplementedException();
        }

        AccountsDTO ISchoolService.GetAccountCodeByGroup(long groupID)
        {
            throw new NotImplementedException();
        }

        List<CurrencyDTO> ISchoolService.GetCurrencyDetails()
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSubGroup(int mainGroupID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAccountCodeByLedger(int ledgerGroupID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAccountGroup(int subGroupID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAccountByGroupID(int groupID)
        {
            throw new NotImplementedException();
        }
        List<KeyValueDTO> ISchoolService.GetCostCenterByAccount(long accountID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAccountByPayementModeID(long paymentModeID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAccountGroupByAccountID(long accountID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetCastByRelegion(int relegionID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetToNotificationUsersByUser(int userID, int branchID, string user)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetStreamByStreamGroup(byte? streamGroupID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetStreamCompulsorySubjects(byte? streamGroupID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetStreamOptionalSubjects(byte? streamGroupID)
        {
            throw new NotImplementedException();
        }

        List<StreamDTO> ISchoolService.GetFullStreamListDatas()
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSubSkillByGroup(int skillGroupID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetAcademicYearBySchool(int schoolID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetClasseByAcademicyear(int academicyearID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetClassesBySchool(byte schoolID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSectionsBySchool(byte schoolID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.GetProgressReportName(long schoolID, int? classID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetRouteStopsByRoute(int routeID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSubjectByClass(int classID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSubjectbyQuestionGroup(long questionGroupID)
        {
            throw new NotImplementedException();
        }

        OnlineExamQuestionDTO ISchoolService.GetQuestionDetailsByQuestionID(long questionID)
        {
            throw new NotImplementedException();
        }

        StaticContentDataDTO ISchoolService.GetAboutandContactDetails(long contentID)
        {
            throw new NotImplementedException();
        }

        LibraryTransactionDTO ISchoolService.GetLibraryStudentFromStudentID(long studentID)
        {
            throw new NotImplementedException();
        }

        LibraryTransactionDTO ISchoolService.GetLibraryStaffFromEmployeeID(long employeeID)
        {
            throw new NotImplementedException();
        }

        TransportApplicationDTO ISchoolService.GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            throw new NotImplementedException();
        }

        List<SubjectMarkEntryDetailDTO> ISchoolService.FillClassStudents(long classID, long sectionID)
        {
            throw new NotImplementedException();
        }

        List<SubjectMarkEntryDetailDTO> ISchoolService.GetSubjectsMarkData(long examID, int classID, int sectionID, int subjectID)
        {
            throw new NotImplementedException();
        }

        ClassSubjectSkillGroupMapDTO ISchoolService.GetExamMarkDetails(long subjectID, long examID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetTeacherEmailByParentLoginID(long loginID)
        {
            throw new NotImplementedException();
        }

        CandidateDTO ISchoolService.GetCandidateDetails(string username, string password)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.ShiftStudentSection(ClassSectionShiftingDTO classSectionShiftingDTO)
        {
            throw new NotImplementedException();
        }

        ClassSectionShiftingDTO ISchoolService.GetStudentsForShifting(int classID, int sectionID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            throw new NotImplementedException();
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            throw new NotImplementedException();
        }
        List<KeyValueDTO> ISchoolService.GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SaveMarkEntry(MarkRegisterDTO markRegisterDTO)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSubjectByType(byte subjectTypeID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.GetAcademicYearNameByID(int academicYearID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.GetSchoolNameByID(int schoolID)
        {
            throw new NotImplementedException();
        }

        List<TransportApplicationStudentMapDTO> ISchoolService.GetTransportApplication(long loginID)
        {
            throw new NotImplementedException();
        }

        List<SchoolGeoLocationDTO> ISchoolService.GetGeoSchoolSetting(long schoolID)
        {
            throw new NotImplementedException();
        }

        void ISchoolService.SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dto)
        {
            throw new NotImplementedException();
        }

        void ISchoolService.ClearGeoSchoolSetting(int schoolID)
        {
            throw new NotImplementedException();
        }

        List<AcademicClassMapWorkingDayDTO> ISchoolService.GetMonthAndYearByAcademicYearID(int? academicYearID)
        {
            throw new NotImplementedException();
        }

        List<AcademicCalenderEventDateDTO> ISchoolService.GetAcademicMonthAndYearByCalendarID(long? calendarID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.CheckAndInsertCalendarEntries(long calendarID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetCalendarByTypeID(byte calendarTypeID)
        {
            throw new NotImplementedException();
        }

        List<SchoolCreditNoteDTO> ISchoolService.GetCreditNoteNumber(long? headID, long studentID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSchoolsByParentLoginID(long? loginID)
        {
            throw new NotImplementedException();
        }

        AcademicYearDTO ISchoolService.GetAcademicYearDataByAcademicYearID(int? academicYearID)
        {
            throw new NotImplementedException();
        }

        List<OnlineExamQuestionDTO> ISchoolService.GetQuestionsByCandidateID(long candidateID)
        {
            throw new NotImplementedException();
        }

        StudentApplicationDTO ISchoolService.GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetUnitByUnitGroup(int groupID)
        {
            throw new NotImplementedException();
        }

        List<OnlineExamResultDTO> ISchoolService.GetOnlineExamsResultByCandidateID(long candidateID)
        {
            throw new NotImplementedException();
        }

        public List<UnitDTO> GetUnitDataByUnitGroup(int groupID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.SubmitAmountAsLog(decimal? totalAmount)
        {
            throw new NotImplementedException();
        }

        PaymentLogDTO ISchoolService.GetLastLogData()
        {
            throw new NotImplementedException();
        }

        PaymentMasterVisaDTO ISchoolService.GetPaymentMasterVisaData()
        {
            throw new NotImplementedException();
        }

        public PaymentMasterVisaDTO UpdatePaymentMasterVisa(PaymentMasterVisaDTO paymentMasterVisaDTO)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO FeeCollectionEntry(List<FeeCollectionDTO> feeCollectionList)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.UpdateStudentsFeePaymentStatus(string transactionNo, long? parentLoginID)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.GetStudentFeeCollectionsHistory(StudentDTO studentData, byte? schoolID, int? academicYearID)
        {
            throw new NotImplementedException();
        }

        List<AcademicYearDTO> ISchoolService.GetCurrentAcademicYearsData()
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAllAcademicYearBySchoolID(int schoolID)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.GetFeeCollectionHistories(List<StudentDTO> studentDatas, byte? schoolID, int? academicYearID)
        {
            throw new NotImplementedException();
        }

        PaymentLogDTO ISchoolService.GetAndInsertLogDataByTransactionID(string transID)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.CheckFeeCollectionStatusByTransactionNumber(string transactionNumber)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID)
        {
            throw new NotImplementedException();
        }

        List<FeeDueMonthlySplitDTO> ISchoolService.GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID)
        {
            throw new NotImplementedException();
        }

        List<FeeCollectionDTO> ISchoolService.GetFeeCollectionDetailsByTransactionNumber(string transactionNumber, string mailID, string feeReceiptNo)
        {
            throw new NotImplementedException();
        }

        OperationResult ISchoolService.DeleteTransportApplication(long transportapplicationId)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetRoutesByRouteGroupID(int? routeGroupID)
        {
            throw new NotImplementedException();
        }

        public AcademicYearDTO GetAcademicYearDataByGroupID(int? routeGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetPickupStopMapsByRouteGroupID(int? routeGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetDropStopMapsByRouteGroupID(int? routeGroupID)
        {
            throw new NotImplementedException();
        }

        public List<StudentPickupRequestDTO> GetStudentPickupRequestsByLoginID(long loginID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ISchoolService.GetSubLedgerByAccount(long accountID)
        {
            throw new NotImplementedException();
        }

        public List<ProgressReportNewDTO> SaveProgressReportData(List<ProgressReportNewDTO> toDtoList)
        {
            throw new NotImplementedException();
        }

        List<ProgressReportNewDTO> ISchoolService.SaveProgressReportData(List<ProgressReportNewDTO> toDtoList)
        {
            throw new NotImplementedException();
        }

        public string UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList)
        {
            throw new NotImplementedException();
        }

        string ISchoolService.UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList)
        {
            throw new NotImplementedException();
        }

        public List<ProgressReportNewDTO> GetProgressReportData(int classID, int? sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<ProgressReportNewDTO> ISchoolService.GetProgressReportData(int classID, int? sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        List<ProgressReportNewDTO> ISchoolService.GetProgressReportList(long studentID, int classID, int sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public string CancelStudentPickupRequestByID(long pickupRequestID)
        {
            throw new NotImplementedException();
        }

        public string CancelTransportApplication(long mapIID)
        {
            throw new NotImplementedException();
        }

        public TransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStaffDetailsByStudentID(int studentID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetParentDetailsByStudentID(int studentID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStudentDetailsByStaff(long staffID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetStudentDetailsByParent(long parentID)
        {
            throw new NotImplementedException();
        }

        public decimal? GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID)
        {
            throw new NotImplementedException();
        }
        public List<StudentFeeConcessionDetailDTO> GetFeeDueForConcession(long studentID, int academicYearID)
        {
            throw new NotImplementedException();
        }
        string ISchoolService.SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public DashBoardChartDTO GetCountsForDashBoardMenuCards(int chartID)
        {
            throw new NotImplementedException();
        }
        public DashBoardChartDTO GetTwinViewData(int chartID,string referenceID)
        {
            throw new NotImplementedException();
        }
        List<ClassClassTeacherMapDTO> ISchoolService.FillEditDatasAndSubjects(int IID, int classID, int sectionID)
        {
            throw new NotImplementedException();
        }

        List<ClassCoordinatorsDTO> ISchoolService.FillClassSectionWiseCoordinators(int classID, int sectionID)
        {
            throw new NotImplementedException();
        }

        List<MailFeeDueStatementReportDTO> ISchoolService.GetFeeDueDatasForReportMail(DateTime asOnDate, int classID, int? sectionID)
        {
            throw new NotImplementedException();
        }

        public DirectorsDashBoardDTO GetTeacherRelatedDataForDirectorsDashBoard()
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetVehiclesByRoute(int routeID)
        {
            throw new NotImplementedException();
        }

        MailFeeDueStatementReportDTO ISchoolService.SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO UserProfileForDashBoard()
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.Notifications.PushNotificationDTO GetNotificationsForDashBoard()
        {
            throw new NotImplementedException();
        }


        public TimeTableDTO GetWeeklyTimeTableForDashBoard(int weekDayID)
        {
            throw new NotImplementedException();
        }

        public List<FeeDueCancellationDetailDTO> GetFeeDueForDueCancellation(long studentID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        StudentFeeDueDTO ISchoolService.GetGridLookUpsForSchoolCreditNote(long studentId)
        {
            throw new NotImplementedException();
        }

        List<EmployeePromotionDTO> ISchoolService.GetEmployeeDetailsByEmployeeID(long employeeID)
        {
            throw new NotImplementedException();
        }

        public string UpdateTCStatus(long? studentID, long TCContentID)
        {
            throw new NotImplementedException();
        }

        public string UpdateTCStatusToComplete(long? studentID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> Getstudentsubjectlist(long studentId)
        {
            throw new NotImplementedException();
        }

        public string InsertProgressReportEntries(List<ProgressReportDTO> progressReportListDTOs, List<SettingDTO> settings)
        {
            throw new NotImplementedException();
        }

        public List<ProgressReportDTO> GetStudentProgressReports(ProgressReportDTO progressReport)
        {
            throw new NotImplementedException();
        }

        public List<ProgressReportDTO> GetStudentPublishedProgressReports(long studentID, long? examID)
        {
            throw new NotImplementedException();
        }

        BankAccountDTO ISchoolService.GetBankDetailsByBankID(long bankID)
        {
            throw new NotImplementedException();
        }

        List<SchoolPayerBankDTO> ISchoolService.FillPayerBanksBySchoolID(long schoolID)
        {
            throw new NotImplementedException();
        }

        public string SaveQPayPayment(PaymentQPAYDTO paymentQPAYDTO)
        {
            throw new NotImplementedException();
        }

        public StudentFeeDueDTO GetStudentFeeDueDetailsByID(long studentFeeDueID)
        {
            throw new NotImplementedException();
        }

        public List<FeeCollectionFeeTypeDTO> GetFeeDuesForCampusTransfer(long studentId, int toSchoolID, int toAcademicYearID, int toClassID)
        {
            throw new NotImplementedException();
        }

        PaymentMasterVisaDTO ISchoolService.GetPaymentMasterVisaDataByTrackID(long trackID)
        {
            throw new NotImplementedException();
        }

        public StudentDTO GetStudentDetailsByStudentID(long studentID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetStudentsByParameters(int academicYearID, int? classID, int? sectionID)
        {
            throw new NotImplementedException();
        }

        public AccountsGroupDTO GetAccountGroupDataByID(int groupID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID)
        {
            throw new NotImplementedException();
        }

        public string UpdateScheduleLogStatus(ScheduleLogDTO dto)
        {
            throw new NotImplementedException();
        }

        public FeePaymentDTO GetStudentFeeDetails(long studentID)
        {
            throw new NotImplementedException();
        }

        public List<FeePaymentHistoryDTO> GetFeeCollectionHistory(long studentID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetProgressReportIDsByStudPromStatus(int classID, int sectionID, long academicYearID, byte statusID, int examID, int examGroupID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSchoolsByLoginIDActiveStuds(long? loginID)
        {
            throw new NotImplementedException();
        }

        public List<StudentBehavioralRemarksDTO> GetStudentBehavioralRemarks(StudentBehavioralRemarksDTO studentBehavioralRemarksDTO)
        {
            throw new NotImplementedException();
        }

        public StudentTransferRequestDTO FillStudentTransferData(long StudentID)
        {
            throw new NotImplementedException();
        }

        public string GetPassageQuestionDetails(long passageQuestionID)
        {
            throw new NotImplementedException();
        }

        public KeyValueDTO GetClassHeadTeacher(int classID, int sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetOtherTeachersByClass(int classID, int sectionID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public CandidateDTO GetCandidateDetailsByCandidateID(long candidateID)
        {
            throw new NotImplementedException();
        }

        public List<DaysDTO> GetWeekDays()
        {
            throw new NotImplementedException();
        }

        public List<TimeTableAllocInfoHeaderDTO> GetSmartTimeTableByDate(int tableMasterId, DateTime timeTableDate)
        {
            throw new NotImplementedException();
        }

        public List<TimeTableListDTO> GetTeacherSummary()
        {
            throw new NotImplementedException();
        }

        public List<TimeTableListDTO> GetClassSummary()
        {
            throw new NotImplementedException();
        }

        public List<TimeTableAllocationDTO> GetClassSectionTimeTableSummary()
        {
            throw new NotImplementedException();
        }

        public List<TimeTableListDTO> GetTeacherSummaryByTeacherID(long employeeID)
        {
            throw new NotImplementedException();
        }

        public List<TimeTableListDTO> GetClassSummaryDetails(long classID, long sectionID)
        { 
            throw new NotImplementedException(); 
        }

    }
}