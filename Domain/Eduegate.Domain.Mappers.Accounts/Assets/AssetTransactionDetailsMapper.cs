using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Services.Contracts.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetTransactionDetailsMapper : IDTOEntityMapper<AssetTransactionDetailsDTO, AssetTransactionDetail>
    {
        private CallContext _context;

        public static AssetTransactionDetailsMapper Mapper(CallContext context)
        {
            var mapper = new AssetTransactionDetailsMapper();
            mapper._context = context;
            return mapper;
        }

        public AssetTransactionDetail ToEntity(AssetTransactionDetailsDTO dto)
        {
            if (dto != null)
            {
                var entity = new AssetTransactionDetail()
                {
                    DetailIID = dto.DetailIID,
                    HeadID = dto.HeadID,
                    AssetID = dto.AssetID,
                    Quantity = dto.Quantity,
                    Amount = dto.Amount,
                    AccountID = dto.AccountID,
                    AssetGlAccID = dto.AssetGlAccID,
                    AccumulatedDepGLAccID = dto.AccumulatedDepGLAccID,
                    DepreciationExpGLAccID = dto.DepreciationExpGLAccID,
                    CostAmount = dto.CostAmount,
                    AccountingPeriodDays = dto.AccountingPeriodDays,
                    DepAccumulatedTillDate = dto.DepAccumulatedTillDate,
                    DepFromDate = dto.DepFromDate,
                    DepToDate = dto.DepToDate,
                    DepAbovePeriod = dto.DepAbovePeriod,
                    BookedDepreciation = dto.BookedDepreciation,
                    AccumulatedDepreciationAmount = dto.AccumulatedDepreciationAmount,
                    CutOffDate = dto.CutOffDate,
                    NetValue = dto.NetValue,
                    PreviousAcculatedDepreciationAmount = dto.PreviousAcculatedDepreciationAmount,
                    ProductSKUMapID = dto.ProductSKUMapID,
                };

                FillAssetAccounts(entity);

                if (dto.DetailIID <= 0)
                {
                    entity.CreatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? (int)_context.LoginID : (int?)null) : null;
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.CreatedBy = dto.CreatedBy;
                    entity.CreatedDate = dto.CreatedDate;
                    entity.UpdatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? (int)_context.LoginID : (int?)null) : null;
                    entity.UpdatedDate = DateTime.Now;
                }

                entity.AssetTransactionSerialMaps = new List<AssetTransactionSerialMap>();
                foreach (var serialMapDTO in dto.AssetTransactionSerialMaps)
                {
                    var serialMapEntity = AssetTransactionSerialMapMapper.Mapper(_context).ToEntity(serialMapDTO);

                    entity.AssetTransactionSerialMaps.Add(serialMapEntity);
                }

                return entity;
            }

            else return new AssetTransactionDetail();
        }

        public AssetTransactionDetailsDTO ToDTO(AssetTransactionDetail entity)
        {
            return ToDTO(entity, _context.IsNotNull() ? _context.CompanyID.Value : default(int));
        }

        public AssetTransactionDetailsDTO ToDTO(AssetTransactionDetail entity, long? fromBranchID = null)
        {
            return ToDTO(entity, _context.IsNotNull() ? _context.CompanyID.Value : default(int), fromBranchID);
        }

        public AssetTransactionDetailsDTO ToDTO(AssetTransactionDetail entity, int? companyID, long? fromBranchID = null)
        {
            if (entity != null)
            {
                var transactionDetailDTO = new AssetTransactionDetailsDTO()
                {
                    DetailIID = entity.DetailIID,
                    HeadID = entity.HeadID,
                    AssetID = entity.AssetID,
                    Quantity = entity.Quantity,
                    Amount = entity.Amount,
                    AccountID = entity.AccountID,
                    AssetGlAccID = entity.AssetGlAccID,
                    AccumulatedDepGLAccID = entity.AccumulatedDepGLAccID,
                    DepreciationExpGLAccID = entity.DepreciationExpGLAccID,
                    CostAmount = entity.CostAmount,
                    AccountingPeriodDays = entity.AccountingPeriodDays,
                    DepAccumulatedTillDate = entity.DepAccumulatedTillDate,
                    DepFromDate = entity.DepFromDate,
                    DepToDate = entity.DepToDate,
                    DepAbovePeriod = entity.DepAbovePeriod,
                    BookedDepreciation = entity.BookedDepreciation,
                    AccumulatedDepreciationAmount = entity.AccumulatedDepreciationAmount,
                    CutOffDate = entity.CutOffDate,
                    NetValue = entity.NetValue,
                    PreviousAcculatedDepreciationAmount = entity.PreviousAcculatedDepreciationAmount,
                    ProductSKUMapID = entity.ProductSKUMapID,
                };

                GetAndFillDetails(transactionDetailDTO, fromBranchID);

                transactionDetailDTO.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
                transactionDetailDTO.AssetTransactionSerialMaps = entity.AssetTransactionSerialMaps.Select(x => AssetTransactionSerialMapMapper.Mapper(_context).ToDTO(x)).ToList();

                //var skuSettingDetail = entity.ProductSKUMapID.IsNotNull() && (long)entity.ProductSKUMapID > 0 ?
                //    new ProductCatalogRepository().GetProductSKUDetail(entity.ProductSKUMapID.Value) : new ProductSKUDetail();

                //if (skuSettingDetail.IsNotNull())
                //{
                //    transactionDetailDTO.IsSerialNumber = skuSettingDetail.IsSerialNumber != null ? Convert.ToBoolean(skuSettingDetail.IsSerialNumber) : false;
                //    transactionDetailDTO.IsSerialNumberOnPurchase = skuSettingDetail.IsSerialNumberOnPurchase != null ? Convert.ToBoolean(skuSettingDetail.IsSerialNumberOnPurchase) : false;

                //    transactionDetailDTO.ProductLength = skuSettingDetail.ProductTypeName == Framework.Enums.ProductTypes.Digital.ToString() ? (skuSettingDetail.ProductLength != null ? Convert.ToInt32(skuSettingDetail.ProductLength) : 0) : 0;
                //    transactionDetailDTO.ProductTypeName = skuSettingDetail.ProductTypeName;
                //}

                //var settingRepository = new SettingRepository();
                //var securityRepository = new SecurityRepository();
                //var hasFullReadClaim = false;
                //try
                //{
                //    var settingValue = settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM)?.SettingValue;

                //    hasFullReadClaim = (_context.IsNotNull() && _context.LoginID.IsNotNull()) ? settingValue != null ? securityRepository.HasClaimAccess(long.Parse(settingValue), _context.LoginID.Value) : false : false;
                //}
                //catch (Exception) { }

                //var hasPartialReadClaim = false;
                //try
                //{
                //    var settingValue = settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM)?.SettingValue;

                //    hasPartialReadClaim = (_context.IsNotNull() && _context.LoginID.IsNotNull()) ? settingValue != null ? securityRepository.HasClaimAccess(long.Parse(settingValue), _context.LoginID.Value) : false : false;
                //}
                //catch (Exception) { }
                //var visibleSerialKey = "";
                //if (entity.SerialNumber.IsNotNull())
                //{
                //    if (hasFullReadClaim)
                //        visibleSerialKey = entity.SerialNumber;
                //    else if (hasPartialReadClaim)
                //    {
                //        var length = entity.SerialNumber.Length;

                //        if (length <= 4)
                //        {
                //            visibleSerialKey = new String('x', length);
                //        }
                //        else
                //        {
                //            visibleSerialKey = new String('x', length - 4) + entity.SerialNumber.Substring(length - 4);
                //        }
                //        entity.SerialNumber = visibleSerialKey;
                //    }
                //    else
                //        visibleSerialKey = "";
                //}
                //transactionDetailDTO.SerialNumber = entity.SerialNumber.IsNotNull() ? visibleSerialKey : null;

                transactionDetailDTO.CreatedBy = entity.CreatedBy;
                transactionDetailDTO.UpdatedBy = entity.UpdatedBy;
                transactionDetailDTO.CreatedDate = entity.CreatedDate;
                transactionDetailDTO.UpdatedDate = entity.UpdatedDate;

                return transactionDetailDTO;
            }

            else return new AssetTransactionDetailsDTO();
        }

        public void FillAssetAccounts(AssetTransactionDetail entity)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var oldTransDetail = entity.DetailIID > 0 ? dbContext.AssetTransactionDetails.Where(x => x.DetailIID == entity.DetailIID).AsNoTracking().FirstOrDefault() : null;

                var assetData = dbContext.Assets.Where(y => y.AssetIID == entity.AssetID).AsNoTracking().FirstOrDefault();

                if (oldTransDetail != null && oldTransDetail.AssetID != entity.AssetID)
                {
                    entity.AssetGlAccID = assetData?.AssetGlAccID;
                    entity.AccumulatedDepGLAccID = assetData?.AccumulatedDepGLAccID;
                    entity.DepreciationExpGLAccID = assetData?.DepreciationExpGLAccId;
                }

                entity.AssetGlAccID = entity.AssetGlAccID.HasValue ? entity.AssetGlAccID : assetData?.AssetGlAccID;
                entity.AccumulatedDepGLAccID = entity.AccumulatedDepGLAccID.HasValue ? entity.AccumulatedDepGLAccID : assetData?.AccumulatedDepGLAccID;
                entity.DepreciationExpGLAccID = entity.DepreciationExpGLAccID.HasValue ? entity.DepreciationExpGLAccID : assetData?.DepreciationExpGLAccId;
            }
            
            entity.AccountID = entity.AccountID;
        }

        public void GetAndFillDetails(AssetTransactionDetailsDTO dto, long? fromBranchID = null)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var assetData = dbContext.Assets.Where(a => a.AssetIID == dto.AssetID)
                    .Include(i => i.AssetCategory)
                    .Include(i => i.AssetInventories)
                    .AsNoTracking().FirstOrDefault();

                var assetListData = dbContext.Assets.Where(a => a.AssetCategoryID == assetData.AssetCategoryID).AsNoTracking().ToList();

                if (assetData != null)
                {
                    dto.AssetName = assetData?.Description;
                    dto.AssetCode = assetData?.AssetCode;
                    dto.IsRequiredSerialNumber = assetData?.IsRequiredSerialNumber ?? false;
                    dto.AssetCategoryID = assetData?.AssetCategoryID ?? null;
                    dto.AssetCategoryName = assetData?.AssetCategory?.CategoryName ?? null;

                    if (fromBranchID.HasValue)
                    {
                        var branchInventories = assetData.AssetInventories.Count > 0 ? assetData.AssetInventories.Where(x => x.BranchID == fromBranchID).ToList() : null;

                        dto.AvailableAssetQuantity = branchInventories != null ? branchInventories.Sum(x => x.Quantity) : 0;
                    }
                }                
                dto.AssetCategoryTotalAssetCount = assetListData?.Count ?? 0;

                var productSKU = dbContext.ProductSKUMaps.Where(p => p.ProductSKUMapIID == dto.ProductSKUMapID).AsNoTracking().FirstOrDefault();
                dto.SKU = productSKU?.SKUName;

                //var serialMap = dbContext.AssetSerialMaps.Where(s => s.AssetSerialMapIID == dto.AssetSerialMapID).AsNoTracking().FirstOrDefault();
                //dto.AssetSerialMapSequenceCode = serialMap?.AssetSequenceCode;
            }
        }

    }
}
