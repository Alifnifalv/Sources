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
using System.Globalization;
using Eduegate.Services.Contracts.Leads;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetSerialMapMapper : DTOEntityDynamicMapper
    {
        public static AssetSerialMapMapper Mapper(CallContext context)
        {
            var mapper = new AssetSerialMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssetSerialMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssetSerialMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetSerialMaps.Where(x => x.AssetSerialMapIID == IID)
                    .Include(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        public AssetSerialMapDTO ToDTO(AssetSerialMap entity)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var assetSerialMapDTO = new AssetSerialMapDTO()
            {
                AssetSerialMapIID = entity.AssetSerialMapIID,
                AssetID = entity.AssetID,
                AssetInventoryID = entity.AssetInventoryID,
                AssetSequenceCode = entity.AssetSequenceCode,
                SerialNumber = entity.SerialNumber,
                AssetTag = entity.AssetTag,
                CostPrice = entity.CostPrice,
                ExpectedLife = entity.ExpectedLife,
                DepreciationRate = entity.DepreciationRate,
                ExpectedScrapValue = entity.ExpectedScrapValue,
                AccumulatedDepreciationAmount = entity.AccumulatedDepreciationAmount,
                DateOfFirstUse = entity.DateOfFirstUse,
                FirstUseDateString = entity.DateOfFirstUse.HasValue ? entity.DateOfFirstUse.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                SupplierID = entity.SupplierID,
                BillNumber = entity.BillNumber,
                BillDate = entity.BillDate,
                BillDateString = entity.BillDate.HasValue ? entity.BillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                IsActive = entity.IsActive,
                LastDepreciationValue = entity.LastDepreciationValue,
                LastDepreciationBooked = entity.LastDepreciationBooked,
                LastNetValue = entity.LastNetValue,
                LastFromDate = entity.LastFromDate,
                LastFromDateString = entity.LastFromDate.HasValue ? entity.LastFromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                LastToDate = entity.LastToDate,
                LastToDateString = entity.LastToDate.HasValue ? entity.LastToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                IsDisposed = entity.IsDisposed,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            BringAssetInventoryData(entity, assetSerialMapDTO);

            return assetSerialMapDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssetSerialMapDTO;

            var entity = SaveAssetSerialMap(toDto);

            return ToDTOString(ToDTO(entity.AssetSerialMapIID));
        }

        public AssetSerialMap SaveAssetSerialMap(AssetSerialMapDTO toDto)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                //if (toDto.AssetSerialMapIID == 0)
                //{
                //    toDto.AssetSequenceCode = AssetMapper.Mapper(_context).GetNextAssetTransactionNumberByID(toDto.AssetID.Value);
                //}

                var entity = new AssetSerialMap()
                {
                    AssetSerialMapIID = toDto.AssetSerialMapIID,
                    AssetID = toDto.AssetID,
                    AssetInventoryID = toDto.AssetInventoryID,
                    AssetSequenceCode = toDto.AssetSequenceCode,
                    SerialNumber = toDto.SerialNumber,
                    AssetTag = toDto.AssetTag,
                    CostPrice = toDto.CostPrice,
                    ExpectedLife = toDto.ExpectedLife,
                    DepreciationRate = toDto.DepreciationRate,
                    ExpectedScrapValue = toDto.ExpectedScrapValue,
                    AccumulatedDepreciationAmount = toDto.AccumulatedDepreciationAmount,
                    DateOfFirstUse = toDto.DateOfFirstUse,
                    SupplierID = toDto.SupplierID,
                    BillNumber = toDto.BillNumber,
                    BillDate = toDto.BillDate,
                    IsActive = toDto.IsActive,
                    LastDepreciationValue = toDto.LastDepreciationValue,
                    LastDepreciationBooked = toDto.LastDepreciationBooked,
                    LastNetValue = toDto.LastNetValue,
                    LastFromDate = toDto.LastFromDate,
                    LastToDate = toDto.LastToDate,
                    IsDisposed = toDto.IsDisposed,
                    CreatedBy = toDto.AssetSerialMapIID == 0 ? _context != null ? (int)_context.LoginID : toDto.CreatedBy : toDto.CreatedBy,
                    UpdatedBy = toDto.AssetSerialMapIID > 0 ? _context != null ? (int)_context.LoginID : toDto.UpdatedBy : toDto.UpdatedBy,
                    CreatedDate = toDto.AssetSerialMapIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.AssetSerialMapIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                dbContext.AssetSerialMaps.Add(entity);
                if (entity.AssetSerialMapIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return entity;
            }
        }

        public void BringAssetInventoryData(AssetSerialMap entity, AssetSerialMapDTO assetSerialMap)
        {
            if (assetSerialMap.AssetInventoryID.HasValue)
            {
                if (entity.AssetInventory != null)
                {
                    assetSerialMap.InventoryBranchID = entity?.AssetInventory?.BranchID;
                    assetSerialMap.InventoryBranchName = entity?.AssetInventory?.Branch?.BranchName;
                }
                else
                {
                    using (var dbContext = new dbEduegateAccountsContext())
                    {
                        var inventory = dbContext.AssetInventories.Where(x => x.AssetInventoryIID == assetSerialMap.AssetInventoryID)
                            .Include(i => i.Branch)
                            .AsNoTracking().FirstOrDefault();

                        assetSerialMap.InventoryBranchID = inventory?.BranchID;
                        assetSerialMap.InventoryBranchName = inventory?.Branch?.BranchName;
                    }
                }
            }
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetID(long assetID)
        {
            var assetSerialMapDTOs = new List<AssetSerialMapDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.AssetSerialMaps.Where(x => x.AssetID == assetID && x.IsActive == true)
                    .Include(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    assetSerialMapDTOs.Add(ToDTO(entity));
                }
            }

            return assetSerialMapDTOs;
        }

        public void GetAndUpdateAssetSerialMapEntry(AssetTransactionDetailsDTO dto)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                if (dto.AssetTransactionSerialMaps != null)
                {
                    foreach (var serialMap in dto.AssetTransactionSerialMaps)
                    {
                        var entity = dbContext.AssetSerialMaps.Where(y => y.AssetSerialMapIID == serialMap.AssetSerialMapID).AsNoTracking().FirstOrDefault();

                        if (entity != null)
                        {
                            entity.AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount;
                            entity.LastDepreciationValue = serialMap.DepAbovePeriod;
                            entity.LastDepreciationBooked = serialMap?.BookedDepreciation;
                            entity.LastNetValue = serialMap.NetValue;
                            entity.LastFromDate = serialMap.DepFromDate;
                            entity.LastToDate = serialMap.DepToDate;

                            dbContext.Entry(entity).State = EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }

        public AssetSerialMapDTO GetAssetSerialDetailsByID(long serialMapID)
        {
            var assetSerialMap = new AssetSerialMapDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetSerialMaps.Where(x => x.AssetSerialMapIID == serialMapID)
                    .Include(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().FirstOrDefault();

                assetSerialMap = ToDTO(entity);
            }

            return assetSerialMap;
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsDetailsByAssetID(long assetID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate, long? branchID)
        {
            var assetSerialMapDTOs = new List<AssetSerialMapDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.AssetSerialMaps
                    .Where(x => x.AssetID == assetID && x.IsActive == true && x.AssetInventory.BranchID == branchID)
                    .Include(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    var transactionSerialMaps = dbContext.AssetTransactionSerialMaps.Where(x => x.AssetID == assetID && x.AssetSerialMapID == entity.AssetSerialMapIID)
                    .AsNoTracking().ToList();

                    decimal? totalAmount = 0;
                    if (transactionSerialMaps.Count > 0 && fiscalYearStartDate != null && fiscalYearEndDate != null)
                    {
                        var filteredSerialMaps = transactionSerialMaps
                            .Where(x => x.DepToDate >= fiscalYearStartDate && x.DepToDate <= fiscalYearEndDate && x.DepToDate.Value.Month != fiscalYearEndDate.Value.Month)
                            .ToList();

                        totalAmount = filteredSerialMaps.Count > 0 ? filteredSerialMaps.Sum(s => s.DepAbovePeriod.HasValue ? s.DepAbovePeriod : 0) : 0;
                    }

                    var serialMapDTO = ToDTO(entity);
                    serialMapDTO.PreviousEntriesDepAbovePeriodTotal = totalAmount;

                    assetSerialMapDTOs.Add(serialMapDTO);
                }
            }

            return assetSerialMapDTOs;
        }

        public List<KeyValueDTO> GetAssetSequencesByAssetID(long assetID)
        {
            var keyValueDTOs = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.AssetSerialMaps.Where(x => x.AssetID == assetID && x.IsActive == true)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    keyValueDTOs.Add(new KeyValueDTO()
                    {
                        Key = entity.AssetSerialMapIID.ToString(),
                        Value = entity.AssetSequenceCode
                    });
                }
            }

            return keyValueDTOs;
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetAndBranchID(long assetID, long branchID)
        {
            var assetSerialMapDTOs = new List<AssetSerialMapDTO>();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entities = dbContext.AssetSerialMaps.Where(x => x.AssetID == assetID && x.IsActive == true && x.AssetInventory.BranchID == branchID)
                    .Include(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    assetSerialMapDTOs.Add(ToDTO(entity));
                }
            }

            return assetSerialMapDTOs;
        }

        public AssetSerialMapDTO GetAssetSerialDetailsBySequenceCode(string assetSequenceCode)
        {
            var assetSerialMap = new AssetSerialMapDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetSerialMaps.Where(x => x.AssetSequenceCode == assetSequenceCode)
                    .Include(i => i.AssetInventory).ThenInclude(i => i.Branch)
                    .AsNoTracking().FirstOrDefault();

                if (entity != null)
                {
                    assetSerialMap = ToDTO(entity);
                }
            }

            return assetSerialMap;
        }

        public bool InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps)
        {
            bool isSuccess = false;
            List<long> assetSerialMapIIDs = new List<long>();

            foreach (var toDto in assetSerialMaps)
            {
                var entity = SaveAssetSerialMap(toDto);
                assetSerialMapIIDs.Add(entity.AssetSerialMapIID);
            }

            if (assetSerialMapIIDs.Count > 0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
        }

    }
}