using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Security;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Commons;
using System.Data;
using Eduegate.Domain.Repository.Accounts.Assets;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Repository.Settings;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Domain.Mappers.Accounts.Assets;
using Eduegate.Framework.Enums;
using Eduegate.Domain.Entity.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Accounts.Assets
{
    public class AssetTransactionBL
    {
        //private AssetTransactionRepository transactionRepo = new AssetTransactionRepository();
        private CallContext _callContext;

        public AssetTransactionBL(CallContext context)
        {
            _callContext = context;
        }

        public List<AssetTransactionHeadDTO> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            // get the listTransactionDetail from repo
            var listTransactionHead = new AssetTransactionRepository().GetAllTransaction(referenceTypes, transactionStatus);
            var _TransactionHeadDTO = new List<AssetTransactionHeadDTO>();

            // convert TransactionDetail into AssetTransactionDetailsDTO
            foreach (var itemTransactionHead in listTransactionHead)
            {
                // add in the TransactionHeadDTO list
                _TransactionHeadDTO.Add(ToAssetTransactionHeadDTO(itemTransactionHead));
            }

            return _TransactionHeadDTO;
        }

        private AssetTransactionHeadDTO ToAssetTransactionHeadDTO(AssetTransactionHead itemTransactionHead)
        {
            var transactionHeadDTO = new AssetTransactionHeadDTO();

            transactionHeadDTO = AssetTransactionHeadMapper.Mapper(_callContext).ToDTO(itemTransactionHead);

            transactionHeadDTO.HeadIID = itemTransactionHead.HeadIID;
            transactionHeadDTO.DocumentTypeID = itemTransactionHead.DocumentTypeID;
            transactionHeadDTO.DocumentReferenceTypeID = (Services.Contracts.Enums.DocumentReferenceTypes)itemTransactionHead.DocumentType.ReferenceTypeID;
            transactionHeadDTO.TransactionNo = itemTransactionHead.TransactionNo;
            transactionHeadDTO.Reference = itemTransactionHead.Reference;
            transactionHeadDTO.SupplierID = itemTransactionHead.SupplierID;
            transactionHeadDTO.ProcessingStatusID = itemTransactionHead.ProcessingStatusID;
            transactionHeadDTO.DocumentStatusID = itemTransactionHead.DocumentStatusID;
            transactionHeadDTO.BranchID = itemTransactionHead.BranchID;
            transactionHeadDTO.CompanyID = itemTransactionHead.CompanyID;
            transactionHeadDTO.EntryDate = itemTransactionHead.EntryDate;
            transactionHeadDTO.ToBranchID = itemTransactionHead.ToBranchID;
            transactionHeadDTO.ReferenceHeadID = itemTransactionHead.ReferenceHeadID.IsNotNull() ? itemTransactionHead.ReferenceHeadID : null;

            transactionHeadDTO.CreatedBy = itemTransactionHead.CreatedBy;
            transactionHeadDTO.UpdatedBy = itemTransactionHead.UpdatedBy;
            transactionHeadDTO.CreatedDate = itemTransactionHead.CreatedDate;
            transactionHeadDTO.UpdatedDate = itemTransactionHead.UpdatedDate;

            transactionHeadDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
            // Add detail rows to DTO
            foreach (var item in itemTransactionHead.AssetTransactionDetails)
            {
                var transactionDetailDTO = new AssetTransactionDetailsDTO();

                // Get the eldest batch for item
                var assetinventories = GetAssetInventories(Convert.ToInt64(item.AssetID), Convert.ToInt64(itemTransactionHead.BranchID));
                var batchID = default(long);

                //TODO : Need to correct this, transaction detail cannot have batchid
                if (assetinventories.IsNotNull() && assetinventories.Count > 0)
                {
                    batchID = assetinventories.First().Batch.Value;
                }

                transactionDetailDTO.DetailIID = item.DetailIID;
                transactionDetailDTO.HeadID = item.HeadID;
                transactionDetailDTO.AssetID = item.AssetID;
                transactionDetailDTO.Quantity = item.Quantity;
                transactionDetailDTO.CostAmount = item.CostAmount;
                transactionDetailDTO.Amount = item.Amount;
                transactionDetailDTO.NetValue = item.NetValue;
                transactionDetailDTO.BatchID = batchID;
                transactionDetailDTO.AccountID = item.AccountID;
                transactionDetailDTO.AssetGlAccID = item.AssetGlAccID;
                transactionDetailDTO.AccumulatedDepGLAccID = item.AccumulatedDepGLAccID;
                transactionDetailDTO.DepreciationExpGLAccID = item.DepreciationExpGLAccID;
                transactionDetailDTO.AccountingPeriodDays = item.AccountingPeriodDays;
                transactionDetailDTO.DepAccumulatedTillDate = item.DepAccumulatedTillDate;
                transactionDetailDTO.DepFromDate = item.DepFromDate;
                transactionDetailDTO.DepToDate = item.DepToDate;
                transactionDetailDTO.DepAbovePeriod = item.DepAbovePeriod;
                transactionDetailDTO.BookedDepreciation = item.BookedDepreciation;
                transactionDetailDTO.AccumulatedDepreciationAmount = item.AccumulatedDepreciationAmount;
                transactionDetailDTO.CutOffDate = item.CutOffDate;
                transactionDetailDTO.PreviousAcculatedDepreciationAmount = item.PreviousAcculatedDepreciationAmount;

                transactionDetailDTO.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
                transactionDetailDTO.AssetTransactionSerialMaps = item.AssetTransactionSerialMaps.Select(x => AssetTransactionSerialMapMapper.Mapper(_callContext).ToDTO(x)).ToList();

                transactionHeadDTO.AssetTransactionDetails.Add(transactionDetailDTO);
            }

            return transactionHeadDTO;
        }

        public List<AssetInventory> GetAssetInventories(long assetID, long branchID)
        {
            return new AssetTransactionRepository().GetAssetInventories(assetID, branchID);
        }

        public bool SaveAssetTransaction(List<AssetTransactionHeadDTO> dtoList)
        {
            return true;
        }

        public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dtos)
        {
            bool isSuccess = false;
            var transRepository = new AssetTransactionRepository();

            var loginID = _callContext != null ? _callContext.LoginID.HasValue ? Convert.ToInt32(_callContext.LoginID) : (int?)null : (int?)null;

            foreach (var item in dtos)
            {
                item.Batch = item.Batch.HasValue ? item.Batch.Value : transRepository.GetNextBatch(item.AssetID.Value, item.BranchID);
                var costSetting = new Domain.Setting.SettingBL(null).GetSettingValue<CostSetting>("COSTSETTING", CostSetting.Average);

                var assetInventory = new AssetInventory()
                {
                    AssetInventoryIID = item.AssetInventoryIID,
                    AssetID = item.AssetID,
                    CompanyID = item.CompanyID.HasValue ? item.CompanyID.Value : _callContext == null ? (int?)null : _callContext.CompanyID.Value,
                    BranchID = item.BranchID,
                    Quantity = item.Quantity,
                    Batch = item.Batch.Value,
                    HeadID = item.HeadID,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                };

                /**
                    1. Asset Entry (Asset register - Manual / Asset Opening): Add stock with given batch and assetID
                    2. Asset Entry Purchase (Asset register - purchase / Asset purchase invoice): Add stock with given batch and assetID
                    3. Asset Transfer Issue: Update stock for given batch + assetID (reduce stock)
                    4. Asset Transfer Receipt: reduced from source branch (transfer issued branch) + add/update to destination branch
                    5. Asset Removal (Asset sold / disposal): reduce the asset from given branch
                **/
                switch (item.DocumentReferenceType)
                {
                    case DocumentReferenceTypes.AssetEntry:
                        assetInventory.Quantity = item.Quantity;
                        assetInventory.OriginalQty = item.Quantity;
                        assetInventory.CostAmount = item.CostAmount;
                        isSuccess = transRepository.IncreaseAssetInventory(assetInventory);
                        item.AssetInventoryIID = assetInventory.AssetInventoryIID;
                        break;
                    case DocumentReferenceTypes.AssetEntryPurchase:
                        assetInventory.Quantity = item.Quantity;
                        assetInventory.OriginalQty = item.Quantity;
                        assetInventory.CostAmount = item.CostAmount;
                        isSuccess = transRepository.IncreaseAssetInventory(assetInventory);
                        item.AssetInventoryIID = assetInventory.AssetInventoryIID;
                        if (item.AssetSerialMapDTOs.Count > 0)
                        {
                            item.AssetSerialMapDTOs.ForEach(x =>
                            {
                                x.AssetInventoryID = assetInventory.AssetInventoryIID;
                            });
                        }
                        break;
                    case DocumentReferenceTypes.AssetTransferIssue:
                        // Reduce from source branch
                        assetInventory.CostAmount = transRepository.GetAssetCostPrice(item.AssetID.Value, item.Batch.Value, costSetting);
                        assetInventory.BranchID = (long)item.BranchID;
                        assetInventory.CreatedBy = item.CreatedBy.HasValue ? item.CreatedBy : loginID;
                        isSuccess = transRepository.InsertAssetInventoryNegativeQuantity(assetInventory);
                        item.AssetInventoryIID = assetInventory.AssetInventoryIID;
                        break;
                    case DocumentReferenceTypes.AssetTransferReceipt:
                        // Add to destination branch
                        assetInventory.BranchID = (long)item.ToBranchID;
                        isSuccess = transRepository.IncreaseAssetInventory(assetInventory);
                        assetInventory.CostAmount = transRepository.GetAssetCostPrice(item.AssetID.Value, item.Batch.Value, costSetting);
                        item.AssetInventoryIID = assetInventory.AssetInventoryIID;
                        break;
                    case DocumentReferenceTypes.AssetRemoval:
                        // Reduce from source branch
                        assetInventory.CostAmount = transRepository.GetAssetCostPrice(item.AssetID.Value, item.Batch.Value, costSetting);
                        assetInventory.BranchID = (long)item.BranchID;
                        assetInventory.CreatedBy = item.CreatedBy.HasValue ? item.CreatedBy : loginID;
                        isSuccess = transRepository.InsertAssetInventoryNegativeQuantity(assetInventory);
                        item.AssetInventoryIID = assetInventory.AssetInventoryIID;
                        break;
                }
            }
            return dtos;
        }

        public List<AssetInventoryDTO> UpdateAssetInventory(List<AssetInventoryDTO> dtos)
        {
            foreach (var item in dtos)
            {
                item.Batch = item.Batch.HasValue ? item.Batch.Value : new AssetTransactionRepository().GetNextBatch(item.AssetID.Value, item.BranchID);

                var productInventory = new AssetInventory()
                {
                    CompanyID = item.CompanyID.HasValue ? item.CompanyID.Value : _callContext.CompanyID.Value,
                    BranchID = item.BranchID,
                    AssetID = item.AssetID,
                    Quantity = item.Quantity,
                    Batch = item.Batch.Value,
                    CostAmount = item.CostAmount,
                    UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : default(int) : default(int),
                    UpdatedDate = DateTime.Now
                };

                new AssetTransactionRepository().UpdateAssetInventory(productInventory);
            }

            return dtos;
        }

        public bool SaveAssetInventoryTransactions(List<AssetInventoryTransactionDTO> dtos)
        {
            bool isSuccess = false;

            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var check = dtos.FirstOrDefault();

                //var referenceHeadID = dbContext.AssetTransactionHeads.Where(x => x.HeadIID == check.AssetTransactionHeadID).AsNoTracking().FirstOrDefault()?.ReferenceHeadID;

                //var docTypeID = referenceHeadID != null ? dbContext.AssetTransactionHeads.Where(r => r.HeadIID == referenceHeadID).AsNoTracking().FirstOrDefault()?.DocumentTypeID : null;

                foreach (var item in dtos)
                {
                    var assetInventoryTransaction = new AssetInventoryTransaction();

                    assetInventoryTransaction.DocumentTypeID = item.DocumentTypeID;
                    assetInventoryTransaction.TransactionNo = item.TransactionNo;
                    assetInventoryTransaction.TransactionDate = item.TransactionDate == null ? DateTime.Now : Convert.ToDateTime(item.TransactionDate);

                    // in DB FK_AssetInventoryTransactions_Asset mapping between asset.Assets
                    assetInventoryTransaction.AssetID = item.AssetID;
                    assetInventoryTransaction.AssetSerialMapID = item.AssetSerialMapID;

                    assetInventoryTransaction.AssetTransactionHeadID = item.AssetTransactionHeadID;
                    assetInventoryTransaction.BatchID = item.BatchID;
                    assetInventoryTransaction.BranchID = item.BranchID;
                    assetInventoryTransaction.CompanyID = item.CompanyID.HasValue ? item.CompanyID : _callContext.CompanyID;

                    assetInventoryTransaction.Quantity = item.Quantity;
                    assetInventoryTransaction.Amount = item.Amount;
                    assetInventoryTransaction.SerialNo = item.SerialNo;
                    //assetInventoryTransaction.OriginalQty = item.Quantity;

                    assetInventoryTransaction.AccountID = item.AccountID;

                    isSuccess = new AssetTransactionRepository().UpdateAssetInventoryTransactions(assetInventoryTransaction);
                }

                if (!isSuccess)
                {
                    return isSuccess = false;
                }

                return isSuccess;
            }
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            var transactionHead = new AssetTransactionHead();
            transactionHead.HeadIID = dto.HeadIID;
            //transactionHead.TransactionStatusID = dto.TransactionStatusID;
            //transactionHead.Description = dto.Description;
            transactionHead.Reference = dto.Reference;
            transactionHead.UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : default(int) : default(int);
            transactionHead.DocumentStatusID = dto.DocumentStatusID;

            return new AssetTransactionRepository().UpdateAssetTransactionHead(transactionHead);
        }

        public AssetInventoryDTO GetAssetInventoryDetail(AssetInventoryDTO dto)
        {
            AssetInventory assetInventory = new AssetInventory();
            assetInventory.AssetID = dto.AssetID;

            // get the ProductInventory
            assetInventory = new AssetTransactionRepository().GetAssetInventoryDetail(assetInventory);

            //convert from ProductInventory to ProductInventoryDTO
            dto.AssetID = assetInventory.AssetID;
            dto.Quantity = assetInventory.Quantity;
            dto.CostAmount = assetInventory.CostAmount;

            return dto;
        }

        public AssetTransactionHeadDTO GetAssetTransactionDetail(long headId)
        {
            return ToAssetTransactionHeadDTO(new AssetTransactionRepository().GetAssetTransactionDetail(headId));
        }

        public AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO transaction)
        {
            long? oldDocumentStatus = (int?)null;
            var transactionDTO = new AssetTransactionDTO();

            if (transaction.AssetTransactionHead.DocumentReferenceTypeID != null)
            {
                switch (transaction.AssetTransactionHead.DocumentReferenceTypeID.Value)
                {
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetEntryManual:
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetEntryPurchase:
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetTransferRequest:
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetTransferIssue:
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetTransferReceipt:
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetDepreciation:
                    case Services.Contracts.Enums.DocumentReferenceTypes.AssetRemoval:


                        break;

                    default:
                        break;
                }
            }

            var transactionHead = new AssetTransactionHead();
            // output
            var assetSerialMaps = new List<AssetSerialMap>();
            List<AssetSerialMap> assetSerialMapsDigi = null;

            if (transaction.IsNotNull())
            {
                if (transaction.AssetTransactionHead.EntryDate.HasValue)
                {
                    if (transaction.AssetTransactionHead.HeadIID == 0 && transaction.AssetTransactionHead.EntryDate.Value.Date < DateTime.Now.Date)
                    {
                        throw new Exception("Date must be greater than or equal to today's date");
                    }
                }

                bool isUpdateTransactionNo = true;

                /*checking if document type is changed or not 
                if it is changed then we have to update the Transaction Number with the last generated number*/
                if (transaction.AssetTransactionHead.HeadIID > 0)
                {
                    var dbDTO = new AssetTransactionRepository().GetAssetTransaction(transaction.AssetTransactionHead.HeadIID);
                    oldDocumentStatus = dbDTO.DocumentStatusID;

                    isUpdateTransactionNo = dbDTO.DocumentTypeID != transaction.AssetTransactionHead.DocumentTypeID ? true : false;

                    //Check for document status

                    // once TransactionStatus completed we should not allow any operation on any transaction..
                    if (
                        dbDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.IntitiateReprecess ||
                        dbDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete ||
                        dbDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess ||
                        dbDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled ||
                        dbDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Confirmed)
                    {
                        if (!(dbDTO.ProcessingStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete
                            && transaction.AssetTransactionHead.DocumentStatusID == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled))
                        {
                            transaction.AssetTransactionHead.IsError = true;
                            transaction.AssetTransactionHead.ErrorCode = ErrorCodes.Transaction.T002;
                            transaction.ErrorCode = ErrorCodes.Transaction.T002;
                            transaction.AssetTransactionHead.IsTransactionCompleted = true;
                            return transaction;
                        }
                    }
                }

                var ignoreInventoryCheck = false;

                if (!transaction.AssetTransactionHead.DocumentTypeID.HasValue)
                {
                    transaction.AssetTransactionHead.IsError = true;
                    transaction.AssetTransactionHead.ErrorCode = ErrorCodes.Transaction.T006;
                    transaction.ErrorCode = ErrorCodes.Transaction.T006;
                    transaction.AssetTransactionHead.IsTransactionCompleted = true;
                    return transaction;
                }

                var documentDetail = new AssetTransactionRepository().GetDocumentType(transaction.AssetTransactionHead.DocumentTypeID.Value);
                ignoreInventoryCheck = documentDetail.IgnoreInventoryCheck.HasValue ? documentDetail.IgnoreInventoryCheck.Value : false;

                if (!transaction.AssetTransactionHead.DocumentReferenceTypeID.HasValue)
                {
                    transaction.AssetTransactionHead.DocumentReferenceTypeID = (Eduegate.Services.Contracts.Enums.DocumentReferenceTypes)documentDetail.ReferenceTypeID;
                }

                //validate the validalble quanitty
                if (transaction.AssetTransactionHead.DocumentReferenceTypeID.Value == Services.Contracts.Enums.DocumentReferenceTypes.AssetTransferIssue ||
                    transaction.AssetTransactionHead.DocumentReferenceTypeID.Value == Services.Contracts.Enums.DocumentReferenceTypes.AssetTransferReceipt)
                {
                    if (!ignoreInventoryCheck)
                    {
                        foreach (var detail in transaction.AssetTransactionDetails)
                        {
                            //TODO : Check later
                            //var skuInventory = new ProductCatalogRepository().GetProductSKUInventoryDetail(
                            //    transaction.AssetTransactionHead.CompanyID.HasValue ? transaction.AssetTransactionHead.CompanyID.Value : _callContext.IsNotNull() ? _callContext.CompanyID.Value : 0,
                            //    detail.ProductSKUMapID.Value, transaction.AssetTransactionHead.BranchID.HasValue ? transaction.AssetTransactionHead.BranchID.Value : 0);

                            //if (skuInventory.IsNull() || !skuInventory.Quantity.HasValue || detail.Quantity > skuInventory.Quantity.Value)
                            //{
                            //    transaction.AssetTransactionHead.IsError = true;
                            //    transaction.AssetTransactionHead.ErrorCode = ErrorCodes.Transaction.T005;
                            //    transaction.ErrorCode = ErrorCodes.Transaction.T005;
                            //    transaction.AssetTransactionHead.IsTransactionCompleted = true;

                            //    if (transaction.AssetTransactionHead.DocumentStatusID != (int)Services.Contracts.Enums.DocumentStatuses.Draft)
                            //    {
                            //        return transaction;
                            //    }
                            //}
                        }
                    }
                }

                if (isUpdateTransactionNo)
                {
                    // If transaction is SO created from order change request
                    var parameters = new List<KeyValueParameterDTO>();

                    if (transaction.AssetTransactionHead.ReferenceHeadID.IsNotNull())
                    {
                        // This will give change request or Parent Transaction
                        var Ptransaction = GetAssetTransaction((long)transaction.AssetTransactionHead.ReferenceHeadID);
                        if (Ptransaction != null && Ptransaction.AssetTransactionHead != null)
                        {
                            var transactionID = Ptransaction.AssetTransactionHead.HeadIID;

                            // Check if parent is change request 
                            if (Ptransaction.AssetTransactionHead.ReferenceHeadID.IsNotNull() && Ptransaction.AssetTransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.OrderChangeRequest.ToString())
                            {
                                // this will give parent SO of change request
                                var parenttransactiondetail = GetAssetTransaction((long)Ptransaction.AssetTransactionHead.ReferenceHeadID);
                                transactionID = (long)parenttransactiondetail.AssetTransactionHead.ReferenceHeadID;
                            }
                            //var parenttransactioncartid = new ShoppingCartBL(_callContext).GetCartDetailByHeadID(transactionID);

                            //if (parenttransactioncartid.IsNotNullOrEmpty() && Ptransaction.AssetTransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder.ToString()
                            //    || Ptransaction.AssetTransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder.ToString())
                            //    parameters.Add(new KeyValueParameterDTO() { ParameterName = "CARTID", ParameterValue = parenttransactioncartid });
                            //else
                            //    parameters = null;
                        }
                    }
                    else
                    {
                        parameters = null;
                    }

                    transaction.AssetTransactionHead.TransactionNo = GetNextTransactionNumber(Convert.ToInt32(transaction.AssetTransactionHead.DocumentTypeID), parameters);
                }

                if (transaction.AssetTransactionHead.IsNotNull())
                {
                    transactionHead = AssetTransactionHeadMapper.Mapper(_callContext).ToEntity(transaction.AssetTransactionHead);
                }

                transactionHead.CompanyID = transactionHead.CompanyID.HasValue ? transactionHead.CompanyID : (_callContext.IsNotNull() ?
                    (_callContext.CompanyID.HasValue && _callContext.CompanyID != default(int) ? _callContext.CompanyID : (int?)null) : null);

                switch (transaction.AssetTransactionHead.DocumentReferenceTypeID.Value)
                {
                    case Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice:
                        {
                            if (transaction.AssetTransactionHead.ReferenceHeadID.IsNotNull() && transaction.AssetTransactionHead.ReferenceHeadID > 0)
                            {
                                var parentTransaction = GetAssetTransactionDetail(Convert.ToInt64(transaction.AssetTransactionHead.ReferenceHeadID));
                                var childTransactions = GetChildAssetTransactions(Convert.ToInt64(transaction.AssetTransactionHead.ReferenceHeadID));


                                // Loop on current transaction's items here
                                foreach (var currentItem in transaction.AssetTransactionDetails)
                                {
                                    //decimal storedQuantity = 0;

                                    //if (childTransactions.Any())
                                    //{
                                    //    var dChild = childTransactions.Where(w => w.AssetTransactionDetails.Any(x => x.ProductSKUMapID == currentItem.ProductSKUMapID)).SelectMany(w => w.AssetTransactionDetails);
                                    //    if (dChild.Any())
                                    //        storedQuantity = dChild.Sum(w => w.Quantity) ?? 0;
                                    //}
                                    // loop on children Heads
                                    //foreach (var childHead in childTransactions)
                                    //{
                                    //    if (currentItem.HeadID != childHead.HeadIID)
                                    //    {
                                    //        var childItem = childHead.AssetTransactionDetails.Where(t => t.ProductSKUMapID == currentItem.ProductSKUMapID).FirstOrDefault();
                                    //        storedQuantity += (childItem != null && childItem.Quantity.HasValue ? childItem.Quantity.Value : 0);
                                    //    }
                                    //}

                                    // stored quantity + current quantity > Total quantity mentioned in PO then reject current order
                                    //if ((storedQuantity + currentItem.Quantity) > (Convert.ToDecimal(parentTransaction.AssetTransactionDetails.Where(t => t.ProductSKUMapID == currentItem.ProductSKUMapID).FirstOrDefault().Quantity)))
                                    //{
                                    //    transaction.ErrorCode = ErrorCodes.PurchaseInvoice.SI001;
                                    //    currentItem.IsError = true;
                                    //    return transaction;
                                    //}
                                    //else
                                    //{

                                    /* Check for serial number autogenerated
                                       && skudetail collection is null(new invoice) 
                                    */
                                    //bool isProductSerialNumberAutoGenerated = Convert.ToBoolean(new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(currentItem.ProductSKUMapID)).IsSerailNumberAutoGenerated);

                                    //if (isProductSerialNumberAutoGenerated == true && (currentItem.SKUDetails.IsNull()
                                    //     || currentItem.SKUDetails.Count == 0 || currentItem.SKUDetails.First().SerialNo.IsNullOrEmpty()))
                                    //{
                                    //    // If autogenerated, generate and create serial map collection
                                    //    currentItem.SKUDetails = new List<ProductSerialMapDTO>();
                                    //    var quantityCounter = currentItem.Quantity;
                                    //    while (Convert.ToInt32(quantityCounter--) > 0)
                                    //    {
                                    //        currentItem.SKUDetails.Add(new ProductSerialMapDTO()
                                    //        {
                                    //            SerialNo = currentItem.SerialNumber.IsNotNull() ? currentItem.SerialNumber : GetUniqueAutoGeneratedSerialNumber(),
                                    //            DetailID = currentItem.DetailIID,
                                    //            ProductSKUMapID = Convert.ToInt64(currentItem.ProductSKUMapID),
                                    //        });
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //var productType = new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(transaction.AssetTransactionDetails.First().ProductSKUMapID)).ProductTypeID;
                                    //var isDigitalProduct = (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital;
                                    // Check for length allowed for serial Number, only for digital
                                    //    if (isDigitalProduct)
                                    //    {
                                    //        var serialNumberLength = Convert.ToInt32(new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(currentItem.ProductSKUMapID)).ProductLength);

                                    //        if (serialNumberLength.IsNotNull() && serialNumberLength != 0 && currentItem.SKUDetails.Any(i => i.SerialNo.Length != serialNumberLength))
                                    //        {
                                    //            currentItem.SKUDetails.ForEach(s =>
                                    //            {
                                    //                if (s.ProductLength != serialNumberLength)
                                    //                {
                                    //                    s.IsError = true;
                                    //                    s.ErrorMessage = ErrorCodes.PurchaseInvoice.SI002;
                                    //                }
                                    //            });
                                    //            currentItem.IsError = true;
                                    //            transaction.ErrorCode = ErrorCodes.PurchaseInvoice.SI002;
                                    //            return transaction;
                                    //        }
                                    //    }
                                    //}

                                    // Remove empty skudetail if any
                                    //if (currentItem.SKUDetails.IsNotNull() && currentItem.SKUDetails.Count > 0)
                                    //{
                                    //    currentItem.SKUDetails.RemoveAll(s => s.ProductSKUMapID == 0);
                                    //}

                                    //transactionHead.AssetTransactionDetails.Add(TransactionDetailsMapper.Mapper(_callContext).ToEntity(currentItem));

                                    //}
                                }

                            }
                            else
                            {
                                transactionHead.AssetTransactionDetails = transaction.AssetTransactionDetails.Select(x => AssetTransactionDetailsMapper.Mapper(_callContext).ToEntity(x)).ToList();
                            }

                            var hashCode = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE");
                            string hash = hashCode == null ? string.Empty : hashCode.SettingValue;

                            // Check for duplicate serialnumber in database
                            foreach (var line in transaction.AssetTransactionDetails)
                            {

                                // Skip checking for autogenerated serials as we did it while generating
                                //if (!Convert.ToBoolean(new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(line.ProductSKUMapID)).IsSerailNumberAutoGenerated) && line.IsSerialNumberOnPurchase)

                                //var lineSkudetail = new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(line.ProductSKUMapID));

                                //if (lineSkudetail != null)
                                //{
                                //    if (!Convert.ToBoolean(lineSkudetail.IsSerailNumberAutoGenerated) && Convert.ToBoolean(lineSkudetail.IsSerialNumberOnPurchase))
                                //    {
                                //        foreach (var skuDetail in line.SKUDetails)
                                //        {
                                //            if (ProductSerialCountCheck(hash, Convert.ToInt64(line.ProductSKUMapID), skuDetail.SerialNo, skuDetail.ProductSerialID))
                                //            {
                                //                line.IsError = true;
                                //                skuDetail.IsError = true;
                                //                skuDetail.ErrorMessage = ErrorCodes.PurchaseInvoice.SI003;
                                //                transaction.ErrorCode = ErrorCodes.PurchaseInvoice.SI003;
                                //                return transaction;
                                //            }
                                //        }
                                //    }
                                //}

                                // Delete allocation for branches if not allocated(if allocation quantity for that branch < 1)
                                //if (line.TransactionAllocations.IsNotNull() && line.TransactionAllocations.Count > 0)
                                //{
                                //    line.TransactionAllocations.RemoveAll(a => a.Quantity < 1);
                                //}


                            }
                            // Check for duplicate serialnumber

                            //Check if user has rights to write serial key ##start
                            //if (transaction.AssetTransactionDetails.Any(a => (Framework.Enums.ProductTypes?)(new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(a.ProductSKUMapID)).ProductTypeID) == ProductTypes.Digital))
                            //{
                            //    var serialKeyClaim = new SettingBL().GetSettingValue<long>(Eduegate.Framework.Helper.Constants.WRITESERIALKEYCLAIM);
                            //    var hasClaim = new SecurityRepository().HasClaimAccess(serialKeyClaim, _callContext.LoginID.Value);

                            //    if (!hasClaim)
                            //    {
                            //        transaction.AssetTransactionDetails.Where(a => (Framework.Enums.ProductTypes)(new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(a.ProductSKUMapID)).ProductTypeID) == ProductTypes.Digital).First().IsError = true;
                            //        transaction.ErrorCode = ErrorCodes.PurchaseInvoice.SI004;
                            //        return transaction;
                            //    }
                            //}
                        }
                        break;
                    case Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice:
                        {
                            //var productType = new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(transaction.AssetTransactionDetails.First().ProductSKUMapID)).ProductTypeID;

                            //if (productType != null && (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital)
                            //{
                            //    var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                            //    assetSerialMapsDigi = new List<ProductInventorySerialMap>();
                            //    var serialKeys = new List<string>();

                            //    foreach (var item in transaction.AssetTransactionDetails)
                            //    {

                            //        // To create SI for digital product pick serial Numbers and save with transaction details
                            //        var piSerialMaps = new AssetTransactionRepository().GetUnUsedSerialKey(item.ProductSKUMapID.Value, transaction.AssetTransactionHead.CompanyID.Value, serialKeys);

                            //        // Considering quantity will be 1 always for digital products, set this as used and add it to a ilst to update later
                            //        if (piSerialMaps != null)
                            //        {
                            //            serialKeys.Add(piSerialMaps.SerialNo);
                            //            item.SerialNumber = StringCipher.Decrypt(piSerialMaps.SerialNo, hash);
                            //            piSerialMaps.Used = true;
                            //            assetSerialMapsDigi.Add(piSerialMaps);
                            //        }

                            //        var detailMap = TransactionDetailsMapper.Mapper(_callContext).ToEntity(item);
                            //        detailMap.ProductSerialMaps = null;//serial key is allocated to row itself so mapping is not required.
                            //        transactionHead.AssetTransactionDetails.Add(detailMap);
                            //    }
                            //}
                            //else
                            //{
                            //    // if product is physical it must have SerialNumber for every item if IsSerialNumber checked for SKU
                            //    var isSerialNumberEmpty = false;
                            //    foreach (var detail in transaction.AssetTransactionDetails)
                            //    {
                            //        var skuMap = new ProductDetailBL().GetProductSKUDetails(detail.ProductSKUMapID.Value);
                            //        // How many config a SKU can have?
                            //        if (skuMap.ProductInventorySKUConfigMaps.IsNotNull() && skuMap.ProductInventorySKUConfigMaps.Count > 0 && Convert.ToBoolean(skuMap.ProductInventorySKUConfigMaps[0].ProductInventoryConfig.IsSerialNumber))
                            //        {
                            //            foreach (var serialMap in detail.SKUDetails)
                            //            {
                            //                if (serialMap.SerialNo.IsNullOrEmpty())
                            //                {
                            //                    isSerialNumberEmpty = true;
                            //                    serialMap.IsError = true;
                            //                    detail.IsError = true;
                            //                    serialMap.ErrorMessage = ErrorCodes.Transaction.T004;
                            //                    transaction.AssetTransactionHead.IsError = true;
                            //                    transaction.AssetTransactionHead.ErrorCode = ErrorCodes.Transaction.T004;
                            //                    transaction.ErrorCode = ErrorCodes.Transaction.T004;
                            //                    return transaction;
                            //                }
                            //            }

                            //        }
                            //    }

                            //    // If serial number empty return
                            //    if (isSerialNumberEmpty)
                            //    {
                            //        return transaction;
                            //    }

                            //    // normally map dto to entity
                            //    transactionHead.AssetTransactionDetails = transaction.AssetTransactionDetails.Select(x => TransactionDetailsMapper.Mapper(_callContext).ToEntity(x)).ToList();
                            //}
                        }
                        break;
                    default:
                        transactionHead.AssetTransactionDetails = transaction.AssetTransactionDetails.Select(x => AssetTransactionDetailsMapper.Mapper(_callContext).ToEntity(x)).ToList();
                        break;
                }
            }

            SetAssetTransactionStatus(transaction, transactionHead);

            if (transaction.AssetTransactionHead.IsError)
            {
                return transaction;
            }

            // Passing entity model data to repository
            transactionHead = new AssetTransactionRepository().SaveAssetTransactions(transactionHead, assetSerialMapsDigi);

            if (transactionHead.IsNotNull())
            {
                // Update LastTransactionNo in [mutual].[DocumentTypes] table
                //DocumentType entity = UpdateLastTransactionNo(Convert.ToInt32(transactionHead.DocumentTypeID), transactionHead.TransactionNo);
                var parameters = new List<KeyValueParameterDTO>();

                if (transactionHead.SupplierID.HasValue)
                {
                    parameters.Add(new KeyValueParameterDTO() { ParameterName = "SupplierID", ParameterValue = transactionHead.SupplierID.Value.ToString() });
                }

                if (transaction.AssetTransactionHead.HeadIID == 0)
                {
                    transactionHead.TransactionNo = GetAndSaveNextTransactionNumber(transactionHead.DocumentTypeID.Value);
                }
            }

            if (transactionHead.IsNotNull())
            {
                transactionDTO = new AssetTransactionBL(_callContext).GetAssetTransaction(transactionHead.HeadIID);
            }

            var headID = transactionDTO?.AssetTransactionHead?.HeadIID;
            if (headID.HasValue && headID > 0)
            {
                if (transaction.AssetTransactionHead.DocumentReferenceTypeID != null)
                {
                    if (transaction.AssetTransactionHead.DocumentReferenceTypeID == Services.Contracts.Enums.DocumentReferenceTypes.AssetDepreciation)
                    {
                        foreach (var detail in transaction.AssetTransactionDetails)
                        {
                            AssetSerialMapMapper.Mapper(_callContext).GetAndUpdateAssetSerialMapEntry(detail);
                        }
                    }
                }
            }

            return transactionDTO;
        }

        private void SetAssetTransactionStatus(AssetTransactionDTO transaction, AssetTransactionHead transactionHead)
        {
            // Set Transaction Status as per Document Status 
            if (transaction.AssetTransactionHead.DocumentStatusID.IsNull())
            {
                // If Null then Draft
                transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Hold;
            }
            else
            {
                // Get Document Statues by document reference type
                var documentStatus = new AssetTransactionRepository().GetDocumentReferenceStatusMap(Convert.ToInt32(transaction.AssetTransactionHead.DocumentStatusID));

                if (documentStatus.IsNull())
                    documentStatus = new DocumentReferenceStatusMap() { DocumentStatusID = 1 };

                switch (documentStatus.DocumentStatusID)
                {
                    case (long)Services.Contracts.Enums.DocumentStatuses.Approved:
                        // New
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Approved;
                        transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.New;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Cancelled:
                        // Cancelled
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Cancelled;
                        transactionHead.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Completed:
                        // Completed
                        transaction.AssetTransactionHead.IsError = true;
                        transaction.AssetTransactionHead.ErrorCode = ErrorCodes.Transaction.T001;
                        transaction.ErrorCode = ErrorCodes.Transaction.T001;
                        //return transaction;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Submitted:
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Submitted;
                        transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.New;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Draft:
                    default:
                        // New
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                        transactionHead.ProcessingStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Hold;
                        break;
                }
            }
        }

        List<AssetTransactionSerialMap> ProductSerialMapList(string hash, long ProductSKUMapID, string SerialNo)
        {
            var list = new List<AssetTransactionSerialMap>();
            //var productType = new ProductDetailBL().GetProductBySKUID(ProductSKUMapID).ProductTypeID;
            //var isDigitalProduct = (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital;
            //return new ProductDetailBL().GetProductSerialMaps(isDigitalProduct ? StringCipher.Encrypt(SerialNo, hash) : SerialNo);
            return list;
        }

        public bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        {
            List<AssetTransactionSerialMap> ProductSerialList = ProductSerialMapList(hash, ProductSKUMapID, SerialNo);
            //if ((ProductSerialID > 0 && ProductSerialList.Count > 0 && ProductSerialID != ProductSerialList.First().ProductSerialIID) || ((ProductSerialID.IsNull() || ProductSerialID == 0) && ProductSerialList.Count > 0)) // Existing serial map can have only 1 row with same serial number
            //{
            //    return true;
            //}
            return false;
        }


        public AssetTransactionDTO GetAssetTransaction(long headIID, bool partialCalulation = false, bool checkClaims = false)
        {
            var transaction = new AssetTransactionRepository().GetAssetTransaction(headIID);
            return TransactionEntityToDTO(headIID, partialCalulation, transaction);
        }

        private AssetTransactionDTO TransactionEntityToDTO(long headIID, bool partialCalulation, AssetTransactionHead transaction)
        {
            // get the TransactionHeadEntitlementMaps by headid
            var transactionDTO = new AssetTransactionDTO();
            var removableItems = new List<AssetTransactionDetail>();

            // Get its Children
            var childTransactions = GetChildAssetTransactions(Convert.ToInt64(headIID));
            // Check if partialCalculations to be considered
            if (partialCalulation)
            {

                // Loop on current transaction's items here
                foreach (var currentItem in transaction.AssetTransactionDetails)
                {
                    // loop on children Heads
                    foreach (var childHead in childTransactions)
                    {
                        var sku = childHead.AssetTransactionDetails.Where(t => t.AssetID == currentItem.AssetID).FirstOrDefault();

                        if (sku != null)
                        {
                            currentItem.Quantity -= Convert.ToDecimal(sku.Quantity);
                        }
                    }

                    // Ideally it should not go below zero
                    if (currentItem.Quantity <= 0)
                    {
                        removableItems.Add(currentItem);
                    }

                }
                transaction.AssetTransactionDetails = transaction.AssetTransactionDetails.Except(removableItems).ToList();
            }

            if (transaction != null)
            {
                transactionDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
                transactionDTO.AssetTransactionHead = AssetTransactionHeadMapper.Mapper(_callContext).ToDTO(transaction);

                //if (transactionDTO.AssetTransactionHead.BranchID.IsNotNull() && transactionDTO.AssetTransactionHead.BranchID != default(long))
                //    transactionDTO.AssetTransactionHead.BranchName = new ReferenceDataRepository().GetBranch((long)transactionDTO.AssetTransactionHead.BranchID, false).BranchName;

                //if (transactionDTO.AssetTransactionHead.ToBranchID.IsNotNull() && transactionDTO.AssetTransactionHead.ToBranchID != default(long))
                //    transactionDTO.AssetTransactionHead.ToBranchName = new ReferenceDataRepository().GetBranch((long)transactionDTO.AssetTransactionHead.ToBranchID, false).BranchName;

                if (transaction.AssetTransactionDetails != null && transaction.AssetTransactionDetails.Count > 0)
                {
                    if (_callContext.IsNotNull() && _callContext.CompanyID > 0)
                    {
                        transactionDTO.AssetTransactionDetails = transaction.AssetTransactionDetails.Select(x => AssetTransactionDetailsMapper.Mapper(_callContext).ToDTO(x, transaction.CompanyID.Value, transactionDTO.AssetTransactionHead.BranchID)).ToList();
                    }
                    else
                    {
                        transactionDTO.AssetTransactionDetails = transaction.AssetTransactionDetails.Select(x => AssetTransactionDetailsMapper.Mapper(_callContext).ToDTO(x, transactionDTO.AssetTransactionHead.BranchID)).ToList();
                    }
                }

                //to get all PI's for PO HeadIID
                var documentReferenceType = new AssetTransactionRepository().GetDocumentReferenceTypeByHeadId(headIID);

                // validate it is PO
                //if (documentReferenceType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                //{
                //    string invoiceStatus = string.Empty;
                //    if (childTransactions.Count == 0)
                //    {
                //        transactionDTO.AssetTransactionHead.InvoiceStatus = string.Empty;
                //    }
                //    if (childTransactions.IsNotNull() && childTransactions.Count > 0)
                //    {
                //        var isComplete = childTransactions.Any(x => x.TransactionStatusID != (int)Eduegate.Framework.Enums.TransactionStatus.Complete);
                //        if (isComplete)
                //        {
                //            transactionDTO.AssetTransactionHead.InvoiceStatus = "Partial";
                //        }
                //        else
                //        {
                //            transactionDTO.AssetTransactionHead.InvoiceStatus = "Completed";
                //        }
                //    }
                //}
                // Shipment details
                //if (transaction.TransactionShipments != null)
                //{
                //    transactionDTO.ShipmentDetails = transaction.TransactionShipments.Select(x => TransactionShipmentMapper.Mapper(_callContext).ToDTO(x)).FirstOrDefault();
                //}

                // Order contact details
                //if (transaction.OrderContactMaps.IsNotNull() && transaction.OrderContactMaps.Count > 0)
                //{
                //    transactionDTO.OrderContactMaps = new List<OrderContactMapDTO>();

                //    foreach (var ocmEntity in transaction.OrderContactMaps)
                //    {
                //        OrderContactMapDTO ocmDTO = OrderContactMapMapper.Mapper(_callContext).ToDTO(ocmEntity);
                //        transactionDTO.OrderContactMaps.Add(ocmDTO);
                //    }

                //    //SO, SR, SRR
                //    transactionDTO.OrderContactMap = OrderContactMapMapper.Mapper(_callContext).ToDTO(transaction.OrderContactMaps.FirstOrDefault());
                //}
                var childTransaction = GetChildTransaction(Convert.ToInt64(headIID));
                if (childTransaction.IsNotNull() && childTransaction.Count > 0)
                {
                    foreach (var thead in childTransaction)
                    {
                        //transaction.TransactionHead2 = thead;
                        //transactionDTO.TransactionHeads = new List<TransactionHeadDTO>();
                        //TransactionHeadDTO transDTO = TransactionHeadMapper.Mapper(_callContext).ToDTO(transaction.TransactionHead2);
                        //transactionDTO.TransactionHeads.Add(transDTO);
                    }
                }

                //if (transaction.TaxTransactions != null && transaction.TaxTransactions.Count > 0)
                //{
                //    // Map TransactionHeadEntitlementMap using mapper
                //    transactionDTO.AssetTransactionHead.TaxDetails = transaction.TaxTransactions.Select(x => TaxTransactionMapper.Mapper(_callContext).ToDTO(x)).ToList();
                //}
            }

            return transactionDTO;
        }

        public List<KeyValueDTO> GetAssetInventorySerialMaps(long productSKUMapID, string searchText, int pageSize, bool serialKeyUsed = false, bool checkClaims = false)
        {
            var settingRepository = new SettingRepository();
            //var securityRepository = new SecurityRepository();
            //var hasFullReadClaim = false;
            //try
            //{
            //    hasFullReadClaim = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM).SettingValue), _callContext.LoginID.Value);
            //}
            //catch (Exception) { }

            //var hasPartialReadClaim = false;

            //try
            //{
            //    hasPartialReadClaim = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READPARTIALSERIALKEYCLAIM).SettingValue), _callContext.LoginID.Value);
            //}
            //catch (Exception) { }

            var keyValues = new List<KeyValueDTO>();

            //var serialMaps = new AssetTransactionRepository().GetProductInventorySerialMaps(productSKUMapID, searchText, pageSize, serialKeyUsed);
            //if (serialMaps != null && serialMaps.Count > 0)
            //{
            //    var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
            //    foreach (var serial in serialMaps)
            //    {
            //        try
            //        {
            //            serial.SerialNo = StringCipher.Decrypt(serial.SerialNo, hash);
            //        }
            //        catch (Exception) { }
            //        if (checkClaims && hasFullReadClaim)
            //            keyValues.Add(new KeyValueDTO() { Key = serial.SerialNo, Value = serial.SerialNo });
            //        else if (checkClaims && hasPartialReadClaim)
            //        {
            //            var length = serial.SerialNo.Length;
            //            var visibleSerialKey = "";
            //            if (length <= 4)
            //            {
            //                visibleSerialKey = new String('x', length);
            //            }
            //            else
            //            {
            //                visibleSerialKey = new String('x', length - 4) + serial.SerialNo.Substring(length - 4);
            //            }
            //            keyValues.Add(new KeyValueDTO() { Key = visibleSerialKey, Value = visibleSerialKey });
            //        }
            //        //else
            //        //    keyValues.Add(new KeyValueDTO() { Key = visibleSerialKey, Value = visibleSerialKey });

            //    }
            //}

            return keyValues;
        }

        public AssetTransactionSummaryDetailDTO GetAssetTransactionDetails(string docuementTypeID, DateTime dateFrom, DateTime dateTo)
        {
            List<int?> documentIds = docuementTypeID.Split(',')
                .Select(x =>
                {
                    int value;
                    return int.TryParse(x, out value) ? value : (int?)null;
                })
                .ToList();
            var detail = new AssetTransactionRepository().GetTransactionDetails(documentIds, dateFrom, dateTo);
            return detail == null ? null : new AssetTransactionSummaryDetailDTO() { TransactionTypeName = detail.TransactionTypeName, Amount = detail.Amount, TransactionCount = detail.TransactionCount };
        }

        public AssetTransactionSummaryDetailDTO GetSupplierTransactionDetails(long loginID, string docuementTypeID, DateTime dateFrom, DateTime dateTo)
        {
            List<int?> documentIds = docuementTypeID.Split(',')
                .Select(x =>
                {
                    int value;
                    return int.TryParse(x, out value) ? value : (int?)null;
                })
                .ToList();
            var detail = new AssetTransactionRepository().GetSupplierTransactionDetails(loginID, documentIds, dateFrom, dateTo);
            return detail == null ? null : new AssetTransactionSummaryDetailDTO() { TransactionTypeName = detail.TransactionTypeName, Amount = detail.Amount, TransactionCount = detail.TransactionCount };
        }

        public List<AssetTransactionHead> GetChildAssetTransactions(long headID)
        {
            return new AssetTransactionRepository().GetChildAssetTransactions(headID);
        }

        public List<AssetTransactionHead> GetChildTransaction(long headID)
        {
            return new AssetTransactionRepository().GetChildTransaction(headID);

        }

        private string GetUniqueAutoGeneratedSerialNumber()
        {
            //var serialNumber = new UtilityBL().GetAutoGeneratedSerialNumber();
            //if (!new ProductDetailBL().IsSerialNumberUnique(serialNumber))
            //    GetUniqueAutoGeneratedSerialNumber();
            //return serialNumber;
            return null;
        }

        // this will return amount and entitlment 
        public List<KeyValueDTO> GetEntitlementsByHeadId(long headId)
        {
            List<KeyValueDTO> keyValueDtos = new List<KeyValueDTO>();
            // get entityTypeEntitlements
            //var entityTypeEntitlements = new AssetTransactionRepository().GetTransactionEntitlementByHeadId(headId);

            // convert from entityTypeEntitlementsto dto
            //if (entityTypeEntitlements.IsNotNull() && entityTypeEntitlements.Count > 0)
            //{
            //    entityTypeEntitlements.ForEach(x =>
            //    {
            //        keyValueDtos.Add(new KeyValueDTO
            //        {
            //            Key = x.Item2.ToString(),
            //            Value = x.Item1,
            //        });
            //    });
            //}

            return keyValueDtos;
        }

        // this will return id and entitlment
        public List<KeyValueDTO> GetEntitlementsByHeadIds(long headId)
        {
            List<KeyValueDTO> keyValueDtos = new List<KeyValueDTO>();
            // get entityTypeEntitlements
            //var entityTypeEntitlements = new AssetTransactionRepository().GetEntitlementsByHeadId(headId);

            // convert from entityTypeEntitlementsto dto
            //if (entityTypeEntitlements.IsNotNull() && entityTypeEntitlements.Count > 0)
            //{
            //    entityTypeEntitlements.ForEach(x =>
            //    {
            //        keyValueDtos.Add(new KeyValueDTO
            //        {
            //            Key = x.EntitlementID.ToString(),
            //            Value = x.EntitlementName,
            //        });
            //    });
            //}

            return keyValueDtos;
        }

        //public DigitalLimitDTO DigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, long companyID)
        //{
        //    decimal checkAmount = new CustomerBL(_callContext).CustomerVerificatonCheck(customerID) == true ? Convert.ToDecimal(new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.CUSVERIFIEDAMTLIMIT, companyID).SettingValue) : Convert.ToDecimal(new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.CUSBLOCKEDAMTLIMIT, companyID).SettingValue);
        //    var cartAmount = new TransactionRepository().DigitalAmountCustomerCheck(customerID, referenceType, (int)companyID);
        //    var totalAmount = cartAmount + cartDigitalAmount;
        //    return new DigitalLimitDTO()
        //    {
        //        IsAllowed = totalAmount <= checkAmount ? true : false,
        //        AmountLimit = cartDigitalAmount - (totalAmount - checkAmount)
        //    };

        //}

        /// <summary>
        /// get TransactionDetail By Job Entry Head Id
        /// </summary>
        /// <param name="jobEntryHeadId"></param>
        /// <param name="partialCalulation"></param>
        /// <returns></returns>
        //public TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false)
        //{
        //    AssetTransactionHead transaction = new AssetTransactionRepository().GetTransactionByJobEntryHeadId(jobEntryHeadId);

        //    if (transaction.IsNotNull() && transaction.HeadIID > 0)
        //    {
        //        return transactionEntityToDto(transaction.HeadIID, partialCalulation, transaction);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public bool HasDigitalProduct(long headID)
        //{
        //    return new AssetTransactionRepository().HasDigitalProduct(headID);
        //}

        public List<AssetTransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey, bool IsDigital)
        {
            var dto = new List<AssetTransactionHeadDTO>();

            var transactions = new AssetTransactionRepository().GetAllTransactionsBySerialKey(serialKey);
            if (IsDigital == true)
            {
                var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                serialKey = StringCipher.Encrypt(serialKey, hash);
                var reftransactions = new AssetTransactionRepository().GetAllTransactionsBySerialKey(serialKey);
                transactions.AddRange(reftransactions);
            }
            List<long> heads = new List<long>();
            var references = transactions.Where(x => x.ReferenceHeadID.HasValue).Select(x => (long)x.ReferenceHeadID).ToList();
            heads.AddRange(references);
            transactions.AddRange(new AssetTransactionRepository().GetParentTransactions(heads.IsNotNull() ? heads : null));
            //return TransactionHeadMapper.Mapper(_callContext).TodTO(transactions);

            return dto;
        }

        public bool CancelAssetTransaction(long headID)
        {
            var head = new AssetTransactionRepository().GetAssetTransaction(headID);

            switch ((DocumentReferenceTypes)head.DocumentType.ReferenceTypeID)
            {
                case DocumentReferenceTypes.AssetTransferReceipt:
                    return new AssetTransactionRepository().CancelAssetTransferReceiptTransaction(headID);
            }

            return false;
        }

        public bool CalculateEntitlement(AssetTransactionDTO transaction)
        {
            decimal detailSum = 0;
            decimal entitlementSum = 0;


            //if (transaction.AssetTransactionHead.TransactionHeadEntitlementMaps.IsNotNull() && transaction.AssetTransactionHead.TransactionHeadEntitlementMaps.Count > 0)
            //{
            //    foreach (var entitlement in transaction.AssetTransactionHead.TransactionHeadEntitlementMaps)
            //    {
            //        entitlementSum += entitlement.Amount.HasValue ? entitlement.Amount.Value : default(decimal);
            //    }
            //}

            if (transaction.AssetTransactionDetails.IsNotNull() && transaction.AssetTransactionDetails.Count > 0)
            {

                foreach (var detail in transaction.AssetTransactionDetails)
                {
                    //detailSum += Convert.ToDecimal(detail.UnitPrice * detail.Quantity);
                }

                //detailSum += transaction.AssetTransactionHead.DeliveryCharge.IsNull() ? 0 : transaction.AssetTransactionHead.DeliveryCharge.Value;
                //detailSum -= transaction.AssetTransactionHead.DiscountAmount.IsNull() ? 0 : transaction.AssetTransactionHead.DiscountAmount.Value;
            }

            //if (transaction.AssetTransactionHead.TaxDetails.IsNotNull() && transaction.AssetTransactionHead.TaxDetails.Count > 0)
            //{
            //    foreach (var tax in transaction.AssetTransactionHead.TaxDetails)
            //    {
            //        detailSum += Convert.ToDecimal(tax.ExclusiveTaxAmount);
            //    }
            //}

            if (entitlementSum != detailSum)
            {
                var diffference = detailSum - entitlementSum;
                // If entitlement is Visa/Master consider x.010 difference
                //if (transaction.AssetTransactionHead.TransactionHeadEntitlementMaps.Any(e => e.EntitlementID == 16) && diffference >= 0)
                //{
                //    return false;
                //}
                //else if (transaction.AssetTransactionHead.TransactionHeadEntitlementMaps.Any(e => e.EntitlementID == 7) && (diffference >= 0 && diffference <= (decimal)0.010))
                //{
                //    return false;
                //}

                return true;
            }
            else
                return false;
        }

        //public OrderDetailDTO GetDeliveryDetails(long Id)
        //{
        //    var order = new AssetTransactionRepository().GetDeliveryDetails(Id);
        //    if (order.IsNotNull())
        //    {
        //        var orderDTO = new OrderDetailDTO();
        //        //orderDTO = TransactionHeadMapper.Mapper(_callContext).FromEntityToDTO(order);
        //        return orderDTO;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted)
        {
            var keySplit = keys.Split(',');
            var keyvalueDTOList = new List<KeyValueDTO>();
            var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
            var stringArray = new string[keySplit.Length, 2];
            if (isEncrypted)
                stringArray = Eduegate.Framework.Security.EncryptDecrypt.Encrypt(keySplit, hash);
            else
                stringArray = Eduegate.Framework.Security.EncryptDecrypt.Decrypt(keySplit, hash);

            for (var i = 0; i <= stringArray.GetLength(0) - 1; i++)
            {
                keyvalueDTOList.Add(new KeyValueDTO() { Key = stringArray[i, 0], Value = stringArray[i, 1] });
            }

            return keyvalueDTOList;
        }

        //Get Counts for Dashbord BoilerPlates
        public AssetTransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID)
        {
            var returnData = new AssetTransactionSummaryDetailDTO();

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var transactions = dbContext.AssetTransactionDetails.Where(x => x.Head.DocumentTypeID == docTypeID /*&& x.AssetTransactionHead.SchoolID == _callContext.SchoolID*/).AsNoTracking().ToList();

                var dailyAmount = transactions.Where(y => y.Head.EntryDate.Value.Date == DateTime.Now.Date).Sum(s => s.Amount);
                var monthlyAmount = transactions.Where(y => y.Head.EntryDate.Value.Month == DateTime.Now.Month && y.Head.EntryDate.Value.Year == DateTime.Now.Year).Sum(s => s.Amount);
                var yearlyAmount = transactions.Where(y => y.Head.EntryDate.Value.Year == DateTime.Now.Year).Sum(s => s.Amount);

                returnData.YearlyAmount = yearlyAmount;
                returnData.MonthlyAmount = monthlyAmount;
                returnData.DailyAmount = dailyAmount;

                return returnData;
            }
        }

        public string GetAndSaveNextTransactionNumber(long documentTypeID, List<KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            var typeID = Convert.ToInt32(documentTypeID);
            DocumentType entity = new AssetTransactionRepository().GetDocumentType(typeID);
            if (entity.IsNotNull())
            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
                entity.LastTransactionNo = entity.LastTransactionNo.HasValue ? entity.LastTransactionNo.Value + 1 : 1;
                entity = new AssetTransactionRepository().SaveDocumentType(entity);
                nextTransactionNumber += entity.LastTransactionNo.IsNull() ? "1" : Convert.ToString(entity.LastTransactionNo);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public DocumentType UpdateLastTransactionNo(long documentTypeID, string transactionNo)
        {
            var typeID = Convert.ToInt32(documentTypeID);
            // get the DocumentType based on DocumentTypeID
            DocumentType entity = new AssetTransactionRepository().GetDocumentType(typeID);
            // split the transactionHead.TransactionNo using entity.TransactionNoPrefix 
            // compare both Transaction Number by add +1 in entity.LastTransactionNo

            if (GetPrefixedNumber(transactionNo) == (entity.LastTransactionNo.IsNull() ? 1 : entity.LastTransactionNo + 1))
            {
                entity.LastTransactionNo = entity.LastTransactionNo.IsNull() ? 1 : entity.LastTransactionNo + 1;
                // Update DocumentType
                entity = new AssetTransactionRepository().SaveDocumentType(entity);
            }
            return entity;
        }

        private long GetPrefixedNumber(string transactionNo)
        {
            long prefixedNumber;

            string lastNumber = new string(transactionNo.Reverse()
                 .SkipWhile(x => !char.IsDigit(x))
                 .TakeWhile(x => char.IsDigit(x))
                 .ToArray());
            long.TryParse(string.Join("", lastNumber.Reverse()), out prefixedNumber);
            return prefixedNumber;
        }

        public decimal CheckAssetAvailability(long branchID, long assetID)
        {
            return new AssetTransactionRepository().CheckAssetAvailability(branchID, assetID);
        }

        public bool CheckAssetAvailability(long? branchID, List<AssetTransactionDetailsDTO> transactions)
        {
            return new AssetTransactionRepository().CheckAssetAvailability(branchID, transactions);
        }

        public string GetNextTransactionNumber(long documentTypeID, List<KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            var typeID = Convert.ToInt32(documentTypeID);
            DocumentType entity = new AssetTransactionRepository().GetDocumentType(typeID);
            if (entity.IsNotNull())
            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
                nextTransactionNumber += entity.LastTransactionNo.IsNull() ? "1" : Convert.ToString(entity.LastTransactionNo + 1);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public bool UpdateAssetSerialMapsInventoryID(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            bool isSuccess = false;
            var transRepository = new AssetTransactionRepository();
            foreach (var serialMapID in AssetSerialMapIDs)
            {
                isSuccess = transRepository.UpdateAssetSerialMapsInventoryID(serialMapID, assetInventory);
            }

            return isSuccess;
        }

        public bool UpdateAssetSerialMapsStatus(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            bool isSuccess = false;
            var transRepository = new AssetTransactionRepository();
            foreach (var serialMapID in AssetSerialMapIDs)
            {
                isSuccess = transRepository.UpdateAssetSerialMapsStatus(serialMapID, assetInventory);
            }

            return isSuccess;
        }

        public bool InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps)
        {
            bool isSuccess = AssetSerialMapMapper.Mapper(_callContext).InsertAssetSerialMapList(assetSerialMaps);

            return isSuccess;
        }

    }
}