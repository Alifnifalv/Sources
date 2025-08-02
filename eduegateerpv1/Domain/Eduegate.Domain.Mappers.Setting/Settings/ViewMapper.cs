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
    public class ViewMapper : DTOEntityDynamicMapper
    {
        public static ViewMapper Mapper(CallContext context)
        {
            var mapper = new ViewMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ViewDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ViewDTO ToDTO(long IID)
        {
            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                var entity = dbContext.Views.Where(x => x.ViewID == IID).AsNoTracking().FirstOrDefault();

                return new ViewDTO()
                {
                    ViewID = entity.ViewID,
                    ViewTypeID = entity.ViewTypeID,
                    ViewName = entity.ViewName,
                    ViewFullPath = entity.ViewFullPath,
                    IsMultiLine = entity.IsMultiLine,
                    IsRowCategory = entity.IsRowCategory,
                    PhysicalSchemaName = entity.PhysicalSchemaName,
                    HasChild = entity.HasChild,
                    IsRowClickForMultiSelect = entity.IsRowClickForMultiSelect,
                    ChildViewID = entity.ChildViewID,
                    ChildFilterField = entity.ChildFilterField,
                    ControllerName = entity.ControllerName,
                    IsMasterDetail = entity.IsMasterDetail,
                    IsEditable = entity.IsEditable,
                    IsGenericCRUDSave = entity.IsGenericCRUDSave,
                    IsReloadSummarySmartViewAlways = entity.IsReloadSummarySmartViewAlways,
                    JsControllerName = entity.JsControllerName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ViewDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new View()
            {
                ViewID = toDto.ViewID,
                ViewTypeID = toDto.ViewTypeID,
                ViewName = toDto.ViewName,
                ViewFullPath = toDto.ViewFullPath,
                IsMultiLine = toDto.IsMultiLine,
                IsRowCategory = toDto.IsRowCategory,
                PhysicalSchemaName = toDto.PhysicalSchemaName,
                HasChild = toDto.HasChild,
                IsRowClickForMultiSelect = toDto.IsRowClickForMultiSelect,
                ChildViewID = toDto.ChildViewID,
                ChildFilterField = toDto.ChildFilterField,
                ControllerName = toDto.ControllerName,
                IsMasterDetail = toDto.IsMasterDetail,
                IsEditable = toDto.IsEditable,
                IsGenericCRUDSave = toDto.IsGenericCRUDSave,
                IsReloadSummarySmartViewAlways = toDto.IsReloadSummarySmartViewAlways,
                JsControllerName = toDto.JsControllerName,
            };

            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                if (entity.ViewID == 0)
                {
                    var maxGroupID = dbContext.Views.Max(a => (long?)a.ViewID);
                    entity.ViewID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Views.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Views.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ViewID));
        }

    }
}