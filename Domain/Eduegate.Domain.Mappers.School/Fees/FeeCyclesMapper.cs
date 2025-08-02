using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeCyclesMapper: DTOEntityDynamicMapper
    {
        public static FeeCyclesMapper Mapper(CallContext context)
        {
            var mapper = new FeeCyclesMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeCyclesDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FeeCyclesDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeeCycles.Where(x => x.FeeCycleID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new FeeCyclesDTO()
                {
                    FeeCycleID = entity.FeeCycleID,
                    Cycle = entity.Cycle,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeCyclesDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FeeCycle()
            {
                FeeCycleID = toDto.FeeCycleID,
                Cycle = toDto.Cycle,
                CreatedBy = toDto.FeeCycleID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FeeCycleID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FeeCycleID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FeeCycleID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.FeeCycleID == 0)
                {
                    var maxGroupID = dbContext.FeeCycles.Max(a => (byte?)a.FeeCycleID);                    
                    entity.FeeCycleID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.FeeCycles.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeeCycles.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.FeeCycleID));
        }
    }
}