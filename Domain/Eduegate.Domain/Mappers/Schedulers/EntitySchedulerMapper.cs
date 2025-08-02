using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Domain.Mappers.Schedulers
{
    public class EntitySchedulerMapper : IDTOEntityMapper<SchedulerDTO, EntityScheduler>
    {
        private CallContext _context;

        public static EntitySchedulerMapper Mapper(CallContext context)
        {
            var mapper = new EntitySchedulerMapper();
            mapper._context = context;
            return mapper;
        }

        public SchedulerDTO ToDTO(EntityScheduler entity)
        {
            if (entity == null) return null;

            return new SchedulerDTO()
            {
                EntitySchedulerIID = entity.EntitySchedulerIID,
                SchedulerType = (Services.Contracts.Enums.Schedulers.SchedulerTypes) entity.SchedulerTypeID,
                SchedulerEntityType = (Services.Contracts.Enums.Schedulers.SchedulerEntityTypes) entity.SchedulerEntityTypeID,
                EntityValue = entity.EntityValue,
                EntityID = entity.EntityID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public List<SchedulerDTO> ToDTO(List<EntityScheduler> entities)
        {
            var schedulers = new List<SchedulerDTO>();

            foreach (var entity in entities)
            {
                schedulers.Add(ToDTO(entity));
            }

            return schedulers;
        }

        public EntityScheduler ToEntity(SchedulerDTO dto)
        {
            var entity = new EntityScheduler()
            {
                EntitySchedulerIID = dto.EntitySchedulerIID,
                EntityValue = dto.EntityValue,
                EntityID = dto.EntityID,
                SchedulerEntityTypeID = (int)dto.SchedulerEntityType,
                SchedulerTypeID = (int)dto.SchedulerType,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(_context.LoginID.Value.ToString()),
                UpdatedDate = DateTime.Now,
            };

            if (dto.EntitySchedulerIID == 0)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public List<EntityScheduler> ToEntity(List<SchedulerDTO> dtos)
        {
            var schedulers = new List<EntityScheduler>();

            foreach (var dto in dtos)
            {
                schedulers.Add(ToEntity(dto));
            }

            return schedulers;
        }
    }
}
