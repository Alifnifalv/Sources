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
using Eduegate.Services.Contracts.School.Payment;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Services.Contracts.Supports;
using System;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    public interface ISchoolService
    {
        string GetDriverGeoLocation();

        List<CircularListDTO> GetLatestCirculars();

        decimal GetLatestCircularCount();

        decimal GetExamCount();

        List<ExamDTO> GetExamLists(long studentID);

        StudentDTO GetStudentDetailsByStudentID(long studentID);

        List<StudentDTO> GetParentStudents();

        List<StudentDTO> GetMyStudents();

        List<ClassTeacherMapDTO> GetTeacherClass();

        List<StudentDTO> GetStudentsByTeacherClassAndSection(long classID, long sectionID = 0);

        EmployeesDTO GetStaffProfile(long employeeID);

        int GetMyStudentsSiblingsCount();

        int GetMyAssignmentsCount();

        int GetMyClassCount();

        int GetMyLessonPlanCount();

        List<LessonPlanDTO> GetMyLessonPlans();

        int GetMyNotificationCount();

        List<NotificationAlertsDTO> GetMyNotification();

        decimal GetFeeDueAmount();

        decimal GetFeeTotal(long studentID);

        List<MarkListViewDTO> GetMarkList(long studentID);

        List<MarkListViewDTO> GetMarkListForTeacher();

        List<AssignmentDTO> GetAssignments(long studentID);

        List<AssignmentDTO> GetAssignmentsForTeacher();

        List<StudentLeaveApplicationDTO> GetLeaveApplication(long studentID);

        List<FeeCollectionDTO> GetStudentFeeCollection(long studentId);

        List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentID);

        List<StudentFeeDueDTO> GetFineCollected(long studentID);

        List<StudentFeeDueDTO> FillFineDue(long studentID);

        List<KeyValueDTO> GetDynamicLookUpDataForMobileApp(DynamicLookUpType lookType, string searchText);

        List<KeyValueDTO> GetClassStudents(int classId, int sectionId);

        List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year);

        List<StudentAttendenceDTO> GetStudentAttendenceByDayClassSection(int month, int year, int classId, int sectionId, int day);

        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthClassSection(int month, int year, int classId, int sectionId);

        List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId);

        List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId);

        OperationResultDTO SaveStudentAttendence(StudentAttendenceInfoDTO attendenceInfo);

        List<LeaveRequestDTO> GetStaffLeaveApplication();

        List<StudentFeeDueDTO> FillFeeDue(long studentID);

        List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId);

        List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId);

        List<StudentFeeDueDTO> GetFeeCollected(long studentId);

        List<StudentApplicationDTO> GetStudentApplication();

        int GetStudentApplicationCount();

        List<AgendaDTO> GetLatestAgenda(long studentID);

        int GetMyAgendaCount();

        StudentAttendenceDTO GetStudentAttendenceCountByStudentID(int month, int year, long studentID);

        decimal GetFeeDueAmountByStudentID(long studentID);

        List<TransportApplicationDTO> GetTransportApplications();

        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStudentID(long studentID);

        List<ClassSubjectMapDTO> GetSubjectsByStudentID(long studentID);

        List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStaffLoginID(long loginID);

        List<School.Transports.VehicleDTO> GetVehicleDetailsByEmployeeLoginID();

        List<DriverScheduleLogDTO> GetDriverScheduleListByParameters(int routeID, int vehicleID, DateTime scheduledDate);

        List<RoutesDTO> GetRoutesByVehicleID(long vehicleID);

        List<RouteStopFeeDTO> GetRouteStopsByRouteID(long routeID);

        List<StudentRouteStopMapDTO> GetStudentsDetailsByRouteStopID(long routeStopMapID);

        void UpdateUserGeoLocation(string geoLocation);

        List<School.Transports.VehicleDTO> GetRouteStudentsAndStaffDetailsByEmployeeLoginID();

        List<School.Transports.VehicleDTO> GetAllRouteStudentsAndStaffDetails();

        List<AssignVehicleDTO> GetVehicleAssignDetailsByEmployeeIDandRouteID(long employeeID, long routeID);

        int GetTransportApplicationCount();

        int GetEmployeeAssignmentsCount();

        List<PresentStatusDTO> GetPresentStatuses();

        List<LessonPlanDTO> GetLessonPlanListByStudentID(long studentID);

        StudentLeaveApplicationDTO GetStudentLeaveCountByStudentID(long studentID);

        OperationResultDTO SubmitStudentLeaveApplication(StudentLeaveApplicationDTO leaveInfo);

        void DeleteStudentLeaveApplicationByID(long leaveApplicationID);

        StudentLeaveApplicationDTO GetStudentLeaveApplicationByID(long leaveApplicationID);

        string SaveStudentAndStaffScheduleLogs(DriverScheduleLogDTO scheduleLogData);


        long SyncDriverScheduleLogs(DriverScheduleLogDTO scheduleLogData);

        //List<DriverScheduleLogDTO> GetScheduleLogs(string scheduleType, string passengerType, long vehicleID, long routeStopMapID, long routeID);

        List<DriverScheduleLogDTO> GetScheduleLogsByRoute(string scheduleType, string passengerType, long vehicleID, long routeID);

        DriverScheduleLogDTO GetStudentStaffDropScheduleDatasforDropOut(string passengerType, long vehicleID, long routeID);

        //DriverScheduleLogDTO GetStaffDropScheduleDatasByRoute(long vehicleID, long routeID);

        List<DriverScheduleLogDTO> GetScheduleLogsByDateForOfflineDB();

        List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year);

        List<StaffAttendenceDTO> GetStaffAttendenceByYearMonthEmployeeID(int month, int year);

        StaffAttendenceDTO GetStaffAttendenceCountByEmployeeID(int month, int year, long employeeID);

        List<KeyValueDTO> GetSchoolsByParent();

        List<KeyValueDTO> GetAcademicYearBySchool(int schoolID);

        List<FeePaymentHistoryDTO> GetFeeCollectionHistories(byte schoolID, int academicYearID);

        List<FeePaymentDTO> GetStudentsFeePaymentDetails();

        string SubmitAmountAsLog(decimal totalAmount);

        string GenerateCardSession(byte? paymentModeID = null);

        OperationResultDTO InitiateFeeCollections(List<FeeCollectionDTO> feeCollectionList);

        string UpdateFeePaymentStatus(string transactionNumber, byte? paymentModeID = null);

        string GetCardSession();

        PaymentMasterVisaDTO GetCardPaymentGatewayDatas();

        string ValidatePayment();

        OperationResultDTO ResendReceiptMail(string transactionNumber, string mailID, string feeReceiptNo);

        OperationResultDTO CheckFeeCollectionExistingStatus(string transactionNumber);

        OperationResultDTO RetryPayment(string transactionNumber, byte? paymentModeID = null);

        List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber);

        bool ValidateCartPayment(long cartID, string returnIndicator, byte? paymentMethodID = null, long? totalCartAmount = null);

        PaymentMasterVisaDTO GetPaymentSession(long shoppingCartID, decimal totalAmount);

        decimal GetPickupRequestsCount();

        List<StudentPickupRequestDTO> GetStudentPickupRequests();

        GuardianDTO GetGuardianDetails();

        OperationResultDTO CancelStudentPickupRequestByID(long pickupRequestID);

        OperationResultDTO SubmitStudentPickupRequest(StudentPickupRequestDTO studentPickupRequest);

        OperationResultDTO SubmitStudentDailyPickupRequest(StudentPickupRequestDTO studentDailyPickupRequest);

        List<StudentPickupRequestDTO> GetRegisteredPickupRequests(long loginID,string barCodeValue);

        OperationResultDTO CancelorActiveStudentPickupRegistration(long studentPickerStudentMapIID);

        decimal GetPickupRegisterCount();

        StudentPickupRequestDTO GePickupRegisteredDetailsByQR(string qrCode);

        OperationResultDTO SubmitStudentPickLogs(StudentPickupRequestDTO submitLog);

        List<StudentPickupRequestDTO> GetTodayStudentPickLogs();

        List<KeyValueDTO> GetAllergies();

        OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID);

        List<AllergyStudentDTO> GetStudentAllergies();

        List<KeyValueDTO> GetSeverity();


        List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID);

        OperationResultDTO SaveDefaultStudent(long studentID);

        CustomerDTO GetDefaultStudent();

        OperationResultDTO SubmitStaffLeaveApplication(LeaveRequestDTO leaveInfo);
    
        List<KeyValueDTO> GetLeaveTypes();

        StaticContentDataDTO GetStaticPage(long contentID);

        VisitorDTO GetVisitorDetails(string QID, string passportNumber);

        OperationResultDTO VisitorRegistration(VisitorDTO visitorDetails);

        VisitorDTO GetVisitorDetailsByLoginID(long loginID);

        string UpdateStudentPickLogStatus(long picklogID);

        VisitorDTO GetVisitorDetailsByVisitorCode(string visitorCode);

        long UploadContentAsString(ContentFileDTO visitorDetails);

        List<KeyValueDTO> GetOrderTypes();

        List<StudentDTO> GetMyStudentsBySchool();

        string MarkNotificationAsRead(long notificationAlertID);

        OperationResultDTO CheckTransactionPaymentStatus(string transactionNumber, byte? paymentModeID = null);

        string GetDriverGeoLocationByStudent(long studentID);

        EmployeesDTO GetDriverDetailsByStudent(long studentID);

        List<RouteStopFeeDTO> GetStopsPositionsByRoute(long studentID);
       
        bool GetStudentInOutVehicleStatus(long studentID);

        List<TicketDTO> GetAllTicketsByLoginID();

        OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO);

        string SendAttendanceNotificationsToParents(int classId, int sectionId);

        OperationResultDTO SubmitDriverVehicleTracking(DriverVehicleTrackingDTO trackingInfo);

        void MarkDriverAttendanceOnPickupStart();

        List<SignUpGroupDTO> GetActiveSignupGroups();

        SignUpGroupDTO FillSignUpDetailsByGroupID(int groupID);

        OperationResultDTO SaveSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap);

        OperationResultDTO CancelSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap);

    }
}