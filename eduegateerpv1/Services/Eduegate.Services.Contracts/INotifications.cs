using System.Collections.Generic;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Notifications;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmailNotifications" in both code and config file together.
    public interface INotifications
    {
        List<NotificationQueueDTO> GetNextNotificationsFromQueue(Eduegate.Services.Contracts.Enums.NotificationTypes notificationType, int numberOfQueues);

        EmailNotificationDTO GetEmailData(long notificationQueueID);

        SMSNotificationDTO GetSMSData(long notificationQueueID);

        EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData);

        SMSNotificationDTO SaveSMSData(SMSNotificationDTO data);

        bool UpdateNotificationStatus(long notificationQueueID, NotificationStatuses statusFrom, NotificationStatuses statusTo, string reason);

        NotificationDTO GetNotificationDetail(long notificationQueueID);

        EmailNotificationTypeDTO GetEmailNotificationType(EmailNotificationTypes notificationType);

        //bool ProcessNotification(int notificationQueueID);

        //bool AddNotification(NotificationDTO notification);
        
        //bool AddNotificationToProcess(NotificationQueueDTO notification);

        //bool RemoveNotificationFromQueue(int notificationQueueID);

        Eduegate.Services.Contracts.Enums.SubscriptionStatus SubscribeStockEmailNotifications(StockNotificationDTO dto);

        EmailNotificationTypeDTO GetEmailDetails(long Id); 

        EmailNotificationTypeDTO SaveEmailTemplates(EmailNotificationTypeDTO email);

        List<NotificationAlertsDTO> GetAlerts(long loginID);

        NotificationAlertsDTO GetAlert(long notificationAlertIID);
    }
}