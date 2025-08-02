using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using System.Text;
using System.Net.Http.Headers;
using System.Net;
using System.Xml;
using HtmlAgilityPack;

namespace Eduegate.ERP.Admin.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class ReportController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProxyReport(string reportName)
        {
            var reportHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerHost");
            var reportUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerDomainUser");
            var reportPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerDomainPassword");

            var reportUrl = $"{reportHost}{reportName}";

            var client1 = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "http://stagingerp.pearlschool.org/ReportServer");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "username", reportUserName },
                { "password", reportPassword }
            });

            var response1 = await client1.SendAsync(request);

            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{reportUserName}:{reportPassword}");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    var response = await httpClient.GetAsync(reportUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        return File(stream, "text/html");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error accessing the report: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while accessing the report: " + ex.Message);
            }
        }

        public async Task<IActionResult> ProxyReport2(string reportName)
        {
            var client = _httpClientFactory.CreateClient();

            var reportHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerHost");
            var reportUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerDomainUser");
            var reportPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ReportServerDomainPassword");

            var result = GetReportUrlandParameters(reportName);

            client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{reportUserName}:{reportPassword}"))}");
            var response = await client.GetAsync($"{result}");

            if (response.IsSuccessStatusCode)
            {
                return null;
                //return GetReportParameters(reportName);
            }
            else
            {
                return null;
            }
        }

        #region To fill report parameter details
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

        #endregion report parameter

        public JsonResult GetReportUrlAandParametersNew(string reportName)
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
                    var data = getparametersAsync(reportHost, reportName);
                    // Define the list of allowed parameters
                    List<string> allowedParameters = new List<string>
                    {
                        "rs:Command",
                        "rs:embed",
                        "ReportFooter",
                        "Logo",
                        "RootUrl",
                        // Add other allowed parameters here
                    };

                    // Create a dictionary to store parameter values
                    Dictionary<string, string> parameters = new Dictionary<string, string>
                    {
                        { "rs:Command", "Render" },
                        { "rs:embed", "true" },
                        { "SchoolID", schoolID.ToString() },
                        { "CurrentAcademicYearID", academicYearID.ToString() },
                        { "ReportFooter", footer },
                        { "Signature", signature },
                        { "Logo", logo },
                        { "SchoolSeal", schoolSeal },
                        { "RootUrl", rootUrl },
                        { "HeaderBGColor", headerBGColor }, // Assuming you have a value for headerBGColor
                        { "HeaderForeColor", headerForeColor } // Assuming you have a value for headerForeColor
                        // Add other parameters here
                    };

                    // Build the query string
                    var queryBuilder = new StringBuilder();
                    foreach (var parameter in parameters)
                    {
                        if (allowedParameters.Contains(parameter.Key))
                        {
                            queryBuilder.Append($"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}&");
                        }
                    }

                    // Remove the trailing '&' if present
                    if (queryBuilder.Length > 0)
                    {
                        queryBuilder.Length -= 1;
                    }

                    // Construct the full URL
                    reportUrl = $"{reportHost}%2f{reportName}&{queryBuilder.ToString()}";

                    successData = reportUrl;
                }

                if (string.IsNullOrEmpty(reportUrl))
                {
                    successData = "Unable to load report!";
                }

                return Json(new { IsError = false, Response = successData });
            }
            catch (Exception ex)
            {
                // Handle HTTP request exception
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        public async Task getparametersAsync(string reportHost,string reportName)
        {
            string reportServerUrl = "http://erp.pearlschool.org:80/ReportServer";
            string reportPath = "/ChartOfAccounts"; // Adjust this based on your report path
            string username = "pearl";
            string password = "pearldoha123$";

            using (var httpClientHandler = new HttpClientHandler())
            {
                // Set credentials
                httpClientHandler.Credentials = new NetworkCredential(username, password);

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    // Make a request to the ReportExecution2005 endpoint to get the report parameters
                    string url = $"{reportServerUrl}/ReportExecution2005.asmx";
                    string soapEnvelope = $@"
                    <soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                        <soap:Body>
                            <GetReportParameters xmlns='http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices'>
                                <Report>{reportPath}</Report>
                            </GetReportParameters>
                        </soap:Body>
                    </soap:Envelope>";

                    var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            if (response.Content.Headers.ContentType.MediaType.ToLower() == "text/xml")
                            {
                                try
                                {
                                    string xmlString = await response.Content.ReadAsStringAsync();

                                    // Parse the XML response to extract the parameter names
                                    XmlDocument xmlDocument = new XmlDocument();
                                    xmlDocument.LoadXml(xmlString);

                                    // Extract parameter information
                                    var parameters = xmlDocument.SelectNodes("//ParameterInfo");
                                    List<string> parameterNames = new List<string>();

                                    foreach (XmlNode parameter in parameters)
                                    {
                                        string paramName = parameter.SelectSingleNode("Name")?.InnerText;
                                        if (!string.IsNullOrEmpty(paramName))
                                        {
                                            parameterNames.Add(paramName);
                                        }
                                    }

                                    // Output the list of parameter names
                                    Console.WriteLine("Parameters used in the report:");
                                    foreach (var paramName in parameterNames)
                                    {
                                        Console.WriteLine(paramName);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error parsing XML: {ex.Message}");
                                }
                            }
                            else if (response.Content.Headers.ContentType.MediaType.ToLower() == "text/html")
                            {

                                string contentString = await response.Content.ReadAsStringAsync();
                                // Use HtmlAgilityPack to parse HTML
                                HtmlDocument htmlDoc = new HtmlDocument();
                                htmlDoc.LoadHtml(contentString);

                                // Extract information from HTML (modify this part based on your HTML structure)
                                var anchorNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                                List<string> reportLinks = new List<string>();

                                if (anchorNodes != null)
                                {
                                    foreach (var anchorNode in anchorNodes)
                                    {
                                        string reportLink = anchorNode.GetAttributeValue("href", "");
                                        reportLinks.Add(reportLink);
                                    }
                                }

                                // Output the list of report links
                                Console.WriteLine("Report links extracted from HTML:");
                                foreach (var link in reportLinks)
                                {
                                    Console.WriteLine(link);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Error: Unexpected content type - {response.Content.Headers.ContentType.MediaType}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing content: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
        }

        public JsonResult GetReportUrlandParametersForDownload(string reportName)
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
                    query["rs:Format"] = "PDF";
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