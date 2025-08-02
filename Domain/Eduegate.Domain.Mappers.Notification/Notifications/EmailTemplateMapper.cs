using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using Eduegate.Services.Contracts.Notifications;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Models.Notification;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Mappers.Notification.Notifications
{
    public class EmailTemplateMapper : DTOEntityDynamicMapper
    {
        public static EmailTemplateMapper Mapper(CallContext context)
        {
            var mapper = new EmailTemplateMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<NotificationEmailTemplateDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private NotificationEmailTemplateDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.EmailTemplates2.Where(X => X.EmailTemplateID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private NotificationEmailTemplateDTO ToDTO(EmailTemplate2 entity)
        {
            var templateDTO = new NotificationEmailTemplateDTO()
            {
                EmailTemplateID = entity.EmailTemplateID,
                TemplateName = entity.TemplateName,
                Subject = entity.Subject,
                EmailTemplate = entity.EmailTemplate,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy
            };

            return templateDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as NotificationEmailTemplateDTO;

            if (string.IsNullOrEmpty(toDto.TemplateName))
            {
                throw new Exception("Template Name is Required!");
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new EmailTemplate2()
                {
                    EmailTemplateID = toDto.EmailTemplateID,
                    TemplateName = toDto.TemplateName,
                    Subject = toDto.Subject,
                    EmailTemplate = toDto.EmailTemplate,
                    CreatedBy = toDto.EmailTemplateID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.EmailTemplateID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.EmailTemplateID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.EmailTemplateID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.EmailTemplates2.Add(entity);

                if (entity.EmailTemplateID == 0)
                {
                    var maxID = dbContext.EmailTemplates2.AsNoTracking().Max(a => (long?)a.EmailTemplateID);
                    entity.EmailTemplateID = maxID != null ? (int)maxID + 1 : 1;

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.EmailTemplateID));
            }
        }

        public NotificationEmailTemplateDTO GetEmailTemplateDetails(string temPlateName)
        {
            var notificationDTO = new NotificationEmailTemplateDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.EmailTemplates2.Where(x => x.TemplateName == temPlateName)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    notificationDTO = ToDTO(entity);
                }
            }

            return notificationDTO;
        }

    }
}