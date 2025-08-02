using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Framework;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.Frameworks
{
    public class ScreenMetadataMapper : IDTOEntityMapper<ScreenMetadataDTO, ScreenMetadata>
    {
        private CallContext _context;

        public static ScreenMetadataMapper Mapper(CallContext context)
        {
            var mapper = new ScreenMetadataMapper();
            mapper._context = context;
            return mapper;
        }

        public ScreenMetadataDTO ToDTO(ScreenMetadata entity)
        {
            if (entity.IsNull()) return new ScreenMetadataDTO();

            var metadata = new ScreenMetadataDTO()
            {
                DisplayName = entity.DisplayName,
                Name = entity.Name,
                IsCacheable = entity.IsCacheable,
                IsSavePanelRequired = entity.IsSavePanelRequired,
                IsGenericCRUDSave = entity.IsGenericCRUDSave,
                SaveCRUDMethod = entity.SaveCRUDMethod,
                JsControllerName = entity.JsControllerName,
                ListActionName = entity.ListActionName,
                ListButtonDisplayName = entity.ListButtonDisplayName,
                Urls = entity.ScreenLookupMaps.Select(a=> new ScreenLookupDTO { Url = a.Url, CallBack = a.CallBack, IsOnInit = a.IsOnInit, LookUpName = a.LookUpName }).ToList(),
                ModelNamespace = entity.ModelNamespace,
                ModelAssembly = entity.ModelAssembly,
                MasterViewModel = entity.MasterViewModel,
                DetailViewModel = entity.DetailViewModel,
                ModelViewModel = entity.ModelViewModel,
                EntityMapperViewModel = entity.EntityMapperViewModel,
                SummaryViewModel = entity.SummaryViewModel,
                ScreenTypeID = entity.ScreenTypeID.HasValue ? entity.ScreenTypeID.Value : 1,
                EntityMapperAssembly = entity.EntityMapperAssembly,
                ViewFullPath = entity.View.ViewFullPath,
                View = entity.View.ViewName,
                PrintPreviewReportName = entity.PrintPreviewReportName,
                EntityType = entity.EntityType
            };

            foreach(var feild in entity.ScreenFieldSettings)
            {
                metadata.ScreenFieldSettings.Add(new ScreenFieldSettingDTO()
                {
                    ScreenFieldSettingID = feild.ScreenFieldSettingID,
                    DateType = feild.ScreenField.DateType,
                    DefaultFormat = feild.DefaultFormat,
                    DefaultValue = feild.DefaultValue,
                    FieldName = feild.ScreenField.FieldName,
                    LookupName = feild.ScreenField.LookupName,
                    ModelName = feild.ScreenField.ModelName,
                    ScreenFieldID = feild.ScreenField.ScreenFieldID,
                    TextTransformTypeId = feild.TextTransformTypeId,
                    Prefix = feild.Prefix,
                });
            }

            return metadata;
        }

        public ScreenMetadata ToEntity(ScreenMetadataDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
