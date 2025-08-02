using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Hostel;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Hostels
{
    public class HostelTypeMapper : DTOEntityDynamicMapper
    {   
        public static  HostelTypeMapper Mapper(CallContext context)
        {
            var mapper = new  HostelTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<HostelTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private HostelTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.HostelTypes.Where(x => x.HostelTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new HostelTypeDTO()
                {
                    HostelTypeID = entity.HostelTypeID,
                    TypeName = entity.TypeName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as HostelTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new HostelType()
            {
                HostelTypeID = toDto.HostelTypeID,
                TypeName = toDto.TypeName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var repository = new EntityRepository<HostelType, dbEduegateSchoolContext>(dbContext);

                //if (entity.HostelTypeID == 0)
                //{
                //    var maxGroupID = repository.GetMaxID(a => a.HostelTypeID);
                //    entity.HostelTypeID = Convert.ToByte(maxGroupID == null ? 1 : Convert.ToByte(maxGroupID.ToString()) + 1);
                //    entity = repository.Insert(entity);
                //}
                //else
                //{
                //    entity = repository.Update(entity);
                //}
            }

            return ToDTOString(ToDTO(entity.HostelTypeID ));
        }       
    }
}