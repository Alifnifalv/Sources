using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Domain.SignUp.SignUps;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Services.Contracts.Schedulers;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Attendences;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.CounselorHub;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Payment;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Services.MobileAppWrapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Eduegate.Domain.Mappers.School.Attendences.StudentAttendenceMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ApiControllerBase
    {
        private readonly ILogger<SchoolController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;

        public SchoolController(ILogger<SchoolController> logger, IHttpContextAccessor context,
            dbEduegateSchoolContext dbContext, IBackgroundJobClient backgroundJobs,
            IServiceProvider serviceProvider, RealtimeClient realtimeClient,
            IHttpContextAccessor accessor) : base(context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _backgroundJobs = backgroundJobs;
            _realtimeClient = realtimeClient;
            _accessor = accessor;
        }

        [HttpGet]
        [Route("GetDriverGeoLocation")]
        public string GetDriverGeoLocation()
        {
            return new SchoolService(CallContext).GetDriverGeoLocation();
        }

        [HttpGet]
        [Route("GetLatestCirculars")]
        public List<CircularListDTO> GetLatestCirculars()
        {
            return new SchoolService(CallContext).GetLatestCirculars();
        }

        [HttpGet]
        [Route("GetLatestCircularCount")]
        public decimal GetLatestCircularCount()
        {
            return new SchoolService(CallContext).GetLatestCircularCount();
        }

        [HttpGet]
        [Route("GetExamCount")]
        public decimal GetExamCount()
        {
            return new SchoolService(CallContext).GetExamCount();
        }

        [HttpGet]
        [Route("GetExamLists")]
        public List<ExamDTO> GetExamLists(long studentID)
        {
            return new SchoolService(CallContext).GetExamLists(studentID);
        }

        [HttpGet]
        [Route("GetAssignments")]
        public List<AssignmentDTO> GetAssignments(long studentID, int? SubjectID)
        {
            return new SchoolService(CallContext).GetAssignments(studentID, SubjectID);
        }

        [HttpGet]
        [Route("GetLatestAgenda")]
        public List<AgendaDTO> GetLatestAgenda(long studentID)
        {
            return new SchoolService(CallContext).GetLatestAgenda(studentID);
        }

        [HttpGet]
        [Route("FillFineDue")]
        public List<StudentFeeDueDTO> FillFineDue(long studentID)
        {
            return new SchoolService(CallContext).FillFineDue(studentID);
        }

        [HttpGet]
        [Route("GetStudentFeeCollection")]
        public List<FeeCollectionDTO> GetStudentFeeCollection(long studentId)
        {
            return new SchoolService(CallContext).GetStudentFeeCollection(studentId);
        }

        [HttpGet]
        [Route("GetFeeDueAmount")]
        public decimal GetFeeDueAmount()
        {
            return new SchoolService(CallContext).GetFeeDueAmount();
        }

        [HttpGet]
        [Route("GetFeeDueAmountByStudentID")]
        public decimal GetFeeDueAmountByStudentID(long studentID)
        {
            return new SchoolService(CallContext).GetFeeDueAmountByStudentID(studentID);
        }

        [HttpGet]
        [Route("GetFeeTotal")]
        public decimal GetFeeTotal(long studentID)
        {
            return new SchoolService(CallContext).GetFeeTotal(studentID);
        }

        [HttpGet]
        [Route("GetFineCollected")]
        public List<StudentFeeDueDTO> GetFineCollected(long studentID)
        {
            return new SchoolService(CallContext).GetFineCollected(studentID);
        }

        [HttpGet]
        [Route("GetLeaveApplication")]
        public List<StudentLeaveApplicationDTO> GetLeaveApplication(long studentID)
        {
            return new SchoolService(CallContext).GetLeaveApplication(studentID);
        }

        [HttpGet]
        [Route("GetMarkList")]
        public List<MarkListViewDTO> GetMarkList(long studentID)
        {
            return new SchoolService(CallContext).GetMarkList(studentID);
        }

        [HttpGet]
        [Route("GetMyAssignmentsCount")]
        public int GetMyAssignmentsCount()
        {
            return new SchoolService(CallContext).GetMyAssignmentsCount();
        }

        [HttpGet]
        [Route("GetEmployeeAssignmentsCount")]
        public int GetEmployeeAssignmentsCount()
        {
            return new SchoolService(CallContext).GetEmployeeAssignmentsCount();
        }

        [HttpGet]
        [Route("GetMyClassCount")]
        public int GetMyClassCount()
        {
            return new SchoolService(CallContext).GetMyClassCount();
        }

        [HttpGet]
        [Route("GetMyAgendaCount")]
        public int GetMyAgendaCount()
        {
            return new SchoolService(CallContext).GetMyAgendaCount();
        }

        [HttpGet]
        [Route("GetMyStudents")]
        public ActionResult GetMyStudents()
        {
            return Ok(new SchoolService(CallContext).GetMyStudents());
        }

        [HttpGet]
        [Route("GetMyStudentsSiblingsCount")]
        public int GetMyStudentsSiblingsCount()
        {
            return new SchoolService(CallContext).GetMyStudentsSiblingsCount();
        }

        [HttpGet]
        [Route("GetStudentDetailsByStudentID")]
        public ActionResult GetStudentDetailsByStudentID(long studentID)
        {
            return Ok(new SchoolService(CallContext).GetStudentDetailsByStudentID(studentID));
        }

        //[HttpGet]
        //[Route("GetParentDetails")]
        //public GuardianDTO GetParentDetails(string EmailID)
        //{
        //    return new SchoolService(CallContext).GetParentDetails(EmailID);
        //}

        [HttpGet]
        [Route("GetParentStudents")]
        public ActionResult GetParentStudents()
        {
            return Ok(new SchoolService(CallContext).GetParentStudents());
        }

        [HttpGet]
        [Route("GetTeacherDetails")]
        public List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentID)
        {
            return new SchoolService(CallContext).GetTeacherDetails(studentID);
        }

        [HttpGet]
        [Route("GetTeacherClass")]
        public List<ClassTeacherMapDTO> GetTeacherClass()
        {
            return new SchoolService(CallContext).GetTeacherClass();
        }

        [HttpGet]
        [Route("GetStudentsByTeacherClassAndSection")]
        public ActionResult GetStudentsByTeacherClassAndSection(long classID, long sectionID)
        {
            return Ok(new SchoolService(CallContext).GetStudentsByTeacherClassAndSection(classID, sectionID));
        }

        [HttpGet]
        [Route("GetStaffProfile")]
        public ActionResult GetStaffProfile(long employeeID)
        {
            return Ok(new SchoolService(CallContext).GetStaffProfile(employeeID));
        }

        [HttpGet]
        [Route("GetClassStudents")]
        public List<KeyValueDTO> GetClassStudents(int classId, int sectionId)
        {
            return new SchoolService(CallContext).GetClassStudents(classId, sectionId);
        }

        [HttpGet]
        [Route("GetDynamicLookUpDataForMobileApp")]
        public List<KeyValueDTO> GetDynamicLookUpDataForMobileApp(DynamicLookUpType lookType, string searchText)
        {
            return new SchoolService(CallContext).GetDynamicLookUpDataForMobileApp(lookType, searchText);
        }

        [HttpGet]
        [Route("GetAcademicCalenderByMonthYear")]
        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year)
        {
            return new SchoolService(CallContext).GetAcademicCalenderByMonthYear(month, year);
        }

        [HttpGet]
        [Route("GetStudentAttendenceByYearMonthClassSection")]
        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthClassSection(int month, int year, int classId, int sectionId)
        {
            return new SchoolService(CallContext).GetStudentAttendenceByYearMonthClassSection(month, year, classId, sectionId);
        }

        [HttpGet]
        [Route("GetStudentAttendenceByDayClassSection")]
        public List<StudentAttendenceDTO> GetStudentAttendenceByDayClassSection(int month, int year, int classId, int sectionId, int day)
        {
            return new SchoolService(CallContext).GetStudentAttendenceByDayClassSection(month, year, classId, sectionId, day);
        }

        [HttpGet]
        [Route("GetStudentAttendenceByYearMonthStudentId")]
        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            return new SchoolService(CallContext).GetStudentAttendenceByYearMonthStudentId(month, year, studentId);
        }

        [HttpGet]
        [Route("GetStudentAttendenceCountByStudentID")]
        public StudentAttendenceDTO GetStudentAttendenceCountByStudentID(int month, int year, long studentID)
        {
            return new SchoolService(CallContext).GetStudentAttendenceCountByStudentID(month, year, studentID);
        }

        [HttpGet]
        [Route("GetClasswiseStudentData")]
        public ActionResult GetClasswiseStudentData(int classId, int sectionId)
        {
            return Ok(new SchoolService(CallContext).GetClasswiseStudentData(classId, sectionId));
        }

        [HttpPost]
        [Route("SaveStudentAttendence")]
        public OperationResultDTO SaveStudentAttendence(StudentAttendenceInfoDTO attendenceInfo)
        {
            return new SchoolService(CallContext).SaveStudentAttendence(attendenceInfo);
        }

        [HttpGet]
        [Route("GetStaffLeaveApplication")]
        public List<LeaveRequestDTO> GetStaffLeaveApplication()
        {
            return new SchoolService(CallContext).GetStaffLeaveApplication();
        }

        [HttpGet]
        [Route("FillFeeDue")]
        public List<StudentFeeDueDTO> FillFeeDue(long studentID)
        {
            return new SchoolService(CallContext).FillFeeDue(studentID);
        }

        [HttpGet]
        [Route("FillFeePaymentDetails")]
        public List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId)
        {
            return new SchoolService(CallContext).FillFeePaymentDetails(loginId);
        }

        [HttpGet]
        [Route("FillPendingFees")]
        public List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId)
        {
            return new SchoolService(CallContext).FillPendingFees(classId, studentId);
        }

        [HttpGet]
        [Route("GetFeeCollected")]
        public List<StudentFeeDueDTO> GetFeeCollected(long studentId)
        {
            return new SchoolService(CallContext).GetFeeCollected(studentId);
        }

        [HttpGet]
        [Route("GetStudentApplication")]
        public List<StudentApplicationDTO> GetStudentApplication()
        {
            return new SchoolService(CallContext).GetStudentApplication();
        }

        [HttpGet]
        [Route("GetStudentApplicationCount")]
        public int GetStudentApplicationCount()
        {
            return new SchoolService(CallContext).GetStudentApplicationCount();
        }

        [HttpGet]
        [Route("GetApplicationCount")]
        public int GetApplicationCount()
        {
            return new SchoolService(CallContext).GetApplicationCount();
        }


        [HttpGet]
        [Route("GetMyLessonPlanCount")]
        public int GetMyLessonPlanCount()
        {
            return new SchoolService(CallContext).GetMyLessonPlanCount();
        }

        [HttpGet]
        [Route("GetMyNotificationCount")]
        public int GetMyNotificationCount()
        {
            return new SchoolService(CallContext).GetMyNotificationCount();
        }

        [HttpGet]
        [Route("GetMyNotification")]
        public List<NotificationAlertsDTO> GetMyNotification(int page, int pageSize)
        {
            return new SchoolService(CallContext).GetMyNotification(page, pageSize);
        }

        [HttpGet]
        [Route("GetAssignmentsForTeacher")]
        public List<AssignmentDTO> GetAssignmentsForTeacher()
        {
            return new SchoolService(CallContext).GetAssignmentsForTeacher();
        }

        [HttpGet]
        [Route("GetMarkListForTeacher")]
        public List<MarkListViewDTO> GetMarkListForTeacher()
        {
            return new SchoolService(CallContext).GetMarkListForTeacher();
        }

        [HttpGet]
        [Route("GetMyLessonPlans")]
        public List<LessonPlanDTO> GetMyLessonPlans()
        {
            return new SchoolService(CallContext).GetMyLessonPlans();
        }

        [HttpGet]
        [Route("GetTransportApplications")]
        public List<TransportApplicationDTO> GetTransportApplications()
        {
            return new SchoolService(CallContext).GetTransportApplications();
        }

        [HttpGet]
        [Route("GetTransportApplicationCount")]
        public int GetTransportApplicationCount()
        {
            return new SchoolService(CallContext).GetTransportApplicationCount();
        }

        [HttpGet]
        [Route("GetTimeTableByStudentID")]
        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStudentID(long studentID)
        {
            return new SchoolService(CallContext).GetTimeTableByStudentID(studentID);
        }

        [HttpGet]
        [Route("GetSubjectsByStudentID")]
        public List<ClassSubjectMapDTO> GetSubjectsByStudentID(long studentID)
        {
            return new SchoolService(CallContext).GetSubjectsByStudentID(studentID);
        }

        [HttpGet]
        [Route("GetTimeTableByStaffLoginID")]
        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStaffLoginID(long loginID)
        {
            return new SchoolService(CallContext).GetTimeTableByStaffLoginID(loginID);
        }

        [HttpGet]
        [Route("GetVehicleDetailsByEmployeeLoginID")]
        public List<Eduegate.Services.Contracts.School.Transports.VehicleDTO> GetVehicleDetailsByEmployeeLoginID()
        {
            return new SchoolService(CallContext).GetVehicleDetailsByEmployeeLoginID();
        }

        [HttpGet]
        [Route("GetDriverScheduleListByParameters")]
        public List<DriverScheduleLogDTO> GetDriverScheduleListByParameters(int routeID, int vehicleID, DateTime scheduledDate)
        {
            return new SchoolService(CallContext).GetDriverScheduleListByParameters(routeID, vehicleID, scheduledDate);
        }

        [HttpGet]
        [Route("GetRoutesByVehicleID")]
        public List<RoutesDTO> GetRoutesByVehicleID(long vehicleID)
        {
            return new SchoolService(CallContext).GetRoutesByVehicleID(vehicleID);
        }

        [HttpGet]
        [Route("GetRouteStopsByRouteID")]
        public List<RouteStopFeeDTO> GetRouteStopsByRouteID(long routeID)
        {
            return new SchoolService(CallContext).GetRouteStopsByRouteID(routeID);
        }

        [HttpGet]
        [Route("GetStudentsDetailsByRouteStopID")]
        public List<StudentRouteStopMapDTO> GetStudentsDetailsByRouteStopID(long routeStopMapID)
        {
            return new SchoolService(CallContext).GetStudentsDetailsByRouteStopID(routeStopMapID);
        }

        [HttpGet]
        [Route("UpdateUserGeoLocation")]
        public void UpdateUserGeoLocation(string geoLocation)
        {
            new SchoolService(CallContext).UpdateUserGeoLocation(geoLocation);
        }

        [HttpGet]
        [Route("GetRouteStudentsAndStaffDetailsByEmployeeLoginID")]
        public List<Eduegate.Services.Contracts.School.Transports.VehicleDTO> GetRouteStudentsAndStaffDetailsByEmployeeLoginID()
        {
            return new SchoolService(CallContext).GetRouteStudentsAndStaffDetailsByEmployeeLoginID();
        }

        [HttpGet]
        [Route("GetAllRouteStudentsAndStaffDetails")]
        public List<Eduegate.Services.Contracts.School.Transports.VehicleDTO> GetAllRouteStudentsAndStaffDetails()
        {
            return new SchoolService(CallContext).GetAllRouteStudentsAndStaffDetails();
        }

        [HttpGet]
        [Route("GetVehicleAssignDetailsByEmployeeIDandRouteID")]
        public List<AssignVehicleDTO> GetVehicleAssignDetailsByEmployeeIDandRouteID(long employeeID, long routeID)
        {
            return new SchoolService(CallContext).GetVehicleAssignDetailsByEmployeeIDandRouteID(employeeID, routeID);
        }

        [HttpGet]
        [Route("GetPresentStatuses")]
        public List<PresentStatusDTO> GetPresentStatuses()
        {
            return new SchoolService(CallContext).GetPresentStatuses();
        }

        [HttpGet]
        [Route("GetLessonPlanListByStudentID")]
        public List<LessonPlanDTO> GetLessonPlanListByStudentID(long studentID)
        {
            return new SchoolService(CallContext).GetLessonPlanListByStudentID(studentID);
        }

        [HttpGet]
        [Route("GetStudentLeaveCountByStudentID")]
        public StudentLeaveApplicationDTO GetStudentLeaveCountByStudentID(long studentID)
        {
            return new SchoolService(CallContext).GetStudentLeaveCountByStudentID(studentID);
        }

        [HttpPost]
        [Route("SubmitStudentLeaveApplication")]
        public OperationResultDTO SubmitStudentLeaveApplication(StudentLeaveApplicationDTO leaveInfo)
        {
            return new SchoolService(CallContext).SubmitStudentLeaveApplication(leaveInfo);
        }

        [HttpGet]
        [Route("DeleteStudentLeaveApplicationByID")]
        public void DeleteStudentLeaveApplicationByID(long leaveApplicationID)
        {
            new SchoolService(CallContext).DeleteStudentLeaveApplicationByID(leaveApplicationID);
        }

        [HttpGet]
        [Route("GetStudentLeaveApplicationByID")]
        public StudentLeaveApplicationDTO GetStudentLeaveApplicationByID(long leaveApplicationID)
        {
            return new SchoolService(CallContext).GetStudentLeaveApplicationByID(leaveApplicationID);
        }

        [HttpPost]
        [Route("SaveStudentAndStaffScheduleLogs")]
        public string SaveStudentAndStaffScheduleLogs(DriverScheduleLogDTO scheduleLogData)
        {
            return new SchoolService(CallContext).SaveStudentAndStaffScheduleLogs(scheduleLogData);
        }

        [HttpPost]
        [Route("SyncDriverScheduleLogs")]
        public long SyncDriverScheduleLogs(DriverScheduleLogDTO scheduleLogData)
        {
            return new SchoolService(CallContext).SyncDriverScheduleLogs(scheduleLogData);
        }

        //[HttpGet]
        //[Route("GetScheduleLogs")]
        //public List<DriverScheduleLogDTO> GetScheduleLogs(string scheduleType, string passengerType, long vehicleID, long routeStopMapID, long routeID)
        //{
        //    return new SchoolService(CallContext).GetScheduleLogs(scheduleType, passengerType, vehicleID, routeStopMapID, routeID);
        //}

        [HttpGet]
        [Route("GetScheduleLogsByRoute")]
        public List<DriverScheduleLogDTO> GetScheduleLogsByRoute(string scheduleType, string passengerType, long vehicleID, long routeID)
        {
            return new SchoolService(CallContext).GetScheduleLogsByRoute(scheduleType, passengerType, vehicleID, routeID);
        }

        [HttpGet]
        [Route("GetStudentStaffDropScheduleDatasforDropOut")]
        public DriverScheduleLogDTO GetStudentStaffDropScheduleDatasforDropOut(string passengerType, long vehicleID, long routeID)
        {
            return new SchoolService(CallContext).GetStudentStaffDropScheduleDatasforDropOut(passengerType, vehicleID, routeID);
        }

        //[HttpGet]
        //[Route("GetStaffDropScheduleDatasByRoute")]
        //public DriverScheduleLogDTO GetStaffDropScheduleDatasByRoute(long vehicleID, long routeID)
        //{
        //    return new SchoolService(CallContext).GetStaffDropScheduleDatasByRoute(vehicleID, routeID);
        //}

        [HttpGet]
        [Route("GetScheduleLogsByDateForOfflineDB")]
        public List<DriverScheduleLogDTO> GetScheduleLogsByDateForOfflineDB()
        {
            return new SchoolService(CallContext).GetScheduleLogsByDateForOfflineDB();
        }

        [HttpGet]
        [Route("GetStaffAttendanceByMonthYear")]
        public List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year)
        {
            return new SchoolService(CallContext).GetStaffAttendanceByMonthYear(month, year);
        }

        [HttpGet]
        [Route("GetStaffAttendenceByYearMonthEmployeeID")]
        public List<StaffAttendenceDTO> GetStaffAttendenceByYearMonthEmployeeID(int month, int year)
        {
            return new SchoolService(CallContext).GetStaffAttendenceByYearMonthEmployeeID(month, year);
        }

        [HttpGet]
        [Route("GetStaffAttendenceCountByEmployeeID")]
        public StaffAttendenceDTO GetStaffAttendenceCountByEmployeeID(int month, int year, long employeeID)
        {
            return new SchoolService(CallContext).GetStaffAttendenceCountByEmployeeID(month, year, employeeID);
        }

        [HttpGet]
        [Route("GetSchoolsByParent")]
        public List<KeyValueDTO> GetSchoolsByParent()
        {
            return new SchoolService(CallContext).GetSchoolsByParent();
        }

        [HttpGet]
        [Route("GetAcademicYearBySchool")]
        public List<KeyValueDTO> GetAcademicYearBySchool(int schoolID)
        {
            return new SchoolService(CallContext).GetAcademicYearBySchool(schoolID);
        }

        [HttpGet]
        [Route("GetFeeCollectionHistories")]
        public List<FeePaymentHistoryDTO> GetFeeCollectionHistories(byte schoolID, int academicYearID)
        {
            return new SchoolService(CallContext).GetFeeCollectionHistories(schoolID, academicYearID);
        }

        [HttpGet]
        [Route("GetStudentsFeePaymentDetails")]
        public List<FeePaymentDTO> GetStudentsFeePaymentDetails()
        {
            return new SchoolService(CallContext).GetStudentsFeePaymentDetails();
        }

        [HttpGet]
        [Route("GetStudentFeeDetails")]
        public FeePaymentDTO GetStudentFeeDetails(long studentID)
        {
            return new SchoolService(CallContext).GetStudentFeeDetails(studentID);
        }

        [HttpPost]
        [Route("SubmitAmountAsLog")]
        public string SubmitAmountAsLog(decimal totalAmount)
        {
            return new SchoolService(CallContext).SubmitAmountAsLog(totalAmount);
        }

        [HttpPost]
        [Route("GenerateCardSession")]
        public string GenerateCardSession(byte? paymentModeID = null)
        {
            return new SchoolService(CallContext).GenerateCardSession(paymentModeID);
        }

        [HttpPost]
        [Route("InitiateFeeCollections")]
        public OperationResultDTO InitiateFeeCollections(List<FeeCollectionDTO> feeCollections)
        {
            return new SchoolService(CallContext).InitiateFeeCollections(feeCollections);
        }

        [HttpGet]
        [Route("UpdateFeePaymentStatus")]
        public string UpdateFeePaymentStatus(string transactionNumber, byte? paymentModeID = null)
        {
            if (transactionNumber == "NA")
            {
                transactionNumber = null;
            }
            if (paymentModeID == 0)
            {
                paymentModeID = null;
            }

            return new SchoolService(CallContext).UpdateFeePaymentStatus(transactionNumber, paymentModeID);
        }

        [HttpGet]
        [Route("GetCardSession")]
        public string GetCardSession()
        {
            return new SchoolService(CallContext).GetCardSession();
        }

        [HttpGet]
        [Route("GetCardPaymentGatewayDatas")]
        public PaymentMasterVisaDTO GetCardPaymentGatewayDatas()
        {
            return new SchoolService(CallContext).GetCardPaymentGatewayDatas();
        }

        [HttpGet]
        [Route("ValidatePayment")]
        public string ValidatePayment()
        {
            return new SchoolService(CallContext).ValidatePayment();
        }

        [HttpPost]
        [Route("ResendReceiptMail")]
        public OperationResultDTO ResendReceiptMail(string transactionNumber, string mailID, string feeReceiptNo)
        {
            return new SchoolService(CallContext).ResendReceiptMail(transactionNumber, mailID,  feeReceiptNo);
        }

        [HttpGet]
        [Route("CheckFeeCollectionExistingStatus")]
        public OperationResultDTO CheckFeeCollectionExistingStatus(string transactionNumber)
        {
            return new SchoolService(CallContext).CheckFeeCollectionExistingStatus(transactionNumber);
        }

        [HttpPost]
        [Route("RetryPayment")]
        public OperationResultDTO RetryPayment(string transactionNumber, byte? paymentModeID = null)
        {
            return new SchoolService(CallContext).RetryPayment(transactionNumber, paymentModeID);
        }

        [HttpGet]
        [Route("GetFeeCollectionDetailsByTransactionNumber")]
        public List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber)
        {
            return new SchoolService(CallContext).GetFeeCollectionDetailsByTransactionNumber(transactionNumber);
        }

        [HttpGet]
        [Route("ValidateCartPayment")]
        public bool ValidateCartPayment(long cartID, string returnIndicator, byte? paymentMethodID = null, long? totalCartAmount = null)
        {
            return new SchoolService(CallContext).ValidateCartPayment(cartID, returnIndicator, paymentMethodID, totalCartAmount);
        }

        [HttpGet]
        [Route("QPayInquiry")]
        public PaymentLogDTO QPayInquiry(long? cartID=0)
        {
            return new SchoolService(CallContext).QPayInquiry(cartID);
        }

        [HttpPost]
        [Route("ValidateMasterCardSuccessResponse")]
        public bool ValidateMasterCardSuccessResponse(long cartID, string returnIndicator)
        {
            return new SchoolService(CallContext).ValidateMasterCardSuccessResponse(cartID, returnIndicator);
        }

        [HttpGet]
        [Route("GetPaymentSession")]
        public PaymentMasterVisaDTO GetPaymentSession(long shoppingCartID, decimal totalAmount, byte? paymentMethodID = null)
        {
            return new SchoolService(CallContext).GetPaymentSession(shoppingCartID, totalAmount, paymentMethodID);
        }

        [HttpGet]
        [Route("GetPickupRequestsCount")]
        public decimal GetPickupRequestsCount()
        {
            return new SchoolService(CallContext).GetPickupRequestsCount();
        }

        [HttpGet]
        [Route("GetStudentPickupRequests")]
        public List<StudentPickupRequestDTO> GetStudentPickupRequests()
        {
            return new SchoolService(CallContext).GetStudentPickupRequests();
        }

        [HttpGet]
        [Route("GetGuardianDetails")]
        public GuardianDTO GetGuardianDetails()
        {
            return new SchoolService(CallContext).GetGuardianDetails();
        }

        [HttpPost]
        [Route("CancelStudentPickupRequestByID")]
        public OperationResultDTO CancelStudentPickupRequestByID(long pickupRequestID)
        {
            return new SchoolService(CallContext).CancelStudentPickupRequestByID(pickupRequestID);
        }

        [HttpPost]
        [Route("SubmitStudentPickupRequest")]
        public OperationResultDTO SubmitStudentPickupRequest(StudentPickupRequestDTO studentPickupRequest)
        {
            return new SchoolService(CallContext).SubmitStudentPickupRequest(studentPickupRequest);
        }

        [HttpPost]
        [Route("SubmitStudentDailyPickupRequest")]
        public OperationResultDTO SubmitStudentDailyPickupRequest(StudentPickupRequestDTO register)
        {
            return new SchoolService(CallContext).SubmitStudentDailyPickupRequest(register);
        }

        [HttpGet]
        [Route("GetRegisteredPickupRequests")]
        public List<StudentPickupRequestDTO> GetRegisteredPickupRequests(long loginID, string barCodeValue)
        {
            return new SchoolService(CallContext).GetRegisteredPickupRequests(loginID, barCodeValue);
        }

        [HttpPost]
        [Route("CancelorActiveStudentPickupRegistration")]
        public OperationResultDTO CancelorActiveStudentPickupRegistration(long studentPickerStudentMapIID)
        {
            return new SchoolService(CallContext).CancelorActiveStudentPickupRegistration(studentPickerStudentMapIID);
        }

        [HttpGet]
        [Route("GetPickupRegisterCount")]
        public decimal GetPickupRegisterCount()
        {
            return new SchoolService(CallContext).GetPickupRegisterCount();
        }

        [HttpGet]
        [Route("GePickupRegisteredDetailsByQR")]
        public StudentPickupRequestDTO GePickupRegisteredDetailsByQR(string qrCode)
        {
            return new SchoolService(CallContext).GePickupRegisteredDetailsByQR(qrCode);
        }

        [HttpPost]
        [Route("SubmitStudentPickLogs")]
        public OperationResultDTO SubmitStudentPickLogs(StudentPickupRequestDTO submitLog)
        {
            return new SchoolService(CallContext).SubmitStudentPickLogs(submitLog);
        }

        [HttpGet]
        [Route("GetTodayStudentPickLogs")]
        public List<StudentPickupRequestDTO> GetTodayStudentPickLogs()
        {
            return new SchoolService(CallContext).GetTodayStudentPickLogs();
        }

        [HttpGet]
        [Route("GetAllergies")]
        public List<KeyValueDTO> GetAllergies()
        {
            return new SchoolService(CallContext).GetAllergies();
        }

        [HttpPost]
        [Route("SaveAllergies")]
        public OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID)
        {
            return new SchoolService(CallContext).SaveAllergies(studentID, allergyID, SeverityID);
        }

        [HttpGet]
        [Route("GetStudentAllergies")]
        public List<AllergyStudentDTO> GetStudentAllergies()
        {
            return new SchoolService(CallContext).GetStudentAllergies();
        }


        [HttpGet]
        [Route("GetSeverity")]
        public List<KeyValueDTO> GetSeverity()
        {
            return new SchoolService(CallContext).GetSeverity();
        }   

        [HttpGet]
        [Route("CheckStudentAllergy")]
        public List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID)
        {
            return new SchoolService(CallContext).CheckStudentAllergy(studentID, cartID);
        }

        [HttpPost]
        [Route("SaveDefaultStudent")]
        public OperationResultDTO SaveDefaultStudent(long studentID)
        {
            return new SchoolService(CallContext).SaveDefaultStudent(studentID);
        }

        [HttpGet]
        [Route("GetDefaultStudent")]
        public CustomerDTO GetDefaultStudent()
        {
            return new SchoolService(CallContext).GetDefaultStudent();
        }

        [HttpPost]
        [Route("SubmitStaffLeaveApplication")]
        public OperationResultDTO SubmitStaffLeaveApplication(LeaveRequestDTO leaveInfo)
        {
            return new SchoolService(CallContext).SubmitStaffLeaveApplication(leaveInfo);
        }

        [HttpGet]
        [Route("GetLeaveTypes")]
        public List<KeyValueDTO> GetLeaveTypes()
        {
            return new SchoolService(CallContext).GetLeaveTypes();
        }

        [HttpGet]
        [Route("GetStaticPage")]
        public StaticContentDataDTO GetStaticPage(long contentID)
        {
            return new SchoolService(CallContext).GetStaticPage(contentID);
        }

        [HttpGet]
        [Route("GetVisitorDetails")]
        public VisitorDTO GetVisitorDetails(string QID, string passportNumber)
        {
            return new SchoolService(CallContext).GetVisitorDetails(QID, passportNumber);
        }

        [HttpPost]
        [Route("VisitorRegistration")]
        public OperationResultDTO VisitorRegistration(VisitorDTO visitorDetails)
        {
            return new SchoolService(CallContext).VisitorRegistration(visitorDetails);
        }

        [HttpGet]
        [Route("GetVisitorDetailsByLoginID")]
        public VisitorDTO GetVisitorDetailsByLoginID(long loginID)
        {
            return new SchoolService(CallContext).GetVisitorDetailsByLoginID(loginID);
        }

        [HttpPost]
        [Route("UpdateStudentPickLogStatus")]
        public string UpdateStudentPickLogStatus(long picklogID)
        {
            return new SchoolService(CallContext).UpdateStudentPickLogStatus(picklogID);
        }

        [HttpGet]
        [Route("GetVisitorDetailsByVisitorCode")]
        public VisitorDTO GetVisitorDetailsByVisitorCode(string visitorCode)
        {
            return new SchoolService(CallContext).GetVisitorDetailsByVisitorCode(visitorCode);
        }

        [HttpPost]
        [Route("UploadContentAsString")]
        public long UploadContentAsString(Services.Contracts.Contents.ContentFileDTO content)
        {
            return new SchoolService(CallContext).UploadContentAsString(content);
        }

        [HttpGet]
        [Route("GetAndUpdateActivePickLogsForInspection")]
        public List<StudentPickupRequestDTO> GetAndUpdateActivePickLogsForInspection()
        {
            return new SchoolService(CallContext).GetAndUpdateActivePickLogsForInspection();
        }

        [HttpGet]
        [Route("GetTodayInspectionColour")]
        public  string GetTodayInspectionColour()
        {
            return new SchoolService(CallContext).GetTodayInspectionColour();
        }

        [HttpGet]
        [Route("GetTodayStaffAttendanceByLoginID")]
        public StaffAttendenceDTO GetTodayStaffAttendanceByLoginID()
        {
            return new SchoolService(CallContext).GetTodayStaffAttendanceByLoginID();
        }

        [HttpGet]
        [Route("GetOrderTypes")]
        public List<KeyValueDTO> GetOrderTypes()
        {
            return new SchoolService(CallContext).GetOrderTypes();
        }

        [HttpGet]
        [Route("GetMyStudentsBySchool")]
        public ActionResult GetMyStudentsBySchool()
        {
            return Ok(new SchoolService(CallContext).GetMyStudentsBySchool());
        }


        [HttpGet]
        [Route("GetParentDetailsByParentCode")]
        public ActionResult GetParentDetailsByParentCode(string parentCode)
        {
            return Ok(new SchoolService(CallContext).GetParentDetailsByParentCode(parentCode));
        }

        [HttpGet]
        [Route("GetParentPortalUrl")]
        public string GetParentPortalUrl()
        {
            return new SchoolService(CallContext).GetParentPortalUrl();
        }

        [HttpGet]
        [Route("GetCircularsByEmployee")]
        public List<CircularListDTO> GetCircularsByEmployee()
        {
            return new SchoolService(CallContext).GetCircularsByEmployee();
        }

        [HttpPost]
        [Route("MarkNotificationAsRead")]
        public string MarkNotificationAsRead(long notificationAlertID)
        {
            return new SchoolService(CallContext).MarkNotificationAsRead(notificationAlertID);
        }

        [HttpGet]
        [Route("CheckAppVersion")]
        public ActionResult CheckAppVersion([FromQuery] string currentAppVersion = "",
            [FromQuery] string mobileAppVersion = "", [FromQuery] string appID = "")
        {
            return Ok(new SchoolService(CallContext)
                .CheckAppVersion(string.IsNullOrEmpty(currentAppVersion) ? mobileAppVersion : currentAppVersion, appID));
        }

        [HttpGet]
        [Route("GetReportcardByStudentID")]
        public List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID)
        {
            return new SchoolService(CallContext).GetAcademicYearByProgressReport(studentID);
        }

        [HttpGet]
        [Route("GetReportCardList")]
        public List<ProgressReportNewDTO> GetReportCardList(long studentID, int classID, int sectionID, int academicYearID)
        {
            return new SchoolService(CallContext).GetReportCardList(studentID, classID, sectionID, academicYearID);
        }

        [HttpGet]
        [Route("GetLastTenFeeCollectionHistories")]
        public List<FeePaymentHistoryDTO> GetLastTenFeeCollectionHistories()
        {
            return new SchoolService(CallContext).GetLastTenFeeCollectionHistories();
        }

        [HttpPost]
        [Route("CheckTransactionPaymentStatus")]
        public OperationResultDTO CheckTransactionPaymentStatus(string transactionNumber, byte? paymentModeID = null)
        {
            return new SchoolService(CallContext).CheckTransactionPaymentStatus(transactionNumber, paymentModeID);
        }

        [HttpGet]
        [Route("GetStudentDetailsByAdmissionNumber")]
        public ActionResult GetStudentDetailsByAdmissionNumber(string admissionNumber)
        {
            return Ok(new SchoolService(CallContext).GetStudentDetailsByAdmissionNumber(admissionNumber));
        }


        [HttpGet]
        [Route("GetLatestStaffCircularCount")]
        public decimal GetLatestStaffCircularCount()
        {
            return new SchoolService(CallContext).GetLatestStaffCircularCount();
        }

        [HttpGet]
        [Route("GetDriverGeoLocationByStudent")]
        public string GetDriverGeoLocationByStudent(long studentID)
        {
            return new SchoolService(CallContext).GetDriverGeoLocationByStudent(studentID);
        }

        [HttpGet]
        [Route("GetDriverDetailsByStudent")]
        public EmployeesDTO GetDriverDetailsByStudent(long studentID)
        {
            return new SchoolService(CallContext).GetDriverDetailsByStudent(studentID);
        }

        [HttpGet]
        [Route("GetStopsPositionsByRoute")]
        public List<RouteStopFeeDTO> GetStopsPositionsByRoute(long studentID)
        {
            return new SchoolService(CallContext).GetStopsPositionsByRoute(studentID);
        }

        [HttpGet]
        [Route("GetStudentInOutVehicleStatus")]
        public bool GetStudentInOutVehicleStatus(long studentID)
        {
            return new SchoolService(CallContext).GetStudentInOutVehicleStatus(studentID);
        }


        [HttpGet]
        [Route("GetAllTickets")]
        public List<TicketDTO> GetAllTickets()
        {
            return new SchoolService(CallContext).GetAllTicketsByLoginID();
        }

        [HttpPost]
        [Route("SaveTicketCommunication")]
        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO)
        {
            return new SchoolService(CallContext).SaveTicketCommunication(ticketCommunicationDTO);
        }

        [HttpGet]
        [Route("GetActiveSignupGroups")]
        public List<SignUpGroupDTO> GetActiveSignupGroups()
        {
            return new SchoolService(CallContext).GetActiveSignupGroups();
        }

        [HttpGet]
        [Route("FillSignUpDetailsByGroupID")]

        public SignUpGroupDTO FillSignUpDetailsByGroupID(int groupID)
        {
            return new SchoolService(CallContext).FillSignUpDetailsByGroupID(groupID);
        }

        [HttpPost]
        [Route("SaveSelectedSignUpSlot")]
        public OperationResultDTO SaveSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            return new SchoolService(CallContext).SaveSelectedSignUpSlot(signUpSlotMap);
        }

        [HttpPost]
        [Route("CancelSelectedSignUpSlot")]
        public OperationResultDTO CancelSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            return new SchoolService(CallContext).CancelSelectedSignUpSlot(signUpSlotMap);
        }

        [HttpPost]
        [Route("GetDriverGeoLocationLogByStudent")]
        public List<GeoLocationLogDTO> GetDriverGeoLocationLogByStudent(long studentID)
        {
            return new SchoolService(CallContext).GetDriverGeoLocationLogByStudent(studentID);
        }

        [HttpPost]
        [Route("SendAttendanceNotificationsToParents")]
        public string SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            return new SchoolService(CallContext).SendAttendanceNotificationsToParents(classId, sectionId);
        }


        [HttpPost]
        [Route("SubmitDriverVehicleTracking")]
        public OperationResultDTO SubmitDriverVehicleTracking(DriverVehicleTrackingDTO trackingInfo)
        {
            return new SchoolService(CallContext).SubmitDriverVehicleTracking(trackingInfo);
        }


        [HttpGet]
        [Route("GetVehicles")]

        public List<KeyValueDTO> GetVehicles()
        {
            return new SchoolService(CallContext).GetVehicles();
        }

        [HttpGet]
        [Route("MarkDriverAttendanceOnPickupStart")]

        public void MarkDriverAttendanceOnPickupStart()
        {
           new SchoolService(CallContext).MarkDriverAttendanceOnPickupStart();
        }

        [HttpGet]
        [Route("GetDriverGeoLocationByStaff")]
        public string GetDriverGeoLocationByStaff()
        {
            return new SchoolService(CallContext).GetDriverGeoLocationByStaff();
        }
        [HttpGet]
        [Route("GetStopsPositionsByRouteStaff")]
        public List<RouteStopFeeDTO> GetStopsPositionsByStaff()
        {
            return new SchoolService(CallContext).GetStopsPositionsByStaff();
        }


        [HttpGet]
        [Route("GetStaffInOutVehicleStatus")]
        public bool GetStaffInOutVehicleStatus()
        {
            return new SchoolService(CallContext).GetStaffInOutVehicleStatus();
        }

        [HttpGet]
        [Route("GetDriverDetailsByStaff")]
        public EmployeesDTO GetDriverDetailsByStaff()
        {
            return new SchoolService(CallContext).GetDriverDetailsByStaff();
        }

        [HttpGet]
        [Route("GetSalarySlipList")]
        public SalarySlipDTO GetSalarySlipList(int Month ,int Year)
        {
            return new SchoolService(CallContext).GetSalarySlipList(Month, Year);
        }

        [HttpGet]
        [Route("GetMeetingRequestsByParentID")]
        public List<MeetingRequestDTO> GetMeetingRequestsByParentID()
        {
            return new SchoolService(CallContext).GetMeetingRequestsByParentID();
        }

        //[HttpPost]
        //[Route("SaveTicketCommunication")]
        //public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO)
        //{
        //    return new SchoolService(CallContext).SaveTicketCommunication(ticketCommunicationDTO);
        //}

        
        [HttpGet]
        [Route("GetSchoolPrincipal")]
        public KeyValueDTO GetSchoolPrincipal(byte schoolID)
        {
            return new SchoolService(CallContext).GetSchoolPrincipal(schoolID);
        }

        [HttpGet]
        [Route("GetSchoolVicePrincipal")]
        public KeyValueDTO GetSchoolVicePrincipal(byte schoolID)
        {
            return new SchoolService(CallContext).GetSchoolVicePrincipal(schoolID);
        }

        [HttpGet]
        [Route("GetSchoolHeadMistress")]
        public KeyValueDTO GetSchoolHeadMistress(byte schoolID)
        {
            return new SchoolService(CallContext).GetSchoolHeadMistress(schoolID);
        }

        [HttpGet]
        [Route("GetClassTeachers")]
        public KeyValueDTO GetClassTeachers(int classID, int sectionID, int academicYearID)
        {
            return new SchoolService(CallContext).GetClassHeadTeacher(classID, sectionID, academicYearID);
        }

        [HttpGet]
        [Route("GetClassCoordinator")]
        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            return new SchoolService(CallContext).GetClassCoordinator(classID, sectionID, academicYearID);
        }

        [HttpGet]
        [Route("GetClassAssociateTeachers")]

        public List<KeyValueDTO> GetClassAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            return new SchoolService(CallContext).GetAssociateTeachers(classID, sectionID, academicYearID);
        }

        [HttpGet]
        [Route("GetClassOtherTeachers")]
        public List<KeyValueDTO> GetClassOtherTeachers(int classID, int sectionID, int academicYearID)
        {
            return new SchoolService(CallContext).GetOtherTeachersByClass(classID, sectionID, academicYearID);
        }     

        [HttpGet]
        [Route("GetMeetingRequestSlotsByEmployeeID")]

        public List<SignUpDTO> GetMeetingRequestSlotsByEmployeeID(long employeeID, string reqSlotDateString)
        {
             return  new SignUpBL(CallContext).GetMeetingRequestSlotsByEmployeeID(employeeID, reqSlotDateString);
        }

        [HttpPost]
        [Route("SubmitMeetingRequest")]
        public OperationResultDTO SubmitMeetingRequest(MeetingRequestDTO meetingRequestDTO)
        {
            return new SignUpBL(CallContext).SaveMeetingRequest(meetingRequestDTO);
        }

        [HttpGet]
        [Route("GetStudentClassWiseAttendance")]
        public List<StudentAttendenceDTO> GetStudentClassWiseAttendance(long studentID, int schoolID)
        {
            var academicYearID = CallContext.AcademicYearID;

            return new SchoolService(CallContext).GetStudentClassWiseAttendance(studentID, schoolID,  academicYearID);
        }

        [HttpPost]
        [Route("CustomerFeedback")]
        public ActionResult CustomerFeedback(CustomerFeedbackDTO feedback)
        {
            return Ok(new SchoolService(CallContext).CustomerFeedback(feedback));
        }

        [HttpGet]
        [Route("GetVehicleDetailsWithRoutesAndStopsByEmployeeLoginID")]
        public List<Eduegate.Services.Contracts.School.Transports.VehicleDTO> GetVehicleDetailsWithRoutesAndStopsByEmployeeLoginID()
        {
            return new SchoolService(CallContext).GetVehicleDetailsWithRoutesAndStopsByEmployeeLoginID();
        }

        [HttpGet]
        [Route("GetParentAllotedMeetings")]
        public List<SignupSlotAllocationMapDTO> GetParentAllotedMeetings()
        {
            return new SchoolService(CallContext).GetParentAllotedMeetings();
        }

        [HttpPost]
        [Route("SubmitMeetingRemarks")]
        public ActionResult SubmitMeetingRemarks(SignupSlotRemarkMapDTO remarkEntry)
        {
            return  Ok(new SchoolService(CallContext).SaveSignupSlotRemarkMap(remarkEntry));
        }


        [HttpPost]
        [Route("GenerateTicket")]
        public OperationResultDTO GenerateTicket(TicketDTO ticket)
        {
            return new SchoolService(CallContext).GenerateTicket(ticket);
        }

        [HttpGet]
        [Route("GetUnverifiedStudentsAssignedToVisitor")]
        public List<StudentPickupRequestDTO> GetUnverifiedStudentsAssignedToVisitor(string visitorCode)
        {
            return new SchoolService(CallContext).GetUnverifiedStudentsAssignedToVisitor(visitorCode);
        }

        [HttpGet]
        [Route("GetTodayStudentPickLogsByvisitorLoginID")]
        public List<StudentPickupRequestDTO> GetTodayStudentPickLogsByvisitorLoginID(string visitorCode)
        {
            return new SchoolService(CallContext).GetTodayStudentPickLogsByvisitorLoginID(visitorCode);
        }


        [HttpPost]
        [Route("EditUserDetails")]
        public UserDTO EditUserDetails([FromBody] UserDTO updatedDetails)
        {
            return new SchoolService(CallContext).EditUserDetails(updatedDetails);
        }


        [HttpGet]
        [Route("GetLibraryTransactions")]
        public List<LibraryTransactionDTO> GetLibraryTransactions(string filter)
        {
            return new SchoolService(CallContext).GetLibraryTransactions(filter);
        }

        [HttpGet]
        [Route("GetClassTeacherClass")]
        public List<ClassTeacherMapDTO> GetClassTeacherClass()
        {
            return new SchoolService(CallContext).GetClassTeacherClass();
        }

        [HttpGet]
        [Route("GetCounselorList")]
        public List<CounselorHubListDTO> GetCounselorList()
        {
            return new SchoolService(CallContext).GetCounselorList();
        }

        [HttpGet]
        [Route("GetLookUpData")]
        public List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText)
        {
            return new SchoolService(CallContext).GetLookUpData(lookType, searchText);
        }

        [HttpPost]
        [Route("GetBoilerPlates")]
        public  Task<IActionResult> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo)
        {
            return new SchoolService(CallContext).GetBoilerPlates(boilerPlateInfo);
        }

        [HttpGet]
        [Route("GetSchoolsProfileWithAcademicYear")]
        public List<SchoolsDTO> GetSchoolsProfileWithAcademicYear()
        {
            return new SchoolService(CallContext).GetSchoolsProfileWithAcademicYear();
        }


        [HttpGet]
        [Route("GetCurrentAcademicYearBySchoolID")]
        public KeyValueDTO GetCurrentAcademicYearBySchoolID(long schoolID)
        {
            return new SchoolService(CallContext).GetCurrentAcademicYearBySchoolID(schoolID);
        }


        [HttpGet]
        [Route("GetActiveAcademicYearListData")]
        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            return new SchoolService(CallContext).GetActiveAcademicYearListData();
        }

        [HttpPost]
        [Route("CheckLeaveDateConflict")]
        public bool CheckLeaveDateConflict(LeaveRequestDTO leaveData)
        {
            return new SchoolService(CallContext).CheckLeaveDateConflict(leaveData);
        }

        [HttpGet]
        [Route("Getstudentsubjectlist")]
        public List<KeyValueDTO> Getstudentsubjectlist(long studentID)
        {
            return new SchoolService(CallContext).Getstudentsubjectlist(studentID);
        }

        [HttpGet]
        [Route("GetStudentSubjectWiseAgendas")]
        public List<AgendaDTO> GetStudentSubjectWiseAgendas(long studentID, long subjectID, string date)
        {
            return new SchoolService(CallContext).GetStudentSubjectWiseAgendas(studentID,  subjectID,  date);
        }


        [HttpGet]
        [Route("GetSupportSubCategoriesByCategoryID")]
        public List<KeyValueDTO> GetSupportSubCategoriesByCategoryID(int? supportCategoryID)
        {
            return new SchoolService(CallContext).GetSupportSubCategoriesByCategoryID(supportCategoryID);
        }

        [HttpGet]
        [Route("GetCircularUserTypes")]
        public List<string> GetCircularUserTypes()
        {
            return new SchoolService(CallContext).GetCircularUserTypes();
        }

        [HttpGet]
        [Route("GetTransportApplication")]
        public List<TransportApplicationStudentMapDTO> GetTransportApplication()
        {
            return new SchoolService(CallContext).GetTransportApplication();
        }


        [HttpGet]
        [Route("GetAboutandContactDetails")]
        public StaticContentDataDTO GetAboutandContactDetails(long contentID)
        {
            return new SchoolService(CallContext).GetAboutandContactDetails(contentID);
        }

        [HttpGet]
        [Route("GetTransportApplicationDetails")]
        public StaticContentDataDTO GetTransportApplicationDetails(long applicationId)
        {
            return new SchoolService(CallContext).GetTransportApplicationDetails(applicationId);
        }

        [HttpGet]
        [Route("GetStudentDetailsForTransportApplication")]
        public TransportApplicationDTO GetStudentDetailsForTransportApplication(long id)
        {
            return new SchoolService(CallContext).GetStudentDetailsForTransportApplication(id);
        }

        [HttpPost]
        [Route("SubmitTransportApplication")]
        public TransportApplicationDTO SubmitTransportApplication(TransportApplicationDTO transportApplicationData)
        {
            return new SchoolService(CallContext).SubmitTransportApplication(transportApplicationData);
        }



        [HttpGet]
        [Route("GetTeacherSubjectWiseAgendas")]
        public List<AgendaDTO> GetTeacherSubjectWiseAgendas( long subjectID, string date)
        {
            return new SchoolService(CallContext).GetTeacherSubjectWiseAgendas( subjectID, date);
        }

        [HttpGet]
        [Route("Getteachersubjectlist")]
        public List<KeyValueDTO> Getteachersubjectlist()
        {
            return new SchoolService(CallContext).Getteachersubjectlist();
        }

        [HttpGet]
        [Route("GetStudentAttendanceForTodayByClassAndSection")]
        public List<StudentAttendanceStatusDTO> GetStudentAttendanceForTodayByClassAndSection(long classID, long sectionID)
        {
            return new SchoolService(CallContext).GetStudentAttendanceForTodayByClassAndSection( classID,  sectionID);
        }

        
        [HttpGet]
        [Route("GetLeaveRequestByClassSectionDate")]
        public List<StudentLeaveApplicationDTO> GetLeaveRequestByClassSectionDate(long classID, long sectionID, DateTime date)
        {
            return new SchoolService(CallContext).GetLeaveRequestByClassSectionDate(classID, sectionID, date);
        }

        [HttpGet]
        [Route("GetPickupRequestsByClassSectionDate")]
        public List<StudentPickupRequestDTO> GetPickupRequestsByClassSectionDate(long classID, long sectionID, DateTime date)
        {
            return new SchoolService(CallContext).GetPickupRequestsByClassSectionDate(classID, sectionID, date);
        }

        [HttpGet]
        [Route("GetClassTimes")]

        public List<KeyValueDTO> GetClassTimes(long studentID)
        {
            return new SchoolService(CallContext).GetClassTimes(studentID);
        }


    }
}