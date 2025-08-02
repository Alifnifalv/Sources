using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class EventMapper : DTOEntityDynamicMapper
    {
        public static EventMapper Mapper(CallContext context)
        {
            var mapper = new EventMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EventDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private EventDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Events.Where(a => a.EventIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new EventDTO()
                {
                    EventIID = entity.EventIID,
                    EventTitle = entity.EventTitle,
                    Description = entity.Description,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    IsThisAHoliday = entity.IsThisAHoliday,
                    IsEnableReminders = entity.IsEnableReminders,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EventDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Event()
            {
                EventIID = toDto.EventIID,
                EventTitle = toDto.EventTitle,
                Description = toDto.Description,
                StartDate = toDto.StartDate,
                EndDate = toDto.EndDate,
                IsThisAHoliday = toDto.IsThisAHoliday,
                IsEnableReminders = toDto.IsEnableReminders,
                CreatedBy = toDto.EventIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.EventIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.EventIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.EventIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.EventIID == 0)
                {
                    var maxGroupID = dbContext.Events.Max(a => (long?)a.EventIID);
                    entity.EventIID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Events.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Events.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.EventIID));
        }

    }
}