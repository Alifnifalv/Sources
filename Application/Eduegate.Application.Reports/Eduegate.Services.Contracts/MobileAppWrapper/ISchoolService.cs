using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Attendences;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    [ServiceContract]
    public interface ISchoolService
    {
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDriverGeoLocation")]
        string GetDriverGeoLocation();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLatestCirculars")]
        List<CircularListDTO> GetLatestCirculars();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLatestCircularCount")]
        decimal GetLatestCircularCount();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetExamCount")]
        decimal GetExamCount();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetExamLists?studentID={studentID}")]
        List<ExamDTO> GetExamLists(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentDetailsByStudentID?studentID={studentID}")]
        StudentDTO GetStudentDetailsByStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetParentStudents")]
        List<StudentDTO> GetParentStudents();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyStudents")]
        List<StudentDTO> GetMyStudents();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTeacherClass")]
        List<ClassTeacherMapDTO> GetTeacherClass();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentsByTeacherClassAndSection?classID={classID}&sectionID={sectionID}")]
        List<StudentDTO> GetStudentsByTeacherClassAndSection(long classID, long sectionID = 0);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaffProfile?employeeID={employeeID}")]
        EmployeesDTO GetStaffProfile(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyStudentsSiblingsCount")]
        int GetMyStudentsSiblingsCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyAssignmentsCount")]
        int GetMyAssignmentsCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyClassCount")]
        int GetMyClassCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyLessonPlanCount")]
        int GetMyLessonPlanCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyLessonPlans")]
        List<LessonPlanDTO> GetMyLessonPlans();


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyNotificationCount")]
        int GetMyNotificationCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyNotification")]
        List<NotificationAlertsDTO> GetMyNotification();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeDueAmount")]
        decimal GetFeeDueAmount();


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeTotal?studentID={studentID}")]
        decimal GetFeeTotal(long studentID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMarkList?studentID={studentID}")]
        List<MarkListViewDTO> GetMarkList(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMarkListForTeacher")]
        List<MarkListViewDTO> GetMarkListForTeacher();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAssignments?studentID={studentID}")]
        List<AssignmentDTO> GetAssignments(long studentID );

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAssignmentsForTeacher")]
        List<AssignmentDTO> GetAssignmentsForTeacher();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLeaveApplication?studentID={studentID}")]
        List<StudentLeaveApplicationDTO> GetLeaveApplication(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentFeeCollection?studentID={studentID}")]
        List<FeeCollectionDTO> GetStudentFeeCollection(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTeacherDetails?studentID={studentID}")]
        List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFineCollected?studentID={studentID}")]
        List<StudentFeeDueDTO> GetFineCollected(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "FillFineDue?studentId={studentId}")]
        List<StudentFeeDueDTO> FillFineDue(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDynamicLookUpDataForMobileApp?lookType={lookType}&searchText={searchText}")]
        List<KeyValueDTO> GetDynamicLookUpDataForMobileApp(DynamicLookUpType lookType, string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClassStudents?classId={classId}&sectionId={sectionId}")]
        List<KeyValueDTO> GetClassStudents(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAcademicCalenderByMonthYear?month={month}&year={year}")]
        List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentAttendenceByDayClassSection?month={month}&year={year}&day={day}&classId={classId}&sectionId={sectionId}")]
        List<StudentAttendenceDTO> GetStudentAttendenceByDayClassSection(int month, int year, int classId, int sectionId, int day);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentAttendenceByYearMonthClassSection?month={month}&year={year}&classId={classId}&sectionId={sectionId}")]
        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthClassSection(int month, int year, int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentAttendenceByYearMonthStudentId?month={month}&year={year}&studentId={studentId}")]
        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClasswiseStudentData?classId={classId}&sectionId={sectionId}")]
        List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveStudentAttendence")]
        OperationResultDTO SaveStudentAttendence(StudentAttendenceInfoDTO attendenceInfo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaffLeaveApplication")]
        List<LeaveRequestDTO> GetStaffLeaveApplication();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "FillFeeDue?studentID={studentID}")]
        List<StudentFeeDueDTO> FillFeeDue(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "FillFeePaymentDetails?loginId={loginId}")]
        List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "FillPendingFees?classId={classId}&studentId={studentId}")]
        List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeCollected?studentId={studentId}")]
        List<StudentFeeDueDTO> GetFeeCollected(long studentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentApplication")]
        List<StudentApplicationDTO> GetStudentApplication();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStudentApplicationCount")]
        int GetStudentApplicationCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestAgenda?studentId={studentId}")]
        List<AgendaDTO> GetLatestAgenda(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyAgendaCount")]
        int GetMyAgendaCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentAttendenceCountByStudentID?month={month}&year={year}&studentID={studentID}")]
        StudentAttendenceDTO GetStudentAttendenceCountByStudentID(int month, int year, long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeDueAmountByStudentID?studentID={studentID}")]
        decimal GetFeeDueAmountByStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransportApplications")]
        List<TransportApplicationDTO> GetTransportApplications();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTimeTableByStudentID?studentID={studentID}")]
        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSubjectsByStudentID?studentID={studentID}")]
        List<ClassSubjectMapDTO> GetSubjectsByStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTimeTableByStaffLoginID?loginID={loginID}")]
        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStaffLoginID(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetVehicleDetailsByEmployeeLoginID")]
        List<School.Transports.VehicleDTO> GetVehicleDetailsByEmployeeLoginID();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDriverScheduleListByParameters?routeID={routeID}&vehicleID={vehicleID}&scheduledDate={scheduledDate}")]
        List<DriverScheduleLogDTO> GetDriverScheduleListByParameters(int routeID, int vehicleID, DateTime scheduledDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRoutesByVehicleID?vehicleID={vehicleID}")]
        List<RoutesDTO> GetRoutesByVehicleID(long vehicleID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRouteStopsByRouteID?routeID={routeID}")]
        List<RouteStopFeeDTO> GetRouteStopsByRouteID(long routeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentsDetailsByRouteStopID?routeStopMapID={routeStopMapID}")]
        List<StudentRouteStopMapDTO> GetStudentsDetailsByRouteStopID(long routeStopMapID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateUserGeoLocation?geoLocation={geoLocation}")]
        void UpdateUserGeoLocation(string geoLocation);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRouteStudentsAndStaffDetailsByEmployeeLoginID")]
        List<School.Transports.VehicleDTO> GetRouteStudentsAndStaffDetailsByEmployeeLoginID();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllRouteStudentsAndStaffDetails")]
        List<School.Transports.VehicleDTO> GetAllRouteStudentsAndStaffDetails();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetVehicleAssignDetailsByEmployeeIDandRouteID?employeeID={employeeID}&routeID={routeID}")]
        List<AssignVehicleDTO> GetVehicleAssignDetailsByEmployeeIDandRouteID(long employeeID,long routeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransportApplicationCount")]
        int GetTransportApplicationCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeAssignmentsCount")]
        int GetEmployeeAssignmentsCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPresentStatuses")]
        List<PresentStatusDTO> GetPresentStatuses();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLessonPlanListByStudentID?studentID={studentID}")]
        List<LessonPlanDTO> GetLessonPlanListByStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentLeaveCountByStudentID?studentID={studentID}")]
        StudentLeaveApplicationDTO GetStudentLeaveCountByStudentID(long studentID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubmitStudentLeaveApplication")]
        OperationResultDTO SubmitStudentLeaveApplication(StudentLeaveApplicationDTO leaveInfo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteStudentLeaveApplicationByID?leaveApplicationID={leaveApplicationID}")]
        void DeleteStudentLeaveApplicationByID(long leaveApplicationID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentLeaveApplicationByID?leaveApplicationID={leaveApplicationID}")]
        StudentLeaveApplicationDTO GetStudentLeaveApplicationByID(long leaveApplicationID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveStudentAndStaffScheduleLogs")]
        string SaveStudentAndStaffScheduleLogs(DriverScheduleLogDTO scheduleLogData);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SyncDriverScheduleLogs")]
        long SyncDriverScheduleLogs(DriverScheduleLogDTO scheduleLogData);

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetScheduleLogs?scheduleType={scheduleType}&passengerType={passengerType}&vehicleID={vehicleID}&routeStopMapID={routeStopMapID}&routeID={routeID}")]
        //List<DriverScheduleLogDTO> GetScheduleLogs(string scheduleType, string passengerType, long vehicleID, long routeStopMapID, long routeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetScheduleLogsByRoute?scheduleType={scheduleType}&passengerType={passengerType}&vehicleID={vehicleID}&routeID={routeID}")]
        List<DriverScheduleLogDTO> GetScheduleLogsByRoute(string scheduleType, string passengerType, long vehicleID, long routeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentStaffDropScheduleDatasforDropOut?passengerType={passengerType}&vehicleID={vehicleID}&routeID={routeID}")]
        DriverScheduleLogDTO GetStudentStaffDropScheduleDatasforDropOut(string passengerType, long vehicleID, long routeID);

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaffDropScheduleDatasByRoute?vehicleID={vehicleID}&routeID={routeID}")]
        //DriverScheduleLogDTO GetStaffDropScheduleDatasByRoute(long vehicleID, long routeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetScheduleLogsByDateForOfflineDB")]
        List<DriverScheduleLogDTO> GetScheduleLogsByDateForOfflineDB();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaffAttendanceByMonthYear?month={month}&year={year}")]
        List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaffAttendenceByYearMonthEmployeeID?month={month}&year={year}")]
        List<StaffAttendenceDTO> GetStaffAttendenceByYearMonthEmployeeID(int month, int year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaffAttendenceCountByEmployeeID?month={month}&year={year}&employeeID={employeeID}")]
        StaffAttendenceDTO GetStaffAttendenceCountByEmployeeID(int month, int year, long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSchoolsByParent")]
        List<KeyValueDTO> GetSchoolsByParent();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAcademicYearBySchool?schoolID={schoolID}")]
        List<KeyValueDTO> GetAcademicYearBySchool(int schoolID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeCollectionHistories?schoolID={schoolID}&academicYearID={academicYearID}")]
        List<FeeCollectionDTO> GetFeeCollectionHistories(byte schoolID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentsFeePaymentDetails")]
        List<FeePaymentDTO> GetStudentsFeePaymentDetails();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SubmitAmountAsLog?totalAmount={totalAmount}")]
        string SubmitAmountAsLog(decimal totalAmount);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerateCardSession")]
        string GenerateCardSession();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "InitiateFeeCollections")]
        string InitiateFeeCollections(List<FeeCollectionDTO> feeCollectionList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateFeePaymentStatus")]
        string UpdateFeePaymentStatus();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCardSession")]
        string GetCardSession();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCardPaymentGatewayDatas")]
        PaymentMasterVisaDTO GetCardPaymentGatewayDatas();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidatePayment")]
        string ValidatePayment();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ResendReceiptMail?transactionNumber={transactionNumber}&mailID={mailID}")]
        OperationResultDTO ResendReceiptMail(string transactionNumber, string mailID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckFeeCollectionExistingStatus?transactionNumber={transactionNumber}")]
        OperationResultDTO CheckFeeCollectionExistingStatus(string transactionNumber);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RetryPayment?transactionNumber={transactionNumber}")]
        OperationResultDTO RetryPayment(string transactionNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeeCollectionDetailsByTransactionNumber?transactionNumber={transactionNumber}")]
        List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidateCartPayment?cartID={cartID}&returnIndicator={returnIndicator}")]
        bool ValidateCartPayment(long cartID, string returnIndicator);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPaymentSession?shoppingCartID={shoppingCartID}&totalAmount={totalAmount}")]
        PaymentMasterVisaDTO GetPaymentSession(long shoppingCartID, decimal totalAmount);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPickupRequestsCount")]
        decimal GetPickupRequestsCount();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentPickupRequests")]
        List<StudentPickupRequestDTO> GetStudentPickupRequests();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetGuardianDetails")]
        GuardianDTO GetGuardianDetails();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CancelStudentPickupRequestByID?pickupRequestID={pickupRequestID}")]
        OperationResultDTO CancelStudentPickupRequestByID(long pickupRequestID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SubmitStudentPickupRequest")]
        OperationResultDTO SubmitStudentPickupRequest(StudentPickupRequestDTO studentPickupRequest);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SubmitStudentDailyPickupRequest")]
        OperationResultDTO SubmitStudentDailyPickupRequest(StudentPickupRequestDTO studentDailyPickupRequest);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRegisteredPickupRequests?loginID={loginID}&barCodeValue={barCodeValue}")]
        List<StudentPickupRequestDTO> GetRegisteredPickupRequests(long loginID,string barCodeValue);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CancelorActiveStudentPickupRegistration?studentPickerStudentMapIID={studentPickerStudentMapIID}")]
        OperationResultDTO CancelorActiveStudentPickupRegistration(long studentPickerStudentMapIID);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPickupRegisterCount")]
        decimal GetPickupRegisterCount();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GePickupRegisteredDetailsByQR?qrCode={qrCode}")]
        StudentPickupRequestDTO GePickupRegisteredDetailsByQR(string qrCode);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SubmitStudentPickLogs")]
        OperationResultDTO SubmitStudentPickLogs(StudentPickupRequestDTO submitLog);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTodayStudentPickLogs")]
        List<StudentPickupRequestDTO> GetTodayStudentPickLogs();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllergies")]
        List<KeyValueDTO> GetAllergies();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAllergies?studentID={studentID}&allergyID={allergyID}&severityID={severityID}")]
        OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentAllergies")]
        List<AllergyStudentDTO> GetStudentAllergies();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckStudentAllergy?studentID={studentID}&cartID={cartID}")]
        List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "saveDefaultStudent?studentID={studentID}")]
        OperationResultDTO saveDefaultStudent(long studentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "getDefaultStudent")]
        CustomerDTO getDefaultStudent();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubmitStaffLeaveApplication")]
        OperationResultDTO SubmitStaffLeaveApplication(LeaveRequestDTO leaveInfo);

        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLeaveTypes")]
        List<KeyValueDTO> GetLeaveTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStaticPage?contentID={contentID}")]
        StaticContentDataDTO GetStaticPage(long contentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVisitorDetails?QID={QID}&passportNumber={passportNumber}")]
        VisitorDTO GetVisitorDetails(string QID, string passportNumber);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VisitorRegistration")]
        OperationResultDTO VisitorRegistration(VisitorDTO visitorDetails);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVisitorDetailsByLoginID?loginID={loginID}")]
        VisitorDTO GetVisitorDetailsByLoginID(long loginID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateStudentPickLogStatus?picklogID={picklogID}")]
        string UpdateStudentPickLogStatus(long picklogID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVisitorDetailsByVisitorCode?visitorCode={visitorCode}")]
        VisitorDTO GetVisitorDetailsByVisitorCode(string visitorCode);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UploadContentAsString")]
        long UploadContentAsString(ContentFileDTO visitorDetails);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAndUpdateActivePickLogsForInspection")]
        List<StudentPickupRequestDTO> GetAndUpdateActivePickLogsForInspection();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTodayInspectionColour")]
        string GetTodayInspectionColour();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSeverity")]
        List<KeyValueDTO> GetSeverity();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetParentDetailsByParentCode?parentCode={parentCode}")]
        GuardianDTO GetParentDetailsByParentCode(string parentCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTodayStaffAttendanceByLoginID")]
        StaffAttendenceDTO GetTodayStaffAttendanceByLoginID();


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "MarkNotificationAsRead?notificationAlertIID={notificationAlertIID}")]
        string MarkNotificationAsRead(long notificationAlertIID);
    }
}