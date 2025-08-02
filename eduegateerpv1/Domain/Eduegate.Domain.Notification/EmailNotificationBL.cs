using Eduegate.Framework;
using System.Net.Mail;
using System.Net;
using Eduegate.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Notifications;
using System.Net.Mime;
using System.Net.Http;
using Eduegate.Domain.Report;

namespace Eduegate.Domain.Notification
{
    public class EmailNotificationBL
    {
        private CallContext _context;

        public EmailNotificationBL(CallContext context = null)
        {
            _context = context;
        }

        public void SendMail(string toEmailID, string subject, string msgContent, EmailTypes typeName, Dictionary<string, string> parameters, List<string> ccMailIDs = null)
        {
            try
            {
                var mailHost = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILHOST_" + typeName.ToString().ToUpper());
                mailHost = string.IsNullOrEmpty(mailHost) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILHOST") : mailHost;

                var mailPortNumber = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPORT_" + typeName.ToString().ToUpper());
                mailPortNumber = string.IsNullOrEmpty(mailPortNumber) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPORT") : mailPortNumber;

                var SMTPUserName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPUSERNAME_" + typeName.ToString().ToUpper());
                SMTPUserName = string.IsNullOrEmpty(SMTPUserName) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPUSERNAME") : SMTPUserName;
                SMTPUserName = ResolveEmailVariable(SMTPUserName, parameters);

                var SMTPPassword = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPASSWORD_" + typeName.ToString().ToUpper());
                SMTPPassword = string.IsNullOrEmpty(SMTPPassword) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPASSWORD") : SMTPPassword;

                var fromEmail = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILFROM_" + typeName.ToString().ToUpper());
                fromEmail = string.IsNullOrEmpty(fromEmail) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILFROM") : fromEmail;
                fromEmail = ResolveEmailVariable(fromEmail, parameters);

                var mailName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILUSER_" + typeName.ToString().ToUpper());
                mailName = string.IsNullOrEmpty(mailName) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILUSER") : mailName;

                var senderEmailID = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILID_" + typeName.ToString().ToUpper());
                senderEmailID = string.IsNullOrEmpty(senderEmailID) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILID") : senderEmailID;
                senderEmailID = ResolveEmailVariable(senderEmailID, parameters);

                var isEnableSecurityProtocol = new Domain.Setting.SettingBL(_context).GetSettingValue<bool>("ISENABLE_SECURITY_PROTOCOL");
                if (isEnableSecurityProtocol == true)
                {
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                          | SecurityProtocolType.Tls11
                                                          | SecurityProtocolType.Tls12;
                    }
                }

                SmtpClient ss = new SmtpClient();
                ss.Host = mailHost;//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = string.IsNullOrEmpty(mailPortNumber) ? 587 : int.Parse(mailPortNumber);// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(SMTPUserName, toEmailID, subject, msgContent);
                mailMsg.From = new MailAddress(fromEmail, mailName);
                mailMsg.To.Add(toEmailID);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                if (ccMailIDs != null)
                {
                    foreach (var ccMailID in ccMailIDs)
                    {
                        mailMsg.CC.Add(ccMailID);
                    }
                }

                ss.Send(mailMsg);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"{typeName} Mail Sending failed. Error message: {errorMessage}", ex);
            }
        }

        public string ResolveEmailVariable(string templateString, Dictionary<string, string> variables)
        {
            //variables.All(variable =>
            //{
            //    templateString = templateString.Replace("{" + variable.Key + "}", variable.Value);
            //    return true;
            //});

            foreach (var variable in variables)
            {
                //logic to resolve veriable
                templateString = templateString.Replace("{" + variable.Key + "}", variable.Value);
            }

            return templateString;
        }

        public void SendMailWithAttachment(string toEmailID, string subject, string msgContent, EmailTypes typeName, Dictionary<string, string> parameters, List<string> listFileNames, string fileNameFormat, List<string> ccMailIDs = null)
        {
            try
            {
                var mailHost = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILHOST_" + typeName.ToString().ToUpper());
                mailHost = string.IsNullOrEmpty(mailHost) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILHOST") : mailHost;

                var mailPortNumber = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPORT_" + typeName.ToString().ToUpper());
                mailPortNumber = string.IsNullOrEmpty(mailPortNumber) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPORT") : mailPortNumber;

                var SMTPUserName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPUSERNAME_" + typeName.ToString().ToUpper());
                SMTPUserName = string.IsNullOrEmpty(SMTPUserName) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPUSERNAME") : SMTPUserName;
                SMTPUserName = ResolveEmailVariable(SMTPUserName, parameters);

                var SMTPPassword = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPASSWORD_" + typeName.ToString().ToUpper());
                SMTPPassword = string.IsNullOrEmpty(SMTPPassword) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPASSWORD") : SMTPPassword;

                var fromEmail = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILFROM_" + typeName.ToString().ToUpper());
                fromEmail = string.IsNullOrEmpty(fromEmail) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILFROM") : fromEmail;
                fromEmail = ResolveEmailVariable(fromEmail, parameters);

                var mailName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILUSER_" + typeName.ToString().ToUpper());
                mailName = string.IsNullOrEmpty(mailName) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILUSER") : mailName;

                var senderEmailID = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILID_" + typeName.ToString().ToUpper());
                senderEmailID = string.IsNullOrEmpty(senderEmailID) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILID") : senderEmailID;
                senderEmailID = ResolveEmailVariable(senderEmailID, parameters);

                var isEnableSecurityProtocol = new Domain.Setting.SettingBL(_context).GetSettingValue<bool>("ISENABLE_SECURITY_PROTOCOL");
                if (isEnableSecurityProtocol == true)
                {
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                          | SecurityProtocolType.Tls11
                                                          | SecurityProtocolType.Tls12;
                    }
                }

                SmtpClient ss = new SmtpClient();
                ss.Host = mailHost;//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = string.IsNullOrEmpty(mailPortNumber) ? 587 : int.Parse(mailPortNumber);// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(SMTPUserName, toEmailID, subject, msgContent);
                mailMsg.From = new MailAddress(fromEmail, mailName);

                AddMailAttachments(mailMsg, listFileNames, fileNameFormat);

                mailMsg.To.Add(toEmailID);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                if (ccMailIDs != null)
                {
                    foreach (var ccMailID in ccMailIDs)
                    {
                        mailMsg.CC.Add(ccMailID);
                    }
                }

                ss.Send(mailMsg);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"{typeName} Mail Sending failed. Error message: {errorMessage}", ex);
            }
        }

        public void SendFromCustomerMail(string toEmailID, string fromEmail, string subject, string msgContent, EmailTypes typeName, Dictionary<string, string> parameters, List<string> ccMailIDs = null)
        {
            try
            {
                var mailHost = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILHOST_" + typeName.ToString().ToUpper());
                mailHost = string.IsNullOrEmpty(mailHost) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILHOST") : mailHost;

                var mailPortNumber = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPORT_" + typeName.ToString().ToUpper());
                mailPortNumber = string.IsNullOrEmpty(mailPortNumber) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPORT") : mailPortNumber;

                var SMTPUserName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPUSERNAME_" + typeName.ToString().ToUpper());
                SMTPUserName = string.IsNullOrEmpty(SMTPUserName) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPUSERNAME") : SMTPUserName;
                SMTPUserName = ResolveEmailVariable(SMTPUserName, parameters);

                var SMTPPassword = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPASSWORD_" + typeName.ToString().ToUpper());
                SMTPPassword = string.IsNullOrEmpty(SMTPPassword) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("SMTPPASSWORD") : SMTPPassword;

                var mailName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILUSER_" + typeName.ToString().ToUpper());
                mailName = string.IsNullOrEmpty(mailName) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILUSER") : mailName;

                var senderEmailID = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILID_" + typeName.ToString().ToUpper());
                senderEmailID = string.IsNullOrEmpty(senderEmailID) ? new Domain.Setting.SettingBL(_context).GetSettingValue<string>("EMAILID") : senderEmailID;
                senderEmailID = ResolveEmailVariable(senderEmailID, parameters);

                var isEnableSecurityProtocol = new Domain.Setting.SettingBL(_context).GetSettingValue<bool>("ISENABLE_SECURITY_PROTOCOL");
                if (isEnableSecurityProtocol == true)
                {
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                          | SecurityProtocolType.Tls11
                                                          | SecurityProtocolType.Tls12;
                    }
                }

                SmtpClient ss = new SmtpClient();
                ss.Host = mailHost;//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = string.IsNullOrEmpty(mailPortNumber) ? 587 : int.Parse(mailPortNumber);// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(SMTPUserName, toEmailID, subject, msgContent);
                mailMsg.From = new MailAddress(fromEmail, mailName);
                mailMsg.To.Add(toEmailID);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                if (ccMailIDs != null)
                {
                    foreach (var ccMailID in ccMailIDs)
                    {
                        mailMsg.CC.Add(ccMailID);
                    }
                }

                ss.Send(mailMsg);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"{typeName} Mail Sending failed. Error message: {errorMessage}", ex);
            }
        }

        #region Test Mail Part

        public OperationResultDTO SendMailNotification(MailNotificationDTO mailDTO)
        {
            var message = new OperationResultDTO()
            {
                operationResult = OperationResult.Success,
                Message = "Success!"
            };

            try
            {
                var hostEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPUSERNAME");
                var hostPassword = new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPPASSWORD");
                var fromEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("EMAILFROM");

                var hostUser = new Domain.Setting.SettingBL().GetSettingValue<string>("EMAILUSER");
                var clientHost = new Domain.Setting.SettingBL().GetSettingValue<string>("EMAILHOST");

                var documentPhysicalPath = new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath");

                var isEnableSecurityProtocol = new Domain.Setting.SettingBL(_context).GetSettingValue<bool>("ISENABLE_SECURITY_PROTOCOL");
                if (isEnableSecurityProtocol == true)
                {
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                          | SecurityProtocolType.Tls11
                                                          | SecurityProtocolType.Tls12;
                    }
                }

                SmtpClient ss = new SmtpClient();
                ss.Host = clientHost;//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = mailDTO.PortNumber;// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = mailDTO.IsUseDefaultCredentials;
                ss.EnableSsl = mailDTO.IsEnableSSL;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(hostEmail, mailDTO.MailToAddress, mailDTO.MailSubject, mailDTO.MailMessage);
                mailMsg.From = new MailAddress(fromEmail, hostUser);

                if (mailDTO.IsMailContainsAttachment)
                {
                    var listFileNames = new List<string>
                    {
                        documentPhysicalPath + "\\DummyPDFFile.pdf"
                    };

                    foreach (var fileName in listFileNames)
                    {
                        mailMsg.Attachments.Add(new System.Net.Mail.Attachment(fileName));
                    }
                }

                mailMsg.To.Add(mailDTO.MailToAddress);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                ss.Send(mailMsg);

                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = "Acknowledgment: Your email has been sent successfully!"
                };
            }
            catch (Exception ex)
            {
                message = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = ex.Message
                };

                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Test Mailing failed. Error message: {errorMessage}", ex);
            }

            return message;
        }

        #endregion Test Mail Part

        public string PopulateBody(string Name, string htmlMessage, string mailclosingRegard = null, string mailRegardName = null)
        {
            #region SettingData binding

            var settingBL = new Domain.Setting.SettingBL(_context);

            string clientName = settingBL.GetSettingValue<string>("CLIENTNAME");

            string clientMailLogo = settingBL.GetSettingValue<string>("CLIENT_MAILLOGO");

            string clientWebsite = settingBL.GetSettingValue<string>("CLIENT_WEBSITE");

            string poweredBy = settingBL.GetSettingValue<string>("POWEREDBYCOMPANYWEBSITE");

            string clientFB = settingBL.GetSettingValue<string>("CLIENT_FBSITE");

            string fbLogo = settingBL.GetSettingValue<string>("CLIENT_FBLOGO");

            string linkedInSite = settingBL.GetSettingValue<string>("CLIENT_LINKEDINSITE");

            string linkedInLogo = settingBL.GetSettingValue<string>("CLIENT_LINKEDINLOGO");

            string clientInsta = settingBL.GetSettingValue<string>("CLIENT_INSTASITE");

            string instaLogo = settingBL.GetSettingValue<string>("CLIENT_INSTALOGO");

            string clientYoutube = settingBL.GetSettingValue<string>("CLIENT_YOUTUBESITE");

            string youtubeLogo = settingBL.GetSettingValue<string>("CLIENT_YOUTUBELOGO");

            string clientWhatsappLink = settingBL.GetSettingValue<string>("CLIENT_WHATSAPPLINK");

            string whatsappLogo = settingBL.GetSettingValue<string>("CLIENT_WHATSAPPLOGO");

            string clientPlayStoreLink = settingBL.GetSettingValue<string>("CLIENT_PLAYSTORELINK");

            string playstoreLogo = settingBL.GetSettingValue<string>("CLIENT_PLAYSTORELOGO");

            string clientAppStoreLink = settingBL.GetSettingValue<string>("CLIENT_APPSTORELINK");

            string appstoreLogo = settingBL.GetSettingValue<string>("CLIENT_APPSTORELOGO");

            string clientDescription = settingBL.GetSettingValue<string>("CLIENT_DESCRIPTION");

            string mailingAddress = settingBL.GetSettingValue<string>("CLIENT_MAILINGADDRESS");

            string disclosure = settingBL.GetSettingValue<string>("CLIENT_DISCLOSURE");

            string headerImage = settingBL.GetSettingValue<string>("CLIENT_HEADERIMAGE");

            string footerLogo = settingBL.GetSettingValue<string>("CLIENT_FOOTERLOGO");

            if (string.IsNullOrEmpty(mailclosingRegard))
            {
                mailclosingRegard = settingBL.GetSettingValue<string>("CLIENT_MAIL_CLOSING_REGARDS");
            }

            if (string.IsNullOrEmpty(mailRegardName))
            {
                mailRegardName = settingBL.GetSettingValue<string>("CLIENT_DEFAULT_MAIL_REGARD_NAME");
            }

            #endregion

            // Get the file path
            string filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Views/External/EmailTemplate.html");

            // Read the content of the file
            string body = System.IO.File.ReadAllText(filePath);

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
            body = body.Replace("{CLIENT_WHATSAPPLINK}", clientWhatsappLink);
            body = body.Replace("{CLIENT_WHATSAPPLOGO}", whatsappLogo);
            body = body.Replace("{CLIENT_PLAYSTORELINK}", clientPlayStoreLink);
            body = body.Replace("{CLIENT_PLAYSTORELOGO}", playstoreLogo);
            body = body.Replace("{CLIENT_APPSTORELINK}", clientAppStoreLink);
            body = body.Replace("{CLIENT_APPSTORELOGO}", appstoreLogo);
            body = body.Replace("{CLIENT_DESCRIPTION}", clientDescription);
            body = body.Replace("{CLIENT_MAILINGADDRESS}", mailingAddress);
            body = body.Replace("{CLIENT_DISCLOSURE}", disclosure);
            body = body.Replace("{CLIENT_HEADERIMAGE}", headerImage);
            body = body.Replace("{CLIENT_FOOTERLOGO}", footerLogo);
            body = body.Replace("{CLIENT_MAIL_REGARDS}", mailclosingRegard);
            body = body.Replace("{CLIENT_MAIL_REGARD_NAME}", mailRegardName);

            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());

            #endregion

            return body;
        }

        public void AddMailAttachments(MailMessage mailMsg, List<string> listFileNames, string fileNameFormat)
        {
            var reportingService = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("REPORTING_SERVICE", "eduegate");

            if (reportingService.ToLower() == "ssrs")
            {
                var reportServer = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("ReportServer");
                var reportServerDirect = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("ReportServerDirect");
                var reportDomainName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("ReportServerDomain");

                var reportUserName = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("ReportServerDomainUser");
                var reportPassword = new Domain.Setting.SettingBL(_context).GetSettingValue<string>("ReportServerDomainPassword");

                foreach (var fileName in listFileNames)
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string updatedFileName = string.IsNullOrEmpty(reportServerDirect) ? fileName : fileName.Replace(reportServer, reportServerDirect);

                        webClient.Credentials = new NetworkCredential(reportUserName, reportPassword, reportDomainName);
                        byte[] pdfBytes = webClient.DownloadData(updatedFileName);

                        // Create the attachment
                        Attachment attachment = new Attachment(new MemoryStream(pdfBytes), fileNameFormat.Contains(".pdf") ? fileNameFormat : fileNameFormat + ".pdf", MediaTypeNames.Application.Pdf);

                        // Add the attachment to the email
                        mailMsg.Attachments.Add(attachment);
                    }
                }
            }
            else
            {
                foreach (var inputString in listFileNames)
                {
                    byte[] pdfBytes = [];

                    if (!inputString.ToLower().Contains(".pdf"))
                    {
                        // Split the input string by '&'
                        string[] parts = inputString.Split('&');

                        // Extract reportName and parameter string
                        string reportName = parts[0].Split('=')[1]; // Extracts the value after 'reportName='
                        string parameter = parts[1].Split('=')[1];  // Extracts the value after 'parameter='
                        string format = "PDF";

                        //Need to use this for bring report
                        //byte[] pdfBytes = new Eduegate.Domain.Report.ReportViewerBL(_context).GetReportFile(reportName, parameter, format);

                        pdfBytes = new EmailReportViewerBL(_context).GetReportFile(reportName, parameter, format);
                    }
                    else
                    {                        
                        // Initialize HttpClient
                        using (HttpClient client = new HttpClient())
                        {
                            // Synchronously download the PDF file as a stream
                            HttpResponseMessage response = client.GetAsync(inputString).Result;  // Blocks until the result is returned
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception("Failed to download PDF file.");
                            }

                            // Read the PDF file and load it into a MemoryStream
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                // Copy the content from the response stream into memory stream
                                response.Content.CopyToAsync(memoryStream).Wait();  // Synchronously wait for the copy to complete

                                // Convert to byte array
                                pdfBytes = memoryStream.ToArray();
                            }
                        }
                    }

                    using (HttpClient httpClientClient = new HttpClient())
                    {
                        // Create the attachment
                        Attachment attachment = new Attachment(new MemoryStream(pdfBytes), fileNameFormat.Contains(".pdf") ? fileNameFormat : fileNameFormat + ".pdf", MediaTypeNames.Application.Pdf);

                        // Add the attachment to the email
                        mailMsg.Attachments.Add(attachment);
                    }
                }
            }
        }

    }
}