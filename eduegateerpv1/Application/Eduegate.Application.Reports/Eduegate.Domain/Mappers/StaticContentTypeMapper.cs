using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers
{
    public static class StaticContentTypeMapper
    {
        public static StaticContentTypeDTO ToStaticContentTypeDTOMap(Eduegate.Domain.Entity.Models.StaticContentType staticContentType)
        {
            return new StaticContentTypeDTO()
            {
                ContentTypeID = staticContentType.ContentTypeID,
                ContentTypeName = staticContentType.ContentTypeName,
                Description = staticContentType.Description,
                ContentTemplateFilePath = staticContentType.ContentTemplateFilePath,
                CreatedBy = staticContentType.CreatedBy,
                UpdatedBy = staticContentType.UpdatedBy,
                CreatedDate = Convert.ToString(staticContentType.CreatedDate),
                UpdatedDate = staticContentType.UpdatedDate,
                TimeStamps = Convert.ToString(staticContentType.TimeStamps),
                StaticContentDTOs = staticContentType.StaticContentDatas.Select(x => StaticContentTypeMapper.ToStaticContentDataDTOMap(x)).ToList()
            };
        }

        public static StaticContentDataDTO ToStaticContentDataDTOMap(StaticContentData staticContentData)
        {
            return new StaticContentDataDTO()
            {
                ContentDataIID = staticContentData.ContentDataIID,
                ContentTypeID = staticContentData.ContentTypeID,
                Title = staticContentData.Title,
                Description = staticContentData.Description,
                ImageFilePath = staticContentData.ImageFilePath,
                AdditionalParameters = JsonConvert.DeserializeObject<List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>>(staticContentData.SerializedJsonParameters),
                CreatedDate = staticContentData.CreatedDate,
                UpdatedDate = staticContentData.UpdatedDate,
                CreatedBy = staticContentData.CreatedBy,
                UpdatedBy = staticContentData.UpdatedBy,
                TimeStamps = staticContentData.TimeStamps
            };
        }
    }
}
