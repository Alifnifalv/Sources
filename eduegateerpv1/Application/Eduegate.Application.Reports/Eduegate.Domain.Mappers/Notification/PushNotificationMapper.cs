using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using Eduegate.Services.Contracts.Notifications;
using System.Linq;
using Eduegate.Domain.Repository;
using System.Collections.Generic;
using Eduegate.Notification.Firebase;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Framework.Extensions;
using System.Net.Mail;
using System.Net;

namespace Eduegate.Domain.Mappers.Notification
{
    public class PushNotificationMapper : DTOEntityDynamicMapper
    {
        public static PushNotificationMapper Mapper(CallContext context)
        {
            var mapper = new PushNotificationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PushNotificationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private PushNotificationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.NotificationAlerts.FirstOrDefault(X => X.NotificationAlertIID == IID);

                var dto = new PushNotificationDTO()
                {
                    NotificationAlertIID = entity.NotificationAlertIID,
                    Message = entity.Message,
                    AlertActionTypeID = entity.AlertTypeID,
                    AlertStatusID = entity.AlertStatusID,
                    NotificationDate = entity.NotificationDate,
                    FromLoginID = entity.FromLoginID,
                    ReferenceID = entity.ReferenceID,
                    CreatedBy = Convert.ToInt32(entity.CreatedBy),
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = Convert.ToInt32(entity.UpdatedBy)
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PushNotificationDTO;
            MutualRepository mutualRepository = new MutualRepository();

            var settings = new Dictionary<string, string>();

            if (toDto.PushNotificationUser.ToLower().Contains("parent"))
            {
                settings = NotificationSetting.GetParentAppSettings();
            }
            if (toDto.PushNotificationUser.ToLower().Contains("staff"))
            {
                settings = NotificationSetting.GetEmployeeAppSettings();
            }

            if (toDto.Message == null)
            {
                throw new Exception("Message is required!");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region  MessageSendType == 1 for 'ALL'

                if (toDto.MessageSendType == "1")
                {
                    List<KeyValueDTO> toLoginLists = new List<KeyValueDTO>();

                    //when send type is 'ALL' and usertype is Student 
                    if (toDto.PushNotificationUser == "Student")
                    {
                        var students = dbContext.Students.Where(x => x.IsActive == true && toDto.BranchID == x.SchoolID && _context.AcademicYearID == x.AcademicYearID).ToList();

                        foreach (var toUser in students)
                        {
                            var parentLgn = dbContext.Parents.FirstOrDefault(p => p.ParentIID == toUser.ParentID && p.LoginID != null);
                            toLoginLists.Add(new KeyValueDTO { Key = parentLgn?.LoginID.ToString(), Value = parentLgn.GaurdianEmail != null ? parentLgn.GaurdianEmail : parentLgn.FatherEmailID }
                                );
                        }

                        toDto.MessageSendTo = toLoginLists;
                    }
                    //when send type is 'ALL' and usertype is Staff 
                    else if (toDto.PushNotificationUser == "Staff")
                    {
                        var staffs = dbContext.Employees.Where(x => x.IsActive == true && toDto.BranchID == x.BranchID).ToList();

                            foreach (var toUser in staffs)
                            {
                                toLoginLists.Add(new KeyValueDTO { Key = toUser.LoginID.ToString(), Value = toUser.WorkEmail != null ? toUser.WorkEmail : null }
                                    );
                            }

                            toDto.MessageSendTo = toLoginLists;
                    }
                    //when send type is 'ALL' and usertype is Parent 
                    else if (toDto.PushNotificationUser == "Parent")
                    {
                        var parents = dbContext.Parents.Where(x => x.Students.Any( s => s.IsActive == true && toDto.BranchID == s.SchoolID)).ToList();

                        foreach (var toUser in parents)
                        {
                            toLoginLists.Add(new KeyValueDTO { Key = toUser.LoginID.ToString(), Value = toUser.GaurdianEmail }
                                );
                        }

                        toDto.MessageSendTo = toLoginLists;
                    }


                    #region old code commented
                    //if (!toDto.BranchID.HasValue && !toDto.PushNotificationUserID.HasValue)
                    //{
                    //    var loginIds = GetAllUsersLoginIds();

                    //    foreach (var login in loginIds)
                    //    {
                    //        if (toDto.IsEmail != true)
                    //        {

                    //        }
                    //        else
                    //        {
                    //            SendPushNotification(login, toDto.Message, toDto.Title, settings);
                    //        }
                    //    }
                    //}

                    //if (toDto.BranchID.HasValue && !toDto.PushNotificationUserID.HasValue)
                    //{
                    //    var loginIds = GetAllUsersLoginIdsByDefultBranch(toDto.BranchID.Value);

                    //    foreach (var login in loginIds)
                    //    {
                    //        SendPushNotification(login, toDto.Message, toDto.Title, settings);
                    //    }
                    //}

                    //if (!toDto.BranchID.HasValue && toDto.PushNotificationUserID.HasValue)
                    //{
                    //    if (toDto.PushNotificationUserID == 1)
                    //    {
                    //        var loginIds = GetAllParentLoginIds();

                    //        foreach (var login in loginIds)
                    //        {
                    //            SendPushNotification(login, toDto.Message, toDto.Title, settings);
                    //        }
                    //    }
                    //}

                    //if (toDto.BranchID.HasValue && toDto.PushNotificationUserID.HasValue)
                    //{
                    //    if (toDto.PushNotificationUserID == 1)
                    //    {
                    //        var loginIds = GetBranchWiseParentLoginIDs(toDto.BranchID);

                    //        foreach (var login in loginIds)
                    //        {
                    //            SendPushNotification(login, toDto.Message, toDto.Title, settings);
                    //        }
                    //    }
                    //}
                    #endregion ol code comment ended
                }
                #endregion

                NotificationAlert notifications = null;
                //PushNotification Send for seperate persons
                if (toDto.MessageSendTo.Count > 0)
                {
                    foreach (var sendTo in toDto.MessageSendTo)
                    {
                        string EmailAddress = null;
                        long? toLoginID = null;

                        if(toDto.MessageSendType == "1")
                        {
                            EmailAddress = sendTo?.Value;
                            toLoginID = long.Parse(sendTo?.Key);
                        }
                        else
                        {
                            if (toDto.PushNotificationUser == "Student")
                            {
                                var studID = Int64.Parse(sendTo.Key);
                                var getStudDatas = dbContext.Students.FirstOrDefault(s => s.StudentIID == studID);

                                EmailAddress = getStudDatas.Parent.GaurdianEmail;
                                toLoginID = getStudDatas.Parent.LoginID;
                            }

                            else if (toDto.PushNotificationUser == "Parent")
                            {
                                var parentID = Int64.Parse(sendTo.Key);
                                var getparentDatas = dbContext.Parents.FirstOrDefault(s => s.ParentIID == parentID);

                                EmailAddress = getparentDatas.GaurdianEmail;
                                toLoginID = getparentDatas.LoginID;
                            }

                            else if (toDto.PushNotificationUser == "Staff")
                            {
                                var staffID = Int64.Parse(sendTo.Key);
                                var getEmployeeDatas = dbContext.Employees.FirstOrDefault(s => s.EmployeeIID == staffID);

                                EmailAddress = getEmployeeDatas.WorkEmail;
                                toLoginID = getEmployeeDatas.LoginID;
                            }
                        }

                        var entity = new NotificationAlert()
                        {
                            NotificationAlertIID = toDto.NotificationAlertIID,
                            Message = toDto.Message,
                            AlertTypeID = toDto.AlertActionTypeID,
                            AlertStatusID = toDto.AlertStatusID,
                            NotificationDate = toDto.NotificationDate.HasValue ? toDto.NotificationDate : DateTime.Now.Date,
                            FromLoginID = toDto.FromLoginID.HasValue ? toDto.FromLoginID : _context.LoginID,
                            ToLoginID = toDto.ToLoginID.HasValue ? toDto.ToLoginID : toLoginID,
                            ReferenceID = toDto.ReferenceID,
                            CreatedBy = toDto.NotificationAlertIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.NotificationAlertIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.NotificationAlertIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = toDto.NotificationAlertIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        };
                        notifications = entity;
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        dbContext.SaveChanges();

                        if (toDto.IsEmail == true)
                        {
                            var emailDetails = toDto.Message;
                            var emailSub = toDto.Subject;

                            var emaildata = new EmailNotificationDTO();
                            try
                            {
                                var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();
                                String replaymessage = PopulateBody(EmailAddress, emailDetails);

                                var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                                string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

                                if (emailDetails != "")
                                {
                                    if (hostDet == "Live")
                                    {
                                        SendMail(EmailAddress, emailSub, replaymessage, hostUser);
                                    }
                                    else
                                    {
                                        SendMail(defaultMail, emailSub, replaymessage, hostUser);
                                    }
                                }

                            }
                            catch { }
                        }
                        else
                        {
                            SendPushNotification((long)toLoginID, toDto.Message, toDto.Title, settings);
                        }
                    }
                }

                return ToDTOString(ToDTO(notifications.NotificationAlertIID));
            }
        }

        public static void SendAndSavePushNotification(long toLoginID, long fromLoginID, string message, string title, IReadOnlyDictionary<string, string> settings)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var alertstatus = mutualRepository.GetSettingData("DEFAULTALERTSTATUSID").SettingValue;

            var registeredDevices = new ReferenceDataRepository().GetRegisteredUserDevice(toLoginID);

            var registeredDevicelogin = registeredDevices.Count > 0 ? registeredDevices.FirstOrDefault().LoginID : null;

            if (registeredDevicelogin != null)
            {
                if (toLoginID == registeredDevicelogin)
                {
                    var entity = new NotificationAlert()
                    {
                        NotificationAlertIID = 0,
                        Message = message,
                        AlertTypeID = null,
                        AlertStatusID = alertstatus != null ? int.Parse(alertstatus) : (int?)null,
                        NotificationDate = DateTime.Now,
                        FromLoginID = fromLoginID,
                        ToLoginID = toLoginID,
                        ReferenceID = null,
                        CreatedBy = fromLoginID,
                        CreatedDate = DateTime.Now,
                    };

                    using (var dbContext = new dbEduegateSchoolContext())
                    {
                        dbContext.NotificationAlerts.Add(entity);

                        if (entity.NotificationAlertIID == 0)
                        {
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        }
                        dbContext.SaveChanges();
                    }

                    SendPushNotification(toLoginID, message, title, settings);
                }
            }
        }

