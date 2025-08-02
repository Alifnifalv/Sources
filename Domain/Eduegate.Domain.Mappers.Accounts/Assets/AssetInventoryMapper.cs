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
    public class AssetInventoryMapper : DTOEntityDynamicMapper
    {
        public static AssetInventoryMapper Mapper(CallContext context)
        {
            var mapper = new AssetInventoryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssetInventoryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssetInventoryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetInventories.Where(x => x.AssetInventoryIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AssetInventoryDTO ToDTO(AssetInventory entity)
        {
            var inventoryDTO = new AssetInventoryDTO()
            {
                AssetInventoryIID = entity.AssetInventoryIID,
                AssetID = entity.AssetID,
                BranchID = entity.BranchID,
                Batch = entity.Batch,
                HeadID = entity.HeadID,
                CompanyID = entity.CompanyID,
                CostAmount = entity.CostAmount,
                Quantity = entity.Quantity,
                OriginalQty = entity.OriginalQty,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            return inventoryDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssetInventoryDTO;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = new AssetInventory()
                {
                    AssetInventoryIID = toDto.AssetInventoryIID,
                    AssetID = toDto.AssetID,
                    BranchID = toDto.BranchID,
                    Batch = toDto.Batch,
                    HeadID = toDto.HeadID,
                    CompanyID = toDto.CompanyID,
                    CostAmount = toDto.CostAmount,
                    Quantity = toDto.Quantity,
                    OriginalQty = toDto.OriginalQty,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.AssetInventoryIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AssetInventoryIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AssetInventoryIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AssetInventoryIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.AssetInventories.Add(entity);
                if (entity.AssetInventoryIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.AssetInventoryIID));
            }
        }

        public AssetInventoryDTO GetAssetInventoryDetail(int companyID, long assetID, long branchID = 0)
        {
            var inventoryDTO = new AssetInventoryDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.AssetInventories
                    .Where(x => x.AssetID == assetID && (branchID > 0 ? x.BranchID == branchID : x.BranchID.HasValue))
                    .AsNoTracking().ToList();

                inventoryDTO.AssetID = assetID;
                inventoryDTO.BranchID = branchID;
                inventoryDTO.AvailableQuantity = entities.Count > 0 ? entities.Sum(s => s.Quantity) : 0;
            }

            return inventoryDTO;
        }

    }
}