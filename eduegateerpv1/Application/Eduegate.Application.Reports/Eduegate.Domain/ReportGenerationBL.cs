using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Eduegate.Domain
{
    public class ReportGenerationBL
    {
        private CallContext Context { get; set; }
        public Utilities.SSRSHelper.Report ReportDetails;
        public ReportGenerationBL(CallContext context)
        {
            Context = context;
        }

        public void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList)
        {
            var listFileNames = new List<string>();

            foreach (var collection in feeCollectionList)
            {
                var filename = ViewFeeReport(collection.FeeCollectionIID.ToString(), collection.AdmissionNo, collection.FeeReceiptNo, collection.EmailID, collection.SchoolID, collection.ReportName);

                listFileNames.Add(filename);
            }

            Email_FeeReceipt(feeCollectionList[0].EmailID, listFileNames);
        }


        public string SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            var listFileNames = new List<string>();

            if (gridData.StudentID.HasValue)
            {
                var filename = ViewFeeDueStatementReport(gridData);

                listFileNames.Add(filename);

                Email_FeeDueReport(gridData.ParentEmailID, listFileNames, gridData);
            }

            return null;
        }

        #region Reports

        public string ViewFeeReport(string collectionID, string admissionNo, string feeReceiptNo, string emailID, byte? schoolID, string reportName)
        {
            if(string.IsNullOrEmpty(reportName))
            {
                reportName = "FeeReceipt";
            }

            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + reportName + ".rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", admissionNo + '_' + feeReceiptNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", collectionID, false));
            rpt.SetParameters(new ReportParameter("FeeCollectionIID", collectionID, false));
            rpt.SetParameters(new ReportParameter("EmailID", emailID, false));

            return ViewReport(rpt, filename, schoolID);
        }

        public string ViewFeeDueStatementReport(MailFeeDueStatementReportDTO gridData)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + "FeeDueStatementAsOn.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;

            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", gridData.AdmissionNo + '_' + "" + '_' + currentDate.ToString("yyyyMMddHHmmss"));

            rpt.SetParameters(new ReportParameter("StudentID", gridData.StudentID.ToString(), false));
            rpt.SetParameters(new ReportParameter("AsOnDate", gridData.AsOnDate, false));

            return ViewReport(rpt, filename, (byte?)gridData.SchoolID);
        }

        public string ViewReport(LocalReport rpt, string filename, byte? schoolID)
        {
            var footer = new SettingBL().GetSettingDetail("REPORT_FOOTER");

            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }
            else
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = ConfigurationExtensions.GetAppConfigValue("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));
            }

            string filePath = Path.GetFullPath(filename);

            GeneratePDF(rpt, filePath);

            rpt.Dispose();

            var data = new FilePathResult(filePath, "application/pdf");

            return data.FileName;
        }

        public DataTable GetDataTable(Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {
            DataTable re = new DataTable();
            if (dataset.Query.CommandType == CommandType.StoredProcedure)
                return GetReportData(dataset, parameters);

            using (SqlDataAdapter da = new SqlDataAdapter(dataset.Query.CommandText, ConfigurationExtensions.GetConnectionString("dbEduegateERPContext")))
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

        public void GeneratePDF(LocalReport rpt, string filePath)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            rpt.EnableExternalImages = true;

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            using (FileStream stream = File.OpenWrite(filePath))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public void Email_FeeReceipt(string emailID, List<string> listFileNames)
        {
            var settingBL = new SettingBL(Context);

            string receiptBody = settingBL.GetSettingValue<string>("FEERECEIPT_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

            var emailBody = receiptBody;

            var emailSubject = "Fee Receipt";

            string replaymessage = PopulateBody(emailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet == "Live")
                {
                    SendReportMail(emailID, emailSubject, replaymessage, hostUser, listFileNames);
                }
                else
                {
                    SendReportMail(defaultMail, emailSubject, replaymessage, hostUser, listFileNames);
                }
            }

        }

        public void Email_FeeDueReport(string emailID, List<string> listFileNames, MailFeeDueStatementReportDTO gridData)
        {
            var settingBL = new SettingBL(Context);

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

            var emailBody = @"<p align='left'>Dear parent <br />Please find the Fee due Statement attached below</p><br />
                        <table align='left'>
                                <tr>
                                    <th>Student Details:</th>
                                </tr>
                                <tr>
                                    <td> Admission No:</td>
                                          <td>" + gridData.AdmissionNo + @"</td> </tr>

                                   <tr>
                        				<td>Student Name :</td>
                                       <td>" + gridData.StudentName + @"</td>  
                                      </tr>
                                   <tr>
                                          <td>Class :</td>
                                            <td>" + gridData.Class + @"</td>
                                         </tr>

                                           </table> <br /> <br /><br /><br /> <br /><br /><br /> <br />
                                        Regards <br/>" + gridData.SchoolName + @"<br/> <br/>  <br/> 
                            <p style = 'font-size:0.7rem;' <b> Please Note: </b>do not reply to this email as it is a computer generated email</p>";

            var emailSubject = "Fee DueStatement";

            string replaymessage = PopulateBody(emailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet == "Live")
                {
                    SendReportMail(emailID, emailSubject, replaymessage, hostUser, listFileNames);
                }
                else
                {
                    SendReportMail(defaultMail, emailSubject, replaymessage, hostUser, listFileNames);
                }
            }

        }

        private string PopulateBody(string Name, string htmlMessage)
        {
            #region SettingData binding

            var settingBL = new SettingBL(Context);

            var settingDatas = settingBL.GetAllSettingDatas();

            string clientName = settingDatas.Find(s => s.SettingCode == "CLIENTNAME").SettingValue;

            string clientMailLogo = settingDatas.Find(s => s.SettingCode == "CLIENT_MAILLOGO").SettingValue;

            string clientWebsite = settingDatas.Find(s => s.SettingCode == "CLIENT_WEBSITE").SettingValue;

            string poweredBy = settingDatas.Find(s => s.SettingCode == "POWEREDBYCOMPANYWEBSITE").SettingValue;

            string clientFB = settingDatas.Find(s => s.SettingCode == "CLIENT_FBSITE").SettingValue;

            string fbLogo = settingDatas.Find(s => s.SettingCode == "CLIENT_FBLOGO").SettingValue;

            string linkedInSite = settingDatas.Find(s => s.SettingCode == "CLIENT_LINKEDINSITE").SettingValue;

            string linkedInLogo = settingDatas.Find(s => s.SettingCode == "CLIENT_LINKEDINLOGO").SettingValue;

            string clientInsta = settingDatas.Find(s => s.SettingCode == "CLIENT_INSTASITE").SettingValue;

            string instaLogo = settingDatas.Find(s => s.SettingCode == "CLIENT_INSTALOGO").SettingValue;

            string clientYoutube = settingDatas.Find(s => s.SettingCode == "CLIENT_YOUTUBESITE").SettingValue;

            string youtubeLogo = settingDatas.Find(s => s.SettingCode == "CLIENT_YOUTUBELOGO").SettingValue;

            #endregion

            string body = "<!DOCTYPE html> <html> <head> <title></title> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1'> <meta http-equiv='X-UA-Compatible' content='IE=edge' /> <style type='text/css'> </style> </head> <body style='background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;'> <!-- HIDDEN PREHEADER TEXT --> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <!-- LOGO --> <tr> <td bgcolor='#bd051c' align='center'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#bd051c' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;'> <div align='center' style='width:100%;display:inline-block;text-align:center;'><img src='{CLIENT_MAILLOGO}' style='height:70px;margin:1rem;' /></div> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='left' style='padding: 1rem; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'>Hi <b>'{CUSTOMERNAME}'</b><br />{HTMLMESSAGE}</td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='black' align='center' style='padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #fffefe; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'> <div class='copyrightdiv' style='color: white;padding: 30px 30px 30px 30px;' >Copyright &copy; {YEAR}<a style='text-decoration: none' target='_blank' href='{CLIENT_WEBSITE}' > <span style='color: white; font-weight: bold;' >&nbsp;&nbsp; {CLIENT_NAME}</span > </a > </div > </td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='#f4f4f4' align='left' style='padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;' > <br > <div class='PoweredBy' style='text-align:center;' > <div style='padding-bottom:1rem;' > Powered By: <a style='text-decoration: none; color: #0c7aec;' id='eduegate' href='{POWEREDBYCOMPANYWEBSITE}' target='_blank' > SOFTOP SOLUTIONS PVT LTD</a > </div > <a href='{CLIENT_FBSITE}' > <img src='{CLIENT_FBLOGO}' alt='facebook' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='{CLIENT_LINKEDINSITE}' > <img src='{CLIENT_LINKEDINLOGO}' alt='linkedin' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='{CLIENT_INSTASITE}' > <img src='{CLIENT_INSTALOGO}' alt='instagram' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='{CLIENT_YOUTUBESITE}' > <img src='{CLIENT_YOUTUBELOGO}' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > </div > </td > </tr > </table > </td > </tr > </table > </body > </html >";

            #region Body data modification

            body = body.Replace("{CLIENT_MAILLOGO}", clientMailLogo);
            body = body.Replace("{CLIENT_WEBSITE}", clientWebsite);
            body = body.Replace("{POWEREDBYCOMPANYWEBSITE}", poweredBy);
            body = body.Replace("{CLIENT_NAME}", clientName);
            body = body.Replace("{CLIENT_FBSITE}", clientFB);
            body = body.Replace("{CLIENT_FBLOGO}", fbLogo);
            body = body.Replace("{CLIENT_LINKEDINSITE}", linkedInSite);
            body = body.Replace("{CLIENT_LINKEDINLOGO}", linkedInLogo);
            body = body.Replace("{CLIENT_INSTASITE}", clientInsta);
            body = body.Replace("{CLIENT_INSTALOGO}", instaLogo);
            body = body.Replace("{CLIENT_YOUTUBESITE}", clientYoutube);
            body = body.Replace("{CLIENT_YOUTUBELOGO}", youtubeLogo);

            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());

            #endregion

            return body;
        }

        public void SendReportMail(string email, string subject, string msg, string mailname, List<string> listFileNames)
        {
            try
            {
                var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
                var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();
                var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();

                //{
                //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                //                                      | SecurityProtocolType.Tls11
                //                                      | SecurityProtocolType.Tls12;
                //}

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

                foreach (var fileName in listFileNames)
                {
                    mailMsg.Attachments.Add(new System.Net.Mail.Attachment(fileName));
                }

                mailMsg.To.Add(email);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);
            }
            catch(Exception ex)
            {
                var errorMessage = ex.Message;
            }
        }

        #endregion

        public string GetStudentDetails(string headID, string transactionNo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                long headIID = long.Parse(headID);

                var transHead = dbContext.TransactionHeads.FirstOrDefault(t => t.HeadIID == headIID);


                var settingBL = new SettingBL(Context);
                var defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

                var studentDetails = dbContext.Students.FirstOrDefault(s => s.StudentIID == transHead.StudentID);

                var emailID = studentDetails.ParentID.HasValue ? studentDetails.Parent != null ? studentDetails.Parent.GaurdianEmail : defaultMail : defaultMail;

                var listFileNames = new List<string>();

                return studentDetails.AdmissionNumber + "#" + emailID + "#" + studentDetails.SchoolID;
            }
        }
        public void GenerateSalesOrderAndSendToMail(string headID, string transactionNo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                long headIID = long.Parse(headID);

                var transHead = dbContext.TransactionHeads.FirstOrDefault(t => t.HeadIID == headIID);


                var settingBL = new SettingBL(Context);
                var defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

                var studentDetails = dbContext.Students.FirstOrDefault(s => s.StudentIID == transHead.StudentID);

                var emailID = studentDetails.ParentID.HasValue ? studentDetails.Parent != null ? studentDetails.Parent.GaurdianEmail : defaultMail : defaultMail;

                var listFileNames = new List<string>();


                var filename = ViewSalesOrderReport(headID, transactionNo);

                listFileNames.Add(filename);

                Email_SalesOrderReport(emailID, listFileNames);
            }
        }

        public string ViewSalesOrderReport(string headID, string transactionNo)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + "SalesOrderReport.rdl";

            var currentDate = DateTime.Now;

            var settingBL = new SettingBL(Context);

            var schoolIID = settingBL.GetSettingValue<string>("ONLINEBRANCHID");

            var schoolID = byte.Parse(schoolIID);

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", transactionNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", headID, false));

            return ViewReport(rpt, filename, schoolID);
        }

        public void Email_SalesOrderReport(string emailID, List<string> listFileNames)
        {
            var settingBL = new SettingBL(Context);

            string receiptBody = settingBL.GetSettingValue<string>("SALESORDER_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

            var emailBody = receiptBody;

            var emailSubject = "Sales Order";

            string replaymessage = PopulateBody(emailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet == "Live")
                {
                    SendReportMail(emailID, emailSubject, replaymessage, hostUser, listFileNames);
                }
                else
                {
                    SendReportMail(defaultMail, emailSubject, replaymessage, hostUser, listFileNames);
                }
            }

        }

        #region Generate Sales Invoice & send mail
        public void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                long headIID = long.Parse(headID);

                var transHead = dbContext.TransactionHeads.FirstOrDefault(t => t.HeadIID == headIID);


                var settingBL = new SettingBL(Context);
                var defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

                var studentDetails = dbContext.Students.FirstOrDefault(s => s.StudentIID == transHead.StudentID);

                var emailID = studentDetails.ParentID.HasValue ? studentDetails.Parent != null ? studentDetails.Parent.GaurdianEmail : defaultMail : defaultMail;

                var listFileNames = new List<string>();


                var filename = ViewSalesInvoiceReport(headID, transactionNo);

                listFileNames.Add(filename);

                Email_SalesInvoiceReport(emailID, listFileNames);
            }
        }

        public string ViewSalesInvoiceReport(string headID, string transactionNo)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + "SalesInvoiceReport.rdl";

            var currentDate = DateTime.Now;

            var settingBL = new SettingBL(Context);

            var schoolIID = settingBL.GetSettingValue<string>("ONLINEBRANCHID");

            var schoolID = byte.Parse(schoolIID);

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", transactionNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", headID, false));

            return ViewReport(rpt, filename, schoolID);
        }

        public void Email_SalesInvoiceReport(string emailID, List<string> listFileNames)
        {
            var settingBL = new SettingBL(Context);

            string receiptBody = settingBL.GetSettingValue<string>("SALESINVOICE_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

            var emailBody = receiptBody;

            var emailSubject = "Sales Invoice";

            string replaymessage = PopulateBody(emailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet == "Live")
                {
                    SendReportMail(emailID, emailSubject, replaymessage, hostUser, listFileNames);
                }
                else
                {
                    SendReportMail(defaultMail, emailSubject, replaymessage, hostUser, listFileNames);
                }
            }

        }

        #endregion
        
        public void GenerateSalesOrderwithFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList)
        {
            var listFileNames = new List<string>();

            foreach (var collection in feeCollectionList)
            {
                var filename = ViewSOFeeReport(collection.FeeCollectionIID.ToString(), collection.AdmissionNo, collection.FeeReceiptNo, collection.EmailID, collection.SchoolID, collection.TransactionHeadID);

                listFileNames.Add(filename);
            }

            Email_FeeReceipt(feeCollectionList[0].EmailID, listFileNames);
        }


        public string ViewSOFeeReport(string collectionID, string admissionNo, string feeReceiptNo, string emailID, byte? schoolID, string transactionheadID)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + "SalesOrderwithFeeReceiptReport.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", admissionNo + '_' + feeReceiptNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", transactionheadID, false));
            rpt.SetParameters(new ReportParameter("HeadIID", transactionheadID, false));
            rpt.SetParameters(new ReportParameter("FeeCollectionID", collectionID, false));
            //rpt.SetParameters(new ReportParameter("EmailID", emailID, false));

            return ViewReport(rpt, filename, schoolID);
        }

        #region Salary Slip
        public void MailSalaryslip(List<SalarySlipDTO> salarySlipList)
        {
            var listFilecontent = new List<ContentFileDTO>();
           
            foreach (var data in salarySlipList)
            {
                var listFileNames = new List<string>();
                listFileNames.Add(ViewSalarySlip(data.SalarySlipIID, data.ReportData,data.ReportName));
                Mail_Salaryslip(data.EmployeeWorkEmail, listFileNames, data.EmployeeCode,data.SlipDate);
            }
           
        }

        public void Mail_Salaryslip(string emailID, List<string> listFileNames,string empCode, DateTime? slipDate)
        {
            var settingBL = new SettingBL(Context);

            string receiptBody = settingBL.GetSettingValue<string>("SALARYSLIP_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

            var emailBody = receiptBody;

            var emailSubject = "Salary Slip_"+(slipDate.Value).ToString("MMM") + "_"+slipDate.Value.Year +"_"+ empCode;

            string replaymessage = PopulateBody(emailID, emailBody);

            if (emailBody != "")
            {
                if (hostDet == "Live")
                {
                    SendReportMail(emailID, emailSubject, replaymessage, hostUser, listFileNames);
                }
                else
                {
                    SendReportMail(defaultMail, emailSubject, replaymessage, hostUser, listFileNames);
                }
            }
        }

        public string ViewSalarySlip(long salarySlipIID,  byte[] reportData,string reportName)
        {          
            string filename = string.Format("{0}\\{1}\\{2}\\{3}",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", reportName);
            string filePath = Path.GetFullPath(filename);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            byte[] bytes = reportData;

            using (FileStream stream = File.OpenWrite(filePath))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            var data = new FilePathResult(filePath, "application/pdf");

            return data.FileName;   
        }

        public List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList)
        {
            var listFilecontent = new List<ContentFileDTO>();

            foreach (var data in salarySlipList)
            {
                listFilecontent.Add(ViewSalarySlip(data.SalarySlipIID,(byte?) data.BranchID,data.SlipDate,data.EmployeeCode));
            }

            return listFilecontent;
        }

        public ContentFileDTO ViewSalarySlip(long salarySlipIID, byte? schoolID,DateTime? slipDate,string employeeCode)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + "SalarySlipReport.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", salarySlipIID + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("SalarySlipIID", salarySlipIID.ToString(), false));
            return SetSalarySlipParameters(rpt, filename, schoolID, salarySlipIID, slipDate, employeeCode);
        }

        public ContentFileDTO SetSalarySlipParameters(LocalReport rpt, string filename, byte? schoolID, long id, DateTime? slipDate, string employeeCode)
        {
            var contentFileDTO = new ContentFileDTO();
            var footer = new SettingBL().GetSettingDetail("REPORT_FOOTER");

            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }
            else
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = ConfigurationExtensions.GetAppConfigValue("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));
            }

            string filePath = Path.GetFullPath(filename);

            contentFileDTO = GenerateByte(rpt, filePath, filename, id,slipDate,employeeCode);

            rpt.Dispose();

            var data = new FilePathResult(filePath, "application/pdf");

            return contentFileDTO;
        }

        public ContentFileDTO GenerateByte(LocalReport rpt, string filePath, string filename, long id, DateTime? slipDate, string employeeCode)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            var contentFileDTO = new ContentFileDTO();
            rpt.EnableExternalImages = true;

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            using (FileStream stream = File.OpenWrite(filePath))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            return new ContentFileDTO()
            {
                ContentFileName = string.Format("{0}{1}{2}{3}.pdf",
                     "SalarySlip_", slipDate.Value.ToString("MMM")  , "_"+slipDate.Value.Year , "_"+employeeCode),
                ReferenceID = id,
                ContentData = bytes
            };
        }
        #endregion Salary Slip

        #region Student TC generation
        public ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO dto)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var listFilecontent = new ContentFileDTO();

                var transferDetails = dbContext.StudentTransferRequests.FirstOrDefault(a => a.StudentTransferRequestIID == dto.StudentTransferRequestIID);

                var studentDetails = dbContext.Students.FirstOrDefault(a => a.StudentIID == transferDetails.StudentID);

                listFilecontent = ViewTransferCertificates(dto, studentDetails.SchoolID, studentDetails.AdmissionNumber);

                return listFilecontent;
            }
        }

        public ContentFileDTO ViewTransferCertificates(StudentTransferRequestDTO dto, byte? schoolID, string admissionNumber)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + "StudentTCDiscReport.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", dto.StudentTransferRequestIID + '_' + currentDate.ToString("yyyyMMddHHmmss"));

            rpt.SetParameters(new ReportParameter("Remarks", dto.OtherRemarks.ToString(), false));
            rpt.SetParameters(new ReportParameter("CBSCRegNumber", dto.CBSC.ToString(), false));
            rpt.SetParameters(new ReportParameter("DateofCertIssue", currentDate.ToString(), false));
            rpt.SetParameters(new ReportParameter("PassedorFailed", dto.PassedorFailedString.ToString(), false));
            rpt.SetParameters(new ReportParameter("StudentTransferRequestIID", dto.StudentTransferRequestIID.ToString(), false));

            return SetTCParameters(rpt, filename, dto.StudentTransferRequestIID, schoolID, admissionNumber, currentDate);
        }

        public ContentFileDTO SetTCParameters(LocalReport rpt, string filename, long id, byte? schoolID, string admissionNumber, DateTime currentDate)
        {
            var contentFileDTO = new ContentFileDTO();
            var footer = new SettingBL().GetSettingDetail("REPORT_FOOTER");

            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }
            else
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = ConfigurationExtensions.GetAppConfigValue("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));
            }

            string filePath = Path.GetFullPath(filename);

            contentFileDTO = GenerateTCByte(rpt, filePath, filename, id, currentDate, admissionNumber);

            rpt.Dispose();

            var data = new FilePathResult(filePath, "application/pdf");

            return contentFileDTO;
        }

        public ContentFileDTO GenerateTCByte(LocalReport rpt, string filePath, string filename, long id, DateTime? currentDate, string admissionNumber)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            var contentFileDTO = new ContentFileDTO();
            rpt.EnableExternalImages = true;

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            using (FileStream stream = File.OpenWrite(filePath))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            return new ContentFileDTO()
            {
                ContentFileName = string.Format("{0}{1}{2}.pdf",
                     "StudentTC_", admissionNumber , "_" + currentDate.Value.ToShortDateString()),
                ReferenceID = id,
                ContentData = bytes
            };
        }
        #endregion

        #region Progress report generation
        public ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName)
        {
            var contentFile = ViewProgressReport(progressReport, reportName);

            return contentFile;
        }

        public ContentFileDTO ViewProgressReport(ProgressReportDTO progressReportDTO, string reportName)
        {
            string RptPath = ConfigurationExtensions.GetAppConfigValue("ReportPhysicalPath").ToString() + reportName;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;

            string filename = progressReportDTO.Student.Value + "_" + progressReportDTO.Class.Value + "_" + "Progress_Report";

            string filePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.pdf",
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Reports", "Temp", "Progress Report", filename);

            return SetProgressReportParameters(rpt, filePath, filename, progressReportDTO);
        }

        public ContentFileDTO SetProgressReportParameters(LocalReport rpt, string filePath, string filename, ProgressReportDTO dto)
        {
            var contentFileDTO = new ContentFileDTO();

            if (rpt.GetParameters().Where(a => a.Name == "SchoolID").FirstOrDefault() != null)
            {
                rpt.SetParameters(new ReportParameter("SchoolID", dto.SchoolID.ToString(), false));
            }

            if (rpt.GetParameters().Where(a => a.Name == "CLASSIDs").FirstOrDefault() != null)
            {
                rpt.SetParameters(new ReportParameter("CLASSIDs", dto.ClassID.ToString(), false));
            }

            if (rpt.GetParameters().Where(a => a.Name == "SECTIONIDs").FirstOrDefault() != null)
            {
                rpt.SetParameters(new ReportParameter("SECTIONIDs", dto.SectionID.ToString(), false));
            }

            if (rpt.GetParameters().Where(a => a.Name == "All_StudentIIDs").FirstOrDefault() != null)
            {
                rpt.SetParameters(new ReportParameter("All_StudentIIDs", dto.StudentID.ToString(), false));
            }

            if (rpt.GetParameters().Where(a => a.Name == "ACDEMICYEARID").FirstOrDefault() != null)
            {
                rpt.SetParameters(new ReportParameter("ACDEMICYEARID", dto.AcademicYearID.ToString(), false));
            }

            if (rpt.GetParameters().Where(a => a.Name == "AcademicYearID").FirstOrDefault() != null)
            {
                rpt.SetParameters(new ReportParameter("AcademicYearID", dto.AcademicYearID.ToString(), false));
            }

            var footer = new SettingBL().GetSettingDetail("REPORT_FOOTER");
            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (dto.SchoolID == 10)
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }
            else
            {
                var logo = new SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = ConfigurationExtensions.GetAppConfigValue("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));
            }

            string fileFullPath = Path.GetFullPath(filePath);

            contentFileDTO = GenerateProgressReportByte(rpt, fileFullPath, filename, dto.StudentID);

            rpt.Dispose();

            var data = new FilePathResult(filePath, "application/pdf");

            return contentFileDTO;
        }

        public ContentFileDTO GenerateProgressReportByte(LocalReport rpt, string fileFullPath, string filename, long? studentID)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            rpt.EnableExternalImages = true;

            if (!Directory.Exists(Path.GetDirectoryName(fileFullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileFullPath));
            }

            byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            using (FileStream stream = File.OpenWrite(fileFullPath))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            var contentFileDTO = new ContentFileDTO()
            {
                ContentFileName = string.Format("{0}.pdf", filename),
                ReferenceID = studentID,
                ContentData = bytes
            };

            return contentFileDTO;
        }
        #endregion report

    }
}