using Eduegate.Domain.Entity;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace Eduegate.Domain.Report
{
    public class ReportGenerationBL
    {
        private CallContext Context { get; set; }
        public Utilities.SSRSHelper.Report ReportDetails;

        public ReportGenerationBL(CallContext context)
        {
            Context = context;
        }

        #region Generate and send fee receipt
        public void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList, EmailTypes mailType)
        {
            var listFileNames = new List<string>();

            try
            {
                if (feeCollectionList.Count > 0)
                {
                    foreach (var collection in feeCollectionList)
                    {
                        var filename = ViewFeeReport(collection.FeeCollectionIID.ToString(), collection.AdmissionNo, collection.FeeReceiptNo, collection.EmailID, collection.SchoolID, collection.ReportName, collection.IsOnlineStore, collection.AcadamicYearID);

                        listFileNames.Add(filename);
                    }

                    Email_FeeReceipt(feeCollectionList[0], listFileNames, mailType);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                Eduegate.Logger.LogHelper<ReportGenerationBL>.Fatal(ex.Message, ex);
            }
        }

        public string ViewFeeReport(string collectionID, string admissionNo, string feeReceiptNo, string emailID, byte? schoolID, string reportName, bool isOnlineStore, int? acadamicYearID = null)
        {
            if (string.IsNullOrEmpty(reportName))
            {
                reportName = "FeeReceipt";
            }
            var reportingService = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");
            string reportNewPath = string.Empty;

            var iswillForReturn = isOnlineStore == true ? "1" : "0";

            if (reportingService.ToLower() == "ssrs")
            {
                var reportPath = GetReportUrlAndDefaultParameters(reportName, schoolID, acadamicYearID);

                if (!string.IsNullOrEmpty(reportPath))
                {
                    var uriBuilder = new UriBuilder(reportPath);
                    var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                    query["HeadID"] = collectionID;
                    query["FeeCollectionIID"] = collectionID;
                    query["EmailID"] = emailID;
                    query["IsWillForReturn"] = iswillForReturn;

                    uriBuilder.Query = query.ToString();

                    var reportUrl = uriBuilder.ToString();

                    reportNewPath = reportUrl;
                }
            }
            else
            {
                // Convert to JSON string
                string parameterString = JsonConvert.SerializeObject(new
                {
                    HeadID = collectionID,
                    FeeCollectionIID = collectionID,
                    EmailID = emailID,
                    IsWillForReturn = iswillForReturn,
                    SchoolID = schoolID,
                    AcademicYearID = acadamicYearID
                });

                reportNewPath = "reportName=" + reportName + "&parameter=" + parameterString;
            }

            return reportNewPath;
        }

        public void Email_FeeReceipt(FeeCollectionDTO feeCollectionDTO, List<string> listFileNames, EmailTypes mailType)
        {
            try
            {
                var emailTemplate = new NotificationEmailTemplateDTO();
                var emailSubject = string.Empty;
                var emailBody = string.Empty;

                string toEmailID = feeCollectionDTO.EmailID;
                string schoolShortName = feeCollectionDTO.StudentSchoolShortName?.ToLower();

                if (string.IsNullOrEmpty(schoolShortName))
                {
                    if (feeCollectionDTO.SchoolID.HasValue)
                    {
                        var schoolData = new Eduegate.Domain.Setting.SettingBL(Context).GetSchoolDetailByID(feeCollectionDTO.SchoolID.Value);

                        schoolShortName = schoolData?.SchoolShortName?.ToLower();
                    }
                }

                var mailParameters = new Dictionary<string, string>()
                {
                    { "SCHOOL_SHORT_NAME", schoolShortName},
                };

                var settingBL = new Domain.Setting.SettingBL(Context);

                string receiptBody = settingBL.GetSettingValue<string>("FEERECEIPT_EMAILBODY_CONTENT");

                string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

                string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

                emailTemplate = EmailTemplateMapper.Mapper(Context).GetEmailTemplateDetails(EmailTemplates.FeeReceiptMail.ToString());

                emailSubject = emailTemplate.Subject;

                emailBody = emailTemplate.EmailTemplate;

                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody);

                DateTime currentDate = DateTime.Now;
                var fileNameFormat = "FeeReceipt" + '_' + currentDate.ToString("yyyyMMddHHmmss");

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, mailType, mailParameters, listFileNames, fileNameFormat);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, mailType, mailParameters, listFileNames, fileNameFormat);
                    }
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ReportGenerationBL>.Fatal(ex.Message, ex);
            }
        }
        #endregion Generate and send fee receipt

        #region Generate and send fee due report
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

        public string ViewFeeDueStatementReport(MailFeeDueStatementReportDTO gridData)
        {
            var reportName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEE_DUE_STATEMENT_REPORT");

            string reportNewPath = string.Empty;

            var reportingService = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            if (reportingService.ToLower() == "ssrs")
            {
                var reportPath = GetReportUrlAndDefaultParameters(reportName, (byte?)gridData.SchoolID, null);

                if (!string.IsNullOrEmpty(reportPath))
                {
                    var uriBuilder = new UriBuilder(reportPath);
                    var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                    if (DateTime.TryParseExact(gridData.AsOnDate, "MM/dd/yyyy", CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out DateTime parsedDate))
                    {
                        query["AsOnDate"] = parsedDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        query["AsOnDate"] = gridData.AsOnDate;
                    }

                    query["StudentID"] = gridData.StudentID.ToString();

                    uriBuilder.Query = query.ToString();

                    var reportUrl = uriBuilder.ToString();

                    reportNewPath = reportUrl;
                }
            }
            else
            {
                var asOnDateString = "";
                if (DateTime.TryParseExact(gridData.AsOnDate, "MM/dd/yyyy", CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime parsedDate))
                {
                    asOnDateString = parsedDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    asOnDateString = gridData.AsOnDate;
                }

                // Convert to JSON string
                string parameterString = JsonConvert.SerializeObject(new
                {
                    StudentID = gridData.StudentID.ToString(),
                    AsOnDate = asOnDateString,
                });

                reportNewPath = "reportName=" + reportName + "&parameter=" + parameterString;
            }

            return reportNewPath;
        }

        public void Email_FeeDueReport(string toEmailID, List<string> listFileNames, MailFeeDueStatementReportDTO gridData)
        {
            string classCode = string.Empty;

            if (string.IsNullOrEmpty(classCode))
            {
                var classID = gridData.ClassID.HasValue ? Convert.ToInt32(gridData.ClassID) : (int?)null;
                if (classID.HasValue)
                {
                    var data = new Eduegate.Domain.Setting.SettingBL(Context).GetClassDetailByClassID(classID.Value);

                    classCode = data?.Code?.ToLower();
                }
            }
            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var settingBL = new Domain.Setting.SettingBL(Context);

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            string mailClosingRegard = settingBL.GetSettingValue<string>("FEEDUE_MAIL_CLOSING_REGARDS");
            string mailClosing = settingBL.GetSettingValue<string>("FEEDUE_MAIL_REGARD_CONTENT");

            var emailTemplate = EmailTemplateMapper.Mapper(Context).GetEmailTemplateDetails(EmailTemplates.FeeDueStatementMail.ToString());

            var emailSubject = string.Empty;
            var emailBody = string.Empty;
            if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
            {
                emailSubject = emailTemplate.Subject;

                emailBody = emailTemplate.EmailTemplate;

                emailBody = emailBody.Replace("{ADMISSION_NO}", gridData.AdmissionNo);
                emailBody = emailBody.Replace("{STUDENT_NAME}", gridData.StudentName);
                emailBody = emailBody.Replace("{CLASS}", gridData.Class);
            }
            else
            {
                emailSubject = "Fee Due Statement";

                emailBody = @"<p align='left'>Dear parent <br />Please find the Fee due Statement attached below</p><br />
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

                                           </table><br/> <br/>  <br/> 
                            <p style = 'font-size:0.7rem;' <b> Please Note: </b>do not reply to this email as it is a computer generated email</p>";
            }

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody, mailClosingRegard, mailClosing);

            DateTime currentDate = DateTime.Now;
            var fileNameFormat = gridData.AdmissionNo + '_' + currentDate.ToString("yyyyMMddHHmmss");

            string ccMailValue = settingBL.GetSettingValue<string>("TICKETING_CC_MAILS", null);
            var ccMailIDs = new List<string>();
            var splitMails = ccMailValue != null ? ccMailValue.Split(",") : null;

            if (splitMails != null)
            {
                foreach (var mail in splitMails)
                {
                    ccMailIDs.Add(mail.Trim());
                }
            }

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, EmailTypes.FeeDueStatement, mailParameters, listFileNames, fileNameFormat, ccMailIDs);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, EmailTypes.FeeDueStatement, mailParameters, listFileNames, fileNameFormat, ccMailIDs);
                }
            }
        }
        #endregion Generate and send fee due report

        #region Proforma Invoice
        public string SendProformaInvoiceToParent(MailFeeDueStatementReportDTO reportDTO)
        {
            var listFileNames = new List<string>();

            var reportName = reportDTO.ReportName;
            var filename = string.Empty;

            var reportingService = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");

            if (reportDTO.StudentID.HasValue)
            {
                if (reportingService.ToLower() == "ssrs")
                {
                    var reportPath = GetReportUrlAndDefaultParameters(reportName, (byte?)reportDTO.SchoolID);

                    if (!string.IsNullOrEmpty(reportPath))
                    {
                        var uriBuilder = new UriBuilder(reportPath);
                        var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                        query["StudentIID"] = reportDTO.StudentID.ToString();

                        uriBuilder.Query = query.ToString();

                        var reportUrl = uriBuilder.ToString();

                        filename = reportUrl;
                    }
                }
                else
                {
                    // Convert to JSON string
                    string parameterString = JsonConvert.SerializeObject(new
                    {
                        StudentIID = reportDTO.StudentID.ToString(),
                    });

                    filename = "reportName=" + reportName + "&parameter=" + parameterString;
                }

                listFileNames.Add(filename);

                Email_ProformaInvoice(reportDTO.ParentEmailID, listFileNames, reportDTO);
            }

            return null;
        }

        public void Email_ProformaInvoice(string toEmailID, List<string> listFileNames, MailFeeDueStatementReportDTO reportDTO)
        {
            string classCode = string.Empty;

            if (string.IsNullOrEmpty(classCode))
            {
                var classID = reportDTO.ClassID.HasValue ? Convert.ToInt32(reportDTO.ClassID) : (int?)null;
                if (classID.HasValue)
                {
                    var data = new Eduegate.Domain.Setting.SettingBL(Context).GetClassDetailByClassID(classID.Value);

                    classCode = data?.Code?.ToLower();
                }
            }
            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var settingBL = new Domain.Setting.SettingBL(Context);

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            string mailClosingRegard = settingBL.GetSettingValue<string>("PROFORMA_MAIL_CLOSING_REGARDS");
            string mailClosing = settingBL.GetSettingValue<string>("PROFORMA_MAIL_REGARD_CONTENT");

            var emailTemplate = EmailTemplateMapper.Mapper(Context).GetEmailTemplateDetails(EmailTemplates.ProformaMail.ToString());

            var emailSubject = string.Empty;
            var emailBody = string.Empty;
            if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
            {
                emailSubject = emailTemplate.Subject;

                emailBody = emailTemplate.EmailTemplate;

                emailBody = emailBody.Replace("{ADMISSION_NO}", reportDTO.AdmissionNo);
                emailBody = emailBody.Replace("{STUDENT_NAME}", reportDTO.StudentName);
                emailBody = emailBody.Replace("{CLASS}", reportDTO.Class);
            }
            else
            {
                emailSubject = "Proforma Invoice";

                emailBody = @"<p align='left'>Dear parent <br />Please find the Proforma Invoice attached below</p><br />
                        <table align='left'>
                                <tr>
                                    <th>Student Details:</th>
                                </tr>
                                <tr>
                                    <td> Admission No:</td>
                                          <td>" + reportDTO.AdmissionNo + @"</td> </tr>

                                   <tr>
                        				<td>Student Name :</td>
                                       <td>" + reportDTO.StudentName + @"</td>  
                                      </tr>
                                   <tr>
                                          <td>Class :</td>
                                            <td>" + reportDTO.Class + @"</td>
                                         </tr>

                                           </table> <br /> <br /><br /><br /> <br /><br /><br /> <br />
                            <p style = 'font-size:0.7rem;' <b> Please Note: </b>do not reply to this email as it is a computer generated email</p>";
            }

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody, mailClosingRegard, mailClosing);

            DateTime currentDate = DateTime.Now;
            var fileNameFormat = reportDTO.AdmissionNo + '_' + currentDate.ToString("yyyyMMddHHmmss");

            string ccMailValue = settingBL.GetSettingValue<string>("TICKETING_CC_MAILS", null);
            var ccMailIDs = new List<string>();
            var splitMails = ccMailValue != null ? ccMailValue.Split(",") : null;

            if (splitMails != null)
            {
                foreach (var mail in splitMails)
                {
                    ccMailIDs.Add(mail.Trim());
                }
            }

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, EmailTypes.FeeDueStatement, mailParameters, listFileNames, fileNameFormat, ccMailIDs);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, EmailTypes.FeeDueStatement, mailParameters, listFileNames, fileNameFormat, ccMailIDs);
                }
            }
        }
        #endregion Proforma Invoice

        #region Salary slip mail send using local path
        public void MailSalaryslip(List<SalarySlipDTO> salarySlipList)
        {
            var listFilecontent = new List<ContentFileDTO>();

            foreach (var data in salarySlipList)
            {
                var listFileNames = new List<string>();
                listFileNames.Add(ViewSalarySlip(data.SalarySlipIID, data.ReportData, data.ReportName));
                Mail_Salaryslip(data.EmployeeWorkEmail, listFileNames, data.EmployeeCode, data.SlipDate, data.ReportName);
            }

        }

        public void Mail_Salaryslip(string toEmailID, List<string> listFileNames, string empCode, DateTime? slipDate, string reportName)
        {
            var settingBL = new Domain.Setting.SettingBL(Context);

            var emailTemplate = EmailTemplateMapper.Mapper(Context).GetEmailTemplateDetails(EmailTemplates.SalarySlipGeneration.ToString());

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");
            var emailBody = emailTemplate?.EmailTemplate;


            var emailSubject = "Salary Slip_" + (slipDate.Value).ToString("MMM") + "_" + slipDate.Value.Year + "_" + empCode;

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody);
            var mailParameters = new Dictionary<string, string>()
                {
                    { "SCHOOL_SHORT_NAME", null},
                };

            var fileNameFormat = reportName;

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, EmailTypes.SalarySlipGeneration, mailParameters, listFileNames, fileNameFormat);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, EmailTypes.SalarySlipGeneration, mailParameters, listFileNames, fileNameFormat);
                }
            }
        }

        public string ViewSalarySlip(long salarySlipIID, byte[] reportData, string reportName)
        {
            string filename = string.Format("{0}/{1}/{2}/{3}",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", reportName);
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

            //TODO: Need to check this code
            //var data = new FilePathResult(filePath, "application/pdf");

            //return data.FileName;   

            var path = Path.Combine(new Domain.Setting.SettingBL().GetSettingValue<string>("CLIENT_PARENT_PORTAL").ToString() + new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath").ToString(), "Reports", "Temp", Path.GetFileName(filename));

            var data = DownloadPdf(filePath);

            return path;
        }
        #endregion Salary slip mail send using local path

        #region Generate salary slip content files
        public List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList)
        {
            var contentFiles = new List<ContentFileDTO>();
            var reportName = "SalarySlipReport";
            //var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var reportingService = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");

            foreach (var data in salarySlipList)
            {
                byte[] pdfBytes;
                if (reportingService.ToLower() == "ssrs")
                {
                    var reportServer = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServer");
                    var reportServerDirect = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDirect");
                    var reportDomainName = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDomain");

                    var reportUserName = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDomainUser");
                    var reportPassword = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDomainPassword");

                    var reportPath = GetReportUrlAndDefaultParameters(reportName, (byte?)data.BranchID, null);

                    var fileName = string.Empty;

                    if (!string.IsNullOrEmpty(reportPath))
                    {
                        var uriBuilder = new UriBuilder(reportPath);
                        var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                        query["SalarySlipIID"] = data.SalarySlipIID.ToString();

                        uriBuilder.Query = query.ToString();

                        var reportUrl = uriBuilder.ToString();

                        fileName = reportUrl;
                    }

                    string updatedFileName = string.IsNullOrEmpty(reportServerDirect) ? fileName : fileName.Replace(reportServer, reportServerDirect);

                    using (WebClient webClient = new WebClient())
                    {
                        webClient.Credentials = new NetworkCredential(reportUserName, reportPassword, reportDomainName);
                        pdfBytes = webClient.DownloadData(updatedFileName);
                    }
                }
                else
                {
                    // Convert to JSON string
                    string parameterString = JsonConvert.SerializeObject(new
                    {
                        SalarySlipIID = data.SalarySlipIID.ToString(),
                        SchoolID = data.BranchID.HasValue ? data.BranchID.ToString() : null,
                    });
                    string format = "PDF";

                    pdfBytes = new ReportViewerBL(Context).GetReportFile(reportName, parameterString, format);
                }

                var contentFile = new ContentFileDTO()
                {
                    ContentFileName = string.Format("{0}.pdf", reportName + '_' + data.EmployeeCode + '_' + Convert.ToDateTime(data.SlipDate).Day + "-" + Convert.ToDateTime(data.SlipDate).Year),
                    ReferenceID = data.SalarySlipIID,
                    ContentData = pdfBytes
                };

                contentFiles.Add(contentFile);
            }

            return contentFiles;
        }
        #endregion Generate salary slip content files

        #region View salary slip - not used
        public ContentFileDTO ViewSalarySlip(long salarySlipIID, byte? schoolID, DateTime? slipDate, string employeeCode)
        {
            string RptPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath").ToString() + "SalarySlipReport.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}/{1}/{2}/{3}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", salarySlipIID + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("SalarySlipIID", salarySlipIID.ToString(), false));
            return SetSalarySlipParameters(rpt, filename, schoolID, salarySlipIID, slipDate, employeeCode);
        }

        public ContentFileDTO SetSalarySlipParameters(LocalReport rpt, string filename, byte? schoolID, long id, DateTime? slipDate, string employeeCode)
        {
            var contentFileDTO = new ContentFileDTO();
            var footer = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_FOOTER");

            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                //TODO: Need to check this code
                //rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));

                DataTable dataTable = GetDataTable(dataset, rpt.GetParameters()); // Replace with your method to get the DataTable
                ReportDataSource reportDataSource = new ReportDataSource();

                reportDataSource.Name = dataset.Name; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = dataTable;

                rpt.DataSources.Add(reportDataSource);
            }

            string filePath = Path.GetFullPath(filename);

            contentFileDTO = GenerateByte(rpt, filePath, filename, id, slipDate, employeeCode);

            rpt.Dispose();

            //TODO: Need to check this code
            //var data = new FilePathResult(filePath, "application/pdf");

            var data = DownloadPdf(filePath);

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
                     "SalarySlip_", slipDate.Value.ToString("MMM"), "_" + slipDate.Value.Year, "_" + employeeCode),
                ReferenceID = id,
                ContentData = bytes
            };
        }
        #endregion View salary slip - not used

        #region Student TC generation - not used
        public ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO dto)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var listFilecontent = new ContentFileDTO();

                var transferDetails = dbContext.StudentTransferRequests.Where(a => a.StudentTransferRequestIID == dto.StudentTransferRequestIID).AsNoTracking().FirstOrDefault();

                var studentDetails = dbContext.Students.Where(a => a.StudentIID == transferDetails.StudentID).AsNoTracking().FirstOrDefault();

                listFilecontent = ViewTransferCertificates(dto, studentDetails.SchoolID, studentDetails.AdmissionNumber);

                return listFilecontent;
            }
        }

        public ContentFileDTO ViewTransferCertificates(StudentTransferRequestDTO dto, byte? schoolID, string admissionNumber)
        {
            string RptPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath").ToString() + "StudentTCDiscReport.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}/{1}/{2}/{3}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", dto.StudentTransferRequestIID + '_' + currentDate.ToString("yyyyMMddHHmmss"));

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
            var footer = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_FOOTER");

            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                //TODO: Need to check this code
                //rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));

                DataTable dataTable = GetDataTable(dataset, rpt.GetParameters()); // Replace with your method to get the DataTable
                ReportDataSource reportDataSource = new ReportDataSource();

                reportDataSource.Name = dataset.Name; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = dataTable;

                rpt.DataSources.Add(reportDataSource);
            }

            string filePath = Path.GetFullPath(filename);

            contentFileDTO = GenerateTCByte(rpt, filePath, filename, id, currentDate, admissionNumber);

            rpt.Dispose();

            //TODO: Need to check this code
            //var data = new FilePathResult(filePath, "application/pdf");

            var data = DownloadPdf(filePath);

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
                     "StudentTC_", admissionNumber, "_" + currentDate.Value.ToShortDateString()),
                ReferenceID = id,
                ContentData = bytes
            };
        }
        #endregion Student TC generation

        #region Progress report generation
        public ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName)
        {
            //var contentFile = ViewProgressReport(progressReport, reportName);

            byte[] pdfBytes = [];
            var contentFile = new ContentFileDTO();

            var reportingService = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");
            try
            {
                if (reportingService.ToLower() == "ssrs")
                {
                    var reportServer = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServer");
                    var reportServerDirect = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDirect");
                    var reportDomainName = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDomain");

                    var reportUserName = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDomainUser");
                    var reportPassword = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("ReportServerDomainPassword");

                    var reportPath = GetReportUrlAndDefaultParameters(reportName, progressReport.SchoolID, progressReport.AcademicYearID);

                    var fileName = string.Empty;

                    if (!string.IsNullOrEmpty(reportPath))
                    {
                        var uriBuilder = new UriBuilder(reportPath);
                        var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                        query["SchoolID"] = progressReport.SchoolID.ToString();
                        query["ACDEMICYEARID"] = progressReport.AcademicYearID.ToString();
                        query["CLASSIDs"] = progressReport.ClassID.ToString();
                        query["SECTIONIDs"] = progressReport.SectionID.ToString();
                        query["StudSearch"] = string.Empty;
                        query["All_StudentIIDs"] = progressReport.StudentID.ToString();
                        query["ExamGroupID"] = progressReport.ExamGroupID.ToString();
                        query["CLASSFILTER"] = null;
                        query["IsCumulative9"] = null;
                        query["AdditionalSubjID"] = null;
                        query["IsClass5AndBelow"] = null;

                        uriBuilder.Query = query.ToString();

                        var reportUrl = uriBuilder.ToString();

                        fileName = reportUrl;
                    }

                    //for production
                    string updatedFileName = string.IsNullOrEmpty(reportServerDirect) ? fileName : fileName.Replace(reportServer, reportServerDirect);

                    //for development
                    //string updatedFileName = string.IsNullOrEmpty(reportServer) ? fileName : fileName.Replace(reportServer, reportServer);

                    using (WebClient webClient = new WebClient())
                    {
                        webClient.Credentials = new NetworkCredential(reportUserName, reportPassword, reportDomainName);
                        pdfBytes = webClient.DownloadData(updatedFileName);
                    }
                }
                else
                {
                    string parameterString = JsonConvert.SerializeObject(new
                    {
                        SchoolID = progressReport.SchoolID.ToString(),
                        ACDEMICYEARID = progressReport.AcademicYearID.ToString(),
                        CLASSIDs = progressReport.ClassID.ToString(),
                        SECTIONIDs = progressReport.SectionID.ToString(),
                        StudSearch = string.Empty,
                        All_StudentIIDs = progressReport.StudentID.ToString(),
                        ExamGroupID = progressReport.ExamGroupID != null ? progressReport.ExamGroupID.ToString() : "0",
                        CLASSFILTER = string.Empty,
                        IsCumulative9 = reportName.Contains("8") || reportName.Contains("9") || reportName.Contains("11") ? 1 : 0,
                });
                    string format = "PDF";

                    pdfBytes = new ReportViewerBL(Context).GetReportFile(reportName, parameterString, format);
                }

                contentFile = new ContentFileDTO()
                {
                    ContentFileName = string.Format("{0}.pdf", reportName + '_' + progressReport.StudentID),
                    ReferenceID = progressReport.StudentID,
                    ContentData = pdfBytes
                };
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Progress report generation failed. Error message: {errorMessage} reportName: {reportName} studentID: {progressReport.StudentID}", ex);
            }

            return contentFile;
        }

        public ContentFileDTO ViewProgressReport(ProgressReportDTO progressReportDTO, string reportName)
        {
            string RptPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath").ToString() + reportName;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;

            string filename = progressReportDTO.Student.Value + "_" + progressReportDTO.Class.Value + "_" + "Progress_Report";

            string filePath = string.Format("{0}/{1}/{2}/{3}/{4}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", "Progress Report", filename);

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

            var footer = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_FOOTER");
            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (dto.SchoolID == 10)
            {
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                //TODO: Need to check this code
                //rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));

                DataTable dataTable = GetDataTable(dataset, rpt.GetParameters()); // Replace with your method to get the DataTable
                ReportDataSource reportDataSource = new ReportDataSource();

                reportDataSource.Name = dataset.Name; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = dataTable;

                rpt.DataSources.Add(reportDataSource);
            }

            string fileFullPath = Path.GetFullPath(filePath);

            contentFileDTO = GenerateProgressReportByte(rpt, fileFullPath, filename, dto.StudentID);

            rpt.Dispose();

            //TODO: Need to check this code
            //var data = new FilePathResult(filePath, "application/pdf");

            var data = DownloadPdf(filePath);

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
        #endregion Progress report generation

        #region Generate Sales Invoice & send mail
        public void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo)
        {
            var defaultMail = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("DEFAULT_MAIL_ID");

            long headIID = long.Parse(headID);

            var transHead = TransactionHeadMapper.Mapper(Context).GetTransactionHeadDetailsByID(headIID);

            var studentDetails = StudentMapper.Mapper(Context).GetStudentDetailsByStudentID(transHead.StudentID.Value);

            var emailID = studentDetails.ParentID.HasValue ? !string.IsNullOrEmpty(studentDetails.ParentEmailID) ? studentDetails.ParentEmailID : defaultMail : defaultMail;

            var listFileNames = new List<string>();

            var filename = ViewSalesInvoiceReport(headID, transactionNo);

            listFileNames.Add(filename);

            Email_SalesInvoiceReport(emailID, listFileNames, transactionNo);
        }

        public string ViewSalesInvoiceReport(string headID, string transactionNo)
        {
            string RptPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath").ToString() + "SalesInvoiceReport.rdl";

            var currentDate = DateTime.Now;

            var settingBL = new Domain.Setting.SettingBL(Context);

            var schoolIID = settingBL.GetSettingValue<string>("ONLINEBRANCHID");

            var schoolID = byte.Parse(schoolIID);

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}/{1}/{2}/{3}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", transactionNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", headID, false));

            return ViewReport(rpt, filename, schoolID);
        }

        public void Email_SalesInvoiceReport(string toEmailID, List<string> listFileNames, string transactionNo)
        {
            var settingBL = new Domain.Setting.SettingBL(Context);

            string receiptBody = settingBL.GetSettingValue<string>("SALESINVOICE_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailBody = receiptBody;

            var emailSubject = "Sales Invoice";

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody);
            var mailParameters = new Dictionary<string, string>();

            DateTime currentDate = DateTime.Now;
            var fileNameFormat = transactionNo + '_' + currentDate.ToString("yyyyMMddHHmmss");

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, EmailTypes.SalesInvoiceGeneration, mailParameters, listFileNames, fileNameFormat);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, EmailTypes.SalesInvoiceGeneration, mailParameters, listFileNames, fileNameFormat);
                }
            }

        }
        #endregion Generate Sales Invoice & send mail

        #region Generate Sales order & send mail
        public void GenerateSalesOrderwithFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList)
        {
            var listFileNames = new List<string>();

            foreach (var collection in feeCollectionList)
            {
                var filename = ViewSOFeeReport(collection.FeeCollectionIID.ToString(), collection.AdmissionNo, collection.FeeReceiptNo, collection.EmailID, collection.SchoolID, collection.TransactionHeadID);

                listFileNames.Add(filename);
            }

            Email_FeeReceipt(feeCollectionList[0], listFileNames, EmailTypes.AutoFeeReceipt);
        }

        public string ViewSOFeeReport(string collectionID, string admissionNo, string feeReceiptNo, string emailID, byte? schoolID, string transactionheadID)
        {
            string RptPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath").ToString() + "SalesOrderwithFeeReceiptReport.rdl";

            var currentDate = DateTime.Now;

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}/{1}/{2}/{3}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", admissionNo + '_' + feeReceiptNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", transactionheadID, false));
            rpt.SetParameters(new ReportParameter("HeadIID", transactionheadID, false));
            rpt.SetParameters(new ReportParameter("FeeCollectionID", collectionID, false));
            //rpt.SetParameters(new ReportParameter("EmailID", emailID, false));

            return ViewReport(rpt, filename, schoolID);
        }
        #endregion Generate Sales order & send mail

        #region Generate and send sales order report
        public void GenerateSalesOrderAndSendToMail(string headID, string transactionNo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                long headIID = long.Parse(headID);

                var transHead = dbContext.TransactionHeads.Where(t => t.HeadIID == headIID).AsNoTracking().FirstOrDefault();


                var settingBL = new Domain.Setting.SettingBL(Context);
                var defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

                var studentDetails = dbContext.Students.Where(s => s.StudentIID == transHead.StudentID).Include(i => i.Parent).AsNoTracking().FirstOrDefault();

                var emailID = studentDetails.ParentID.HasValue ? studentDetails.Parent != null ? studentDetails.Parent.GaurdianEmail : defaultMail : defaultMail;

                var listFileNames = new List<string>();


                var filename = ViewSalesOrderReport(headID, transactionNo);

                listFileNames.Add(filename);

                Email_SalesOrderReport(emailID, listFileNames, transactionNo);
            }
        }

        public string ViewSalesOrderReport(string headID, string transactionNo)
        {
            string RptPath = new Domain.Setting.SettingBL().GetSettingValue<string>("ReportPhysicalPath").ToString() + "SalesOrderReport.rdl";

            var currentDate = DateTime.Now;

            var settingBL = new Domain.Setting.SettingBL(Context);

            var schoolIID = settingBL.GetSettingValue<string>("ONLINEBRANCHID");

            var schoolID = byte.Parse(schoolIID);

            LocalReport rpt = new LocalReport();
            ReportDetails = Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}/{1}/{2}/{3}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", transactionNo + '_' + currentDate.ToString("yyyyMMddHHmmss"));
            rpt.SetParameters(new ReportParameter("HeadID", headID, false));

            return ViewReport(rpt, filename, schoolID);
        }

        public void Email_SalesOrderReport(string toEmailID, List<string> listFileNames, string transactionNo)
        {
            var settingBL = new Domain.Setting.SettingBL(Context);

            string receiptBody = settingBL.GetSettingValue<string>("SALESORDER_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailBody = receiptBody;

            var emailSubject = "Sales Order";

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody);
            var mailParameters = new Dictionary<string, string>();

            DateTime currentDate = DateTime.Now;
            var fileNameFormat = transactionNo + '_' + currentDate.ToString("yyyyMMddHHmmss");

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, EmailTypes.SalesOrderConfirmation, mailParameters, listFileNames, fileNameFormat);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, EmailTypes.SalesOrderConfirmation, mailParameters, listFileNames, fileNameFormat);
                }
            }
        }
        #endregion Generate and send sales order report

        #region Set Parameters and URL (SSRS Report Viewer)
        public string GetReportUrlAndDefaultParameters(string reportName, byte? schoolID, int? currenAacademicYearID = null)
        {
            try
            {
                string successData = string.Empty;
                string reportUrl = string.Empty;

                schoolID = Context != null && Context.SchoolID.HasValue ? (byte)Context.SchoolID : schoolID;
                var academicYearID = Context != null && Context.AcademicYearID.HasValue ? Context.AcademicYearID : currenAacademicYearID;

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

                return successData;
            }
            catch (Exception ex)
            {
                // Handle HTTP request exception
                return ex.Message;
            }
        }
        #endregion Set Parameters and URL

        #region report generation
        public string ViewReport(LocalReport rpt, string filename, byte? schoolID)
        {
            var footer = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_FOOTER");

            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                var logo = new Domain.Setting.SettingBL().GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
                if (logo != null)
                {
                    if (rpt.GetParameters().Where(a => a.Name == "Logo").FirstOrDefault() != null)
                    {
                        rpt.SetParameters(new ReportParameter("Logo", logo.SettingValue, false));
                    }
                }
            }

            string RootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl");

            var rootUrl = ReportDetails.ReportParameters.Where(a => a.Name == "RootUrl").FirstOrDefault();

            if (rootUrl != null)
            {
                rpt.SetParameters(new ReportParameter("RootUrl", RootUrl, false));
            }

            //REPORT_HEADER_BGCOLOR
            var headerBGColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_BGCOLOR");

            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }

            var headerForeColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_HEADER_FORECOLOR");

            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }

            var titleColor = new Domain.Setting.SettingBL().GetSettingDetail("REPORT_TITLE_FORECOLOR");

            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }

            foreach (var dataset in ReportDetails.DataSets)
            {
                //TODO: Need to check this code
                //rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));

                DataTable dataTable = GetDataTable(dataset, rpt.GetParameters()); // Replace with your method to get the DataTable
                ReportDataSource reportDataSource = new ReportDataSource();

                reportDataSource.Name = dataset.Name; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = dataTable;

                rpt.DataSources.Add(reportDataSource);
            }

            string filePath = Path.GetFullPath(filename);

            GeneratePDF(rpt, filePath);

            rpt.Dispose();

            //TODO: Need to check this code
            //var data = new FilePathResult(filePath, "application/pdf");

            //return data.FileName;

            var data = DownloadPdf(filePath);

            return data.FileDownloadName;
        }

        public DataTable GetDataTable(Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {
            DataTable re = new DataTable();
            if (dataset.Query.CommandType == CommandType.StoredProcedure)
                return GetReportData(dataset, parameters);

            using (SqlDataAdapter da = new SqlDataAdapter(dataset.Query.CommandText, Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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

        public DataTable GetReportData(Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {
            using (DataTable _sDt = new DataTable(dataset.Name))
            {
                using (SqlConnection _sConn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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
        #endregion report generation

        #region PDF generation & download using Local path
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

        public FileContentResult DownloadPdf(string filePath)
        {
            // Read the PDF file as bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Return the file as a response
            return new FileContentResult(fileBytes, "application/pdf")
            {
                FileDownloadName = Path.GetFileName(filePath)
            };
        }
        #endregion PDF generation & download using Local path

        //public string GetStudentDetails(string headID, string transactionNo)
        //{
        //    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
        //    {
        //        long headIID = long.Parse(headID);

        //        var transHead = dbContext.TransactionHeads.Where(t => t.HeadIID == headIID).AsNoTracking().FirstOrDefault();


        //        var settingBL = new Domain.Setting.SettingBL(Context);
        //        var defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

        //        var studentDetails = dbContext.Students.Where(s => s.StudentIID == transHead.StudentID).AsNoTracking().FirstOrDefault();

        //        var emailID = studentDetails.ParentID.HasValue ? studentDetails.Parent != null ? studentDetails.Parent.GaurdianEmail : defaultMail : defaultMail;

        //        var listFileNames = new List<string>();

        //        return studentDetails.AdmissionNumber + "#" + emailID + "#" + studentDetails.SchoolID;
        //    }
        //}

        #region Send Job Offer Letter Mail
        public OperationResultDTO MailJobOfferLetter(EmployeeSalaryStructureDTO dto, EmailTypes mailType)
        {
            var message = new OperationResultDTO()
            {
                operationResult = OperationResult.Success,
                Message = "Success!"
            };

            try
            {
                var reportName = "JobOfferLetter";
                var listFileNames = new List<string>();
                var filename = string.Empty;

                var reportingService = new Domain.Setting.SettingBL(Context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");

                if (dto.JobInterviewMapID.HasValue)
                {
                    if (reportingService.ToLower() == "ssrs")
                    {
                        var reportPath = GetReportUrlAndDefaultParameters(reportName, (byte?)dto.SchoolID);

                        if (!string.IsNullOrEmpty(reportPath))
                        {
                            var uriBuilder = new UriBuilder(reportPath);
                            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                            query["JobInterviewMapID"] = dto.JobInterviewMapID.ToString();

                            uriBuilder.Query = query.ToString();

                            var reportUrl = uriBuilder.ToString();

                            filename = reportUrl;
                        }
                    }
                    else
                    {
                        // Convert to JSON string
                        string parameterString = JsonConvert.SerializeObject(new
                        {
                            JobInterviewMapID = dto.JobInterviewMapID.ToString(),
                        });

                        filename = "reportName=" + reportName + "&parameter=" + parameterString;
                    }
                    listFileNames.Add(filename);

                    SendMailJobOfferLetter(dto.CandidateMailID, listFileNames, dto);

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Acknowledgment: Your email has been sent successfully!"
                    };

                    if (message.operationResult == OperationResult.Success)
                    {
                        using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                        {
                            var mapData = dbContext.JobInterviewMaps.FirstOrDefault(x => x.MapID == dto.JobInterviewMapID);
                            if(mapData != null)
                            {
                                mapData.IsOfferLetterSent = true;
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };

                var errorMessage = ex.Message;
                Eduegate.Logger.LogHelper<ReportGenerationBL>.Fatal(ex.Message, ex);
            }

            return message;
        }

        public void SendMailJobOfferLetter(string toEmailID, List<string> listFileNames, EmployeeSalaryStructureDTO dto)
        {
            string classCode = string.Empty;

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var settingBL = new Domain.Setting.SettingBL(Context);

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailSubject = "Job Offer Letter";
            var emailBody = string.Empty;

            emailBody = @"Dear {ApplicantName},<p>Please find the attached offer letter for the {JobTitle} at {CompanyName}.</p>";
            emailBody = emailBody.Replace("{ApplicantName}", dto.ApplicantName);
            emailBody = emailBody.Replace("{JobTitle}", dto.Designation);
            emailBody = emailBody.Replace("{CompanyName}", dto.CompanyName);

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(Context).PopulateBody(toEmailID, emailBody);

            DateTime currentDate = DateTime.Now;
            var fileNameFormat = "JobOfferLetter_" + dto.ApplicantName;

            string ccMailValue = settingBL.GetSettingValue<string>("TICKETING_CC_MAILS", null);
            var ccMailIDs = new List<string>();
            var splitMails = ccMailValue != null ? ccMailValue.Split(",") : null;

            if (splitMails != null)
            {
                foreach (var mail in splitMails)
                {
                    ccMailIDs.Add(mail.Trim());
                }
            }

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(toEmailID, emailSubject, mailMessage, EmailTypes.RecruitmentPortal, mailParameters, listFileNames, fileNameFormat, ccMailIDs);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(Context).SendMailWithAttachment(defaultMail, emailSubject, mailMessage, EmailTypes.RecruitmentPortal, mailParameters, listFileNames, fileNameFormat, ccMailIDs);
                }
            }
        }
        #endregion 
    }
}