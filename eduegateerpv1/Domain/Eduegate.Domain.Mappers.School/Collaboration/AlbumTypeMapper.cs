using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class AlbumTypeMapper : DTOEntityDynamicMapper
    {   
        public static  AlbumTypeMapper Mapper(CallContext context)
        {
            var mapper = new  AlbumTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AlbumTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private AlbumTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AlbumTypes.Where(a => a.AlbumTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new AlbumTypeDTO()
                {
                    AlbumTypeID = entity.AlbumTypeID,
                    AlbumTypeName = entity.AlbumTypeName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AlbumTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new AlbumType()
            {
                AlbumTypeID = toDto.AlbumTypeID,
                AlbumTypeName = toDto.AlbumTypeName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
              
                if (entity.AlbumTypeID == 0)
                {
                    var maxGroupID = dbContext.AlbumTypes.Max(a => (int?)a.AlbumTypeID);                    
                    entity.AlbumTypeID = Convert.ToByte(maxGroupID == null ? 1 : Convert.ToByte(maxGroupID.ToString()) + 1);
                    dbContext.AlbumTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.AlbumTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.AlbumTypeID ));
        }       
    }
}