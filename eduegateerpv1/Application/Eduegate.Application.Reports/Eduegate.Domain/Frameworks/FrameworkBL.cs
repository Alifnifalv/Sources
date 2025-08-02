using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Domain.Repository.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Frameworks
{
    public class FrameworkBL
    {
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public FrameworkBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public ScreenMetadataDTO GetScreenMetadata(long screenID)
        {
            var dto = Mappers.Frameworks.ScreenMetadataMapper.Mapper(_callContext).ToDTO(new FrameworkRepository().GetScreenMetadata(screenID, _callContext.LanguageCode));

            if (_callContext.LoginID.HasValue)
            {
                var features = new SecurityRepository().GetUserClaimsByType(_callContext.LoginID.Value, (int)ClaimType.Features);
                dto.Claims = Mappers.Security.ClaimMapper.Mapper(_callContext).ToDTO(features);
            }

            if(string.IsNullOrEmpty(dto.EntityMapperAssembly))
            {
                dto.EntityMapperAssembly = "Eduegate.Domain.dll";
            }

            return dto;
        }

        public string GetScreenData(long screenID, long IID)
        {
            var screen = Framework.CacheManager.MemCacheManager<string>.Get("SCREEN_" + screenID);

            if (screen == null)
            {
                var screenData = GetScreenMetadata(screenID);

                if (!string.IsNullOrEmpty(screenData.EntityMapperAssembly) && !string.IsNullOrEmpty(screenData.EntityMapperViewModel))
                {
                    var mapper = DTOEntityDynamicMapper.GetMapper(screenData.EntityMapperAssembly, screenData.EntityMapperViewModel);
                    mapper.SetContext(_callContext);
                    return mapper.GetEntity(IID);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return screen;
            }
        }

        public ScreenDataDTO SaveScreenData(ScreenDataDTO data)
        {
            return null;
        }

        public CRUDDataDTO SaveCRUDData(CRUDDataDTO data)
        {
            try
            {
                var screenData = GetScreenMetadata(data.ScreenID);
                var mapper = DTOEntityDynamicMapper.GetMapper(screenData.EntityMapperAssembly, screenData.EntityMapperViewModel);
                mapper.SetContext(_callContext);
                var dto = mapper.ToDTO(data.Data);
                var entity = mapper.SaveEntity(dto);
                return new CRUDDataDTO() { ScreenID = data.ScreenID, Data = entity, IsError = false };
            }catch(Exception ex)
            {
                return new CRUDDataDTO() { IsError = true, ErrorMessage = ex.Message };
            }
        }

        public KeyValueDTO ValidateField(CRUDDataDTO data, string field)
        {
            var screenData = GetScreenMetadata(data.ScreenID);
            var mapper = DTOEntityDynamicMapper.GetMapper(screenData.EntityMapperAssembly, screenData.EntityMapperViewModel);
            mapper.SetContext(_callContext);
            return mapper.ValidateField(mapper.ToDTO(data.Data), field);
        }

        public bool DeleteCRUDData(long screenID, long IID)
        {
            var screenData = GetScreenMetadata(screenID);
            var mapper = DTOEntityDynamicMapper.GetMapper(screenData.EntityMapperAssembly, screenData.EntityMapperViewModel);
            mapper.SetContext(_callContext);
            return mapper.DeleteCRUDData(screenID, IID);
        }

        public long CloneCRUDData(long screenID, long IID)
        {
            var screenData = GetScreenMetadata(screenID);
            var mapper = DTOEntityDynamicMapper.GetMapper(screenData.EntityMapperAssembly, screenData.EntityMapperViewModel);
            mapper.SetContext(_callContext);
            return mapper.Clone(screenID, IID);
        }
    }
}
