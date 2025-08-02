using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Eduegate.Application.Mvc;
using Eduegate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.ERP.Admin.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class ReportController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewReports(string reportName, string parameter = null, bool returnFileBytes = false)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("Edueagte.ERP.AdminCore.dll", string.Empty);
            string rdlcFilePath = string.Format(@"{0}\{1}\{2}.rdl", fileDirPath, @"\Areas\Reports\RDL", reportName);
            ViewBag.ReportParameter = parameter;
            ViewBag.ReportName = reportName;

            if (!System.IO.File.Exists(rdlcFilePath))
            {
                return View("ViewReports", reportName);
            }
            else
            {
                var metaData = new Eduegate.Domain.Frameworks.FrameworkBL(CallContext).GetScreenMetadataByName(reportName);
                var reportDetails = Eduegate.Utilities.SSRSHelper.Report.GetReportFromFile(rdlcFilePath); 
                ViewBag.Parameters = reportDetails.ReportParameters;
                ViewBag.MetaData = metaData;
                return View("ViewReports", reportName);
            }
        }

        [HttpGet]
        [Route("Reports/Show/{reportName}/")]
        public async Task<IActionResult> Show([FromQuery] string reportName, string parameters, string format)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("Edueagte.ERP.AdminCore.dll", string.Empty);
            string rdlcFilePath = string.Format(@"{0}\{1}\{2}.rdl", fileDirPath, @"Areas\Reports\RDL", reportName);

            using (var client = new HttpClient())
            {
                var model = new ReportViewModel()
                {
                    ReportFile = rdlcFilePath,
                    Parameters = parameters,
                    Format = format
                };

                var serviceUrl = new SettingBL(null).GetSettingValue<string>("ReportServiceUrl");
                client.BaseAddress = new Uri(serviceUrl);
                var result = await client.PostAsync("",
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
                var reportFile = (await result.Content.ReadAsStringAsync());
                reportFile = reportFile.Replace("\"", "");
                switch (format.ToLower())
                {
                    case "excel":
                        var excelResult = PhysicalFile(reportFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = reportName + ".xls"
                        }.ToString();
                        return excelResult;
                    case "pdf":
                        var myfile = System.IO.File.ReadAllBytes(reportFile);
                        var reportFileName = Helpers.PortalWebHelper.InsertSpacesBetweenCapitalLetters(reportName);
						// Add the Content-Disposition header
						Response.Headers.Add("Content-Disposition", $"inline; filename=\"{reportFileName}\"");
						return new FileContentResult(myfile, "application/pdf");
                    case "html5":
                    default:
                        return PhysicalFile(reportFile, "text/html");
                }
            }
        }

        [HttpGet]
        public FileContentResult GetPDF(string reportName)
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var rootDirectory = Path.GetDirectoryName(path);

            string rdlcFilePath = string.Format(@"{0}\{1}\{2}.rdl", rootDirectory, @"\Areas\Reports\RDL", reportName);
            var reportDetails = Eduegate.Utilities.SSRSHelper.Report.GetReportFromFile(rdlcFilePath);
            var parameters = new Dictionary<string, string>();
            var result = GenerateReportAsync(rdlcFilePath, reportDetails.DataSets, null);
            return File(result.MainStream, System.Net.Mime.MediaTypeNames.Application.Pdf, reportName + ".pdf");
        }

        [HttpGet]
        public PhysicalFileResult PDFViewer(string reportName)
        {
            var filepath = Eduegate.Web.Library.Helpers.InvokeProcess.Process(reportName + ".rdl");
            return new PhysicalFileResult(filepath, "application/pdf");
        }

        public ReportResult GenerateReportAsync(string reportName, List<Eduegate.Utilities.SSRSHelper.DataSet> datasets, Dictionary<string, string> parameters)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
            LocalReport report = new LocalReport(reportName);
            foreach (var dataset in datasets)
            {
                var dataTable = GetDataTable(dataset, parameters);
                report.AddDataSource(dataset.Name, dataTable);
            }

            return report.Execute(GetRenderType("html"), 1, parameters);
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;
            switch (reportType.ToLower())
            {
                default:
                case "pdf":
                    renderType = RenderType.Pdf;
                    break;
                case "image":
                    renderType = RenderType.Image;
                    break;
                case "word":
                    renderType = RenderType.Word;
                    break;
                case "excel":
                    renderType = RenderType.Excel;
                    break;
                case "html":
                    renderType = RenderType.Html;
                    break;

            }

            return renderType;
        }

        public System.Data.DataTable GetDataTable(Eduegate.Utilities.SSRSHelper.DataSet dataset, Dictionary<string, string> parameters)
        {
            System.Data.DataTable re = new System.Data.DataTable();

            using (SqlDataAdapter da =
               new SqlDataAdapter(dataset.Query.CommandText, ConfigHelper.GetDefaultConnectionString()))
            {
                if (dataset.Query.QueryParameters.Count > 0 && parameters != null)
                {
                    SqlParameterCollection parmeter = da.SelectCommand.Parameters;

                    foreach (var param in dataset.Query.QueryParameters)
                    {
                        string paramName = param.Name;
                        var parameter = parameters.Where(a => a.Key == paramName.Replace("@", "")).FirstOrDefault();
                        object value1 = null;

                        switch (param.DataType)
                        {
                            case "String":
                                parmeter.Add(new SqlParameter(paramName, SqlDbType.VarChar)
                                {
                                    Value = value1 ?? string.Empty
                                });
                                break;
                            case "Boolean":
                                parmeter.Add(new SqlParameter(paramName, SqlDbType.Bit)
                                {
                                    Value = true
                                });
                                break;
                            case "DateTime":
                                parmeter.Add(new SqlParameter(paramName, SqlDbType.Date)
                                {
                                    Value = (value1 == null ? DateTime.Now : DateTime.Parse(value1.ToString()))
                                });
                                break;
                            case "Integer":
                                parmeter.Add(new SqlParameter(paramName, SqlDbType.Int)
                                {
                                    Value = value1 == null ? 0 : int.Parse(value1.ToString())
                                });
                                break;
                            case "Float":
                                parmeter.Add(new SqlParameter(paramName, SqlDbType.Decimal)
                                {
                                    Value = value1 == null ? 0 : float.Parse(value1.ToString())
                                });
                                break;
                            default:
                                parmeter.Add(new SqlParameter(paramName, ""));
                                break;
                        }
                    }
                }

                da.Fill(re);
                re.TableName = dataset.Name;
                return re;
            }
        }

    }
}