        public static void SendPushNotification(long loginID, string message, string title, IReadOnlyDictionary<string, string> settings)
        {
            try
            {
                var registeredDevices = new ReferenceDataRepository().GetRegisteredUserDevice(loginID);

                foreach (var device in registeredDevices)
                {
                    if (!string.IsNullOrEmpty(device.DeviceToken))
                    {
                        var notificationHelper = new FirebaseHelper(settings["KEY_FILE"]);
                        //notificationHelper.SendMessageAsync(device.DeviceToken, title, message, settings, string.Empty)
                        //.GetAwaiter().GetResult();
                        var result = notificationHelper.SendMessageAsync(device.DeviceToken, title, message, settings, string.Empty).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogHelper<PushNotificationMapper>.Fatal(ex.Message, ex);
            }
        }

        public List<long> GetAllUsersLoginIds()
        {
            return new UserServiceRepository().GetAllUsersLoginIds();
        }

        public List<long> GetAllUsersLoginIdsByDefultBranch(long branchId)
        {
            return new UserServiceRepository().GetAllUsersLoginIdsByDefultBranch(branchId);
        }

        public List<long> GetUsersLoginIdsByCustomerId(long customerId)
        {
            return new UserServiceRepository().GetUsersLoginIdsByCustomerId(customerId);
        }

        public List<long> GetAllParentLoginIds()
        {
            return new UserServiceRepository().GetAllParentLoginIds();
        }

        public List<long> GetBranchWiseParentLoginIDs(long? branchID)
        {
            return new UserServiceRepository().GetBranchWiseParentLoginIDs(branchID);
        }

        public List<KeyValueDTO> GetToNotificationUsersByUser(int userID,int branchID,string user)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dataList = new List<KeyValueDTO>();

                if (user == "Student")
                {
                    var entities = branchID == 0 ? dbContext.Students.Where(x => x.AcademicYearID == _context.AcademicYearID && x.IsActive == true).ToList() :
                        dbContext.Students.Where(x => x.SchoolID == branchID && x.AcademicYearID == _context.AcademicYearID && x.IsActive == true).ToList();

                    foreach (var stud in entities)
                    {
                        dataList.Add(new KeyValueDTO
                        {
                            Key = stud.StudentIID.ToString(),
                            Value = stud.AdmissionNumber + " - " + stud.FirstName + " " + stud.MiddleName + " " + stud.LastName,
                        });
                    }
                }
                else if (user == "Parent")
                {
                    var entities = branchID == 0 ? dbContext.Parents.Where(x => x.Students.Any(y => y.ParentID == x.ParentIID && y.AcademicYearID == _context.AcademicYearID && y.IsActive == true)).ToList() :
                    dbContext.Parents.Where(x => x.Students.Any(y => y.ParentID == x.ParentIID && y.AcademicYearID == _context.AcademicYearID && y.SchoolID == branchID && y.IsActive == true)).ToList();

                    foreach (var parent in entities)
                    {
                        dataList.Add(new KeyValueDTO
                        {
                            Key = parent.ParentIID.ToString(),
                            Value = parent.ParentCode + " - " + parent.FatherFirstName + " " + parent.FatherMiddleName + " " + parent.FatherLastName,
                        });
                    }
                }
                else if (user == "Staff")
                {
                    var entities = branchID == 0 ? dbContext.Employees.Where(x => x.IsActive == true).ToList() :
                    dbContext.Employees.Where(x => x.BranchID == branchID && x.IsActive == true).ToList();

                    foreach (var emp in entities)
                    {
                        dataList.Add(new KeyValueDTO
                        {
                            Key = emp.EmployeeIID.ToString(),
                            Value = emp.EmployeeCode + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName,
                        });
                    }
                }

                return dataList;
            }
        }


