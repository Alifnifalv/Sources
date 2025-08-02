using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers
{
    public class EmailNotificationTypeMapper : IDTOEntityMapper<EmailNotificationTypeDTO, EmailNotificationType>
    {

        public static EmailNotificationTypeMapper Mapper()
        {
            var mapper = new EmailNotificationTypeMapper();
            return mapper;
        }
        public EmailNotificationTypeDTO ToDTO(EmailNotificationType entity)
        {
            if (entity.IsNull())
                return null;

            return new EmailNotificationTypeDTO()
            {
                EmailNotificationTypeID = entity.EmailNotificationTypeID,
                EmailSubject = entity.EmailSubject,
                EmailTemplateFilePath = entity.EmailTemplateFilePath,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                ToCCEmailID = entity.ToCCEmailID,
                ToBCCEmailID = entity.ToBCCEmailID,
                Name = entity.Name,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                TimeStamp = entity.TimeStamp,
            };
        }

        public EmailNotificationType ToEntity(EmailNotificationTypeDTO dto)
        {
            return new EmailNotificationType()
            {
                EmailNotificationTypeID = dto.EmailNotificationTypeID,
                EmailSubject = dto.EmailSubject,
                EmailTemplateFilePath = dto.EmailTemplateFilePath,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                Description = dto.Description,
                Name = dto.Name,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate,
                TimeStamp = dto.TimeStamp,
                ToBCCEmailID = dto.ToBCCEmailID,
                ToCCEmailID = dto.ToCCEmailID
            };
        }
    }
}
