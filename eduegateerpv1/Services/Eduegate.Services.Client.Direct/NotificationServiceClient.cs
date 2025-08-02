using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class NotificationServiceClient : BaseClient, INotifications
    {
        Notifications service = new Notifications();

        public NotificationServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
            service.CallContext = context;
        }

        public EmailNotificationDTO GetEmailData(long notificationQueueID)
        {
            return service.GetEmailData(notificationQueueID);
        }

        public SMSNotificationDTO GetSMSData(long notificationQueueID)
        {
            return service.GetSMSData(notificationQueueID);
        }

        public List<NotificationQueueDTO> GetNextNotificationsFromQueue(NotificationTypes notificationType, int numberOfQueues)
        {
            return service.GetNextNotificationsFromQueue(notificationType, numberOfQueues);
        }

        public Services.Contracts.NotificationDTO GetNotificationDetail(long notificationQueueID)
        {
            return service.GetNotificationDetail(notificationQueueID);
        }

        public SMSNotificationDTO SaveSMSData(SMSNotificationDTO data)
        {
            return service.SaveSMSData(data);
        }

        public EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData)
        {
            return service.SaveEmailData(emailData);
        }

        public SubscriptionStatus SubscribeStockEmailNotifications(StockNotificationDTO dto)
        {
            return service.SubscribeStockEmailNotifications(dto); 
        }

        public bool UpdateNotificationStatus(long notificationQueueID, NotificationStatuses statusFrom, NotificationStatuses statusTo, string reason)
        {
            return service.UpdateNotificationStatus(notificationQueueID, statusFrom, statusTo, reason);
        }

        public EmailNotificationTypeDTO GetEmailNotificationType(EmailNotificationTypes notificationType)
        {
            return service.GetEmailNotificationType(notificationType);
        }

        public EmailNotificationTypeDTO GetEmailDetails(long Id)
        {
            return service.GetEmailDetails(Id);
        }

        public EmailNotificationTypeDTO SaveEmailTemplates(EmailNotificationTypeDTO email)
        {
            return service.SaveEmailTemplates(email);
        }

        public List<NotificationAlertsDTO> GetAlerts(long loginID)
        {
            return service.GetAlerts(loginID);
        }

        public NotificationAlertsDTO GetAlert(long notificationAlertIID)
        {
            return service.GetAlert(notificationAlertIID);
        }
    }
}
