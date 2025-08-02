using Eduegate.Domain;
using Eduegate.Domain.Content;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Mappers.HR.Leaves;
using Eduegate.Domain.Mappers.HR.Payroll;
using Eduegate.Domain.Mappers.Mutual;
using Eduegate.Domain.Mappers.Notifications;
using Eduegate.Domain.Mappers.Payment;
using Eduegate.Domain.Mappers.School.Academics;
using Eduegate.Domain.Mappers.School.Attendences;
using Eduegate.Domain.Mappers.School.Circulars;
using Eduegate.Domain.Mappers.School.CounselorHub;
using Eduegate.Domain.Mappers.School.Exams;
using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Domain.Mappers.School.Library;
using Eduegate.Domain.Mappers.School.Mutual;
using Eduegate.Domain.Mappers.School.School;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.Mappers.School.Transports;
using Eduegate.Domain.Mappers.SignUp.SignUps;
using Eduegate.Domain.Mappers.Support.CustomerSupport;
using Eduegate.Domain.MobileAppWrapper;
using Eduegate.Domain.Payment;
using Eduegate.Domain.Report;
using Eduegate.Domain.Repository.HR.Employee;
using Eduegate.Domain.Security;
using Eduegate.Domain.Setting;
using Eduegate.Domain.SignUp.SignUps;
using Eduegate.Domain.Support;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.MobileAppWrapper;
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
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Globalization;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;
using UglyToad.PdfPig.Content;
using static Eduegate.Domain.Mappers.School.Attendences.StudentAttendenceMapper;

namespace Eduegate.Services.MobileAppWrapper
{
    public class SchoolService : BaseService, ISchoolService
    {
        public SchoolService(CallContext context)
        {
            CallContext = context;
        }

        public string GetDriverGeoLocation()
        {
            var data = GeoLocationLogMapper
            .Mapper(CallContext)
            .GetGeoLocationByParentLoginID(CallContext.LoginID.Value);

            // return string.Join(",", data.Select(x => new {x.Latitude,x.Longitude}));
            if (data.Count > 0)
            {
                var d = data.Select(x => new { x.Latitude, x.Longitude }).FirstOrDefault();
                return string.Join(",", d.Latitude, d.Longitude);
            }
            else
            {
                return null;
            }
        }

        public List<CircularListDTO> GetLatestCirculars()
        {
            return CircularMapper
            .Mapper(CallContext)
            .GetCircularList(CallContext.LoginID.Value);
        }
        public List<CircularListDTO> GetCircularListByStudentID(long studentID)
        {
            return CircularMapper
           .Mapper(CallContext)
           .GetCircularListByStudentID(studentID);
        }
        public decimal GetLatestCircularCount()
        {
            return CircularMapper
            .Mapper(CallContext)
            .GetCircularCount(CallContext.LoginID.Value);
        }

        public decimal GetExamCount()
        {
            var parentID = StudentMapper
            .Mapper(CallContext)
            .GetParentID(CallContext.LoginID.Value);
            return ExamMapper
                .Mapper(CallContext)
                .GetExamCount(parentID);
        }

        public List<ExamDTO> GetExamLists(long studentID)
        {
            return ExamMapper
                .Mapper(CallContext)
                .GetExamLists(studentID);
        }

        public List<AssignmentDTO> GetAssignments(long studentID, int? SubjectID )
        {
            return AssignmentMapper
                .Mapper(CallContext)
                .GetAssignmentStudentwise(studentID, SubjectID);
        }

        public List<AgendaDTO> GetLatestAgenda(long studentID)
        {
            return AgendaTopicMapper
                .Mapper(CallContext)
                .GetAgendaList(studentID);
        }

        public List<StudentFeeDueDTO> FillFineDue(long studentID)
        {
            throw new NotImplementedException();
        }

        public List<FeeCollectionDTO> GetStudentFeeCollection(long studentId)
        {
            return FeeDueGenerationMapper
                .Mapper(CallContext)
                .GetStudentFeeCollection(studentId);
        }

        public decimal GetFeeDueAmount()
        {
            var parentID = StudentMapper
             .Mapper(CallContext)
             .GetParentID(CallContext.LoginID.Value);
            var pendingFee = FeeDueGenerationMapper
                .Mapper(CallContext)
                .PendingFeesByParent(parentID);
            return pendingFee;
        }

        public decimal GetFeeDueAmountByStudentID(long studentID)
        {
            var pendingFee = FeeDueGenerationMapper
                .Mapper(CallContext)
                .PendingFeesByStudent(studentID);

            return pendingFee;
        }

        public decimal GetFeeTotal(long studentID)
        {
            throw new NotImplementedException();
        }

        public List<StudentFeeDueDTO> GetFineCollected(long studentID)
        {
            throw new NotImplementedException();
        }

        public List<StudentLeaveApplicationDTO> GetLeaveApplication(long studentID)
        {
            return StudentLeaveApplicationMapper
                .Mapper(CallContext)
                .GetStudentLeaveApplication(studentID);
        }

        public List<MarkListViewDTO> GetMarkList(long studentID)
        {
            return MarkRegisterMapper
                .Mapper(CallContext)
                .GetMarkListStudentwise(studentID);
        }

        public int GetMyAssignmentsCount()
        {
            var parentID = StudentMapper.Mapper(CallContext).GetParentID(CallContext.LoginID.Value);

            return AssignmentMapper.Mapper(CallContext).GetAssignmentStudentCount(parentID);
        }

        public int GetEmployeeAssignmentsCount()
        {
            return AssignmentMapper.Mapper(CallContext)
                .GetAssignmentEmployeeCount(CallContext.EmployeeID.Value);
        }

        public int GetMyClassCount()
        {
            return ClassTeacherMapMapper
                .Mapper(CallContext)
                .GetTeacherClassCount(CallContext.EmployeeID.Value);
        }


        public int GetMyAgendaCount()
        {
            return AgendaTopicMapper
               .Mapper(CallContext)
               .GetAgendaCount(CallContext.LoginID.Value);
        }

        public List<StudentDTO> GetMyStudents()
        {
            //var parentID = StudentMapper
            //    .Mapper(CallContext)
            //    .GetParentLoginID(CallContext.LoginID.Value);
            return StudentMapper
                .Mapper(CallContext)
                .GetStudentsSiblings(CallContext.LoginID.Value);
        }

        public int GetMyStudentsSiblingsCount()
        {
            return StudentMapper.Mapper(CallContext)
                .GetStudentsSiblingsCount(CallContext.LoginID.Value);
        }

        public StudentDTO GetStudentDetailsByStudentID(long studentID)
        {
            return StudentMapper
                .Mapper(CallContext)
                .GetStudentDetailsByStudentID(studentID);
        }

        //To Get Parent Details
        //public GuardianDTO GetParentDetails(String EmailID)
        //{
        //    return StudentMapper
        //        .Mapper(CallContext)
        //        .GetParentDetails(EmailID);
        //}
        //End To Get Parent Details

        public List<StudentDTO> GetParentStudents()
        {
            return StudentMapper.Mapper(CallContext).GetStudentsSiblings(CallContext.LoginID.Value);
        }

        public List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentID)
        {
            return ClassTeacherMapMapper
                .Mapper(CallContext)
                .GetTeacherDetails(studentID);
        }

        public List<ClassTeacherMapDTO> GetTeacherClass()
        {
            return ClassTeacherMapMapper
                .Mapper(CallContext)
                .GetTeacherClass(CallContext.LoginID.Value);
        }

        public List<StudentDTO> GetStudentsByTeacherClassAndSection(long classID, long sectionID)
        {
            return ClassTeacherMapMapper
                .Mapper(CallContext)
                .GetStudentsByTeacherClassAndSection(classID, sectionID);
        }

        public EmployeesDTO GetStaffProfile(long employeeID)
        {
            return EmployeeRoleMapper
                .Mapper(CallContext)
                .GetStaffProfile(CallContext.LoginID.Value);
        }

        public List<KeyValueDTO> GetClassStudents(int classId, int sectionId)
        {
            var listStudents = StudentMapper.Mapper(CallContext).GetClassStudents(classId, sectionId);
            return listStudents;
        }

