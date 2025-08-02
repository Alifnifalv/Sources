using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Payment;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Data.OleDb;
using Eduegate.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Fees;
using System.Collections.Generic;
using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Services.Contracts.Payments;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class BillPaymentController : Controller
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        public BillPaymentController(IBackgroundJobClient backgroundJobs)
        {
            _backgroundJobs = backgroundJobs;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetBillInformation(string agentId, string token, string timeStamp, string StudentRollNumber, string ChildQID)
        {
            try
            {
                ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                {
                    CardType = "CBQ",
                    RequestType = "Initiating GetBillInformation",
                    RequestLog = "agentId:'" + agentId + "' token:'" + token + "' timeStamp '" + timeStamp + "'  StudentRollNumber:'" + StudentRollNumber + "' ChildQID :'" + ChildQID + "'"
                });
                var result = new BillPaymentService().GetBillingInformation(agentId, token, timeStamp, StudentRollNumber, ChildQID);

                if (result != null)
                {
                    var classSection = result.ClassSection.Split('#');
                    FeePaymentResponse _response = new FeePaymentResponse()
                    {
                        ActCode = result.ActCode,
                        ActDescription = result.ActDescription,
                        termDetails = result.Term,
                        outstandingAmount = result.OutstandingAmount,
                        studentName = result.StudentName,
                        description = result.Description,
                        currency = "QAR",
                        section = classSection[1],
                        resposeCode = result.ResposeCode,
                        classname = classSection[0],
                        remarks = "Total due amount",
                        Company = result.Company,
                        MerchantID = result.MerchantID,
                    };
                    ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                    {
                        CardType = "CBQ",
                        RequestType = "Initiating GetBillInformation",
                        RequestLog = "agentId:'" + agentId + "' token:'" + token + "' timeStamp '" + timeStamp + "'  StudentRollNumber:'" + StudentRollNumber + "' ChildQID :'" + ChildQID + "'",
                        ResponseStatus = "SUCCESS",
                        ResponseLog = "ActCode:'" + result.ActCode + "' ActDescription:'" + result.ActDescription + "' termDetails '" + result.Term + "' " +
                        " outstandingAmount:'" + result.OutstandingAmount + "' studentName :'" + result.StudentName + "'" +
                        " description:'" + result.Description + "' section:'" + classSection[1] + "' resposeCode '" + result.ResposeCode + "' " +
                        " classname:'" + classSection[0] + "' Company :'" + result.Company + "' MerchantID:'" + result.MerchantID + "' ",
                    });
                    return Json(_response);
                }
                else
                {
                    FeePaymentResponse _response = new FeePaymentResponse()
                    {
                        ActCode = "3",
                        ActDescription = "Fee Not Generated",
                        termDetails = "null",
                        outstandingAmount = 0,
                        studentName = "null",
                        description = "Fee Not Generated",
                        currency = "QAR",
                        section = "null",
                        resposeCode = 3,
                        classname = "null",
                        remarks = "Student not found",
                        Company = "null",
                        MerchantID = "null",
                    };
                    return Json(_response);
                }

            }
            catch (Exception ex)
            {
                FeePaymentResponse _response = new FeePaymentResponse()
                {
                    ActCode = "3",
                    ActDescription = "Fee Not Generated",
                    termDetails = "null",
                    outstandingAmount = 0,
                    studentName = "null",
                    description = "Fee Not Generated",
                    currency = "QAR",
                    section = "null",
                    resposeCode = 3,
                    classname = "null",
                    remarks = "Student not found"
                };
                ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                {
                    CardType = "CBQ",
                    RequestType = "GetBillInformation",
                    RequestLog = "agentId:'" + agentId + "' StudentRollNumber:'" + StudentRollNumber + "'",
                    ResponseStatus = "FAILURE",
                    ResponseLog = ex.Message
                });
                Eduegate.Logger.LogHelper<BillPaymentController>.Fatal(ex.Message, ex);
                return Json(_response);
            }
        }


        private async Task<string> GetRequestBodyAsString(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            request.EnableBuffering(); // Allow the request body to be read multiple times
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                return await reader.ReadToEndAsync();
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> MakePayment([FromBody] FeePaymentRequest _sRequest)
        {

            string agentId = _sRequest.agentId;
            string token = _sRequest.token;
            string timeStamp = _sRequest.timeStamp;
            string StudentRollNumber = _sRequest.StudentRollNumber;
            string ChildQID = _sRequest.ChildQID;
            decimal amount = _sRequest.amount; string currency = _sRequest.currency;
            string remarks = _sRequest.remarks;
            string transactionId = _sRequest.transactionId;
            string requestBody = null, requestBody1 = null;
            try
            {
                //requestBody= await GetRequestBodyAsString(Request);


                //Enable request buffering
                //  HttpContext.Request.EnableBuffering();

                //Read the request body as string
                //using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                //{
                //    requestBody1 = await reader.ReadToEndAsync();

                //}

                ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                {
                    CardType = "CBQ",
                    RequestType = "Initiating MakePayment",
                    //RequestLog = requestBody
                    //RequestLog = "requestBody'"+ requestBody+ "' requestBody1 '"+ requestBody1 + "' agentId:'" + agentId + "' token:'" + token + "' timeStamp '" + timeStamp + "'  StudentRollNumber:'" + StudentRollNumber + "' ChildQID :'" + ChildQID + "' amount :" + amount + "   transactionId:'" + transactionId + "'"
                    RequestLog = " agentId:'" + agentId + "' token:'" + token + "' timeStamp '" + timeStamp + "'  StudentRollNumber:'" + StudentRollNumber + "' ChildQID :'" + ChildQID + "' amount :" + amount + "   transactionId:'" + transactionId + "'"
                });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BillPaymentController>.Fatal(ex.Message, ex);
            }

            try
            {

                var result = new BillPaymentService().MakePayment(new BankBillPaymentDTO()
                {
                    AgentId = agentId,
                    Amount = amount,
                    ChildQID = ChildQID,
                    Currency = currency,
                    Remarks = remarks,
                    StudentRollNumber = StudentRollNumber,
                    Token = token,
                    TransactionId = transactionId
                });


                if (result == null || result.remarks == "Failure" || result.remarks == "No pending fees" || result.description == "Transaction not Completed")
                {
                    var result1 = new PaymentMainResponseDTO()
                    {
                        description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = "12345",
                        remarks = result == null ? "Transaction not Completed" : result.description,
                        resposeCode = 1
                    };
                    return Json(result1);
                }
                else
                {
                    #region Sending Reports

                    var feeCollectionData = new FeeCollectionDTO
                    {
                        FeeCollectionIID = result.id ?? 0,
                        AdmissionNo = result.admissionNumber,
                        FeeReceiptNo = result.receiptNo,
                        EmailID = result.mailID,//"vineethakr@outlook.com",
                        SchoolID = result.schoolID,
                        AcadamicYearID = result.AcadamicYearID
                    };
                    var collectionDataList = new List<FeeCollectionDTO>();
                    collectionDataList.Add(feeCollectionData);
                    ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                    {
                        CardType = "CBQ",
                        RequestType = "MakePayment",
                        RequestLog = "agentId:'" + agentId + "' token:'" + token + "' timeStamp '" + timeStamp + "'  StudentRollNumber:'" + StudentRollNumber + "' ChildQID :'" + ChildQID + "' amount :" + amount + "   transactionId:'" + transactionId + "'",
                        ResponseLog = " result.description: '" + result.description + "ReceiptNo:'" + result.receiptNo + "' FeeCollectionIID:'" + result.id + "' EmailID:'" + result.mailID + "' AcadamicYearID'" + result.AcadamicYearID + "' ",

                        ResponseStatus = "SUCCESS"
                    }); ;
                    if (result.id.HasValue && !string.IsNullOrEmpty(result.mailID))
                    {
                        _backgroundJobs.Enqueue(() => ClientFactory.ReportGenerationServiceClient(null).GenerateFeeReceiptAndSendToMail(collectionDataList, EmailTypes.AutoFeeReceipt));
                    }
                    ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                    {
                        CardType = "CBQ",
                        RequestType = "MakePayment",
                        RequestLog = "agentId:'" + agentId + "' token:'" + token + "' timeStamp '" + timeStamp + "'  StudentRollNumber:'" + StudentRollNumber + "' ChildQID :'" + ChildQID + "' amount :" + amount + "   transactionId:'" + transactionId + "'",
                        ResponseLog = "AutoFeeReceipt Generated ",
                        ResponseStatus = "SUCCESS"
                    });
                    #endregion
                }
                var returnSuccess = new PaymentMainResponseDTO()
                {
                    description = result.description,
                    referenceNumber = result.referenceNumber,
                    remarks = result.remarks,
                    resposeCode = result.resposeCode
                };
                return Json(returnSuccess);
            }
            catch (Exception ex)
            {
                var result1 = new PaymentMainResponseDTO()
                {
                    description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                    referenceNumber = "12345",
                    remarks = "Transaction not Completed",
                    resposeCode = 1
                };
                ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
                {
                    CardType = "CBQ",
                    RequestType = "MakePayment",
                    ResponseLog = ex.Message,
                    ResponseStatus = "FAILED"
                });
                Eduegate.Logger.LogHelper<BillPaymentController>.Fatal(ex.Message, ex);
                return Json(result1);
            }

        }
        private async Task<string> GetRequestBodyAsStringAsync(Stream body)
        {
            using (StreamReader reader = new StreamReader(body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }


        //[HttpPost]
        //public ActionResult MakePayment()
        //{

        //    try
        //    {
        //        var result = new BillPaymentService().MakePayment(new BankBillPaymentDTO()
        //        {
        //            AgentId = "5803",//agentId,
        //            //Amount = amount,
        //            //ChildQID = ChildQID,
        //            //Currency = currency,
        //            //Remarks = remarks,
        //            StudentRollNumber = "P4218",//StudentRollNumber,
        //            Token = "Sr@C2!ebiwV*9",
        //            //TransactionId = transactionId
        //        });

        //        #region Sending Reports
        //        var mailID = "softoptestmail@gmail.com";
        //        var id = result.id;// "52679";
        //        var schoolID = result.schoolID; //(byte?)30;
        //        var admissionNo = result.admissionNumber;
        //        var feereceiptNo = result.receiptNo;
        //        var filename = ViewReport(id.ToString(), mailID, schoolID,admissionNo,feereceiptNo);
        //        Email_FeeReceipt(mailID, filename);
        //        #endregion
        //        var returnSuccess = new PaymentMainResponseDTO()
        //        {
        //            //description = result.description,
        //            //referenceNumber = result.referenceNumber,
        //            //remarks = result.remarks,
        //            //resposeCode = result.resposeCode
        //        };
        //        return Json(returnSuccess);
        //    }
        //    catch (Exception ex)
        //    {
        //        var result1 = new PaymentMainResponseDTO()
        //        {
        //            description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
        //            referenceNumber = "12345",
        //            remarks = ex.Message,
        //            resposeCode = 1
        //        };
        //        return Json(result1);
        //    }
        //}

        #region Reports

        public Eduegate.Utilities.SSRSHelper.Report ReportDetails;
        public string ViewReport(string headID, string emailID, byte? schoolID, string admissionNo, string feeReceiptNo)
        {
            //TODO: Need to check later
            //string RptPath = Server.MapPath("~/Reports/RDL/FeeReceipt.rdl");
            string RptPath = @"~/Reports/RDL/FeeReceipt.rdl";

            var rpt = new LocalReport();
            ReportDetails = Eduegate.Utilities.SSRSHelper.Report.GetReportFromFile(RptPath);
            rpt.ReportPath = RptPath;
            rpt.EnableHyperlinks = true;
            rpt.EnableExternalImages = true;
            string filename = string.Format("{0}\\{1}\\{2}\\{3}.pdf",
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Reports", "Temp", admissionNo + '_' + feeReceiptNo);
            rpt.SetParameters(new ReportParameter("HeadID", headID, false));
            rpt.SetParameters(new ReportParameter("FeeCollectionIID", headID, false));
            rpt.SetParameters(new ReportParameter("EmailID", emailID, false));
            var footer = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_FOOTER");
            if (footer != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "ReportFooter").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("ReportFooter", footer.SettingValue, false));
                }
            }

            if (schoolID == 10)
            {
                var logo = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("COMPANY_REPORT_LOGO_PEARL");
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
                var logo = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("COMPANY_REPORT_LOGO_PODAR");
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
            var headerBGColor = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_HEADER_BGCOLOR");
            if (headerBGColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderBGColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderBGColor", headerBGColor.SettingValue, false));
                }
            }
            var headerForeColor = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_HEADER_FORECOLOR");
            if (headerForeColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "HeaderForeColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("HeaderForeColor", headerForeColor.SettingValue, false));
                }
            }
            var titleColor = Eduegate.Services.Client.Factory.ClientFactory.SettingServiceClient(null).GetSettingDetail("REPORT_TITLE_FORECOLOR");
            if (titleColor != null)
            {
                if (rpt.GetParameters().Where(a => a.Name == "TitleColor").FirstOrDefault() != null)
                {
                    rpt.SetParameters(new ReportParameter("TitleColor", titleColor.SettingValue, false));
                }
            }
            foreach (var dataset in ReportDetails.DataSets)
            {
                //TODO: Need to check later
                //rpt.DataSources.Add(new ReportDataSource(dataset.Name, GetDataTable(dataset, rpt.GetParameters())));
                DataTable dataTable = GetDataTable(dataset, rpt.GetParameters()); // Replace with your method to get the DataTable
                ReportDataSource reportDataSource = new ReportDataSource();

                reportDataSource.Name = dataset.Name; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = dataTable;

                rpt.DataSources.Add(reportDataSource);
            }

            string filePath = System.IO.Path.GetFullPath(filename);

            GeneratePDF(rpt, filePath);

            rpt.Dispose();
            var data = File(filePath, "application/pdf");
            return data.FileName;
        }

        public System.Data.DataTable GetDataTable(Eduegate.Utilities.SSRSHelper.DataSet dataset, ReportParameterInfoCollection parameters)
        {
            System.Data.DataTable re = new System.Data.DataTable();

            using (SqlDataAdapter da =
               new SqlDataAdapter(dataset.Query.CommandText, Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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

        public string GeneratePDF(LocalReport rpt, string filePath)
        {
            string ack = "";
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                //var ReportViewer = new Microsoft.Reporting.WebForms.ReportViewer();

                //rpt.EnableExternalImages = true;

                //if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                //{
                //    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                //}

                //var dataSourceList = rpt.GetDataSourceNames();
                //foreach (var source in dataSourceList)
                //{
                //    ReportDataSource rds = new ReportDataSource();
                //    rds.Name = source;
                //    rds.Value = source;

                //    rpt.DataSources.Add(rds);
                //}

                //var parameters = rpt.GetParameters();

                ////foreach (var Key in parameters)
                //rpt.SetParameters(new ReportParameter("HeadID", "52679", false));
                //rpt.SetParameters(new ReportParameter("EmailID", "softoptestmail@gmail.com", false));

                //byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                //using (FileStream stream = System.IO.File.OpenWrite(filePath))
                //{
                //    stream.Write(bytes, 0, bytes.Length);
                //}
                return ack;
            }
            catch (Exception ex)
            {
                ack = ex.InnerException.Message;
                return ack;
            }
        }

        public string Email_FeeReceipt(string emailID, string fileName)
        {
            byte[] attachments = null;

            if (System.IO.File.Exists(fileName))
            {
                attachments = System.IO.File.ReadAllBytes(fileName);
            }

            String emailBody = "";
            String emailSubject = "";
            var result = string.Empty;

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
                var hostUser = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailUser").ToString();

                String replaymessage = PopulateBody(emailID, emailBody);

                MutualRepository mutualRepository = new MutualRepository();
                var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        result = SendReportMail(emailID, emailSubject, replaymessage, hostUser, fileName);
                    }
                    else
                    {
                        result = SendReportMail(defaultMail, emailSubject, replaymessage, hostUser, fileName);
                    }
                }

            }
            catch { }

            if (result == "true")
            {
                return "Mail Sent Sucessfully";
            }
            else
            {
                return null;
            }

        }

        private String PopulateBody(String Name, String htmlMessage)
        {

            #region SettingData binding

            var settingBL = new Domain.Setting.SettingBL();

            var settingDatas = settingBL.GetAllSettings();

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

        public string SendReportMail(string email, string subject, string msg, string mailname, string fileName)
        {
            string email_id = email;
            string mail_body = msg;
            try
            {
                var hostEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPUserName").ToString();
                var hostPassword = new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPPassword").ToString();
                var fromEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom").ToString();

                SmtpClient ss = new SmtpClient();
                ss.Host = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = new Domain.Setting.SettingBL().GetSettingValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(hostEmail, email, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailname);

                mailMsg.Attachments.Add(new Attachment(fileName));

                mailMsg.To.Add(email);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {
                return "false";
                //lb_error.Visible = true;
                // return Json(ex.Message);
            }
            //return Json("ok");
            return "true";
        }

        #endregion


    }

    public class FeePaymentResponse
    {
        public string termDetails { get; set; }
        public decimal outstandingAmount { get; set; }
        public string studentName { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public string section { get; set; }
        public int resposeCode { get; set; }
        public string classname { get; set; }
        public string remarks { get; set; }
        public string ActCode { get; set; }
        public string ActDescription { get; set; }
        public string Company { get; set; }
        public string MerchantID { get; set; }


    }
    public class FeePaymentRequest
    {
        public string agentId { get; set; }
        public string token { get; set; }
        public string timeStamp { get; set; }
        public string StudentRollNumber { get; set; }
        public string ChildQID { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string remarks { get; set; }
        public string transactionId { get; set; }
    }
}