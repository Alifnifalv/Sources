using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Frameworks.Mvc.ActionFilters;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }
        private static string AccountService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);

        [HttpGet]
        public ActionResult Updates()
        {
            string path = ConfigurationExtensions.GetAppConfigValue("LatestUpgradePhysicalPath");
            string documentPath = ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath");

            if (System.IO.File.Exists(Path.Combine(documentPath, "eduegateportalupdates.zip")))
            {
                System.IO.File.Delete(Path.Combine(documentPath, "eduegateportalupdates.zip"));
            }

            ZipFile.CreateFromDirectory(path, Path.Combine(documentPath, "eduegateportalupdates.zip"));
            return File(Path.Combine(documentPath, "eduegateportalupdates.zip"), "application/zip", "eduegateportalupdates.zip");
        }

        [CustomAuthorization]
        public ActionResult Index()
        {
            var cultureCode = Request.QueryString["language"];

            if (string.IsNullOrEmpty(cultureCode))
            {
                cultureCode = "en-QA";
            }

            if (!string.IsNullOrEmpty(cultureCode))
            {
                ViewBag.CultureCode = cultureCode;
                base.CallContext.LanguageCode = cultureCode;
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCode);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureCode);
                Response.Cookies["_uiculture"].Value = cultureCode;
            }

            ViewBag.CallContext = CallContext;
            ResetCallContext(CallContext);
            if (Request.QueryString["layout"] != null && Request.QueryString["layout"] == "smart")
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
                return Json(new { IsError = true, Response = headerInfo }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = false, Response = headerInfo }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserDetails()
        {
            var userDetail = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

            if (userDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                userDetail.ProfileFile = userDetail.ProfileFile; //string.Format("{0}/{1}/{2}", ConfigurationExtensions.GetAppConfigValue("ImageHostUrl"), EduegateImageTypes.UserProfile, userDetail.ProfileFile);
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

                return Json(new { IsError = false, Response = userDetail }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetNotificationAlerts()
        {
            var notifications = ClientFactory.SchoolServiceClient(CallContext).GetNotificationAlerts(CallContext.LoginID.Value);
            return Json(new { IsError = false, Response = notifications }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotificationAlertsCount()
        {
            var alertCount = ClientFactory.SchoolServiceClient(CallContext).GetNotificationAlertsCount(CallContext.LoginID.Value);
            return Json(new { IsError = false, Response = alertCount }, JsonRequestBehavior.AllowGet);
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

            foreach (var key in Request.QueryString.AllKeys)
            {
                queryString = queryString + key + "=" + Request.QueryString[key] + "&";
            }

            return RedirectPermanent("../Reports/ReportViewer.aspx/GetReport" + queryString.Substring(0, queryString.Length - 1));
        }

        public ActionResult GeneratePDFReports(string reportName)
        {
            string queryString = "?";

            foreach (var key in Request.QueryString.AllKeys)
            {
                queryString = queryString + key + "=" + Request.QueryString[key] + "&";
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
            return Json(productDet, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductBundleData(long productskuId)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetProductBundleData(productskuId);
            return Json(productDet, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFineAmount(int fineMasterID)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetFineAmount(fineMasterID);
            return Json(productDet, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ClearCaches()
        {
            Framework.CacheManager.MemCacheManager<string>.ClearAll();
            return Json(new { Message = "Cache cleared" }, JsonRequestBehavior.AllowGet);
        }

        public string GetConfigData()
        {
            var documentPath = ConfigurationExtensions.GetAppConfigValue("DocumentPhysicalPathForMailAttach");
            return documentPath;
        }

        public JsonResult GetUnitByUnitGroup(int groupID)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetUnitByUnitGroup(groupID);
            return Json(productDet, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateAndEmailFeeReceipt(FeeCollectionDTO feeCollection)
        {
            try
            {
                var feeCollectionDatas = new List<FeeCollectionDTO>
                {
                    feeCollection
                };

                if (feeCollectionDatas != null && feeCollectionDatas.Count > 0)
                {
                    ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(feeCollectionDatas);

                    return Json(new { IsError = false, Response = "Mail sent successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsError = true, Response = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetStaffDetailsByStudentID(int studentID)
        {
            var staffDetail = ClientFactory.SchoolServiceClient(CallContext).GetStaffDetailsByStudentID(studentID);
            return Json(staffDetail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParentDetailsByStudentID(int studentID)
        {
            var parentDetails = ClientFactory.SchoolServiceClient(CallContext).GetParentDetailsByStudentID(studentID);
            return Json(parentDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID)
        {
            var feeAmount = ClientFactory.SchoolServiceClient(CallContext).GetFeeAmount(studentID, academicYearID, feeMasterID, feePeriodID);
            return Json(feeAmount, JsonRequestBehavior.AllowGet);

        }
      
    }
}