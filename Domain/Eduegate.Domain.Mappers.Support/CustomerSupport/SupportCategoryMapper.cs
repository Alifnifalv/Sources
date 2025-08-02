using Eduegate.Domain.Entity.Supports;
using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Support.CustomerSupport;
using System;

namespace Eduegate.Domain.Mappers.Support.CustomerSupport
{
    public class SupportCategoryMapper : DTOEntityDynamicMapper
    {
        public static SupportCategoryMapper Mapper(CallContext context)
        {
            var mapper = new SupportCategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SupportCategoryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SupportCategoryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var entity = dbContext.SupportCategories.Where(x => x.SupportCategoryID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private SupportCategoryDTO ToDTO(SupportCategory entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var categoryDTO = new SupportCategoryDTO()
            {
                SupportCategoryID = entity.SupportCategoryID,
                CategoryName = entity.CategoryName,
                ParentCategoryID = entity.ParentCategoryID,
                SortOrder = entity.SortOrder,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };

            return categoryDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SupportCategoryDTO;

            using (var dbContext = new dbEduegateSupportContext())
            {
                var entity = new SupportCategory()
                {
                    SupportCategoryID = toDto.SupportCategoryID,
                    CategoryName = toDto.CategoryName,
                    ParentCategoryID = toDto.ParentCategoryID,
                    SortOrder = toDto.SortOrder,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.SupportCategoryID > 0 ? toDto.CreatedBy : _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null,
                    CreatedDate = toDto.SupportCategoryID > 0 ? toDto.CreatedDate : DateTime.Now,
                    UpdatedBy = toDto.SupportCategoryID > 0 ? _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null : (int?)null,
                    UpdatedDate = toDto.SupportCategoryID > 0 ? DateTime.Now : (DateTime?)null,
                };                

                dbContext.SupportCategories.Add(entity);

                if (entity.SupportCategoryID == 0)
                {
                    var maxGroupID = dbContext.SupportCategories.Max(a => (int?)a.SupportCategoryID);
                    entity.SupportCategoryID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.SupportCategoryID));
            }
        }
        
        public List<KeyValueDTO> GetSupportSubCategoriesByCategoryID(int? supportCategoryID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var keyValueList = new List<KeyValueDTO>();

                var entities = dbContext.SupportCategories.Where(x => x.ParentCategoryID == supportCategoryID && x.IsActive == true)
                    .OrderBy(o => o.SortOrder)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in entities)
                {
                    keyValueList.Add(new KeyValueDTO()
                    {
                        Key = entity.SupportCategoryID.ToString(),
                        Value = entity.CategoryName
                    });
                }

                return keyValueList;
            }
        }

    }
}