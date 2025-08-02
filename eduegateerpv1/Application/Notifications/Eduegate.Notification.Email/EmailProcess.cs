//using Eduegate.Framework.Extensions;
//using Eduegate.Notification.Email.ViewModels;
//using Eduegate.Services.Contracts.Notifications;
////using RazorEngine;
////using RazorEngine.Templating;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Mail;
//using System.Reflection;
//using Eduegate.Domain;

//namespace Eduegate.Notification.Email
//{
//    public class EmailProcess
//    {
//        public static void ProcessEmail(EmailNotificationDTO emailInfo)
//        {
//            var parameterVM = ViewModels.KeyValueParameterViewModel.GetParameterCollection(emailInfo);
//            var additionaParams = ViewModels.KeyValueParameterViewModel.ToParameterVM(emailInfo.AdditionalParameters);
//            parameterVM.AddRange(additionaParams);
//            var emailHtmlBody = SendEmail(
//                emailInfo.TemplateName, 
//                parameterVM,
//                emailInfo.Subject, 
//                emailInfo.ToEmailID,
//                emailInfo.FromEmailID,
//                emailInfo.FromEmailID,
//                emailInfo.FromEmailID,
//                emailInfo.ToBCCEmailID
//                );
//            emailInfo.EmailData = emailHtmlBody;
//        }

//        private static string SendEmail(
//            string templateName,
//           List<ViewModels.KeyValueParameterViewModel> parameterVM,
//           string subject,
//           string toEmailID, 
//           string toEmailDisplayName ,
//           string fromEmailID, 
//           string fromEmailDisplayName, 
//           string bcc = null)
//        {
//            //build the email tempalte
//            //TODO : issue with razor compilation, required more R&D, see debug support
//            //emailHtmlBody = CompileRazerGetHtml(emailInfo.TemplateName, parameterVM);
//            // now taking for ERP host
//            var emailHtmlBody = GetTemplate(templateName, parameterVM);

//            //******************
//            #region  SMTP MSG
//            string emailFrom = new Eduegate.Domain.SettingBL().GetSettingValue<string>("EmailFrom");
//            System.Net.Mail.SmtpClient smtpMsg = new System.Net.Mail.SmtpClient();
//            smtpMsg.Port =  new Domain.Setting.SettingBL().GetSettingValue<int>("smtpPort");
//            smtpMsg.EnableSsl = true;
//            smtpMsg.TargetName = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom");
//            smtpMsg.Host = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailHost").ToString();
//            smtpMsg.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
//            System.Net.NetworkCredential credentialMsg = new System.Net.NetworkCredential(
//                new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPUserName").ToString(),
//                new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPPassword").ToString()
//                );
//            smtpMsg.Credentials = credentialMsg;
//            System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
//            //System.Net.Mail.MailAddress toAddress = new System.Net.Mail.MailAddress(emailInfo.ToEmailID);
//            msgMail.To.Add(toEmailID);
//            #endregion
//            var fromAddress = string.IsNullOrWhiteSpace(fromEmailID) ?
//                new System.Net.Mail.MailAddress(emailFrom, string.IsNullOrEmpty(fromEmailDisplayName) ?
//                fromEmailID : fromEmailDisplayName) :
//                new System.Net.Mail.MailAddress(fromEmailID, string.IsNullOrEmpty(toEmailDisplayName) ?
//                toEmailID : toEmailDisplayName);

//            // Put email address separated by "," as per documentation for Bcc.Add
//            // Blind carbon copy
//            if (bcc.IsNotNullOrEmpty())
//            {
//                msgMail.Bcc.Add(bcc);
//            }

//            // Carbon copy
//            if (bcc.IsNotNullOrEmpty())
//            {
//                msgMail.CC.Add(bcc);
//            }


//            //Check if reportName && returnFile params are there and true
//            /* once PDF attachments is fixed then we have to enable it */
//            System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType();
//            contentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;

//            foreach (var parameter in parameterVM.Where(p => p.ParameterName.Contains("attachment")))
//            {
//                contentType.Name = Path.GetFileName(parameter.ParameterValue);
//                msgMail.Attachments.Add(new Attachment(parameter.ParameterValue, contentType));
//            }

//            //msgMail.To.Add(toAddress);
//            msgMail.From = fromAddress;
//            msgMail.IsBodyHtml = true;
//            msgMail.BodyEncoding = System.Text.UTF8Encoding.UTF8;
//            msgMail.Subject = subject;
//            msgMail.Body = @emailHtmlBody;
//            smtpMsg.EnableSsl = true;
//            if (new Domain.Setting.SettingBL().GetSettingValue<bool>("EnableSsl"))
//            {
//                smtpMsg.EnableSsl = true;
//            }

//            if (new Domain.Setting.SettingBL().GetSettingValue<int>("PortNo") != 0)
//            {
//                smtpMsg.Port = new Domain.Setting.SettingBL().GetSettingValue<int>("PortNo");
//            }
//            smtpMsg.Send(msgMail);
//            return emailHtmlBody;
//        }

//        private static string GetTemplate(string templateName, List<KeyValueParameterViewModel> parameters)
//        {
//            var cshtmlTemplate = ReadResourceFile(templateName);
//            //make the list as flag
//            var p = new Dictionary<string, object>();

//            foreach (var parameter in parameters)
//            {
//                if (parameter.ParameterObject != null)
//                {
//                    p[parameter.ParameterName] = parameter.ParameterObject;
//                }
//                else
//                {
//                    p[parameter.ParameterName] = parameter.ParameterValue;
//                }
//            }

//            return RunCompile(cshtmlTemplate, p);
//        }

//        private static string RunCompile(string template, object model)
//        {
//            return Engine.Razor.RunCompile(template, "templateKey", null, model);
//        }

//        private static string ReadResourceFile(string filename)
//        {
//            var thisAssembly = Assembly.GetExecutingAssembly();
//            var resourceFile = thisAssembly.GetManifestResourceNames()
//                .Where(x=> x.Contains(filename))
//                .FirstOrDefault();
//            using (var stream = thisAssembly.GetManifestResourceStream(resourceFile))
//            {
//                using (var reader = new StreamReader(stream))
//                {
//                    return reader.ReadToEnd();
//                }
//            }
//        }
//    }
//}
