using Newtonsoft.Json;
using Eduegate.Domain.Entity.Contents;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Domain.Entity.Contents.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Contents
{
    public class ContentTypeMapper : DTOEntityDynamicMapper
    {
        public static ContentTypeMapper Mapper(CallContext context)
        {
            var mapper = new ContentTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ContentTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ContentTypeDTO ToDTO(long IID)
        {
            using (dbContentContext dbContext = new dbContentContext())
            {
                var entity = dbContext.ContentTypes.Where(a => a.ContentTypeID == IID).AsNoTracking().FirstOrDefault();

                return new ContentTypeDTO()
                {
                    ContentTypeID = entity.ContentTypeID,
                    ContentName = entity.ContentName
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ContentTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Eduegate.Domain.Entity.Contents.ContentType()
            {
                ContentTypeID = toDto.ContentTypeID,
                ContentName = toDto.ContentName
            };

            using (dbContentContext dbContext = new dbContentContext())
            {
                if (entity.ContentTypeID == 0)
                {
                    var maxGroupID = dbContext.ContentTypes.Max(a => (int?)a.ContentTypeID);
                    entity.ContentTypeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.ContentTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ContentTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
           
            return ToDTOString(ToDTO(entity.ContentTypeID));
        }

    }
}