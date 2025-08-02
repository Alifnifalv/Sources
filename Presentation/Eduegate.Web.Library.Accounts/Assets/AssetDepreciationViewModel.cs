using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.Accounts.Assets
{
    public class AssetDepreciationViewModel : BaseMasterViewModel
    {
        public AssetDepreciationViewModel()
        {
            MasterViewModel = new AssetDepreciationMasterViewModel();
            DetailViewModel = new List<AssetDepreciationDetailViewModel>() { new AssetDepreciationDetailViewModel() };
        }

        public AssetDepreciationMasterViewModel MasterViewModel { get; set; }
        public List<AssetDepreciationDetailViewModel> DetailViewModel { get; set; }

        public static AssetDepreciationViewModel ToVM(AssetTransactionDTO dto)
        {
            if (dto.IsNotNull())
            {
                var ad = new AssetDepreciationViewModel();
                ad.MasterViewModel = new AssetDepreciationMasterViewModel();
                ad.DetailViewModel = new List<AssetDepreciationDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                ad.MasterViewModel.TransactionHeadIID = dto.AssetTransactionHead.HeadIID;
                ad.MasterViewModel.Branch = dto.AssetTransactionHead.BranchID.HasValue ? new KeyValueViewModel()
                {
                    Key = dto.AssetTransactionHead.BranchID.ToString(),
                    Value = dto.AssetTransactionHead.BranchName
                } : new KeyValueViewModel();
                ad.MasterViewModel.DocumentType = dto.AssetTransactionHead.DocumentTypeID.HasValue ? new KeyValueViewModel()
                {
                    Key = dto.AssetTransactionHead.DocumentTypeID.ToString(),
                    Value = dto.AssetTransactionHead.DocumentTypeName
                } : new KeyValueViewModel();
                ad.MasterViewModel.TransactionNo = dto.AssetTransactionHead.TransactionNo;
                ad.MasterViewModel.TransactionDateString = dto.AssetTransactionHead.EntryDate != null ? Convert.ToDateTime(dto.AssetTransactionHead.EntryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                ad.MasterViewModel.DocumentStatus = dto.AssetTransactionHead.DocumentStatusID.HasValue ? new KeyValueViewModel()
                {
                    Key = dto.AssetTransactionHead.DocumentStatusID.ToString(),
                    Value = dto.AssetTransactionHead.DocumentStatusName
                } : new KeyValueViewModel();                
                ad.MasterViewModel.Remarks = dto.AssetTransactionHead.Remarks;
                ad.MasterViewModel.IsTransactionCompleted = dto.AssetTransactionHead.IsTransactionCompleted;
                ad.MasterViewModel.CompanyID = dto.AssetTransactionHead.CompanyID;
                ad.MasterViewModel.DocumentReferenceTypeID = dto.AssetTransactionHead.DocumentReferenceTypeID;

                if (dto.AssetTransactionHead.DepreciationStartDate.HasValue)
                {
                    ad.MasterViewModel.DepreciationMonth = dto.AssetTransactionHead.DepreciationStartDate.Value.Month.ToString();
                    ad.MasterViewModel.DepreciationYear = dto.AssetTransactionHead.DepreciationStartDate.Value.Year.ToString();
                }
                else if (dto.AssetTransactionHead.DepreciationEndDate.HasValue)
                {
                    ad.MasterViewModel.DepreciationMonth = dto.AssetTransactionHead.DepreciationEndDate.Value.Month.ToString();
                    ad.MasterViewModel.DepreciationYear = dto.AssetTransactionHead.DepreciationEndDate.Value.Year.ToString();
                }

                ad.MasterViewModel.CreatedBy = dto.AssetTransactionHead.CreatedBy;
                ad.MasterViewModel.CreatedDate = dto.AssetTransactionHead.CreatedDate;
                ad.MasterViewModel.UpdatedBy = dto.AssetTransactionHead.UpdatedBy;
                ad.MasterViewModel.UpdatedDate = dto.AssetTransactionHead.UpdatedDate;

                if (dto.AssetTransactionDetails != null && dto.AssetTransactionDetails.Count > 0)
                {
                    var firstAssetDet = dto.AssetTransactionDetails.FirstOrDefault();
                    long detCount = 0;
                    if (firstAssetDet != null)
                    {
                        //ad.MasterViewModel.AssetCategory = firstAssetDet.AssetCategoryID.HasValue ? new KeyValueViewModel()
                        //{
                        //    Key = firstAssetDet.AssetCategoryID.ToString(),
                        //    Value = firstAssetDet.AssetCategoryName,
                        //} : new KeyValueViewModel();

                        detCount = dto.AssetTransactionDetails.Where(x => x.AssetCategoryID == firstAssetDet.AssetCategoryID).Count();
                    }

                    ad.MasterViewModel.Assets = new List<KeyValueViewModel>();

                    if (firstAssetDet.AssetCategoryTotalAssetCount == detCount)
                    {
                        ad.MasterViewModel.Assets.Add(new KeyValueViewModel()
                        {
                            Key = "0",
                            Value = "ALL",
                        });
                    }

                    foreach (var transactionDetail in dto.AssetTransactionDetails)
                    {
                        //if (ad.MasterViewModel.Assets.Count == 0)
                        //{
                        //    if (transactionDetail.AssetID.HasValue)
                        //    {
                        //        ad.MasterViewModel.Assets.Add(new KeyValueViewModel()
                        //        {
                        //            Key = transactionDetail.AssetID.ToString(),
                        //            Value = transactionDetail.AssetName,
                        //        });
                        //    }
                        //}

                        var atDetail = new AssetDepreciationDetailViewModel();

                        atDetail.TransactionDetailIID = transactionDetail.DetailIID;
                        atDetail.TransactionHeadID = transactionDetail.HeadID;
                        atDetail.Quantity = transactionDetail.Quantity.HasValue ? transactionDetail.Quantity : 0;
                        atDetail.AssetID = transactionDetail.AssetID;
                        atDetail.AssetCode = transactionDetail.AssetCode;
                        atDetail.AssetDescription = transactionDetail.AssetName;
                        atDetail.AssetCategory = transactionDetail.AssetCategoryName;
                        atDetail.AssetGlAccID = transactionDetail?.AssetGlAccID;
                        atDetail.AccumulatedDepGLAccID = transactionDetail?.AccumulatedDepGLAccID;
                        atDetail.DepreciationExpGLAccID = transactionDetail?.DepreciationExpGLAccID;
                        atDetail.AccountingPeriodDays = transactionDetail.AccountingPeriodDays;
                        atDetail.AccumulatedTillDateString = transactionDetail.DepAccumulatedTillDate.HasValue ? transactionDetail.DepAccumulatedTillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        atDetail.DepFromDateString = transactionDetail.DepFromDate.HasValue ? transactionDetail.DepFromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        atDetail.DepToDateString = transactionDetail.DepToDate.HasValue ? transactionDetail.DepToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        atDetail.DepAbovePeriod = transactionDetail.DepAbovePeriod;
                        atDetail.BookedDepreciation = transactionDetail.BookedDepreciation;
                        atDetail.AccumulatedDepreciationAmount = transactionDetail.AccumulatedDepreciationAmount;
                        atDetail.NetValue = transactionDetail.NetValue;
                        atDetail.Amount = transactionDetail.Amount;
                        atDetail.PreviousAcculatedDepreciationAmount = transactionDetail.PreviousAcculatedDepreciationAmount;

                        atDetail.CreatedBy = transactionDetail.CreatedBy;
                        atDetail.CreatedDate = transactionDetail.CreatedDate;
                        atDetail.UpdatedBy = transactionDetail.UpdatedBy;
                        atDetail.UpdatedDate = transactionDetail.UpdatedDate;

                        atDetail.TransactionSerialMaps = new List<AssetDepreciationDetailSerialMapViewModel>();
                        if (transactionDetail.AssetTransactionSerialMaps != null && transactionDetail.AssetTransactionSerialMaps.Count > 0)
                        {
                            foreach (var transSerialMap in transactionDetail.AssetTransactionSerialMaps)
                            {
                                atDetail.TransactionSerialMaps.Add(new AssetDepreciationDetailSerialMapViewModel()
                                {
                                    AssetTransactionSerialMapIID = transSerialMap.AssetTransactionSerialMapIID,
                                    TransactionDetailID = transSerialMap.TransactionDetailID,
                                    AssetID = transSerialMap.AssetID,
                                    AssetSerialMapID = transSerialMap.AssetSerialMapID,
                                    AssetSequenceCode = transSerialMap.AssetSequenceCode,
                                    AccountingPeriodDays = transSerialMap.AccountingPeriodDays,
                                    AccumulatedTillDateString = transSerialMap.DepAccumulatedTillDate.HasValue ? transSerialMap.DepAccumulatedTillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                    DepFromDateString = transSerialMap.DepFromDate.HasValue ? transSerialMap.DepFromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                    DepToDateString = transSerialMap.DepToDate.HasValue ? transSerialMap.DepToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                    DepAbovePeriod = transSerialMap.DepAbovePeriod,
                                    BookedDepreciation = transSerialMap.BookedDepreciation,
                                    AccumulatedDepreciationAmount = transSerialMap.AccumulatedDepreciationAmount,
                                    NetValue = transSerialMap.NetValue,
                                    PreviousAcculatedDepreciationAmount = transSerialMap.PreviousAcculatedDepreciationAmount,
                                });
                            }
                        }

                        ad.DetailViewModel.Add(atDetail);
                    }
                }

                return ad;
            }
            else
                return new AssetDepreciationViewModel();
        }

        public static AssetTransactionDTO ToDTO(AssetDepreciationViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm.IsNotNull())
            {
                var transactionDTO = new AssetTransactionDTO();
                transactionDTO.AssetTransactionHead = new AssetTransactionHeadDTO();
                transactionDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

                if (vm.MasterViewModel.IsNotNull())
                {
                    if (vm.MasterViewModel.DocumentType == null || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Value))
                    {
                        throw new Exception("Select Document Type!");
                    }

                    transactionDTO.AssetTransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                    transactionDTO.AssetTransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType == null || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? (int?)null : int.Parse(vm.MasterViewModel.DocumentType.Key);
                    transactionDTO.AssetTransactionHead.BranchID = vm.MasterViewModel.Branch == null || string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? (long?)null : long.Parse(vm.MasterViewModel.Branch.Key);
                    transactionDTO.AssetTransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                    transactionDTO.AssetTransactionHead.EntryDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDateString) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDateString, dateFormat, CultureInfo.InvariantCulture);
                    transactionDTO.AssetTransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus == null || string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? (int?)null : int.Parse(vm.MasterViewModel.DocumentStatus.Key);
                    transactionDTO.AssetTransactionHead.Remarks = vm.MasterViewModel.Remarks;
                    transactionDTO.AssetTransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                    transactionDTO.AssetTransactionHead.CompanyID = vm.MasterViewModel.CompanyID;

                    int? depreciationYear = string.IsNullOrEmpty(vm.MasterViewModel.DepreciationYear) ? (int?) null : int.Parse(vm.MasterViewModel.DepreciationYear);
                    int? depreciationMonth = string.IsNullOrEmpty(vm.MasterViewModel.DepreciationMonth) ? (int?)null : int.Parse(vm.MasterViewModel.DepreciationMonth);

                    if (depreciationYear.HasValue && depreciationMonth.HasValue)
                    {
                        // Get the number of days in the specified month
                        int daysInMonth = DateTime.DaysInMonth(depreciationYear.Value, depreciationMonth.Value);

                        transactionDTO.AssetTransactionHead.DepreciationStartDate = new DateTime(depreciationYear.Value, depreciationMonth.Value, 1);
                        transactionDTO.AssetTransactionHead.DepreciationEndDate = new DateTime(depreciationYear.Value, depreciationMonth.Value, daysInMonth);
                    }

                    transactionDTO.AssetTransactionHead.CreatedBy = vm.MasterViewModel.CreatedBy;
                    transactionDTO.AssetTransactionHead.CreatedDate = vm.MasterViewModel.CreatedDate;
                    transactionDTO.AssetTransactionHead.UpdatedBy = vm.MasterViewModel.UpdatedBy;
                    transactionDTO.AssetTransactionHead.UpdatedDate = vm.MasterViewModel.UpdatedDate;
                }

                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (!string.IsNullOrEmpty(transactionDetail.DepToDateString))
                        {
                            var transactionDetailDTO = new AssetTransactionDetailsDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailIID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHeadID;
                            transactionDetailDTO.AssetID = transactionDetail.AssetID;
                            transactionDetailDTO.AssetGlAccID = transactionDetail?.AssetGlAccID;
                            transactionDetailDTO.AccumulatedDepGLAccID = transactionDetail?.AccumulatedDepGLAccID;
                            transactionDetailDTO.DepreciationExpGLAccID = transactionDetail?.DepreciationExpGLAccID;
                            transactionDetailDTO.AccountingPeriodDays = transactionDetail.AccountingPeriodDays;
                            transactionDetailDTO.DepAccumulatedTillDate = string.IsNullOrEmpty(transactionDetail.AccumulatedTillDateString) ? (DateTime?)null : DateTime.ParseExact(transactionDetail.AccumulatedTillDateString, dateFormat, CultureInfo.InvariantCulture);
                            transactionDetailDTO.DepFromDate = string.IsNullOrEmpty(transactionDetail.DepFromDateString) ? (DateTime?)null : DateTime.ParseExact(transactionDetail.DepFromDateString, dateFormat, CultureInfo.InvariantCulture);
                            transactionDetailDTO.DepToDate = string.IsNullOrEmpty(transactionDetail.DepToDateString) ? (DateTime?)null : DateTime.ParseExact(transactionDetail.DepToDateString, dateFormat, CultureInfo.InvariantCulture);
                            transactionDetailDTO.DepAbovePeriod = transactionDetail.DepAbovePeriod;
                            transactionDetailDTO.BookedDepreciation = transactionDetail.BookedDepreciation;
                            transactionDetailDTO.AccumulatedDepreciationAmount = transactionDetail.AccumulatedDepreciationAmount;
                            transactionDetailDTO.NetValue = transactionDetail.NetValue;
                            transactionDetailDTO.Quantity = Convert.ToInt32(transactionDetail.Quantity);
                            transactionDetailDTO.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                            transactionDetailDTO.Amount = transactionDetail.Amount;
                            transactionDetailDTO.PreviousAcculatedDepreciationAmount = transactionDetail.PreviousAcculatedDepreciationAmount;

                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.CreatedDate = transactionDetail.CreatedDate;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UpdatedDate = transactionDetail.UpdatedDate;

                            transactionDetailDTO.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
                            if (transactionDetail.TransactionSerialMaps != null && transactionDetail.TransactionSerialMaps.Count > 0)
                            {
                                foreach (var transSerialMap in transactionDetail.TransactionSerialMaps)
                                {
                                    if (transSerialMap.AssetSerialMapID.HasValue)
                                    {
                                        transactionDetailDTO.AssetTransactionSerialMaps.Add(new AssetTransactionSerialMapDTO()
                                        {
                                            AssetTransactionSerialMapIID = transSerialMap.AssetTransactionSerialMapIID,
                                            TransactionDetailID = transSerialMap.TransactionDetailID,
                                            AssetID = transSerialMap.AssetID,
                                            AssetSerialMapID = transSerialMap.AssetSerialMapID,
                                            AccountingPeriodDays = transSerialMap.AccountingPeriodDays,
                                            DepAccumulatedTillDate = string.IsNullOrEmpty(transSerialMap.AccumulatedTillDateString) ? (DateTime?)null : DateTime.ParseExact(transSerialMap.AccumulatedTillDateString, dateFormat, CultureInfo.InvariantCulture),
                                            DepFromDate = string.IsNullOrEmpty(transSerialMap.DepFromDateString) ? (DateTime?)null : DateTime.ParseExact(transSerialMap.DepFromDateString, dateFormat, CultureInfo.InvariantCulture),
                                            DepToDate = string.IsNullOrEmpty(transSerialMap.DepToDateString) ? (DateTime?)null : DateTime.ParseExact(transSerialMap.DepToDateString, dateFormat, CultureInfo.InvariantCulture),
                                            DepAbovePeriod = transSerialMap.DepAbovePeriod,
                                            BookedDepreciation = transSerialMap.BookedDepreciation,
                                            AccumulatedDepreciationAmount = transSerialMap.AccumulatedDepreciationAmount,
                                            NetValue = transSerialMap.NetValue,
                                            PreviousAcculatedDepreciationAmount = transSerialMap.PreviousAcculatedDepreciationAmount,
                                        });
                                    }
                                }
                            }

                            transactionDTO.AssetTransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transactionDTO.AssetTransactionDetails.Count() == 0)
                {
                    throw new Exception("Need at least one detail grid for saving the entry!");
                }

                return transactionDTO;
            }
            else
            {
                return new AssetTransactionDTO();
            }
        }

    }
}