        public void SendMail(string email, string subject, string msg, string mailname)
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
                ss.UseDefaultCredentials = true;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(hostEmail, email, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailname);
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
        private String PopulateBody(String Name, String htmlMessage)
        {
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader("http://erp.eduegate.com/emailtemplate.html"))
            //{
            //    body = reader.ReadToEnd();
            //}
            body = "<!DOCTYPE html> <html> <head> <title></title> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1'> <meta http-equiv='X-UA-Compatible' content='IE=edge' /> <style type='text/css'> </style> </head> <body style='background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;'> <!-- HIDDEN PREHEADER TEXT --> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <!-- LOGO --> <tr> <td bgcolor='#bd051c' align='center'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#bd051c' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;'> <div align='center' style='width:100%;display:inline-block;text-align:center;'><img src='https://parent.pearlschool.org/img/podarlogo_mails.png' style='height:70px;margin:1rem;' /></div> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='left' style='padding-left: 1rem; color: #666666; font-family: Lato, Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'>{HTMLMESSAGE}</td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='black' align='center' style='padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #fffefe; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'> <div class='copyrightdiv' style='color: white;padding: 30px 30px 30px 30px;' >Copyright &copy; {YEAR}<a style='text-decoration: none' target='_blank' href='http://pearlschool.org/' > <span style='color: white; font-weight: bold;' >&nbsp;&nbsp; PEARL SCHOOL</span > </a > </div > </td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='#f4f4f4' align='left' style='padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;' > <br > <div class='PoweredBy' style='text-align:center;' > <div style='padding-bottom:1rem;' > Powered By: <a style='text-decoration: none; color: #0c7aec;' id='eduegate' href='https://softopsolutionpvtltd.com/' target='_blank' > SOFTOP SOLUTIONS PVT LTD</a > </div > <a href='https://www.facebook.com/pearladmin1/' > <img src='https://parent.pearlschool.org/Images/logo/fb-logo.png' alt='facebook' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.linkedin.com/company/pearl-school-qatar/?viewAsMember=true' > <img src='https://parent.pearlschool.org/Images/logo/linkedin-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.instagram.com/pearlschool_qatar/' > <img src='https://parent.pearlschool.org/Images/logo/insta-logo.png' alt='instagram' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.youtube.com/channel/UCFQKYMivtaUgeSifVmg79aQ' > <img src='https://parent.pearlschool.org/Images/logo/youtube-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > </div > </td > </tr > </table > </td > </tr > </table > </body > </html >";
            //body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }
    }
}