using Microsoft.Reporting.WebForms;
using Newtonsoft.Json.Linq;
using Eduegate.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Newtonsoft.Json;
using Eduegate.Utilities.Barcode;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Rectangle = iTextSharp.text.Rectangle;
using Eduegate.Domain;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.CertificateIssue;

namespace Eduegate.ERP.Admin.Reports
{

    public partial class ReportViewer : System.Web.UI.Page
    {
        private static int m_currentPageIndex;
        private static IList<Stream> m_streams;
        private static PrintDocument printdoc;

        Framework.CallContext _CallContext = new Framework.CallContext();
        private SqlParameterCollection parmeterCollection = null;
        public Eduegate.Utilities.SSRSHelper.Report ReportDetails;
        public string RootUrl = ConfigurationExtensions.GetAppConfigValue("RootUrl");
        private class Parameters
        {
            public string ParamName { get; set; }
            public string ParamValue { get; set; }
        }
        List<Parameters> parametervalues = new List<Parameters>();
        protected override void OnInit(EventArgs e)
        {
            //viewer.PageNavigation += Viewer_PageNavigation;
            //viewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            //viewer.LocalReport.SetBasePermissionsForSandboxAppDomain(new System.Security.PermissionSet())
            //viewer.LocalReport.GetTotalPages()

            string siteCookieKey = ConfigurationExtensions.GetAppConfigValue("SiteCookieKey");
            string callContextKey = ConfigurationExtensions.GetAppConfigValue("ContextKey");
            if (this.Page.Request.Cookies[callContextKey] != null)
            {
                _CallContext = XmlSerializerHelper.FromXml<Framework.CallContext>
                    (Eduegate.Framework.Security.StringCipher.Decrypt(this.Page.Request.Cookies[callContextKey].Value, siteCookieKey));
            }
            viewer.ShowPrintButton = false;
            base.OnInit(e);
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Viewer_PageNavigation(object sender, PageNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            viewer.Drillthrough += Viewer_Drillthrough;

            if (!IsPostBack) //added for calender filter auto refresh
            {
                if (Request.QueryString.AllKeys.Contains("CertificateLogIID"))
                {
                    long masterId = 0;
                    long.TryParse(Request.QueryString["CertificateLogIID"], out masterId);
                    var logDTO = new CertificateBL(null).GetCertificateLogByID(masterId);
                    var certificateTemplate = new CertificateBL(null).GetCertificateTemplateByID((long)logDTO.CertificateTemplateIID);
                    viewer.LocalReport.ReportPath = "Reports\\RDL\\" + Path.GetFileName(certificateTemplate.ReportName) + ".rdl";
                    viewer.LocalReport.Refresh();
                    viewer.AsyncRendering = true;
                    viewer.LocalReport.EnableHyperlinks = true;
                    viewer.LocalReport.EnableExternalImages = true;

                    var jsonvalue = JsonConvert.DeserializeObject(logDTO.ParameterValue);
                    var output = JsonConvert.DeserializeObject<List<Parameters>>(logDTO.ParameterValue);
                    foreach (Parameters param in output)
                    {
                        if (viewer.LocalReport.GetParameters().Where(a => a.Name == param.ParamName).FirstOrDefault() != null)
                        {
                            viewer.LocalReport.SetParameters(new ReportParameter(param.ParamName, param.ParamValue, false));
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.QueryString["reportName"]))
                        return;
                    viewer.LocalReport.ReportPath = "Reports\\RDL\\" + Path.GetFileName(Request.QueryString["reportName"]) + ".rdl";

                    this.viewer.AsyncRendering = true;
                    viewer.LocalReport.EnableHyperlinks = true;
                    viewer.LocalReport.EnableExternalImages = true;
                }

                GetReportDetails(Path.Combine(Server.MapPath("~"), viewer.LocalReport.ReportPath));
                if (viewer.LocalReport.GetParameters().Where(a => a.Name == "isWithLog").FirstOrDefault() != null)
                {
                    btnView.Visible = false;
                }
                else
                {
                    btnIssue.Visible = false;
                }


                
                if (viewer.LocalReport.GetParameters().Where(a => a.Name == "Hide_Excel_Word").FirstOrDefault() != null)
                {
                    string word = "WORDOPENXML";
                    string excel = "EXCELOPENXML";
                    //string pdf = "PDF";

                    var extensions = viewer.LocalReport.ListRenderingExtensions().Where(x => x.Name == word || x.Name == excel).ToList();

                    foreach (var extension in extensions)
                    {
                        System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                        fieldInfo.SetValue(extension, false);
                    }
                }

                // getting school_IDs from setting Table
                var schoolID_Thumama = new SettingBL(null).GetSettingDetail("SCHOOLID_THUMAMA_10");
                var schoolID_WestBay = new SettingBL(null).GetSettingDetail("SCHOOLID_WESTBAY_20");
                var schoolID_Meshaf = new SettingBL(null).GetSettingDetail("SCHOOLID_MESHAF_30");


                if (Request.QueryString.AllKeys.Any(k => k == "returnFileBytes") && Convert.ToBoolean(Request.QueryString["returnFileBytes"].ToString()))
                {

                    if (_CallContext.SchoolID == short.Parse(schoolID_Thumama.SettingValue))
                    {
                        var footer = new SettingBL(null).GetSettingDetail("REPORT_FOOTER_SCHOOL_ADDRESS_THUMAMA_10");
                        if (footer != null)
                        {
                            if (viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                            {
                                viewer.LocalReport.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                            }
                        }
                    }

                    else if (_CallContext.SchoolID == short.Parse(schoolID_WestBay.SettingValue))
                    {
                        var footer = new SettingBL(null).GetSettingDetail("REPORT_FOOTER_SCHOOL_ADDRESS_WESTBAY_20");
                        if (footer != null)
                        {
                            if (viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                            {
                                viewer.LocalReport.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                            }
                        }
                    }

                    else
                    {
                        var footer = new SettingBL(null).GetSettingDetail("REPORT_FOOTER_SCHOOL_ADDRESS_MESHAF_30");
                        if (footer != null)
                        {
                            if (viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                            {
                                viewer.LocalReport.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                            }
                        }
                    }


                    if (_CallContext.SchoolID == short.Parse(schoolID_Meshaf.SettingValue))
                    {
                        var logo = new SettingBL(null).GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                        if (logo != null)
                        {
                            if (viewer.LocalReport.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                            {
                                viewer.LocalReport.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                            }
                        }
                    }
                    else
                    {
                        var logo = new SettingBL(null).GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
                        if (logo != null)
                        {
                            if (viewer.LocalReport.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                            {
                                viewer.LocalReport.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                            }
                        }
                    }

                    var schoolSeal = new SettingBL(null).GetSettingDetail("SCHOOL_STAMP");
                    if (schoolSeal != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "SchoolSeal").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new ReportParameter("SchoolSeal", schoolSeal.SettingValue, false));
                        }
                    }

                    Response.Clear();
                    Response.ContentType = "text/javascript";
                    Response.Write(GetReport());
                    Response.End();
                }
                else
                {
                    if (!Request.QueryString.AllKeys.Contains("CertificateLogIID"))
                    {
                        foreach (var key in Request.QueryString.AllKeys)
                        {
                            if (key.ToUpper() != "REPORTNAME")
                            {
                                this.viewer.LocalReport.SetParameters(new ReportParameter(key, Request.QueryString[key], false));
                            }
                        }
                    }

                    //set School ID
                    var schoolID = _CallContext.SchoolID;
                    if (schoolID != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "SchoolID").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new ReportParameter("SchoolID", schoolID.ToString(), false));
                        }
                    }

                    var currentAcademicYearID = _CallContext.AcademicYearID;
                    if (currentAcademicYearID != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "CurrentAcademicYearID").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new ReportParameter("CurrentAcademicYearID", currentAcademicYearID.ToString(), false));
                        }
                    }