        public List<KeyValueDTO> GetDynamicLookUpDataForMobileApp(DynamicLookUpType lookType, string searchText)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            try
            {
                List<KeyValueDTO> values = new ReferenceDataBL(CallContext).GetDynamicLookUpDataForMobileApp(lookType, searchText, CallContext.LoginID.Value);
                return values;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year)
        {
            var calendarList = AcademicCalendarMapper.Mapper(CallContext).GetAcademicCalenderByMonthYear(month, year);
            return calendarList;
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthClassSection(int month, int year, int classId, int sectionId)
        {
            var attendanceList = StudentAttendenceMapper.Mapper(CallContext).GetStudentAttendenceByYearMonthClassSection(month, year, classId, sectionId);
            return attendanceList;
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByDayClassSection(int month, int year, int classId, int sectionId, int day)
        {
            var attendanceList = StudentAttendenceMapper.Mapper(CallContext).GetStudentAttendenceByDayClassSection(month, year, classId, sectionId, day);
            return attendanceList;
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            var attendanceList = StudentAttendenceMapper.Mapper(CallContext)
                .GetStudentAttendenceByYearMonthStudentId(month, year, studentId);
            return attendanceList;
        }

        public StudentAttendenceDTO GetStudentAttendenceCountByStudentID(int month, int year, long studentID)
        {
            var data = StudentAttendenceMapper.Mapper(CallContext)
                .GetStudentAttendenceCountByStudentID(month, year, studentID);
            return data;
        }

        public List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId)
        {
            var classStduentList = StudentMapper.Mapper(CallContext).GetClasswiseStudentData(classId, sectionId);
            return classStduentList;
        }

        public OperationResultDTO SaveStudentAttendence(StudentAttendenceInfoDTO attendenceInfo)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var attendenceDate = DateTime.ParseExact(attendenceInfo.AttendanceDateString, dateFormat, CultureInfo.InvariantCulture);

            var studentAttendence = StudentAttendenceMapper.Mapper(CallContext)
                .GetStudentAttendence(attendenceInfo.StudentID, attendenceDate);

            var attendence = new StudentAttendenceDTO()
            {
                StudentAttendenceIID = studentAttendence == null ? 0 : studentAttendence.StudentAttendenceIID,
                StudentID = attendenceInfo.StudentID,
                AttendenceDate = attendenceDate,
                PresentStatusID = attendenceInfo.PresentStatusID,
                AttendenceReasonID = attendenceInfo.AttendanceReasonID,
                Reason = attendenceInfo.Reason,
                ClassID = attendenceInfo.ClassID,
                SectionID = attendenceInfo.SectionID,
                SchoolID = studentAttendence?.SchoolID,
                AcademicYearID = studentAttendence?.AcademicYearID,
            };

            var message = new OperationResultDTO();

            var data = StudentAttendenceMapper.Mapper(CallContext).SaveStudentAttendence(attendence);

            if (data != null)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = data
                };

            }
            else
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Attendance, failed to update!"
                };
            }

            return message;
        }

        public List<LeaveRequestDTO> GetStaffLeaveApplication()
        {
            return LeaveRequestMapper
                .Mapper(CallContext)
                .GetStaffLeaveApplication(CallContext.LoginID.Value);
        }

        public List<StudentFeeDueDTO> FillFeeDue(long studentID)
        {
            var data = FeeDueGenerationMapper.Mapper(CallContext)
                .FillFeeDue(0, studentID);

            return data;
        }

        public List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext)
                .FillFeePaymentDetails(loginId);
        }

        public List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext)
                .FillPendingFees(classId, studentId);
        }

        public List<StudentFeeDueDTO> GetFeeCollected(long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext)
                .GetFeeCollected(studentId);
        }

        public List<StudentApplicationDTO> GetStudentApplication()
        {
            return StudentApplicationMapper.Mapper(CallContext).GetStudentApplicationsByLogin(CallContext.LoginID.Value);
        }

        public int GetStudentApplicationCount()
        {
            return StudentApplicationMapper.Mapper(CallContext)
                .GetStudentApplicationsByLoginCount(CallContext.LoginID.Value);
        }

        public int GetApplicationCount()
        {
            return StudentApplicationMapper.Mapper(CallContext)
                .GetApplicationsByLoginCount(CallContext.LoginID.Value);
        }

        public int GetMyLessonPlanCount()
        {
            return LessonPlanMapper.Mapper(CallContext).GetMyLessonPlanCount(CallContext.EmployeeID.Value);
        }

        public int GetMyNotificationCount()
        {
            return NotificationAlertMapper.Mapper(CallContext)
                .GetNotificationAlertsCount(CallContext.LoginID.Value);
        }

        public List<NotificationAlertsDTO> GetMyNotification(int page, int pageSize)
        {
            return NotificationAlertMapper.Mapper(CallContext)
                .GetNotificationAlerts(CallContext.LoginID.Value, page, pageSize);
        }


        public List<AssignmentDTO> GetAssignmentsForTeacher()
        {
            return AssignmentMapper
              .Mapper(CallContext)
              .GetAssignmentStaffwise(CallContext.EmployeeID.Value);
        }

        public List<MarkListViewDTO> GetMarkListForTeacher()
        {
            return MarkRegisterMapper
                .Mapper(CallContext)
                .GetMarkListTeacherwise(CallContext.EmployeeID.Value);
        }

        public List<LessonPlanDTO> GetMyLessonPlans()
        {
            return LessonPlanMapper
                .Mapper(CallContext)
                .GetMyLessonPlans(CallContext.EmployeeID.Value);
        }

        public List<TransportApplicationDTO> GetTransportApplications()
        {
            return TransportApplicationMapper.Mapper(CallContext).GetTransportApplicationsByLoginID(CallContext.LoginID.Value);
        }

        public int GetTransportApplicationCount()
        {
            return TransportApplicationMapper.Mapper(CallContext)
                .GetTransportApplicationCount(CallContext.LoginID.Value);
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStudentID(long studentID)
        {
            return TimeTableAllocationMapper
                .Mapper(CallContext)
                .GetTimeTableByStudentID(studentID);
        }

        public List<ClassSubjectMapDTO> GetSubjectsByStudentID(long studentID)
        {
            return ClassSubjectMapMapper
                .Mapper(CallContext)
                .GetSubjectsByStudentID(studentID);
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStaffLoginID(long loginID)
        {
            return TimeTableAllocationMapper
                .Mapper(CallContext)
                .GetTimeTableByStaffLoginID(loginID);
        }

        public List<Contracts.School.Transports.VehicleDTO> GetVehicleDetailsByEmployeeLoginID()
        {
            return AssignVehicleMapper
                .Mapper(CallContext)
                .GetVehicleDetailsByEmployeeLoginID(CallContext.LoginID.Value);
        }

        public List<DriverScheduleLogDTO> GetDriverScheduleListByParameters(int routeID, int vehicleID, DateTime scheduledDate)
        {
            return AssignVehicleMapper
                .Mapper(CallContext)
                .GetDriverScheduleListByParameters(routeID, vehicleID, scheduledDate);
        }

        public List<RoutesDTO> GetRoutesByVehicleID(long vehicleID)
        {
            return RouteVehicleMapMapper
                .Mapper(CallContext)
                .GetRoutesByVehicleID(vehicleID);
        }

        public List<RouteStopFeeDTO> GetRouteStopsByRouteID(long routeID)
        {
            return RouteVehicleMapMapper
                .Mapper(CallContext)
                .GetRouteStopsByRouteID(routeID);
        }

        public List<StudentRouteStopMapDTO> GetStudentsDetailsByRouteStopID(long routeStopMapID)
        {
            return StudentRouteStopMapMapper
                .Mapper(CallContext)
                .GetStudentsDetailsByRouteStopID(routeStopMapID);
        }

        public void UpdateUserGeoLocation(string geoLocation)
        {
            bool? isDriver;

            var designationData = EmployeeRoleMapper.Mapper(CallContext).GetDriverDetailsByLoginID(CallContext.LoginID.Value);

            if (designationData != null)
            {
                isDriver = true;
            }
            else
            {
                isDriver = false;
            }

            GeoLocationLogMapper.Mapper(CallContext).UpdateUserGeoLocation(geoLocation, isDriver);

            MarkStaffAttendanceByGeoLocation(geoLocation);
        }

        public List<Contracts.School.Transports.VehicleDTO> GetRouteStudentsAndStaffDetailsByEmployeeLoginID()
        {
            return DriverScheduleLogMapper
                .Mapper(CallContext)
                .GetRouteStudentsAndStaffDetailsByEmployeeLoginID(CallContext.LoginID.Value);
        }

        public List<Contracts.School.Transports.VehicleDTO> GetAllRouteStudentsAndStaffDetails()
        {
            return DriverScheduleLogMapper
                .Mapper(CallContext)
                .GetAllRouteStudentsAndStaffDetails();
        }

        public List<AssignVehicleDTO> GetVehicleAssignDetailsByEmployeeIDandRouteID(long employeeID, long routeID)
        {
            return AssignVehicleMapper
                .Mapper(CallContext)
                .GetVehicleAssignDetailsByEmployeeIDandRouteID(employeeID, routeID);
        }

        public List<PresentStatusDTO> GetPresentStatuses()
        {
            var statusList = StudentAttendenceMapper.Mapper(CallContext).GetPresentStatuses();
            return statusList;
        }

        public List<LessonPlanDTO> GetLessonPlanListByStudentID(long studentID)
        {
            return LessonPlanMapper
                .Mapper(CallContext)
                .GetLessonPlanList(studentID);
        }

        public StudentLeaveApplicationDTO GetStudentLeaveCountByStudentID(long studentID)
        {
            var data = StudentLeaveApplicationMapper.Mapper(CallContext)
                .GetStudentLeaveCountByStudentID(studentID);
            return data;
        }

        public OperationResultDTO SubmitStudentLeaveApplication(StudentLeaveApplicationDTO leaveInfo)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var studentData = StudentMapper.Mapper(CallContext).GetStudentDetailsByStudentID(Convert.ToInt64(leaveInfo.StudentID));

            var leave = new StudentLeaveApplicationDTO()
            {
                StudentLeaveApplicationIID = leaveInfo.StudentLeaveApplicationIID,
                StudentID = leaveInfo.StudentID,
                FromDate = DateTime.ParseExact(leaveInfo.FromDateString, dateFormat, CultureInfo.InvariantCulture),
                ToDate = DateTime.ParseExact(leaveInfo.ToDateString, dateFormat, CultureInfo.InvariantCulture),
                Reason = leaveInfo.Reason,
                ClassID = studentData?.ClassID,
                SchoolID = studentData?.SchoolID,
                AcademicYearID = studentData?.AcademicYearID,
                LeaveStatusID = leaveInfo.LeaveStatusID,
            };

            var message = new OperationResultDTO();

            var data = StudentLeaveApplicationMapper.Mapper(CallContext).SubmitStudentLeaveApplication(leave);

            if (data != null)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = data
                };

            }
            else
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Failed to save!"
                };
            }

            return message;
        }

        public void DeleteStudentLeaveApplicationByID(long leaveApplicationID)
        {
            StudentLeaveApplicationMapper.Mapper(CallContext)
                .DeleteLeaveApplication(leaveApplicationID);
        }

        public StudentLeaveApplicationDTO GetStudentLeaveApplicationByID(long leaveApplicationID)
        {
            var data = StudentLeaveApplicationMapper.Mapper(CallContext)
                .GetLeaveApplication(leaveApplicationID);
            return data;
        }

        public string SaveStudentAndStaffScheduleLogs(DriverScheduleLogDTO scheduleLogData)
        {
            var data = DriverScheduleLogMapper.Mapper(CallContext).SubmitScheduleLogs(scheduleLogData);

            return data;
        }
        public long SyncDriverScheduleLogs(DriverScheduleLogDTO scheduleLogData)
        {
            var data = DriverScheduleLogMapper.Mapper(CallContext).SyncDriverScheduleLogs(scheduleLogData);

            return data;
        }

        public void MarkStaffAttendanceByGeoLocation(string geoLocation)
        {
            var loginID = CallContext.LoginID.Value;

            //Get the employee school
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);

            if (empDetails.BranchID.HasValue)
            {
                long schoolID = Convert.ToInt64(empDetails.BranchID);
                //Get school location
                var schoolGeoLocationLogs = SchoolGeoLocationLogMapper.Mapper(CallContext).GetGeoSchoolSettingBySchoolID(schoolID);

                //Get employee latest geo location
                var employeeGeoLocation = GeoLocationLogMapper.Mapper(CallContext).GetLastGeoLocationByLoginID(loginID);

                if (schoolGeoLocationLogs.Count > 0)
                {
                    if (employeeGeoLocation != null)
                    {
                        //Check the employee is inside the school
                        var isEmployeeInside = IsPointInPolygon(employeeGeoLocation, schoolGeoLocationLogs);

                        if (isEmployeeInside == true)
                        {
                            //mark the attendance
                            StaffAttendenceMapper.Mapper(CallContext).MarkStaffAttendanceByEmployeeID(empDetails.EmployeeIID, empDetails.BranchID, loginID);
                        }
                    }
                }
            }
        }

        public bool IsPointInPolygon(GeoLocationLog employeeGeoLocation, List<SchoolGeoLocationDTO> schoolGeoLocationLogs)
        {
            bool inside = false;
            List<GeoLocationLog> schoolGeoLocationLogPolygon = new List<GeoLocationLog>();

            double employeeLastLatitude = double.Parse(employeeGeoLocation.Latitude);
            double employeeLastLongitude = double.Parse(employeeGeoLocation.Longitude);

            foreach (var data in schoolGeoLocationLogs)
            {
                schoolGeoLocationLogPolygon.Add(new GeoLocationLog()
                {
                    Latitude = data.Latitude,
                    Longitude = data.Longitude
                });
            }
            if (schoolGeoLocationLogPolygon.Count > 0)
            {
                double minX = double.Parse(schoolGeoLocationLogPolygon[0].Latitude);
                double maxX = double.Parse(schoolGeoLocationLogPolygon[0].Latitude);
                double minY = double.Parse(schoolGeoLocationLogPolygon[0].Longitude);
                double maxY = double.Parse(schoolGeoLocationLogPolygon[0].Longitude);

                for (int i = 1; i < schoolGeoLocationLogPolygon.Count; i++)
                {
                    GeoLocationLog q = schoolGeoLocationLogPolygon[i];
                    minX = Math.Min(double.Parse(q.Latitude), minX);
                    maxX = Math.Max(double.Parse(q.Latitude), maxX);
                    minY = Math.Min(double.Parse(q.Longitude), minY);
                    maxY = Math.Max(double.Parse(q.Longitude), maxY);
                }
                if (employeeLastLatitude < minX || employeeLastLatitude > maxX || employeeLastLongitude < minY || employeeLastLongitude > maxY)
                {
                    inside = false;
                }

                inside = false;
                for (int i = 0, j = schoolGeoLocationLogPolygon.Count - 1; i < schoolGeoLocationLogPolygon.Count; j = i++)
                {
                    if ((double.Parse(schoolGeoLocationLogPolygon[i].Longitude) > employeeLastLongitude) != (double.Parse(schoolGeoLocationLogPolygon[j].Longitude) > employeeLastLongitude) &&
                         employeeLastLatitude < (double.Parse(schoolGeoLocationLogPolygon[j].Latitude) - double.Parse(schoolGeoLocationLogPolygon[i].Latitude)) * (employeeLastLatitude - double.Parse(schoolGeoLocationLogPolygon[i].Longitude)) / (double.Parse(schoolGeoLocationLogPolygon[j].Longitude) - double.Parse(schoolGeoLocationLogPolygon[i].Longitude)) + double.Parse(schoolGeoLocationLogPolygon[i].Latitude))
                    {
                        inside = true;
                    }
                }
            }
            return inside;
        }

        //public List<DriverScheduleLogDTO> GetScheduleLogs(string scheduleType, string passengerType, long vehicleID, long routeStopMapID, long routeID)
        //{
        //    return DriverScheduleLogMapper
        //        .Mapper(CallContext)
        //        .GetScheduleLogs(scheduleType, passengerType, vehicleID, routeStopMapID, routeID);
        //}

        public List<DriverScheduleLogDTO> GetScheduleLogsByRoute(string scheduleType, string passengerType, long vehicleID, long routeID)
        {
            return DriverScheduleLogMapper
                .Mapper(CallContext)
                .GetScheduleLogsByRoute(scheduleType, passengerType, vehicleID, routeID);
        }

        public DriverScheduleLogDTO GetStudentStaffDropScheduleDatasforDropOut(string passengerType, long vehicleID, long routeID)
        {
            return DriverScheduleLogMapper
                .Mapper(CallContext)
                .GetStudentStaffDropScheduleDatasforDropOut(passengerType, vehicleID, routeID);
        }

        //public DriverScheduleLogDTO GetStaffDropScheduleDatasByRoute(long vehicleID, long routeID)
        //{
        //    return DriverScheduleLogMapper
        //        .Mapper(CallContext)
        //        .GetStaffDropScheduleDatasByRoute(vehicleID, routeID);
        //}

        public List<DriverScheduleLogDTO> GetScheduleLogsByDateForOfflineDB()
        {
            return DriverScheduleLogMapper
                .Mapper(CallContext)
                .GetScheduleLogsByDateForOfflineDB(CallContext.LoginID.Value);
        }

        public List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year)
        {
            return StaffAttendenceMapper
                .Mapper(CallContext)
                .GetStaffAttendanceByMonthYear(month, year);
        }

        public List<StaffAttendenceDTO> GetStaffAttendenceByYearMonthEmployeeID(int month, int year)
        {
            var attendanceList = StaffAttendenceMapper.Mapper(CallContext)
                .GetStaffAttendenceByYearMonthEmployeeID(month, year);
            return attendanceList;
        }

        public StaffAttendenceDTO GetStaffAttendenceCountByEmployeeID(int month, int year, long employeeID)
        {
            var data = StaffAttendenceMapper.Mapper(CallContext)
                .GetStaffAttendenceCountByEmployeeID(month, year, employeeID);
            return data;
        }

        public List<KeyValueDTO> GetSchoolsByParent()
        {
            long? loginID = CallContext.LoginID;

            var schoolLists = SchoolsMapper.Mapper(CallContext).GetSchoolsByParentLoginID(loginID);

            return schoolLists;
        }

        public List<KeyValueDTO> GetAcademicYearBySchool(int schoolID)
        {
            var academicYearList = AcademicYearMapper.Mapper(CallContext).GetAllAcademicYearBySchoolID(schoolID);

            return academicYearList;
        }

        public List<FeePaymentHistoryDTO> GetFeeCollectionHistories(byte schoolID, int academicYearID)
        {
            return new FeePaymentBL(CallContext).GetFeeCollectionHistoriesByFilter(schoolID, academicYearID);
        }

        public List<FeePaymentDTO> GetStudentsFeePaymentDetails()
        {
            var parentLoginID = CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0;

            var feePaymentDTO = FeeCollectionMapper.Mapper(CallContext).FillStudentsFeePaymentDetailsByParentLogin(parentLoginID);

            return feePaymentDTO;
        }

        public FeePaymentDTO GetStudentFeeDetails(long studentID)
        {
            var feeDetails = FeeCollectionMapper.Mapper(CallContext).GetStudentFeeDetails(studentID);

            return feeDetails;
        }

        public string SubmitAmountAsLog(decimal totalAmount)
        {
            return PaymentLogMapper.Mapper(CallContext).SubmitAmountAsLog(totalAmount);
        }

        public string GenerateCardSession(byte? paymentModeID = null)
        {
            return new PaymentGatewayBL(CallContext).GenerateCardSessionID(paymentModeID);
        }

        public OperationResultDTO InitiateFeeCollections(List<FeeCollectionDTO> feeCollections)
        {
            var result = new OperationResultDTO();

            var studentIds = feeCollections.Select(x => x.StudentID.Value).ToList();
            var hasParentAccess = new SchoolSecurityBL(CallContext).CheckParentAccess(CallContext.LoginID.Value, studentIds);
            if (hasParentAccess)
            {
                result = FeeCollectionMapper.Mapper(CallContext).FeeCollectionEntry(feeCollections);
            }
            else
            {
                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Something went wrong, try again later!"
                };
            }

            return result;
        }

        public string UpdateFeePaymentStatus(string transactionNumber, byte? paymentModeID = null)
        {
            if (string.IsNullOrEmpty(transactionNumber) || transactionNumber == "null")
            {
                var masterVisaData = PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaData();

                transactionNumber = masterVisaData?.TransID;
            }
            else
            {
                if (paymentModeID.HasValue && paymentModeID == (byte)PaymentModes.QPAY)
                {
                    var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");

                    transactionNumber = transactionNumber.Replace(transactionPrefix, "");
                }
                else
                {
                    var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                    transactionNumber = transactionNumber.Replace(transactionPrefix, "");
                }
            }
            string paymentStatus;

            var parentLoginID = CallContext.LoginID;

            var paymentUpdateDatas = FeeCollectionMapper.Mapper(CallContext).UpdateStudentsFeePaymentStatus(transactionNumber, parentLoginID);

            if (paymentUpdateDatas != null)
            {
                paymentStatus = "Successfully Updated";

                //Send Mail
                var reportgenBL = new ReportGenerationBL(CallContext);

                reportgenBL.GenerateFeeReceiptAndSendToMail(paymentUpdateDatas, EmailTypes.AutoFeeReceipt);
            }
            else
            {
                paymentStatus = null;
            }

            return paymentStatus;
        }

        public string GetCardSession()
        {
            string session = string.Empty;
            var data = PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaData();

            if (data != null)
            {
                if (data.Response != null)
                {
                    var nvc = HttpUtility.ParseQueryString(data.Response);

                    if (nvc["result"] == "SUCCESS")
                    {
                        session = nvc["session.id"];
                    }
                }
            }
            return session;
        }

        public PaymentMasterVisaDTO GetCardPaymentGatewayDatas(byte? paymentMethodID = null)
        {
            var masterVisaDTO = new PaymentMasterVisaDTO();

            var settingBL = new Domain.Setting.SettingBL(CallContext);
            string defaultCurrency, merchantID, merchantName, orderDescription, merchantCheckoutJSLink, merchantLogoURL, actionID, merchantCardURL;

            if (paymentMethodID == (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
            {
                defaultCurrency = settingBL.GetSettingValue<string>("QPAY_DEFAULT_CURRENCY_CODE");

                merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");
                merchantName = settingBL.GetSettingValue<string>("QPAY-MERCHANTNAME");

                orderDescription = settingBL.GetSettingValue<string>("QPAYDESCRIPTION");

                merchantCheckoutJSLink = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAYCHECKOUTJSLINK");

                merchantLogoURL = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAYLOGOURL");

                actionID = settingBL.GetSettingValue<string>("QPAY-ACTIONID");

                merchantCardURL = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");
            }
            else
            {
                defaultCurrency = settingBL.GetSettingValue<string>("DEFAULT_CURRENCY_CODE");

                merchantID = settingBL.GetSettingValue<string>("MERCHANTID");

                merchantName = settingBL.GetSettingValue<string>("MERCHANTNAME");

                orderDescription = settingBL.GetSettingValue<string>("ONLINEPAYMENTDESCRIPTION");

                merchantCheckoutJSLink = settingBL.GetSettingValue<string>("MERCHANTGATEWAYCHECKOUTJSLINK");

                merchantLogoURL = settingBL.GetSettingValue<string>("MERCHANTGATEWAYLOGOURL");

                merchantCardURL = settingBL.GetSettingValue<string>("MERCHANTCARDURL");
            }

            var data = PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaData();
            if (paymentMethodID != (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
            {
                if (data != null)
                {
                    if (data.Response != null)
                    {
                        var nvc = HttpUtility.ParseQueryString(data.Response);

                        if (nvc["result"] == "SUCCESS")
                        {
                            var session = nvc["session.id"];

                            masterVisaDTO = new PaymentMasterVisaDTO()
                            {
                                TrackIID = data.TrackIID,
                                CustomerID = data.CustomerID,
                                LoginID = data.LoginID,
                                PaymentAmount = data.PaymentAmount,
                                TransID = data.TransID,
                                LogType = data.LogType,
                                BankSession = session,
                                PaymentCurrency = defaultCurrency,
                                MerchantID = merchantID,
                                MerchantName = merchantName,
                                OrderDescription = orderDescription,
                                MerchantCheckoutJS = merchantCheckoutJSLink,
                                MerchantLogoURL = merchantLogoURL,
                                MerchantCardURL = merchantCardURL,
                                SuccessStatus = data.SuccessStatus,
                            };
                        }
                    }
                }
            }
            else
            {
                masterVisaDTO = new PaymentMasterVisaDTO()
                {
                    TrackIID = data.TrackIID,
                    CustomerID = data.CustomerID,
                    LoginID = data.LoginID,
                    PaymentAmount = data.PaymentAmount,
                    TransID = data.TransID,
                    LogType = data.LogType,
                    BankSession = null,
                    PaymentCurrency = defaultCurrency,
                    MerchantID = merchantID,
                    MerchantName = merchantName,
                    OrderDescription = orderDescription,
                    MerchantCheckoutJS = merchantCheckoutJSLink,
                    MerchantLogoURL = merchantLogoURL,
                    MerchantCardURL = merchantCardURL,
                    CardTypeID = 2,
                    CardType = "QPAY",
                    SuccessStatus = data.SuccessStatus,
                };
            }
            return masterVisaDTO;
        }

        public string ValidatePayment()
        {
            return new PaymentGatewayBL(CallContext).PaymentValidation();
        }

        public OperationResultDTO ResendReceiptMail(string transactionNumber, string mailID, string feeReceiptNo)
        {
            var message = new OperationResultDTO();

            try
            {
                var feeCollectionDatas = FeeCollectionMapper.Mapper(CallContext).GetFeeCollectionDetailsByTransactionNumber(transactionNumber, mailID, feeReceiptNo);

                if (feeCollectionDatas != null && feeCollectionDatas.Count > 0)
                {
                    var reportgenBL = new ReportGenerationBL(CallContext);

                    reportgenBL.GenerateFeeReceiptAndSendToMail(feeCollectionDatas, EmailTypes.ResendFeeReceipt);

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Successfully Sent Mail!"
                    };
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return message;
        }

        public OperationResultDTO CheckFeeCollectionExistingStatus(string transactionNumber)
        {
            var message = new OperationResultDTO();
            try
            {
                var feeCollectionStatus = FeeCollectionMapper.Mapper(CallContext).CheckFeeCollectionStatusByTransactionNumber(transactionNumber);

                if (feeCollectionStatus == null)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = ""
                    };
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = feeCollectionStatus
                    };
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return message;

        }

        //To retry payment
        public OperationResultDTO RetryPayment(string transactionNumber, byte? paymentModeID = null)
        {
            var message = new OperationResultDTO();
            try
            {
                var data = new PaymentGatewayBL(CallContext).GenerateCardSessionIDByTransactionNo(transactionNumber, paymentModeID);

                if (data != null)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = data
                    };
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return message;
        }

        public List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber)
        {
            var feeCollectionDetails = FeeCollectionMapper.Mapper(CallContext).FillCollectionDetailsByTransactionNumber(transactionNumber);

            return feeCollectionDetails;
        }

        public bool ValidateCartPayment(long cartID, string returnIndicator, byte? paymentMethodID = null, long? totalCartAmount = null)
        {
            try
            {
                Eduegate.Logger.LogHelper<string>.Fatal("ValidateCartPayment cartID:" + cartID, new Exception("ValidateCartPayment(long cartID, string returnIndicator)"));
                return new PaymentGatewayBL(CallContext).PaymentValidationByCartID(cartID, paymentMethodID, totalCartAmount);
                //return ValidateMasterCardSuccessResponse(cartID, returnIndicator);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal("ValidateCartPayment :" + errorMessage, ex);
                return false;
            }
        }

        public PaymentLogDTO QPayInquiry(long? cartID = 0)
        {
            return new PaymentGatewayBL(CallContext).QPayInquiry();
        }

        public bool ValidateMasterCardSuccessResponse(long cartID, string returnIndicator)
        {
            try
            {
                var paymentDetails = PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaData();

                if (paymentDetails != null)
                {
                    if (paymentDetails.CartID == cartID)
                    {
                        returnIndicator = new PaymentGatewayBL(CallContext).PaymentValidation();

                        if (!string.IsNullOrEmpty(returnIndicator))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public PaymentMasterVisaDTO GetPaymentSession(long shoppingCartID, decimal totalAmount, byte? paymentMethodID = null)
        {
            SubmitAmountAsLog(totalAmount);
            var masterVisaDTO = new PaymentMasterVisaDTO();


            var generateCartSessionID = new PaymentGatewayBL(CallContext).GenerateCardSessionIDByCart(shoppingCartID, totalAmount, paymentMethodID);

            if (generateCartSessionID != null)
            {
                masterVisaDTO = GetCardPaymentGatewayDatas(paymentMethodID);
            }

            return masterVisaDTO;
        }

        public decimal GetPickupRequestsCount()
        {
            return StudentPickupRequestMapper
            .Mapper(CallContext)
            .GetPickupRequestsCount(CallContext.LoginID.Value);
        }

        public List<StudentPickupRequestDTO> GetStudentPickupRequests()
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetStudentPickupRequestsByLoginID(CallContext.LoginID.Value);
        }

        public GuardianDTO GetGuardianDetails()
        {
            long studentId = 0;

            return StudentMapper.Mapper(CallContext).GetGuardianDetails(studentId);
        }

        public OperationResultDTO CancelStudentPickupRequestByID(long pickupRequestID)
        {
            var message = new OperationResultDTO();
            try
            {
                var data = StudentPickupRequestMapper.Mapper(CallContext).CancelStudentPickupRequestByID(pickupRequestID);

                if (data != null)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = data
                    };
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return message;
        }

        public OperationResultDTO SubmitStudentPickupRequest(StudentPickupRequestDTO studentPickupRequest)
        {
            _ = new OperationResultDTO();
            OperationResultDTO result;

            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                var pickupRequest = new StudentPickupRequestDTO()
                {
                    StudentPickupRequestIID = studentPickupRequest.StudentPickupRequestIID,
                    StudentID = studentPickupRequest.StudentID,
                    PickedByID = studentPickupRequest.PickedByID,
                    FirstName = studentPickupRequest.FirstName,
                    MiddleName = studentPickupRequest.MiddleName,
                    LastName = studentPickupRequest.LastName,
                    RequestDate = string.IsNullOrEmpty(studentPickupRequest.RequestStringDate) ? (DateTime?)null : DateTime.ParseExact(studentPickupRequest.RequestStringDate, dateFormat, CultureInfo.InvariantCulture),
                    PickedDate = string.IsNullOrEmpty(studentPickupRequest.PickStringDate) ? (DateTime?)null : DateTime.ParseExact(studentPickupRequest.PickStringDate, dateFormat, CultureInfo.InvariantCulture),
                    FromTime = string.IsNullOrEmpty(studentPickupRequest.FromStringTime) ? (TimeSpan?)null : DateTime.Parse(studentPickupRequest.FromStringTime).TimeOfDay,
                    ToTime = string.IsNullOrEmpty(studentPickupRequest.ToStringTime) ? (TimeSpan?)null : DateTime.Parse(studentPickupRequest.ToStringTime).TimeOfDay,
                    AdditionalInfo = studentPickupRequest.AdditionalInfo,
                    RequestStatusID = studentPickupRequest.RequestStatusID,
                };

                result = StudentPickupRequestMapper.Mapper(CallContext).SubmitStudentPickupRequest(pickupRequest);

            }
            catch (Exception ex)
            {
                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return result;
        }

        #region Student Daily PickUp Register
        public OperationResultDTO SubmitStudentDailyPickupRequest(StudentPickupRequestDTO register)
        {
            _ = new OperationResultDTO();
            OperationResultDTO result;

            try
            {
                result = StudentPickupRequestMapper.Mapper(CallContext).SubmitStudentDailyPickupRequest(register);
            }
            catch (Exception ex)
            {
                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return result;
        }

        public List<StudentPickupRequestDTO> GetRegisteredPickupRequests(long loginID, string barCodeValue)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetRegisteredPickupRequestsByLoginID(loginID, barCodeValue);
        }


        public OperationResultDTO CancelorActiveStudentPickupRegistration(long studentPickerStudentMapIID)
        {
            var message = new OperationResultDTO();
            try
            {
                var data = StudentPickupRequestMapper.Mapper(CallContext).CancelorActiveStudentPickupRegistration(studentPickerStudentMapIID);

                if (data != null)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = data
                    };
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return message;
        }

        public decimal GetPickupRegisterCount()
        {

            return StudentPickupRequestMapper
            .Mapper(CallContext)
            .GetPickupRegisterCount(CallContext.LoginID.Value);
        }

        public StudentPickupRequestDTO GePickupRegisteredDetailsByQR(string qrCode)
        {
            var scanByQR = Regex.Replace(qrCode, @"\s+", "+");
            return StudentPickupRequestMapper.Mapper(CallContext).GePickupRegisteredDetailsByQR(scanByQR);
        }

        public OperationResultDTO SubmitStudentPickLogs(StudentPickupRequestDTO submitLog)
        {
            _ = new OperationResultDTO();
            OperationResultDTO result;

            try
            {
                result = StudentPickupRequestMapper.Mapper(CallContext).SubmitStudentPickLogs(submitLog);
            }
            catch (Exception ex)
            {
                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return result;
        }

        public List<StudentPickupRequestDTO> GetTodayStudentPickLogs()
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetTodayStudentPickLogs();
        }

        #endregion

        public List<KeyValueDTO> GetAllergies()
        {
            return new AppDataBL(this.CallContext).GetAllergies();
        }

        public OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID)
        {
            return new AppDataBL(this.CallContext).SaveAllergies(studentID, allergyID, SeverityID);
        }

        public List<AllergyStudentDTO> GetStudentAllergies()
        {
            return new AppDataBL(this.CallContext).GetStudentAllergies();
        }

        public List<KeyValueDTO> GetSeverity()
        {
            return new AppDataBL(this.CallContext).GetSeverity();
        }

        public List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID)
        {
            return new AppDataBL(this.CallContext).CheckStudentAllergy(studentID, cartID);
        }

        public OperationResultDTO SaveDefaultStudent(long studentID)
        {
            return new AppDataBL(this.CallContext).SaveDefaultStudent(studentID);
        }

        public CustomerDTO GetDefaultStudent()
        {
            return new AppDataBL(this.CallContext).GetDefaultStudent();
        }

        public OperationResultDTO SubmitStaffLeaveApplication(LeaveRequestDTO leaveInfo)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //var studentData = StudentMapper.Mapper(CallContext).GetStudentDetailsByStudentID(Convert.ToInt64(leaveInfo.EmployeeID));

            var leave = new LeaveRequestDTO()
            {

                LeaveApplicationIID = leaveInfo.LeaveApplicationIID,
                EmployeeID = leaveInfo.EmployeeID,
                LeaveTypeID = leaveInfo.LeaveTypeID,
                FromDate = DateTime.ParseExact(leaveInfo.FromDateString, dateFormat, CultureInfo.InvariantCulture),
                ToDate = DateTime.ParseExact(leaveInfo.ToDateString, dateFormat, CultureInfo.InvariantCulture),
                OtherReason = leaveInfo.OtherReason,
                LeaveStatusID = leaveInfo.LeaveStatusID,
            };

            var message = new OperationResultDTO();

            var data = LeaveRequestMapper.Mapper(CallContext).SubmitStaffLeaveApplication(leave);

            if (data != null)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = data
                };

            }
            else
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Failed to save!"
                };
            }

            return message;
        }

        public List<KeyValueDTO> GetLeaveTypes()
        {
            return new AppDataBL(this.CallContext).GetLeaveTypes();
        }

        public StaticContentDataDTO GetStaticPage(long contentID)
        {
            return StaticContentDataMapper.Mapper(CallContext).GetStaticPage(contentID);
        }

        public VisitorDTO GetVisitorDetails(string QID, string passportNumber)
        {
            var visitorData = VisitorMapper.Mapper(CallContext).GetVisitorDetails(QID, passportNumber);

            return visitorData;
        }

        public OperationResultDTO VisitorRegistration(VisitorDTO visitorDetails)
        {
            var result = VisitorMapper.Mapper(CallContext).RegisterVisitorDetails(visitorDetails);

            return result;
        }

        public VisitorDTO GetVisitorDetailsByLoginID(long loginID)
        {
            var visitorData = VisitorMapper.Mapper(CallContext).GetVisitorDetailsByLoginID(loginID);

            return visitorData;
        }

        public string UpdateStudentPickLogStatus(long picklogID)
        {
            var result = StudentPickupRequestMapper.Mapper(CallContext).UpdateStudentPickLogStatus(picklogID);

            return result;
        }


        public VisitorDTO GetVisitorDetailsByVisitorCode(string visitorCode)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetVisitorDetailsByVisitorCode(visitorCode);
        }

        public long UploadContentAsString(Services.Contracts.Contents.ContentFileDTO content)
        {
            using (var memoryStream = new MemoryStream())
            {
                var savedContent = new ContentBL(CallContext).SaveFile(new Services.Contracts.Contents.ContentFileDTO()
                {
                    ContentFileName = content.ContentFileName,
                    ContentData = Convert.FromBase64String(content.ContentDataString),
                });
                return savedContent.ContentFileIID;
            }
        }
        public List<StudentPickupRequestDTO> GetAndUpdateActivePickLogsForInspection()
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetAndUpdateActivePickLogsForInspection();
        }

        public string GetTodayInspectionColour()
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetTodayInspectionColour();
        }


        public StaffAttendenceDTO GetTodayStaffAttendanceByLoginID()
        {
            var data = StaffAttendenceMapper.Mapper(CallContext)
                .GetTodayStaffAttendanceByLoginID();
            return data;
        }

        public List<KeyValueDTO> GetOrderTypes()
        {
            return new AppDataBL(this.CallContext).GetOrderTypes();
        }

        public List<StudentDTO> GetMyStudentsBySchool()
        {
            return new AppDataBL(this.CallContext).GetMyStudentsBySchool();
        }

        public GuardianDTO GetParentDetailsByParentCode(string parentCode)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetParentDetailsByParentCode(parentCode);
        }

        public PaymentMasterVisaDTO GetCardPaymentGatewayDatas()
        {
            throw new NotImplementedException();
        }

        public PaymentMasterVisaDTO GetPaymentSession(long shoppingCartID, decimal totalAmount)
        {
            throw new NotImplementedException();
        }
        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByTrackID(long trackID)
        {
            return PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaDataByTrackID(trackID);
        }

        public string GetParentPortalUrl()
        {
            return new AppDataBL(this.CallContext).GetParentPortalUrl();
        }

        public List<CircularListDTO> GetCircularsByEmployee()
        {
            return CircularMapper
            .Mapper(CallContext)
            .GetCircularsByEmployee(CallContext.LoginID.Value);
        }

        public string MarkNotificationAsRead(long notificationAlertID)
        {
            return NotificationAlertMapper.Mapper(CallContext).MarkNotificationAsRead(CallContext.LoginID.Value, notificationAlertID);
        }

        public AppVersionDTO CheckAppVersion(string currentAppVersion, string appID)
        {
            var appVersion = new AppVersionDTO() { IsUpdated = true };
            if (!string.IsNullOrEmpty(currentAppVersion))
            {
                var latestAppVersion = string.IsNullOrEmpty(appID) ? new SettingBL(null).GetSettingValue<string>("LATEST_APP_VERSION")
                    : new SettingBL(null).GetSettingValue<string>(string.Concat("LATEST_APP_VERSION_", appID));
                var currentVersion = Version.Parse(currentAppVersion);

                if (!string.IsNullOrEmpty(latestAppVersion))
                {
                    var latestVersion = Version.Parse(latestAppVersion);

                    var result = currentVersion.CompareTo(latestVersion);
                    if (result < 0)
                    {
                        appVersion.IsUpdated = false;

                        if (currentVersion.Major < latestVersion.Major)
                        {
                            appVersion.IsMajor = true;
                        }
                    }

                }

                var forceUpdateVersion = new SettingBL(null).GetSettingValue<string>("FORCE_UPDATE_MIN_VERSION_" + appID);

                if (!string.IsNullOrEmpty(forceUpdateVersion))
                {
                    var forceUpdate = Version.Parse(forceUpdateVersion);
                    var forceResult = currentVersion.CompareTo(forceUpdate);

                    if (forceResult < 0)
                    {
                        appVersion.IsUpdated = false;
                        appVersion.IsMajor = true;
                    }
                }
            }

            appVersion.RedirectUrl = new SettingBL(null).GetSettingValue<string>("REDIRECT_URL_FOR_APP_UPDATE");

            appVersion.UpdateMessage = string.IsNullOrEmpty(appID) ? new SettingBL(null).GetSettingValue<string>("APP_UPDATE_MESSAGE")
                   : new SettingBL(null).GetSettingValue<string>(string.Concat("APP_UPDATE_MESSAGE_", appID), null);

            appVersion.CustomImageURL = string.IsNullOrEmpty(appID) ? new SettingBL(null).GetSettingValue<string>("APP_UPDATE_CUSTOM_IMAGE_URL")
                   : new SettingBL(null).GetSettingValue<string>(string.Concat("APP_UPDATE_CUSTOM_IMAGE_URL_", appID), null);

            return appVersion;
        }

        public List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID)
        {
            return StudentMapper.Mapper(CallContext).GetAcademicYearByProgressReport(studentID);
        }

        public List<ProgressReportNewDTO> GetReportCardList(long studentID, int classID, int sectionID, int academicYearID)
        {
            return ProgressReportMapper.Mapper(CallContext).GetStudentProgressReportData(studentID, classID, sectionID, academicYearID);
        }

        public List<FeePaymentHistoryDTO> GetLastTenFeeCollectionHistories()
        {
            return new FeePaymentBL(CallContext).GetLastTenFeeCollectionHistories();
        }

        public OperationResultDTO CheckTransactionPaymentStatus(string transactionNumber, byte? paymentModeID = null)
        {
            var message = new OperationResultDTO();
            try
            {
                if (paymentModeID.HasValue && paymentModeID == (byte)PaymentModes.QPAY)
                {
                    var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");

                    transactionNumber = transactionNumber.Replace(transactionPrefix, "");
                }
                else
                {
                    var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                    transactionNumber = transactionNumber.Replace(transactionPrefix, "");
                }

                var result = new PaymentGatewayBL(CallContext).ValidatePaymentByTransaction(transactionNumber, paymentModeID);

                if (!string.IsNullOrEmpty(result))
                {
                    if (result.ToLower() == "success")
                    {
                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Success,
                            Message = result
                        };
                    }
                    else
                    {
                        message = new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = result
                        };
                    }
                }
                else
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Not recived payment"
                    };
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };
            }

            return message;
        }

        public StudentDTO GetStudentDetailsByAdmissionNumber(string admissionNumber)
        {
            return StudentMapper
                .Mapper(CallContext)
                .GetStudentDetailsByAdmissionNumber(admissionNumber);
        }

        public decimal GetLatestStaffCircularCount()
        {
            return CircularMapper
            .Mapper(CallContext)
            .GetLatestStaffCircularCount(CallContext.LoginID.Value);
        }

        public string GetDriverGeoLocationByStudent(long studentID)
        {
            var data = GeoLocationLogMapper
            .Mapper(CallContext)
            .GetDriverGeoLocationByStudent(studentID);

            // return string.Join(",", data.Select(x => new {x.Latitude,x.Longitude}));
            if (data.Count > 0)
            {
                var d = data.Select(x => new { x.Latitude, x.Longitude }).FirstOrDefault();
                return string.Join(",", d.Latitude, d.Longitude);
            }
            else
            {
                return null;
            }
        }

        public EmployeesDTO GetDriverDetailsByStudent(long studentID)
        {
            var data = AssignVehicleMapper
            .Mapper(CallContext)
            .GetDriverDetailsByStudent(studentID);

            return data;
        }


        public List<RouteStopFeeDTO> GetStopsPositionsByRoute(long studentID)
        {
            var data = AssignVehicleMapper
            .Mapper(CallContext)
            .GetStopsPositionsByRoute(studentID);

            return data;
        }

        public bool GetStudentInOutVehicleStatus(long studentID)
        {
            //bool? isStudentIn;

            var isStudentIn = AssignVehicleMapper
               .Mapper(CallContext)
               .GetStudentInOutVehicleStatus(studentID);

            return isStudentIn;
        }
        public List<TicketDTO> GetAllTicketsByLoginID()
        {
            var result = TicketingMapper
            .Mapper(CallContext)
            .GetAllTicketsByLoginID(CallContext.LoginID.Value);

            return result;
        }

        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO ticketCommunicationDTO)
        {
            return TicketingMapper.Mapper(CallContext).SaveTicketCommunication(ticketCommunicationDTO);
        }

        public List<SignUpGroupDTO> GetActiveSignupGroups()
        {
            var result = new SignUpBL(CallContext).GetActiveSignupGroups();

            return result;
        }

        public SignUpGroupDTO FillSignUpDetailsByGroupID(int groupID)
        {
            var result = new SignUpBL(CallContext).FillSignUpDetailsByGroupID(groupID);

            return result;
        }

        public OperationResultDTO SaveSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            var result = new SignUpBL(CallContext).SaveSelectedSignUpSlot(signUpSlotMap);

            return result;
        }

        public OperationResultDTO CancelSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            var result = new SignUpBL(CallContext).CancelSelectedSignUpSlot(signUpSlotMap);

            return result;
        }

        public List<GeoLocationLogDTO> GetDriverGeoLocationLogByStudent(long studentID)
        {
            var data = GeoLocationLogMapper
            .Mapper(CallContext)
            .GetDriverGeoLocationLogByStudent(studentID);

            return data;
        }

        public string SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            return StudentAttendenceMapper.Mapper(CallContext).SendAttendanceNotificationsToParents(classId, sectionId);
        }

        public OperationResultDTO SubmitDriverVehicleTracking(DriverVehicleTrackingDTO trackingInfo)
        {
            return VehicleMapper.Mapper(CallContext).SubmitDriverVehicleTracking(trackingInfo);
        }

        public List<KeyValueDTO> GetVehicles()
        {
            return new AppDataBL(this.CallContext).GetVehicles(CallContext.LoginID.Value);
        }

        public void MarkDriverAttendanceOnPickupStart()
        {
            var loginID = CallContext.LoginID.Value;
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);

            if (empDetails.BranchID.HasValue)
            {
                long schoolID = Convert.ToInt64(empDetails.BranchID);
                StaffAttendenceMapper.Mapper(CallContext).MarkStaffAttendanceByEmployeeID(empDetails.EmployeeIID, empDetails.BranchID, loginID);

            }
        }

        public string GetDriverGeoLocationByStaff()
        {
            var loginID = CallContext.LoginID.Value;
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);
            var data = GeoLocationLogMapper
            .Mapper(CallContext)
            .GetDriverGeoLocationByStaff(empDetails.EmployeeIID);

            // return string.Join(",", data.Select(x => new {x.Latitude,x.Longitude}));
            if (data.Count > 0)
            {
                var d = data.Select(x => new { x.Latitude, x.Longitude }).FirstOrDefault();
                return string.Join(",", d.Latitude, d.Longitude);
            }
            else
            {
                return null;
            }
        }
        public List<RouteStopFeeDTO> GetStopsPositionsByStaff()
        {
            var loginID = CallContext.LoginID.Value;
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);
            var data = AssignVehicleMapper
            .Mapper(CallContext)
            .GetStopsPositionsByRouteStaff(empDetails.EmployeeIID);

            return data;
        }

        public bool GetStaffInOutVehicleStatus()
        {
            //bool? isStudentIn;
            var loginID = CallContext.LoginID.Value;
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);
            var isStudentIn = AssignVehicleMapper
               .Mapper(CallContext)
               .GetStaffInOutVehicleStatus(empDetails.EmployeeIID);

            return isStudentIn;
        }

        public EmployeesDTO GetDriverDetailsByStaff()
        {
            var loginID = CallContext.LoginID.Value;
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);
            var data = AssignVehicleMapper
            .Mapper(CallContext)
            .GetDriverDetailsByStaff(empDetails.EmployeeIID);

            return data;
        }

        public SalarySlipDTO GetSalarySlipList(int Month, int Year)
        {
            var loginID = CallContext.LoginID.Value;
            var empDetails = EmployeeRoleMapper.Mapper(CallContext).GetStaffDetailsByLoginID(loginID);
            return SalarySlipMapper.Mapper(CallContext).GetSalarySlipList(empDetails.EmployeeIID, Month, Year);
        }

        public List<MeetingRequestDTO> GetMeetingRequestsByParentID()
        {
            var parentID = StudentMapper.Mapper(CallContext).GetParentID(CallContext.LoginID.Value);

            var result = new SignUpBL(CallContext).GetMeetingRequestsByParentID(parentID);

            return result;
        }

        public KeyValueDTO GetSchoolPrincipal(byte schoolID)
        {
            return EmployeeRoleMapper.Mapper(CallContext).GetSchoolPrincipal(schoolID);
        }

        public KeyValueDTO GetSchoolVicePrincipal(byte schoolID)
        {
            return EmployeeRoleMapper.Mapper(CallContext).GetSchoolVicePrincipal(schoolID);
        }

        public KeyValueDTO GetSchoolHeadMistress(byte schoolID)
        {
            return EmployeeRoleMapper.Mapper(CallContext).GetSchoolHeadMistress(schoolID);
        }

        public KeyValueDTO GetClassHeadTeacher(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetClassHeadTeacher(classID, sectionID, academicYearID);
        }
        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetClassCoordinator(classID, sectionID, academicYearID);
        }
        public List<KeyValueDTO> GetAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetAssociateTeachers(classID, sectionID, academicYearID);
        }
        public List<KeyValueDTO> GetOtherTeachersByClass(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetOtherTeachersByClass(classID, sectionID, academicYearID);
        }
        public List<StudentAttendenceDTO> GetStudentClassWiseAttendance(long studentID, int schoolID, int? academicYearID)
        {
            return StudentAttendenceMapper.Mapper(CallContext)
                .GetStudentClassWiseAttendance(studentID, schoolID, CallContext.LoginID.Value, academicYearID);
        }
        public OperationResultDTO CustomerFeedback(CustomerFeedbackDTO feedback)
        {
            return TicketingMapper.Mapper(CallContext).CustomerFeedback(feedback);
        }

        public List<Contracts.School.Transports.VehicleDTO> GetVehicleDetailsWithRoutesAndStopsByEmployeeLoginID()
        {
            return RouteVehicleMapMapper
                .Mapper(CallContext)
                .GetVehicleDetailsWithRoutesAndStopsByEmployeeLoginID(CallContext.LoginID.Value);
        }

        public List<SignupSlotAllocationMapDTO> GetParentAllotedMeetings()
        {
         
                var parentDetail = ParentMapper.Mapper(CallContext).GetParentDetailsByLoginID(CallContext.LoginID);
                var parentID = parentDetail.ParentIID;
         

            var result = new SignUpBL(CallContext).GetParentAllotedMeetings(parentID);

            return result;
        }
        public string SaveSignupSlotRemarkMap(SignupSlotRemarkMapDTO slotRemarkMap)
        {
            return SignupSlotRemarkMapMapper.Mapper(CallContext).SaveSignupSlotRemarkMap(slotRemarkMap);
        }

        public OperationResultDTO GenerateTicket(TicketDTO ticket)
        {
            return new SupportBL(CallContext).GenerateTicket(ticket);

        }

        public List<StudentPickupRequestDTO> GetUnverifiedStudentsAssignedToVisitor(string visitorCode)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetUnverifiedStudentsAssignedToVisitor(visitorCode);
        }

        public List<StudentPickupRequestDTO> GetTodayStudentPickLogsByvisitorLoginID(string visitorCode)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetTodayStudentPickLogsByvisitorLoginID(visitorCode);
        }

        public List<KeyValueDTO> GetAcademicYearBySchoolForApplications(int schoolID)
        {
            var academicYearList = AcademicYearMapper.Mapper(CallContext).GetAllAcademicYearBySchoolID(schoolID);

            return academicYearList;
        }

        public UserDTO EditUserDetails(UserDTO updatedDetails)
        {
            string loginEmailID = this.CallContext.EmailID;
            string mobileNumber = this.CallContext.MobileNumber; // You can choose to pass this or derive it

            return new AccountBL(this.CallContext).EditUserDetails(loginEmailID, updatedDetails, mobileNumber);
        }

        public List<LibraryTransactionDTO> GetLibraryTransactions(string filter)
        {
            var parentID = StudentMapper.Mapper(CallContext).GetParentID(CallContext.LoginID.Value);


            return LibraryTransactionMapper
                .Mapper(CallContext)
                .GetLibraryTransactionDetails(parentID , filter);
        }

        public List<ClassTeacherMapDTO> GetClassTeacherClass()
        {
            return ClassTeacherMapMapper
                .Mapper(CallContext)
                .GetClassTeacherClass(CallContext.LoginID.Value);
        }
        public List<CounselorHubListDTO> GetCounselorList()
        {

            return CounselorHubMapper
                .Mapper(CallContext)
                .GetCounselorList(CallContext.LoginID.Value);
        }


        public List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            try
            {
                List<KeyValueDTO> values = new ReferenceDataBL(CallContext).GetLookUpData(lookType, searchText);
                return values;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public  Task<IActionResult> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo)
        {
            return new  BoilerPlateBL(CallContext).GetBoilerPlatesForMobileApp(boilerPlateInfo);

        }

        public List<SchoolsDTO> GetSchoolsProfileWithAcademicYear()
        {
            return SchoolsMapper.Mapper(CallContext).GetSchoolsProfileWithAcademicYear();
        }

        public KeyValueDTO GetCurrentAcademicYearBySchoolID(long schoolID)
        {
            return SchoolsMapper.Mapper(CallContext).GetCurrentAcademicYearBySchoolID(schoolID);
        }


        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            return AcademicYearMapper.Mapper(CallContext).GetActiveAcademicYearListData();
        }

        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            return SchoolsMapper.Mapper(CallContext).GetPowerBIDataUsingPageID(pageID);
        }

        public bool CheckLeaveDateConflict(LeaveRequestDTO leaveData)
        {
            return  LeaveRequestMapper.Mapper(CallContext).CheckLeaveDateConflict(leaveData);
        }

        public List<KeyValueDTO> Getstudentsubjectlist(long studentID)
        {
            return AssignmentMapper
                .Mapper(CallContext)
                .Getstudentsubjectlist(studentID);
        }

        public List<AgendaDTO> GetStudentSubjectWiseAgendas(long studentID, long subjectID, string date)
        {
            return AgendaTopicMapper
                .Mapper(CallContext)
                .GetStudentSubjectWiseAgendas(studentID,  subjectID,  date);
        }

        public List<KeyValueDTO> GetSupportSubCategoriesByCategoryID(int? supportCategoryID)
        {
            return SupportCategoryMapper
                .Mapper(CallContext)
                .GetSupportSubCategoriesByCategoryID(supportCategoryID);
        }
                
        public List<string> GetCircularUserTypes()
        {
            return CircularMapper
                .Mapper(CallContext)
                .GetCircularUserTypes();
        }


        public List<TransportApplicationStudentMapDTO> GetTransportApplication()
        {
            return TransportApplicationMapper
                .Mapper(CallContext)
                .GetTransportApplication(CallContext.LoginID.Value);
        }

        public StaticContentDataDTO GetAboutandContactDetails(long contentID)
        {
            return StaticContentDataMapper
                .Mapper(CallContext)
                .GetAboutandContactDetails(contentID);
        }

        public StaticContentDataDTO GetTransportApplicationDetails(long contentID)
        {
            //return TransportApplicationMapper
            //    .Mapper(CallContext)
            //    .GetTransportApplicationDetails(contentID);
            return null;
        }

        public TransportApplicationDTO GetStudentDetailsForTransportApplication(long id)
        {
            return TransportApplicationMapper
                .Mapper(CallContext)
                .GetTransportStudentDetailsByParentLoginID(id);
        }

        public TransportApplicationDTO SubmitTransportApplication(TransportApplicationDTO transportApplicationData)
        {
            return TransportApplicationMapper
                .Mapper(CallContext)
                .SubmitTransportApplication(transportApplicationData);
        }

        public List<AgendaDTO> GetTeacherSubjectWiseAgendas( long subjectID, string date)
        {
            return AgendaTopicMapper
                .Mapper(CallContext)
                .GetAgendaStaffwise(CallContext.EmployeeID.Value, subjectID, date);
        }

        public List<KeyValueDTO> Getteachersubjectlist()
        {
            return SubjectMapper
                .Mapper(CallContext)
                .Getteachersubjectlist(CallContext.EmployeeID.Value);
        }

        public List<StudentAttendanceStatusDTO> GetStudentAttendanceForTodayByClassAndSection(long classID, long sectionID)
        {
            return StudentAttendenceMapper
                .Mapper(CallContext)
                .GetStudentAttendanceForTodayByClassAndSection( classID,  sectionID);
        }

        public List<StudentLeaveApplicationDTO> GetLeaveRequestByClassSectionDate(long classID, long sectionID, DateTime date)
        {
            return StudentLeaveApplicationMapper
                .Mapper(CallContext)
                .GetLeaveRequestByClassSectionDate(classID, sectionID, date);
        }

        public List<StudentPickupRequestDTO> GetPickupRequestsByClassSectionDate(long classID, long sectionID, DateTime date)
        {
            return StudentPickupRequestMapper
                .Mapper(CallContext)
                .GetPickupRequestsByClassSectionDate(classID, sectionID, date);
        }

        public List<KeyValueDTO> GetClassTimes(long studentID)
        {
            return TimeTableAllocationMapper
                .Mapper(CallContext)
                .GetClassTimes(studentID);
        }

    }
}