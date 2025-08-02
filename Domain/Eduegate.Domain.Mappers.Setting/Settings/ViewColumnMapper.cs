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
    public class ViewColumnMapper : DTOEntityDynamicMapper
    {
        public static ViewColumnMapper Mapper(CallContext context)
        {
            var mapper = new ViewColumnMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ViewColumnDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ViewColumnDTO ToDTO(long IID)
        {
            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                var entity = dbContext.ViewColumns.Where(x => x.ViewColumnID == IID).AsNoTracking().FirstOrDefault();

                return new ViewColumnDTO()
                {
                    ViewColumnID = entity.ViewColumnID,
                    ViewID = entity.ViewID,
                    ColumnName = entity.ColumnName,
                    DataType = entity.DataType,
                    PhysicalColumnName = entity.PhysicalColumnName,
                    IsDefault = entity.IsDefault,
                    IsVisible = entity.IsVisible,
                    IsSortable = entity.IsSortable,
                    IsQuickSearchable = entity.IsQuickSearchable,
                    SortOrder = entity.SortOrder,
                    IsExpression = entity.IsExpression,
                    Expression = entity.Expression,
                    FilterValue = entity.FilterValue,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ViewColumnDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ViewColumn()
            {
                ViewColumnID = toDto.ViewColumnID,
                ViewID = toDto.ViewID,
                ColumnName = toDto.ColumnName,
                DataType = toDto.DataType,
                PhysicalColumnName = toDto.PhysicalColumnName,
                IsDefault = toDto.IsDefault,
                IsVisible = toDto.IsVisible,
                IsSortable = toDto.IsSortable,
                IsQuickSearchable = toDto.IsQuickSearchable,
                SortOrder = toDto.SortOrder,
                IsExpression = toDto.IsExpression,
                Expression = toDto.Expression,
                FilterValue = toDto.FilterValue,
            };

            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                if (entity.ViewColumnID == 0)
                {
                    var maxGroupID = dbContext.ViewColumns.Max(a => (long?)a.ViewColumnID);
                    entity.ViewColumnID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                    dbContext.ViewColumns.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ViewColumns.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ViewColumnID));
        }

    }
}