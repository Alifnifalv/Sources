using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Eduegate.Domain.Repository
{
    public class NotificationRepository
    {
        public List<NotificationsQueue> GetNextNotificationsFromQueue(NotificationTypes notificationType, int numberOfQueues)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<NotificationsQueue> queues = null;

                if (notificationType == NotificationTypes.All)
                    queues = db.NotificationsQueues.Take(numberOfQueues).ToList();
                else
                    queues = db.NotificationsQueues.Where(x => x.NotificationTypeID == (int)notificationType).Take(numberOfQueues).ToList();

                foreach (var queue in queues)
                {
                    db.NotificationsQueues.Remove(queue);
                    db.NotificationsProcessings.Add(new NotificationsProcessing()
                    {
                        NotificationQueueID = queue.NotificationQueueIID,
                        NotificationTypeID = queue.NotificationTypeID,
                        CreatedDate = DateTime.Now,
                        IsReprocess = queue.IsReprocess,
                    });
                }

                db.SaveChanges();
                return queues;
            }
        }

        /// <summary>
        /// Email data for processing
        /// </summary>
        /// <param name="notificationQueueID"></param>
        /// <returns></returns>
        public EmailNotificationData GetEmailData(long notificationQueueID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.EmailNotificationDatas.Where(a => a.NotificationQueueID == notificationQueueID).FirstOrDefault();
                dbContext.Entry(entity).Reference(a => a.EmailNotificationType).Load();
                return entity;
            }
        }

        public EmailNotificationData SaveEmailData(EmailNotificationData emailData, long parentNotificationID = 0, bool isReprocess = false)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (emailData.EmailMetaDataIID == 0)
                {
                    //get a new queue id and associate with the email data
                    var newQueue = dbContext.NotificationsQueues.Add(new NotificationsQueue() { IsReprocess = isReprocess, NotificationTypeID = (int)NotificationTypes.Email, CreatedDate = DateTime.Now });
                    dbContext.SaveChanges();

                    if (parentNotificationID == 0)
                        parentNotificationID = newQueue.NotificationQueueIID;

                    // Insert notification parent map for this queue
                    dbContext.NotificationQueueParentMaps.Add(new NotificationQueueParentMap() { NotificationQueueID = newQueue.NotificationQueueIID, ParentNotificationQueueID = parentNotificationID });

                    dbContext.SaveChanges();
                    emailData.NotificationQueueID = newQueue.NotificationQueueIID;
                    dbContext.EmailNotificationDatas.Add(emailData);
                    dbContext.Entry(emailData).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    var entity = dbContext.EmailNotificationDatas.Where(x => x.EmailMetaDataIID == emailData.EmailMetaDataIID).FirstOrDefault();
                    entity.EmailData = emailData.EmailData;
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }

                dbContext.SaveChanges();

                var result = dbContext.EmailNotificationDatas.Where(x => x.EmailMetaDataIID == emailData.EmailMetaDataIID).FirstOrDefault();
                dbContext.Entry(result).Reference(a => a.EmailNotificationType).Load();
                return result;

            }
        }

        public SMSNotificationData GetSMSData(long notificationQueueID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.SMSNotificationDatas.Where(a => a.NotificationQueueID == notificationQueueID).FirstOrDefault();
                dbContext.Entry(entity).Reference(a => a.SMSNotificationType).Load();
                return entity;
            }
        }

        public SMSNotificationData SaveSMSData(SMSNotificationData data, long parentNotificationID = 0, bool isReprocess = false)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (data.SMSNotificationDataIID == 0)
                {
                    //get a new queue id and associate with the email data
                    var newQueue = dbContext.NotificationsQueues.Add(new NotificationsQueue() { IsReprocess = isReprocess, NotificationTypeID = (int)NotificationTypes.Email, CreatedDate = DateTime.Now });
                    dbContext.SaveChanges();

                    if (parentNotificationID == 0)
                        parentNotificationID = newQueue.NotificationQueueIID;

                    // Insert notification parent map for this queue
                    dbContext.NotificationQueueParentMaps.Add(new NotificationQueueParentMap() { NotificationQueueID = newQueue.NotificationQueueIID, ParentNotificationQueueID = parentNotificationID });

                    dbContext.SaveChanges();
                    data.NotificationQueueID = newQueue.NotificationQueueIID;
                    dbContext.SMSNotificationDatas.Add(data);
                    dbContext.Entry(data).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    var entity = dbContext.SMSNotificationDatas.Where(x => x.SMSNotificationDataIID == data.SMSNotificationDataIID).FirstOrDefault();
                    entity.SMSContent = data.SMSContent;
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }

                dbContext.SaveChanges();

                var result = dbContext.SMSNotificationDatas.Where(x => x.SMSNotificationDataIID == data.SMSNotificationDataIID).FirstOrDefault();
                dbContext.Entry(result).Reference(a => a.SMSNotificationType).Load();
                return result;

            }
        }

        public NotificationQueueParentMap GetParentNotification(long notificationQueueID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.NotificationQueueParentMaps.Where(x => x.NotificationQueueID == notificationQueueID).FirstOrDefault();
            }
        }

        public bool UpdateNotificationFromInprocess(long notificationQueueID, int status, string reason = null)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                try
                {
                    var entity = dbContext.NotificationsProcessings.Where(a => a.NotificationQueueID == notificationQueueID).FirstOrDefault();
                    dbContext.NotificationsProcessings.Remove(entity);

                    //insert into log
                    dbContext.NotificationLogs.Add(new NotificationLog()
                    {
                        NotificationQueueID = entity.NotificationQueueID,
                        NotificationStatusID = status,
                        NotificationTypeID = entity.NotificationTypeID,
                        EndDate = DateTime.Now,
                        StartDate = entity.CreatedDate,
                        Reason = reason,
                    });

                    dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Add email notification in Queue
        /// </summary>
        /// <param name="notification"></param>
        /// <returns>EmailNotificationsQueue</returns>
        public NotificationsQueue AddNotification(NotificationsQueue notification)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = db.NotificationsQueues.Add(notification);
                db.SaveChanges();
            }
            return notification;
        }

        /// <summary>
        /// Add notification data to metadata
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns>bool</returns>
        public bool AddEmailMetaData(EmailNotificationData metadata)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = db.EmailNotificationDatas.Add(metadata);
                db.SaveChanges();
            }
            return exit;
        }

        public bool UpdateNotificationParentMap(long notificationQueueID, bool isSuccess)
        {
            var exit = false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var notificationParentMap = dbContext.NotificationQueueParentMaps.Where(x => x.NotificationQueueID == notificationQueueID).SingleOrDefault();
                if (notificationParentMap.IsNotNull())
                {
                    notificationParentMap.IsNotificationSuccess = isSuccess;
                    dbContext.SaveChanges();
                    exit = true;
                }
            }
            return exit;
        }

        /// <summary>
        /// Single method to add notification in queue and metadata
        /// </summary>
        /// <param name="notification">EmailNotificationsQueue</param>
        /// <param name="metadata">string</param>
        /// <returns></returns>
        public bool AddNotification(NotificationsQueue notification, string metadata)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = db.NotificationsQueues.Add(notification);
                if (result.IsNotNull())
                {
                    var emailMetaData = new EmailNotificationData();
                    emailMetaData.NotificationQueueID = result.NotificationQueueIID;
                    emailMetaData.EmailData = metadata;
                    db.EmailNotificationDatas.Add(emailMetaData);
                    db.SaveChanges();
                    exit = true;
                }
            }
            return exit;
        }

        public bool AddNotificationToProcess(NotificationsProcessing notification)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = db.NotificationsProcessings.Add(notification);
                db.SaveChanges();
                exit = true;
            }
            return exit;
        }

        public Services.Contracts.NotificationDTO GetNotificationDetail(long notificationQueueID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                return (from nd in dbContext.EmailNotificationDatas
                        join ent in dbContext.EmailNotificationTypes on nd.EmailNotificationTypeID equals ent.EmailNotificationTypeID

                        // Left join notification tables
                        join nl in dbContext.NotificationLogs on nd.NotificationQueueID equals nl.NotificationQueueID into ndnl
                        from nl in ndnl.DefaultIfEmpty()

                        join nq in dbContext.NotificationsQueues on nd.NotificationQueueID equals nq.NotificationQueueIID into ndnq
                        from nq in ndnq.DefaultIfEmpty()

                        join np in dbContext.NotificationsProcessings on nd.NotificationQueueID equals np.NotificationQueueID into ndnp
                        from np in ndnp.DefaultIfEmpty()

                        join ns in dbContext.NotificationStatuses on nl.NotificationStatusID equals ns.NotificationStatusID into ndns
                        from ns in ndns.DefaultIfEmpty()

                        where nd.NotificationQueueID == notificationQueueID
                        select new Services.Contracts.NotificationDTO
                        {
                            FromEmailID = nd.FromEmailID,
                            NotificationQueueIID = nd.NotificationQueueID,
                            NotificationStatus = (ns.Description == null && nq.IsReprocess == true) ? Services.Contracts.Enums.NotificationStatuses.Reprocess :
                            (ns.Description == null) ? Services.Contracts.Enums.NotificationStatuses.New :
                            (ns.Description == null) ? Services.Contracts.Enums.NotificationStatuses.InProcess : (Services.Contracts.Enums.NotificationStatuses)ns.NotificationStatusID,
                            NotificationTypeName = ent.Name,
                            ToEmailID = nd.ToEmailID,
                            Reason = nl.Reason,
                            IsReprocess = np.IsReprocess,
                            Subject = nd.Subject,
                            NotificationProcessingID = np.NotificationProcessingIID,
                            CreatedDate = nd.CreatedDate,


                        }).FirstOrDefault();

            }
        }

        public bool RemoveNotificationFromQueue(long emailNotificationQueueID)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var notificationQueue = db.NotificationsQueues.Where(x => x.NotificationQueueIID == emailNotificationQueueID).SingleOrDefault();
                db.NotificationsQueues.Remove(notificationQueue);
                db.SaveChanges();
                exit = true;
            }
            return exit;
        }


        public bool ProcessNotification(NotificationsProcessing processingNotification)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // Insert into processing
                var result = db.NotificationsProcessings.Add(processingNotification);

                // Remove from queue
                if (result.NotificationProcessingIID > 0)
                {
                    exit = RemoveNotificationFromQueue(result.NotificationQueueID);
                    db.SaveChanges();
                }
            }
            return exit;
        }

        public NotificationsQueue GetNotificationFromQueue(int notificationQueueID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.NotificationsQueues.Where(x => x.NotificationQueueIID == notificationQueueID).SingleOrDefault();
            }
        }

        public EmailNotificationType GetEmailNotificationType(int notificationTypeID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.EmailNotificationTypes.Where(x => x.EmailNotificationTypeID == notificationTypeID).FirstOrDefault();
            }
        }

        public string GetEmailNotificationTemplate(int emailNotificationTypeID)
        {
            var templatePath = string.Empty;

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var notificationtype = db.EmailNotificationTypes.Where(x => x.EmailNotificationTypeID == emailNotificationTypeID).SingleOrDefault();
                templatePath = notificationtype.EmailTemplateFilePath;
            }

            return templatePath;
        }

        public bool AddNotificationLog(NotificationLog notificationLog)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                try
                {
                    dbContext.NotificationLogs.Add(notificationLog);
                    dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsStockNotificationSubscribed(long skuId, string emailId)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                isSuccess = dbContext.StockNotifications.Where(x => x.ProductSKUMapID == skuId && x.EmailID == emailId).Any();
            }
            return isSuccess;
        }

        public bool AddStockNotification(StockNotification entity)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.StockNotifications.Add(entity);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }


        public bool ModifyStockNotification(StockNotification entity)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                StockNotification stockNotification = dbContext.StockNotifications.Where(x => x.ProductSKUMapID == entity.ProductSKUMapID && x.EmailID == entity.EmailID).FirstOrDefault();
                stockNotification.NotficationStatusID = entity.NotficationStatusID;
                dbContext.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool AddNotificationAlert(long referenceID, Eduegate.Services.Contracts.Enums.AlertTypeEnum alert, long fromLoginID, string desription)
        {

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (dbContext.NotificationAlerts.Where(a => a.ReferenceID == referenceID && a.AlertTypeID == (int)alert).Any())
                {
                    var entity = dbContext.NotificationAlerts.Where(a => a.ReferenceID == referenceID && a.AlertTypeID == (int)alert).FirstOrDefault();
                    dbContext.NotificationAlerts.Remove(entity);
                }

                NotificationAlert nAlert = new NotificationAlert();
                nAlert.AlertTypeID = (int)alert;
                nAlert.ReferenceID = referenceID;
                nAlert.FromLoginID = fromLoginID;
                nAlert.NotificationDate = DateTime.Now;
                nAlert.CreatedBy = fromLoginID;
                nAlert.CreatedDate = DateTime.Now;
                nAlert.AlertStatusID = 2;
                nAlert.Message = desription;

                dbContext.NotificationAlerts.Add(nAlert);
                dbContext.SaveChanges();

                return true;

            }
            return false;
        }
        //public Services.Contracts.NotificationDTO GetNotificationDetails(long notificationQueueID)
        //{
        //    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
        //    {
        //        try
        //        {
        //            return (from nd in dbContext.EmailNotificationDatas
        //                    from ent in dbContext.EmailNotificationTypes.Where(X=>X.EmailNotificationTypeID == nd.EmailNotificationTypeID)

        //                    // Left join notification tables
        //                    from nl in dbContext.NotificationLogs.Where(x=> x.NotificationQueueID == nd.NotificationQueueID)

        //                    from nq in dbContext.NotificationsQueues.Where(x=> x.NotificationQueueIID == nd.NotificationQueueID)

        //                    from np in dbContext.NotificationsProcessings.Where(x=>x.NotificationQueueID == nd.NotificationQueueID)

        //                    from ns in dbContext.NotificationStatuses.Where(x=>x.NotificationStatusID == nl.NotificationStatusID)

        //                    where nd.NotificationQueueID == notificationQueueID
        //                    select new Services.Contracts.NotificationDTO
        //                    {
        //                        FromEmailID = nd.FromEmailID,
        //                        NotificationQueueIID = nd.NotificationQueueID,
        //                        NotificationStatus = (nq.NotificationQueueIID != null && ns.Description == null && nq.IsReprocess == true) ? Services.Contracts.Enums.NotificationStatuses.Reprocess :
        //                        (nq.NotificationQueueIID != null && ns.Description == null) ? Services.Contracts.Enums.NotificationStatuses.New :
        //                        (np.NotificationQueueID != null && ns.Description == null) ? Services.Contracts.Enums.NotificationStatuses.InProcess : (Services.Contracts.Enums.NotificationStatuses)ns.NotificationStatusID,
        //                        NotificationTypeName = ent.Name,
        //                        ToEmailID = nd.ToEmailID,
        //                        Reason = nl.Reason,
        //                        IsReprocess = np.IsReprocess,
        //                        Subject = nd.Subject,
        //                        NotificationProcessingID = np.NotificationProcessingIID,
        //                        CreatedDate = nd.CreatedDate,


        //                    }).FirstOrDefault();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}


        public string GetNotificationDetails(long NotificationQueueID)
        {

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var details = (from nd in db.EmailNotificationDatas
                               join nq in db.NotificationsQueues on nd.NotificationQueueID equals nq.NotificationQueueIID
                               where nd.NotificationQueueID == NotificationQueueID
                               select nd);
                return details.ToString();
            }

        }

        public EmailNotificationType GetEmailDetails(long Id)
        {
            using (var db = new dbEduegateERPContext())
            {
                var entity = db.EmailNotificationTypes.Where(a => a.EmailNotificationTypeID == Id)
                           .FirstOrDefault();
                return entity;
            }
        }

        public EmailNotificationType SaveEmailTemplates(EmailNotificationType email)
        {
            try
            {
                using (var db = new dbEduegateERPContext())
                {
                    db.EmailNotificationTypes.Add(email);
                    db.Entry(email).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return email;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<NotificationAlert> GetAlerts(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var entity = db.NotificationAlerts
                            .Include(a => a.AlertStatus)
                            //.Include(a => a.AlertTypeID)
                            .Include(a => a.Login)
                            .Include(a => a.Login.Employees)
                            .Include(a => a.Login1)
                            .Include(a => a.Login1.Employees)
                            .Where(a => a.ToLoginID == loginID)
                            .OrderByDescending(a => a.NotificationAlertIID)
                           .ToList();
                return entity;
            }
        }
        public List<NotificationAlert> GetNotificationAlert(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var alertStatus = db.Settings.FirstOrDefault(x => x.SettingCode == "ALERTSTATUS_ID").SettingValue;//statusID for Unread notification
                int? alertStatusID = int.Parse(alertStatus);
                var entity = db.NotificationAlerts
                            .Include(a => a.AlertStatus)
                            //.Include(a => a.AlertTypeID)
                            .Include(a => a.Login)
                            .Include(a => a.Login.Employees)
                            .Include(a => a.Login1)
                            .Include(a => a.Login1.Employees)
                            .Where(a => a.ToLoginID == loginID && a.AlertStatusID == alertStatusID  /*(DbFunctions.TruncateTime(a.ExpiryDate.Value) > DbFunctions.TruncateTime(System.DateTime.Now) || DbFunctions.TruncateTime(a.ExpiryDate.Value) == DbFunctions.TruncateTime(System.DateTime.Now))*/)
                            .OrderByDescending(a => a.NotificationAlertIID)
                           .ToList();
                return entity;
            }
        }

        public List<NotificationAlert> GetAllNotificationAlert(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var entity = db.NotificationAlerts
                            .Include(a => a.AlertStatus)
                            //.Include(a => a.AlertTypeID)
                            .Include(a => a.Login)
                            .Include(a => a.Login.Employees)
                            .Include(a => a.Login1)
                            .Include(a => a.Login1.Employees)
                            .Where(a => a.ToLoginID == loginID || a.FromLoginID == loginID/*(DbFunctions.TruncateTime(a.ExpiryDate.Value) > DbFunctions.TruncateTime(System.DateTime.Now) || DbFunctions.TruncateTime(a.ExpiryDate.Value) == DbFunctions.TruncateTime(System.DateTime.Now))*/)
                            .OrderByDescending(a => a.NotificationAlertIID)
                           .ToList();
                return entity;
            }
        }

        public int GetNotificationAlertsCount(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var alertStatus = db.Settings.FirstOrDefault(x => x.SettingCode == "ALERTSTATUS_ID").SettingValue;
                int? alertStatusID = int.Parse(alertStatus);

                var count = 0;

                //var count = db.NotificationAlerts
                //            .Where(a => a.ToLoginID == loginID && a.AlertStatusID == alertStatusID /*(DbFunctions.TruncateTime(a.ExpiryDate.Value) > DbFunctions.TruncateTime(System.DateTime.Now) || DbFunctions.TruncateTime(a.ExpiryDate.Value) == DbFunctions.TruncateTime(System.DateTime.Now))*/)
                //           .Count();

                try
                {
                    var notificationAlerts = db.NotificationAlerts.Where(a => a.ToLoginID == loginID && a.AlertStatusID == alertStatusID).ToList();

                    count = notificationAlerts != null ? notificationAlerts.Count() : 0;
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                    count = 0;
                }

                return count;
            }
        }

        public NotificationAlert GetAlert(long notificationAlertIID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var entity = db.NotificationAlerts
                            .Include(a => a.AlertStatus)
                            //.Include(a => a.AlertTypeID)
                            .Include(a => a.Login)
                            .Include(a => a.Login.Employees)
                            .Include(a => a.Login1)
                            .Include(a => a.Login1.Employees)
                            .Where(a => a.NotificationAlertIID == notificationAlertIID)
                           .FirstOrDefault();
                return entity;
            }
        }

        public NotificationAlert SaveAlerts(NotificationAlert alert)
        {
            try
            {
                using (var db = new dbEduegateERPContext())
                {
                    db.NotificationAlerts.Add(alert);
                    db.Entry(alert).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return alert;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotificationAlert> SaveAlerts(List<NotificationAlert> alerts)
        {
            try
            {
                using (var db = new dbEduegateERPContext())
                {
                    db.NotificationAlerts.AddRange(alerts);
                    foreach (var alert in alerts)
                    {
                        if (alert.NotificationAlertIID == 0)
                        {
                            db.Entry(alert).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            db.Entry(alert).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                    return alerts;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}