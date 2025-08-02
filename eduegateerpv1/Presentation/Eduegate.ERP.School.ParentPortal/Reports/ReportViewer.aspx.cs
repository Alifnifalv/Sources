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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using Eduegate.Domain.Repository;

namespace Eduegate.ERP.School.Portal.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        Framework.CallContext _CallContext = new Framework.CallContext();
        public Eduegate.Utilities.SSRSHelper.Report ReportDetails;
        public string RootUrl = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("RootUrl");

        protected override void OnInit(EventArgs e)
        {
            //viewer.PageNavigation += Viewer_PageNavigation;
            //viewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            //viewer.LocalReport.SetBasePermissionsForSandboxAppDomain(new System.Security.PermissionSet())
            //viewer.LocalReport.GetTotalPages()
            string siteCookieKey = ConfigurationExtensions.GetAppConfigValue("SiteCookieKey");
            string callContextKey = ConfigurationExtensions.GetAppConfigValue("ContextKey");
            _CallContext = XmlSerializerHelper.FromXml<Framework.CallContext>
                (Eduegate.Framework.Security.StringCipher.Decrypt(this.Page.Request.Cookies[callContextKey].Value, siteCookieKey));
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
            if (!IsPostBack) //added for calender filter auto refresh
            {
                if (string.IsNullOrEmpty(Request.QueryString["reportName"])) return;
                viewer.LocalReport.ReportPath = "Reports\\RDL\\" +  Path.GetFileName(Request.QueryString["reportName"]) + ".rdl";
                this.viewer.AsyncRendering = true;
                viewer.LocalReport.EnableHyperlinks = true;
                viewer.LocalReport.EnableExternalImages = true;
                GetReportDetails(Path.Combine(Server.MapPath("~"), viewer.LocalReport.ReportPath));
                if (Request.QueryString.AllKeys.Any(k => k == "returnFileBytes") && Convert.ToBoolean(Request.QueryString["returnFileBytes"].ToString()))
                {
                    var footer = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_FOOTER");
                    if (footer != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ReportFooter", footer.SettingValue, false));
                        }
                    }

                    if (_CallContext.SchoolID == 10) {
                        var logo = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                        var logo = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                        if (logo != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Logo", logo.SettingValue, false));
                            }
                        }
                    }
                    Response.Clear();
                    Response.ContentType = "text/javascript";
                    Response.Write(GetReport());
                    Response.End();
                }
                else
                {
                    foreach (var key in Request.QueryString.AllKeys)
                    {
                        if (key.ToUpper() != "REPORTNAME")
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter(key, Request.QueryString[key], false));
                        }
                    }

                    //set School ID
                    var schoolID = _CallContext.SchoolID;
                    if (schoolID != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "SchoolID").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("SchoolID", schoolID.ToString(), false));
                        }
                    }

                    var currentAcademicYearID = _CallContext.AcademicYearID;
                    if (currentAcademicYearID != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "CurrentAcademicYearID").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("CurrentAcademicYearID", currentAcademicYearID.ToString(), false));
                        }
                    }

                    var footer = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_FOOTER");
                    if (footer != null) {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ReportFooter", footer.SettingValue, false));
                        }
                    }

                    if (_CallContext.SchoolID == 10)
                    {
                        var logo = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                        var logo = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                        if (logo != null)
                        {
                            if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                            {
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Logo", logo.SettingValue, false));
                            }
                        }
                    }
                    //set root Url
                    var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();
                    if(rootUrl != null)
                    {
                        this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("RootUrl", RootUrl, false));
                    }
                    //REPORT_HEADER_BGCOLOR
                    var headerBGColor = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_HEADER_BGCOLOR");
                    if (headerBGColor != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                        }
                    }
                    var headerForeColor = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_HEADER_FORECOLOR");
                    if (headerForeColor != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                        }
                    }
                    var titleColor = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_TITLE_FORECOLOR");
                    if (titleColor != null)
                    {
                        if (this.viewer.LocalReport.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                        {
                            this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("TitleColor", titleColor.SettingValue, false));
                        }
                    }
                    GetReportDataSource();
                    viewer.LocalReport.Refresh();
                    var parameters = ReportDetails.ReportParameters.Where(a => a.Hidden == false).ToList();
                    ReportDetails.ReportParameters.RemoveAll(a => a.Hidden == false);
                    foreach (var parameter in parameters)
                    {
                        if (parameter.MultiValue)
                        {
                            parameter.DataType = "select2";
                        }
                    }
                    reportParameterPanel.DataSource = parameters;
                    btnView.Visible = false;
                    lblParameterHeading.Visible = false;
                    reportParameterPanel.DataBind();
                    foreach (var parameter in parameters)
                    {
                        if (string.IsNullOrEmpty(Request.QueryString["SCHOOLID"])) return;
                        var school_ID = Request.QueryString["SCHOOLID"];
                        if (string.IsNullOrEmpty(Request.QueryString["ACDEMICYEARID"])) return;
                        var accYearID = Request.QueryString["ACDEMICYEARID"];
                        if (string.IsNullOrEmpty(Request.QueryString["CLASSIDs"])) return;
                        var classID = Request.QueryString["CLASSIDs"];
                        if (string.IsNullOrEmpty(Request.QueryString["SECTIONIDs"])) return;
                        var sectionID = Request.QueryString["SECTIONIDs"];
                        if (string.IsNullOrEmpty(Request.QueryString["STUDENT_IDs"])) return;
                        var studentID = Request.QueryString["STUDENT_IDs"];
                        switch (parameter.Name)
                        {
                            case "SCHOOLID":
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter(parameter.Name, school_ID, false));
                                break;
                            case "ACDEMICYEARID":
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter(parameter.Name, accYearID, false));
                                break;
                            case "CLASSIDs":
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter(parameter.Name, classID, false));
                                break;
                            case "SECTIONIDs":
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter(parameter.Name, sectionID, false));
                                break;
                            case "STUDENT_IDs":
                                this.viewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter(parameter.Name, studentID, false));
                                break;
                            default:
                                break;
                        }
                    }
                    viewer.LocalReport.Refresh();
                }
            }
            else
            {
                GetReportDetails(Path.Combine(Server.MapPath("~"), viewer.LocalReport.ReportPath));
            }
        }

        private void GetReportDetails(string filePath)
        {
            //var fileName = Path.GetFileNameWithoutExtension(filePath);
            //ReportDetails = Framework.CacheManager.MemCacheManager<Eduegate.Utilities.SSRSHelper.Report>.Get("REPORT_" + fileName);

            //if (ReportDetails == null)
            {
                ReportDetails = Eduegate.Utilities.SSRSHelper.Report.GetReportFromFile(filePath);
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
                                if(app.Key.ToString().Equals("id"))
                                {
                                    if(!string.IsNullOrEmpty(parametreValue))
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

                //reportParametre.Values[0] = parametreValue;

            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                this.viewer.LocalReport.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, this.viewer.LocalReport.GetParameters())));
            }
        }

        //DataSet.cs
        public System.Data.DataTable GetDataTable(Eduegate.Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {
            System.Data.DataTable re = new System.Data.DataTable();
            if (dataset.Query.CommandType == CommandType.StoredProcedure)
                return GetReportData(dataset,parameters);
            using (SqlDataAdapter da =
               new SqlDataAdapter(dataset.Query.CommandText, ConfigurationExtensions.GetConnectionString("dbEduegateERPContext")))
            {
                if (dataset.Query.QueryParameters.Count > 0)
                {
                    SqlParameterCollection parmeter = da.SelectCommand.Parameters;

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
                                parmeter.Add(new SqlParameter(paramName, OleDbType.Decimal)
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

        private string GetReport()
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            foreach (var key in Request.QueryString.AllKeys)
            {
                if (key.ToUpper() != "REPORTNAME" && key.ToUpper() != "RETURNFILEBYTES")
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
            var pdfFormat = viewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", Guid.NewGuid());

            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }

            using (FileStream fs = new FileStream(filename, FileMode.Append))
            {
                fs.Write(pdfFormat, 0, pdfFormat.Length);
                fs.Flush();
                fs.Close();
            }

            // viewer.LocalReport.Refresh();

            //string contentType;
            ////Export the RDLC Report to Byte Array.
            //byte[] bytes = GetLocalReport().Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Button clicked.');", true);
            ////Download the RDLC Report in Word, Excel, PDF and Image formats.
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Request.QueryString["reportName"]) + "." + extension);
            //Response.BinaryWrite(bytes);
            //Response.Flush();
            ////var emailID = ReportDetails.ReportParameters.Where(a => a.Name == "EmailID").FirstOrDefault();
            ////{ 

            ////    if (Request.QueryString["EmailID"] != null && Request.QueryString["EmailID"] != string.Empty)
            ////    {
            ////        string mailID = Request.QueryString["EmailID"];                    
            ////        EmailProcess(bytes, mailID);
            ////    }
            ////}
            //Response.End();
            return Path.Combine(ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath").ToString(), "Reports", "Temp", Path.GetFileName(filename));
        }



        public void EmailProcess(byte[] attachments,string emailID)
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

                String replaymessage = PopulateBody(emailID, emailBody);

                MutualRepository mutualRepository = new MutualRepository();
                var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

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
                //mailMsg.From = new MailAddress(hostEmail);
                using (MemoryStream ms = new MemoryStream(attachments))
                {
                    mailMsg.Attachments.Add(new Attachment(ms, "SOMEFILENAMEANDJUNK"));
                    mailMsg.To.Add(email);
                    mailMsg.IsBodyHtml = true;
                    mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    ss.Send(mailMsg);
                }
               

            }
            catch (Exception ex)
            {

                //lb_error.Visible = true;
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


            //return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public class ReportServerCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
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
                        else if (parameter.ValidValues.ParameterValues != null) {
                            selectList.DataSource = parameter.ValidValues.ParameterValues;
                            selectList.DataTextField = "Label";
                            selectList.DataValueField = "Value";
                            selectList.DataBind();
                        }
                    }
                }
            }

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            GetReportDataSource();
            viewer.LocalReport.Refresh();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportDataSource();
            viewer.LocalReport.Refresh();
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            GetReportDataSource();
            viewer.LocalReport.Refresh();
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
        public System.Data.DataTable GetReportData(Eduegate.Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
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
    }
}