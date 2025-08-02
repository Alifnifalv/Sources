using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework;
using System.Linq;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.School.Models.School;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class StaticContentDataMapper : DTOEntityDynamicMapper
    {
        public static StaticContentDataMapper Mapper(CallContext context)
        {
            var mapper = new StaticContentDataMapper();
            mapper._context = context;
            return mapper;
        }


        public StaticContentDataDTO GetAboutandContactDetails(long contentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StaticContentDatas.Where(x => x.ContentDataIID == contentID).AsNoTracking().FirstOrDefault();

                return new StaticContentDataDTO()
                {
                    ContentDataIID = entity.ContentDataIID,
                    ContentTypeID = entity.ContentTypeID,
                    Title = entity.Title,
                    Description = entity.Description,
                    ImageFilePath = entity.ImageFilePath,
                    SerializedJsonParameters = entity.SerializedJsonParameters,
                };
            }
        }

        public StaticContentDataDTO GetStaticPage(long contentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StaticContentDatas.Where(x => x.ContentDataIID == contentID).AsNoTracking().FirstOrDefault();

                return new StaticContentDataDTO()
                {
                    ContentDataIID = entity.ContentDataIID,
                    ContentTypeID = entity.ContentTypeID,
                    Title = entity.Title,
                    Description = entity.Description,
                    ImageFilePath = entity.ImageFilePath,
                    SerializedJsonParameters = entity.SerializedJsonParameters,
                };
            }
        }


    }
}