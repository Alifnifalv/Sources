using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using System;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class SchoolEventsMapper : DTOEntityDynamicMapper
    {
        public static SchoolEventsMapper Mapper(CallContext context)
        {
            var mapper = new SchoolEventsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SchoolEventsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SchoolEventsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SchoolEvents.AsNoTracking().FirstOrDefault(x => x.SchoolEventIID == IID);

                var toDTO = new SchoolEventsDTO()
                {
                    SchoolEventIID = entity.SchoolEventIID,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    Destination = entity.Destination,
                    Description = entity.Description,
                    EventName = entity.EventName,
                    EventDate = entity.EventDate,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                return toDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SchoolEventsDTO;

            var entity = new SchoolEvent()
            {
                SchoolEventIID = toDto.SchoolEventIID,
                EventName = toDto.EventName,
                EventDate = toDto.EventDate,
                Description = toDto.Description,
                Destination = toDto.Destination,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.SchoolEventIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SchoolEventIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SchoolEventIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SchoolEventIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if(toDto.SchoolEventIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.SchoolEventIID));
        }

    }
}