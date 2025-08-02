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
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetMapper : DTOEntityDynamicMapper
    {
        public static AssetMapper Mapper(CallContext context)
        {
            var mapper = new AssetMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssetDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssetDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.Assets.Where(x => x.AssetIID == IID)
                    .Include(i => i.AssetCategory)
                    .Include(i => i.AssetGlAcc)
                    .Include(i => i.AccumulatedDepGLAcc)
                    .Include(i => i.DepreciationExpGLAcc)
                    .Include(i => i.AssetProductMaps).ThenInclude(i => i.ProductSKUMap)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AssetDTO ToDTO(Asset entity)
        {
            var assetDTO = new AssetDTO();
            if (entity != null)
            {
                assetDTO = new AssetDTO()
                {
                    AssetIID = entity.AssetIID,
                    AssetTypeID = entity.AssetTypeID,
                    AssetGroupID = entity.AssetGroupID,
                    AssetCategoryID = entity.AssetCategoryID,
                    AssetCategoryName = entity.AssetCategoryID.HasValue ? entity.AssetCategory?.CategoryName : null,
                    AssetCategoryDepreciationRate = entity.AssetCategoryID.HasValue ? entity.AssetCategory?.DepreciationRate : null,
                    AssetCategoryPrefix = entity.AssetCategoryID.HasValue ? entity.AssetCategory?.CategoryPrefix : null,
                    AssetSubCategoryID = entity.AssetSubCategoryID,
                    AssetCode = entity.AssetCode,
                    Description = entity.Description,
                    AssetGlAccID = entity.AssetGlAccID,
                    AccumulatedDepGLAccID = entity.AccumulatedDepGLAccID,
                    DepreciationExpGLAccID = entity.DepreciationExpGLAccId,
                    DepreciationYears = entity.DepreciationYears,
                    UnitID = entity.UnitID,
                    AssetPrefix = entity.AssetPrefix,
                    LastSequenceNumber = entity.LastSequenceNumber ?? 0,
                    IsRequiredSerialNumber = entity.IsRequiredSerialNumber,
                    AssetGlAccount = entity.AssetGlAccID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AssetGlAcc.AccountID.ToString(),
                        Value = entity.AssetGlAcc.AccountName
                    } : new KeyValueDTO(),
                    AccumulatedDepGLAccount = entity.AccumulatedDepGLAccID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AccumulatedDepGLAcc.AccountID.ToString(),
                        Value = entity.AccumulatedDepGLAcc.AccountName
                    } : new KeyValueDTO(),
                    DepreciationExpGLAccount = entity.DepreciationExpGLAccId.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.DepreciationExpGLAcc.AccountID.ToString(),
                        Value = entity.DepreciationExpGLAcc.AccountName
                    } : new KeyValueDTO(),
                    Remarks = entity.Remarks,
                    Reference = entity.Reference,
                    DepreciationTypeID = entity.DepreciationTypeID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                FillAssetProductMapDTOs(entity, assetDTO);
                FillAssetSerialMapDTOs(entity, assetDTO);
            }

            return assetDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssetDTO;

            if (string.IsNullOrEmpty(toDto.AssetCategoryPrefix))
            {
                throw new Exception("A prefix is not available for the selected category. Please add it to proceed.");
            }

            if (toDto.AssetIID == 0)
            {
                toDto.AssetCode = AssetCategoryMapper.Mapper(_context).GetNextAssetCodeByCategoryID(toDto.AssetCategoryID.Value);
            }

            var defaultDepTypeID = new Setting.SettingBL(_context).GetSettingValue<int>("DEPRECIATION_TYPE_ID_SLM", 1);

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = new Asset()
                {
                    AssetIID = toDto.AssetIID,
                    AssetTypeID = toDto.AssetTypeID,
                    AssetGroupID = toDto.AssetGroupID,
                    AssetCategoryID = toDto.AssetCategoryID,
                    AssetSubCategoryID = toDto.AssetSubCategoryID,
                    AssetCode = toDto.AssetCode,
                    Description = toDto.Description,
                    AssetGlAccID = toDto.AssetGlAccID,
                    AccumulatedDepGLAccID = toDto.AccumulatedDepGLAccID,
                    DepreciationExpGLAccId = toDto.DepreciationExpGLAccID,
                    DepreciationYears = toDto.DepreciationYears,
                    UnitID = toDto.UnitID,
                    AssetPrefix = toDto.AssetPrefix,
                    LastSequenceNumber = toDto.LastSequenceNumber,
                    IsRequiredSerialNumber = toDto.IsRequiredSerialNumber,
                    Remarks = toDto.Remarks,
                    Reference = toDto.Reference,
                    DepreciationTypeID = toDto.DepreciationTypeID.HasValue ? toDto.DepreciationTypeID : defaultDepTypeID,
                    CreatedBy = toDto.AssetIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AssetIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AssetIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AssetIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                entity.AssetProductMaps = new List<AssetProductMap>();

                foreach (var prodMap in toDto.AssetProductMapDTOs)
                {
                    entity.AssetProductMaps.Add(new AssetProductMap()
                    {
                        AssetProductMapIID = prodMap.AssetProductMapIID,
                        AssetID = prodMap.AssetID,
                        ProductSKUMapID = prodMap.ProductSKUMapID,
                        CreatedBy = prodMap.AssetProductMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = prodMap.AssetProductMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = prodMap.AssetProductMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = prodMap.AssetProductMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    });
                }

                dbContext.Assets.Add(entity);
                if (entity.AssetIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    var oldProductMaps = dbContext.AssetProductMaps.Where(x => x.AssetID == entity.AssetIID).AsNoTracking().ToList();

                    if (oldProductMaps.Count > 0)
                    {
                        dbContext.AssetProductMaps.RemoveRange(oldProductMaps);
                    }

                    foreach (var productMap in entity.AssetProductMaps)
                    {
                        if (productMap.AssetProductMapIID == 0)
                        {
                            dbContext.Entry(productMap).State = EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.AssetIID));
            }
        }

        public void FillAssetProductMapDTOs(Asset entity, AssetDTO assetDTO)
        {
            assetDTO.AssetProductMapDTOs = new List<AssetProductMapDTO>();
            if (entity.AssetProductMaps != null)
            {
                foreach (var prodMap in entity.AssetProductMaps)
                {
                    assetDTO.AssetProductMapDTOs.Add(new AssetProductMapDTO()
                    {
                        AssetProductMapIID = prodMap.AssetProductMapIID,
                        AssetID = prodMap.AssetID,
                        ProductSKUMapID = prodMap.ProductSKUMapID,
                        ProductSKUName = prodMap.ProductSKUMapID.HasValue ? prodMap.ProductSKUMap?.SKUName : null,
                    });
                }
            }
        }

        public void FillAssetSerialMapDTOs(Asset entity, AssetDTO assetDTO)
        {
            assetDTO.AssetSerialMapDTOs = new List<AssetSerialMapDTO>();
            if (entity.AssetSerialMaps != null)
            {
                foreach (var serialMap in entity.AssetSerialMaps)
                {
                    var serialMapData = AssetSerialMapMapper.Mapper(_context).ToDTO(serialMap);

                    assetDTO.AssetSerialMapDTOs.Add(serialMapData);
                }
            }
        }

        public string GetNextAssetTransactionNumberByID(long assetID)
        {
            string assetTransactionNumber = string.Empty;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var assetDetail = dbContext.Assets.Where(x => x.AssetIID == assetID).AsNoTracking().FirstOrDefault();

                if (assetDetail != null)
                {
                    var lastSequenceNumber = Convert.ToInt64(assetDetail.LastSequenceNumber != null ? assetDetail.LastSequenceNumber + 1 : 1);

                    assetTransactionNumber = assetDetail.AssetPrefix + "-" + lastSequenceNumber;

                    assetDetail.LastSequenceNumber = lastSequenceNumber;

                    dbContext.Entry(assetDetail).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }

            return assetTransactionNumber;
        }

        public AssetDTO GetAssetDetailsByID(long assetID)
        {
            var assetDTO = new AssetDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.Assets.Where(x => x.AssetIID == assetID)
                    .Include(i => i.AssetCategory)
                    .Include(i => i.AssetGlAcc)
                    .Include(i => i.AccumulatedDepGLAcc)
                    .Include(i => i.DepreciationExpGLAcc)
                    .Include(i => i.AssetProductMaps).ThenInclude(i => i.ProductSKUMap)
                    .Include(i => i.AssetSerialMaps).ThenInclude(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().FirstOrDefault();

                assetDTO = ToDTO(entity);
            }

            return assetDTO;
        }

        public List<KeyValueDTO> GetFullActiveAssets()
        {
            var keyValueList = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.Assets.Where(x => !string.IsNullOrEmpty(x.AssetCode)).AsNoTracking().ToList();

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

        public AssetDTO GetAssetDetailsByAssetAndBranch(long assetID, long? branchID)
        {
            var assetDTO = new AssetDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.Assets.Where(x => x.AssetIID == assetID && x.AssetInventories.Any(y => y.BranchID == branchID))
                    .Include(i => i.AssetCategory)
                    .Include(i => i.AssetGlAcc)
                    .Include(i => i.AccumulatedDepGLAcc)
                    .Include(i => i.DepreciationExpGLAcc)
                    .Include(i => i.AssetInventories)
                    .Include(i => i.AssetProductMaps).ThenInclude(i => i.ProductSKUMap)
                    .Include(i => i.AssetSerialMaps).ThenInclude(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().FirstOrDefault();

                assetDTO = ToDTO(entity);
            }

            return assetDTO;
        }

        public List<AssetDTO> GetAssetsDetailByCategoryAndBranch(long categoryID, long? branchID)
        {
            var assetDTOList = new List<AssetDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.Assets.Where(x => x.AssetCategoryID == categoryID && x.AssetInventories.Any(y => y.BranchID == branchID))
                    .Include(i => i.AssetCategory)
                    .Include(i => i.AssetGlAcc)
                    .Include(i => i.AccumulatedDepGLAcc)
                    .Include(i => i.DepreciationExpGLAcc)
                    .Include(i => i.AssetInventories)
                    .Include(i => i.AssetProductMaps).ThenInclude(i => i.ProductSKUMap)
                    .Include(i => i.AssetSerialMaps).ThenInclude(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    assetDTOList.Add(ToDTO(entity));
                }
            }

            return assetDTOList;
        }

        public List<AssetDTO> GetFullActiveAssetsDetailByBranchID(long? branchID)
        {
            var assetDTOList = new List<AssetDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.Assets.Where(x => x.AssetInventories.Any(y => y.BranchID == branchID))
                    .Include(i => i.AssetCategory)
                    .Include(i => i.AssetGlAcc)
                    .Include(i => i.AccumulatedDepGLAcc)
                    .Include(i => i.DepreciationExpGLAcc)
                    .Include(i => i.AssetInventories)
                    .Include(i => i.AssetProductMaps).ThenInclude(i => i.ProductSKUMap)
                    .Include(i => i.AssetSerialMaps).ThenInclude(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    assetDTOList.Add(ToDTO(entity));
                }
            }

            return assetDTOList;
        }

        public List<KeyValueDTO> GetAssetsByProductSKUID(long productSKUID)
        {
            var keyValueList = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.AssetProductMaps.Where(x => x.ProductSKUMapID == productSKUID)
                    .Include(i => i.Asset)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    keyValueList.Add(new KeyValueDTO()
                    {
                        Key = entity.AssetID.ToString(),
                        Value = entity.Asset?.AssetCode
                    });
                }
            }

            return keyValueList;
        }

    }
}