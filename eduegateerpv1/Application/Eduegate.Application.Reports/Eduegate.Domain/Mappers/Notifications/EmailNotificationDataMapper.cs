using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Framework.Extensions;
using Newtonsoft.Json;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Domain.Mappers.Notifications
{
    public class EmailNotificationDataMapper : IDTOEntityMapper<EmailNotificationDTO, EmailNotificationData>
    {
        private CallContext _context;

        public static EmailNotificationDataMapper Mapper(CallContext context)
        {
            var mapper = new EmailNotificationDataMapper();
            mapper._context = context;
            return mapper;
        }

        public EmailNotificationDTO ToDTO(EmailNotificationData entity)
        {
            return new EmailNotificationDTO()
            {
                NotificationQueueID = entity.NotificationQueueID,
                EmailMetaDataIID = entity.EmailMetaDataIID,
                ToEmailID = entity.ToEmailID,
                TemplateName = string.IsNullOrEmpty(entity.TemplateName) ? (entity.EmailNotificationType.IsNotNull() ? entity.EmailNotificationType.EmailTemplateFilePath : entity.TemplateName) : entity.TemplateName,
                Subject = entity.Subject,
                FromEmailID = entity.FromEmailID,
                ToBCCEmailID = entity.EmailNotificationType.IsNotNull() ? entity.EmailNotificationType.ToBCCEmailID : string.Empty,
                ToCCEmailID = entity.EmailNotificationType.IsNotNull() ? entity.EmailNotificationType.ToCCEmailID : string.Empty,
                EmailNotificationType = (Eduegate.Services.Contracts.Enums.EmailNotificationTypes)entity.EmailNotificationTypeID,
                AdditionalParameters = JsonConvert.DeserializeObject<List<KeyValueParameterDTO>>(entity.SerializedJsonParameters),
                EmailData = entity.EmailData.IsNotNullOrEmpty() ? entity.EmailData : null,
            };
        }

        public EmailNotificationData ToEntity(EmailNotificationDTO dto)
        {

            var notificationType = new NotificationBL(_context ?? _context).GetEmailNotificationType(dto.EmailNotificationType);

            var notifyData = new EmailNotificationData()
            {
                EmailMetaDataIID = dto.EmailMetaDataIID,
                ToEmailID = dto.ToEmailID,
                TemplateName = notificationType.IsNotNull() ? notificationType.EmailTemplateFilePath : String.Empty,
                Subject = dto.Subject.IsNotNullOrEmpty() ? dto.Subject : (notificationType.IsNotNull() ? notificationType.EmailSubject : String.Empty),
                FromEmailID = dto.FromEmailID,
                EmailNotificationTypeID = (int)dto.EmailNotificationType,
                SerializedJsonParameters = JsonConvert.SerializeObject(dto.AdditionalParameters),
                //UpdatedBy = _callContext.LoginID,
                UpdatedDate = DateTime.Now,
                EmailData = dto.EmailData.IsNotNullOrEmpty() ? dto.EmailData : null,
            };

            if (dto.EmailMetaDataIID == 0)
            {
                notifyData.CreatedDate = DateTime.Now;
                //notifyData.UpdatedBy = _callContext.LoginID;
            }

            return notifyData;
        }

        //public EmailNotificationData ToEntity(EmailNotificationDTO dto)
        //{
        //    var notifyData = new EmailNotificationData()
        //    {
        //        EmailMetaDataIID = dto.EmailMetaDataIID,
        //        ToEmailID = dto.ToEmailID,
        //        TemplateName = dto.TemplateName,
        //        Subject = dto.Subject.IsNotNullOrEmpty() ? dto.Subject :  String.Empty,
        //        FromEmailID = dto.FromEmailID,
        //        EmailNotificationTypeID = (int)dto.EmailNotificationType,
        //        SerializedJsonParameters = JsonConvert.SerializeObject(dto.AdditionalParameters),
        //        //UpdatedBy = _callContext.LoginID,
        //        UpdatedDate = DateTime.Now,
        //        EmailData = dto.EmailData.IsNotNullOrEmpty() ? dto.EmailData : null,
        //    };

        //    if (dto.EmailMetaDataIID == 0)
        //    {
        //        notifyData.CreatedDate = DateTime.Now;
        //        //notifyData.UpdatedBy = _callContext.LoginID;
        //    }

        //    return notifyData;
        //}
    }
}
