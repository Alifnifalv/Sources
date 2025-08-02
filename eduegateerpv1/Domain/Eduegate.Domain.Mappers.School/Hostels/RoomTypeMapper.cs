using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Hostels;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Hostels
{
    public class RoomTypeMapper : DTOEntityDynamicMapper
    {   
        public static  RoomTypeMapper Mapper(CallContext context)
        {
            var mapper = new  RoomTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RoomTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private RoomTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.RoomTypes.Where(x => x.RoomTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new RoomTypeDTO()
                {
                    RoomTypeID = entity.RoomTypeID,
                    Description = entity.Description,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RoomTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new RoomType()
            {
                RoomTypeID = toDto.RoomTypeID,
                Description = toDto.Description,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var repository = new EntityRepository<RoomType, dbEduegateSchoolContext>(dbContext);

                //if (entity.RoomTypeID == 0)
                //{
                //    var maxGroupID = repository.GetMaxID(a => a.RoomTypeID);
                //    entity.RoomTypeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                //    entity = repository.Insert(entity);
                //}
                //else
                //{
                //    entity = repository.Update(entity);
                //}
            }

            return ToDTOString(ToDTO(entity.RoomTypeID ));
        }       
    }
}