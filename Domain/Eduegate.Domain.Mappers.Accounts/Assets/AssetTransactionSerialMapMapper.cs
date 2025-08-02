using System;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounts.Assets;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetTransactionSerialMapMapper : IDTOEntityMapper<AssetTransactionSerialMapDTO, AssetTransactionSerialMap>
    {
        private CallContext _context;

        public static AssetTransactionSerialMapMapper Mapper(CallContext context)
        {
            var mapper = new AssetTransactionSerialMapMapper();
            mapper._context = context;
            return mapper;
        }

        public AssetTransactionSerialMap ToEntity(AssetTransactionSerialMapDTO dto)
        {
            var entity = new AssetTransactionSerialMap();

            if (dto != null)
            {
                if (dto.AssetTransactionSerialMapIID == 0 && !dto.AssetSerialMapID.HasValue)
                {
                    dto.AssetSequenceCode = AssetMapper.Mapper(_context).GetNextAssetTransactionNumberByID(dto.AssetID.Value);
                }

                entity = new AssetTransactionSerialMap
                {
                    AssetTransactionSerialMapIID = dto.AssetTransactionSerialMapIID,
                    TransactionDetailID = dto.TransactionDetailID,
                    AssetSerialMapID = dto.AssetSerialMapID,
                    AssetID = dto.AssetID,
                    AssetSequenceCode = dto.AssetSequenceCode,
                    SerialNumber = dto.SerialNumber,
                    AssetTag = dto.AssetTag,
                    CostPrice = dto.CostPrice,
                    ExpectedLife = dto.ExpectedLife,
                    DepreciationRate = dto.DepreciationRate,
                    ExpectedScrapValue = dto.ExpectedScrapValue,
                    AccumulatedDepreciationAmount = dto.AccumulatedDepreciationAmount,
                    DateOfFirstUse = dto.DateOfFirstUse,
                    SupplierID = dto.SupplierID,
                    BillNumber = dto.BillNumber,
                    BillDate = dto.BillDate,
                    AccountingPeriodDays = dto.AccountingPeriodDays,
                    DepAccumulatedTillDate = dto.DepAccumulatedTillDate,
                    DepFromDate = dto.DepFromDate,
                    DepToDate = dto.DepToDate,
                    DepAbovePeriod = dto.DepAbovePeriod,
                    BookedDepreciation = dto.BookedDepreciation,
                    PreviousAcculatedDepreciationAmount = dto.PreviousAcculatedDepreciationAmount,
                    NetValue = dto.NetValue,
                    DifferenceAmount = dto.DifferenceAmount,
                    DisposibleAmount = dto.DisposibleAmount,
                    LastDepreciationDate = dto.LastDepreciationDate,
                };

                if (dto.AssetTransactionSerialMapIID <= 0)
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
            }

            return entity;
        }

        public AssetTransactionSerialMapDTO ToDTO(AssetTransactionSerialMap entity)
        {
            var serialMapDTO = new AssetTransactionSerialMapDTO();

            if (entity != null)
            {
                serialMapDTO = new AssetTransactionSerialMapDTO()
                {
                    AssetTransactionSerialMapIID = entity.AssetTransactionSerialMapIID,
                    TransactionDetailID = entity.TransactionDetailID,
                    AssetSerialMapID = entity.AssetSerialMapID,
                    AssetID = entity.AssetID,
                    AssetSequenceCode = entity.AssetSequenceCode,
                    SerialNumber = entity.SerialNumber,
                    AssetTag = entity.AssetTag,
                    CostPrice = entity.CostPrice,
                    ExpectedLife = entity.ExpectedLife,
                    DepreciationRate = entity.DepreciationRate,
                    ExpectedScrapValue = entity.ExpectedScrapValue,
                    AccumulatedDepreciationAmount = entity.AccumulatedDepreciationAmount,
                    DateOfFirstUse = entity.DateOfFirstUse,
                    SupplierID = entity.SupplierID,
                    BillNumber = entity.BillNumber,
                    BillDate = entity.BillDate,
                    AccountingPeriodDays = entity.AccountingPeriodDays,
                    DepAccumulatedTillDate = entity.DepAccumulatedTillDate,
                    DepFromDate = entity.DepFromDate,
                    DepToDate = entity.DepToDate,
                    DepAbovePeriod = entity.DepAbovePeriod,
                    BookedDepreciation = entity.BookedDepreciation,
                    PreviousAcculatedDepreciationAmount = entity.PreviousAcculatedDepreciationAmount,
                    NetValue = entity.NetValue,
                    DifferenceAmount = entity.DifferenceAmount,
                    DisposibleAmount = entity.DisposibleAmount,
                    LastDepreciationDate = entity.LastDepreciationDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                if (entity.SupplierID.HasValue && entity.Supplier != null)
                {
                    serialMapDTO.SupplierName = entity.Supplier.SupplierCode + " - " + entity.Supplier.FirstName + " " +
                        (string.IsNullOrEmpty(entity.Supplier.MiddleName) ? "" : entity.Supplier.MiddleName + " ") + entity.Supplier.LastName;
                }

                if (string.IsNullOrEmpty(serialMapDTO.AssetSerialMapSequenceCode))
                {
                    var serialDetails = serialMapDTO.AssetSerialMapID.HasValue ? AssetSerialMapMapper.Mapper(_context).GetAssetSerialDetailsByID(serialMapDTO.AssetSerialMapID.Value) : null;

                    serialMapDTO.AssetSerialMapSequenceCode = serialDetails?.AssetSequenceCode;
                }
                //else
                //{
                //    if (!entity.AssetSerialMapID.HasValue)
                //    {
                //        AssetSerialMapMapper.Mapper(_context).GetAssetSerialDetailsBySequenceCode(serialMapDTO.AssetSequenceCode);
                //    }
                //}
            }

            return serialMapDTO;
        }

        public decimal GetPreviousAcculatedDepreciationAmount(long assetSerialMapID, DateTime? fiscalYearStartDate)
        {
            decimal lastDepAmount = 0;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetTransactionSerialMaps
                    .Where(x => x.AssetSerialMapID == assetSerialMapID && x.DepToDate.Value.Month == fiscalYearStartDate.Value.Month
                    && x.DepToDate.Value.Year == fiscalYearStartDate.Value.Year)
                    .AsNoTracking().FirstOrDefault();

                lastDepAmount = entity != null ? entity.PreviousAcculatedDepreciationAmount.Value : 0;
            }

            return lastDepAmount;
        }

        public decimal GetPreviousEntriesDepAbovePeriodTotalAmount(long assetID, long assetSerialMapID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate)
        {
            decimal totalAmount = 0;
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var transactionSerialMaps = dbContext.AssetTransactionSerialMaps.Where(x => x.AssetID == assetID && x.AssetSerialMapID == assetSerialMapID)
                .AsNoTracking().ToList();

                if (transactionSerialMaps.Count > 0 && fiscalYearStartDate != null && fiscalYearEndDate != null)
                {
                    var filteredSerialMaps = transactionSerialMaps
                        .Where(x => x.DepToDate >= fiscalYearStartDate && x.DepToDate <= fiscalYearEndDate && x.DepToDate.Value.Month != fiscalYearEndDate.Value.Month)
                        .ToList();

                    totalAmount = Convert.ToDecimal(filteredSerialMaps.Count > 0 ? filteredSerialMaps.Sum(s => s.DepAbovePeriod.HasValue ? s.DepAbovePeriod : 0) : 0);
                }
            }

            return totalAmount;
        }

    }
}