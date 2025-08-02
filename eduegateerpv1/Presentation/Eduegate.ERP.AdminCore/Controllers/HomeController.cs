using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Globalization;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eduegate.ERP.Admin.Models;
using System.Diagnostics;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.ERP.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string AccountService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);

        [HttpGet]
        public ActionResult Updates()
        {
            string path = new Domain.Setting.SettingBL().GetSettingValue<string>("LatestUpgradePhysicalPath");
            string documentPath = new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath");

            if (System.IO.File.Exists(Path.Combine(documentPath, "eduegateportalupdates.zip")))
            {
                System.IO.File.Delete(Path.Combine(documentPath, "eduegateportalupdates.zip"));
            }

            ZipFile.CreateFromDirectory(path, Path.Combine(documentPath, "eduegateportalupdates.zip"));
            return File(Path.Combine(documentPath, "eduegateportalupdates.zip"), "application/zip", "eduegateportalupdates.zip");
        }

        [Authorize]
        public ActionResult Index()
        {
            //var cultureCode = Request.Query["language"];

            //if (string.IsNullOrEmpty(cultureCode))
            //{
            //    cultureCode = "en-QA";
            //}

            var language = Request.Query["language"];

            if (string.IsNullOrEmpty(language))
            {
                language = "en";
            }

            if (!string.IsNullOrEmpty(language))
            {
                var cultureCode = language[0];
                
                cultureCode = string.IsNullOrEmpty(cultureCode) ? "en-US" : cultureCode;

                ViewBag.CultureCode = cultureCode;
                base.CallContext.LanguageCode = cultureCode;
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCode);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureCode);
                //Response.Cookies["_uiculture"].Value = cultureCode;
                Response.Cookies.Append("_uiculture", cultureCode);
            }

            ViewBag.CallContext = CallContext;
            ResetCallContext(CallContext);

            var layout = Request.Query["layout"];
            if (layout.Count > 0 && layout[0]?.ToString() != null && layout[0]?.ToString() == "smart")
            {
                return View("SmartView");
            }
            else
            {
                //var userDashBaords = ClientFactory.UserServiceClient(CallContext).GetClaimsByTypeAndLoginID(Services.Contracts.Enums.ClaimType.ERPDashbaord, CallContext.LoginID.Value);

                var userDashBaords = ClientFactory.UserServiceClient(CallContext).GetDashBoardByUserID(Services.Contracts.Enums.ClaimType.ERPDashbaord, CallContext.LoginID.Value);

                var pageResource = userDashBaords.IsNotNull() && userDashBaords.Count > 0 ? userDashBaords.ToList().LastOrDefault().ResourceName : "45";
                var pageID = string.IsNullOrEmpty(pageResource) ? ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompanyWithDefault<long>("DEFAULTDASHABAORDPAGE", CallContext.CompanyID.Value, 45)
                    : long.Parse(pageResource);
                return View(new PageRedererViewModel(PageEngineViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(pageID, string.Empty), string.Empty, true)));
            }
        }
        public ActionResult SchoolSelection()
        {
            return View();
        }

        public ActionResult TwinView(long referenceID,string imageContentID)
        {
            ViewBag.referenceID = referenceID;
            ViewBag.imageContentID = imageContentID;
            return View("_TwinView");
        }

        [HttpGet]
        public JsonResult GetHeaderInfo(decimal loggedInUserIID)
        {
            var headerInfo = new HeaderInfoViewModel();
            try
            {
                headerInfo.userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(JsonConvert.SerializeObject(ClientFactory.UserServiceClient(CallContext).GetUserInfo(loggedInUserIID)));
                headerInfo.UnreadEmailCount = 2;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<HomeController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, Response = headerInfo });
            }

            return Json(new { IsError = false, Response = headerInfo });
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
                userDetail.ProfileFile = userDetail.ProfileFile; //string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, userDetail.ProfileFile);
                userDetail.AcademicYearID = CallContext.AcademicYearID;
                if (CallContext.AcademicYearID.HasValue)
                {
                    userDetail.AcademicYear = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearNameByID(CallContext.AcademicYearID.Value);
                }

                userDetail.SchoolID = (byte?)CallContext.SchoolID;
                if (CallContext.SchoolID.HasValue)
                {
                    userDetail.School = ClientFactory.SchoolServiceClient(CallContext).GetSchoolNameByID(CallContext.SchoolID.Value);
                }

                return Json(new { IsError = false, Response = userDetail });
            }
        }

        public JsonResult GetNotificationAlerts()
        {
            var notifications = ClientFactory.SchoolServiceClient(CallContext).GetNotificationAlerts(CallContext.LoginID.Value);
            return Json(new { IsError = false, Response = notifications });
        }

        public JsonResult GetNotificationAlertsCount()
        {
            var alertCount = ClientFactory.SchoolServiceClient(CallContext).GetNotificationAlertsCount(CallContext.LoginID.Value);
            return Json(new { IsError = false, Response = alertCount });
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

        public ActionResult ViewReports(string reportName)
        {
            string queryString = "?";

            foreach (var key in Request.Query.Keys)
            {
                queryString = queryString + key + "=" + Request.Query[key] + "&";
            }

            return RedirectPermanent("../Reports/ReportViewer.aspx/GetReport" + queryString.Substring(0, queryString.Length - 1));
        }

        public ActionResult GeneratePDFReports(string reportName)
        {
            string queryString = "?";

            foreach (var key in Request.Query.Keys)
            {
                queryString = queryString + key + "=" + Request.Query[key] + "&";
            }

            return Redirect("../Reports/ReportViewer.aspx/GetReport" + queryString.Substring(0, queryString.Length - 1));
        }

        public ActionResult CustomDashbaord(long pageID)
        {
            ViewBag.CallContext = CallContext;
            return View(new PageRedererViewModel(PageEngineViewModel.FromDTO(
                    ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(pageID, string.Empty), string.Empty, true)));
        }

        public JsonResult GetProductDetails(long productId)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetProductDetails(productId);
            return Json(productDet);
        }

        public JsonResult GetProductBundleData(long productskuId)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetProductBundleData(productskuId);
            return Json(productDet);
        }
        public JsonResult GetFineAmount(int fineMasterID)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetFineAmount(fineMasterID);
            return Json(productDet);
        }

        public string GetConfigData()
        {
            var documentPath = new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentPhysicalPathForMailAttach");
            return documentPath;
        }

        public JsonResult GetUnitByUnitGroup(int groupID)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetUnitByUnitGroup(groupID);
            return Json(productDet);
        }

        [HttpPost]
        public ActionResult GenerateAndEmailFeeReceipt([FromBody] FeeCollectionDTO feeCollection)
        {
            try
            {
                var feeCollectionDatas = new List<FeeCollectionDTO>
                {
                    feeCollection
                };

                if (feeCollectionDatas != null && feeCollectionDatas.Count > 0)
                {
                    ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(feeCollectionDatas, EmailTypes.AutoFeeReceipt);

                    return Json(new { IsError = false, Response = "Mail sent successfully" });
                }
                else
                {
                    return Json(new { IsError = true, Response = "Something went wrong!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        public JsonResult GetStaffDetailsByStudentID(int studentID)
        {
            var staffDetail = ClientFactory.SchoolServiceClient(CallContext).GetStaffDetailsByStudentID(studentID);
            return Json(staffDetail);
        }

        public JsonResult GetParentDetailsByStudentID(int studentID)
        {
            var parentDetails = ClientFactory.SchoolServiceClient(CallContext).GetParentDetailsByStudentID(studentID);
            return Json(parentDetails);
        }

        public JsonResult GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID)
        {
            var feeAmount = ClientFactory.SchoolServiceClient(CallContext).GetFeeAmount(studentID, academicYearID, feeMasterID, feePeriodID);
            return Json(feeAmount);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetEmployeesByDepartment(long? departmentID)
        {
            departmentID = departmentID != null ? departmentID : 0;
            var employees = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeesByDepartment(departmentID.Value);

            return Json(employees);
        }

    }
}