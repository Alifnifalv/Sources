using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.BlackPearl;
using Eduegate.Services.Contracts.Commons;
using Newtonsoft.Json;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain
{
    public class StaticContentBL
    {
        private CallContext _callContext;
        private StaticContentRepository contentRepository = new StaticContentRepository();

        public StaticContentBL(CallContext context)
        {
            _callContext = context;
        }


        public StaticContentDTO SaveContent(StaticContentDTO dto)
        {
            var defaultSKUID = contentRepository.GetDefaultSKUID(Convert.ToInt32(dto.AdditionalParameters[0].ParameterValue));

            if (defaultSKUID > 0)
            {
                dto.AdditionalParameters.Add(new Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Enums.StaticContentType.ByBrand.Keys.SKUID, ParameterValue = defaultSKUID.ToString() });
            }

            var entity = contentRepository.SaveContent(ToEntity(dto), _callContext);
            return FromEntity(entity);
        }

        public StaticContentData ToEntity(StaticContentDTO dto)
        {

            var staticContentData = new StaticContentData()
            {
                ContentDataIID = dto.ContentDataIID,
                ContentTypeID = (int)dto.ContentTypeID,
                Title = dto.Title,
                Description = dto.Description,
                ImageFilePath = dto.ImageFilePath,
                SerializedJsonParameters = JsonConvert.SerializeObject(dto.AdditionalParameters),
            };

            return staticContentData;
        }

        public static StaticContentDTO FromEntity(StaticContentData entity)
        {
            return new StaticContentDTO()
            {
                ContentDataIID = entity.ContentDataIID,
                ContentTypeID = (Eduegate.Services.Contracts.Enums.StaticContentTypes)entity.ContentTypeID,
                Title = entity.Title,
                Description = entity.Description,
                ImageFilePath = entity.ImageFilePath,
                AdditionalParameters = JsonConvert.DeserializeObject<List<KeyValueParameterDTO>>(entity.SerializedJsonParameters),
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };
        }

        public StaticContentDTO GetStaticContent(long contentID)
        {
            var content = contentRepository.GetStaticContent(contentID);
            return FromEntity(content);
        }

        public StaticContentTypeDTO GetStaticContentTypes(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes)
        {
            StaticContentType staticContentType = contentRepository.GetStaticContentTypes((int)staticContentTypes);
            StaticContentTypeDTO staticContentTypeDTO = StaticContentTypeMapper.ToStaticContentTypeDTOMap(staticContentType);
            return staticContentTypeDTO;
        }

        public List<StaticContentDataDTO> GetStaticContentData(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes, int pageSize, int pageNumber)
        {
            List<StaticContentData> staticContentDatas = contentRepository.GetStaticContentData(staticContentTypes, pageSize,pageNumber);
            List<StaticContentDataDTO> staticContentDataDTOs = staticContentDatas.Select(x => StaticContentDataMapper.ToStaticContentDataDTOMap(x)).ToList();
            return staticContentDataDTOs;
        }

    }
}
