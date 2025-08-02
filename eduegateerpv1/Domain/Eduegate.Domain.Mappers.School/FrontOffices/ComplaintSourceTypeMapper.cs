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
    public class ComplaintSourceTypeMapper : DTOEntityDynamicMapper
    {   
        public static  ComplaintSourceTypeMapper Mapper(CallContext context)
        {
            var mapper = new  ComplaintSourceTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ComplaintSourceTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private ComplaintSourceTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ComplaintSourceTypes.Where(x => x.ComplaintSourceTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ComplaintSourceTypeDTO()
                {
                    ComplaintSourceTypeID = entity.ComplaintSourceTypeID,
                    SourceDescription = entity.SourceDescription,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ComplaintSourceTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ComplaintSourceType()
            {
                ComplaintSourceTypeID = toDto.ComplaintSourceTypeID,
                SourceDescription = toDto.SourceDescription,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.ComplaintSourceTypeID == 0)
                {
                    
                   
                    var maxGroupID = dbContext.ComplaintSourceTypes.Max(a => (byte?)a.ComplaintSourceTypeID);

                    entity.ComplaintSourceTypeID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.ComplaintSourceTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ComplaintSourceTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ComplaintSourceTypeID ));
        }       
    }
}