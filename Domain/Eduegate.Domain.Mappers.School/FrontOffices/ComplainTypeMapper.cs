using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.FrontOffices
{
    public class ComplainTypeMapper : DTOEntityDynamicMapper
    {   
        public static  ComplainTypeMapper Mapper(CallContext context)
        {
            var mapper = new  ComplainTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ComplainTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private ComplainTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ComplainTypes.Where(x => x.ComplainTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ComplainTypeDTO()
                {
                    ComplainTypeID = entity.ComplainTypeID,
                    ComplainDescription = entity.ComplainDescription,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ComplainTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ComplainType()
            {
                ComplainTypeID = toDto.ComplainTypeID,
                ComplainDescription = toDto.ComplainDescription,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                
                if (entity.ComplainTypeID == 0)
                {
                  
                    var maxGroupID = dbContext.ComplainTypes.Max(a => (byte?)a.ComplainTypeID);
                    entity.ComplainTypeID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.ComplainTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ComplainTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ComplainTypeID ));
        }       
    }
}




