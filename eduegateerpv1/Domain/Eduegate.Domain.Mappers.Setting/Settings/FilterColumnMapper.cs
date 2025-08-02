using Newtonsoft.Json;
using Eduegate.Domain.Entity.Setting.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Setting.Settings;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Setting.Settings
{
    public class FilterColumnMapper : DTOEntityDynamicMapper
    {
        public static FilterColumnMapper Mapper(CallContext context)
        {
            var mapper = new FilterColumnMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FilterColumnDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FilterColumnDTO ToDTO(long IID)
        {
            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                var entity = dbContext.FilterColumns.Where(x => x.FilterColumnID == IID).AsNoTracking().FirstOrDefault();

                return new FilterColumnDTO()
                {
                    FilterColumnID = entity.FilterColumnID,
                    SequenceNo = entity.SequenceNo,
                    ViewID = entity.ViewID,
                    ColumnCaption = entity.ColumnCaption,
                    ColumnName = entity.ColumnName,
                    DataTypeID = entity.DataTypeID,
                    UIControlTypeID = entity.UIControlTypeID,
                    DefaultValues = entity.DefaultValues,
                    IsQuickFilter = entity.IsQuickFilter,
                    LookupID = entity.LookupID,
                    Attribute1 = entity.Attribute1,
                    Attribute2 = entity.Attribute2,
                    IsLookupLazyLoad = entity.IsLookupLazyLoad,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FilterColumnDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FilterColumn()
            {
                FilterColumnID = toDto.FilterColumnID,
                SequenceNo = toDto.SequenceNo,
                ViewID = toDto.ViewID,
                ColumnCaption = toDto.ColumnCaption,
                ColumnName = toDto.ColumnName,
                DataTypeID = toDto.DataTypeID,
                UIControlTypeID = toDto.UIControlTypeID,
                DefaultValues = toDto.DefaultValues,
                IsQuickFilter = toDto.IsQuickFilter,
                LookupID = toDto.LookupID,
                Attribute1 = toDto.Attribute1,
                Attribute2 = toDto.Attribute2,
                IsLookupLazyLoad = toDto.IsLookupLazyLoad,
            };

            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                if (entity.FilterColumnID == 0)
                {
                    var maxGroupID = dbContext.FilterColumns.Max(a => (long?)a.FilterColumnID);
                    entity.FilterColumnID = Convert.ToByte(maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1);
                    dbContext.FilterColumns.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FilterColumns.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.FilterColumnID));
        }

    }
}