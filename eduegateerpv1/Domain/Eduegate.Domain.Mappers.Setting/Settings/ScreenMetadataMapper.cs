using Newtonsoft.Json;
using Eduegate.Domain.Entity.Setting.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Setting.Settings;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Setting.Settings
{
    public class ScreenMetadataMapper : DTOEntityDynamicMapper
    {
        public static ScreenMetadataMapper Mapper(CallContext context)
        {
            var mapper = new ScreenMetadataMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ScreenMetadataDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ScreenMetadataDTO ToDTO(long IID)
        {
            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                var entity = dbContext.ScreenMetadatas.Where(x => x.ScreenMetadataID == IID).AsNoTracking().FirstOrDefault();

                return new ScreenMetadataDTO()
                {
                    ScreenID = entity.ScreenID,
                    ViewID = entity.ViewID,
                    Name = entity.Name,
                    ListActionName = entity.ListActionName,
                    ListButtonDisplayName = entity.ListButtonDisplayName,
                    ModelAssembly = entity.ModelAssembly,
                    ModelNamespace = entity.ModelNamespace,
                    ModelViewModel = entity.ModelViewModel,
                    MasterViewModel = entity.MasterViewModel,
                    DetailViewModel = entity.DetailViewModel,
                    SummaryViewModel = entity.SummaryViewModel,
                    DisplayName = entity.DisplayName,
                    JsControllerName = entity.JsControllerName,
                    IsCacheable = entity.IsCacheable,
                    IsSavePanelRequired = entity.IsSavePanelRequired,
                    IsGenericCRUDSave = entity.IsGenericCRUDSave,
                    EntityMapperAssembly = entity.EntityMapperAssembly,
                    EntityMapperViewModel = entity.EntityMapperViewModel,
                    SaveCRUDMethod = entity.SaveCRUDMethod,
                    ScreenTypeID = entity.ScreenTypeID,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ScreenMetadataDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ScreenMetadata()
            {
                ScreenID = toDto.ScreenID,
                ViewID = toDto.ViewID,
                Name = toDto.Name,
                ListActionName = toDto.ListActionName,
                ListButtonDisplayName = toDto.ListButtonDisplayName,
                ModelAssembly = toDto.ModelAssembly,
                ModelNamespace = toDto.ModelNamespace,
                ModelViewModel = toDto.ModelViewModel,
                MasterViewModel = toDto.MasterViewModel,
                DetailViewModel = toDto.DetailViewModel,
                SummaryViewModel = toDto.SummaryViewModel,
                DisplayName = toDto.DisplayName,
                JsControllerName = toDto.JsControllerName,
                IsCacheable = toDto.IsCacheable,
                IsSavePanelRequired = toDto.IsSavePanelRequired,
                IsGenericCRUDSave = toDto.IsGenericCRUDSave,
                EntityMapperAssembly = toDto.EntityMapperAssembly,
                EntityMapperViewModel = toDto.EntityMapperViewModel,
                SaveCRUDMethod = toDto.SaveCRUDMethod,
                ScreenTypeID = toDto.ScreenTypeID,
            };

            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                if (entity.ScreenMetadataID == 0)
                {
                    var maxGroupID = dbContext.ScreenMetadatas.Max(a => (int?)a.ScreenMetadataID);
                    entity.ScreenMetadataID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.ScreenMetadatas.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ScreenMetadatas.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ScreenMetadataID));
        }

    }
}