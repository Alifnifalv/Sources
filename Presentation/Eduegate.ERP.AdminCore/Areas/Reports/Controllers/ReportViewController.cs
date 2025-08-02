using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using Eduegate.ERP.Admin.Areas.Reports.Models;
using Eduegate.Services.Contracts.Framework;
using OfficeOpenXml;
using HtmlAgilityPack;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Xml.Linq;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using System.Text.RegularExpressions;
using Eduegate.Domain.Report;

namespace Eduegate.ERP.Admin.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class ReportViewController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        // IWebHostEnvironment used with sample to get the application data from wwwroot.
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private readonly IConverter _converter;

        // Post action to process the report from server based json parameters and send the result back to the client.
        public ReportViewController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IConverter converter)
        {
            _hostingEnvironment = hostingEnvironment;
            _converter = converter;
        }

        public IActionResult ViewReports(string reportName, string parameter = null, bool returnFileBytes = false)
        {
            string rdlcFilePath = string.Empty;
            string reportPhysicalPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath", null).ToString();

            var rdlPath = string.Format(@"{0}/{1}.rdl", reportPhysicalPath, reportName);

            if (!System.IO.File.Exists(rdlPath))
            {
                string fileDirPath = _hostingEnvironment.ContentRootPath;
                rdlcFilePath = fileDirPath + @"/Areas/Reports/RDL/" + reportName + ".rdl";
            }
            else
            {
                rdlcFilePath = rdlPath;
            }

            // Create a list of KeyValueDTO (or however your class is defined)
            List<KeyValueDTO> reportParameters = new List<KeyValueDTO>();

            var convertedReportParameters = string.IsNullOrEmpty(parameter) ?
                new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(parameter);

            foreach (var param in convertedReportParameters)
            {
                reportParameters.Add(new KeyValueDTO()
                {
                    Key = param.Key,
                    Value = param.Value,
                });
            }

            ViewBag.ReportParameter = reportParameters;
            ViewBag.ReportName = reportName;

            if (!System.IO.File.Exists(rdlcFilePath))
            {
                return View("ViewReports", reportName);
            }
            else
            {
                var metaData = new Domain.Frameworks.FrameworkBL(CallContext).GetScreenMetadataByName(reportName);
                var reportDetails = Eduegate.Utilities.SSRSHelper.Report.GetReportFromFile(rdlcFilePath);

                ViewBag.ReportLookUps = FillReportLookups(reportDetails);

                ViewBag.Parameters = reportDetails.ReportParameters;
                ViewBag.MetaData = metaData;

                return View("ViewReports", reportName);
            }
        }

        public List<ScreenLookupDTO> FillReportLookups(Eduegate.Utilities.SSRSHelper.Report reportDetails)
        {
            var lookupDTOs = new List<ScreenLookupDTO>();

            var searchParameters = reportDetails.ReportParameters.Count > 0 ?
                reportDetails.ReportParameters
                .Where(p => p.Prompt != null && (p.Prompt.ToLower().Contains(" search") || p.Prompt.ToLower().Contains("search ")))
                .ToList() : null;

            foreach (var parameter in reportDetails.ReportParameters)
            {
                var dataSetName = parameter?.ValidValues?.DataSetReference?.DataSetName;
                if (!string.IsNullOrEmpty(dataSetName))
                {
                    var dataSetDetail = reportDetails.DataSets.FirstOrDefault(d => d.Name == dataSetName);
                    var query = dataSetDetail?.Query?.CommandText;

                    var parameterKey = parameter?.ValidValues?.DataSetReference?.ValueField;
                    var parameterValue = parameter?.ValidValues?.DataSetReference?.LabelField;

                    var keyValues = new List<KeyValueDTO>();

                    bool isLazyLoad = false;
                    if (parameter.Name.ToLower() == "student" || parameter.Name.ToLower() == "students" || parameter.Name.ToLower() == "studentid" || parameter.Name.ToLower() == "studentiid")
                    {
                        isLazyLoad = true;
                    }
                    else if (parameter.Name.ToLower() == "employee" || parameter.Name.ToLower() == "employees" || parameter.Name.ToLower() == "employeename" || parameter.Name.ToLower() == "employeeid" || parameter.Name.ToLower() == "employeeiid")
                    {
                        isLazyLoad = true;
                    }

                    if (searchParameters.Any(s => query.Contains(s.Name)))
                    {
                        isLazyLoad = true;
                    }
                    else
                    {
                        if (isLazyLoad == false)
                        {
                            isLazyLoad = false;
                            keyValues = new ReferenceDataBL(CallContext).GetLookupsByQuery(query, parameterKey, parameterValue);

                            if (keyValues.Count > 150)
                            {
                                isLazyLoad = true;
                                keyValues = new List<KeyValueDTO>();
                            }
                        }
                    }

                    // Create a list to store the query parameters
                    List<string> queryParameters = new List<string>();

                    if (!string.IsNullOrEmpty(query))
                    {
                        // Regex pattern to find all words starting with '@'
                        string pattern = @"@\w+";

                        // Match all occurrences of the pattern
                        MatchCollection matches = Regex.Matches(query, pattern);

                        // Add each match to the list
                        foreach (Match match in matches)
                        {
                            if (!queryParameters.Contains(match.Value))
                            {
                                queryParameters.Add(match.Value);
                            }
                        }
                    }

                    lookupDTOs.Add(new ScreenLookupDTO()
                    {
                        IsOnInit = true,
                        LookUpName = parameter.Name,
                        Url = "Mutual/GetLazyLookUpDataByReportField?columnName=" + parameter.Name,
                        Lookups = keyValues,
                        InitLookups = keyValues,
                        IsLazyLoad = isLazyLoad,
                        LookUpQuery = query,
                        LookUpQueryParameters = queryParameters,
                        ParameterKey = parameterKey,
                        ParameterValue = parameterValue,
                    });
                }
            }

            return lookupDTOs;
        }

        [HttpGet]
        public JsonResult GetUpdatedLookupsByParameters(string query, string parameterKey = null, string parameterValue = null, string parameters = null, bool isProcedure = false)
        {
            var keyValues = new List<KeyValueDTO>();
            var reportParameters = new List<KeyValueDTO>();

            var convertedReportParameters = string.IsNullOrEmpty(parameters) ?
                    new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(parameters);

            foreach (var param in convertedReportParameters)
            {
                reportParameters.Add(new KeyValueDTO()
                {
                    Key = param.Key,
                    Value = param.Value,
                });
            }

            if (!isProcedure)
            {
                keyValues = new ReferenceDataBL(CallContext).GetLookupsByQuery(query, parameterKey, parameterValue, null, null, reportParameters);
            }
            else
            {
                keyValues = new ReferenceDataBL(CallContext).GetLookupsByProcedure(query, parameterKey, parameterValue, null, null, reportParameters);
            }

            return Json(new { IsError = false, Response = keyValues });
        }

        [HttpGet]
        public IActionResult Show([FromQuery] string reportName, string parameters, string format, bool isPrint = false)
        {
            string fileDirPath = _hostingEnvironment.ContentRootPath;
            string rdlcFilePath = fileDirPath + @"/Areas/Reports/RDL/" + reportName + ".rdl";

            // Generate file for specified format and set filename in the response
            var filename = $"{reportName}_{DateTime.Now:yyyyMMdd_HHmmss}";

            var resultBytes = new ReportViewerBL(CallContext).GetReportFile(reportName, parameters, format, rdlcFilePath);

            if (resultBytes != null && resultBytes.Length > 0)
            {
                // Determine the format and render accordingly
                switch (format.ToLower())
                {
                    case "pdf":
                        if (isPrint)
                        {
                            // Return the PDF to be opened for print
                            return File(resultBytes, "application/pdf");
                        }
                        else
                        {
                            // Download the PDF file if isPrint is false
                            return File(resultBytes, "application/pdf", $"{filename}.pdf");
                        }

                    case "excel":
                        return File(resultBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{filename}.xlsx");

                    case "word":

                        return File(resultBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{filename}.docx");

                    //case "powerpoint":
                    //    return File(resultBytes, "application/vnd.openxmlformats-officedocument.presentationml.presentation", $"{filename}.pptx");

                    case "csv":
                        return File(resultBytes, "text/csv", $"{filename}.csv");

                    case "xml":
                        return File(resultBytes, "application/xml", $"{filename}.xml");

                    case "html5":
                    default:
                        var htmlContent = System.Text.Encoding.UTF8.GetString(resultBytes);

                        if (isPrint)
                        {
                            return File(resultBytes, "text/html", $"{filename}.html");
                        }
                        else
                        {
                            return Content(htmlContent, "text/html");
                        }

                }
            }
            else
            {
                return BadRequest("Failed to generate the report.");
            }
        }

        [HttpPost]
        public IActionResult ReportViewPOST([FromBody] ReportRequestModel requestModel)
        {
            bool isPrint = requestModel?.IsPrint ?? false;
            string reportName = requestModel?.ReportName;
            string parameters = requestModel?.Parameters;
            string format = requestModel?.Format;

            string fileDirPath = _hostingEnvironment.ContentRootPath;
            string rdlcFilePath = fileDirPath + @"/Areas/Reports/RDL/" + reportName + ".rdl";

            // Generate file for specified format and set filename in the response
            var filename = $"{reportName}_{DateTime.Now:yyyyMMdd_HHmmss}";

            var resultBytes = new ReportViewerBL(CallContext).GetReportFile(reportName, parameters, format, rdlcFilePath);

            if (resultBytes != null && resultBytes.Length > 0)
            {
                // Determine the format and render accordingly
                switch (format.ToLower())
                {
                    case "pdf":
                        if (isPrint)
                        {
                            // Return the PDF to be opened for print
                            return File(resultBytes, "application/pdf");
                        }
                        else
                        {
                            // Download the PDF file if isPrint is false
                            return File(resultBytes, "application/pdf", $"{filename}.pdf");
                        }

                    case "excel":
                        return File(resultBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{filename}.xlsx");

                    case "word":

                        return File(resultBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{filename}.docx");

                    //case "powerpoint":
                    //    return File(resultBytes, "application/vnd.openxmlformats-officedocument.presentationml.presentation", $"{filename}.pptx");

                    case "csv":
                        return File(resultBytes, "text/csv", $"{filename}.csv");

                    case "xml":
                        return File(resultBytes, "application/xml", $"{filename}.xml");

                    case "html5":
                    default:
                        var htmlContent = System.Text.Encoding.UTF8.GetString(resultBytes);

                        if (isPrint)
                        {
                            return File(resultBytes, "text/html", $"{filename}.html");
                        }
                        else
                        {
                            return Content(htmlContent, "text/html");
                        }

                }
            }
            else
            {
                return BadRequest("Failed to generate the report.");
            }
        }

        #region Converting files
        //// Function to convert HTML content to PDF using iText
        //private byte[] RenderAndConvertToPdf(Microsoft.Reporting.NETCore.LocalReport viewer, string rdlcFilePath)
        //{
        //    var htmlBytes = viewer.Render("HTML5"); // Render HTML first
        //    var htmlContent = System.Text.Encoding.UTF8.GetString(htmlBytes);

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        // Load RDLC for page dimensions and margins
        //        var xdoc = XDocument.Load(rdlcFilePath);

        //        var pageWidth = (float)ConvertToPoints(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "PageWidth")?.Value);
        //        var pageHeight = (float)ConvertToPoints(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "PageHeight")?.Value);
        //        var leftMargin = (float)ConvertToPoints(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "LeftMargin")?.Value);
        //        var rightMargin = (float)ConvertToPoints(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "RightMargin")?.Value);
        //        var topMargin = (float)ConvertToPoints(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "TopMargin")?.Value);
        //        var bottomMargin = (float)ConvertToPoints(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "BottomMargin")?.Value);

        //        var pdfWriter = new iText.Kernel.Pdf.PdfWriter(memoryStream);
        //        var pdfDocument = new iText.Kernel.Pdf.PdfDocument(pdfWriter);

        //        // Define the page size
        //        var pageSize = new iText.Kernel.Geom.PageSize(pageWidth, pageHeight);

        //        var document = new iText.Layout.Document(pdfDocument, pageSize);
        //        document.SetMargins(topMargin, rightMargin, bottomMargin, leftMargin);

        //        // Convert HTML to PDF
        //        using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
        //        {
        //            var converterProperties = new iText.Html2pdf.ConverterProperties();
        //            iText.Html2pdf.HtmlConverter.ConvertToPdf(htmlStream, pdfDocument, converterProperties);
        //        }

        //        // Close the document to complete the PDF creation
        //        document.Close();

        //        return memoryStream.ToArray();
        //    }
        //}

        // Function to convert HTML content to PDF using DinkToPdf
        public byte[] RenderAndConvertToPdf(Microsoft.Reporting.NETCore.LocalReport viewer, string rdlcFilePath)
        {
            var htmlBytes = viewer.Render("HTML5"); // Render HTML first
            var htmlContent = System.Text.Encoding.UTF8.GetString(htmlBytes);

            // Load RDLC report settings like PageWidth, PageHeight, Margins
            var xdoc = XDocument.Load(rdlcFilePath);

            // Extract header/footer settings from RDLC
            var headerFontSize = ExtractFontSizeFromRdlc(xdoc, "Header");
            var headerFontFamily = ExtractFontFamilyFromRdlc(xdoc, "Header");

            var footerFontSize = ExtractFontSizeFromRdlc(xdoc, "Footer");
            var footerFontFamily = ExtractFontFamilyFromRdlc(xdoc, "Footer");

            // ConvertToMillimeters returns decimal?, and null check is added
            var pageWidth = (decimal?)ConvertToMillimeters(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "PageWidth")?.Value) ?? 210m; // Default A4 width in mm
            var pageHeight = (decimal?)ConvertToMillimeters(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "PageHeight")?.Value) ?? 297m; // Default A4 height in mm
            var leftMargin = (decimal?)ConvertToMillimeters(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "LeftMargin")?.Value) ?? 10m; // Default margin in mm
            var rightMargin = (decimal?)ConvertToMillimeters(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "RightMargin")?.Value) ?? 10m;
            var topMargin = (decimal?)ConvertToMillimeters(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "TopMargin")?.Value) ?? 10m;
            var bottomMargin = (decimal?)ConvertToMillimeters(xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "BottomMargin")?.Value) ?? 10m;

            // Determine orientation based on page dimensions
            var isLandscape = pageWidth > pageHeight;
            var orientation = isLandscape ? Orientation.Landscape : Orientation.Portrait;

            // Swap width and height if Landscape
            if (isLandscape)
            {
                var temp = pageWidth;
                pageWidth = pageHeight;
                pageHeight = temp;
            }

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = new PechkinPaperSize(pageWidth + "mm", pageHeight + "mm"),
                    Orientation = orientation,
                    Margins = new MarginSettings
                    {
                        Top = (double?)topMargin,
                        Right = (double?)rightMargin,
                        Bottom = (double?)bottomMargin,
                        Left = (double?)leftMargin
                    }
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                        // Add additional settings for better scaling
                        WebSettings = new DinkToPdf.WebSettings
                        {
                            DefaultEncoding = "utf-8",
                            EnableIntelligentShrinking = false, // Disable shrinking if content appears too small
                            LoadImages = true,
                            PrintMediaType = true,
                            MinimumFontSize = 12 // Ensures that font size doesn't get too small
                        },
                        // Optionally specify additional settings to handle page breaks, zoom, etc.
                        HeaderSettings = new HeaderSettings
                        {
                            FontSize = (int)headerFontSize,
                            //FontName = headerFontFamily,
                            //Line = true,
                            //Center = "Report Header" // Example content, modify as needed
                        },
                        // Apply footer settings from RDLC
                        FooterSettings = new FooterSettings
                        {
                            FontSize = (int)footerFontSize,
                            //FontName = footerFontFamily,
                            //Line = true,
                            //Center = "Page [page] of [toPage]" // Example content, modify as needed
                        }
                    }
                }
            };

            var file = _converter.Convert(pdf);
            return file;
        }

        // Function to extract font size from RDLC (based on header or footer)
        private decimal ExtractFontSizeFromRdlc(XDocument xdoc, string section)
        {
            var fontSizeElement = xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == $"{section}FontSize");
            return fontSizeElement != null ? decimal.Parse(fontSizeElement.Value, CultureInfo.InvariantCulture) : 9m; // Default to 9 if not found
        }

        // Function to extract font family from RDLC (based on header or footer)
        private string ExtractFontFamilyFromRdlc(XDocument xdoc, string section)
        {
            var fontFamilyElement = xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == $"{section}FontFamily");
            return fontFamilyElement != null ? fontFamilyElement.Value : "Arial"; // Default to Arial if not found
        }

        // Convert from points to millimeters (1 point = 0.3528 mm)
        public decimal? ConvertToMillimeters(string size)
        {
            if (string.IsNullOrEmpty(size))
                return null;

            // Convert size to millimeters
            var points = ConvertToPoints(size);
            return points * 0.3528m;
        }

        // Function to convert unit values to points (1 inch = 72 points, 1 cm ≈ 28.35 points)
        public decimal? ConvertToPoints(string size)
        {
            if (string.IsNullOrEmpty(size))
                return null;

            if (size.EndsWith("cm"))
            {
                var cmValue = decimal.Parse(size.Replace("cm", "").Trim(), CultureInfo.InvariantCulture);
                return cmValue * 28.35m; // Convert cm to points
            }
            else if (size.EndsWith("in"))
            {
                var inchValue = decimal.Parse(size.Replace("in", "").Trim(), CultureInfo.InvariantCulture);
                return inchValue * 72m; // Convert inches to points
            }

            return null; // Return null if unit is unknown
        }

        // Convert HTML to Excel
        private byte[] RenderAndConvertToExcel(Microsoft.Reporting.NETCore.LocalReport viewer)
        {
            var htmlBytes = viewer.Render("HTML5"); // Render HTML first
            var htmlContent = System.Text.Encoding.UTF8.GetString(htmlBytes);

            using (var memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(htmlContent);

                    var table = htmlDocument.DocumentNode.SelectSingleNode("//table");
                    if (table != null)
                    {
                        var rowIndex = 1;
                        var headers = table.SelectNodes(".//tr[1]/th");
                        if (headers != null)
                        {
                            var colIndex = 1;
                            foreach (var header in headers)
                            {
                                worksheet.Cells[rowIndex, colIndex++].Value = header.InnerText.Trim();
                            }
                            rowIndex++;
                        }

                        var rows = table.SelectNodes(".//tr[position()>1]");
                        foreach (var row in rows)
                        {
                            var colIndex = 1;
                            var cells = row.SelectNodes(".//td");
                            if (cells != null)
                            {
                                foreach (var cell in cells)
                                {
                                    worksheet.Cells[rowIndex, colIndex++].Value = cell.InnerText.Trim();
                                }
                            }
                            rowIndex++;
                        }
                    }

                    package.Save();
                }

                return memoryStream.ToArray();
            }
        }

        //Convert HTML to Word
        private byte[] RenderAndConvertToWord(Microsoft.Reporting.NETCore.LocalReport viewer)
        {
            var htmlBytes = viewer.Render("HTML5");
            var htmlContent = System.Text.Encoding.UTF8.GetString(htmlBytes);

            using (var memoryStream = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document(new Body());

                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(htmlContent);
                    ProcessHtmlNodes(htmlDocument.DocumentNode, mainPart.Document.Body);

                    mainPart.Document.Save();
                }

                return memoryStream.ToArray();
            }
        }

        private void ProcessHtmlNodes(HtmlNode node, DocumentFormat.OpenXml.Wordprocessing.Body body)
        {
            // Handle the different node types
            switch (node.NodeType)
            {
                case HtmlNodeType.Element:
                    // Process based on the element name
                    ProcessHtmlElement(node, body);
                    break;

                case HtmlNodeType.Text:
                    // Handle text nodes directly
                    var textParagraph = new Paragraph(new Run(new Text(node.InnerText.Trim())));
                    body.Append(textParagraph);
                    break;

                case HtmlNodeType.Comment:
                    // Optionally handle comments if needed
                    // In this example, comments are ignored
                    break;
            }

            // Process child nodes recursively
            foreach (var childNode in node.ChildNodes)
            {
                ProcessHtmlNodes(childNode, body);
            }
        }

        private void ProcessHtmlElement(HtmlNode node, DocumentFormat.OpenXml.Wordprocessing.Body body)
        {
            switch (node.Name.ToLower())
            {
                case "h1":
                case "h2":
                case "h3":
                    // Heading levels
                    var headingLevel = node.Name.ToLower() == "h1" ? "Heading1" :
                                       node.Name.ToLower() == "h2" ? "Heading2" : "Heading3";
                    var headingParagraph = new Paragraph(
                        new ParagraphProperties(new ParagraphStyleId() { Val = headingLevel }),
                        new Run(new Text(node.InnerText.Trim())));
                    body.Append(headingParagraph);
                    break;

                case "p":
                    // Paragraph
                    var paragraph = new Paragraph(new Run(new Text(node.InnerText.Trim())));
                    body.Append(paragraph);
                    break;

                case "table":
                    // Table
                    var table = new Table();
                    foreach (var row in node.SelectNodes("tr") ?? Enumerable.Empty<HtmlNode>())
                    {
                        var tableRow = new TableRow();
                        foreach (var cell in row.SelectNodes("td") ?? Enumerable.Empty<HtmlNode>())
                        {
                            var tableCell = new TableCell(new Paragraph(new Run(new Text(cell.InnerText.Trim()))));
                            tableRow.Append(tableCell);
                        }
                        table.Append(tableRow);
                    }
                    body.Append(table);
                    break;

                // Handle other HTML elements as needed

                default:
                    // Skip elements not handled
                    break;
            }
        }

        //Convert PDF to Power Point Used Aspose.Pdf nuget package
        //private byte[] ConvertPdfBytesToPpt(byte[] pdfBytes)
        //{
        //    using (var memoryStream = new MemoryStream(pdfBytes))
        //    {
        //        // Load the PDF document from the byte array
        //        Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(memoryStream);

        //        // Create a temporary memory stream for the PowerPoint file
        //        using (var pptStream = new MemoryStream())
        //        {
        //            // Convert the PDF to PowerPoint
        //            pdfDocument.Save(pptStream, Aspose.Pdf.SaveFormat.Pptx);

        //            // Return the PowerPoint file as a byte array
        //            return pptStream.ToArray();
        //        }
        //    }
        //}
        #endregion Converting files

    }
}