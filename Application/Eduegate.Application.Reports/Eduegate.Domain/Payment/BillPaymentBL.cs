using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Domain.Repository.Payment;
using Eduegate.Domain.Repository.School;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Eduegate.Domain.Payment
{
    public class BillPaymentBL
    {
        CallContext _context;

        public BillPaymentBL(CallContext context)
        {
            _context = context;
        }

        public BillingResponseDTO GetBillingInformation(string agentID, string token, string timeStamp,
            string StudentRollNumber, string ChildQID)
        {
            var tokenSetting = new SettingBL(null).GetSettingValue<string>("BillPaymentAPIToken");

            if (token != tokenSetting)
            {
                throw new Exception("Invalid token");
            }

            var agentSetting = new SettingBL(null).GetSettingValue<string>("BillPaymentAPIAgent");

            if (agentID != agentSetting)
            {
                throw new Exception("Invalid agent");
            }

            var student = new SchoolRepository().GetStudentDetail(ChildQID, StudentRollNumber);

            if (student == null)
            {
                throw new Exception("Invalid student");
            }
            try
            {
                var feeDue = FeeDueGenerationMapper.Mapper(_context)
                    .FillPendingFeesForBank(student.ClassID.Value, student.StudentIID);

                //var term = new StringBuilder();
                //feeDue.ForEach(x => x.FeePeriod.ForEach(y => { term.Append(y.Key); term.Append(" "); }));

                if (feeDue.Any(x => x.ActCode != "1"))
                {
                    return new BillingResponseDTO()
                    {
                        ActCode = feeDue.Select(x => x.ActCode).FirstOrDefault(),
                        ResposeCode = feeDue.Select(x => x.ResposeCode).FirstOrDefault(),
                        ActDescription = feeDue.Select(x => x.ActDescription).FirstOrDefault(),
                        Description = feeDue.Select(x => x.ResposeDescription).FirstOrDefault(),
                        SchoolId = student.SchoolID.HasValue ? student.SchoolID.ToString() : null,
                        SchoolName = student.School.SchoolName,
                        ClassSection = student.Class.ClassDescription + '#' + student.Section.SectionName,
                        Term = String.Join(",", feeDue.Where(z => z.InvoiceAmount > 0).Select(w => w.Remarks.ToString())),//feeDue.Select(x => x.Remarks).FirstOrDefault(),
                        RollNo = student.AdmissionNumber,
                        OutstandingAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        Remarks = "Total due amount",
                        LocalTxnDtTime = DateTime.Now,
                        BilledAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        StudentName = student.FirstName + " " + (student.MiddleName == null ? "" : student.MiddleName) + " " + student.LastName
                    };
                }
                else
                {
                    return new BillingResponseDTO()
                    {
                        ActCode = "1",
                        ActDescription = "Fee Due",
                        SchoolId = student.SchoolID.HasValue ? student.SchoolID.ToString() : null,
                        SchoolName = student.School.SchoolName,
                        ClassSection = student.Class.ClassDescription + '#' + student.Section.SectionName,
                        Term = String.Join(",", feeDue.Select(w => w.Remarks.ToString())),
                        RollNo = student.AdmissionNumber,
                        OutstandingAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        Remarks = "Total due amount",
                        LocalTxnDtTime = DateTime.Now,
                        BilledAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        StudentName = student.FirstName + " " + (student.MiddleName == null ? "" : student.MiddleName) + " " + student.LastName
                    };
                }
            }
            catch (Exception ex)
            {
                return new BillingResponseDTO()
                {
                    ActCode = "1",
                    ActDescription = "Fee Due",
                    SchoolId = student.SchoolID.HasValue ? student.SchoolID.ToString() : null,
                    SchoolName = student.School.SchoolName,
                    ClassSection = student.Class.ClassDescription + '#' + student.Section.SectionName,
                    Term = "",
                    RollNo = student.AdmissionNumber,
                    OutstandingAmount = 0,
                    Remarks = "Total due amount",
                    LocalTxnDtTime = DateTime.Now,
                    BilledAmount = 0,
                    StudentName = student.FirstName + " " + (student.MiddleName == null ? "" : student.MiddleName) + " " + student.LastName
                };
            }
        }

        public PaymentResponseDTO MakePayment(BankBillPaymentDTO payment)
        {
            var mutualRepository = new Repository.MutualRepository();

            var tokenSetting = new SettingBL(null).GetSettingValue<string>("BillPaymentAPIToken");

            if (payment.Token != tokenSetting)
            {
                throw new Exception("Invalid token");
            }

            var agentSetting = new SettingBL(null).GetSettingValue<string>("BillPaymentAPIAgent");

            if (payment.AgentId != agentSetting)
            {
                throw new Exception("Invalid agent");
            }


            var settingValue = mutualRepository.GetSettingData("ISENABLEFEECOLL_DT");

            if (string.IsNullOrEmpty(settingValue.SettingValue) || settingValue.SettingValue == "0")
            {
                return new PaymentResponseDTO()
                {
                    description = "Transaction Completed",//"Enable Direct Transfer Collection",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                    referenceNumber = "12345", //returnCollection.FeeReceiptNo,
                    remarks = "Success",
                    resposeCode = 0
                };
            }
            //Temparary logs
            new PaymentRepository().MakePaymentErrorEntry(new Entity.Models.PaymentDetailsLog()
            {
                TransRef = payment.TransactionId,
                TransResult = Newtonsoft.Json.JsonConvert.SerializeObject(payment)
            });

            try
            {
                Entity.Models.Settings.Sequence transactionNoSequence = new Entity.Models.Settings.Sequence();

                var dataTransferPaymodeID = int.Parse(mutualRepository.GetSettingData("DIRECTTRANSFERID").SettingValue);
                var defaultStaffID = int.Parse(mutualRepository.GetSettingData("DEFAULTSTAFFID").SettingValue);
                var bankID = int.Parse(mutualRepository.GetSettingData("BANKID").SettingValue);
                var collectedStatusID = int.Parse(mutualRepository.GetSettingData("FEECOLLECTIONSTATUSID_COLLECTED").SettingValue);

                try
                {
                    transactionNoSequence = mutualRepository.GetNextSequence("BILLDESK_TRANSACTION_NO");
                }
                catch (Exception ex)
                {
                    var data = ex;
                    transactionNoSequence = null;
                }
                

                var student = new SchoolRepository().GetStudentDetail(payment.ChildQID, payment.StudentRollNumber);
                var feeDue = FeeDueGenerationMapper.Mapper(_context)
                  .FillPendingFeesForBank(student.ClassID.Value, student.StudentIID);

                if (feeDue.Count() == 0 || feeDue.Sum(x => x.InvoiceAmount).Value == 0)
                {
                    return new PaymentResponseDTO()
                    {
                        description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = "12345",
                        remarks = "No pending fees",
                        resposeCode = 1
                    };
                }
                var feeCollection = new FeeCollectionDTO();
                var payList = new List<FeeCollectionPaymentModeMapDTO>();
                feeCollection.StudentID = student.StudentIID;
                feeCollection.ClassID = student.ClassID;
                feeCollection.SectionID = student.SectionID;
                feeCollection.SchoolID = student.SchoolID;
                feeCollection.AcadamicYearID = student.AcademicYearID;
                feeCollection.CollectionDate = System.DateTime.Now;
                feeCollection.Amount = feeDue.Sum(x => x.InvoiceAmount).Value;
                feeCollection.PaidAmount = feeDue.Sum(x => x.InvoiceAmount).Value;
                feeCollection.CashierID = defaultStaffID;
                payList.Add(new FeeCollectionPaymentModeMapDTO()
                {
                    Amount = feeDue.Sum(x => x.InvoiceAmount).Value,
                    BankID = bankID,
                    PaymentModeID = dataTransferPaymodeID
                });
                feeCollection.FeeCollectionPaymentModeMapDTO = payList;
                feeCollection.FeeCollectionStatusID = collectedStatusID;
                feeCollection.GroupTransactionNumber = transactionNoSequence == null ? null : transactionNoSequence.Prefix == null ? null : transactionNoSequence.Prefix + transactionNoSequence.LastSequence;

                var feeData = new List<FeeCollectionFeeTypeDTO>();
                feeCollection.FeeTypes = new List<FeeCollectionFeeTypeDTO>();
                feeCollection.FeeFines = new List<FeeCollectionFeeFinesDTO>();
                if (feeDue.Any())
                {
                    feeDue.All(feetypess =>
                    {
                        feeData = (from n in feetypess.FeeDueFeeTypeMap
                                   where (n.Amount ?? 0) != 0
                                   select new FeeCollectionFeeTypeDTO()
                                   {
                                       Amount = n.Amount ?? 0,
                                       Balance = (n.Amount ?? 0) - (n.CreditNoteAmount ?? 0),
                                       NowPaying = (n.Amount ?? 0) - (n.CreditNoteAmount ?? 0),
                                       CreditNoteAmount = n.CreditNoteAmount ?? 0,
                                       FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsIID,
                                       CreditNoteFeeTypeMapID = n.CreditNoteFeeTypeMapID,
                                       CollectedAmount = n.CollectedAmount,
                                       StudentFeeDueID = n.StudentFeeDueID,
                                       FeeStructureFeeMapID = n.FeeStructureFeeMapID,
                                       MontlySplitMaps = !n.FeeDueMonthlySplit.Any(w => (w.Amount ?? 0) != 0) ? new List<FeeCollectionMonthlySplitDTO>()
                                                                    : (from m in n.FeeDueMonthlySplit
                                                                       where (m.Amount ?? 0) != 0
                                                                       select new FeeCollectionMonthlySplitDTO()
                                                                       {
                                                                           Amount = m.Amount ?? 0,
                                                                           Balance = (m.Amount ?? 0) - (m.CreditNoteAmount ?? 0),
                                                                           CreditNoteAmount = m.CreditNoteAmount ?? 0,
                                                                           NowPaying = (m.Amount ?? 0) - (m.CreditNoteAmount ?? 0),
                                                                           MonthID = m.MonthID,
                                                                           FeeDueMonthlySplitID = m.FeeDueMonthlySplitIID,
                                                                           Year = m.Year,
                                                                           RefundAmount = m.RefundAmount,
                                                                           TaxAmount = m.TaxAmount,
                                                                           TaxPercentage = m.TaxPercentage,
                                                                       }
                                                                     ).ToList(),

                                       FeeMasterID = n.FeeMasterID,
                                       FeePeriodID = n.FeePeriodID,

                                   }).ToList();

                        if (feeData.Any())
                            feeCollection.FeeTypes.AddRange(feeData);
                        return true;
                    });
                }

                var result = FeeCollectionMapper.Mapper(_context).SaveEntity(feeCollection);

                var returnCollection = Newtonsoft.Json.JsonConvert.DeserializeObject<FeeCollectionDTO>(result);
                // EmailProcess("vineethakr@outlook.com", feeCollection);
                if (returnCollection != null)
                {
                    return new PaymentResponseDTO()
                    {
                        description = "Transaction Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = returnCollection.FeeReceiptNo,
                        remarks = "Success",
                        resposeCode = 0,
                        mailID = returnCollection.EmailID,
                        id = returnCollection.FeeCollectionIID,
                        schoolID = returnCollection.SchoolID,
                        admissionNumber = returnCollection.AdmissionNo,
                        receiptNo = returnCollection.FeeReceiptNo
                    };
                }
                else
                {
                    return new PaymentResponseDTO()
                    {
                        description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = "12345",
                        remarks = "No pending fees",
                        resposeCode = 1
                    };
                }

            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BillPaymentBL>.Fatal(ex.Message, ex);

                return new PaymentResponseDTO()
                {
                    description = "Student not found",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                    referenceNumber = "123456", //returnCollection.FeeReceiptNo,
                    remarks = "Failure",
                    resposeCode = 1
                };
            }
        }

        public void EmailProcess(string email, FeeCollectionDTO entity)
        {
            String emailBody = "";
            String emailSubject = "";

            emailBody = @"<br /><p align='left'>Dear Parent/Guardian,<br /><br /></p>
                            Thank you for payment of fees<br />
                           
                            This is an online receipt generated which doesn’t require a signature.<br />
                            Any clarification please call School Accounts Dept.<br /><br />
                            Best regards<br /><br />
                            Podar Pearl School<br /><br />                        
                            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

            //Kindly find the receipt for the payment.< br />
            //emailBody = @"<br />
            //            <p align='left'>
            //            Dear Parent/Guardian,<br /></p>
            //            Fees has been collected.<br />
            //            please find the attachment herewith<br /><br />                        
            //            <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";

            emailSubject = "Fee Receipt";
            //var emaildata = new EmailNotificationDTO();

            var mutualRepository = new Repository.MutualRepository();
            var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

            string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

            try
            {
                var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

                String replaymessage = PopulateBody(email, emailBody);
                if (emailBody != "")
                {
                    if (hostDet == "Live")
                    {
                        SendMail(email, emailSubject, replaymessage, hostUser, null, null);
                    }
                    else
                    {
                        SendMail(defaultMail, emailSubject, replaymessage, hostUser, null, null);
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
        public void SendMail(string email, string subject, string msg, string mailname, String maildomain, List<string> attachments)
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

                // mailMsg.Attachments.Add(new Attachment(fileName));

                mailMsg.To.Add(email);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {
                //return "false";
                //lb_error.Visible = true;
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //return Json("ok", JsonRequestBehavior.AllowGet);
            //return "true";
        }

        //private void PDFExport()
        //{
        //    ReportParameter[] param = new ReportParameter[5];
        //    param[0] = new ReportParameter("Report_Parameter_0", "1st Para");
        //    param[1] = new ReportParameter("Report_Parameter_1", "2nd Para");
        //    param[2] = new ReportParameter("Report_Parameter_2", "3rd Para");
        //    param[3] = new ReportParameter("Report_Parameter_3", "4th Para");
        //    param[4] = new ReportParameter("Report_Parameter_4", "5th Para");

        //    DataSet dsData = "Fill this dataset with your data";
        //    ReportDataSource rdsAct = new ReportDataSource("RptActDataSet_usp_GroupAccntDetails", dsActPlan.Tables[0]);
        //    ReportViewer viewer = new ReportViewer();
        //    viewer.LocalReport.Refresh();
        //    viewer.LocalReport.ReportPath = "Reports/AcctPlan.rdlc"; //This is your rdlc name.
        //    viewer.LocalReport.SetParameters(param);
        //    viewer.LocalReport.DataSources.Add(rdsAct); // Add  datasource here         
        //    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        //    // byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        //    // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
        //    // System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Buffer = true;
        //    Response.Clear();
        //    Response.ContentType = mimeType;
        //    Response.AddHeader("content-disposition", "attachment; filename= filename" + "." + extension);
        //    Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
        //    Response.Flush(); // send it to the client to download  
        //    Response.End();

        //}


    }
}