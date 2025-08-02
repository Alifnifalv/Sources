using System.Linq;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Mappers.Accounts.Assets;
using Eduegate.Domain.Mappers.Accounts;
using System.Collections.Generic;
using System;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Setting;

namespace Eduegate.Domain.Accounts.Assets
{
    public class AssetBL
    {
        private CallContext _callContext;

        public AssetBL(CallContext context)
        {
            _callContext = context;
        }

        public decimal GetAccumulatedDepreciation(long assetId, int documentTypeID)
        {
            return new FixedAssetsRepository().GetAccumulatedDepreciation(assetId, documentTypeID);
        }

        public AssetInventoryDTO GetAssetInventoryDetail(long assetID, long branchID = 0, int companyID = 0)
        {
            var assetInventoryDTO = AssetInventoryMapper.Mapper(_callContext).GetAssetInventoryDetail(companyID > 0 ? companyID : _callContext.CompanyID.Value, assetID, branchID);

            return assetInventoryDTO;
        }

        public AssetTransactionDTO GetCalculatedDepreciationDetail(List<KeyValueDTO> categoryKeyValueDTOs, List<KeyValueDTO> assetKeyValueDTOs, int? month = null, int? year = null, long? branchID = null)
        {
            var assetTransactionDTO = new AssetTransactionDTO();
            assetTransactionDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
            var assetDetailDTOs = new List<AssetDTO>();

            var DepWDVTypeID = new SettingBL().GetSettingValue<int>("DEPRECIATION_TYPE_ID_WDV");
            var defaultDecimalPoints = new SettingBL().GetSettingValue<int>("DEFAULT_DECIMALPOINTS", 2);

            var fiscalYears = FinancialYearClosingMapper.Mapper(_callContext).GetFiscalYearDetails();

            var currentFiscalYear = fiscalYears.Where(x => month.Value >= x.StartDate.Value.Month && x.StartDate.Value.Year >= year.Value && month.Value <= x.EndDate.Value.Month && x.EndDate.Value.Year <= year.Value).FirstOrDefault();

            decimal fiscalYearMonthCount = currentFiscalYear != null ? currentFiscalYear?.StartDate != null && currentFiscalYear?.EndDate != null
                ? ((currentFiscalYear.EndDate.Value.Year - currentFiscalYear.StartDate.Value.Year) * 12)
                + (currentFiscalYear.EndDate.Value.Month - currentFiscalYear.StartDate.Value.Month + 1)
                : 12 : 12;

            if (assetKeyValueDTOs.Count == 1)
            {
                var keyValue = assetKeyValueDTOs.FirstOrDefault();

                var assetID = keyValue != null ? !string.IsNullOrEmpty(keyValue.Key) ? long.Parse(keyValue.Key) : 0 : 0;
                if (assetID == 0)
                {
                    var keyValueList = new List<KeyValueDTO>();
                    foreach (var assetCategory in categoryKeyValueDTOs)
                    {
                        long assetCategoryID = string.IsNullOrEmpty(assetCategory.Key) ? 0 : long.Parse(assetCategory.Key);

                        if (assetCategoryID > 0)
                        {
                            assetDetailDTOs = AssetMapper.Mapper(_callContext).GetAssetsDetailByCategoryAndBranch(assetCategoryID, branchID);
                        }
                        else
                        {
                            assetDetailDTOs = AssetMapper.Mapper(_callContext).GetFullActiveAssetsDetailByBranchID(branchID);
                        }
                    }
                }
                else
                {
                    var assetDetail = AssetMapper.Mapper(_callContext).GetAssetDetailsByID(assetID);

                    if (assetDetail != null)
                    {
                        assetDetailDTOs.Add(assetDetail);
                    }
                }
            }
            else
            {
                foreach (var assetKeyValue in assetKeyValueDTOs)
                {
                    var assetID = string.IsNullOrEmpty(assetKeyValue.Key) ? 0 : long.Parse(assetKeyValue.Key);
                    var assetDetail = assetID > 0 ? AssetMapper.Mapper(_callContext).GetAssetDetailsByID(assetID) : null;

                    if (assetDetail != null)
                    {
                        assetDetailDTOs.Add(assetDetail);
                    }
                }
            }

            foreach (var assetDetail in assetDetailDTOs)
            {
                var assetID = assetDetail.AssetIID;

                // For debugging purpose
                //if (assetID == 158)
                //{
                //}

                if (assetID <= 0)
                {
                    continue;
                }

                var detailVM = new AssetTransactionDetailsDTO()
                {
                    DetailIID = 0,
                    AssetID = assetID,
                    AssetCode = assetDetail?.AssetCode,
                    AssetName = assetDetail?.Description,
                    AssetGlAccID = assetDetail?.AssetGlAccID,
                    AccumulatedDepGLAccID = assetDetail?.AccumulatedDepGLAccID,
                    DepreciationExpGLAccID = assetDetail?.DepreciationExpGLAccID,
                    AssetCategoryID = assetDetail?.AssetCategoryID,
                    AssetCategoryName = assetDetail?.AssetCategoryName,
                };

                detailVM.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
                if (assetDetail.AssetSerialMapDTOs.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    foreach (var serialMap in assetDetail.AssetSerialMapDTOs)
                    {
                        // For debugging purpose
                        //if (serialMap.AssetSerialMapIID == 709)
                        //{
                        //}

                        if (serialMap.InventoryBranchID != branchID)
                        {
                            continue;
                        }

                        serialMap.PreviousEntriesDepAbovePeriodTotal = AssetTransactionSerialMapMapper.Mapper(_callContext).GetPreviousEntriesDepAbovePeriodTotalAmount(assetID, serialMap.AssetSerialMapIID, currentFiscalYear?.StartDate, currentFiscalYear?.EndDate);

                        var assetTransactionSerialMap = new AssetTransactionSerialMapDTO()
                        {
                            AssetTransactionSerialMapIID = 0,
                            Quantity = 1,
                            AssetID = assetID,
                            AssetSerialMapID = serialMap.AssetSerialMapIID,
                            AssetSequenceCode = serialMap.AssetSequenceCode,
                            PreviousAcculatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount.HasValue ? serialMap.AccumulatedDepreciationAmount : 0,
                        };

                        // Check the condition
                        DateTime? accumulatedDate;
                        DateTime? depStartDate;
                        if (serialMap.LastToDate.HasValue)
                        {
                            accumulatedDate = serialMap.LastToDate.Value;
                            depStartDate = accumulatedDate.Value.AddDays(1);
                        }
                        else if (serialMap.DateOfFirstUse.HasValue)
                        {
                            //accumulatedDate = serialMap.DateOfFirstUse.Value.AddDays(-1);
                            accumulatedDate = null;
                            depStartDate = serialMap.DateOfFirstUse.Value;
                        }
                        else
                        {
                            // Take the current month's first date
                            //accumulatedDate = new DateTime(now.Year, now.Month, 1);
                            accumulatedDate = null;
                            depStartDate = accumulatedDate;
                        }

                        // Get the number of days in the specified month
                        int daysInMonth = DateTime.DaysInMonth(depStartDate.Value.Year, depStartDate.Value.Month);

                        // Get the number of days in the year
                        int daysInYear = DateTime.IsLeapYear(depStartDate.Value.Year) ? 366 : 365;

                        // Get the last day of the month
                        DateTime depEndDate = new DateTime(depStartDate.Value.Year, depStartDate.Value.Month, daysInMonth);

                        serialMap.DepreciationRate = serialMap.DepreciationRate.HasValue ? serialMap.DepreciationRate.Value : serialMap.ExpectedLife.HasValue ? (100 / serialMap.ExpectedLife) : null;

                        if (!serialMap.DepreciationRate.HasValue)
                        {
                            continue;
                        }

                        // Check if either depStartDate or depEndDate is in the future compared to the current date (now).
                        // Condition 1: Check if depStartDate is in a future year OR in the same year but a future month
                        // Condition 2: Check if depEndDate is in a future year OR in the same year but a future month
                        if ((depStartDate.Value.Year > now.Year || (depStartDate.Value.Year == now.Year && depStartDate.Value.Month > now.Month)) ||
                            (depEndDate.Year > now.Year || (depEndDate.Year == now.Year && depEndDate.Month > now.Month)))
                        {
                            // If either depStartDate or depEndDate is in the future, skip this iteration and continue to the next loop cycle
                            continue;
                        }

                        if (depEndDate.Month != month || depEndDate.Year != year)
                        {
                            continue;
                        }

                        // Assigning the values for the variables in alphabetical order
                        decimal costAmount = serialMap.CostPrice.HasValue ? serialMap.CostPrice.Value : 0; // Cost Amount (Net)
                        decimal depRatePercentage = ((serialMap.DepreciationRate.Value * fiscalYearMonthCount) / 12) / 100; // Rate of Depreciation (20% as decimal)
                        decimal expScrapVal = serialMap.ExpectedScrapValue.HasValue ? serialMap.ExpectedScrapValue.Value : 0; // Expected Net Realisable Scrap Value
                        decimal accumDepAmount = serialMap.AccumulatedDepreciationAmount.HasValue ? serialMap.AccumulatedDepreciationAmount.Value : 0;    // Accumulated Depreciation
                        decimal acntPeriodDays = daysInYear;// Number of Days in the Accounting Period - daysInYear (365/366)
                        DateTime? depAccumTillDate = accumulatedDate; //Dep. Accumulated till date
                        DateTime? depFromDate = depStartDate; // Start Date of Depreciation (adjust days if needed) - Dep from
                        DateTime? depToDate = depEndDate;// Dep till date

                        // Calculate the number of days for DateTime differences
                        //decimal depToAndAccumTillDateDayDiffer = (decimal)(depToDate - depAccumTillDate).Value.TotalDays;
                        decimal depToAndDepStartDateDayDiffer = (decimal)(depToDate - depFromDate).Value.TotalDays + 1;

                        decimal depResult = 0;
                        if (currentFiscalYear != null && currentFiscalYear.EndDate.HasValue)
                        {
                            if (assetDetail.DepreciationTypeID == DepWDVTypeID)
                            {
                                // Retrieve the opening accumulated depreciation amount for the fiscal year.
                                var fiscalYearOpeningAmount = AssetTransactionSerialMapMapper.Mapper(_callContext).GetPreviousAcculatedDepreciationAmount(serialMap.AssetSerialMapIID, currentFiscalYear?.StartDate);

                                decimal calculatedDepreciation = (costAmount - expScrapVal - fiscalYearOpeningAmount) * depRatePercentage * depToAndDepStartDateDayDiffer / acntPeriodDays;
                                decimal remainingDepreciableValue = costAmount - expScrapVal - accumDepAmount;
                                if (calculatedDepreciation < remainingDepreciableValue)
                                {
                                    depResult = calculatedDepreciation;
                                }
                                else
                                {
                                    depResult = remainingDepreciableValue;
                                }
                            }
                            else
                            {
                                if (currentFiscalYear.EndDate.Value.Month == month && currentFiscalYear.EndDate.Value.Year == year && serialMap.PreviousEntriesDepAbovePeriodTotal > 0)
                                {
                                    decimal yearDepreciation;

                                    if (serialMap.DateOfFirstUse.HasValue && serialMap.DateOfFirstUse.Value.Month > currentFiscalYear.StartDate.Value.Month && serialMap.DateOfFirstUse.Value.Year >= currentFiscalYear.StartDate.Value.Year)
                                    {
                                        // Depreciation for a this asset starts in the middle of the fiscal year, so the yearly depreciation value is calculated based on the number of days.
                                        decimal dayDifference = (decimal)(currentFiscalYear.EndDate.Value - serialMap.DateOfFirstUse.Value).TotalDays + 1;
                                        yearDepreciation = (costAmount - expScrapVal) * depRatePercentage * dayDifference / acntPeriodDays;
                                    }
                                    else
                                    {
                                        // Full year depreciation
                                        yearDepreciation = (costAmount - expScrapVal) * depRatePercentage;
                                    }

                                    decimal calculatedDepreciation = yearDepreciation - (serialMap.PreviousEntriesDepAbovePeriodTotal.HasValue ? serialMap.PreviousEntriesDepAbovePeriodTotal.Value : 0);
                                    decimal remainingDepreciableValue = costAmount - expScrapVal - accumDepAmount;

                                    if (calculatedDepreciation < remainingDepreciableValue)
                                    {
                                        depResult = calculatedDepreciation;
                                    }
                                    else
                                    {
                                        depResult = remainingDepreciableValue;
                                    }
                                }
                                else
                                {
                                    decimal calculatedDepreciation = (costAmount - expScrapVal) * depRatePercentage * depToAndDepStartDateDayDiffer / acntPeriodDays;
                                    decimal remainingDepreciableValue = costAmount - expScrapVal - accumDepAmount;

                                    if (calculatedDepreciation < remainingDepreciableValue)
                                    {
                                        depResult = calculatedDepreciation;
                                    }
                                    else
                                    {
                                        depResult = remainingDepreciableValue;
                                    }
                                }
                            }
                        }

                        //Add values to DTO
                        assetTransactionSerialMap.DepAccumulatedTillDate = accumulatedDate;
                        assetTransactionSerialMap.DepFromDate = depFromDate;
                        assetTransactionSerialMap.DepToDate = depEndDate;
                        assetTransactionSerialMap.AccountingPeriodDays = (int)acntPeriodDays;
                        assetTransactionSerialMap.DepAbovePeriod = Math.Round(depResult, defaultDecimalPoints, MidpointRounding.AwayFromZero);
                        assetTransactionSerialMap.BookedDepreciation = Math.Round(depResult, defaultDecimalPoints, MidpointRounding.AwayFromZero);
                        assetTransactionSerialMap.AccumulatedDepreciationAmount = Math.Round((accumDepAmount + assetTransactionSerialMap.BookedDepreciation.Value), defaultDecimalPoints, MidpointRounding.AwayFromZero);
                        assetTransactionSerialMap.NetValue = Math.Round((costAmount - assetTransactionSerialMap.AccumulatedDepreciationAmount.Value), defaultDecimalPoints, MidpointRounding.AwayFromZero);

                        assetTransactionSerialMap.DepAbovePeriod = assetTransactionSerialMap.DepAbovePeriod > 0 ? assetTransactionSerialMap.DepAbovePeriod : 0;
                        assetTransactionSerialMap.BookedDepreciation = assetTransactionSerialMap.BookedDepreciation > 0 ? assetTransactionSerialMap.BookedDepreciation : 0;
                        assetTransactionSerialMap.AccumulatedDepreciationAmount = assetTransactionSerialMap.AccumulatedDepreciationAmount > 0 ? assetTransactionSerialMap.AccumulatedDepreciationAmount : 0;
                        assetTransactionSerialMap.NetValue = assetTransactionSerialMap.NetValue > 0 ? assetTransactionSerialMap.NetValue : 0;

                        detailVM.AssetTransactionSerialMaps.Add(assetTransactionSerialMap);
                    }
                }

                if (detailVM.AssetTransactionSerialMaps.Count > 0)
                {
                    detailVM.Quantity = detailVM.AssetTransactionSerialMaps.Count();
                    detailVM.DepAccumulatedTillDate = detailVM.AssetTransactionSerialMaps?.FirstOrDefault()?.DepAccumulatedTillDate;
                    detailVM.DepFromDate = detailVM.AssetTransactionSerialMaps?.FirstOrDefault()?.DepFromDate;
                    detailVM.DepToDate = detailVM.AssetTransactionSerialMaps?.FirstOrDefault()?.DepToDate;
                    detailVM.AccountingPeriodDays = detailVM.AssetTransactionSerialMaps?.FirstOrDefault()?.AccountingPeriodDays;
                    detailVM.DepAbovePeriod = detailVM.AssetTransactionSerialMaps?.Sum(x => x.DepAbovePeriod);
                    detailVM.BookedDepreciation = detailVM.AssetTransactionSerialMaps?.Sum(x => x.BookedDepreciation);
                    detailVM.AccumulatedDepreciationAmount = detailVM.AssetTransactionSerialMaps?.Sum(x => x.AccumulatedDepreciationAmount);
                    detailVM.NetValue = detailVM.AssetTransactionSerialMaps?.Sum(x => x.NetValue);
                    detailVM.PreviousAcculatedDepreciationAmount = detailVM.AssetTransactionSerialMaps?.Sum(x => x.PreviousAcculatedDepreciationAmount);

                    assetTransactionDTO.AssetTransactionDetails.Add(detailVM);
                }
            }

            // The transaction details are ordered first by Asset Category, then by Asset.
            assetTransactionDTO.AssetTransactionDetails = assetTransactionDTO.AssetTransactionDetails.Count > 0 ? 
                assetTransactionDTO.AssetTransactionDetails.OrderBy(o => o.AssetCategoryID).ThenBy(x => x.AssetID).ToList() : assetTransactionDTO.AssetTransactionDetails;

            return assetTransactionDTO;
        }

    }
}