using Microsoft.AspNetCore.Mvc;
using BoldReports.Web.ReportViewer;
using Eduegate.Application.Mvc;
using BoldReports.Web;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace Eduegate.ERP.Admin.Controllers
{
    //This used Bold Reports to bring report
    public class ReportViewerController : BaseController, IReportController
    {
        public IActionResult Index(string reportName)
        {
            ViewBag.ReportName = reportName;

            return View();
        }

        // Report viewer requires a memory cache to store the information of consecutive client request and
        // have the rendered Report Viewer information in server.
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        // IWebHostEnvironment used with sample to get the application data from wwwroot.
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        // Post action to process the report from server based json parameters and send the result back to the client.
        public ReportViewerController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
        }

        // Post action to process the report from server based json parameters and send the result back to the client.
        [HttpPost]
        public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
        {
            //Contains helper methods that help to process a Post or Get request from the Report Viewer control and return the response to the Report Viewer control
            return ReportHelper.ProcessReport(jsonArray, this, this._cache);
        }

        // Method will be called to initialize the report information to load the report with ReportHelper for processing.
        [NonAction]
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string basePath = _hostingEnvironment.ContentRootPath;
            string rdlPath = basePath + @"/Areas/Reports/RDL/" + reportOption.ReportModel.ReportPath + ".rdl";

            // Loaded the report from application the folder wwwroot/Reports/RDL. rdl report should be there in wwwroot/Reports/RDL application folder.
            FileStream inputStream = new FileStream(rdlPath, FileMode.Open, FileAccess.Read);
            MemoryStream reportStream = new MemoryStream();
            inputStream.CopyTo(reportStream);
            reportStream.Position = 0;
            inputStream.Close();
            reportOption.ReportModel.Stream = reportStream;

            SetDefaultParameters(reportOption);

            // Set up the DataSource credentials dynamically
            reportOption.ReportModel.DataSourceCredentials = new List<DataSourceCredentials>();

            // Retrieve the connection string from appsettings.json
            var connectionString = Infrastructure.ConfigHelper.GetDefaultConnectionString();

            // Extract individual details from connection string
            var builder = new SqlConnectionStringBuilder(connectionString);
            string dataSource = builder.DataSource;
            string initialCatalog = builder.InitialCatalog;
            string userId = builder.UserID;
            string password = builder.Password;

            var dataSourceName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportDataSourceName", "Eduegate");

            reportOption.ReportModel.DataSourceCredentials.Add(new DataSourceCredentials
            {
                Name = dataSourceName, // This should match the DataSource name defined in your RDL report
                UserId = userId,
                Password = password,
                ConnectionString = "Data Source="+ dataSource + ";Initial Catalog=" + initialCatalog + ";user id=" + userId + ";password=" + password
            });
        }

        // Method will be called when reported is loaded with internally to start to layout process with ReportHelper.
        [NonAction]
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
        }

        //Get action for getting resources from the report
        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        // Method will be called from Report Viewer client to get the image src for Image report item.
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, _cache);
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }

        public void SetDefaultParameters(ReportViewerOptions reportOption)
        {
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

            // Create a list of ReportParameter objects
            var parameters = new List<ReportParameter>
            {
                new ReportParameter { Name = "SchoolID", Values = new List<string> { schoolID.Value.ToString() } },
                new ReportParameter { Name = "CurrentAcademicYearID", Values = new List<string> { academicYearID.Value.ToString() } },
                new ReportParameter { Name = "Signature", Values = new List<string> { signature } },
                new ReportParameter { Name = "Logo", Values = new List<string> { logo } },
                new ReportParameter { Name = "SchoolSeal", Values = new List<string> { schoolSeal } },
                new ReportParameter { Name = "ReportFooter", Values = new List<string> { footer } },
                new ReportParameter { Name = "HeaderBGColor", Values = new List<string> { headerBGColor } },
                new ReportParameter { Name = "HeaderForeColor", Values = new List<string> { headerForeColor } },
                new ReportParameter { Name = "RootUrl", Values = new List<string> { rootUrl } }
            };

            // Assign the list of parameters to the report model
            reportOption.ReportModel.Parameters = parameters;
        }

    }
}