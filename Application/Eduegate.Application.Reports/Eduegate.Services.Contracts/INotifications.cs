using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Notifications;
using Enums = Eduegate.Framework.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmailNotifications" in both code and config file together.
    [ServiceContract]
    public interface INotifications
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNextNotificationsFromQueue?notificationType={notificationType}&numberOfQueues={numberOfQueues}")]
        List<NotificationQueueDTO> GetNextNotificationsFromQueue(Eduegate.Services.Contracts.Enums.NotificationTypes notificationType, int numberOfQueues);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmailData?notificationQueueID={notificationQueueID}")]
        EmailNotificationDTO GetEmailData(long notificationQueueID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSMSData?notificationQueueID={notificationQueueID}")]
        SMSNotificationDTO GetSMSData(long notificationQueueID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveEmailData")]
        EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveSMSData")]
        SMSNotificationDTO SaveSMSData(SMSNotificationDTO data);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateNotificationStatus?notificationQueueID={notificationQueueID}&statusFrom={statusFrom}&statusTo={statusTo}&reason={reason}")]
        bool UpdateNotificationStatus(long notificationQueueID, NotificationStatuses statusFrom, NotificationStatuses statusTo, string reason);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNotificationDetail?notificationQueueID={notificationQueueID}")]
        NotificationDTO GetNotificationDetail(long notificationQueueID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmailNotificationType?notificationType={notificationType}")]
        EmailNotificationTypeDTO GetEmailNotificationType(EmailNotificationTypes notificationType);
        

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ProcessNotification?notificationQueueID={notificationQueueID}")]
        //bool ProcessNotification(int notificationQueueID);

            //[OperationContract]
            //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddNotification")]
            //bool AddNotification(NotificationDTO notification);

            //[OperationContract]
            //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddNotificationToProcess")]
            //bool AddNotificationToProcess(NotificationQueueDTO notification);

            //[OperationContract]
            //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RemoveNotificationFromQueue")]
            //bool RemoveNotificationFromQueue(int notificationQueueID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SubscribeStockEmailNotifications")]
        Eduegate.Services.Contracts.Enums.SubscriptionStatus SubscribeStockEmailNotifications(StockNotificationDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmailDetails?Id={Id}")]
        EmailNotificationTypeDTO GetEmailDetails(long Id); 

        [OperationContract] 
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveEmailTemplates")]
        EmailNotificationTypeDTO SaveEmailTemplates(EmailNotificationTypeDTO email);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAlerts?loginID={loginID}")]
        List<NotificationAlertsDTO> GetAlerts(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAlert?notificationAlertIID={notificationAlertIID}")]
        NotificationAlertsDTO GetAlert(long notificationAlertIID);
    }
}
