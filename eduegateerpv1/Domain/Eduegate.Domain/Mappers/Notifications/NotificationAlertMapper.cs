using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.Notifications
{
    public class NotificationAlertMapper : IDTOEntityMapper<NotificationAlertsDTO, NotificationAlert>
    {
        private CallContext _context;

        public static NotificationAlertMapper Mapper(CallContext context)
        {
            var mapper = new NotificationAlertMapper();
            mapper._context = context;
            return mapper;
        }

        public List<NotificationAlertsDTO> ToDTO(List<NotificationAlert> entities)
        {
            var dots = new List<NotificationAlertsDTO>();

            foreach (var entity in entities)
            {
                dots.Add(ToDTO(entity));
            }

            return dots;
        }

        public NotificationAlertsDTO ToDTO(NotificationAlert entity)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            return new NotificationAlertsDTO()
            {
                NotificationAlertIID = entity.NotificationAlertIID,
                AlertStatus = entity.AlertStatus?.StatusName,
                AlertStatusID = entity.AlertStatusID.HasValue ? entity.AlertStatusID : null,
                AlertTypeID = entity.AlertTypeID.HasValue ? entity.AlertTypeID : null,
                FromLogin = entity.Login?.LoginEmailID,
                ToLogin = entity.Login1?.LoginEmailID,
                FromLoginID = entity.FromLoginID,
                ReferenceScreenID = entity.ReferenceScreenID,
                Message = entity.Message,
                ToLoginID = entity.ToLoginID,
                ReferenceID = entity.ReferenceID,
                NotificationDate = entity.NotificationDate,
                NotificationDateString = entity.NotificationDate.HasValue ? entity.NotificationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                UpdatedBy = entity.UpdatedBy.HasValue ? Convert.ToInt32(entity.UpdatedBy) : (int?)null,
                CreatedBy = entity.CreatedBy.HasValue ? Convert.ToInt32(entity.CreatedBy) : (int?)null,
                CreatedDate = entity.CreatedDate,
                FromEmployeeName = entity.FromLoginID.HasValue ? entity.Login.Employees.Count > 0 ? entity.Login.Employees.Select(e =>e.EmployeeCode+" - "+e.FirstName+" "+e.MiddleName+" "+e.LastName).FirstOrDefault() : "IT department" : "IT department",
                UpdatedDate = entity.UpdatedDate,
            };
        }

        public List<NotificationAlert> ToEntity(List<NotificationAlertsDTO> dtos)
        {
            var entities = new List<NotificationAlert>();
            foreach (var dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }

            return entities;
        }

        public NotificationAlert ToEntity(NotificationAlertsDTO dto)
        {
            var entity = new NotificationAlert()
            {
                AlertStatusID = dto.AlertStatusID,
                AlertTypeID = dto.AlertTypeID,
                FromLoginID = dto.FromLoginID,
                ToLoginID = dto.ToLoginID,
                Message = dto.Message,
                ReferenceID = dto.ReferenceID,
                NotificationDate = dto.NotificationDate,
                NotificationAlertIID = dto.NotificationAlertIID,
            };

            if (!dto.CreatedBy.HasValue)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public List<NotificationAlertsDTO> GetNotificationAlerts(long loginID)
        {
            var alerts = new NotificationRepository().GetNotificationAlert(loginID);

            if (alerts.IsNotNull())
            {
                var dtos = ToDTO(alerts);
                return dtos;
            }
            else
            {
                return null;
            }
        }

        public List<NotificationAlertsDTO> GetAllNotificationAlerts(long loginID)
        {
            var alerts = new NotificationRepository().GetAllNotificationAlert(loginID);

            if (alerts.IsNotNull())
            {
                var dtos = ToDTO(alerts);
                return dtos;
            }
            else
            {
                return null;
            }
        }

        public List<MailBoxDTO> GetSendMailFromParent(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var mailBox = db.MailBoxes.Where(x => x.fromID == loginID).AsNoTracking().ToList();
                var getData = new List<MailBoxDTO>();
                foreach (var mails in mailBox)
                {
                    var employee = db.Employees.Where(e => e.LoginID == mails.toID).AsNoTracking().FirstOrDefault();
                    getData.Add(new MailBoxDTO()
                    {
                        mailSubject = mails.mailSubject,
                        mailBody = mails.mailBody,
                        onDate = mails.onDate,
                        SendTo = employee != null ? employee.FirstName+" "+employee.MiddleName+" "+employee.LastName : null,
                    });
                }
                return getData;
            }
        }

        public int GetNotificationAlertsCount(long loginID)
        {
            return new NotificationRepository().GetNotificationAlertsCount(loginID);
        }


        public string MarkNotificationAsRead(long loginID,long notificationAlertIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //Update Single AlertStatusID by AlertIID
                if (notificationAlertIID !=0 )
                {
                    var singleData = dbContext.NotificationAlerts.Where(x => x.NotificationAlertIID == notificationAlertIID).AsNoTracking().FirstOrDefault();
                    if (singleData != null)
                    {
                        singleData.AlertStatusID = 1;
                        singleData.UpdatedBy = (int)_context.LoginID;
                        singleData.UpdatedDate = DateTime.Now;
                    }
                    dbContext.Entry(singleData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
                //Update All AlertStatusID to Read by LoginID
                else if (loginID != 0)
                {
                    var notifications = dbContext.NotificationAlerts.Where(x => x.ToLoginID == loginID).AsNoTracking().ToList();
                    foreach (var dat in notifications)
                    {
                        dat.AlertStatusID = 1;
                        dat.UpdatedBy = (int)_context.LoginID;
                        dat.UpdatedDate = DateTime.Now;
                        dbContext.Entry(dat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }
            }
            return "success";
        }

    }
}