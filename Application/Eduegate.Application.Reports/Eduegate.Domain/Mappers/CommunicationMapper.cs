using Newtonsoft.Json;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using System;
using Eduegate.Domain.Repository;
using System.Linq;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Communications;
using System.Net.Mail;
using Eduegate.Framework.Extensions;
using System.Net;

namespace Eduegate.Domain.Mappers
{
    public class CommunicationMapper : DTOEntityDynamicMapper
    {
        public static CommunicationMapper Mapper(CallContext context)
        {
            var mapper = new CommunicationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CommunicationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CommunicationDTO ToDTO(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Communications.FirstOrDefault(X => X.CommunicationIID == IID);

                var communicationDTO = new CommunicationDTO()
                {
                    CommunicationIID = entity.CommunicationIID,
                    ReferenceID = entity.LeadID,
                    FromEmail = entity.FromEmail,
                    ToEmail = entity.Email,
                    Subject = entity.Notes,
                    EmailContent = entity.EmailContent,
                    MobileNumber = entity.MobileNumber,
                    FollowUpDate = entity.FollowUpDate.HasValue ? entity.FollowUpDate : null,
                    EmailTemplateID = entity.EmailTemplateID,
                    CommunicationTypeID = entity.CommunicationTypeID,
                };

                return communicationDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CommunicationDTO;
            string emailBody = "";
            string emailSubject = "";

            //convert the dto to entity and pass to the repository.
            var entity = new Communication()
            {
                CommunicationIID = toDto.CommunicationIID,
                LeadID = toDto.ReferenceID,
                FromEmail = toDto.FromEmail,
                Email = toDto.ToEmail,
                Notes = toDto.Subject,
                EmailContent = toDto.EmailContent,
                MobileNumber = toDto.MobileNumber,
                EmailTemplateID = toDto.EmailTemplateID,
                FollowUpDate = toDto.FollowUpDate,
                CommunicationTypeID = toDto.CommunicationTypeID,
                CommunicationDate = DateTime.Now,
            };

            if (toDto.CommunicationIID == 0)
            {
                entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                entity.CreatedDate = DateTime.Now;
            }
            else
            {
                entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                entity.UpdatedDate = DateTime.Now;
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                dbContext.Communications.Add(entity);

                if (entity.CommunicationIID == 0 && entity.CommunicationTypeID != null)
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            if (entity.CommunicationIID != 0 && entity.CommunicationTypeID == 1)
            {
                emailBody = @"<br/><p style='font-family:Helvetica;font-size:1rem; font-weight:bold;'>"+ entity.EmailContent + "</p><br/><br/><br/><p style='font-size:0.7rem;'<b>Please Note : </b>do not reply to this email as it is a computer generated email</p>";
                emailSubject = entity.Notes;
            }

            string replaymessage = PopulateBody(toDto.ToEmail, emailBody);
            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();

            MutualRepository mutualRepository = new MutualRepository();
            var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

            string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

            if (emailBody != "")
            {
                if (hostDet == "Live")
                {
                    SendMail(toDto.ToEmail, toDto.FromEmail, emailSubject, replaymessage, hostUser);
                }
                else
                {
                    SendMail(defaultMail, toDto.FromEmail, emailSubject, replaymessage, hostUser);
                }
            }

            return ToDTOString(ToDTO(entity.CommunicationIID));
        }

        public void SendMail(string toEmail, string fromEmail, string subject, string msg, string mailname)
        {
            string email_id = toEmail;
            string mail_body = msg;
            try
            {
                var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
                var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();
                SmtpClient ss = new SmtpClient();
                ss.Host = ConfigurationExtensions.GetAppConfigValue("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = ConfigurationExtensions.GetAppConfigValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = true;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(fromEmail, toEmail, subject, msg);
                mailMsg.From = new MailAddress(fromEmail);
                mailMsg.To.Add(toEmail);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {
                //throw new Exception("Something Went Wrong! Mail Not Sended");
                //lb_error.Visible = true;
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //return Json("ok", JsonRequestBehavior.AllowGet);
        }

        private string PopulateBody(string Name, string htmlMessage)
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

        public CommunicationDTO GetMailIDDetailsFromLead(long? leadID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var leadEntity = dbContext.Leads.FirstOrDefault(X => X.LeadIID == leadID);

                var loginEntity = _context != null ? _context.LoginID.HasValue ? dbContext.Logins.FirstOrDefault(X => X.LoginIID == _context.LoginID) : null : null;

                var communicationDTO = new CommunicationDTO()
                {
                    FromEmail = loginEntity != null ? loginEntity.LoginEmailID : null,
                    ToEmail = leadEntity.EmailAddress,
                    MobileNumber = leadEntity.MobileNumber,
                };

                return communicationDTO;
            }
        }

        public CommunicationDTO GetEmailTemplateByID(int? templateID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var emailTemplate = dbContext.EmailTemplates.FirstOrDefault(X => X.EmailTemplateID == templateID);

                var loginEntity = _context != null ? _context.LoginID.HasValue ? dbContext.Logins.FirstOrDefault(X => X.LoginIID == _context.LoginID) : null : null;

                var communicationDTO = new CommunicationDTO()
                {
                    EmailTemplateID = emailTemplate.EmailTemplateID,
                    EmailTemplate = emailTemplate.Template,
                };

                return communicationDTO;
            }
        }

    }
}