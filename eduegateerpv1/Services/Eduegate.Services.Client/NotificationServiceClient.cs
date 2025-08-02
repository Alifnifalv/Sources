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

namespace Eduegate.Service.Client
{
    public class NotificationServiceClient : BaseClient, INotifications
    {

        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string notificationService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.NOTIFICATION_SERVICE_NAME);

        public NotificationServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public EmailNotificationDTO GetEmailData(long notificationQueueID)
        {
            var uri = string.Format("{0}/{1}?notificationQueueID={2}", notificationService, "GetEmailData", notificationQueueID);
            return ServiceHelper.HttpGetRequest<EmailNotificationDTO>(uri, _callContext, _logger);
        }

        public SMSNotificationDTO GetSMSData(long notificationQueueID)
        {
            var uri = string.Format("{0}/{1}?notificationQueueID={2}", notificationService, "GetSMSData", notificationQueueID);
            return ServiceHelper.HttpGetRequest<SMSNotificationDTO>(uri, _callContext, _logger);
        }

        public List<NotificationQueueDTO> GetNextNotificationsFromQueue(NotificationTypes notificationType, int numberOfQueues)
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.NotificationDTO GetNotificationDetail(long notificationQueueID)
        {
            throw new NotImplementedException();
        }

        public SMSNotificationDTO SaveSMSData(SMSNotificationDTO data)
        {
            string serviceResult = ServiceHelper.HttpPostRequest(string.Concat(notificationService, "SaveSMSData"), data);
            return JsonConvert.DeserializeObject<SMSNotificationDTO>(serviceResult);
        }

        public EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData)
        {
            string serviceResult = ServiceHelper.HttpPostRequest(string.Concat(notificationService, "SaveEmailData"), emailData);
            return JsonConvert.DeserializeObject<EmailNotificationDTO>(serviceResult);
        }

        public SubscriptionStatus SubscribeStockEmailNotifications(StockNotificationDTO dto)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNotificationStatus(long notificationQueueID, NotificationStatuses statusFrom, NotificationStatuses statusTo, string reason)
        {
            throw new NotImplementedException();
        }

        public EmailNotificationTypeDTO GetEmailNotificationType(EmailNotificationTypes notificationType)
        {
            var uri = string.Format("{0}/{1}?notificationType={2}", notificationService, "GetEmailNotificationType", notificationType);
            return ServiceHelper.HttpGetRequest<EmailNotificationTypeDTO>(uri, _callContext, _logger);

        }

        public EmailNotificationTypeDTO GetEmailDetails(long Id)
        {
            var uri = string.Concat(notificationService, "GetEmailDetails?Id=" + Id);
            return ServiceHelper.HttpGetRequest<EmailNotificationTypeDTO>(uri, _callContext);
        }

        public EmailNotificationTypeDTO SaveEmailTemplates(EmailNotificationTypeDTO email)
        {
            var result = ServiceHelper.HttpPostRequest(notificationService + "SaveEmailTemplates", email, _callContext);
            return JsonConvert.DeserializeObject<EmailNotificationTypeDTO>(result);
        }

        public List<NotificationAlertsDTO> GetAlerts(long loginID)
        {
            var uri = string.Concat(notificationService, "GetAlerts?loginID=" + loginID);
            return ServiceHelper.HttpGetRequest<List<NotificationAlertsDTO>>(uri, _callContext);
        }

        public NotificationAlertsDTO GetAlert(long notificationAlertIID)
        {
            var uri = string.Concat(notificationService, "GetAlert?notificationAlertIID=" + notificationAlertIID);
            return ServiceHelper.HttpGetRequest<NotificationAlertsDTO>(uri, _callContext);
        }
    }
}
