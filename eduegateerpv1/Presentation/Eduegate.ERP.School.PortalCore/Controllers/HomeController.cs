using Eduegate.Domain;
using Eduegate.ERP.School.ParentPortal.Models;
using Eduegate.ERP.School.ParentPortal.ViewModel;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Application.Mvc;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Web.Library.School.Exams;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.School.Students;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Contracts.Enums;
using System.IO.Compression;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class HomeController : BaseController
    {
        //[Frameworks.Mvc.ActionFilters.CustomAuthorization]
        public ActionResult Index()
        {
            var cultureCode = this.Request.Query["language"];

            if (!string.IsNullOrEmpty(cultureCode))
            {
                ViewBag.CultureCode = cultureCode;
                base.CallContext.LanguageCode = cultureCode;
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCode);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureCode);
            }

            var userDTO = new UserDTO();
            var CompanyID = "1";

            if (!string.IsNullOrEmpty(CallContext.EmailID))
            {
                if (ClientFactory.AccountServiceClient(CallContext).IsPasswordResetRequired(CallContext.EmailID))
                    return Redirect("~/Account/ResetPassword");
            }

            if (CallContext.UserRole.IsNullOrEmpty())
            {
                userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

                if (userDTO.IsNotNull())
                    ResetCookies(userDTO, int.Parse(CompanyID));
            }
            if (CallContext.UserRole.IsNullOrEmpty())
            {
                return Redirect("~/Home/AnonymousDashbaord");
            }
            else
            {
                return Redirect("~/Home/MyWards");
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }

    

        public ActionResult Attendance()
        {
            return View();
        }
        public ActionResult Mailbox()
        {
            return View();
        }
        public ActionResult ApplicationF(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new StudentApplicationViewModel();
            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();
            vm.SchoolAcademicyear = academicYear.Value;
            vm.AcademicyearID = int.Parse(academicYear.Key);
            vm.CurriculamString = schoolSyllabus.Value;
            vm.CurriculamID = byte.Parse(schoolSyllabus.Key);

            if (Id != 0)
            {
                var application = ClientFactory.SchoolServiceClient(CallContext).GetApplication(Id);

                if (application != null)
                {
                    vm = vm.ToVM(application);
                }
            }

            return View("ApplicationF", vm);
        }
        public ActionResult ParentDashboard()
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View();
        }

        public ActionResult ToGetAttendancePercentageByParentLoginid()
        {
            String[] dataResult = null;
            List<ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID> attendence = new List<ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("[schools].[GET_ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID]", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@LoginID"].Value = CallContext.LoginID;// 30159;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String stringDateFrom = row.ItemArray[6].ToString(); String stringDateTo = row.ItemArray[7].ToString();

                        //DateTime? DateFrom = string.IsNullOrEmpty(row.ItemArray[6].ToString()) ? (DateTime?)null : DateTime.ParseExact(row.ItemArray[6].ToString(), dateFormat, CultureInfo.InvariantCulture);
                        //DateTime? DateTo =  string.IsNullOrEmpty(row.ItemArray[7].ToString()) ? (DateTime?)null : DateTime.ParseExact(row.ItemArray[7].ToString(), dateFormat, CultureInfo.InvariantCulture);

                        ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID studentAttendence = new ATTENDENCE_PERCENTAGE_BY_PARENT_LOGINID();
                        studentAttendence.PercentAttendance = Convert.ToDecimal(row.ItemArray[0]);
                        studentAttendence.TotalWorkingDays = Convert.ToDecimal(row.ItemArray[1]);
                        studentAttendence.TotalPresetDays = Convert.ToDecimal(row.ItemArray[2]);
                        studentAttendence.AttendanceDate = Convert.ToDateTime(row.ItemArray[3]);
                        studentAttendence.AcademicStartDate = Convert.ToDateTime(row.ItemArray[4]);
                        studentAttendence.AcademicEndDate = Convert.ToDateTime(row.ItemArray[5]);
                        studentAttendence.TotalLeave = Convert.ToInt64(row.ItemArray[6].ToString());
                        studentAttendence.StudentIID = Convert.ToInt64(row.ItemArray[7]);
                        studentAttendence.IsActive = Convert.ToBoolean(row.ItemArray[8]);
                        studentAttendence.LoginID = Convert.ToInt64(row.ItemArray[9]);
                        studentAttendence.StudentName = row.ItemArray[10].ToString();
                        attendence.Add(studentAttendence);
                    }
                }

            }
            return Json(attendence);
        }

        public ActionResult GetStudentTransportDetails(Int64? StudentID)
        {
            String[] dataResult = null;
            List<STUDENT_TRANSPORT_DETAILS> studentsTrasnports = new List<STUDENT_TRANSPORT_DETAILS>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("[schools].[GET_STUDENT_TRANSPORT_DETAILS_BY_PARENT_LOGINID]", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@LoginID"].Value = CallContext.LoginID;// 30159;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@StudentID"].Value = StudentID;// 30159;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String stringDateFrom = row.ItemArray[6].ToString();
                        String stringDateTo = row.ItemArray[7].ToString();

                        DateTime? DateFrom = string.IsNullOrEmpty(stringDateFrom) ? null : Convert.ToDateTime(stringDateFrom); //string.IsNullOrEmpty(row.ItemArray[6].ToString()) ? (DateTime?)null : DateTime.ParseExact(row.ItemArray[6].ToString(), dateFormat, CultureInfo.InvariantCulture);
                        DateTime? DateTo = string.IsNullOrEmpty(stringDateTo) ? null : Convert.ToDateTime(stringDateTo); //string.IsNullOrEmpty(row.ItemArray[7].ToString()) ? (DateTime?)null : DateTime.ParseExact(row.ItemArray[7].ToString(), dateFormat, CultureInfo.InvariantCulture);

                        STUDENT_TRANSPORT_DETAILS studentTransport = new STUDENT_TRANSPORT_DETAILS();
                        studentTransport.StudentID = Convert.ToInt64(row.ItemArray[0]);
                        studentTransport.Name = row.ItemArray[1].ToString();
                        //studentTransport.PickupStopMapID = Convert.ToInt64(row.ItemArray[2]);
                        studentTransport.PickupStopMapName = row.ItemArray[3].ToString();
                        //studentTransport.DropStopMapID = Convert.ToInt64(row.ItemArray[4]);
                        studentTransport.DropStopMapName = row.ItemArray[5].ToString();
                        studentTransport.IsOneWay = Convert.ToBoolean(row.ItemArray[8]);
                        studentTransport.DateFrom = DateFrom?.ToString("dd-MM-yyyy");
                        studentTransport.DateTo = DateTo?.ToString("dd-MM-yyyy");
                        studentTransport.ClassID = Convert.ToInt64(row.ItemArray[9]);
                        studentTransport.SectionID = Convert.ToInt64(row.ItemArray[10]);
                        studentTransport.PickupRouteCode = row.ItemArray[12].ToString();
                        studentTransport.PickupStopDriverName = row.ItemArray[13].ToString();
                        studentTransport.DropStopRouteCode = row.ItemArray[14].ToString();
                        studentTransport.DropStopDriverName = row.ItemArray[15].ToString();

                        studentTransport.PickupContactNo = row.ItemArray[16].ToString();
                        studentTransport.DropContactNo = row.ItemArray[17].ToString();
                        studentsTrasnports.Add(studentTransport);
                    }
                }

            }

            return Json(studentsTrasnports);
        }

        //To Get Student Progress Report
        public ActionResult GetStudentProgressReport(Int64? StudentID)
        {
            var ResModel = new ResponseModel();
            List<STUDENT_PROGRESS_REPORT> studentsProgressReport = new List<STUDENT_PROGRESS_REPORT>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue("DateFormat", 0, "dd/MM/yyyy");
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                try
                {
                    conn.Open();
                    var markList = new List<STUDENT_PROGRESS_REPORT_MARKLIST>();
                    var examList = new List<STUDENT_PROGRESS_REPORT>();

                    using (SqlCommand cmd = new SqlCommand("[schools].[GET_STUDENT_PROGRESS_REPORT]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentID", StudentID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                STUDENT_PROGRESS_REPORT_MARKLIST markListItem = new STUDENT_PROGRESS_REPORT_MARKLIST
                                {
                                    ExamID = Convert.ToInt64(reader["ExamID"]),
                                    ExamName = (string)reader["ExamName"],
                                    StudentId = Convert.ToInt64(reader["StudentId"]),
                                    StudentName = (string)reader["StudentName"],
                                    ClassID = Convert.ToInt64(reader["ClassID"]),
                                    SectionID = Convert.ToInt64(reader["SectionID"]),
                                    ExamTypeID = Convert.ToInt64(reader["ExamTypeID"]),
                                    ExamTypeName = (string)reader["ExamTypeName"],
                                    SubjectID = Convert.ToInt64(reader["SubjectID"]),
                                    SubjectName = (string)reader["SubjectName"],
                                    MinimumMarks = (double)reader["MinimumMarks"],
                                    MaximumMarks = (double)reader["MaximumMarks"],
                                    Mark = (double)reader["Mark"],
                                    MarksGradeMapID = Convert.ToInt64(reader["MarksGradeMapID"]),
                                    GradeName = (string)reader["GradeName"],
                                    IsPassed = (bool)reader["IsPassed"],
                                };

                                STUDENT_PROGRESS_REPORT examListItem = new STUDENT_PROGRESS_REPORT
                                {
                                    ExamID = (Int64)reader["ExamID"],
                                    ExamName = (string)reader["ExamName"],
                                    MarkList = new List<STUDENT_PROGRESS_REPORT_MARKLIST> { markListItem }
                                };

                                markList.Add(markListItem);
                                examList.Add(examListItem);
                            }
                        }
                    }

                    if (markList.Count > 0)
                    {
                        ResModel.Data = new
                        {
                            markList,
                            examList
                        };
                        string message = string.Format("");
                    }
                }
                catch (Exception e)
                {
                    string message = string.Format(e.Message + ", " + e.InnerException?.Message);
                    ResModel.Message = message;
                    ResModel.Data = "";
                }
            }

            return Json(ResModel);
        }
        //End To Get Student Progress Report

        public ActionResult GetAllMailsByLoginIDandFolderName(String Folder)
        {
            var ResModel = new ResponseModel();
            var mailList = new List<MAIL_LIST>();

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("[schools].[GET_MAIL_LIST]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginID", CallContext.LoginID);
                        cmd.Parameters.AddWithValue("@FloderName", Folder);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MAIL_LIST mailListItem = new MAIL_LIST
                                {
                                    mailBoxID = Convert.ToInt64(reader["mailBoxID"]),
                                    fromID = Convert.ToInt64(reader["fromID"]),
                                    toID = Convert.ToInt64(reader["toID"]),
                                    mailSubject = (string)reader["mailSubject"],
                                    mailBody = (string)reader["mailBody"],
                                    mailFolder = (string)reader["mailFolder"],
                                    viewStatus = Convert.ToBoolean(reader["viewStatus"]),
                                    fromDelete = Convert.ToBoolean(reader["fromDelete"]),
                                    toDelete = Convert.ToBoolean(reader["toDelete"]),
                                    onDate = Convert.ToDateTime(reader["onDate"]),
                                    UserName = (string)reader["UserName"],
                                    // Map other properties
                                };

                                mailList.Add(mailListItem);
                            }
                        }
                    }

                    if (mailList.Count > 0)
                    {
                        ResModel.Data = new
                        {
                            mailList,
                            isError = false
                        };
                    }
                    else
                    {
                        ResModel.Data = new
                        {
                            isError = true,
                        };
                    }
                }
                catch (Exception e)
                {
                    var message = string.Format(e.Message + ", " + e.InnerException?.Message);
                    ResModel.Message = message;
                    ResModel.Data = "";
                }
            }

            return Json(ResModel);
        }
        public ActionResult Index2(long Id = 0)
        {

            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new StudentApplicationViewModel();
            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();
            vm.SchoolAcademicyear = academicYear.Value;
            vm.AcademicyearID = int.Parse(academicYear.Key);
            vm.CurriculamString = schoolSyllabus.Value;
            vm.CurriculamID = byte.Parse(schoolSyllabus.Key);

            if (Id != 0)
            {
                var application = ClientFactory.SchoolServiceClient(CallContext).GetApplication(Id);

                if (application != null)
                {
                    vm = vm.ToVM(application);
                }
            }
            return View(vm);
        }
        public ActionResult AnonymousDashbaord(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View("AnonymousDashbaord", "_Layout_Guest");
        }

        public ActionResult ParentDashbaord()
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }

            var parentDashboard = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<string>("ParentDashboard");
            var studentDashboard = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<string>("StudentDashboard");
            var userDashBaords = ClientFactory.UserServiceClient(CallContext).GetClaimsByTypeAndLoginID(Services.Contracts.Enums.ClaimType.ERPDashbaord, CallContext.LoginID.Value);
            var pageResource = userDashBaords.IsNotNull() && userDashBaords.Count > 0 ? userDashBaords.FirstOrDefault().ResourceName : parentDashboard;
            var pageID = string.IsNullOrEmpty(parentDashboard) ? ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompanyWithDefault<long>("DEFAULTPARENTPORTAL", CallContext.CompanyID.Value, 2)
                : long.Parse(pageResource);
            return View(new PageRedererViewModel(
                PageEngineViewModel.FromDTO(
                ClientFactory.PageRenderServiceClient(CallContext)
                .GetPageInfo(pageID, string.Empty), string.Empty, true)));
        }

        public ActionResult About()
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ViewBag.Message = "About Us";
            return View();
        }

        public ActionResult Mywards()
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ViewBag.Message = "My Wards";
            return View();
        }

        public ActionResult Mywards_v01()
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ViewBag.Message = "My Wards";
            return View();
        }

        public ActionResult Contact()
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ViewBag.Message = "Your contact page";
            return View();
        }

        public ActionResult CustomDashbaord(long pageID)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ViewBag.CallContext = CallContext;
            return View(new PageRedererViewModel(PageEngineViewModel.FromDTO(
                    ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(pageID, string.Empty), string.Empty, true)));
        }

        public JsonResult GetClasswiseStudentData(int classId, int sectionId)
        {
            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClasswiseStudentData(classId, sectionId);
            return Json(listStudents);
        }

        public JsonResult GetNotificationAlertsCount()
        {
            var alertCount = ClientFactory.SchoolServiceClient(CallContext).GetNotificationAlertsCount(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);
            return Json(alertCount);
        }

        [HttpPost]
        public string MarkNotificationAsRead(long notificationAlertIID)
        {
            var saveDatas = ClientFactory.SchoolServiceClient(CallContext).MarkNotificationAsRead(CallContext.LoginID.Value, notificationAlertIID);
            if (saveDatas == "success")
            {
                return saveDatas;
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStudentAttendenceByYearMonthStudentId(month, year, studentId);
            return Json(attendence);
        }

        public ActionResult GetAcademicCalenderByMonthYear(int month, int year)
        {
            var calendarList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicCalenderByMonthYear(month, year);
            return Json(calendarList);
        }

        public JsonResult GetUserDetails()
        {
            var userDetail = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

            if (userDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                if (!string.IsNullOrEmpty(userDetail.ProfileFile))
                {
                    userDetail.ProfileFile = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, userDetail.ProfileFile);
                }

                return Json(new { IsError = false, Response = userDetail });
            }
        }

        public ActionResult NewApplication(long loginID)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new StudentApplicationViewModel();
            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();
            if (academicYear != null)
            {
                vm.SchoolAcademicyear = academicYear.Value;
                vm.AcademicyearID = int.Parse(academicYear.Key);
            }
            vm.CurriculamString = schoolSyllabus.Value;
            vm.CurriculamID = byte.Parse(schoolSyllabus.Key);

            if (loginID != 0)
            {
                var application = ClientFactory.SchoolServiceClient(CallContext).GetApplicationByLoginID(loginID);

                if (application != null)
                {
                    vm = vm.ToVM(application);
                }
            }

            return View("NewApplication", vm);
        }

        public ActionResult GetApplication(long Id)
        {
            var application = ClientFactory.SchoolServiceClient(CallContext).GetApplication(Id);
            return Json(new { IsError = false, Response = application });
        }

        public ActionResult DeleteApplication(long Id)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteApplication(Id);
            return Redirect("~/Home/AnonymousDashbaord");
        }

        [HttpPost]
        public ActionResult SubmitApplication(StudentApplicationViewModel application)
        {
            //Get academic year and school syllabus
            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();

            //Student Related Details Date Conversion
            application.DateOfBirthString = application.DateOfBirthString != null ? Convert.ToDateTime(application.DateOfBirthString).ToString("dd/MM/yyyy") : null;
            application.VisaExpiryDateString = application.VisaExpiryDateString != null ? Convert.ToDateTime(application.VisaExpiryDateString).ToString("dd/MM/yyyy") : null;
            application.PassportNoIssueString = application.PassportNoIssueString != null ? Convert.ToDateTime(application.PassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.PassportNoExpiryString = application.PassportNoExpiryString != null ? Convert.ToDateTime(application.PassportNoExpiryString).ToString("dd/MM/yyyy") : null;
            application.StudentNationalIDNoIssueDateString = application.StudentNationalIDNoIssueDateString != null ? Convert.ToDateTime(application.StudentNationalIDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.StudentNationalIDNoExpiryDateString = application.StudentNationalIDNoExpiryDateString != null ? Convert.ToDateTime(application.StudentNationalIDNoExpiryDateString).ToString("dd/MM/yyyy") : null;

            //Mother Related Details Date Conversion
            application.FatherMotherDetails.MotherPassportNoIssueString = application.FatherMotherDetails.MotherPassportNoIssueString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherPassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.MotherPassportNoExpiryString = application.FatherMotherDetails.MotherPassportNoExpiryString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherPassportNoExpiryString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.MotherNationalDNoIssueDateString = application.FatherMotherDetails.MotherNationalDNoIssueDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherNationalDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.MotherNationaIDNoExpiryDateString = application.FatherMotherDetails.MotherNationaIDNoExpiryDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherNationaIDNoExpiryDateString).ToString("dd/MM/yyyy") : null;

            //Father Related Details Date Conversion
            application.FatherMotherDetails.FatherNationalDNoIssueDateString = application.FatherMotherDetails.FatherNationalDNoIssueDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherNationalDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.FatherNationalDNoExpiryDateString = application.FatherMotherDetails.FatherNationalDNoExpiryDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherNationalDNoExpiryDateString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.FatherPassportNoIssueString = application.FatherMotherDetails.FatherPassportNoIssueString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherPassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.FatherPassportNoExpiryString = application.FatherMotherDetails.FatherPassportNoExpiryString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherPassportNoExpiryString).ToString("dd/MM/yyyy") : null;

            //Guardian Related Details Date Conversion
            application.GuardianDetails.GuardianNationalIDNoIssueDateString = application.GuardianDetails.GuardianNationalIDNoIssueDateString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianNationalIDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.GuardianDetails.GuardianNationalIDNoExpiryDateString = application.GuardianDetails.GuardianNationalIDNoExpiryDateString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianNationalIDNoExpiryDateString).ToString("dd/MM/yyyy") : null;
            application.GuardianDetails.GuardianPassportNoIssueString = application.GuardianDetails.GuardianPassportNoIssueString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianPassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.GuardianDetails.GuardianPassportNoExpiryString = application.GuardianDetails.GuardianPassportNoExpiryString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianPassportNoExpiryString).ToString("dd/MM/yyyy") : null;

            #region Inside ID passing for DropDowns
            application.GuardianDetails.GuardianStudentRelationShip = application.GuardianStudentRelationShip != null ? application.GuardianStudentRelationShip : null;
            application.GuardianDetails.GuardianNationality = application.GuardianNationality != null ? application.GuardianNationality : null;
            application.GuardianDetails.GuardianCountryofIssue = application.GuardianCountryofIssue != null ? application.GuardianCountryofIssue : null;
            // FatherScreen
            application.FatherMotherDetails.FatherCountry = application.FatherCountry != null ? application.FatherCountry : null;
            application.FatherMotherDetails.FatherCountryofIssue = application.FatherCountryofIssue != null ? application.FatherCountryofIssue : null;
            application.FatherMotherDetails.CanYouVolunteerToHelpOneString = application.CanYouVolunteerToHelpOneString != null ? application.CanYouVolunteerToHelpOneString : null;
            // MotherScreen
            application.FatherMotherDetails.MotherCountry = application.MotherCountry != null ? application.MotherCountry : null;
            application.FatherMotherDetails.CanYouVolunteerToHelpTwoString = application.CanYouVolunteerToHelpTwoString != null ? application.CanYouVolunteerToHelpTwoString : null;
            application.FatherMotherDetails.MotherCountryofIssue = application.MotherCountryofIssue != null ? application.MotherCountryofIssue : null;
            //LocationTab
            application.Address.Country = application.Country != null ? application.Country : null;
            //PreviousSchoolTab
            application.PreviousSchoolDetails.IsStudentStudiedBeforeForPortal = application.PreviousSchoolDetails.IsStudentStudiedBeforeForPortal;
            application.PreviousSchoolDetails.PreviousSchoolSyllabus = application.PreviousSchoolSyllabus != null ? application.PreviousSchoolSyllabus : null;
            application.PreviousSchoolDetails.PreviousSchoolClassClassKey = application.PreviousSchoolClassClassKey != null ? application.PreviousSchoolClassClassKey : null;
            #endregion

            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }

            try
            {
                application.ApplicationStatusID = application.ApplicationIID == 0 ? 1 : application.ApplicationStatusID;
                var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.StudentApplication, Data = application.AsDTOString(application.ToDTO(CallContext)) });
                if (crudSave.IsError)
                {
                    if (academicYear != null)
                    {
                        application.AcademicyearID = int.Parse(academicYear.Key);
                        application.SchoolAcademicyear = academicYear.Value;
                    }
                    application.CurriculamID = byte.Parse(schoolSyllabus.Key);
                    application.CurriculamString = schoolSyllabus.Value;

                    if (crudSave.ErrorMessage.Contains("Please Check! whether the application is submitted before for this student."))
                    {
                        return View("ApplicationConfirmationPopGuest", "_Layout_Guest");
                    }

                    return View("NewApplication", application);
                }
                else
                {
                    return Redirect("~/Home/AnonymousDashbaord");
                }
            }
            catch
            {
                if (academicYear != null)
                {
                    application.AcademicyearID = int.Parse(academicYear.Key);
                    application.SchoolAcademicyear = academicYear.Value;
                }
                application.CurriculamID = byte.Parse(schoolSyllabus.Key);
                application.CurriculamString = schoolSyllabus.Value;

                return View("NewApplication", application);
            }
        }

        public ActionResult NewApplicationEdit(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new StudentApplicationViewModel();

            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();
            if (academicYear != null)
            {
                vm.SchoolAcademicyear = academicYear.Value;
                vm.AcademicyearID = int.Parse(academicYear.Key);
            }
            vm.CurriculamString = schoolSyllabus.Value;
            vm.CurriculamID = byte.Parse(schoolSyllabus.Key);

            if (Id != 0)
            {
                var application = ClientFactory.SchoolServiceClient(CallContext).GetApplication(Id);

                if (application != null)
                {
                    vm = vm.ToVM(application);
                }
            }

            return View("NewApplication", vm);
        }


        public ActionResult NewSiblingApplication(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new StudentApplicationViewModel();

            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();
            if (academicYear != null)
            {
                vm.SchoolAcademicyear = academicYear.Value;
                vm.AcademicyearID = int.Parse(academicYear.Key);
            }
            vm.CurriculamString = schoolSyllabus.Value;
            vm.CurriculamID = byte.Parse(schoolSyllabus.Key);

            if (Id != 0)
            {
                var application = ClientFactory.SchoolServiceClient(CallContext).GetApplication(Id);

                if (application != null)
                {
                    vm = vm.ToVM(application);
                }
            }

            return View("NewApplicationFromParent", vm);
        }

        public ActionResult ApplicationListFromParent(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View("ApplicationListFromParent", "_Layout");
        }

        public ActionResult _CircularList_Parent(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View("_CircularList_Parent", "_Layout");
        }
        
        public ActionResult GalleryView(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View("Gallery", "_Layout");
        }

        public ActionResult Gallery_Parent(long Id = 0)
        {
            return View("Gallery", "_Layout");
        }

        public ActionResult GetSiblingApplication(long Id)
        {
            //try
            //{
            //    if (CallContext.LoginID == null)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //}
            //catch { }
            var application = ClientFactory.SchoolServiceClient(CallContext).GetApplication(Id);
            return Json(new { IsError = false, Response = application });
        }


        public ActionResult DeleteSiblingApplication(long Id)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ClientFactory.SchoolServiceClient(CallContext).DeleteSiblingApplication(Id);

            return Redirect("~/Home/ApplicationListFromParent");
        }

        public ActionResult ReportCard(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View("ReportCard", "_Layout");
        }

        [HttpPost]
        public ActionResult SubmitSiblingApplication([FromForm] StudentApplicationViewModel application)
        {
            var docUploads = new List<StudentApplicationDocUploadViewModel>();
            //Get academic year and school syllabus
            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();

            //Student Related Details Date Conversion
            application.DateOfBirthString = application.DateOfBirthString != null ? Convert.ToDateTime(application.DateOfBirthString).ToString("dd/MM/yyyy") : null;
            application.VisaExpiryDateString = application.VisaExpiryDateString != null ? Convert.ToDateTime(application.VisaExpiryDateString).ToString("dd/MM/yyyy") : null;
            application.PassportNoIssueString = application.PassportNoIssueString != null ? Convert.ToDateTime(application.PassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.PassportNoExpiryString = application.PassportNoExpiryString != null ? Convert.ToDateTime(application.PassportNoExpiryString).ToString("dd/MM/yyyy") : null;
            application.StudentNationalIDNoIssueDateString = application.StudentNationalIDNoIssueDateString != null ? Convert.ToDateTime(application.StudentNationalIDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.StudentNationalIDNoExpiryDateString = application.StudentNationalIDNoExpiryDateString != null ? Convert.ToDateTime(application.StudentNationalIDNoExpiryDateString).ToString("dd/MM/yyyy") : null;

            //Mother Related Details Date Conversion
            application.FatherMotherDetails.MotherPassportNoIssueString = application.FatherMotherDetails.MotherPassportNoIssueString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherPassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.MotherPassportNoExpiryString = application.FatherMotherDetails.MotherPassportNoExpiryString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherPassportNoExpiryString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.MotherNationalDNoIssueDateString = application.FatherMotherDetails.MotherNationalDNoIssueDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherNationalDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.MotherNationaIDNoExpiryDateString = application.FatherMotherDetails.MotherNationaIDNoExpiryDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.MotherNationaIDNoExpiryDateString).ToString("dd/MM/yyyy") : null;

            //Father Related Details Date Conversion
            application.FatherMotherDetails.FatherNationalDNoIssueDateString = application.FatherMotherDetails.FatherNationalDNoIssueDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherNationalDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.FatherNationalDNoExpiryDateString = application.FatherMotherDetails.FatherNationalDNoExpiryDateString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherNationalDNoExpiryDateString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.FatherPassportNoIssueString = application.FatherMotherDetails.FatherPassportNoIssueString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherPassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.FatherMotherDetails.FatherPassportNoExpiryString = application.FatherMotherDetails.FatherPassportNoExpiryString != null ? Convert.ToDateTime(application.FatherMotherDetails.FatherPassportNoExpiryString).ToString("dd/MM/yyyy") : null;

            //Guardian Related Details Date Conversion
            application.GuardianDetails.GuardianNationalIDNoIssueDateString = application.GuardianDetails.GuardianNationalIDNoIssueDateString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianNationalIDNoIssueDateString).ToString("dd/MM/yyyy") : null;
            application.GuardianDetails.GuardianNationalIDNoExpiryDateString = application.GuardianDetails.GuardianNationalIDNoExpiryDateString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianNationalIDNoExpiryDateString).ToString("dd/MM/yyyy") : null;
            application.GuardianDetails.GuardianPassportNoIssueString = application.GuardianDetails.GuardianPassportNoIssueString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianPassportNoIssueString).ToString("dd/MM/yyyy") : null;
            application.GuardianDetails.GuardianPassportNoExpiryString = application.GuardianDetails.GuardianPassportNoExpiryString != null ? Convert.ToDateTime(application.GuardianDetails.GuardianPassportNoExpiryString).ToString("dd/MM/yyyy") : null;

            #region Inside ID passing for DropDowns
            application.GuardianDetails.GuardianStudentRelationShip = application.GuardianStudentRelationShip != null ? application.GuardianStudentRelationShip : null;
            application.GuardianDetails.GuardianNationality = application.GuardianNationality != null ? application.GuardianNationality : null;
            application.GuardianDetails.GuardianCountryofIssue = application.GuardianCountryofIssue != null ? application.GuardianCountryofIssue : null;
            // FatherScreen
            application.FatherMotherDetails.FatherCountry = application.FatherCountry != null ? application.FatherCountry : null;
            application.FatherMotherDetails.FatherCountryofIssue = application.FatherCountryofIssue != null ? application.FatherCountryofIssue : null;
            application.FatherMotherDetails.CanYouVolunteerToHelpOneString = application.CanYouVolunteerToHelpOneString != null ? application.CanYouVolunteerToHelpOneString : null;
            // MotherScreen
            application.FatherMotherDetails.MotherCountry = application.MotherCountry != null ? application.MotherCountry : null;
            application.FatherMotherDetails.CanYouVolunteerToHelpTwoString = application.CanYouVolunteerToHelpTwoString != null ? application.CanYouVolunteerToHelpTwoString : null;
            application.FatherMotherDetails.MotherCountryofIssue = application.MotherCountryofIssue != null ? application.MotherCountryofIssue : null;
            //LocationTab
            application.Address.Country = application.Country != null ? application.Country : null;
            //PreviousSchoolTab
            application.PreviousSchoolDetails.IsStudentStudiedBeforeForPortal = application.PreviousSchoolDetails.IsStudentStudiedBeforeForPortal;
            application.PreviousSchoolDetails.PreviousSchoolSyllabus = application.PreviousSchoolSyllabus != null ? application.PreviousSchoolSyllabus : null;
            application.PreviousSchoolDetails.PreviousSchoolClassClassKey = application.PreviousSchoolClassClassKey != null ? application.PreviousSchoolClassClassKey : null;
            #endregion

            //Optional Subjects
            if (application.OptionalSubjectID.HasValue || application.OptionalSubjectID.IsNotNull())
            {
                application.OptionalSubjects = new List<KeyValueViewModel>
                {
                    new KeyValueViewModel { Key = application.OptionalSubjectID.ToString() }
                };
            }

            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            try
            {
                var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.StudentApplication, Data = application.AsDTOString(application.ToDTO(CallContext)) });
                if (crudSave.IsError)
                {
                    if (academicYear != null)
                    {
                        application.AcademicyearID = int.Parse(academicYear.Key);
                        application.SchoolAcademicyear = academicYear.Value;
                    }
                    application.CurriculamID = byte.Parse(schoolSyllabus.Key);
                    application.CurriculamString = schoolSyllabus.Value;

                    if (crudSave.ErrorMessage.Contains("Please Check! whether the application is submitted before for this student."))
                    {
                        return View("ApplicationConfirmationPop", "_Layout");
                    }

                    return View("NewApplicationFromParent", application);
                }
                else
                {
                    var schoolID = string.IsNullOrEmpty(application.School) ? (byte?)null : byte.Parse(application.School);

                    string schoolShortName = string.Empty;
                    if (schoolID.HasValue)
                    {
                        var schoolData = new Eduegate.Domain.Setting.SettingBL(CallContext).GetSchoolDetailByID(schoolID.Value);

                        schoolShortName = schoolData?.SchoolShortName.ToLower();
                    }

                    // var data = crudSave.Data.to;
                    string emailBody = @"<br />
                                        <strong style='font - size:1.8rem;'>Your application has been submitted successfully. We will contact you soon once the application verified!";
                    string mailMessage = PopulateBody("Parent", emailBody);

                    var hostDet = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                    string defaultMail = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
                    var subject = "Automatic reply: Received your application";

                    var mailParameters = new Dictionary<string, string>()
                    {
                        { "SCHOOL_SHORT_NAME", schoolShortName},
                    };

                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(application.ParentLoginEmailID, subject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(defaultMail, subject, mailMessage, EmailTypes.StudentApplicaton, mailParameters);
                    }

                    return Redirect("~/Home/ApplicationListFromParent");
                }
            }
            catch
            {
                if (academicYear != null)
                {
                    application.AcademicyearID = int.Parse(academicYear.Key);
                    application.SchoolAcademicyear = academicYear.Value;
                }
                application.CurriculamID = byte.Parse(schoolSyllabus.Key);
                application.CurriculamString = schoolSyllabus.Value;

                return View("NewApplicationFromParent", application);
            }
        }

        private String PopulateBody(String Name, String htmlMessage)
        {
            string body = string.Empty;

            // Get the file path
            string filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Views/External/emailtemplate_otp.html");

            // Read the content of the file
            body = System.IO.File.ReadAllText(filePath);

            //body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }

        private String PopulateBodyForOTP(String Name, String content_01, String content_02, String content_03, String content_otp)
        {
            string body = string.Empty;

            // Get the file path
            string filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Views/External/emailtemplate_otp.html");

            // Read the content of the file
            body = System.IO.File.ReadAllText(filePath);

            body = body.Replace("{content_01}", content_01);
            body = body.Replace("{Content_02}", content_02);
            body = body.Replace("{Content_03}", content_03);
            body = body.Replace("{Content_OTP}", content_otp);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }
        public ActionResult StudentLeaveApplication(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new StudentLeaveApplicationViewModel();

            if (Id != 0)
            {
                var leaveApplication = ClientFactory.SchoolServiceClient(CallContext).GetLeaveApplication(Id);

                if (leaveApplication != null)
                {
                    vm = vm.ToVM(leaveApplication);
                }
            }

            return View("StudentLeaveApplication", vm);
        }

        public ActionResult GetLeaveApplication(long Id)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }

            var vm = new StudentLeaveApplicationViewModel();

            if (Id != 0)
            {
                var leaveApplicationDTO = ClientFactory.SchoolServiceClient(CallContext).GetLeaveApplication(Id);

                if (leaveApplicationDTO != null)
                {
                    vm = vm.ToVM(leaveApplicationDTO);
                }
            }

            return Json(new { IsError = false, Response = vm });
        }

        public ActionResult DeleteLeaveApplication(long Id)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ClientFactory.SchoolServiceClient(CallContext).DeleteLeaveApplication(Id);

            return Redirect("~/Home/Mywards");
        }

        [HttpPost]
        public ActionResult SubmitLeaveApplication([FromBody] StudentLeaveApplicationViewModel leaveApplication)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.StudentLeaveApplication, Data = leaveApplication.AsDTOString(leaveApplication.ToDTO(CallContext)) });

            return Json(new { IsError = false, Response = crudSave });

        }

        public JsonResult ComposeMail(CallContext context, ComposeMailClass mailClass)
        {
            try
            {
                string[] MailName = mailClass.frommail.Split('@');
                string emailBody = mailClass.bodymail;
                string replaymessage = PopulateBody(MailName[0], emailBody);

                var hostDet = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                string defaultMail = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                var mailType = "Compose";

                if (hostDet.ToLower() == "live")
                {
                    SendComposeMail(mailClass.tomail, mailClass.subjectmail, replaymessage, MailName[0], mailClass.frommail);
                }
                else
                {
                    SendComposeMail(defaultMail, mailClass.subjectmail, replaymessage, MailName[0], mailClass.frommail);
                }

                var notifications = new List<NotificationAlertsDTO>();
                try
                {
                    using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
                    {
                        //Insert/update schools.StudentRouteMonthlySplit  
                        using (SqlCommand sqlCommand2 = new SqlCommand("[schools].[SP_INSERT_INTO_MAIL_BOX]", conn))
                        {
                            sqlCommand2.CommandType = CommandType.StoredProcedure;

                            sqlCommand2.Parameters.Add(new SqlParameter("@fromID", SqlDbType.BigInt));
                            sqlCommand2.Parameters["@fromID"].Value = CallContext.LoginID;

                            sqlCommand2.Parameters.Add(new SqlParameter("@toID", SqlDbType.BigInt));
                            sqlCommand2.Parameters["@toID"].Value = mailClass.tomail;

                            sqlCommand2.Parameters.Add(new SqlParameter("@mailSubject", SqlDbType.NVarChar));
                            sqlCommand2.Parameters["@mailSubject"].Value = mailClass.subjectmail;

                            sqlCommand2.Parameters.Add(new SqlParameter("@mailBody", SqlDbType.NVarChar));
                            sqlCommand2.Parameters["@mailBody"].Value = mailClass.bodymail;

                            sqlCommand2.Parameters.Add(new SqlParameter("@mailFolder", SqlDbType.NVarChar));
                            sqlCommand2.Parameters["@mailFolder"].Value = "Inbox";

                            sqlCommand2.Parameters.Add(new SqlParameter("@viewStatus", SqlDbType.Bit));
                            sqlCommand2.Parameters["@viewStatus"].Value = false;

                            sqlCommand2.Parameters.Add(new SqlParameter("@fromDelete", SqlDbType.Bit));
                            sqlCommand2.Parameters["@fromDelete"].Value = false;

                            sqlCommand2.Parameters.Add(new SqlParameter("@toDelete", SqlDbType.Bit));
                            sqlCommand2.Parameters["@toDelete"].Value = false;

                            sqlCommand2.Parameters.Add(new SqlParameter("@onDate", SqlDbType.SmallDateTime));
                            sqlCommand2.Parameters["@onDate"].Value = DateTime.UtcNow;

                            try
                            {
                                conn.Open();

                                // Run the stored procedure.
                                sqlCommand2.ExecuteNonQuery();


                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Something Wrong! Please check after sometime");
                            }
                            finally
                            {
                                conn.Close();
                            }
                            notifications.Add(new NotificationAlertsDTO()
                            {
                                AlertStatusID = 1,
                                AlertTypeID = 3,
                                CreatedBy = (int?)CallContext.LoginID,
                                FromLoginID = CallContext.LoginID,
                                ToLoginID = Int64.Parse(mailClass.tomail),
                                NotificationDate = DateTime.Now,
                                ReferenceID = 0,
                                UpdatedBy = (int?)CallContext.LoginID,
                                UpdatedDate = DateTime.Now.Date,
                                CreatedDate = DateTime.Now.Date,
                                Message = "Message from Parent Portal ::" + " " + mailClass.subjectmail,
                            });
                            //set the inbox notification
                            Task.Factory.StartNew(() => new NotificationBL(context).SaveNotificationAlerts(notifications));
                        }
                        //End Insert/update schools.StudentRouteMonthlySplit  


                    }
                }
                catch { }
                return Json(true);
            }
            catch { }

            return Json(false);
        }

        public JsonResult SendComposeMail(string toEmail, string subject, string msg, string mailName, string fromEmail)
        {
            try
            {
                var mailHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMAILHOST_ADMISSION_APPLICATION").ToString();
                var mailPortNumber = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SMTPPORT_ADMISSION_APPLICATION").ToString();
                var SMTPUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SMTPUSERNAME_ADMISSION_APPLICATION").ToString();
                var SMTPPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SMTPPASSWORD_ADMISSION_APPLICATION").ToString();
                var senderEmailID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMAILID_ADMISSION_APPLICATION").ToString();

                if (string.IsNullOrEmpty(mailHost))
                {
                    mailHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILHOST").ToString();
                }

                if (string.IsNullOrEmpty(mailPortNumber))
                {
                    mailPortNumber = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SMTPPORT").ToString();
                }

                if (string.IsNullOrEmpty(SMTPUserName))
                {
                    SMTPUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SMTPUSERNAME").ToString();
                }

                if (string.IsNullOrEmpty(SMTPPassword))
                {
                    SMTPPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SMTPPASSWORD").ToString();
                }

                if (string.IsNullOrEmpty(mailName))
                {
                    mailName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILUSER").ToString();
                }

                if (string.IsNullOrEmpty(senderEmailID))
                {
                    senderEmailID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILID").ToString();
                }

                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                      | SecurityProtocolType.Tls11
                                                      | SecurityProtocolType.Tls12;
                }

                SmtpClient ss = new SmtpClient();
                ss.Host = mailHost;//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = string.IsNullOrEmpty(mailPortNumber) ? 587 : int.Parse(mailPortNumber);// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(SMTPUserName, toEmail, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailName);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {
                var errorMessage = string.Empty;
                if (ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception"))
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                Eduegate.Logger.LogHelper<string>.Fatal("Compose_Mail Error" + errorMessage, ex);

                return Json(ex.Message);
            }

            return Json("ok");
        }

        public class ComposeMailClass
        {
            public String frommail { get; set; }
            public String tomail { get; set; }
            public String subjectmail { get; set; }
            public String bodymail { get; set; }
            public Int64 toID { get; set; }
        }

        public JsonResult UserApplications()
        {
            var userDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentApplication(CallContext.LoginID.Value);

            if (userDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = userDetail });
            }
        }

        [HttpGet]
        public JsonResult GetDynamicLookUpData(DynamicLookUpType lookType, bool defaultBlank = true, bool isBlank = false, string searchText = "", string lookupName = "")
        {
            if (isBlank)
            {
                return Json(new KeyValueViewModel());
            }

            var VM = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(lookType, searchText));

            if (defaultBlank)
            {
                VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            }

            if (string.IsNullOrEmpty(lookupName))
            {
                return Json(VM);
            }
            else
            {
                return Json(new { LookUpName = lookupName, Data = VM });
            }
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            try
            {
                var VMS = KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.Countries, string.Empty));
                //var dtos = ClientFactory.CountryMasterServiceClient(CallContext).GetCountries();
                //var VM = dtos.Select(x => new KeyValueViewModel() { Key = x.CountryID.ToString(), Value = x.CountryName }).ToList();

                //VM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
                return Json(VMS);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<HomeController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() });
            }
        }

        [HttpGet]
        public JsonResult GetStudentsSiblings(long parentId)
        {
            var siblingsDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (siblingsDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = siblingsDetail });
            }
        }

        [HttpGet]
        public JsonResult GetCircularList()
        {
            var circularDetail = ClientFactory.SchoolServiceClient(CallContext).GetCircularList(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (circularDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = circularDetail });
            }
        }

        [HttpGet]
        public JsonResult GetGalleryView(long academicYearID)
        {
            var galleryView = ClientFactory.SchoolServiceClient(CallContext).GetGalleryView(academicYearID);

            if (galleryView == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = galleryView });
            }
        }

        [HttpGet]
        public JsonResult GetLessonPlanList(long studentID)
        {
            var lessonPlanDetail = ClientFactory.SchoolServiceClient(CallContext).GetLessonPlanList(studentID);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            if (lessonPlanDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = lessonPlanDetail });
            }
        }

        [HttpGet]
        public JsonResult GetAgendaList(long studentID)
        {
            var agendaList = ClientFactory.SchoolServiceClient(CallContext).GetAgendaList(studentID);

            if (agendaList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = agendaList });
            }
        }

        [HttpGet]
        public JsonResult FillFeeDue(long studentId)
        {

            List<StudentFeeDueDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillFeeDue(0, studentId);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            var feeTypes = new List<FeeCollectionFeeTypeViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeDueFeeTypeMap
                                         select new FeeCollectionFeeTypeViewModel()
                                         {
                                             //IsExpand = false,
                                             InvoiceNo = dat.InvoiceNo,
                                             Amount = feeType.FeeDueMonthlySplit.Count > 0 ? feeType.FeeDueMonthlySplit.Select(x => x.Balance).Sum() : feeType.Amount - feeType.CreditNoteAmount,
                                             FeePeriodID = feeType.FeePeriodID,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             //FeeMasterClassMapID = feeType.FeeMasterClassMapID,
                                             FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                                             InvoiceDateString = dat.InvoiceDate == null ? "" : Convert.ToDateTime(dat.InvoiceDate).ToString(dateFormat),
                                             FeeMaster = feeType.FeeMaster.Value,
                                             FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod.Value : null,
                                             FeeMasterID = int.Parse(feeType.FeeMaster.Key),

                                             FeeMonthly = (from split in feeType.FeeDueMonthlySplit
                                                           select new FeeAssignMonthlySplitViewModel()
                                                           {
                                                               Amount = split.Amount,
                                                               CreditNote = split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0,
                                                               Balance = split.Balance.HasValue ? split.Balance.Value : 0,
                                                               MonthID = split.MonthID,
                                                               Year = split.Year,
                                                               FeeDueMonthlySplitID = split.FeeDueMonthlySplitIID,
                                                               IsRowSelected = true,
                                                               MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year
                                                           }).ToList(),
                                         });

                feeTypes.AddRange(collectionFeeType);
            }

            if (feeTypes == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeTypes });
            }
        }

        [HttpGet]
        public JsonResult FillFineDue(long studentId)
        {

            List<StudentFeeDueDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillFineDue(0, studentId);

            var feeTypes = new List<FeeCollectionFineViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeFineMap
                                         select new FeeCollectionFineViewModel()
                                         {
                                             //IsExpand = false,
                                             InvoiceNo = dat.InvoiceNo,
                                             FineMapDateString = feeType.FineMapDate == null ? "" : Convert.ToDateTime(feeType.FineMapDate).ToString("dd/MMM/yyyy"),
                                             Amount = feeType.Amount,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                                             InvoiceDateString = dat.InvoiceDate == null ? "" : Convert.ToDateTime(dat.InvoiceDate).ToString("dd/MMM/yyyy"),
                                             Fine = feeType.FineName,
                                         });

                feeTypes.AddRange(collectionFeeType);
            }

            if (feeTypes == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeTypes });
            }
        }

        [HttpGet]
        public JsonResult GetStudentFeeCollection(long studentId)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetStudentFeeCollection(studentId);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            var feeTypes = new List<FeeCollectionHistoryFeeTypeViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeTypes
                                         select new FeeCollectionHistoryFeeTypeViewModel()
                                         {
                                             IsExpand = false,
                                             FeeReceiptNo = dat.FeeReceiptNo,
                                             CollectionDate = dat.CollectionDate == null ? "" : Convert.ToDateTime(dat.CollectionDate).ToString(dateFormat),
                                             Amount = feeType.Amount,
                                             FeeMaster = new KeyValueViewModel { Key = feeType.FeeMasterID.ToString(), Value = feeType.FeeMaster },
                                             FeePeriod = feeType.FeePeriodID.HasValue ? new KeyValueViewModel()
                                             {
                                                 Key = feeType.FeePeriodID.HasValue ? Convert.ToString(feeType.FeePeriodID) : null,
                                                 Value = !feeType.FeePeriodID.HasValue ? null : feeType.FeePeriod
                                             } : new KeyValueViewModel(),

                                             FeeMonthly = (from split in feeType.MontlySplitMaps
                                                           select new FeeAssignMonthlySplitViewModel()
                                                           {
                                                               Amount = split.Amount,
                                                               MonthID = split.MonthID,
                                                               CreditNote = split.CreditNoteAmount,
                                                               Balance = split.Balance,
                                                               MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year
                                                           }).ToList(),
                                         }); ;

                feeTypes.AddRange(collectionFeeType);
            }
            if (feeTypes == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeTypes });
            }

        }

        [HttpGet]
        public JsonResult GetFineCollected(long studentId)
        {
            List<StudentFeeDueDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetFineCollected(studentId);

            var feeTypes = new List<FeeCollectionFineViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeFineMap
                                         select new FeeCollectionFineViewModel()
                                         {
                                             InvoiceNo = dat.InvoiceNo,
                                             InvoiceDateString = dat.InvoiceDate == null ? "" : Convert.ToDateTime(dat.InvoiceDate).ToString("dd/MMM/yyyy"),
                                             FineMapDateString = feeType.FineMapDate == null ? "" : Convert.ToDateTime(feeType.FineMapDate).ToString("dd/MMM/yyyy"),
                                             Amount = feeType.Amount,
                                             Fine = feeType.FineName,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID

                                         });

                feeTypes.AddRange(collectionFeeType);
            }
            if (feeTypes == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeTypes });
            }

        }

        [HttpGet]
        public JsonResult GetMarkListStudentwise(long studentId)
        {
            List<MarkListViewDTO> dataList = ClientFactory.SchoolServiceClient(CallContext).GetMarkListStudentwise(studentId);

            var markLists = new List<MarkListViewModel>();
            foreach (var data in dataList)
            {
                var markList = new MarkListViewModel()
                {

                    Exam = new KeyValueViewModel()
                    {
                        Key = data.Exam.ToString(),
                        Value = data.Exam.Value
                    },
                    IsExpand = false,
                    MarkRegisterIID = data.MarkRegisterIID,
                    MarkRegisterDetailsSplit = (from marks in data.MarkSubjectDetails
                                                select new MarkListSubjectMapViewModel()
                                                {
                                                    Grade = marks.Grade,
                                                    Subject = marks.Subject,
                                                    IsPassed = marks.IsPassed,
                                                    IsAbsent = marks.IsAbsent,
                                                    SubjectID = marks.SubjectID,
                                                    MinimumMark = marks.MinimumMark,
                                                    MaximumMark = marks.MaximumMark,
                                                    MarkRegisterID = marks.MarkRegisterID,
                                                    MarksGradeMapID = marks.MarksGradeMapID,
                                                    Mark = marks.Mark.HasValue ? marks.Mark : (decimal?)null,
                                                    MarkRegisterSubjectMapIID = marks.MarkRegisterSubjectMapIID

                                                }).ToList()
                };
                markLists.Add(markList);
            }
            if (markLists == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = markLists });
            }

        }

        [HttpGet]
        public JsonResult GetAssignmentStudentwise(long studentId, int? SubjectID)
        {
            List<AssignmentDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetAssignmentStudentwise(studentId, SubjectID);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var AssignmentList = new List<AssignmentViewViewModel>();
            foreach (var assignment in data)
            {
                var ass = new AssignmentViewViewModel()
                {
                    AssignmentIID = Convert.ToInt32(assignment.AssignmentIID),
                    IsExpand = false,
                    Title = assignment.Title != null ? assignment.Title : null,
                    IsActive = assignment.IsActive != null ? assignment.IsActive : null,
                    Description = assignment.Description != null ? assignment.Description : null,
                    StartDateString = assignment.StartDate.Value.ToString(dateFormat),
                    FreezeDateString = assignment.FreezeDate.Value.ToString(dateFormat),
                    SubmissionDateString = assignment.DateOfSubmission.Value.ToString(dateFormat),
                    ClassID = string.IsNullOrEmpty(assignment.Class.Key) ? (int?)null : int.Parse(assignment.Class.Key),
                    SubjectID = string.IsNullOrEmpty(assignment.Subject.Key) ? (int?)null : int.Parse(assignment.Subject.Key),
                    SectionID = string.IsNullOrEmpty(assignment.Section.Key) ? (int?)null : int.Parse(assignment.Section.Key),
                    AcademicYearID = string.IsNullOrEmpty(assignment.AcademicYear.Key) ? (int?)null : int.Parse(assignment.AcademicYear.Key),
                    AssignmentTypeId = string.IsNullOrEmpty(assignment.AssignmentType.Key) ? (byte?)null : byte.Parse(assignment.AssignmentType.Key),
                    AssignmentStatusId = string.IsNullOrEmpty(assignment.AssignmentStatus.Key) ? (byte?)null : byte.Parse(assignment.AssignmentStatus.Key),
                    StudentClass = assignment.ClassID.HasValue ? new KeyValueViewModel() { Key = assignment.ClassID.ToString(), Value = assignment.Class.Value } : new KeyValueViewModel(),
                    Subject = assignment.SubjectID.HasValue ? new KeyValueViewModel() { Key = assignment.Subject.Key.ToString(), Value = assignment.Subject.Value } : new KeyValueViewModel(),
                    Section = assignment.SectionID.HasValue ? new KeyValueViewModel() { Key = assignment.Section.Key.ToString(), Value = assignment.Section.Value } : new KeyValueViewModel(),
                    Academic = assignment.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = assignment.AcademicYear.Key.ToString(), Value = assignment.AcademicYear.Value } : new KeyValueViewModel(),
                    AssignmentType = assignment.AssignmentTypeID.HasValue ? new KeyValueViewModel() { Key = assignment.AssignmentType.Key.ToString(), Value = assignment.AssignmentType.Value } : new KeyValueViewModel(),
                    CreatedDateString = assignment.CreatedDate.Value.ToString(dateFormat),
                    AssignmentStatus = assignment.AssignmentStatusID.HasValue ? new KeyValueViewModel() { Key = assignment.AssignmentStatus.Key.ToString(), Value = assignment.AssignmentStatus.Value } : new KeyValueViewModel(),
                    Attachments = (from attachment in assignment.AssignmentAttachmentMaps
                                   select new AssignmentAttachmentViewModel()
                                   {
                                       AssignmentAttachmentIID = attachment.AssignmentAttachmentMapIID,
                                       ContentFileIID = attachment.AttachmentReferenceID.HasValue ? attachment.AttachmentReferenceID : null,
                                       AssignmentID = attachment.AssignmentID.HasValue ? attachment.AssignmentID : null,
                                       ContentFileName = attachment.AttachmentName != null ? attachment.AttachmentName : null,
                                       Description = attachment.AttachmentDescription != null ? attachment.AttachmentDescription : null,
                                       Notes = attachment.Notes != null ? attachment.Notes : null,
                                   }).ToList(),
                };

                AssignmentList.Add(ass);
            }
            if (AssignmentList == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = AssignmentList });
            }

        }

        public JsonResult Getstudentsubjectlist(long studentId)
        {
            List<KeyValueDTO> data = ClientFactory.SchoolServiceClient(CallContext).Getstudentsubjectlist(studentId);


            if (data == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = data });
            }

        }

        [HttpGet]
        public JsonResult GetStudentDetails(long studentId)
        {
            List<StudentDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetStudentDetails(studentId);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            var StudentDetail = new List<StudentProfileViewModel>();
            foreach (var stud in data)
            {
                var std = new StudentProfileViewModel()
                {
                    StudentIID = stud.StudentIID,
                    AdmissionNumber = stud.AdmissionNumber,
                    FirstName = stud.FirstName,
                    MiddleName = stud.MiddleName,
                    LastName = stud.LastName,
                    StudentClass = stud.ClassID.HasValue ? new KeyValueViewModel() { Key = stud.ClassID.ToString(), Value = stud.ClassName } : new KeyValueViewModel(),
                    Section = stud.SectionID.HasValue ? new KeyValueViewModel() { Key = stud.SectionID.ToString(), Value = stud.SectionName } : new KeyValueViewModel(),
                    Gender = stud.GenderID.HasValue ? new KeyValueViewModel() { Key = stud.GenderID.Value.ToString(), Value = stud.GenderName } : new KeyValueViewModel(),
                    Category = stud.CategoryID.HasValue ? new KeyValueViewModel() { Key = stud.CategoryID.ToString(), Value = stud.CategoryName } : new KeyValueViewModel(),
                    Cast = stud.CastID.HasValue ? new KeyValueViewModel() { Key = stud.CastID.Value.ToString(), Value = stud.CastName } : new KeyValueViewModel(),
                    Relegion = stud.RelegionID.HasValue ? new KeyValueViewModel() { Key = stud.RelegionID.Value.ToString(), Value = stud.RelegionName } : new KeyValueViewModel(),
                    Community = stud.CommunityID.HasValue ? new KeyValueViewModel() { Key = stud.CommunityID.Value.ToString(), Value = stud.Community } : new KeyValueViewModel(),
                    BloodGroup = stud.BloodGroupID.HasValue ? new KeyValueViewModel() { Key = stud.BloodGroupID.Value.ToString(), Value = stud.BloodGroupName } : new KeyValueViewModel(),
                    StudentHouse = stud.StudentHouseID.HasValue ? new KeyValueViewModel() { Key = stud.StudentHouseID.Value.ToString(), Value = stud.StudentHouse } : new KeyValueViewModel(),
                    DateOfBirthString = stud.DateOfBirth.HasValue ? stud.DateOfBirth.Value.ToString(dateFormat) : null,
                    AsOnDateString = stud.AsOnDate.HasValue ? stud.AsOnDate.Value.ToString(dateFormat) : null,
                    AdmissionDateString = stud.AdmissionDate.HasValue ? stud.AdmissionDate.Value.ToString(dateFormat) : null,
                    School = stud.SchoolID.HasValue ? new KeyValueViewModel() { Key = stud.SchoolID.ToString(), Value = stud.SchoolName } : new KeyValueViewModel(),
                    LoginID = stud.LoginID.HasValue ? stud.LoginID : (long?)null,
                    EmailID = stud.EmailID,
                    AsOnDate = stud.AsOnDate,
                    DateOfBirth = stud.DateOfBirth,
                    MobileNumber = stud.MobileNumber,
                    StudentHouseID = stud.StudentHouseID,
                    Height = stud.Height,
                    Weight = stud.Weight,
                    PassportNo = stud.StudentPassportDetails?.PassportNo,
                    PassportNoExpiry = stud.StudentPassportDetails?.PassportNoExpiry,
                    AdhaarCardNo = stud.StudentPassportDetails?.AdhaarCardNo,
                    VisaNo = stud.StudentPassportDetails?.VisaNo,
                    VisaExpiry = stud.StudentPassportDetails?.VisaExpiry,
                    NationalIDNo = stud.StudentPassportDetails?.NationalIDNo,
                    NationalIDNoExpiry = stud.StudentPassportDetails?.NationalIDNoExpiry,
                };

                StudentDetail.Add(std);
            }
            if (StudentDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = StudentDetail });
            }

        }

        [HttpGet]
        public JsonResult GetStudentLeaveApplication(long studentId)
        {
            List<StudentLeaveApplicationDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetStudentLeaveApplication(studentId);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var StudentLeaveApplication = new List<StudentLeaveApplicationViewModel>();
            foreach (var studentleave in data)
            {
                var stdlv = new StudentLeaveApplicationViewModel()
                {
                    StudentID = studentleave.StudentID,
                    StudentLeaveApplicationIID = studentleave.StudentLeaveApplicationIID,
                    Reason = studentleave.Reason,
                    Remarks = studentleave.Remarks,
                    ClassName = studentleave.ClassID.HasValue ? studentleave.ClassName : null,
                    StudentLeaveStatus = studentleave.LeaveStatusID.HasValue ? studentleave.LeaveStatusDescription : null,
                    LeaveStatusID = studentleave.LeaveStatusID,
                    LeaveAppNumber = studentleave.LeaveAppNumber,
                    FromDateString = studentleave.FromDate.HasValue ? studentleave.FromDate.Value.ToString(dateFormat) : null,
                    ToDateString = studentleave.ToDate.HasValue ? studentleave.ToDate.Value.ToString(dateFormat) : null,
                    CreatedDateString = studentleave.CreatedDate.HasValue ? studentleave.CreatedDate.Value.ToString(dateFormat) : null,
                };

                StudentLeaveApplication.Add(stdlv);
            }
            if (StudentLeaveApplication == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = StudentLeaveApplication });
            }

        }

        public JsonResult GetClassTimeTable(long studentId)
        {
            List<TimeTableAllocationDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetClassTimeTable(studentId);

            var ClassTimeTable = new List<TimeTableAllocationViewModel>();
            foreach (var tta in data)
            {
                var ta = new TimeTableAllocationViewModel()
                {
                    TimeTableAllocationIID = tta.TimeTableAllocationIID,
                    TimeTableID = tta.TimeTableID,
                    WeekDayID = tta.WeekDayID,
                    ClassTimingID = tta.ClassTimingID,
                    SubjectID = tta.SubjectID,
                    StaffID = tta.StaffIDList,
                    SectionID = tta.SectionID,
                    ClassId = tta.ClassId,
                    WeekDay = tta.WeekDayID.HasValue ? new KeyValueViewModel() { Key = tta.WeekDayID.ToString(), Value = tta.WeekDay.Value } : new KeyValueViewModel(),
                    ClassTiming = tta.ClassTimingID.HasValue ? new KeyValueViewModel() { Key = tta.ClassTimingID.ToString(), Value = tta.ClassTiming.Value } : new KeyValueViewModel(),
                    Subject = tta.SubjectID.HasValue ? new KeyValueViewModel() { Key = tta.SubjectID.ToString(), Value = tta.Subject.Value } : new KeyValueViewModel() { Key = null, Value = tta.Subject.Value },
                    // Employee = tta.StaffID.HasValue ? new KeyValueViewModel() { Key = tta.StaffID.ToString(), Value = tta.Employee.Value } : new KeyValueViewModel() { Key = null, Value = tta.Employee.Value },
                    StudentClass = tta.ClassId.HasValue ? new KeyValueViewModel() { Key = tta.ClassId.ToString(), Value = tta.Class.Value } : new KeyValueViewModel(),
                    Section = tta.SectionID.HasValue ? new KeyValueViewModel() { Key = tta.SectionID.ToString(), Value = tta.Section.Value } : new KeyValueViewModel(),
                };

                ClassTimeTable.Add(ta);
            }
            if (ClassTimeTable == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = ClassTimeTable });
            }
        }

        public JsonResult GetExamLists(long studentId)
        {
            List<ExamDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetExamLists(studentId);

            var ExamLists = new List<ExamViewModel>();
            foreach (var exm in data)
            {
                var exList = new ExamViewModel()
                {
                    ExamIID = exm.ExamIID,
                    ExamDescription = exm.ExamDescription,
                    ExamTypeID = exm.ExamTypeID,
                    ExamTypes = exm.ExamTypeName,
                    ExamSubject = new ExamSubjectViewModel()
                    {
                        ExamSubjects = (from subject in exm.ExamSubjects
                                        select new ExamSubjectMapsViewModel()
                                        {
                                            ExamSubjectMapIID = subject.ExamSubjectMapIID,
                                            SubjectID = subject.SubjectID,
                                            //ExamDate = subject.ExamDate,
                                            ExamID = subject.ExamID,
                                            MinimumMarks = subject.MinimumMarks,
                                            MaximumMarks = subject.MaximumMarks,
                                            //ExamDateString = subject.ExamDate.HasValue ? subject.ExamDate.Value.ToLongDateString() : null,
                                            Subject = subject.SubjectID.HasValue ? new KeyValueViewModel() { Key = subject.SubjectID.ToString(), Value = subject.Subject } : new KeyValueViewModel(),
                                            //StartTimeString = subject.StartTime.HasValue ? subject.StartTime.Value.ToString() : null,
                                            //EndTimeString = subject.EndTime.HasValue ? subject.EndTime.Value.ToString() : null,
                                        }).ToList(),
                    }
                };

                ExamLists.Add(exList);
            }
            if (ExamLists == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = ExamLists });
            }
        }

        public ActionResult StudentTransferApplication(long Id = 0)
        {
            var vm = new StudentTransferRequestViewModel();

            if (Id != 0)
            {
                var StudentTransferRequest = ClientFactory.SchoolServiceClient(CallContext).GetTransferApplication(Id);
                if (StudentTransferRequest != null)
                {
                    vm = vm.ToVM(StudentTransferRequest);
                }
            }

            return Json(new { IsError = false, Response = vm });
            //return View("StudentTCApplication", vm);
        }

        public ActionResult GetTransferApplication(long Id)
        {
            var TransferApplication = ClientFactory.SchoolServiceClient(CallContext).GetTransferApplication(Id);
            return Json(new { IsError = false, Response = TransferApplication });
        }

        public ActionResult DeleteTransferApplication(long Id)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteTransferApplication(Id);
            return Redirect("~/Home/Mywards");
        }

        [HttpPost]
        public ActionResult SubmitTransferApplication([FromBody] StudentTransferRequestViewModel TransferApplication)
        {
            //TransferApplication.ExpectingRelivingDateString = TransferApplication.ExpectingRelivingDateString != null ? Convert.ToDateTime(TransferApplication.ExpectingRelivingDateString).ToString("dd/MM/yyyy") : null;

            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.StudentTransferRequest, Data = TransferApplication.AsDTOString(TransferApplication.ToDTO(CallContext)) });

            //return Redirect("~/Home/Mywards");
            return Json(new { IsError = false, Response = crudSave });

        }

        [HttpGet]
        public JsonResult GetStudentTransferApplication(long studentId)
        {
            List<StudentTransferRequestDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetStudentTransferApplication(studentId);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var StudentTransferRequestApplication = new List<StudentTransferRequestViewModel>();
            foreach (var studenttransfer in data)
            {
                var stdlv = new StudentTransferRequestViewModel()
                {
                    StudentTransferRequestIID = studenttransfer.StudentTransferRequestIID,
                    StudentID = studenttransfer.StudentID,
                    OtherReason = studenttransfer.OtherReason,
                    TCAppNumber = studenttransfer.TCAppNumber,
                    TransferRequestStatus = studenttransfer.TransferRequestStatusDescription,
                    TransferRequestStatusID = studenttransfer.TransferRequestStatusID,
                    ExpectingRelivingDate = studenttransfer.ExpectingRelivingDate,
                    ExpectingRelivingDateString = studenttransfer.ExpectingRelivingDate.HasValue ? studenttransfer.ExpectingRelivingDate.Value.ToString(dateFormat) : null,
                    CreatedDateString = studenttransfer.CreatedDate.HasValue ? studenttransfer.CreatedDate.Value.ToString(dateFormat) : null,
                    TransferRequestReasonID = studenttransfer.TransferRequestReasonID,
                    StudentTransferRequestReasons = studenttransfer.StudentTransferRequestReasons,
                };

                StudentTransferRequestApplication.Add(stdlv);
            }
            if (StudentTransferRequestApplication == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = StudentTransferRequestApplication });
            }

        }

        public ActionResult NewApplicationFromSibling(long loginID)
        {
            var vm = new StudentApplicationViewModel();

            var academicYear = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.AcademicYear, string.Empty).LastOrDefault();
            var schoolSyllabus = ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(DynamicLookUpType.SchoolSyllabus, string.Empty).LastOrDefault();

            if (loginID != 0)
            {
                var application = ClientFactory.SchoolServiceClient(CallContext).GetApplicationByLoginID(loginID);

                if (application != null)
                {
                    if (academicYear != null)
                    {
                        application.SchoolAcademicyear = academicYear.Value;
                        application.SchoolAcademicyearID = int.Parse(academicYear.Key);
                    }
                    application.Curriculam = schoolSyllabus.Value;
                    application.CurriculamID = byte.Parse(schoolSyllabus.Key);

                    vm = vm.ToVM(application);
                }
            }
            return View("NewApplicationFromParent", vm);
        }

        public ActionResult ApplicationListFromSibling(long Id = 0)
        {
            return View("ApplicationListFromParent", "_Layout");
        }

        public ActionResult CircularList(long Id = 0)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            return View("_CircularList_Parent", "_Layout");
        }

        public JsonResult GetApplicationByLoginID(long loginID)
        {
            StudentApplicationDTO data = ClientFactory.SchoolServiceClient(CallContext).GetApplicationByLoginID(loginID);

            var ApplicationList = new StudentApplicationViewModel();
            if (ApplicationList == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = ApplicationList });
            }
        }

        [HttpGet]
        public JsonResult GetTeacherDetails(long studentId)
        {
            List<ClassClassTeacherMapDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetTeacherDetails(studentId);

            var TeacherDetail = new List<ClassTeacherMapViewModel>();
            foreach (var teacher in data)
            {
                var tr = new ClassTeacherMapViewModel()
                {
                    //ClassTeacherMapIID = teacher.ClassTeacherMapIID,
                    //Class = teacher.ClassID.HasValue ? new KeyValueViewModel() { Key = teacher.ClassID.ToString(), Value = teacher.ClassName } : new KeyValueViewModel(),
                    //Section = teacher.SectionID.HasValue ? new KeyValueViewModel() { Key = teacher.SectionID.ToString(), Value = teacher.SectionName } : new KeyValueViewModel(),
                    Employee = teacher.ClassTeacherID.HasValue ? new KeyValueViewModel() { Key = teacher.ClassTeacherID.Value.ToString(), Value = teacher.HeadTeacherName } : new KeyValueViewModel(),
                    SubjectName = teacher == null || teacher.SubjectName == null ? null : teacher.SubjectName,
                    //HighestAcademicQualitication = teacher == null || teacher.HighestAcademicQualitication == null ? null : teacher.HighestAcademicQualitication,
                    //Description = teacher == null || teacher.GenderDescription == null ? null : teacher.GenderDescription,
                    EmployeePhoto = teacher == null || teacher.EmployeePhoto == null ? null : teacher.EmployeePhoto,
                    //EmployeeID = teacher == null || teacher.EmployeeID == null ? null : teacher.EmployeeID,
                    WorkEmail = teacher == null || teacher.WorkEmail == null ? null : teacher.WorkEmail,
                };

                TeacherDetail.Add(tr);
            }
            if (TeacherDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = TeacherDetail });
            }
        }

        [HttpGet]
        public JsonResult GetStudentSkillRegister(long studentId, int ClassID)
        {
            ProgressReportDTO dataList = ClientFactory.SchoolServiceClient(CallContext).GetStudentSkillRegister(studentId, ClassID);

            var markLists = new List<MarkListViewModel>();

            if (markLists == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = dataList });
            }

        }

        public JsonResult GetNotificationAlerts()
        {
            var notificationAlerts = ClientFactory.SchoolServiceClient(CallContext).GetNotificationAlerts(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (notificationAlerts == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = notificationAlerts });
            }
        }

        public JsonResult GetAllNotificationAlerts()
        {
            var notificationAlerts = ClientFactory.SchoolServiceClient(CallContext).GetAllNotificationAlerts(CallContext.LoginID.Value);

            if (notificationAlerts == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = notificationAlerts });
            }
        }

        public JsonResult GetSendMailFromParent()
        {
            var notificationAlerts = ClientFactory.SchoolServiceClient(CallContext).GetSendMailFromParent(CallContext.LoginID.Value);

            if (notificationAlerts == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = notificationAlerts });
            }
        }

        public ActionResult FeepaymentGateway()
        {
            FeeCollectionDTO obj = new FeeCollectionDTO();
            obj.AdmissionNo = "testdata";
            ViewData["payde"] = obj;
            //return View("FeePaymentGateway");
            return View();
        }

        public ActionResult PaymentGateway(decimal? totalAmount)
        {
            ViewBag.TotalAmountToBePaid = totalAmount;

            return View();
        }

        public ActionResult SaveFeepaymentGateway()
        {
            var obj = (FeeCollectionDTO)ViewData["payde"];
            // save operatipon 
            return View("FeePaymentGateway");

        }

        public ActionResult AssignmentDocument()
        {
            return View();
        }
    

        //[HttpPost]
        //public JsonResult AssignmentDocument(HttpPostedFileBase DocumentPath)
        //{
        //    return Json(new { IsError = false, Response = "" });
        //}

        //TODO: Need to Check later
        //public String singlefileupload(Int64 DocumentID, HttpPostedFileBase PostedFile, String path, String ext, String documentname)
        //{

        //    //tbl_documents document = new tbl_documents();
        //    try
        //    {

        //        // NewsImagePath.SaveAs(path + "News_Image_" + NewsID + ext);
        //        try
        //        {
        //            System.Drawing.Image image1 = System.Drawing.Image.FromStream(PostedFile.InputStream);
        //            //ImageSizeReduce(image1, "~/Pictures/Gallery/", shortName + "_000" + tbl_Gallery.GalleryID + ext);
        //            //Bitmap bmp = Resize(new Bitmap(image1), 800, 600, 90);
        //            // PostedFile.SaveAs("~/Document/" + documentname);
        //            PostedFile.SaveAs(path + documentname);
        //        }
        //        catch { PostedFile.SaveAs(path + documentname); }

        //    }
        //    catch { }
        //    return "";
        //}


        [HttpPost]
        public ActionResult SubmitFeePayment(FeePaymentViewModel paymentViewModel)
        {
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.FeePayment, Data = paymentViewModel.AsDTOString(paymentViewModel.ToDTO(CallContext)) });
            return Json(new { IsError = false, Response = crudSave });
        }

        [HttpGet]
        public JsonResult GetCastByRelegion(int relegionID)
        {
            var castList = ClientFactory.SchoolServiceClient(CallContext).GetCastByRelegion(relegionID);

            if (castList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = castList });
            }
        }

        [HttpGet]
        public JsonResult GetStreamByStreamGroup(byte? streamGroupID)
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetStreamByStreamGroup(streamGroupID);

            if (streamList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = streamList });
            }
        }

        [HttpGet]
        public JsonResult GetStreamCompulsorySubjects(byte? streamID)
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetStreamCompulsorySubjects(streamID);

            if (streamList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = streamList });
            }
        }

        [HttpGet]
        public JsonResult GetStreamOptionalSubjects(byte? streamID)
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetStreamOptionalSubjects(streamID);

            if (streamList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = streamList });
            }
        }

        [HttpGet]
        public JsonResult GetFullStreamListDatas()
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetFullStreamListDatas();

            if (streamList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = streamList });
            }
        }


        [HttpGet]
        public JsonResult GetClasseByAcademicyear(int academicyearID)
        {
            var classList = ClientFactory.SchoolServiceClient(CallContext).GetClasseByAcademicyear(academicyearID);

            if (classList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = classList });
            }
        }

        [HttpGet]
        public JsonResult GetClassesBySchool(byte schoolID)
        {
            var classList = ClientFactory.SchoolServiceClient(CallContext).GetClassesBySchool(schoolID);

            if (classList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = classList });
            }
        }

        [HttpGet]
        public JsonResult GetAcademicYearBySchool(int schoolID)
        {
            var yearList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearBySchool(schoolID);

            if (yearList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = yearList });
            }
        }


        [HttpGet]
        public JsonResult GetAboutandContactDetails(long contentID)
        {
            var aboutandContactDetail = ClientFactory.SchoolServiceClient(CallContext).GetAboutandContactDetails(contentID);

            if (aboutandContactDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = aboutandContactDetail });
            }
        }

        public JsonResult GetTeacherEmailByParentLoginID()
        {
            var ResModel = new ResponseModel();
            List<TEACHERS_MAIL_ID_BY_PARENT_LOGINID> mailList = new List<TEACHERS_MAIL_ID_BY_PARENT_LOGINID>();

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("[schools].[GET_ALL_TEACHERS_MAIL_ID_BY_PARENT_LOGINID]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginID", CallContext.LoginID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var mailItem = new TEACHERS_MAIL_ID_BY_PARENT_LOGINID
                                {
                                    LoginEmailID = (string)reader["LoginEmailID"],
                                    EmployeeName = (string)reader["EmployeeName"],
                                    EmployeeLoginID = (long)reader["EmployeeLoginID"],
                                    // Map other properties
                                };

                                mailList.Add(mailItem);
                            }
                        }
                    }

                    if (mailList.Count > 0)
                    {
                        ResModel.Data = new
                        {
                            mailList
                        };
                    }
                }
                catch (Exception e)
                {
                    string message = string.Format(e.Message + ", " + e.InnerException?.Message);
                    ResModel.Message = message;
                    ResModel.Data = "";
                }
            }

            return Json(ResModel);
        }

        [HttpGet]
        public JsonResult GetGuardianDetails(long studentId)
        {
            GuardianDTO data = ClientFactory.SchoolServiceClient(CallContext).GetGuardianDetails(studentId);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            var guardianDetails = new GaurdianViewModel()
            {
                ParentIID = data.ParentIID,
                ParentCode = data.ParentCode,
                FatherFirstName = data.FatherFirstName,
                FatherMiddleName = data.FatherMiddleName,
                FatherLastName = data.FatherLastName,
                MotherFirstName = data.MotherFirstName,
                MotherMiddleName = data.MotherMiddleName,
                MotherLastName = data.MotherLastName,
                GuardianFirstName = data.GuardianFirstName,
                GuardianMiddleName = data.GuardianMiddleName,
                GuardianLastName = data.GuardianLastName,
                FatherEmailID = data.FatherEmailID ?? "NA",
                GaurdianEmail = data.GaurdianEmail ?? "NA",
                MotherEmailID = data.MotherEmailID ?? "NA",
                MotherPhone = data.MotherPhone ?? "NA",
                GuardianPhone = data.GuardianPhone ?? "NA",
                FatherPhoneNumber = data.PhoneNumber ?? "NA",
                FatherMobileNumberTwo = data.FatherMobileNumberTwo ?? "NA",
                FatherPassportNumber = data.FatherPassportNumber ?? "NA",
                MotherPassportNumber = data.MotherPassportNumber ?? "NA",
                MotherNationalID = data.MotherNationalID,
                FatherNationalID = data.FatherNationalID,
                FatherCompanyName = data.FatherCompanyName ?? "NA",
                MotherCompanyName = data.MotherCompanyName ?? "NA",
                MotherCountry = data.MotherCountry ?? "NA",
                FatherCountry = data.FatherCountry ?? "NA",
                FatherOccupation = data.FatherOccupation ?? "NA",
                MotherOccupation = data.MotherOccupation ?? "NA",
                FatherPassportNoExpiryString = data.FatherPassportNoExpiryDate.HasValue ? data.FatherPassportNoExpiryDate.Value.ToString(dateFormat) : "NA",
                FatherPassportNoIssueString = data.FatherPassportNoIssueDate.HasValue ? data.FatherPassportNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherPassportNoExpiryString = data.MotherPassportNoExpiryDate.HasValue ? data.MotherPassportNoExpiryDate.Value.ToString(dateFormat) : "NA",
                MotherPassportNoIssueString = data.MotherPassportNoIssueDate.HasValue ? data.MotherPassportNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherNationalDNoIssueDateString = data.MotherNationalDNoIssueDate.HasValue ? data.MotherNationalDNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherNationaIDNoExpiryDateString = data.MotherNationalDNoExpiryDate.HasValue ? data.MotherNationalDNoExpiryDate.Value.ToString(dateFormat) : "NA",
                FatherNationalDNoIssueDateString = data.FatherNationalDNoIssueDate.HasValue ? data.FatherNationalDNoIssueDate.Value.ToString(dateFormat) : "NA",
                FatherNationalDNoExpiryDateString = data.FatherNationalDNoExpiryDate.HasValue ? data.FatherNationalDNoExpiryDate.Value.ToString(dateFormat) : "NA",
                LocationNo = data.LocationNo ?? "NA",
                BuildingNo = data.BuildingNo ?? "NA",
                FlatNo = data.FlatNo ?? "NA",
                StreetName = data.StreetName ?? "NA",
                StreetNo = data.StreetNo ?? "NA",
                ZipNo = data.ZipNo ?? "NA",
                LocationName = data.LocationName ?? "NA",
                PostBoxNo = data.PostBoxNo ?? "NA",
                City = data.City ?? "NA",
                CountryID = data.CountryID,
                FatherCountryID = data.FatherCountryID,
                MotherCountryID = data.MotherCountryID,
                GuardianTypeID = data.GuardianTypeID,
                Country = data.Country ?? "NA",
            };

            if (guardianDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = guardianDetails });
            }

        }

        [HttpPost]
        public ActionResult AddLead(LeadInfoViewModel leadInfo)
        {
            dynamic response = new { IsError = false, Message = "Successfully Registered" };
            try
            {

                var rtnValue = ClientFactory.CRMServiceClient(null).AddLead(new LeadDTO()
                {
                    LeadTypeID = 1,
                    DateOfBirth = Convert.ToDateTime(leadInfo.Dob1),
                    MobileNumber = leadInfo.Mobile,
                    ParentName = leadInfo.Pname,
                    GenderID = leadInfo.Gender == "M" ? (byte?)1 : (byte?)2,
                    EmailAddress = leadInfo.Email,
                    ClassName = leadInfo.Grade,
                    ClassID = int.Parse(leadInfo.Grade),
                    StudentName = leadInfo.Sname,
                    AcademicYear = leadInfo.Academic_year,
                    AcademicYearID = int.Parse(leadInfo.Academic_year),
                    NationalityID = int.Parse(leadInfo.Nationality),
                    LeadContact = new LeadContactDTO() { MobileNo1 = leadInfo.Mobile },
                    RequestTypeID = byte.Parse(leadInfo.Referal_code),
                    LeadStatusID = 1,
                    LeadName = leadInfo.Grade + "-" + leadInfo.Sname,
                    ReferalCode = leadInfo.Referal_code,
                    SchoolID = byte.Parse(leadInfo.SchoolID),
                    CurriculamID = 1,
                    LeadSourceID = int.Parse(leadInfo.Referal_code),

                });


            }
            catch (Exception ex)
            {
                response = new { IsError = true, Message = ex.Message };

            }
            return Json(response);

        }

        [HttpGet]

        public JsonResult GetAcademicYear(byte schoolID)
        {
            var academicyearList = ClientFactory.CRMServiceClient(null).GetAcademicYearCodeBySchool(schoolID);
            return Json(academicyearList);

        }
        [HttpGet]
        public JsonResult GetClasses(byte schoolID)
        {
            var classes = ClientFactory.CRMServiceClient(null).GetClassesBySchool(schoolID, 0);
            return Json(classes);

        }
        [HttpGet]
        public JsonResult GetLeadSource()
        {
            var sources = ClientFactory.CRMServiceClient(null).GetLeadSource();

            return Json(sources);
        }

        [HttpGet]
        public JsonResult GetNationalities()
        {
            var sources = ClientFactory.CRMServiceClient(null).GetNationalities();

            return Json(sources);
        }

        [HttpGet]
        public JsonResult GetDeafaultSchool()
        {
            var school = ClientFactory.CRMServiceClient(null).GetDeafaultSchool();

            return Json(school);

        }

        public void ReadContentsByID(long? contentID)
        {
            if (contentID.HasValue)
            {
                var imageData = ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value);

                if (imageData.ContentFileName.EndsWith(".pdf"))
                {
                    Response.ContentType = "application/pdf";
                }

                else if (imageData.ContentFileName.EndsWith(".xls") || imageData.ContentFileName.EndsWith(".xlsx"))
                {
                    Response.ContentType = "Application/x-msexcel";
                }

                else if (imageData.ContentFileName.EndsWith(".doc") || imageData.ContentFileName.EndsWith(".rtf") || imageData.ContentFileName.EndsWith(".docx"))
                {
                    Response.ContentType = "Application/msword";
                }

                else if (imageData.ContentFileName.EndsWith(".gif"))
                {
                    Response.ContentType = "image/GIF";
                }

                else if (imageData.ContentFileName.EndsWith(".htm") || imageData.ContentFileName.EndsWith(".html"))
                {
                    Response.ContentType = "text/HTML";
                }

                else if (imageData.ContentFileName.EndsWith(".txt"))
                {
                    Response.ContentType = "text/plain";
                }

                else if (imageData.ContentFileName.EndsWith(".jpeg") || imageData.ContentFileName.EndsWith(".jfif") || imageData.ContentFileName.EndsWith(".webp") || imageData.ContentFileName.EndsWith(".jpg"))
                {
                    Response.ContentType = "image/jpeg";
                }

                else
                {
                    Response.ContentType = "image/pmg";
                }
                //Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData.ContentFileName }.ToString());
                //Response.BinaryWrite(imageData.ContentData);
                HttpContext.Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData != null ? imageData.ContentFileName : "No file" }.ToString());

                if (imageData != null)
                {
                    // Check if the content data is compressed
                    if (IsCompressed(imageData.ContentData))
                    {
                        // Decompress the content data
                        byte[] decompressedData = Decompress(imageData.ContentData);
                        Response.Body.WriteAsync(decompressedData);
                    }
                    else
                    {
                        // If not compressed, write the data directly
                        Response.Body.WriteAsync(imageData.ContentData);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult AddLeadExternal(string Name, string Email, string Mobile, string Grade, string Query, string ReferalCode
            , string Dob, string Pname, string Gender, string AcademicYear, string Adgroup, string Campaign, string Keyword, string Adtype/*,string Nationality=null*/)
        {
            dynamic response = new { IsError = false, Message = "Successfully Registered" };
            try
            {
                var rtnValue = ClientFactory.CRMServiceClient(null).AddLead(new LeadDTO()
                {
                    LeadTypeID = 1,
                    DateOfBirth = Convert.ToDateTime(Dob),
                    MobileNumber = Mobile,
                    ParentName = Pname.ToUpper(),
                    GenderID = Gender.ToLower() == "male" ? (byte?)1 : (byte?)2,
                    EmailAddress = Email.ToUpper(),
                    ClassName = Grade,
                    //                    ClassID = int.Parse(leadInfo.Grade),
                    StudentName = Name.ToUpper(),
                    AcademicYear = AcademicYear,
                    //AcademicYearID = int.Parse(leadInfo.Academic_year),
                    LeadContact = new LeadContactDTO() { MobileNo1 = Mobile },
                    // RequestTypeID = byte.Parse(leadInfo.Referal_code),
                    LeadStatusID = 1,
                    LeadName = Grade,
                    ReferalCode = ReferalCode,
                    //SchoolID = byte.Parse(leadInfo.SchoolID),
                    CurriculamID = 1,
                    //LeadSourceID = int.Parse(leadInfo.Referal_code),
                    Remarks = Query,
                    // NationalityID = string.IsNullOrEmpty(Nationality) ? (int?)null:int.Parse(Nationality),

                });


            }
            catch (Exception ex)
            {
                response = new { IsError = true, Message = ex.Message };
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            return Json(response);

        }

        public ActionResult StudentTransportRequestList()
        {
            return View();
        }

        public ActionResult StudentTransportRequestApplication()
        {
            return View();
        }

        public JsonResult GetUserTransportApplications()
        {
            var detail = ClientFactory.SchoolServiceClient(CallContext).GetTransportApplication(CallContext.LoginID.Value);

            var listDatas = detail.OrderByDescending(o => o.TransportApplctnStudentMapIID).ToList();

            if (listDatas == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = listDatas });
            }
        }

        [HttpGet]
        public TransportApplicationViewModel GetStudentDetailsForTransportApplication(long id)
        {
            TransportApplicationDTO data = ClientFactory.SchoolServiceClient(CallContext).GetTransportStudentDetailsByParentLoginID(id);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            var transportStudentMap = new List<TransportApplicationStudentMapViewModel>();
            var applicationDetails = new TransportApplicationViewModel();

            foreach (var stud in data.TransportApplicationStudentMaps)
            {
                transportStudentMap.Add(new TransportApplicationStudentMapViewModel()
                {
                    TransportApplctnStudentMapIID = stud.TransportApplctnStudentMapIID,
                    TransportApplicationID = stud.TransportApplicationID,
                    StudentID = stud.StudentID,
                    FirstName = stud.FirstName,
                    MiddleName = stud.MiddleName,
                    LastName = stud.LastName,
                    ClassID = stud.ClassID,
                    ApplicationStatus = stud.ApplicationStatus,
                    CheckBoxStudent = stud.CheckBoxStudent,
                    ClassName = stud.ClassName,
                    //Class = stud.ClassID.HasValue ? new KeyValueViewModel() { Key = stud.ClassID.ToString(), Value = stud.ClassName} : new KeyValueViewModel(),
                    GenderID = stud.GenderID,
                    Gender = stud.GenderID.HasValue ? stud.GenderName : null,
                    SchoolID = stud.SchoolID,
                    IsNewRider = stud.IsNewRider,
                    LocationChange = stud.LocationChange,
                    IsActive = stud.IsActive,
                    StartDate = stud.StartDate,
                    TransportApplcnStatusID = stud.TransportApplcnStatusID,
                    StartDateString = stud.StartDate.HasValue ? stud.StartDate.Value.ToString("yyyy-MM-dd") : null,
                    IsMedicalCondition = stud.IsMedicalCondition,
                    CreatedByID = stud?.CreatedBy,
                    CreateDate = stud.CreatedDate,
                    Remarks = stud.Remarks,
                    CreatedDateString = stud.CreatedDate.HasValue ? stud.CreatedDate.Value.ToString(dateFormat) : null,
                });
            }

            applicationDetails = new TransportApplicationViewModel()
            {
                TransportApplicationIID = data.TransportApplicationIID,
                ApplicationNumber = data.ApplicationNumber,
                LoginID = data.LoginID,
                ParentID = data.ParentID,
                FatherName = data.FatherName,
                MotherName = data.MotherName,
                FatherEmailID = data.FatherEmailID,
                FatherContactNumber = data.FatherContactNumber,
                MotherEmailID = data.MotherEmailID,
                MotherContactNumber = data.MotherContactNumber,
                EmergencyEmailID = data.EmergencyEmailID,
                EmergencyContactNumber = data.EmergencyContactNumber,
                LandMark = data.LandMark,
                LocationName = data.LocationName,
                Building_FlatNo = data.Building_FlatNo,
                StreetNo = data.StreetNo,
                ZoneNo = data.ZoneNo,
                Remarks = data.Remarks,
                //SchoolID = data.SchoolID,
                //School = data.SchoolID.HasValue ? data.SchoolID.ToString() : null,
                CreatedBy = data?.CreatedBy,
                CreatedDate = data?.CreatedDate,
                UpdatedBy = data?.UpdatedBy,
                UpdatedDate = data?.UpdatedDate,
                TransportApplicationStudentMaps = transportStudentMap,
            };

            //if (applicationDetails == null)
            //{
            //    return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            //}
            //else
            //{
            //    return Json(new { IsError = false, Response = applicationDetails });
            //}

            return applicationDetails;

        }

        [HttpPost]

        public ActionResult SubmitTransportApplication([FromBody] TransportApplicationViewModel transportApplication)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            foreach (var data in transportApplication.TransportApplicationStudentMaps)
            {
                if (data.StartDateString != null)
                {
                    data.StartDateString = Convert.ToDateTime(data.StartDateString).ToString(dateFormat);
                }
            }
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.TransportApplication, Data = transportApplication.AsDTOString(transportApplication.ToDTO(CallContext)) });

            if (crudSave.IsError == true)
            {
                return Json(new { IsError = true, Response = crudSave.ErrorMessage });
            }
            else
            {
                return Redirect("~/Home/StudentTransportRequestList");
            }
            //return Json(new { IsError = false, Response = crudSave });

        }

        [HttpGet]
        public string GetProgressReport(long studentId)
        {
            StudentDTO stud = ClientFactory.SchoolServiceClient(CallContext).GetStudentDetails(studentId).FirstOrDefault();
            string reportName = ClientFactory.SchoolServiceClient(CallContext).GetProgressReportName(stud.StudentIID, stud.ClassID);

            var progressReportUrl = String.Format("/Reports/ReportViewer.aspx?reportName={0}&SCHOOLID={1}&ACDEMICYEARID={2}&CLASSIDs={3}&SECTIONIDs={4}&STUDENT_IDs={5}"
                , reportName, stud.SchoolID, stud.SchoolAcademicyearID, stud.ClassID, stud.SectionID, stud.StudentIID);
            return progressReportUrl;
        }

        [HttpGet]
        public JsonResult GetSchoolsByParentLoginID(long? loginID)
        {
            var schoolLists = ClientFactory.SchoolServiceClient(CallContext).GetSchoolsByParentLoginID(loginID);
            return Json(schoolLists);

        }

        [HttpGet]
        public JsonResult GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, string dobString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");
            DateTime? dateofBirth = string.IsNullOrEmpty(dobString) ? (DateTime?)null : DateTime.ParseExact(dobString, dateFormat, CultureInfo.InvariantCulture);
            var dobData = ClientFactory.SchoolServiceClient(CallContext).GetAgeCriteriaDetails(classID, academicID, schoolID, dateofBirth);

            var ageCriteriaWaringMessage = dobData.AgeCriteriaWarningMsg;
            return Json(ageCriteriaWaringMessage);

        }

        [HttpGet]
        public JsonResult GetAllAcademicYearBySchoolID(int schoolID)
        {
            var academicYearList = ClientFactory.SchoolServiceClient(CallContext).GetAllAcademicYearBySchoolID(schoolID);

            if (academicYearList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = academicYearList });
            }
        }

        public ActionResult DeleteTransportApplication(long Id)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            ClientFactory.SchoolServiceClient(CallContext).DeleteTransportApplication(Id);
            return Redirect("~/Home/StudentTransportRequestList");
        }

        public ActionResult StudentPickupRequestList()
        {
            return View();
        }

        public ActionResult StudentPickupRequest()
        {
            return View();
        }
      
     

        public JsonResult GetStudentPickupRequests()
        {
            try
            {
                var pickupRequests = ClientFactory.SchoolServiceClient(CallContext).GetStudentPickupRequestsByLoginID(CallContext.LoginID.Value);

                if (pickupRequests == null)
                {
                    return Json(new { IsError = true, Response = "Something went wrong!" });
                }
                else
                {
                    return Json(new { IsError = false, Response = pickupRequests });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }

        }

        [HttpPost]
        public ActionResult SubmitStudentPickupRequest([FromBody] StudentPickupRequestViewModel studentPickupRequest)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                if (!string.IsNullOrEmpty(studentPickupRequest.RequestDateString))
                {
                    studentPickupRequest.RequestDateString = Convert.ToDateTime(studentPickupRequest.RequestDateString).ToString(dateFormat);
                }
                if (!string.IsNullOrEmpty(studentPickupRequest.PickedDateString))
                {
                    studentPickupRequest.PickedDateString = Convert.ToDateTime(studentPickupRequest.PickedDateString).ToString(dateFormat);
                }

                var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.StudentPickupRequest, Data = studentPickupRequest.AsDTOString(studentPickupRequest.ToDTO(CallContext)) });

                if (crudSave.IsError == true)
                {
                    return Json(new { IsError = true, Response = crudSave.ErrorMessage });
                }
                else
                {
                    return Json(new { IsError = false, Response = "Successfully Saved" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        public JsonResult CancelStudentPickupRequestByID(long pickupRequestID)
        {
            try
            {
                var status = ClientFactory.SchoolServiceClient(CallContext).CancelStudentPickupRequestByID(pickupRequestID);

                if (status == null)
                {
                    return Json(new { IsError = true, Response = "Something went wrong!" });
                }
                else
                {
                    return Json(new { IsError = false, Response = status });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }

        }

        [HttpGet]
        public JsonResult GetReportCardList(long studentID, int classID, int sectionID, int academicYearID)
        {
            var reportCards = ClientFactory.SchoolServiceClient(CallContext).GetProgressReportList(studentID, classID, sectionID, academicYearID);

            if (reportCards == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = reportCards });
            }
        }

        public ActionResult TransportApplicationClick(long id)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch { }
            var vm = new TransportApplicationViewModel();

            var application = GetStudentDetailsForTransportApplication(id);

            if (application != null)
            {
                vm = application;
            }

            return View("StudentTransportRequestApplication", vm);
        }

        public ActionResult CancelTransportApplication(long mapIID)
        {
            //try
            //{
            //    if (CallContext.LoginID == null)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //}
            //catch { }
            //ClientFactory.SchoolServiceClient(CallContext).CancelTransportApplication(Id);
            //return Redirect("~/Home/StudentTransportRequestList");


            try
            {
                var status = ClientFactory.SchoolServiceClient(CallContext).CancelTransportApplication(mapIID);

                if (status == null)
                {
                    return Json(new { IsError = true, Response = "Something went wrong!" });
                }
                else
                {
                    return Json(new { IsError = false, Response = status });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID)
        {
            var examList = ClientFactory.SchoolServiceClient(CallContext).GetExamsByClassAndGroup(classID, sectionID, examGroupID, academicYearID);

            if (examList == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong!" });
            }
            else
            {
                return Json(new { IsError = false, Response = examList });
            }
        }

        [HttpGet]
        public JsonResult GetStudentProgressReports(ProgressReportDTO toDto)
        {
            var progressReportsData = ClientFactory.SchoolServiceClient(CallContext).GetStudentProgressReports(toDto);

            if (progressReportsData == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong!" });
            }
            else
            {
                return Json(new { IsError = false, Response = progressReportsData });
            }
        }


        [HttpGet]
        public JsonResult GetStudentPublishedProgressReports(long studentID, long? examID)
        {
            var progressReportsData = ClientFactory.SchoolServiceClient(CallContext).GetStudentPublishedProgressReports(studentID, examID);

            if (progressReportsData == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong!" });
            }
            else
            {
                return Json(new { IsError = false, Response = progressReportsData });
            }
        }

        [HttpGet]
        public JsonResult GetAcademicYearByProgressReport(int studentID)
        {
            var yearList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearByProgressReport(studentID);

            if (yearList == null)
            {
                return Json(new { IsError = true, Response = "There are some issue, please check with the administrator." });
            }
            else
            {
                return Json(new { IsError = false, Response = yearList });
            }
        }

        [HttpGet]
        public JsonResult GetStudentFeeDetails(long studentID)
        {
            var feeDueDetails = ClientFactory.SchoolServiceClient(CallContext).GetStudentFeeDetails(studentID);

            if (feeDueDetails == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong, try again later!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeDueDetails });
            }
        }

        [HttpGet]
        public JsonResult GetSchoolsByLoginIDActiveStuds(long? loginID)
        {
            var schoolLists = ClientFactory.SchoolServiceClient(CallContext).GetSchoolsByLoginIDActiveStuds(loginID);
            return Json(schoolLists);

        }
        private bool IsCompressed(byte[] data)
        {
            return data.Length > 2 && data[0] == 31 && data[1] == 139; // Check if the first two bytes match GZip header
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(compressedData))
            {
                using (GZipStream decompressionStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    using (MemoryStream decompressedStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }

        public ActionResult CounselorHubList()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCounselorList()
        {
            var counselorDetail = ClientFactory.SchoolServiceClient(CallContext).GetCounselorList(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (counselorDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = counselorDetail });
            }
        }

    }
}