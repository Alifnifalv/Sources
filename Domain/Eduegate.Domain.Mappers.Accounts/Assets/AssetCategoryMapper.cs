using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using System.Collections.Generic;
using System;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetCategoryMapper : DTOEntityDynamicMapper
    {
        public static AssetCategoryMapper Mapper(CallContext context)
        {
            var mapper = new AssetCategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssetCategoryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssetCategoryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetCategories.Where(x => x.AssetCategoryID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AssetCategoryDTO ToDTO(AssetCategory entity)
        {
            var groupDTO = new AssetCategoryDTO()
            {
                AssetCategoryID = entity.AssetCategoryID,
                CategoryName = entity.CategoryName,
                IsActive = entity.IsActive,
                DepreciationRate = entity.DepreciationRate,
                DepreciationPeriodID = entity.DepreciationPeriodID,
                CategoryPrefix = entity.CategoryPrefix,
                OldCategoryPrefix = entity.CategoryPrefix,
                LastSequenceNumber = entity.LastSequenceNumber,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            return groupDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssetCategoryDTO;

            var depPeriodID = new Domain.Setting.SettingBL(_context).GetSettingValue<int>("DEPRECIATION_PERIOD_ID_MONTHLY", 1);

            if (toDto.LastSequenceNumber.HasValue && toDto.LastSequenceNumber > 0)
            {
                if (!string.IsNullOrEmpty(toDto.OldCategoryPrefix) && toDto.OldCategoryPrefix != toDto.CategoryPrefix)
                {
                    throw new Exception("Sequences are already created for this particular category, so it's not possible to change the Category Prefix!");
                }
            }

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = new AssetCategory()
                {
                    AssetCategoryID = toDto.AssetCategoryID,
                    CategoryName = toDto.CategoryName,
                    IsActive = toDto.IsActive,
                    DepreciationRate = toDto.DepreciationRate,
                    DepreciationPeriodID = toDto.DepreciationPeriodID.HasValue ? toDto.DepreciationPeriodID : depPeriodID,
                    CategoryPrefix = toDto.CategoryPrefix,
                    LastSequenceNumber = toDto.LastSequenceNumber.HasValue ? toDto.LastSequenceNumber : 0,
                    CreatedBy = toDto.AssetCategoryID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AssetCategoryID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AssetCategoryID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AssetCategoryID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.AssetCategories.Add(entity);
                if (entity.AssetCategoryID == 0)
                {
                    var maxGroupID = dbContext.AssetCategories.Max(a => (int?)a.AssetCategoryID);
                    entity.AssetCategoryID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.AssetCategoryID));
            }
        }

        public List<KeyValueDTO> GetAssetsByCategoryID(long categoryID)
        {
            var keyValueList = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.Assets
                    .Where(x => categoryID != 0 ? x.AssetCategoryID == categoryID : !string.IsNullOrEmpty(x.AssetCode))
                    .AsNoTracking().ToList();

                if (entities.Count > 0)
                {
                    keyValueList.Add(new KeyValueDTO()
                    {
                        Key = "0",
                        Value = "ALL"
                    });
                }

                foreach (var entity in entities)
                {
                    keyValueList.Add(new KeyValueDTO()
                    {
                        Key = entity.AssetIID.ToString(),
                        Value = entity.AssetCode
                    });
                }
            }

            return keyValueList;
        }

        public AssetCategoryDTO GetAssetCategoryDetailsByID(long assetCategoryID)
        {
            var assetCategoryDTO = new AssetCategoryDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetCategories.Where(x => x.AssetCategoryID == assetCategoryID).AsNoTracking().FirstOrDefault();

                if (entity != null)
                {
                    assetCategoryDTO = ToDTO(entity);
                }
            }

            return assetCategoryDTO;
        }

        public string GetNextAssetCodeByCategoryID(long assetCategoryID)
        {
            string nextAssetCode = string.Empty;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetCategories.Where(x => x.AssetCategoryID == assetCategoryID).AsNoTracking().FirstOrDefault();

                if (entity != null)
                {
                    entity.LastSequenceNumber = entity.LastSequenceNumber.HasValue ? entity.LastSequenceNumber + 1 : 1;

                    dbContext.Entry(entity).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    nextAssetCode = entity.CategoryPrefix + "-" + entity.LastSequenceNumber.ToString();
                }
            }

            return nextAssetCode;
        }

    }
}