                    if (_CallContext.SchoolID == short.Parse(schoolID_Thumama.SettingValue))
                    {
                        var footer = new SettingBL(null).GetSettingDetail("REPORT_FOOTER_SCHOOL_ADDRESS_THUMAMA_10");
                        if (footer != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ReportFooter", footer.SettingValue, false));
                            }
                        }
                    }
                    else if (_CallContext.SchoolID == short.Parse(schoolID_WestBay.SettingValue))
                    {
                        var footer = new SettingBL(null).GetSettingDetail("REPORT_FOOTER_SCHOOL_ADDRESS_WESTBAY_20");
                        if (footer != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ReportFooter", footer.SettingValue, false));
                            }
                        }
                    }
                    else
                    {
                        var footer = new SettingBL(null).GetSettingDetail("REPORT_FOOTER_SCHOOL_ADDRESS_MESHAF_30");
                        if (footer != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ReportFooter", footer.SettingValue, false));
                            }
                        }
                    }

                    if (_CallContext.SchoolID == short.Parse(schoolID_Meshaf.SettingValue))
                    {
                        var signature = new SettingBL(null).GetSettingDetail("PRINCIPAL_SIGNATURE_01");
                        if (signature != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "Signature").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Signature", signature.SettingValue, false));
                            }
                        }
                    }

                    if (_CallContext.SchoolID == short.Parse(schoolID_Meshaf.SettingValue))
                    {
                        var logo = new SettingBL(null).GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                        if (logo != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Logo", logo.SettingValue, false));
                            }
                        }
                    }
                    else
                    {
                        var logo = new SettingBL(null).GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
                        if (logo != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Logo", logo.SettingValue, false));
                            }
                        }
                    }

                    var schoolSeal = new SettingBL(null).GetSettingDetail("SCHOOL_STAMP");
                    if (schoolSeal != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "SchoolSeal").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new ReportParameter("SchoolSeal", schoolSeal.SettingValue, false));
                        }
                    }

                    //set root Url
                    var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

                    if (rootUrl != null)
                    {
                        this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("RootUrl", RootUrl, false));
                    }

                    //var barcode = ReportDetails.ReportParameters.Where(a => a.Name == "Barcode").FirstOrDefault().ValidValues.ToString();

                    //if (barcode != null)
                    //{
                    //    this.viewer.LocalReport.SetParameters(new ReportParameter("BarcodeImage", GetItemBacCodeImage(barcode)), false);
                    //}

                    //REPORT_HEADER_BGCOLOR
                    var headerBGColor = new SettingBL(null).GetSettingDetail("REPORT_HEADER_BGCOLOR");
                    if (headerBGColor != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                        }
                    }

                    var headerForeColor = new SettingBL(null).GetSettingDetail("REPORT_HEADER_FORECOLOR");
                    if (headerForeColor != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                        }
                    }

                    //var titleColor = new SettingBL(null).GetSettingDetail("REPORT_TITLE_FORECOLOR");
                    //if (titleColor != null)
                    //{
                    //    if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                    //    {
                    //        this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("TitleColor", titleColor.SettingValue, false));
                    //    }
                    //}

                    GetReportDataSource();
                    viewer.LocalReport.Refresh();
                    var parameters = ReportDetails.ReportParameters.Where(a => a.Hidden == false).ToList();
                    ReportDetails.ReportParameters.RemoveAll(a => a.Hidden == false);

                    //viewer.LocalReport.SetParameters(new ReportParameter("RowsPerPage", "50", false));

                    foreach (var parameter in parameters)
                    {
                        if (parameter.MultiValue)
                        {
                            parameter.DataType = "select2";
                        }
                    }

                    reportParameterPanel.DataSource = parameters;

                    if (parameters.Count == 0)
                    {
                        btnView.Visible = false;
                        lblParameterHeading.Visible = false;
                    }

                    //btnView.Visible = false;
                    reportParameterPanel.DataBind();
                }
            }
            else
            {
                GetReportDetails(Path.Combine(Server.MapPath("~"), viewer.LocalReport.ReportPath));
            }
        }

        private void Viewer_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            //ERP.Admin.Controllers.HomeController homeController = new Controllers.HomeController();
            //homeController.ViewDrillReports(e.ReportPath, e.ReportPath);
            e.Cancel = true;
            //throw new NotImplementedException();
        }

        private void GetReportDetails(string filePath)
        {
            //var fileName = Path.GetFileNameWithoutExtension(filePath);
            //ReportDetails = Framework.CacheManager.MemCacheManager<Eduegate.Utilities.SSRSHelper.Report>.Get("REPORT_" + fileName);

            //if (ReportDetails == null)
            {
                ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(filePath);
                //Framework.CacheManager.MemCacheManager<Eduegate.Utilities.SSRSHelper.Report>.Add(ReportDetails, "REPORT_" + fileName);
            }
        }

        private void GetReportDataSource()
        {
            this.viewer.LocalReport.DataSources.Clear();
            var parameters = this.viewer.LocalReport.GetParameters();

            for (int index = 0; index < this.reportParameterPanel.Items.Count; index++)
            {
                var hiddenField = this.reportParameterPanel.Items[index].FindControl("HiddenField") as HiddenField;
                ReportParameterInfo reportParametre = parameters.Where(a => a.Name == hiddenField.Value).FirstOrDefault();
                string parametreValue = null;

                var htmlSelect = this.reportParameterPanel.Items[index].FindControl("Select2") as HtmlSelect;

                if (htmlSelect == null || htmlSelect.Visible)
                {
                    var selectedHiddenField = this.reportParameterPanel.Items[index].FindControl("Select2Hidden") as HiddenField;

                    if (selectedHiddenField != null && !string.IsNullOrEmpty(selectedHiddenField.Value))
                    {
                        var objects = JArray.Parse(selectedHiddenField.Value); // parse as array  
                        foreach (JObject root in objects)
                        {
                            foreach (KeyValuePair<String, JToken> app in root)
                            {
                                if (app.Key.ToString().Equals("id"))
                                {
                                    if (!string.IsNullOrEmpty(parametreValue))
                                    {
                                        parametreValue = parametreValue + ",";
                                    }
                                    else
                                    {
                                        parametreValue = "";
                                    }

                                    parametreValue = parametreValue + app.Value.ToString();
                                }
                            }
                        }
                    }

                }


                if (parametreValue == null)
                {
                    var textBox = this.reportParameterPanel.Items[index].FindControl("FreeTextField") as TextBox;

                    if (textBox == null || !textBox.Visible)
                    {
                        var datePicker = this.reportParameterPanel.Items[index].FindControl("DateField") as TextBox;

                        if (datePicker == null || !datePicker.Visible)
                        {
                            var drpList = this.reportParameterPanel.Items[index].FindControl("DropDownList") as DropDownList;

                            if (drpList != null || !drpList.Visible)
                            {
                                parametreValue = drpList.Text;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(datePicker.Text))
                            {
                                var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<string>("DateFormat");
                                parametreValue = DateTime.ParseExact(datePicker.Text, dateFormat, CultureInfo.InvariantCulture).ToShortDateString();
                            }
                        }
                    }
                    else
                    {
                        parametreValue = textBox.Text;
                    }
                }

                if (string.IsNullOrEmpty(parametreValue))
                {
                    parametreValue = null;
                }

                this.viewer.LocalReport.SetParameters(new ReportParameter(reportParametre.Name, parametreValue, false));


                parametervalues.Add(new Parameters { ParamName = reportParametre.Name, ParamValue = parametreValue });


            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                this.viewer.LocalReport.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, this.viewer.LocalReport.GetParameters())));
            }
        }

        //DataSet.cs
        public DataTable GetDataTable(Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {

            DataTable re = new DataTable();
            if (dataset.Query.CommandType == CommandType.StoredProcedure)
                return GetReportData(dataset, parameters);
            using (SqlDataAdapter da =
               new SqlDataAdapter(dataset.Query.CommandText, ConfigurationExtensions.GetConnectionString("dbEduegateERPContext")))
            {
                if (dataset.Query.QueryParameters.Count > 0)
                {
                    parmeterCollection = da.SelectCommand.Parameters;

                    foreach (var param in dataset.Query.QueryParameters)
                    {
                        string paramName = param.Name;
                        var parameter = parameters.Where(a => a.Name == paramName.Replace("@", "")).FirstOrDefault();
                        object value1 = null;
                        if (parameter != null && parameter.Values.Count > 0)
                        {
                            value1 = parameter.Values[0];

                        }
                        switch (param.DataType)
                        {
                            case "String":
                                parmeterCollection.Add(new SqlParameter(paramName, SqlDbType.VarChar)
                                {
                                    Value = value1 ?? string.Empty

                                });
                                break;
                            case "Boolean":
                                parmeterCollection.Add(new SqlParameter(paramName, SqlDbType.Bit)
                                {
                                    Value = true
                                });
                                break;
                            case "DateTime":
                                parmeterCollection.Add(new SqlParameter(paramName, SqlDbType.Date)
                                {
                                    Value = (value1 == null ? DateTime.Now : DateTime.Parse(value1.ToString()))
                                });
                                break;
                            case "Integer":
                                parmeterCollection.Add(new SqlParameter(paramName, SqlDbType.Int)
                                {
                                    Value = value1 == null ? 0 : int.Parse(value1.ToString())
                                });
                                break;
                            case "Float":
                                parmeterCollection.Add(new SqlParameter(paramName, OleDbType.Decimal)
                                {
                                    Value = value1 == null ? 0 : float.Parse(value1.ToString())
                                });
                                break;
                            default:
                                parmeterCollection.Add(new SqlParameter(paramName, ""));
                                break;
                        }
                    }
                }

                da.Fill(re);
                re.TableName = dataset.Name;
                return re;
            }
        }

        private string GetReport()
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            foreach (var key in Request.QueryString.AllKeys)
            {
                if (key.ToUpper() != "REPORTNAME" && key.ToUpper() != "RETURNFILEBYTES" && key.ToUpper() != "REPORTTITLE")
                {
                    this.viewer.LocalReport.SetParameters(new ReportParameter(key, Request.QueryString[key], false));
                }
            }
            //set root Url
            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();
            if (rootUrl != null)
            {
                this.viewer.LocalReport.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }
            GetReportDataSource();
            // Returns byte array of document
            var reportTitled = Path.GetFileName(Request.QueryString["reportTitle"]) != null ? Path.GetFileName(Request.QueryString["reportTitle"]) : Path.GetFileName(Request.QueryString["reportName"]);

            //reportTitled = reportTitled + Convert.ToString(DateTime.Now);
            var pdfFormat = viewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", reportTitled);

            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }
            using (FileStream fs = new FileStream(filename, FileMode.Append))
            {
                //var filepath = Path.Combine(ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath").ToString(), "Reports", "Temp", Path.GetFileName(filename));
                //if (File.Exists(filepath))
                //{
                //    File.Delete(filepath);
                //}
                fs.Write(pdfFormat, 0, pdfFormat.Length);
                //fs.Flush();
                //fs.Close();
            }

            return Path.Combine(ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath").ToString(), "Reports", "Temp", Path.GetFileName(filename));
        }



        public void EmailProcess(byte[] attachments, string emailID)
        {
            String emailBody = "";
            String emailSubject = "";

            emailBody = @"<br /><p align='left'>Dear Parent/Guardian,<br /><br /></p>
                            Thank you for payment of fees<br />
                            Kindly find the receipt for the payment.<br />
                            This is an online receipt generated which doesn’t require a signature.<br />
                            Any clarification please call School Accounts Dept.<br /><br />
                            Best regards<br /><br />
                            Podar Pearl School<br /><br />                        
                            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

            //emailBody = @"<br />
            //            <p align='left'>
            //            Dear Parent/Guardian,<br /></p>
            //            Fees has been collected.<br />
            //            please find the attachment herewith<br /><br />                        
            //            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

            emailSubject = "Fee Receipt";
            //var emaildata = new EmailNotificationDTO();
            try
            {
                var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();
                var hostDet = new SettingBL(null).GetSettingDetail("HOST_NAME").SettingValue;

                string defaultMail = new SettingBL(null).GetSettingDetail("DEFAULT_MAIL_ID").SettingValue;

                String replaymessage = PopulateBody(emailID, emailBody);

                //MutualRepository mutualRepository = new MutualRepository();
                //var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                if (emailBody != "")
                {
                    if (hostDet == "Live")
                    {
                        SendMail(emailID, emailSubject, replaymessage, hostUser, attachments);
                    }
                    else
                    {
                        SendMail(defaultMail, emailSubject, replaymessage, hostUser, attachments);
                    }
                }

            }
            catch { }

        }
        private String PopulateBody(String Name, String htmlMessage)
        {
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader("http://erp.eduegate.com/emailtemplate.html"))
            //{
            //    body = reader.ReadToEnd();
            //}
            body = "<!DOCTYPE html> <html> <head> <title></title> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1'> <meta http-equiv='X-UA-Compatible' content='IE=edge' /> <style type='text/css'> </style> </head> <body style='background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;'> <!-- HIDDEN PREHEADER TEXT --> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <!-- LOGO --> <tr> <td bgcolor='#bd051c' align='center'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#bd051c' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;'> <div align='center' style='width:100%;display:inline-block;text-align:center;'><img src='https://parent.pearlschool.org/img/podarlogo_mails.png' style='height:70px;margin:1rem;' /></div> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='left' style='padding: 1rem; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'>Hi <b>'{CUSTOMERNAME}'</b><br />{HTMLMESSAGE}</td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='black' align='center' style='padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #fffefe; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'> <div class='copyrightdiv' style='color: white;padding: 30px 30px 30px 30px;' >Copyright &copy; {YEAR}<a style='text-decoration: none' target='_blank' href='http://pearlschool.org/' > <span style='color: white; font-weight: bold;' >&nbsp;&nbsp; PEARL SCHOOL</span > </a > </div > </td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='#f4f4f4' align='left' style='padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;' > <br > <div class='PoweredBy' style='text-align:center;' > <div style='padding-bottom:1rem;' > Powered By: <a style='text-decoration: none; color: #0c7aec;' id='eduegate' href='https://softopsolutionpvtltd.com/' target='_blank' > SOFTOP SOLUTIONS PVT LTD</a > </div > <a href='https://www.facebook.com/pearladmin1/' > <img src='https://parent.pearlschool.org/Images/logo/fb-logo.png' alt='facebook' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.linkedin.com/company/pearl-school-qatar/?viewAsMember=true' > <img src='https://parent.pearlschool.org/Images/logo/linkedin-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.instagram.com/pearlschool_qatar/' > <img src='https://parent.pearlschool.org/Images/logo/insta-logo.png' alt='instagram' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.youtube.com/channel/UCFQKYMivtaUgeSifVmg79aQ' > <img src='https://parent.pearlschool.org/Images/logo/youtube-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > </div > </td > </tr > </table > </td > </tr > </table > </body > </html >";
            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }

        //public void SendMail(string email, string subject, string msg, string mailname, String maildomain, byte[] attachments)
        //{
        //    string email_id = email;
        //    string mail_body = msg;
        //    try
        //    {
        //        var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
        //        var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();
        //        var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();
        //        SmtpClient ss = new SmtpClient();
        //        ss.Host = ConfigurationExtensions.GetAppConfigValue("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
        //        ss.Port = ConfigurationExtensions.GetAppConfigValue<int>("smtpPort");// 587;//465;//;
        //        ss.Timeout = 20000;
        //        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        ss.UseDefaultCredentials = true;
        //        ss.EnableSsl = true;
        //        ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$
        //        MailMessage mailMsg = new MailMessage(hostPassword, email, subject, msg);
        //        mailMsg.From = new MailAddress(fromEmail, mailname);               
        //        mailMsg.IsBodyHtml = true;
        //        mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

        //        using (MemoryStream ms = new MemoryStream(attachments))
        //        {
        //            mailMsg.Attachments.Add(new Attachment(ms, "SOMEFILENAMEANDJUNK"));
        //        }

        //        ss.Send(mailMsg);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public void SendMail(string email, string subject, string msg, string mailname, byte[] attachments)
        {
            string email_id = email;
            string mail_body = msg;
            try
            {
                var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
                var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();
                var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();
                SmtpClient ss = new SmtpClient();
                ss.Host = ConfigurationExtensions.GetAppConfigValue("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = ConfigurationExtensions.GetAppConfigValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$
                MailMessage mailMsg = new MailMessage(hostEmail, email, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailname);
                using (MemoryStream ms = new MemoryStream(attachments))
                {
                    mailMsg.Attachments.Add(new Attachment(ms, "SOMEFILENAMEANDJUNK"));
                }
                mailMsg.To.Add(email);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);
            }
            catch (Exception ex)
            {
                //lb_error.Visible = true;
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public class ReportServerCredentials : IReportServerCredentials
        {
            public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
            {
                authCookie = null;
                userName = password = authority = null;
                return false;
            }
            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return WindowsIdentity.GetCurrent(); }
            }
            public System.Net.ICredentials NetworkCredentials
            {
                get { return new System.Net.NetworkCredential(ConfigurationExtensions.GetAppConfigValue("ReportServerDomainUser"), ConfigurationExtensions.GetAppConfigValue("ReportServerDomainPassword")); }
            }
        }

        protected void reportParameterPanel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList selectList = e.Item.FindControl("DropDownList") as DropDownList;
            if (selectList != null)
            {
                var parameter = e.Item.DataItem as Eduegate.Utilities.SSRSHelper.ReportParameter;

                if (parameter != null)
                {
                    if (parameter.ValidValues != null)
                    {
                        if (parameter.ValidValues.DataSetReference != null)
                        {
                            foreach (var dataSource in this.viewer.LocalReport.DataSources)
                            {
                                if (dataSource.Name == parameter.ValidValues.DataSetReference.DataSetName)
                                {
                                    selectList.DataSource = dataSource.Value;
                                    break;
                                }
                            }

                            selectList.DataTextField = parameter.ValidValues.DataSetReference.LabelField;
                            selectList.DataValueField = parameter.ValidValues.DataSetReference.ValueField;
                            selectList.DataBind();
                        }
                        else if (parameter.ValidValues.ParameterValues != null)
                        {
                            selectList.DataSource = parameter.ValidValues.ParameterValues;
                            selectList.DataTextField = "Label";
                            selectList.DataValueField = "Value";
                            selectList.DataBind();
                        }
                    }
                }
            }

        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            var collection = reportParameterPanel.Items;
            GetReportDataSource();
            viewer.LocalReport.Refresh();
            InsertIssueLog();

            if (viewer.LocalReport.ReportPath.Contains("StudentTCDiscReport"))
            {
                var parameters = this.viewer.LocalReport.GetParameters();
                var tcDTO = new StudentTransferRequestDTO()
                {
                    OtherRemarks = parameters[3].Values[0].ToString(),
                    IssuedDate = DateTime.Parse(parameters[7].Values[0]),
                    CBSC = parameters[4].Values[0].ToString(),
                    PassedorFailedString = parameters[6].Values[0].ToString(),
                    StudentTransferRequestIID = int.Parse(parameters[8].Values[0]),
                    //isWithLog = 
                };

                //var contentData = ClientFactory.ReportGenerationServiceClient(_CallContext).GenerateTransferRequestContentFile(tcDTO);

                //var savedContentata = ClientFactory.ContentServicesClient(_CallContext).SaveFile(contentData);

                //var updateStatus = ClientFactory.SchoolServiceClient(_CallContext).UpdateTCStatus(tcDTO.StudentTransferRequestIID, savedContentata.ContentFileIID);
            };
        }

        private void InsertIssueLog()
        {
            var headerBGColor = new SettingBL(null).GetSettingDetail("REPORT_HEADER_BGCOLOR");
            if (headerBGColor != null)
            {
                if (viewer.LocalReport.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    viewer.LocalReport.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }
            var reportName = Convert.ToString(Request.QueryString["reportName"]);
            viewer.LocalReport.Refresh();
            var certificateTemplate = new CertificateBL(null).GetCertificateTemplate(reportName);
            if (certificateTemplate.CertificateTemplateIID == 0)
            {
                var certificate = new CertificateTemplatesDTO
                {
                    CreatedBy = (int?)_CallContext.LoginID,
                    CreatedDate = DateTime.Now,
                    CertificateName = reportName,
                    ReportName = reportName
                };
                certificateTemplate = new CertificateBL(null).SaveCertificateTemplate(certificate);
            }
            var jsonvalue = JsonConvert.SerializeObject(parametervalues);
            var certificateLog = new CertificateLogsDTO
            {
                CreatedBy = (int?)_CallContext.LoginID,
                CreatedDate = DateTime.Now,
                CertificateTemplateIID = certificateTemplate.CertificateTemplateIID,
                //CertificateTemplateParameterID = certificateTemplateparams.CertificateTemplateParameterIID,
                ParameterValue = jsonvalue
            };
            var certificate_log = new CertificateBL(null).SaveCertificateLog(certificateLog);

            //var output = JsonConvert.DeserializeObject<dynamic>(ss);
        }
        //throw new NotImplementedException();


        protected void btnView_Click(object sender, EventArgs e)
        {
            GetReportDataSource();
            viewer.LocalReport.Refresh();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ReportPrint(viewer.LocalReport);
            return;
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Impression", "window.print()", true);
            return;
            //string _defPrinter = string.Empty;
            //foreach (var _printer in PrinterSettings.InstalledPrinters)
            //{
            //    PrinterSettings p = new PrinterSettings();
            //    p.PrinterName = Convert.ToString(_printer ?? string.Empty);
            //    //p.CreateMeasurementGraphics(new PageSettings().PaperSize.
            //    if (p.IsDefaultPrinter)
            //    {
            //        _defPrinter = Convert.ToString(_printer ?? string.Empty);//.PrinterName;
            //    }
            //}
            //print_microsoft_report(viewer.LocalReport, _defPrinter);
            //return;
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            var stringfile = HttpContext.Current.Server.MapPath("output.pdf");
            if (!File.Exists(stringfile))
            {
                File.Delete(HttpContext.Current.Server.MapPath("output.pdf"));
            }
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"), FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            //Open existing PDF
            var paperWidth = viewer.LocalReport.GetDefaultPageSettings().PaperSize.Width;
            var paperHeight = viewer.LocalReport.GetDefaultPageSettings().PaperSize.Height;
            var pgSize = new Rectangle(paperWidth, paperHeight);

            var leftMargin = viewer.LocalReport.GetDefaultPageSettings().Margins.Left;
            var rightMargin = viewer.LocalReport.GetDefaultPageSettings().Margins.Right;
            var topMargin = viewer.LocalReport.GetDefaultPageSettings().Margins.Top;
            var bottomMargin = viewer.LocalReport.GetDefaultPageSettings().Margins.Bottom;
            Document document = new Document(pgSize, leftMargin, rightMargin, topMargin, bottomMargin);

            PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath("output.pdf"));

            //Attach pdf to the iframe
            frmPrint.Attributes["src"] = "output.pdf";

            if (!File.Exists(HttpContext.Current.Server.MapPath("Print.pdf")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("Print.pdf"));
            }

            //Getting a instance of new PDF writer
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
               HttpContext.Current.Server.MapPath("Print.pdf"), FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            int i = 0;
            int p = 0;
            int n = reader.NumberOfPages;
            Rectangle psize = reader.GetPageSize(1);

            float width = psize.Width;
            float height = psize.Height;

            //Add Page to new document
            while (i < n)
            {
                document.NewPage();
                p++;
                i++;

                PdfImportedPage page1 = writer.GetImportedPage(reader, i);
                cb.AddTemplate(page1, 0, 0);
            }

            //Attach javascript to the document
            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
            writer.AddJavaScript(jAction);
            document.Close();






            //$http({ method: 'GET', url: "Home/ViewReports?returnFileBytes=true&HeadID=" + headIID + "&reportName=" + reportFullName })
            //    .then(function(filename) {
            //    var w = window.open();
            //    w.document.write('');
            //});

            //ReportPrint(viewer.LocalReport);
            //return;
            //GetReportDataSource();
            //viewer.LocalReport.Refresh();
            //#region Direct Print
            //Export(viewer.LocalReport);
            //m_currentPageIndex = 0;
            //Print();
            //#endregion
            //return;
            //Warning[] warnings;
            //string[] streamIds;
            //string contentType;
            //string encoding;
            //string extension;
            ////Export the RDLC Report to Byte Array.
            //byte[] bytes = GetLocalReport().Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Button clicked.');", true);
            ////Download the RDLC Report in Word, Excel, PDF and Image formats.
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Request.QueryString["reportName"]) + "." + extension);
            //Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();
        }

        protected void Export(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = GetLocalReport().Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Button clicked.');", true);
            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Request.QueryString["reportName"]) + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        private LocalReport GetLocalReport()
        {
            return viewer.LocalReport;
        }

        private object GetParamValue(ReportParameterInfo _sParamInfo)
        {
            object _sRetData = null;
            switch (_sParamInfo.DataType)
            {
                case ParameterDataType.Boolean:
                    _sRetData = "False";
                    break;
                case ParameterDataType.Integer:
                case ParameterDataType.Float:
                    _sRetData = "0";
                    break;
                default:
                    _sRetData = string.Empty;
                    break;
            }
            if (_sParamInfo.Values.Any())
            {
                _sRetData = string.Join(",", _sParamInfo.Values.ToArray());
                if (_sParamInfo.DataType == ParameterDataType.DateTime)
                {
                    DateTime _sDate = DateTime.Now;
                    DateTime.TryParse(_sParamInfo.Values[0], out _sDate);
                    _sRetData = _sDate.ToString("yyyyMMMdd");
                }
            }
            return _sRetData;
        }

        //DataSet.cs
        public DataTable GetReportData(Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {
            using (DataTable _sDt = new DataTable(dataset.Name))
            {
                using (SqlConnection _sConn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext")))
                {
                    try { _sConn.Open(); }
                    catch { return _sDt; }
                    using (SqlCommand _sCmd = new SqlCommand(dataset.Query.CommandText, _sConn))
                    {
                        _sCmd.CommandText = dataset.Query.CommandText;
                        _sCmd.CommandType = dataset.Query.CommandType;
                        foreach (var d in parameters)
                        {
                            if (dataset.Query.QueryParameters.Any(w => w.Name.Replace("@", string.Empty) == d.Name))
                            {
                                _sCmd.Parameters.AddWithValue("@" + d.Name.Replace("@", string.Empty), GetParamValue(d));
                            }
                        }
                        using (SqlDataReader _sDr = _sCmd.ExecuteReader())
                        {
                            _sDt.Load(_sDr);
                            return _sDt;
                        }
                    }
                }
            }
        }

        #region Code Created by Sudish for Print 
        //private int m_currentPageIndex;
        //private IList<Stream> m_streams;
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {

            //string deviceInfo =
            //  "<DeviceInfo>" +
            //  "  <OutputFormat>EMF</OutputFormat>" +
            //  "  <PageWidth>0in</PageWidth>" +
            //  "  <PageHeight>0in</PageHeight>" +
            //  "  <MarginTop>0in</MarginTop>" +
            //  "  <MarginLeft>0in</MarginLeft>" +
            //  "  <MarginRight>0in</MarginRight>" +
            //  "  <MarginBottom>0in</MarginBottom>" +
            //"</DeviceInfo>";


            PaperSize _pSize = report.GetDefaultPageSettings().PaperSize;
            Margins _pMargin = report.GetDefaultPageSettings().Margins;
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>" + _pSize.Width + "in</PageWidth>" +
              "  <PageHeight>" + _pSize.Height + "in</PageHeight>" +
              "  <MarginTop>" + _pMargin.Top + "in</MarginTop>" +
              "  <MarginLeft>" + _pMargin.Left + "in</MarginLeft>" +
              "  <MarginRight>" + _pMargin.Right + "in</MarginRight>" +
              "  <MarginBottom>" + _pMargin.Bottom + "in</MarginBottom>" +
              "</DeviceInfo>";
            if (report.GetDefaultPageSettings().IsLandscape)
            {
                deviceInfo =
                 "<DeviceInfo>" +
                 "  <OutputFormat>EMF</OutputFormat>" +
                 "<Orientation>Landscape</Orientation>" +
                 "  <PageWidth>" + _pSize.Width + "in</PageWidth>" +
                 "  <PageHeight>" + _pSize.Height + "in</PageHeight>" +
                 "  <MarginTop>" + _pMargin.Top + "in</MarginTop>" +
                 "  <MarginLeft>" + _pMargin.Left + "in</MarginLeft>" +
                 "  <MarginRight>" + _pMargin.Right + "in</MarginRight>" +
                 "  <MarginBottom>" + _pMargin.Bottom + "in</MarginBottom>" +
                 "</DeviceInfo>";
            }

            //SetPaperSizeA5(ref deviceInfo);

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            //Stream stream = new FileStream(Server.MapPath("~/" + Convert.ToString(Request.QueryString["ReportName"])), FileMode.Create);
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private string GetDefaultPrinterName()
        {
            string _defPrinter = string.Empty;
            foreach (var _printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                PageSettings pgSettings = new PageSettings();
                pgSettings.PaperSize = new PaperSize("A5 (210 x 148 mm)", 400, 200);
                PrinterSettings p = new PrinterSettings();
                p.CreateMeasurementGraphics(pgSettings);
                p.PrinterName = Convert.ToString(_printer ?? string.Empty);
                //p.CreateMeasurementGraphics(new PageSettings().PaperSize.
                if (p.IsDefaultPrinter)
                {
                    return Convert.ToString(_printer ?? string.Empty);//.PrinterName;
                }
            }
            return _defPrinter;
        }

        //public void Dispose()
        //{
        //    if (m_streams != null)
        //    {
        //        foreach (Stream stream in m_streams)
        //            stream.Close();
        //        m_streams = null;
        //    }
        //}
        private void ReportPrint(LocalReport report)
        {
            var pageSettings = new PageSettings();
            pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
            pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
            pageSettings.Margins = report.GetDefaultPageSettings().Margins;
            ReportPrint(report, pageSettings);
        }
        private void ReportPrint(LocalReport report, PageSettings pageSettings)
        {
            string deviceInfo =
                $@"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{pageSettings.PaperSize.Width * 100}in</PageWidth>
                <PageHeight>{pageSettings.PaperSize.Height * 100}in</PageHeight>
                <MarginTop>{pageSettings.Margins.Top * 100}in</MarginTop>
                <MarginLeft>{pageSettings.Margins.Left * 100}in</MarginLeft>
                <MarginRight>{pageSettings.Margins.Right * 100}in</MarginRight>
                <MarginBottom>{pageSettings.Margins.Bottom * 100}in</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;
            var streams = new List<Stream>();
            var currentPageIndex = 0;

            report.Render("Image", deviceInfo,
                (name, fileNameExtension, encoding, mimeType, willSeek) =>
                {
                    var stream = new MemoryStream();
                    streams.Add(stream);
                    return stream;
                }, out warnings);

            foreach (Stream stream in streams)
                stream.Position = 0;

            if (streams == null || streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            var printDocument = new PrintDocument();
            printDocument.DefaultPageSettings = pageSettings;
            if (!printDocument.PrinterSettings.IsValid)
                throw new Exception("Error: cannot find the default printer.");
            else
            {
                printDocument.PrintPage += (sender, e) =>
                {
                    Metafile pageImage = new Metafile(streams[currentPageIndex]);
                    System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                        e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                        e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                        e.PageBounds.Width,
                        e.PageBounds.Height);
                    e.Graphics.FillRectangle(Brushes.White, adjustedRect);
                    e.Graphics.DrawImage(pageImage, adjustedRect);
                    currentPageIndex++;
                    e.HasMorePages = (currentPageIndex < streams.Count);
                    e.Graphics.DrawRectangle(Pens.Red, adjustedRect);
                };
                printDocument.EndPrint += (Sender, e) =>
                {
                    if (streams != null)
                    {
                        foreach (Stream stream in streams)
                            stream.Close();
                        streams = null;
                    }
                };
                printDocument.Print();
            }
        }
        private void Print()
        {
            try
            {
                string printerName = GetDefaultPrinterName();
                //// try
                ////{
                //if (string.IsNullOrEmpty(printerName))
                //{
                //    printerName = GetDefaultPrinterName();
                //}
                if (m_streams == null || m_streams.Count == 0)
                    return;



                PrintDocument printDoc = new PrintDocument();


                //printDoc.DefaultPageSettings.PaperSize = paperSize;
                printDoc.PrinterSettings.PrinterName = printerName;
                ////printDoc.PrintController = prv;

                // string _rptName = Request.QueryString["ReportName"] ?? string.Empty;
                //var data = A5Settings.Where(w => w.ReportName == _rptName);

                printDoc.DefaultPageSettings.PaperSize = viewer.LocalReport.GetDefaultPageSettings().PaperSize;
                printDoc.DefaultPageSettings.Margins = viewer.LocalReport.GetDefaultPageSettings().Margins;
                printDoc.DefaultPageSettings.Landscape = viewer.LocalReport.GetDefaultPageSettings().IsLandscape;
                printDoc.PrinterSettings.DefaultPageSettings.Landscape = viewer.LocalReport.GetDefaultPageSettings().IsLandscape;
                printDoc.PrinterSettings.DefaultPageSettings.PaperSize = viewer.LocalReport.GetDefaultPageSettings().PaperSize;
                printDoc.PrinterSettings.DefaultPageSettings.Margins = viewer.LocalReport.GetDefaultPageSettings().Margins;

                if (!printDoc.PrinterSettings.IsValid)
                {

                    string msg = String.Format(
                       "Can't find printer \"{0}\".", printerName);
                    //   MessageBox.Show(msg, "Print Error");
                    throw new Exception(msg);
                    return;
                }
                //viewer.LocalReport.GetDefaultPageSettings().IsLandscape
                //printDoc.PrinterSettings.LandscapeAngle = true;

                //printDoc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.Landscape = true;

                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

                printDoc.Print();
            }
            catch (Exception ex)
            {

            }
        }

        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //ev.PageSettings.Landscape = true;
            //ev.PageSettings.PrinterSettings.DefaultPageSettings.Landscape = true;

            // Adjust rectangular area with printer margins.
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            //ev.PageSettings.PaperSize = ReportViewer1.LocalReport.GetDefaultPageSettings().PaperSize;
            //ev.PageSettings.Landscape = ReportViewer1.LocalReport.GetDefaultPageSettings().IsLandscape;
            //ev.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = ReportViewer1.LocalReport.GetDefaultPageSettings().PaperSize;
            //ev.PageSettings.PrinterSettings.DefaultPageSettings.Landscape = ReportViewer1.LocalReport.GetDefaultPageSettings().IsLandscape;

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);
            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
            //if (paperSize != null)
            //{
            //    ev.PageSettings.PaperSize = paperSize;
            //    ev.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
            //}
            //HttpContext.Current.Response.Write("<script>window.print();</script>");
            //ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            //m_currentPageIndex++;
            //ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public byte[] GetItemBacCodeImage(string barcode)
        {
            GenerateBarcode g = new GenerateBarcode();

            return g.Get128BacCodeImage(barcode);
        }

        #region Print Test

        //DirectPrint directprint = new DirectPrint()
        //DirectPrint.print_microsoft_report()
        public static void print_microsoft_report(LocalReport report, string printer_name = null)
        {
            var page_width = report.GetDefaultPageSettings().PaperSize.Width;
            var page_height = report.GetDefaultPageSettings().PaperSize.Height;
            var islandscap = report.GetDefaultPageSettings().IsLandscape;

            printdoc = new PrintDocument();
            if (printer_name != null)
                printdoc.PrinterSettings.PrinterName = printer_name;
            if (!printdoc.PrinterSettings.IsValid)
                throw new Exception("Cannot find the specified printer");
            else
            {
                PaperSize ps = new PaperSize("Custom", page_width, page_height);
                printdoc.DefaultPageSettings.PaperSize = ps;
                printdoc.DefaultPageSettings.Landscape = islandscap;
                ExportReport(report);
                PrintReport();
            }
        }

        private static void PrintReport()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            printdoc.PrintPage += PrintReportPage;
            m_currentPageIndex = 0;
            printdoc.Print();
        }

        private static void PrintReportPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(ev.PageBounds.Left - System.Convert.ToInt32(ev.PageSettings.HardMarginX), ev.PageBounds.Top - System.Convert.ToInt32(ev.PageSettings.HardMarginY), ev.PageBounds.Width, ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex += 1;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private static void ExportReport(LocalReport report)
        {
            int w;
            int h;
            printdoc = new PrintDocument();
            if (printdoc.DefaultPageSettings.Landscape == true)
            {
                w = printdoc.DefaultPageSettings.PaperSize.Height;
                h = printdoc.DefaultPageSettings.PaperSize.Width;
            }
            else
            {
                w = printdoc.DefaultPageSettings.PaperSize.Width;
                h = printdoc.DefaultPageSettings.PaperSize.Height;
            }
            string deviceInfo = "<DeviceInfo>" + "<OutputFormat>EMF</OutputFormat>" + "<PageWidth>" + w / (double)100 + "in</PageWidth>" + "<PageHeight>" + h / (double)100 + "in</PageHeight>" + "<MarginTop>0.0in</MarginTop>" + "<MarginLeft>0.0in</MarginLeft>" + "<MarginRight>0.0in</MarginRight>" + "<MarginBottom>0.0in</MarginBottom>" + "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreatePrintStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private static Stream CreatePrintStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        #endregion Print Test
    }
}
#endregion