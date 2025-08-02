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
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Domain.Entity.School.Models;
using Microsoft.EntityFrameworkCore;
using Eduegate.Logger;

namespace Eduegate.Domain.Mappers.Notification.Notifications
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
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private PushNotificationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.NotificationAlerts.Where(X => X.NotificationAlertIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

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
                        var students = dbContext.Students.Where(x => x.IsActive == true && toDto.BranchID == x.SchoolID && _context.AcademicYearID == x.AcademicYearID).AsNoTracking().ToList();

                        foreach (var toUser in students)
                        {
                            var parentLgn = dbContext.Parents.AsNoTracking().FirstOrDefault(p => p.ParentIID == toUser.ParentID && p.LoginID != null);
                            toLoginLists.Add(new KeyValueDTO
                            {
                                Key = parentLgn?.LoginID.ToString(),
                                Value = parentLgn.GaurdianEmail != null ? parentLgn.GaurdianEmail : parentLgn.FatherEmailID
                            });
                        }

                        toDto.MessageSendTo = toLoginLists;
                    }

                    //when send type is 'ALL' and usertype is Staff 
                    else if (toDto.PushNotificationUser == "Staff")
                    {
                        var staffs = dbContext.Employees.Where(x => x.IsActive == true && toDto.BranchID == x.BranchID).AsNoTracking().ToList();

                        foreach (var toUser in staffs)
                        {
                            toLoginLists.Add(new KeyValueDTO
                            {
                                Key = toUser.LoginID.ToString(),
                                Value = toUser.WorkEmail != null ? toUser.WorkEmail : null
                            });
                        }

                        toDto.MessageSendTo = toLoginLists;
                    }

                    //when send type is 'ALL' and usertype is Parent 
                    else if (toDto.PushNotificationUser == "Parent")
                    {
                        var parents = dbContext.Parents.Where(x => x.Students.Any( s => s.IsActive == true && toDto.BranchID == s.SchoolID)).AsNoTracking().ToList();

                        foreach (var toUser in parents)
                        {
                            toLoginLists.Add(new KeyValueDTO
                            {
                                Key = toUser.LoginID.ToString(),
                                Value = toUser.GaurdianEmail
                            });
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
                        string toEmailAddress = null;
                        long? toLoginID = null;

                        if(toDto.MessageSendType == "1")
                        {
                            toEmailAddress = sendTo?.Value;
                            toLoginID = long.Parse(sendTo?.Key);
                        }
                        else
                        {
                            if (toDto.PushNotificationUser == "Student")
                            {
                                var studID = Int64.Parse(sendTo.Key);
                                var getStudDatas = dbContext.Students.AsNoTracking().FirstOrDefault(s => s.StudentIID == studID);

                                toEmailAddress = getStudDatas.Parent.GaurdianEmail;
                                toLoginID = getStudDatas.Parent.LoginID;
                            }

                            else if (toDto.PushNotificationUser == "Parent")
                            {
                                var parentID = Int64.Parse(sendTo.Key);
                                var getparentDatas = dbContext.Parents.AsNoTracking().FirstOrDefault(s => s.ParentIID == parentID);

                                toEmailAddress = getparentDatas.GaurdianEmail;
                                toLoginID = getparentDatas.LoginID;
                            }

                            else if (toDto.PushNotificationUser == "Staff")
                            {
                                var staffID = Int64.Parse(sendTo.Key);
                                var getEmployeeDatas = dbContext.Employees.AsNoTracking().FirstOrDefault(s => s.EmployeeIID == staffID);

                                toEmailAddress = getEmployeeDatas.WorkEmail;
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();

                        if (toDto.IsEmail == true)
                        {
                            var emailDetails = toDto.Message;
                            var emailSub = toDto.Subject;

                            var emaildata = new EmailNotificationDTO();
                            try
                            {
                                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmailAddress, emailDetails);

                                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
                                var mailParameters = new Dictionary<string, string>();

                                if (emailDetails != "")
                                {
                                    if (hostDet.ToLower() == "live")
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmailAddress, emailSub, mailMessage, Eduegate.Services.Contracts.Enums.EmailTypes.PushNotification, mailParameters);
                                    }
                                    else
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, Eduegate.Services.Contracts.Enums.EmailTypes.PushNotification, mailParameters);
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

        public static void SendAndSavePushNotification(long toLoginID, long fromLoginID, string message, string title, IReadOnlyDictionary<string, string> settings, long? referenceID = null, long? referenceScreenID = null)
        {
            var alertstatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULTALERTSTATUSID");

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
                        ReferenceID = referenceID.HasValue ? referenceID : null,
                        ReferenceScreenID = referenceScreenID.HasValue ? referenceScreenID : null,
                        CreatedBy = fromLoginID,
                        CreatedDate = DateTime.Now,
                    };

                    using (var dbContext = new dbEduegateSchoolContext())
                    {
                        dbContext.NotificationAlerts.Add(entity);

                        if (entity.NotificationAlertIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        dbContext.SaveChanges();
                    }

                    SendPushNotification(toLoginID, message, title, settings, string.Empty);
                }
            }
        }

        //public static void SendPushNotification(long loginID, string message, string title, IReadOnlyDictionary<string, string> settings)
        //{
        //    try
        //    {
        //        var registeredDevices = new ReferenceDataRepository().GetRegisteredUserDevice(loginID);

        //        foreach (var device in registeredDevices)
        //        {
        //            if (!string.IsNullOrEmpty(device.DeviceToken))
        //            {
        //                var notificationHelper = new FirebaseHelper(settings["KEY_FILE"]);
        //                //notificationHelper.SendMessageAsync(device.DeviceToken, title, message, settings, string.Empty)
        //                //.GetAwaiter().GetResult();
        //                var result = notificationHelper.SendMessageAsync(device.DeviceToken, title, message, settings, string.Empty).ConfigureAwait(false);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogHelper<PushNotificationMapper>.Fatal(ex.Message, ex);
        //    }
        //}

        public static void SendPushNotification(long loginID, string message, string title, IReadOnlyDictionary<string, string> settings, string appID = null)
        {
            try
            {
                var registeredDevices = new ReferenceDataRepository().GetRegisteredUserDevice(loginID);

                foreach (var device in registeredDevices)
                {
                    if (!string.IsNullOrEmpty(device.DeviceToken))
                    {
                        var notificationHelper = new FirebaseHelper(string.IsNullOrEmpty(appID) ? settings["KEY_FILE"] : settings["KEY_FILE_" + appID]);

                        notificationHelper.SendMultiCastMessageAsync(new List<DeviceTokenInfo> { new DeviceTokenInfo()
                        { ChannelName = "", DeviceToken = device.DeviceToken } },
                            title, message, settings, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper<PushNotificationMapper>.Fatal(ex.Message, ex);
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
                    var entities = branchID == 0 ? dbContext.Students.Where(x => x.AcademicYearID == _context.AcademicYearID && x.IsActive == true).AsNoTracking().ToList() :
                        dbContext.Students.Where(x => x.SchoolID == branchID && x.AcademicYearID == _context.AcademicYearID && x.IsActive == true).AsNoTracking().ToList();

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
                    var entities = branchID == 0 ? dbContext.Parents.Where(x => x.Students.Any(y => y.ParentID == x.ParentIID && y.AcademicYearID == _context.AcademicYearID && y.IsActive == true)).AsNoTracking().ToList() :
                    dbContext.Parents.Where(x => x.Students.Any(y => y.ParentID == x.ParentIID && y.AcademicYearID == _context.AcademicYearID && y.SchoolID == branchID && y.IsActive == true)).AsNoTracking().ToList();

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
                    var entities = branchID == 0 ? dbContext.Employees.Where(x => x.IsActive == true).AsNoTracking().ToList() :
                    dbContext.Employees.Where(x => x.BranchID == branchID && x.IsActive == true).AsNoTracking().ToList();

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

        public NotificationAlertsDTO GetNotificationAlertByReferenceID(long? toLoginID, long referenceID, long referenceScreenID)
        {
            var alertDTO = new NotificationAlertsDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new NotificationAlert();
                if (toLoginID.HasValue)
                {
                    entity = dbContext.NotificationAlerts
                        .Where(x => x.ToLoginID == toLoginID && x.ReferenceID == referenceID && x.ReferenceScreenID == referenceScreenID)
                        .AsNoTracking().FirstOrDefault();
                }
                else
                {
                    entity = dbContext.NotificationAlerts
                        .Where(x => x.ReferenceID == referenceID && x.ReferenceScreenID == referenceScreenID)
                        .AsNoTracking().FirstOrDefault();
                }

                if (entity != null)
                {
                    alertDTO = new NotificationAlertsDTO()
                    {
                        NotificationAlertIID = entity.NotificationAlertIID,
                        Message = entity.Message,
                        NotificationDate = entity.NotificationDate,
                        ToLoginID = entity.ToLoginID,
                        ReferenceID = entity.ReferenceID,
                        ReferenceScreenID = entity.ReferenceScreenID,
                    };
                }
                else
                {
                    alertDTO = null;
                }
            }

            return alertDTO;
        }

        public string AlertNotifiedStatusByReferenceID(long? toLoginID, long referenceID, long referenceScreenID)
        {
            var result = string.Empty;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new NotificationAlert();
                if (toLoginID.HasValue)
                {
                    entity = dbContext.NotificationAlerts
                        .Where(x => x.ToLoginID == toLoginID && x.ReferenceID == referenceID && x.ReferenceScreenID == referenceScreenID)
                        .AsNoTracking().FirstOrDefault();
                }
                else
                {
                    entity = dbContext.NotificationAlerts
                        .Where(x => x.ReferenceID == referenceID && x.ReferenceScreenID == referenceScreenID)
                        .AsNoTracking().FirstOrDefault();
                }

                if (entity != null)
                {
                    result = "Notified";
                }
                else
                {
                    result = "Not notified";
                }
            }

            return result;
        }

    }
}