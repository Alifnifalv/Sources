using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Newtonsoft.Json;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework;
using Enums = Eduegate.Framework.Enums;
using Eduegate.Services.Contracts;
using System.Configuration;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Domain
{
    public class NotificationBL
    {
        private static NotificationRepository notificationRepo = new NotificationRepository();
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public NotificationBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }
        
        public string SendOTP(string mobileNumber)
        {
            var mobileNumberPrefix = new Domain.Setting.SettingBL().GetSettingValue<string>("DefaultMobilePrefix");
            var localnumberSize = new Domain.Setting.SettingBL().GetSettingValue<int>("DefaultLocalNumberSize");

            if (mobileNumber.Length == localnumberSize)
            {
                mobileNumber = mobileNumberPrefix + mobileNumber;
            }

            return Utilities.SMSProviders.SMSGlobal.SMSGlobalProvider.SendOTP(mobileNumber);
        }
        /// <summary>
        /// Get next notification and move to inprocess 
        /// </summary>
        /// <param name="notificationType"></param>
        /// <param name="numberOfQueues"></param>
        /// <returns></returns>
        public List<NotificationQueueDTO> GetNextNotificationsFromQueue(NotificationTypes notificationType, int numberOfQueues)
        {
            var notificationList = new List<NotificationQueueDTO>();

            // Get notifications from queue
            var notifications = notificationRepo.GetNextNotificationsFromQueue((Eduegate.Framework.Enums.NotificationTypes)notificationType, numberOfQueues);

            if (notifications.IsNotNull())
            {
                foreach (var notification in notifications)
                {
                    // Map entity to dto
                    var dto = new NotificationQueueDTO();
                    dto.NotificationTypeID = (NotificationTypes)notification.NotificationTypeID;
                    dto.EmailNotificationQueueID = notification.NotificationQueueIID;
                    dto.IsReprocess = notification.IsReprocess.HasValue ? notification.IsReprocess.Value : false;
                    notificationList.Add(dto);
                }
            }

            return notificationList;
        }

        public EmailNotificationDTO GetEmailData(long notificationQueueID)
        {
            return Mappers.Notifications.EmailNotificationDataMapper.Mapper(_callContext).ToDTO(notificationRepo.GetEmailData(notificationQueueID));
        }

        public SMSNotificationDTO GetSMSData(long notificationQueueID)
        {
            return Mappers.Notifications.SMSNotificationDataMapper.Mapper(_callContext).ToDTO(notificationRepo.GetSMSData(notificationQueueID));
        }

        public EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData)
        {
            var parentNotificationID = default(long);

            // If notification already exists
            if (emailData.NotificationQueueID.IsNotDefault() && emailData.IsReprocess == true)
            {
                parentNotificationID = notificationRepo.GetParentNotification(emailData.NotificationQueueID).ParentNotificationQueueID;
                emailData.NotificationQueueID = default(long);
            }

            //Add call context to the parameter collection
            if (_callContext.IsNotNull())
            {
                if (emailData.AdditionalParameters.IsNull())
                {
                    emailData.AdditionalParameters = new List<KeyValueParameterDTO>();
                }

                if (_callContext.CompanyID.HasValue)
                {
                    emailData.AdditionalParameters.Add(new KeyValueParameterDTO() { ParameterName = "CompanyID", ParameterValue = _callContext.CompanyID.Value.ToString() });
                }

                if (_callContext.SiteID.IsNotNull())
                {
                    emailData.AdditionalParameters.Add(new KeyValueParameterDTO() { ParameterName = "SiteID", ParameterValue = _callContext.SiteID });
                }
            }

            var emailMapper = Mappers.Notifications.EmailNotificationDataMapper.Mapper(_callContext);
            var updatedEntity = notificationRepo.SaveEmailData(emailMapper.ToEntity(emailData), parentNotificationID, emailData.IsReprocess);
            return emailMapper.ToDTO(updatedEntity);
        }

        public SMSNotificationDTO SaveSMSData(SMSNotificationDTO data)
        {
            var parentNotificationID = default(long);

            // If notification already exists
            if (data.NotificationQueueID.IsNotDefault() && data.IsReprocess == true)
            {
                parentNotificationID = notificationRepo.GetParentNotification(data.NotificationQueueID).ParentNotificationQueueID;
                data.NotificationQueueID = default(long);
            }

            var mapper = Mappers.Notifications.SMSNotificationDataMapper.Mapper(_callContext);
            var updatedEntity = notificationRepo.SaveSMSData(mapper.ToEntity(data), parentNotificationID, data.IsReprocess);
            return mapper.ToDTO(updatedEntity);
        }

        public bool UpdateNotificationStatus(long notificationQueueID, NotificationStatuses statusFrom, NotificationStatuses statusTo, string reason = null)
        {
            try
            {
                switch (statusFrom)
                {
                    case NotificationStatuses.InProcess:
                        notificationRepo.UpdateNotificationFromInprocess(notificationQueueID, (int)statusTo, reason);
                        break;
                }

                // To update notification parent table
                switch (statusTo)
                {
                    case NotificationStatuses.Failed:
                        this.UpdateNotificationParentMapStatus(notificationQueueID, Enums.NotificationParentStatuses.Fail);
                        break;
                    case NotificationStatuses.Completed:
                        this.UpdateNotificationParentMapStatus(notificationQueueID, Enums.NotificationParentStatuses.Success);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateNotificationParentMapStatus(long notificationQueueID, Enums.NotificationParentStatuses statusTo)
        {
           return notificationRepo.UpdateNotificationParentMap(notificationQueueID, Convert.ToBoolean(statusTo));
        }

        /// <summary>
        /// Add notification to queue and metadata
        /// </summary>
        /// <param name="notificationDTO">EmailNotificationDTO</param>
        /// <returns>bool</returns>
        public bool AddNotification(Services.Contracts.Notifications.NotificationDTO notificationDTO)
        {
            var exit = false;

            //Create notification queue
            var notification = new NotificationsQueue();
            notification.NotificationTypeID = (int)notificationDTO.NotificationType;
            notification.CreatedDate = DateTime.Now;
            exit = notificationRepo.AddNotification(notification, JsonConvert.SerializeObject(notificationDTO));

            return exit;
        }

        public bool AddNotificationToProcess(NotificationQueueDTO notificationDTO)
        {
            var exit = false;

            //Create notification queue
            var notification = new NotificationsProcessing();
            notification.NotificationQueueID = notificationDTO.EmailNotificationQueueID;
            notification.NotificationTypeID = (int)notificationDTO.NotificationTypeID;
            notification.CreatedDate = DateTime.Now;
            //notification.Status = (int)NotificationStatuses.InProcess;

            exit = notificationRepo.AddNotificationToProcess(notification);
            return exit;
        }

        public Services.Contracts.NotificationDTO GetNotificationDetail(long notificationQueueID)
        {
          return  notificationRepo.GetNotificationDetail(notificationQueueID);
        }

        public bool RemoveNotificationFromQueue(int emailNotificationQueueID)
        {
            return notificationRepo.RemoveNotificationFromQueue(emailNotificationQueueID);
        }

        /// <summary>
        /// Add notification data to metadata
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns>bool</returns>
        public bool AddEmailMetaData(EmailNotificationData metadata)
        {
            var exit = false;
            exit = notificationRepo.AddEmailMetaData(metadata);
            return exit;
        }

        public bool ProcessNotification(int notificationQueueID)
        {
            var exit = false;
            // Get notification from queue
            var queue = notificationRepo.GetNotificationFromQueue(notificationQueueID);

            if (queue.IsNotNull())
            {
                // Create processing notification
                var processingNotification = new NotificationsProcessing();
                processingNotification.NotificationQueueID = queue.NotificationQueueIID;
                processingNotification.NotificationTypeID = queue.NotificationTypeID;
                //processingNotification.Status = (int)NotificationStatuses.InProcess;
                processingNotification.CreatedDate = DateTime.Now;

                // Add to processing + Remove from queue
                exit = notificationRepo.ProcessNotification(processingNotification);
            }
            return exit;
        }

        public EmailNotificationTypeDTO GetEmailNotificationType(EmailNotificationTypes notificationType)
        {
            //return notificationRepo.GetEmailNotificationType((int)notificationType);
            return Eduegate.Domain.Mappers.EmailNotificationTypeMapper.Mapper().ToDTO(new NotificationRepository().GetEmailNotificationType((int)notificationType));
        }

        /// <summary>
        /// Add email notification in Queue
        /// </summary>
        /// <param name="EmailNotificationDTO"></param>
        /// <returns>bool</returns>
        //public bool AddNotification(EmailNotificationDTO notificationDTO)
        //{
        //    var exit = false;

        //    var notification = new EmailNotificationsQueue();
        //    notification.NotificationTypeID = (int)notificationDTO.NotificationType;

        //    // Add notification in queue
        //    notification = notificationRepo.AddNotification(notification);
        //    if (notification.EmailNotificationQueueIID > 0)
        //    {
        //        //Create Metadata object
        //        var notificationMetaData = new EmailMetaData();
        //        notificationMetaData.NotificationQueueID = notification.EmailNotificationQueueIID;
        //        notificationMetaData.EmailData = JsonConvert.SerializeObject(notificationDTO);

        //        //Add metadata for the queue id
        //        notificationRepo.AddEmailMetaData(notificationMetaData);
        //        exit = true;
        //    }
        //    return exit;
        //}
        
        public SubscriptionStatus SubscribeStockEmailNotifications(StockNotificationDTO dto)
        {
            bool isSuccess =  notificationRepo.IsStockNotificationSubscribed((long)dto.ProductSKUMapID, dto.EmailID);

            StockNotification entity = new StockNotification();
            entity.ProductSKUMapID = dto.ProductSKUMapID;
            entity.EmailID = dto.EmailID;
            if (_callContext.UserId.IsNotNull())
            {
                entity.LoginID = Convert.ToInt64(_callContext.LoginID);
            }
            entity.NotficationStatusID = (int)Enums.StockNotificationStatuses.Subscribed;

            if (isSuccess)
            {
                //modify StockNotification
                isSuccess = notificationRepo.ModifyStockNotification(entity);
                if (!isSuccess)
                {
                    return SubscriptionStatus.Failed;
                }
                return SubscriptionStatus.Updated;
            }
            else
            {
                //add StockNotification
                isSuccess = notificationRepo.AddStockNotification(entity);
                if (!isSuccess)
                {
                    return SubscriptionStatus.Failed;
                }
                return SubscriptionStatus.Inserted;
            }
        }
        
        public EmailNotificationTypeDTO GetEmailDetails(long Id)
        { 
            var email = new NotificationRepository().GetEmailDetails(Id);
            if (email.IsNotNull())
            {
                var emailDTO = new EmailNotificationTypeDTO();
                emailDTO = Eduegate.Domain.Mappers.EmailNotificationTypeMapper.Mapper().ToDTO(email);
                return emailDTO;
            }
            else
            {
                return null;
            }
        }

        public EmailNotificationTypeDTO SaveEmailTemplates(EmailNotificationTypeDTO dto)
        {
            var email = new EmailNotificationType();
            email = Eduegate.Domain.Mappers.EmailNotificationTypeMapper.Mapper().ToEntity(dto);
            if (dto.IsNotNull())
            {
               var emailTemplate = new NotificationRepository().SaveEmailTemplates(email);
            }
            return null;
        }

        public List<NotificationAlertsDTO> GetAlerts(long loginID)
        {
            var alerts = new NotificationRepository().GetAlerts(loginID);

            if (alerts.IsNotNull())
            {
                var dtos = Mappers.Notifications.NotificationAlertMapper.Mapper(_callContext).ToDTO(alerts);
                return dtos;
            }
            else
            {
                return null;
            }
        }

        public NotificationAlertsDTO GetAlert(long notificationAlertIID)
        {
            var alerts = new NotificationRepository().GetAlert(notificationAlertIID);

            if (alerts.IsNotNull())
            {
                var dtos = Mappers.Notifications.NotificationAlertMapper.Mapper(_callContext).ToDTO(alerts);
                return dtos;
            }
            else
            {
                return null;
            }
        }

        public NotificationAlertsDTO SaveNotificationAlerts(NotificationAlertsDTO dto)
        {
            var mapper = Eduegate.Domain.Mappers.Notifications.NotificationAlertMapper.Mapper(_callContext);
            var alert = mapper.ToEntity(dto);
            if (dto.IsNotNull())
            {
                var modifeidEntity = new NotificationRepository().SaveAlerts(alert);
                return mapper.ToDTO(modifeidEntity);
            }

            return null;
        }

        public void SaveNotificationAlerts(List<NotificationAlertsDTO> dto)
        {
            var mapper = Eduegate.Domain.Mappers.Notifications.NotificationAlertMapper.Mapper(_callContext);
            var alert = mapper.ToEntity(dto);
            if (dto.IsNotNull())
            {
                var modifeidEntity = new NotificationRepository().SaveAlerts(alert);
                //return mapper.ToDTO(modifeidEntity);
            }

            //return null;
        }
    }
}
