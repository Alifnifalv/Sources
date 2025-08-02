using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Notifications;

namespace Eduegate.Services
{
    public class Notifications : BaseService, INotifications
    {
        public List<NotificationQueueDTO> GetNextNotificationsFromQueue(Eduegate.Services.Contracts.Enums.NotificationTypes notificationType, int numberOfQueues)
        {
            try
            {
                var result = new NotificationBL(CallContext).GetNextNotificationsFromQueue(notificationType, numberOfQueues);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmailNotificationDTO GetEmailData(long notificationQueueID)
        {
            try
            {
                var result = new NotificationBL(CallContext).GetEmailData(notificationQueueID);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData)
        {
            try
            {
                var result = new NotificationBL(CallContext).SaveEmailData(emailData);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateNotificationStatus(long notificationQueueID, NotificationStatuses statusFrom, NotificationStatuses statusTo, string reason)
        {
            try
            {
                var result = new NotificationBL(CallContext).UpdateNotificationStatus(notificationQueueID, statusFrom, statusTo, reason);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public bool AddNotification(Contracts.Notifications.NotificationDTO notification)
        {
            try
            {
                var result = new NotificationBL(CallContext).AddNotification(notification);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool ProcessNotification(int notificationQueueID)
        {
            try
            {
                var result = new NotificationBL(CallContext).ProcessNotification(notificationQueueID);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public bool AddNotificationToProcess(NotificationQueueDTO notification)
        {
            try
            {
                var result = new NotificationBL(CallContext).AddNotificationToProcess(notification);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool RemoveNotificationFromQueue(int notificationQueueID)
        {
            try
            {
                var result = new NotificationBL(CallContext).RemoveNotificationFromQueue(notificationQueueID);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public SubscriptionStatus SubscribeStockEmailNotifications(StockNotificationDTO dto)
        {
            return new NotificationBL(CallContext).SubscribeStockEmailNotifications(dto);
        }

        public Contracts.NotificationDTO GetNotificationDetail(long notificationQueueID)
        {
            return new NotificationBL(CallContext).GetNotificationDetail(notificationQueueID);
        }

        public EmailNotificationTypeDTO GetEmailNotificationType(EmailNotificationTypes notificationType)
        {
            return new NotificationBL(CallContext).GetEmailNotificationType(notificationType);
        }

        public EmailNotificationTypeDTO GetEmailDetails(long ID)
        {
            try 
            {
                var emailDetails = new NotificationBL(CallContext).GetEmailDetails(ID);
                Eduegate.Logger.LogHelper<EmailNotificationTypeDTO>.Info("Service Result: " + emailDetails);
                return emailDetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<EmailNotificationTypeDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }

        }

        public EmailNotificationTypeDTO SaveEmailTemplates(EmailNotificationTypeDTO email)
        {
            try
            {
                EmailNotificationTypeDTO emailTemplate = new NotificationBL(CallContext).SaveEmailTemplates(email);
                Eduegate.Logger.LogHelper<EmailNotificationTypeDTO>.Info("Service Result: " + emailTemplate);
                return emailTemplate;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<EmailNotificationTypeDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public SMSNotificationDTO GetSMSData(long notificationQueueID)
        {
            try
            {
                var result = new NotificationBL(CallContext).GetSMSData(notificationQueueID);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public SMSNotificationDTO SaveSMSData(SMSNotificationDTO data)
        {
            try
            {
                var result = new NotificationBL(CallContext).SaveSMSData(data);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<NotificationAlertsDTO> GetAlerts(long loginID)
        {
            try
            {
                var result = new NotificationBL(CallContext).GetAlerts(loginID);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public NotificationAlertsDTO GetAlert(long notificationAlertIID)
        {
            try
            {
                var result = new NotificationBL(CallContext).GetAlert(notificationAlertIID);
                Eduegate.Logger.LogHelper<Notifications>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Notifications>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
