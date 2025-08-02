using Eduegate.Domain.Setting;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Eduegate.Domain.Report
{
    public class EmailReportViewerBL
    {
        private CallContext Context { get; set; }

        public EmailReportViewerBL(CallContext context)
        {
            Context = context;
        }

        public byte[] GetReportFile(string reportName, string reportParameter, string format, string rdlcFilePath = null)
        {
            try
            {
                string reportPhysicalPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath", null).ToString();

                var rdlPath = string.Format(@"{0}/{1}.rdl", reportPhysicalPath, reportName);

                if (!System.IO.File.Exists(rdlPath))
                {
                    if (string.IsNullOrEmpty(rdlcFilePath))
                    {
                        string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("Eduegate.Domain.Notification.dll", string.Empty);

                        rdlcFilePath = string.Format(@"{0}/{1}/{2}.rdl", fileDirPath, @"Areas/Reports/RDL", reportName);
                    }
                }
                else
                {
                    rdlcFilePath = rdlPath;
                }

                format = format ?? "HTML5";
                format = format.Replace("\"", string.Empty);

                var convertedReportParameters = string.IsNullOrEmpty(reportParameter) ?
                    new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(reportParameter);

                var reportParameters = new List<KeyValueDTO>();

                foreach (var parameter in convertedReportParameters)
                {
                    reportParameters.Add(new KeyValueDTO()
                    {
                        Key = parameter.Key,
                        Value = parameter.Value,
                    });
                }

                var reportDefinition = Framework.CacheManager
                    .MemCacheManager<Eduegate.Utilities.SSRSHelper.Report>
                    .Get("REPORT_" + rdlcFilePath);

                if (reportDefinition == null)
                {
                    reportDefinition = Utilities.SSRSHelper.Report
                        .GetReportFromFile(rdlcFilePath);
                    Framework.CacheManager.MemCacheManager<Eduegate.Utilities.SSRSHelper.Report>
                        .Add(reportDefinition, "REPORT_" + rdlcFilePath);
                }

                var viewer = new Microsoft.Reporting.NETCore.LocalReport();
                viewer.EnableHyperlinks = true;
                viewer.ReportPath = rdlcFilePath;
                viewer.EnableExternalImages = true;

                // Fill parameters
                DefaultReportParameters(viewer, reportParameters);

                viewer.DataSources.Clear();
                foreach (var dataset in reportDefinition.DataSets)
                {
                    var reportDataSource = new Microsoft.Reporting.NETCore.ReportDataSource(dataset.Name,
                            GetDataTable(dataset, reportParameters));
                    viewer.DataSources.Add(reportDataSource);
                }

                return GenerateAndConvertToFiles(viewer, format, reportName);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"{reportName} generation failed. Error message: {errorMessage}, reportPath: {rdlcFilePath}", ex);

                return null;
            }
        }

        private byte[] GenerateAndConvertToFiles(Microsoft.Reporting.NETCore.LocalReport viewer, string format, string reportName)
        {
            // Generate file for specified format and set filename in the response
            var filename = $"{reportName}_{DateTime.Now:yyyyMMdd_HHmmss}";

            // Determine the format and render accordingly
            switch (format.ToLower())
            {
                case "pdf":
                    var pdfBytes = viewer.Render("PDF");
                    return pdfBytes;

                case "excel":
                    var excelBytes = viewer.Render("EXCELOPENXML");
                    return excelBytes;

                case "word":
                    var wordBytes = viewer.Render("WORDOPENXML");
                    return wordBytes;

                case "csv":
                    var csvBytes = viewer.Render("CSV");
                    return csvBytes;

                case "xml":
                    var xmlBytes = viewer.Render("XML");
                    return xmlBytes;

                case "html5":
                default:
                    var htmlBytes = viewer.Render("HTML5"); // Render HTML                    
                    return htmlBytes;
            }
        }

        private void DefaultReportParameters(Microsoft.Reporting.NETCore.LocalReport viewer, List<KeyValueDTO> parameters)
        {
            var dateFormat = new SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");
            var reportDateFormat = new SettingBL(null).GetSettingValue<string>("ReportDateFormat", "yyyy-MM-dd");

            //RemoveDefaultParameters(parameters);
            var reportParameters = viewer.GetParameters();
            foreach (var parameter in parameters)
            {
                var paramDet = reportParameters.FirstOrDefault(p => p.Name == parameter.Key);
                if (paramDet != null)
                {
                    try
                    {
                        // Check if parameter.Value is null or empty
                        string[] parameterValues;

                        if (string.IsNullOrEmpty(parameter.Value))
                        {
                            // If parameter.Value is null or empty, set default value "0"
                            parameterValues = ["0"];
                        }
                        //else if (parameter.Value.Contains(","))
                        //{
                        //    // If parameter.Value contains commas, split it into multiple values
                        //    parameterValues = parameter.Value.Split(',');
                        //}
                        else
                        {
                            // If parameter.Value does not contain commas, use it as a single value
                            parameterValues = [parameter.Value];
                        }

                        var dataType = paramDet?.DataType.ToString()?.ToLower();
                        if (!string.IsNullOrEmpty(dataType) && dataType == "datetime")
                        {
                            // Parse the input string into a DateTime object using the known dd/MM/yyyy format
                            DateTime parsedDate = parameterValues[0] == null ? DateTime.Now : DateTime.ParseExact(parameterValues[0].ToString(), dateFormat, CultureInfo.InvariantCulture);

                            // Convert it back to a string using a standardized format (e.g., yyyy-MM-dd)
                            //string formattedDate = parsedDate.ToString(reportDateFormat);

                            // Convert it back to long date string
                            parameterValues = [parsedDate.ToLongDateString()];
                        }

                        viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter(parameter.Key, parameterValues, false));
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            var schoolID = Context.SchoolID;
            var academicYearID = Context.AcademicYearID;

            //Get school IDs
            var schoolID_Thumama = new Domain.Setting.SettingBL(null).GetSettingValue<short>("SCHOOLID_THUMAMA_10", 10);
            var schoolID_WestBay = new Domain.Setting.SettingBL(null).GetSettingValue<short>("SCHOOLID_WESTBAY_20", 20);
            var schoolID_Meshaf = new Domain.Setting.SettingBL(null).GetSettingValue<short>("SCHOOLID_MESHAF_30", 30);

            //Set school ID
            var schoolParam = parameters.FirstOrDefault(p => p.Key == "SchoolID");
            if (schoolParam != null)
            {
                schoolID = string.IsNullOrEmpty(schoolParam.Value) ? schoolID : short.Parse(schoolParam.Value) == 0 ? schoolID : short.Parse(schoolParam.Value);
                schoolParam.Value = schoolID.ToString();
            }
            if (reportParameters.Where(a => a.Name == "SchoolID").FirstOrDefault() != null)
            {
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("SchoolID", schoolID.HasValue ? schoolID.Value.ToString() : null, false));
            }

            //Set academic year ID
            var academicYearParam = parameters.FirstOrDefault(p => p.Key == "AcademicYearID");
            if (academicYearParam != null)
            {
                academicYearID = string.IsNullOrEmpty(academicYearParam.Value) ? academicYearID : short.Parse(academicYearParam.Value) == 0 ? academicYearID : short.Parse(academicYearParam.Value);
                academicYearParam.Value = academicYearID?.ToString();
            }
            if (reportParameters.Where(a => a.Name == "CurrentAcademicYearID").FirstOrDefault() != null)
            {
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("CurrentAcademicYearID", academicYearID.HasValue ? academicYearID.Value.ToString() : null, false));
            }

            //set root url
            var rootUrl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ERPRootUrl");
            if (reportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault() != null)
            {
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("RootUrl", rootUrl, false));
            }
            var rootUrlParam = parameters.FirstOrDefault(p => p.Key == "RootUrl");
            if (rootUrlParam != null)
            {
                rootUrlParam.Value = rootUrl;
            }

            //Set logo
            var logo = string.Empty;
            if (reportParameters.Where(a => a.Name == "Logo").FirstOrDefault() != null)
            {
                if (schoolID == schoolID_Meshaf)
                {
                    logo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("COMPANY_REPORT_LOGO_PODAR");
                }
                else
                {
                    logo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("COMPANY_REPORT_LOGO_PEARL");
                }
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("Logo", logo, false));
            }
            var logoParam = parameters.FirstOrDefault(p => p.Key == "Logo");
            if (logoParam != null)
            {
                logoParam.Value = logo;
            }

            //Set principal signature
            var signature = string.Empty;
            if (reportParameters.Where(a => a.Name == "Signature").FirstOrDefault() != null)
            {
                if (schoolID == schoolID_Meshaf)
                {
                    signature = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PRINCIPAL_SIGNATURE_01");
                }

                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("Signature", signature, false));
            }
            var signatureParam = parameters.FirstOrDefault(p => p.Key == "Signature");
            if (signatureParam != null)
            {
                signatureParam.Value = signature;
            }

            //Set footer
            var footer = string.Empty;
            if (reportParameters.Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
            {
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

                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("ReportFooter", footer, false));
            }
            var footerParam = parameters.FirstOrDefault(p => p.Key == "ReportFooter");
            if (footerParam != null)
            {
                footerParam.Value = footer;
            }

            //Set school seal
            var schoolSeal = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SCHOOL_STAMP");
            if (reportParameters.Where(a => a.Name == "SchoolSeal").FirstOrDefault() != null)
            {
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("SchoolSeal", schoolSeal, false));
            }
            var schoolSealParam = parameters.FirstOrDefault(p => p.Key == "SchoolSeal");
            if (schoolSealParam != null)
            {
                schoolSealParam.Value = schoolSeal;
            }

            //Set header baground color
            var headerBGColor = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_HEADER_BGCOLOR");
            if (reportParameters.Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
            {
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("HeaderBGColor", headerBGColor, false));
            }
            var headerBGColorParam = parameters.FirstOrDefault(p => p.Key == "HeaderBGColor");
            if (headerBGColorParam != null)
            {
                headerBGColorParam.Value = headerBGColor;
            }

            //Set header fore color
            var headerForeColor = new Domain.Setting.SettingBL(null).GetSettingValue<string>("REPORT_HEADER_FORECOLOR");
            if (reportParameters.Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
            {
                viewer.SetParameters(new Microsoft.Reporting.NETCore.ReportParameter("HeaderForeColor", headerForeColor, false));
            }
            var headerForeColorParam = parameters.FirstOrDefault(p => p.Key == "HeaderForeColor");
            if (headerForeColorParam != null)
            {
                headerForeColorParam.Value = headerForeColor;
            }
        }

        private void RemoveDefaultParameters(List<KeyValueDTO> parameters)
        {
            parameters.RemoveAll(param => param.Key == "RootUrl");
            parameters.RemoveAll(param => param.Key == "Logo");
            parameters.RemoveAll(param => param.Key == "SchoolID");
            parameters.RemoveAll(param => param.Key == "CurrentAcademicYearID");
            parameters.RemoveAll(param => param.Key == "Signature");
            parameters.RemoveAll(param => param.Key == "ReportFooter");
            parameters.RemoveAll(param => param.Key == "SchoolSeal");
            parameters.RemoveAll(param => param.Key == "HeaderBGColor");
            parameters.RemoveAll(param => param.Key == "HeaderForeColor");
            parameters.RemoveAll(param => param.Key == "TitleColor");
        }

        private System.Data.DataTable GetDataTable(Eduegate.Utilities.SSRSHelper.DataSet dataset, List<KeyValueDTO> parameters)
        {
            System.Data.DataTable re = new System.Data.DataTable();

            var dateFormat = new SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");
            var reportDateFormat = new SettingBL(null).GetSettingValue<string>("ReportDateFormat", "yyyy-MM-dd");

            using (var da = new System.Data.SqlClient.SqlDataAdapter(dataset.Query.CommandText,
               Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                da.SelectCommand.CommandType = dataset.Query.CommandType;

                if (dataset.Query.QueryParameters.Count > 0)
                {
                    foreach (var param in dataset.Query.QueryParameters)
                    {
                        string paramName = param.Name;
                        var parameter = parameters.Where(a => a.Key.ToLower() == paramName.ToLower().Replace("@", "")).FirstOrDefault();
                        object value1 = null;

                        if (parameter != null && !string.IsNullOrEmpty(parameter.Value))
                        {
                            value1 = parameter.Value;
                        }

                        var dataType = string.IsNullOrEmpty(param.DataType) ? param.DataType : param.DataType.ToLower();

                        switch (dataType)
                        {
                            case "string":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.VarChar)
                                {
                                    Value = value1 ?? string.Empty
                                });
                                break;
                            case "boolean":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.Bit)
                                {
                                    Value = true
                                });
                                break;
                            case "datetime":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.Date)
                                {
                                    Value = value1 == null ? (DateTime?)null : DateTime.ParseExact(value1.ToString(), dateFormat, CultureInfo.InvariantCulture)
                                });
                                break;
                            case "integer":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.Int)
                                {
                                    Value = value1 == null ? 0 : int.Parse(value1.ToString())
                                });
                                break;
                            case "float":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.Float)
                                {
                                    Value = value1 == null ? 0 : float.Parse(value1.ToString())
                                });
                                break;
                            case "decimal":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.Decimal)
                                {
                                    Value = value1 == null ? 0 : decimal.Parse(value1.ToString())
                                });
                                break;
                            case "double":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, SqlDbType.Decimal)
                                {
                                    Value = value1 == null ? 0 : double.Parse(value1.ToString())
                                });
                                break;
                            default:
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, ""));
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