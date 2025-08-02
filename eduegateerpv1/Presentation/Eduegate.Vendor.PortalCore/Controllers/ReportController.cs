using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class ReportController : BaseController
    {
        private readonly HttpClient _httpClient;

        public ReportController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Reportview(string reportUrl, string listScreen)
        {
            ViewBag.ReportUrl = reportUrl; // Store the URL in ViewBag
            ViewBag.ListScreen = listScreen;

            return View();
        }

        public JsonResult GetReportUrlandParameters(string reportName)
        {
            try
            {
                string successData = string.Empty;
                string reportUrl = string.Empty;

                var schoolID = CallContext.SchoolID;
                var academicYearID = CallContext.AcademicYearID;

                //Get school IDs
                var schoolID_Thumama = new Domain.Setting.SettingBL(null).GetSettingValue<short>("SCHOOLID_THUMAMA_10", 10);
                var schoolID_WestBay = new Domain.Setting.SettingBL(null).GetSettingValue<short>("SCHOOLID_WESTBAY_20", 20);
                var schoolID_Meshaf = new Domain.Setting.SettingBL(null).GetSettingValue<short>("SCHOOLID_MESHAF_30", 30);

                //set root Url
                var rootUrl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("RootUrl");

                //Set footer
                var footer = string.Empty;
                if (schoolID == schoolID_Thumama)
                {
                    footer = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_FOOTER_SCHOOL_ADDRESS_THUMAMA_10");
                }
                else if (schoolID == schoolID_WestBay)
                {
                    footer = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_FOOTER_SCHOOL_ADDRESS_WESTBAY_20");
                }
                else
                {
                    footer = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_FOOTER_SCHOOL_ADDRESS_MESHAF_30");
                }

                //Set principal signature
                var signature = string.Empty;
                if (schoolID == schoolID_Meshaf)
                {
                    signature = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PRINCIPAL_SIGNATURE_01");
                }

                //Set logo
                var logo = string.Empty;
                if (schoolID == schoolID_Meshaf)
                {
                    logo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("COMPANY_REPORT_LOGO_PODAR");
                }
                else
                {
                    logo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("COMPANY_REPORT_LOGO_PEARL");
                }

                //Set school seal
                var schoolSeal = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SCHOOL_STAMP");

                //Set colors
                var headerBGColor = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_HEADER_BGCOLOR");
                var headerForeColor = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_HEADER_FORECOLOR");

                //Set report server domain related
                var reportHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerHost");
                var reportUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerDomainUser");
                var reportPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerDomainPassword");

                if (!string.IsNullOrEmpty(reportHost))
                {
                    var uriBuilder = new UriBuilder(reportHost + reportName);
                    var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                    query["rs:Command"] = "Render";
                    query["rs:embed"] = "true";
                    query["rs:Username"] = reportUserName;
                    query["rs:Password"] = reportPassword;
                    query["SchoolID"] = schoolID.ToString();
                    query["CurrentAcademicYearID"] = academicYearID.ToString();
                    query["ReportFooter"] = footer;
                    query["Signature"] = signature;
                    query["Logo"] = logo;
                    query["SchoolSeal"] = schoolSeal;
                    query["HeaderBGColor"] = headerBGColor;
                    query["HeaderForeColor"] = headerForeColor;
                    query["RootUrl"] = rootUrl;

                    uriBuilder.Query = query.ToString();

                    reportUrl = uriBuilder.ToString();

                    successData = reportUrl;
                }

                if (string.IsNullOrEmpty(reportUrl))
                {
                    successData = "Unable to load report!";
                }

                //return successData;
                return Json(new { IsError = false, Response = successData });
            }
            catch (Exception ex)
            {
                // Handle HTTP request exception
                return Json(new { IsError = true, Response = ex.Message });
                //return null;
            }
        }

    }
}
