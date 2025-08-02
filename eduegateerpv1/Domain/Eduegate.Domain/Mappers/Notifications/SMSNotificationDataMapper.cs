using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Enums.Notifications;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Domain.Entity.Models.Notification;

namespace Eduegate.Domain.Mappers.Notifications
{
    public class SMSNotificationDataMapper : IDTOEntityMapper<SMSNotificationDTO, SMSNotificationData>
    {
        private CallContext _context;

        public static SMSNotificationDataMapper Mapper(CallContext context)
        {
            var mapper = new SMSNotificationDataMapper();
            mapper._context = context;
            return mapper;
        }

        public SMSNotificationDTO ToDTO(SMSNotificationData entity)
        {
            var sms = new SMSNotificationDTO()
            {
                NotificationQueueID = entity.NotificationQueueID,
                SMSMetaDataIID = entity.SMSNotificationDataIID,
                ToMobileNumbers = entity.ToMobileNumber,
                TemplateName = string.IsNullOrEmpty(entity.TemplateName) ? (entity.SMSNotificationType.IsNotNull() ? entity.SMSNotificationType.TemplateFilePath : entity.TemplateName) : entity.TemplateName,
                Subject = entity.Subject,
                FromMobileNumber = entity.FromMobileNumber,
                ToBCCMobileNumber = entity.SMSNotificationType.IsNotNull() ? entity.SMSNotificationType.ToBCC : string.Empty,
                ToCCMobileNumber = entity.SMSNotificationType.IsNotNull() ? entity.SMSNotificationType.ToBCC : string.Empty,
                NotificationType = (SMSNotificationTypes)entity.SMSNotificationTypeID,
                AdditionalParameters = JsonConvert.DeserializeObject<List<KeyValueParameterDTO>>(entity.SerializedJsonParameters),
                Content = entity.SMSContent.IsNotNullOrEmpty() ? entity.SMSContent : null,
            };

            if (_context.IsNotNull())
            {
                if (sms.AdditionalParameters.IsNull())
                {
                    sms.AdditionalParameters = new List<KeyValueParameterDTO>();
                }

                if (_context.CompanyID.HasValue)
                {
                    sms.AdditionalParameters.Add(new KeyValueParameterDTO() { ParameterName = "CompanyID", ParameterValue = _context.CompanyID.Value.ToString() });
                }

                if (_context.SiteID.IsNotNull())
                {
                    sms.AdditionalParameters.Add(new KeyValueParameterDTO() { ParameterName = "SiteID", ParameterValue = _context.SiteID });
                }
            }

            return sms;
        }

        public SMSNotificationData ToEntity(SMSNotificationDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
