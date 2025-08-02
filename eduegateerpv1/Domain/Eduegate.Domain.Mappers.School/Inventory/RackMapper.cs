using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Inventory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Inventory
{
    public class RackMapper : DTOEntityDynamicMapper
    {
        public static RackMapper Mapper(CallContext context)
        {
            var mapper = new RackMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RackDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public RackDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Racks.Where(x => x.RackIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new RackDTO()
                {
                    RackID = entity.RackIID,
                    RackName = entity.RackName,
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
            var toDto = dto as RackDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Rack()
            {
                RackIID = toDto.RackID,
                RackName = toDto.RackName,
                CreatedBy = toDto.RackID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.RackID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.RackID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.RackID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.RackIID == 0)
                {  
                    var maxGroupID = dbContext.Racks.Max(a => (long?)a.RackIID);
                    entity.RackIID = Convert.ToByte(maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1);
                    dbContext.Racks.Add(entity);
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Racks.Add(entity);
                    dbContext.Entry(entity).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new RackDTO()
            {
                RackID = entity.RackIID,
                RackName = entity.RackName,
            });
        }
    }
}