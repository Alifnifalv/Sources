using Newtonsoft.Json;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using System;
using System.Linq;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Communications;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.Enums;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers
{
    public class CommunicationMapper : DTOEntityDynamicMapper
    {
        public static CommunicationMapper Mapper(CallContext context)
        {
            var mapper = new CommunicationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CommunicationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CommunicationDTO ToDTO(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Communications.Where(X => X.CommunicationIID == IID).AsNoTracking().FirstOrDefault();

                var communicationDTO = new CommunicationDTO()
                {
                    CommunicationIID = entity.CommunicationIID,
                    ReferenceID = entity.LeadID,
                    FromEmail = entity.FromEmail,
                    ToEmail = entity.Email,
                    Subject = entity.Notes,
                    EmailContent = entity.EmailContent,
                    MobileNumber = entity.MobileNumber,
                    FollowUpDate = entity.FollowUpDate,
                    EmailTemplateID = entity.EmailTemplateID,
                    CommunicationTypeID = entity.CommunicationTypeID,
                };

                return communicationDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CommunicationDTO;
            string emailBody = "";
            string emailSubject = "";

            //convert the dto to entity and pass to the repository.
            var entity = new Communication()
            {
                CommunicationIID = toDto.CommunicationIID,
                LeadID = toDto.ReferenceID,
                FromEmail = toDto.FromEmail,
                Email = toDto.ToEmail,
                Notes = toDto.Subject,
                EmailContent = toDto.EmailContent,
                MobileNumber = toDto.MobileNumber,
                EmailTemplateID = toDto.EmailTemplateID,
                FollowUpDate = toDto.FollowUpDate,
                CommunicationTypeID = toDto.CommunicationTypeID,
                CommunicationDate = DateTime.Now,
            };

            if (toDto.CommunicationIID == 0)
            {
                entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                entity.CreatedDate = DateTime.Now;
            }
            else
            {
                entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                entity.UpdatedDate = DateTime.Now;
            }

            string schoolShortName = string.Empty;
            if (toDto.SchoolID.HasValue)
            {
                var schoolData = new Eduegate.Domain.Setting.SettingBL(_context).GetSchoolDetailByID(toDto.SchoolID.Value);

                schoolShortName = schoolData?.SchoolShortName?.ToLower();
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                dbContext.Communications.Add(entity);

                if (entity.CommunicationIID == 0 && entity.CommunicationTypeID != null)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            if (entity.CommunicationIID != 0 && entity.CommunicationTypeID == 1)
            {
                emailBody = @"<br/><p style='font-family:Helvetica;font-size:1rem; font-weight:bold;'>"+ entity.EmailContent + "</p><br/><br/><br/><p style='font-size:0.7rem;'<b>Please Note : </b>do not reply to this email as it is a computer generated email</p>";
                emailSubject = entity.Notes;
            }

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toDto.ToEmail, emailBody);

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            var mailParameters = new Dictionary<string, string>()
            {
                { "SCHOOL_SHORT_NAME", schoolShortName},
            };

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendFromCustomerMail(toDto.ToEmail, toDto.FromEmail, emailSubject, mailMessage, EmailTypes.ParentCommunication, mailParameters);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendFromCustomerMail(defaultMail, toDto.FromEmail, emailSubject, mailMessage, EmailTypes.ParentCommunication, mailParameters);
                }
            }

            return ToDTOString(ToDTO(entity.CommunicationIID));
        }

        public CommunicationDTO GetMailIDDetailsFromLead(long? leadID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var leadEntity = dbContext.Leads.Where(X => X.LeadIID == leadID).AsNoTracking().FirstOrDefault();

                var loginEntity = _context != null ? _context.LoginID.HasValue ? dbContext.Logins.Where(X => X.LoginIID == _context.LoginID).AsNoTracking().FirstOrDefault() : null : null;

                var communicationDTO = new CommunicationDTO()
                {
                    FromEmail = loginEntity != null ? loginEntity.LoginEmailID : null,
                    ToEmail = leadEntity.EmailAddress,
                    MobileNumber = leadEntity.MobileNumber,
                };

                return communicationDTO;
            }
        }

        public CommunicationDTO GetEmailTemplateByID(int? templateID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var emailTemplate = dbContext.EmailTemplates.Where(X => X.EmailTemplateID == templateID).AsNoTracking().FirstOrDefault();

                var loginEntity = _context != null ? _context.LoginID.HasValue ? dbContext.Logins.Where(X => X.LoginIID == _context.LoginID).AsNoTracking().FirstOrDefault() : null : null;

                var communicationDTO = new CommunicationDTO()
                {
                    EmailTemplateID = emailTemplate.EmailTemplateID,
                    EmailTemplate = emailTemplate.Template,
                };

                return communicationDTO;
            }
        }

    }
}