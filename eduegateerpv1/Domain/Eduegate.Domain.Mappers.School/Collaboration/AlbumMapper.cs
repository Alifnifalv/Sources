using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class AlbumMapper : DTOEntityDynamicMapper
    {   
        public static  AlbumMapper Mapper(CallContext context)
        {
            var mapper = new  AlbumMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AlbumDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private AlbumDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Albums.Where(a => a.AlbumID == IID)
                    .Include(i => i.AlbumType)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new AlbumDTO()
                {
                    AlbumID = entity.AlbumID,
                    AlbumName = entity.AlbumName,
                    AlbumTypeID = entity.AlbumTypeID,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AlbumDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Album()
            {
                AlbumID = toDto.AlbumID,
                AlbumName = toDto.AlbumName,
                AlbumTypeID = toDto.AlbumTypeID,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.AlbumID == 0)
                {
                   
                    var maxGroupID = dbContext.Albums.Max(a => (int?)a.AlbumID);
                    entity.AlbumID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Albums.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Albums.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.AlbumID ));
        }       
    }
}