using Eduegate.Domain.Entity.OnlineExam;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Catalog;
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
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts.Schools
{
    [ServiceContract]
    public interface ISchoolService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentApplication?loginID={loginID}")]
        List<StudentApplicationDTO> GetStudentApplication(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteApplication?applicationId={applicationId}")]
        OperationResult DeleteApplication(long applicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteSiblingApplication?applicationId={applicationId}")]
        OperationResult DeleteSiblingApplication(long applicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetApplication?applicationId={applicationId}")]
        StudentApplicationDTO GetApplication(long applicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentAttendence?studentID={studentID}&date={date}")]
        StudentAttendenceDTO GetStudentAttendence(long studentID, DateTime date);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaffAttendence?staffID={staffID}&date={date}")]
        StaffAttendenceDTO GetStaffAttendence(long staffID, DateTime date);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveStudentAttendence")]
        string SaveStudentAttendence(StudentAttendenceDTO attendence);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveAcademicCalendar")]
        string SaveAcademicCalendar(AcadamicCalendarDTO acadamic);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveStaffAttendance")]
        string SaveStaffAttendance(StaffAttendenceDTO attendence);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentAttendenceByYearMonth?month={month}&year={year}&classId={classId}&sectionId={sectionId}")]
        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentAttendenceByYearMonthStudentId?month={month}&year={year}&studentId={studentId}")]
        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaffAttendanceByMonthYear?month={month}&year={year}")]
        List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPresentStatuses")]
        List<PresentStatusDTO> GetPresentStatuses();

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubjectByClassID?classID={classID}")]
        //List<ClassSubjectMapDTO> GetSubjectByClassID(int classID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveTimeTable")]
        string SaveTimeTable(TimeTableAllocationDTO timeTableAlloc);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveTimeTableLog")]
        string SaveTimeTableLog(TimeTableLogDTO timeTableAlloc);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTimeTableByClassID?classID={classID}&tableMasterId={tableMasterId}")]
        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByClassID(int classID, int tableMasterId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTimeTableByDate?classID={classID}&tableMasterId={tableMasterId}&timeTableDate={timeTableDate}")]
        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteTimeTableEntry?timeTableAllocationID={timeTableAllocationID}")]
        void DeleteTimeTableEntry(string timeTableAllocationID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteDailyTimeTableEntry?timeTableLogID={timeTableLogID}")]
        void DeleteDailyTimeTableEntry(long timeTableLogID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTeacherBySubject?classID={classID},SectionId={sectionId},SubjectId={subjectId}")]
        List<SubjectTeacherMapDTO> GetTeacherBySubject(int classId, int SectionId, int subjectId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveStudentFeeDue")]
        string SaveStudentFeeDue(StudentFeeDueDTO feeDueInfo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveStudentApplication")]
        string SaveStudentApplication(StudentApplicationDTO studentApplication);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SendTodayAttendancePushNotification?classId={classId},sectionId={sectionId}")]
        string SendTodayAttendancePushNotification(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ResentFromLeadLoginCredentials")]
        string ResentFromLeadLoginCredentials(StudentApplicationDTO studentApplication);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerateTimeTable")]
        string GenerateTimeTable(TimeTableAllocationDTO timeTableLog);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillFeeDue?classId={classId},studentId={studentId}")]
        List<StudentFeeDueDTO> FillFeeDue(int classId, long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillPendingFees?classId={classId},studentId={studentId}")]
        List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetInvoiceForCreditNote?classId={classId},studentId={studentId},feeMasterID={feeMasterID},feePeriodID={feePeriodID}")]
        List<KeyValueDTO> GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeDueMonthlyDetails?studentFeeDueID={studentFeeDueID},feeMasterID={feeMasterID},feePeriodID={feePeriodID}")]
        List<FeeDueMonthlySplitDTO> GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeesByInvoiceNo?studentFeeDueID={studentFeeDueID}")]
        List<StudentFeeDueDTO> GetFeesByInvoiceNo(long studentFeeDueID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClassStudents?classId={classId},sectionId={sectionId})")]
        List<KeyValueDTO> GetClassStudents(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearDataByCalendarID?calendarID={calendarID})")]
        List<AcademicYearDTO> GetAcademicYearDataByCalendarID(long calendarID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYear?academicYearID={academicYearID})")]
        List<AcademicYearDTO> GetAcademicYear(int academicYearID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAdvancedAcademicYearBySchool?schoolID={schoolID})")]
        List<KeyValueDTO> GetAdvancedAcademicYearBySchool(int schoolID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClasswiseStudentData?classId={classId},sectionId={sectionId})")]
        List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClasswiseRemarksEntryStudentData?classId={classId},sectionId={sectionId},examGroupID={examGroupID})")]
        List<RemarksEntryStudentsDTO> GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHealthEntryStudentData?classId={classId},sectionId={sectionId},academicYearID={academicYearID},examGroupID={examGroupID})")]
        List<HealthEntryStudentMapDTO> GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentandStaffsByRouteIDforEvent?routeID={routeID},IsRouteType={IsRouteType})")]
        List<EventTransportAllocationMapDTO> GetStudentandStaffsByRouteIDforEvent(int routeID, string IsRouteType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentTransportDetailsByStudentID?studentID={studentID},IsRouteType={IsRouteType})")]
        EventTransportAllocationMapDTO GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaffTransportDetailsByStaffID?staffID={staffID},IsRouteType={IsRouteType})")]
        EventTransportAllocationMapDTO GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentDatasFromRouteID?routeId={routeId},academicId={academicId})")]
        List<RouteShiftingStudentMapDTO> GetStudentDatasFromRouteID(int routeId, int academicId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillClassandSectionWiseSubjects?classID={classID},sectionID={sectionID},IID={IID})")]
        List<ClassSectionSubjectListMapDTO> FillClassandSectionWiseSubjects(int classID, int sectionID, int IID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaffDatasFromRouteID?routeId={routeId},academicId={academicId})")]
        List<RouteShiftingStaffMapDTO> GetStaffDatasFromRouteID(int routeId, int academicId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClasswiseStudentDataForCampusTransfer?classId={classId},sectionId={sectionId})")]
        List<CampusTransferMapDTO> GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentsSiblings?parentId={parentId}")]
        List<StudentDTO> GetStudentsSiblings(long parentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAgeCriteriaByClassID?classId={classId}")]
        List<AgeCriteriaMapDTO> GetAgeCriteriaByClassID(int classId, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBooKCategoryName?bookCategoryCodeId={bookCategoryCodeId}")]
        LibraryBookDTO GetBooKCategoryName(long bookCategoryCodeId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBookQuantityDetails?CallAccNo={CallAccNo},bookMapID={bookMapID}")]
        LibraryTransactionDTO GetBookQuantityDetails(string CallAccNo, int? bookMapID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetIssuedBookDetails?CallAccNo={CallAccNo},bookMapID={bookMapID}")]
        LibraryTransactionDTO GetIssuedBookDetails(string CallAccN, int? bookMapID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCircularList?parentId={parentId}")]
        List<CircularListDTO> GetCircularList(long parentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGalleryView?academicYearID={academicYearID}")]
        List<GalleryDTO> GetGalleryView(long academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAgendaList?loginID={loginID}")]
        List<AgendaDTO> GetAgendaList(long loginID);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLessonPlanList?studentID={studentID}")]
        List<LessonPlanDTO> GetLessonPlanList(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentFeeCollection?studentId={studentId}")]
        List<FeeCollectionDTO> GetStudentFeeCollection(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPickUpBusSeatAvailabilty?RouteStopMapId={RouteStopMapId},academicYearID={academicYearID}")]
        StudentRouteStopMapDTO GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDropBusSeatAvailabilty?RouteStopMapId={RouteStopMapId},academicYearID={academicYearID}")]
        StudentRouteStopMapDTO GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPickUpBusSeatStaffAvailabilty?RouteStopMapId={RouteStopMapId},academicYearID={academicYearID}")]
        StaffRouteStopMapDTO GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDropBusSeatStaffAvailabilty?RouteStopMapId={RouteStopMapId},academicYearID={academicYearID}")]
        StaffRouteStopMapDTO GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssignmentStudentwise?studentId={studentId}&SubjectID={SubjectID}")]
        List<AssignmentDTO> GetAssignmentStudentwise(long studentId, int? SubjectID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetMarkListStudentwise?studentId={studentId}")]
        List<MarkListViewDTO> GetMarkListStudentwise(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClassByExam?examId={examId})")]
        ExamDTO GetClassByExam(int examId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeePeriodMonthlySplit?examId={feePeriodIds})")]
        List<FeePeriodsDTO> GetFeePeriodMonthlySplit(List<int> feePeriodIds);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetExamSubjectsMarks?studentId={studentId},examId={examId},ClassId={ClassId})")]
        List<MarkRegisterDetailsSplitDTO> GetExamSubjectsMarks(long studentId, long examId, int ClassId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGradeByExamSubjects?examId={examId},classId={classId},subjectID={subjectID},typeId={typeId})")]
        List<MarkGradeMapDTO> GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCollectFeeAccountData?fromDate={fromDate},toDate={toDate}")]
        List<CollectFeeAccountDetailDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentDetails?studentId={studentId}")]
        List<StudentDTO> GetStudentDetails(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentDetailsByParentLoginID?id={id}")]
        TransportApplicationDTO GetTransportStudentDetailsByParentLoginID(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGuardianDetails?studentId={studentId}")]
        GuardianDTO GetGuardianDetails(long studentId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FeeAccountPosting?fromDate={fromDate},toDate={toDate}, cashierID={cashierID}")]
        string FeeAccountPosting(DateTime fromDate, DateTime toDate, long cashierID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLeaveApplication?applicationId={applicationId}")]
        StudentLeaveApplicationDTO GetLeaveApplication(long leaveapplicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteLeaveApplication?applicationId={applicationId}")]
        OperationResult DeleteLeaveApplication(long leaveapplicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentLeaveApplication?applicationId={applicationId}")]
        List<StudentLeaveApplicationDTO> GetStudentLeaveApplication(long leaveapplicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClassTimeTable?studentId={studentId}")]
        List<TimeTableAllocationDTO> GetClassTimeTable(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetExamLists?studentId={studentId}")]
        List<ExamDTO> GetExamLists(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransferApplication?studentId={studentId}")]
        StudentTransferRequestDTO GetTransferApplication(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteTransferApplication?studentId={studentId}")]
        OperationResult DeleteTransferApplication(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentTransferApplication?studentId={studentId}")]
        List<StudentTransferRequestDTO> GetStudentTransferApplication(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetApplicationByLoginID?loginID={loginID}")]
        StudentApplicationDTO GetApplicationByLoginID(long loginID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNotificationAlerts?loginID={loginID}")]
        List<NotificationAlertsDTO> GetNotificationAlerts(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllNotificationAlerts?loginID={loginID}")]
        List<NotificationAlertsDTO> GetAllNotificationAlerts(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSendMailFromParent?loginID={loginID}")]
        List<MailBoxDTO> GetSendMailFromParent(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNotificationAlertsCount?loginID={loginID}")]
        int GetNotificationAlertsCount(long loginID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MarkNotificationAsRead?loginID={loginID},notificationAlertIID={notificationAlertIID}")]
        string MarkNotificationAsRead(long loginID, long notificationAlertIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTeacherDetails?studentId={studentId}")]
        List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentId);



        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSiblingDueDetailsFromStudentID?StudentID={StudentID}")]
        List<FeeCollectionDTO> GetSiblingDueDetailsFromStudentID(long StudentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFineCollected?studentId={studentId}")]
        List<StudentFeeDueDTO> GetFineCollected(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillFineDue?classId={classId}&studentId={studentId}")]
        List<StudentFeeDueDTO> FillFineDue(int classId, long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentSkills?skillId={skillId}")]
        List<StudentSkillRegisterSplitDTO> GetStudentSkills(long skillId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGradeByExamSkill?examId={examId},skillId={skillId},subjectId={subjectId},classId={classId},markGradeID={markGradeID})")]
        List<MarkGradeMapDTO> GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentSkillRegister?studentId={studentId},ClassID={ClassID}")]
        ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductDetails?productId={productId},ClassID={ClassID}")]
        ProductBundleDTO GetProductDetails(long productId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCollectTimeSheetsData?employeeID={employeeID},fromDate={fromDate},toDate={toDate}")]
        EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductBundleData?productskuId={productskuId},")]
        ProductBundleDTO GetProductBundleData(long productskuId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFineAmount?fineMasterID={fineMasterID},")]
        FineMasterStudentMapDTO GetFineAmount(int fineMasterID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillFeePaymentDetails?loginId={loginId}")]
        List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillFeeDueForSettlement?studentId={studentId}&academicId={academicId}")]
        List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId, int academicId);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillFeeDueDataForSettlement?studentId={studentId}&academicId={academicId}")]
        List<FeeCollectionDTO> FillFeeDueDataForSettlement(long studentId, int academicId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetIssuedCreditNotesForCollectedFee?studentId={studentId}")]
        List<FeeCollectionPreviousFeesDTO> GetIssuedCreditNotesForCollectedFee(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployeeFromEmployeeID?examId={employeeID}")]
        EmployeesDTO GetEmployeeFromEmployeeID(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGroupCodeByParentGroup?ParentGroupID={parentGroupID}")]
        AccountsGroupDTO GetGroupCodeByParentGroup(long parentGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountCodeByGroup?GroupID={groupID}")]
        AccountsDTO GetAccountCodeByGroup(long groupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentDetailFromStudentID?studentId={StudentID}")]
        StudentDTO GetStudentDetailFromStudentID(long StudentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCurrencyDetails")]
        List<CurrencyDTO> GetCurrencyDetails();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AutoReceiptAccountTransactionSync?accountTransactionHeadIID={accountTransactionHeadIID}&referenceID={referenceID}&loginId={loginId}")]
        List<FeeCollectionDTO> AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DueFees?feemasterID={feemasterID}&invoiceDate={invoiceDate}&amount={amount}&studentID={studentID}&creditNoteID={creditNoteID}")]

        string DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AutoCreditNoteAccountTransactionSync?accountTransactionHeadIID={accountTransactionHeadIID}&studentID={studentID}&loginId={loginId}")]
        SchoolCreditNoteDTO AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetParentDetailFromParentID?parentId={ParentID}")]
        GuardianDTO GetParentDetailFromParentID(long ParentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeStructure?academicYearID={academicYearID}")]
        List<KeyValueDTO> GetFeeStructure(int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubGroup?mainGroupID={mainGroupID}")]
        List<KeyValueDTO> GetSubGroup(int mainGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountCodeByLedger?ledgerGroupID={ledgerGroupID}")]
        List<KeyValueDTO> GetAccountCodeByLedger(int ledgerGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountGroup?subGroupID={subGroupID}")]
        List<KeyValueDTO> GetAccountGroup(int subGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountByGroupID?groupID={groupID}")]
        List<KeyValueDTO> GetAccountByGroupID(int groupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCostCenterByAccount?accountID={accountID}")]
        List<KeyValueDTO> GetCostCenterByAccount(long accountID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountByPayementModeID?paymentModeID={paymentModeID}")]
        List<KeyValueDTO> GetAccountByPayementModeID(long paymentModeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountGroupByAccountID?accountID={accountID}")]
        List<KeyValueDTO> GetAccountGroupByAccountID(long accountID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeePeriod?academicYearID={academicYearID},studentID={studentID}")]
        List<KeyValueDTO> GetFeePeriod(int academicYearID, long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransportFeePeriod?academicYearID={academicYearID}")]
        List<KeyValueDTO> GetTransportFeePeriod(int academicYearID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicCalenderByAcademicYear?academicYearID={academicYearID}&academicCalendarStatusID={academicCalendarStatusID}&academicCalendarID={academicCalendarID}")]

        List<AcademicCalenderEventDateDTO> GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID);



        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicCalenderByMonthYear?month={month}&year={year}")]
        List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteAcademicCalendarEvent?academicYearCalendarEventIID={academicYearCalendarEventIID}&academicYearCalendarID={academicYearCalendarID}")]
        void DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentsCount")]
        int GetStudentsCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCastByRelegion?relegionID={relegionID}")]
        List<KeyValueDTO> GetCastByRelegion(int relegionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetToNotificationUsersByUser?userID={userID},branchID={branchID},user={user}")]
        List<KeyValueDTO> GetToNotificationUsersByUser(int userID, int branchID, string user);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStreamByStreamGroup?streamGroupID={streamGroupID}")]
        List<KeyValueDTO> GetStreamByStreamGroup(byte? streamGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStreamCompulsorySubjects?streamID={streamID}")]
        List<KeyValueDTO> GetStreamCompulsorySubjects(byte? streamGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStreamOptionalSubjects?streamID={streamID}")]
        List<KeyValueDTO> GetStreamOptionalSubjects(byte? streamGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFullStreamListDatas")]
        List<StreamDTO> GetFullStreamListDatas();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubSkillByGroup?skillGroupID={skillGroupID}")]
        List<KeyValueDTO> GetSubSkillByGroup(int skillGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearBySchool?schoolID={schoolID}")]
        List<KeyValueDTO> GetAcademicYearBySchool(int schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClasseByAcademicyear?academicyearID={academicyearID}")]
        List<KeyValueDTO> GetClasseByAcademicyear(int academicyearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClassesBySchool?schoolID={schoolID}")]
        List<KeyValueDTO> GetClassesBySchool(byte schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSectionsBySchool?schoolID={schoolID}")]
        List<KeyValueDTO> GetSectionsBySchool(byte schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProgressReportName?schoolID={schoolID}&classID={classID}")]
        string GetProgressReportName(long schoolID, int? classID);



        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRouteStopsByRoute?routeID={routeID}")]
        List<KeyValueDTO> GetRouteStopsByRoute(int routeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubjectByClass?classID={classID}")]
        List<KeyValueDTO> GetSubjectByClass(int classID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubjectbyQuestionGroup?questionGroupID={questionGroupID}")]
        List<KeyValueDTO> GetSubjectbyQuestionGroup(long questionGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetQuestionDetailsByQuestionID?questionID={questionID}")]
        OnlineExamQuestionDTO GetQuestionDetailsByQuestionID(long questionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAboutandContactDetails?contentID={contentID}")]
        StaticContentDataDTO GetAboutandContactDetails(long contentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLibraryStudentFromStudentID?studentId={studentID}")]
        LibraryTransactionDTO GetLibraryStudentFromStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLibraryStaffFromEmployeeID?employeeId={employeeID}")]
        LibraryTransactionDTO GetLibraryStaffFromEmployeeID(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentTransportApplication?TransportApplctnStudentMapIID={TransportApplctnStudentMapIID}")]
        TransportApplicationDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillClassStudents?classID={classID},sectionID={sectionID}")]
        List<SubjectMarkEntryDetailDTO> FillClassStudents(long classID, long sectionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubjectsMarkData?studentId={studentId},examID={examID},classID={classID},sectionID={sectionID},subjectID={subjectID}")]
        List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examID, int classID, int sectionID, int subjectID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetExamMarkDetails?subjectID={subjectID},examID={examID}")]
        ClassSubjectSkillGroupMapDTO GetExamMarkDetails(long subjectID, long examID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTeacherEmailByParentLoginID?loginID={loginID}")]
        List<KeyValueDTO> GetTeacherEmailByParentLoginID(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCandidateDetails?username={username},password={password}")]
        CandidateDTO GetCandidateDetails(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ShiftStudentSection")]
        string ShiftStudentSection(ClassSectionShiftingDTO classSectionShiftingDTO);

        [OperationContract]
        ///[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentsForShifting?username={classID},password={sectionID}")]
        ClassSectionShiftingDTO GetStudentsForShifting(int classID, int sectionID);
        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentsForShifting?username={classID},password={sectionID}")]
        List<KeyValueDTO> GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID);
        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentsForShifting?username={classID},password={sectionID}")]
        List<KeyValueDTO> GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID);
        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentsForShifting?classID={classID},password={sectionID}")]
        List<KeyValueDTO> GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID);
        [OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClassWiseExamGroup?classID={classID},password={sectionID}")]
        List<KeyValueDTO> GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID);

        [OperationContract]
        List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);

        [OperationContract]
        List<MarkRegisterDetailsDTO> GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);
        [OperationContract]
        string SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO);
        List<MarkRegisterDetailsDTO> GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);
        [OperationContract]
        string SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO);

        [OperationContract]
        string UpdateMarkEntryStatus(MarkRegisterDTO dto);


        [OperationContract]
        List<StudentMarkEntryDTO> GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);
        [OperationContract]
        string SaveMarkEntry(MarkRegisterDTO markRegisterDTO);
        [OperationContract]
        List<KeyValueDTO> GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO);
        [OperationContract]
        List<KeyValueDTO> GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO);
        [OperationContract]
        List<KeyValueDTO> GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetExamsByClassAndGroup?classID={classID},sectionID={sectionID},examGroupID={examGroupID},academicYearID={academicYearID}")]
        List<KeyValueDTO> GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubjectByType?subjectTypeID={subjectTypeID}")]
        List<KeyValueDTO> GetSubjectByType(byte subjectTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSkillByExamAndClass?classID={classID},sectionID={sectionID},examID={examID},academicYearID={academicYearID},termID={termID}")]
        List<KeyValueDTO> GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubjectsAndMarksToPublish")]
        List<MarkRegisterDetailsDTO> GetSubjectsAndMarksToPublish(MarkEntrySearchArgsDTO markEntrySearchArgsDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearNameByID?academicYearID={academicYearID}")]
        string GetAcademicYearNameByID(int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSchoolNameByID?schoolID={schoolID}")]
        string GetSchoolNameByID(int schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransportApplication?loginID={loginID}")]
        List<TransportApplicationStudentMapDTO> GetTransportApplication(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGeoSchoolSetting?schoolID={schoolID}")]
        List<SchoolGeoLocationDTO> GetGeoSchoolSetting(long schoolID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveGeoSchoolSetting")]
        void SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ClearGeoSchoolSetting?schoolID={schoolID}")]
        void ClearGeoSchoolSetting(int schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetMonthAndYearByAcademicYearID?academicYearID={academicYearID}")]
        List<AcademicClassMapWorkingDayDTO> GetMonthAndYearByAcademicYearID(int? academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicMonthAndYearByCalendarID?calendarID={calendarID}")]

        List<AcademicCalenderEventDateDTO> GetAcademicMonthAndYearByCalendarID(long? calendarID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillCollectedFeesDetails?studentId={studentId}&academicId={academicId}")]
        List<FeeCollectionDTO> FillCollectedFeesDetails(long studentId, int academicId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckAndInsertCalendarEntries?calendarID={calendarID})")]
        string CheckAndInsertCalendarEntries(long calendarID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCalendarByTypeID?calendarTypeID={calendarTypeID}")]
        List<KeyValueDTO> GetCalendarByTypeID(byte calendarTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBookDetailsByCallNo?CallAccNo={CallAccNo}")]
        List<KeyValueDTO> GetBookDetailsByCallNo(string CallAccNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCalendarEventsByCalendarID?calendarID={calendarID}")]
        List<AcademicCalenderEventDateDTO> GetCalendarEventsByCalendarID(long calendarID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployeesByCalendarID?calendarID={calendarID}")]
        List<EmployeesDTO> GetEmployeesByCalendarID(long calendarID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaffPresentStatuses")]
        List<PresentStatusDTO> GetStaffPresentStatuses();

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBookDetailsChange?BookID={BookID}")]
        //LibraryBookDTO GetBookDetailsChange(long BookID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCreditNoteNumber?headID={headID}&studentID={studentID}")]
        List<SchoolCreditNoteDTO> GetCreditNoteNumber(long? headID, long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSchoolsByParentLoginID?loginID={loginID}")]
        List<KeyValueDTO> GetSchoolsByParentLoginID(long? loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearDataByAcademicYearID?academicYearID={academicYearID}")]
        AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetQuestionsByCandidateID?candidateID={candidateID}")]
        List<OnlineExamQuestionDTO> GetQuestionsByCandidateID(long candidateID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAgeCriteriaDetails?classID={classID} & academicID={academicID} & schoolID={schoolID} & dob={dob}")]
        StudentApplicationDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUnitByUnitGroup?groupID={groupID},")]
        List<KeyValueDTO> GetUnitByUnitGroup(int groupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUnitDataByUnitGroup?groupID={groupID},")]
        List<UnitDTO> GetUnitDataByUnitGroup(int groupID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOnlineExamsResultByCandidateID?candidateID={candidateID}")]
        List<OnlineExamResultDTO> GetOnlineExamsResultByCandidateID(long candidateID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SubmitAmountAsLog")]
        string SubmitAmountAsLog(decimal? totalAmount);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLastLogData")]
        PaymentLogDTO GetLastLogData();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPaymentMasterVisaData")]
        PaymentMasterVisaDTO GetPaymentMasterVisaData();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FeeCollectionEntry")]
        string FeeCollectionEntry(List<FeeCollectionDTO> feeCollectionList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateStudentsFeePaymentStatus")]
        List<FeeCollectionDTO> UpdateStudentsFeePaymentStatus(string transactionNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentFeeCollectionsHistory")]
        List<FeeCollectionDTO> GetStudentFeeCollectionsHistory(StudentDTO studentData, byte? schoolID, int? academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCurrentAcademicYearsData")]
        List<AcademicYearDTO> GetCurrentAcademicYearsData();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllAcademicYearBySchoolID?schoolID={schoolID}")]
        List<KeyValueDTO> GetAllAcademicYearBySchoolID(int schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeCollectionHistories")]
        List<FeeCollectionDTO> GetFeeCollectionHistories(List<StudentDTO> studentDatas, byte? schoolID, int? academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAndInsertLogDataByTransactionID")]
        PaymentLogDTO GetAndInsertLogDataByTransactionID(string transID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckFeeCollectionStatusByTransactionNumber?transactionNumber={transactionNumber}")]
        string CheckFeeCollectionStatusByTransactionNumber(string transactionNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeCollectionDetailsByTransactionNumber?transactionNumber={transactionNumber}&mailID={mailID}")]
        List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber, string mailID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteTransportApplication?applicationId={applicationId}")]
        OperationResult DeleteTransportApplication(long transportapplicationId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRoutesByRouteGroupID?routeGroupID={routeGroupID}")]
        List<KeyValueDTO> GetRoutesByRouteGroupID(int? routeGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearDataByGroupID?routeGroupID={routeGroupID}")]
        AcademicYearDTO GetAcademicYearDataByGroupID(int? routeGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPickupStopMapsByRouteGroupID?routeGroupID={routeGroupID}")]
        List<KeyValueDTO> GetPickupStopMapsByRouteGroupID(int? routeGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDropStopMapsByRouteGroupID?routeGroupID={routeGroupID}")]
        List<KeyValueDTO> GetDropStopMapsByRouteGroupID(int? routeGroupID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentPickupRequestsByLoginID?loginID={loginID}")]
        List<StudentPickupRequestDTO> GetStudentPickupRequestsByLoginID(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSubLedgerByAccount?accountID={accountID}")]
        List<KeyValueDTO> GetSubLedgerByAccount(long accountID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveProgressReportData?toDtoList={toDtoList}")]
        List<ProgressReportNewDTO> SaveProgressReportData(List<ProgressReportNewDTO> toDtoList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdatePublishStatus?toDtoList={toDtoList}")]
        string UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProgressReportData?classID={classID},sectionID={sectionID},academicYearID={academicYearID}")]
        List<ProgressReportNewDTO> GetProgressReportData(int classID, int? sectionID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProgressReportList?studentID={studentID}&classID={classID}&sectionID={sectionID}&academicYearID={academicYearID}")]
        List<ProgressReportNewDTO> GetProgressReportList(long studentID, int classID, int sectionID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CancelStudentPickupRequestByID?pickupRequestID={pickupRequestID}")]
        string CancelStudentPickupRequestByID(long pickupRequestID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CancelTransportApplication?mapIID={mapIID}")]
        string CancelTransportApplication(long mapIID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCountByDocumentTypeID?docTypeID={docTypeID}")]
        TransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentDetailsByStaff?staffID={staffID}")]
        List<KeyValueDTO> GetStudentDetailsByStaff(long staffID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentDetailsByParent?parentID={parentID}")]
        List<KeyValueDTO> GetStudentDetailsByParent(long parentID);
        
       
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaffDetailsByStudentID?studentID={studentID}")]
        List<KeyValueDTO> GetStaffDetailsByStudentID(int studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetParentDetailsByStudentID?studentID={studentID}")]
        List<KeyValueDTO> GetParentDetailsByStudentID(int studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeAmount?studentID={studentID},academicYearID={academicYearID},feeMasterID={feeMasterID},feePeriodID={feePeriodID})")]
        decimal? GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SendAttendanceNotificationsToParents?classId={classId},sectionId={sectionId}")]
        string SendAttendanceNotificationsToParents(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCountsForDashBoardMenuCards?chartID={chartID}")]
        DashBoardChartDTO GetCountsForDashBoardMenuCards(int chartID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillEditDatasAndSubjects?IID={IID},classID={classID},sectionID={sectionID})")]
        List<ClassClassTeacherMapDTO> FillEditDatasAndSubjects(int IID,int classID, int sectionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeDueForConcession?studentID={studentID}&academicYearID={academicYearID}")]
        List<StudentFeeConcessionDetailDTO> GetFeeDueForConcession(long studentID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FillClassSectionWiseCoordinators?classID={classID},sectionID={sectionID})")]
        List<ClassCoordinatorsDTO> FillClassSectionWiseCoordinators(int classID, int sectionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeDueDatasForReportMail?asOnDate={asOnDate},classID={classID},sectionID={sectionID})")]
        List<MailFeeDueStatementReportDTO> GetFeeDueDatasForReportMail(DateTime asOnDate,int classID, int? sectionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTeacherRelatedDataForDirectorsDashBoard")]
        DirectorsDashBoardDTO GetTeacherRelatedDataForDirectorsDashBoard();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetVehiclesByRoute?routeID={routeID}")]
        List<KeyValueDTO> GetVehiclesByRoute(int routeID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SendFeeDueMailReportToParent")]
        MailFeeDueStatementReportDTO SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UserProfileForDashBoard")]
        EmployeeDTO UserProfileForDashBoard();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNotificationsForDashBoard")]
        Eduegate.Services.Contracts.Notifications.PushNotificationDTO GetNotificationsForDashBoard();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWeeklyTimeTableForDashBoard?weekDayID={weekDayID}")]
        TimeTableDTO GetWeeklyTimeTableForDashBoard(int weekDayID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeDueForDueCancellation?studentID={studentID}&academicYearID={academicYearID}")]
        List<FeeDueCancellationDetailDTO> GetFeeDueForDueCancellation(long studentID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGridLookUpsForSchoolCreditNote?studentId={studentId}")]
        StudentFeeDueDTO GetGridLookUpsForSchoolCreditNote(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployeeDetailsByEmployeeID?employeeID={employeeID}")]
        List<EmployeePromotionDTO> GetEmployeeDetailsByEmployeeID(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateTCStatus?StudentTransferRequestIID={StudentTransferRequestIID}&TCContentID={TCContentID}")]
        string UpdateTCStatus(long? StudentTransferRequestIID, long TCContentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateTCStatusToComplete?StudentTransferRequestIID={StudentTransferRequestIID}")]
        string UpdateTCStatusToComplete(long? StudentTransferRequestIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Getstudentsubjectlist?studentId={studentId}")]
        List<KeyValueDTO> Getstudentsubjectlist(long studentId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "InsertProgressReportEntries")]
        string InsertProgressReportEntries(List<ProgressReportDTO> progressReportListDTOs,List<SettingDTO> settings);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentProgressReports")]
        List<ProgressReportDTO> GetStudentProgressReports(ProgressReportDTO progressReport);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentPublishedProgressReports?studentID={studentID}&examID={examID}")]
        List<ProgressReportDTO> GetStudentPublishedProgressReports(long studentID, long? examID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBankDetailsByBankID?bankID={bankID}")]
        BankAccountDTO GetBankDetailsByBankID(long bankID);

    }
}