using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Domain.Mappers.Distributions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Domain.Mappers.InventoryTransactions;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Domain.Repository.Workflows;
using Eduegate.Services.Contracts.Commons;
using System.Data;
//using Eduegate.Services.Contracts.OrderHistory;
using System.Data.SqlClient;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Setting;
using Eduegate.Notification.Email.ViewModels;
using Eduegate.Domain.Mappers.Inventory;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.Supports.Models.Mutual;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Services.Contracts.Enums.Notifications;
//using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain
{
    public class TransactionBL
    {
        //private TransactionRepository transactionRepo = new TransactionRepository();
        private CallContext _callContext;

        public TransactionBL(CallContext context)
        {
            _callContext = context;
        }

        public List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            // get the listTransactionDetail from repo
            var listTransactionHead = new TransactionRepository().GetAllTransaction(referenceTypes, transactionStatus);
            var _TransactionHeadDTO = new List<TransactionHeadDTO>();

            // convert TransactionDetail into TransactionDetailDTO
            foreach (var itemTransactionHead in listTransactionHead)
            {
                // add in the TransactionHeadDTO list
                _TransactionHeadDTO.Add(ToTransactionHeadDTO(itemTransactionHead));
            }

            return _TransactionHeadDTO;
        }

        private TransactionHeadDTO ToTransactionHeadDTO(TransactionHead itemTransactionHead)
        {
            var transactionHeadDTO = new TransactionHeadDTO();

            transactionHeadDTO.HeadIID = itemTransactionHead.HeadIID;
            transactionHeadDTO.DocumentTypeID = itemTransactionHead.DocumentTypeID;
            transactionHeadDTO.DocumentReferenceTypeID = (Services.Contracts.Enums.DocumentReferenceTypes)itemTransactionHead.DocumentType.ReferenceTypeID;
            transactionHeadDTO.TransactionNo = itemTransactionHead.TransactionNo;
            transactionHeadDTO.Description = itemTransactionHead.Description;
            transactionHeadDTO.Reference = itemTransactionHead.Reference;
            transactionHeadDTO.CustomerID = itemTransactionHead.CustomerID;
            transactionHeadDTO.StudentID = itemTransactionHead.StudentID;
            transactionHeadDTO.StaffID = itemTransactionHead.StaffID;
            transactionHeadDTO.SupplierID = itemTransactionHead.SupplierID;
            transactionHeadDTO.TransactionStatusID = itemTransactionHead.TransactionStatusID;
            transactionHeadDTO.DocumentStatusID = itemTransactionHead.DocumentStatusID;
            transactionHeadDTO.BranchID = itemTransactionHead.BranchID;
            transactionHeadDTO.CompanyID = itemTransactionHead.CompanyID;
            transactionHeadDTO.TransactionDate = itemTransactionHead.TransactionDate;
            transactionHeadDTO.ToBranchID = itemTransactionHead.ToBranchID;
            transactionHeadDTO.ReferenceHeadID = itemTransactionHead.ReferenceHeadID.IsNotNull() ? itemTransactionHead.ReferenceHeadID : null;
            transactionHeadDTO.DeliveryDate = itemTransactionHead.DeliveryDate;
            transactionHeadDTO.CurrencyID = itemTransactionHead.CurrencyID;
            transactionHeadDTO.DeliveryTypeID = itemTransactionHead.DeliveryTypeID;
            transactionHeadDTO.DeliveryMethodID = itemTransactionHead.DeliveryMethodID;
            transactionHeadDTO.DiscountAmount = itemTransactionHead.DiscountAmount;
            transactionHeadDTO.DiscountPercentage = itemTransactionHead.DiscountPercentage;
            transactionHeadDTO.ExchangeRate = itemTransactionHead.ExchangeRate;
            transactionHeadDTO.ForeignAmount = itemTransactionHead.ForeignAmount;
            transactionHeadDTO.TransactionDetails = new List<TransactionDetailDTO>();
            transactionHeadDTO.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapDTO>();
            transactionHeadDTO.CreatedBy = itemTransactionHead.CreatedBy;
            transactionHeadDTO.UpdatedBy = itemTransactionHead.UpdatedBy;
            transactionHeadDTO.CreatedDate = !itemTransactionHead.CreatedDate.HasValue ? null : itemTransactionHead.CreatedDate.Value.ToLongDateString();
            transactionHeadDTO.UpdatedDate = !itemTransactionHead.UpdatedDate.HasValue ? null : itemTransactionHead.UpdatedDate.Value.ToLongDateString();

            // Add entitlement maps to DTO
            if (itemTransactionHead.TransactionHeadEntitlementMaps.IsNotNull() && itemTransactionHead.TransactionHeadEntitlementMaps.Count > 0)
            {
                // Map TransactionHeadEntitlementMap using mapper
                transactionHeadDTO.TransactionHeadEntitlementMaps = itemTransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapMapper.Mapper(_callContext).ToDTO(x)).ToList();

                foreach (var entitlement in transactionHeadDTO.TransactionHeadEntitlementMaps)
                {

                    // ... if it is voucher
                    setVoucherCodeByClaim(itemTransactionHead, entitlement);
                }
            }
            else
                transactionHeadDTO.TransactionHeadEntitlementMaps = null;

            transactionHeadDTO.TaxDetails = new List<TaxDetailsDTO>();

            foreach (var tax in itemTransactionHead.TaxTransactions)
            {
                transactionHeadDTO.TaxDetails.Add(new TaxDetailsDTO()
                {
                    AccountID = tax.AccoundID,
                    Amount = tax.Amount,
                    ExclusiveTaxAmount = tax.ExclusiveAmount,
                    HasTaxInclusive = tax.HasTaxInclusive,
                    TaxTemplateID = tax.TaxTemplateID,
                    InclusiveTaxAmount = tax.InclusiveAmount,
                    Percentage = tax.Percentage,
                    TaxTypeID = tax.TaxTypeID,
                    TaxTemplateItemID = tax.TaxTemplateItemID
                });
            }

            // Add detail rows to DTO
            foreach (var item in itemTransactionHead.TransactionDetails)
            {
                var transactionDetailDTO = new TransactionDetailDTO();

                // Get the eldest batch for item
                var productinventories = GetProductInventories(Convert.ToInt64(item.ProductSKUMapID), Convert.ToInt64(itemTransactionHead.BranchID));
                var batchID = default(long);

                //TODO : Need to correct this, transaction detail cannot have batchid
                if (productinventories.IsNotNull() && productinventories.Count > 0)
                {
                    batchID = productinventories.First().Batch;
                }

                transactionDetailDTO.DetailIID = item.DetailIID;
                transactionDetailDTO.HeadID = item.HeadID;
                transactionDetailDTO.ProductID = item.ProductID;
                transactionDetailDTO.ProductSKUMapID = item.ProductSKUMapID;
                transactionDetailDTO.Quantity = item.Quantity;
                transactionDetailDTO.UnitID = item.UnitID;
                transactionDetailDTO.DiscountPercentage = item.DiscountPercentage;
                transactionDetailDTO.UnitPrice = item.UnitPrice;
                transactionDetailDTO.Amount = item.Amount;
                transactionDetailDTO.ExchangeRate = item.ExchangeRate;
                transactionDetailDTO.BatchID = batchID;
                transactionDetailDTO.SerialNumber = item.SerialNumber;
                transactionDetailDTO.ParentDetailID = item.ParentDetailID;
                transactionDetailDTO.Action = item.Action;
                transactionDetailDTO.Remark = item.Remark;
                transactionDetailDTO.LandingCost = item.LandingCost;
                transactionDetailDTO.LastCostPrice = item.LastCostPrice;
                transactionDetailDTO.Fraction = item.Fraction;
                transactionDetailDTO.ForeignAmount = item.ForeignAmount ?? 0;
                transactionDetailDTO.ForeignRate = item.ForeignRate ?? 0;
                transactionDetailDTO.ExchangeRate = item.ExchangeRate;
                //Adding Transaction Allocation Detail if available
                transactionDetailDTO.TransactionAllocations = new List<TransactionAllocationDTO>();

                if (item.TransactionAllocations.Count > 0)
                {
                    transactionDetailDTO.TransactionAllocations.AddRange(item.TransactionAllocations.Select(t => TransactionAllocationMapper.Mapper().ToDTO(t)).ToList());
                }
                else
                {
                    transactionDetailDTO.TransactionAllocations = null;
                }

                transactionHeadDTO.TransactionDetails.Add(transactionDetailDTO);
            }

            return transactionHeadDTO;
        }

        public List<ProductInventory> GetProductInventories(long productSKUMapID, long branchID)
        {
            return new TransactionRepository().GetProductInventories(productSKUMapID, branchID);
        }

        public bool SaveTransaction(List<TransactionHeadDTO> dtoList)
        {
            return true;
        }

        public List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dtos)
        {
            bool isSuccess = false;
            var transRepository = new TransactionRepository();

            foreach (var item in dtos)
            {
                item.Batch = item.Batch.HasValue ? item.Batch.Value : transRepository.GetNextBatch(item.ProductSKUMapID, item.BranchID);
                var costSetting = new Domain.Setting.SettingBL(null).GetSettingValue<CostSetting>("COSTSETTING", CostSetting.Average);

                var productInventory = new ProductInventory()
                {
                    CompanyID = item.CompanyID.HasValue ? item.CompanyID.Value : _callContext == null ? (int?)null : _callContext.CompanyID.Value,
                    BranchID = item.BranchID,
                    ProductSKUMapID = item.ProductSKUMapID,
                    Quantity = item.Quantity,
                    Batch = item.Batch.Value,
                    HeadID = item.HeadID,
                };
                /**
                    1. purchase invoice: Add stock with given batch if 
                    2. sales invoice: Update stock for given batch + sku (reduce stock)
                    3. branch transfer: reduce from source branch + add/update to destination branch
                **/
                switch (item.ReferenceTypes)
                {
                    case DocumentReferenceTypes.PurchaseInvoice:
                        {
                            productInventory.Quantity = item.Quantity * (item.Fraction ?? 1);
                            productInventory.OriginalQty = item.Quantity;
                            productInventory.CostPrice = item.CostPrice;
                            isSuccess = transRepository.IncreaseProductInventory(productInventory);

                            var productDetail = new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(item.ProductSKUMapID), companyID: (int)item.CompanyID);

                            // create entries in product-inventory-serial-maps only for below condition
                            if (isSuccess && productDetail.IsNotNull() && productDetail.IsSerialNumber == true && (productDetail.IsSerailNumberAutoGenerated == true || productDetail.IsSerialNumberOnPurchase == true))
                            {
                                // Add items to ProductInventorySerialMap
                                var productInventorySerialMaps = new List<ProductInventorySerialMap>();
                                var vouchers = new List<VoucherMasterDTO>();
                                var productSerialMaps = new ProductDetailBL().GetProductSerialMaps(item.TransactioDetailID, item.ProductSKUMapID, Convert.ToInt32(item.Quantity));
                                for (int counter = 0; counter < item.Quantity; counter++)
                                {
                                    var serialMap = new ProductInventorySerialMap();
                                    serialMap.ProductSKUMapID = item.ProductSKUMapID;
                                    serialMap.Batch = item.Batch.Value;
                                    serialMap.CompanyID = item.CompanyID.HasValue ? item.CompanyID.Value : _callContext.CompanyID.Value;
                                    serialMap.BranchID = Convert.ToInt64(item.BranchID);
                                    serialMap.SerialNo = (productSerialMaps.IsNotNull() && productSerialMaps.Count > 0) ? productSerialMaps.First().SerialNo : ""; // get serial number from productSerialMaps
                                    serialMap.Used = false;
                                    serialMap.CreatedDate = DateTime.Now;

                                    if (_callContext != null)
                                        serialMap.CreatedBy = Convert.ToInt32(_callContext.UserId);

                                    if (productSerialMaps.Count > 0)
                                        productSerialMaps.RemoveAt(0); // remove product serialmap from list

                                    productInventorySerialMaps.Add(serialMap);
                                    var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                                    var product = new ProductDetailRepository().GetProduct(productDetail.ProductIID);
                                    if (productDetail.IsSerailNumberAutoGenerated == true)
                                    {
                                        decimal voucherAmount = 0;

                                        // Get voucher amount from ProductPriceListSKUMaps
                                        var priceSettingList = new PriceSettingsBL(null).GetProductPriceListForSKU(item.ProductSKUMapID, item.CompanyID.HasValue ? item.CompanyID.Value : _callContext.IsNotNull() ? _callContext.CompanyID.Value : default(int));
                                        // Blink Pricelist(default)
                                        var defaultPriceListID = 1; // How to get default pricelist for sku if more than one available

                                        if (priceSettingList.IsNotNull() && priceSettingList.Count > 0)
                                        {
                                            if (priceSettingList.Any(p => p.ProductPriceListID == defaultPriceListID))
                                            {
                                                voucherAmount = Convert.ToDecimal(priceSettingList.Where(p => p.ProductPriceListID == defaultPriceListID).Single().Price);
                                            }
                                            else
                                            {
                                                voucherAmount = Convert.ToDecimal(priceSettingList.First().Price);
                                            }
                                        }

                                        // Add to voucher dto list
                                        if (ProductTypes.Physical != (Framework.Enums.ProductTypes)(product.ProductTypeID))
                                        {
                                            vouchers.Add(new VoucherMasterDTO()
                                            {
                                                VoucherNo = StringCipher.Decrypt(serialMap.SerialNo, hash),
                                                VoucherType = Convert.ToString((int)VoucherTypes.Marketing),
                                                VoucherAmount = voucherAmount,
                                                StatusID = Convert.ToInt16(Framework.Enums.VoucherStatus.Active),
                                                IsSharable = true,
                                            });
                                        }
                                    }

                                }

                                isSuccess = new ProductDetailRepository().UpdateProductInventorySerialMaps(productInventorySerialMaps, false);

                                if (isSuccess)
                                {

                                }
                            }
                        }
                        break;
                    case DocumentReferenceTypes.SalesInvoice:
                        productInventory.Quantity = item.Quantity * (item.Fraction ?? 1);
                        productInventory.OriginalQty = item.Quantity;
                        isSuccess = transRepository.DecreaseProductInventory(productInventory);
                        productInventory.CostPrice = transRepository.GetCostPrice(item.ProductSKUMapID, item.Batch.Value, costSetting);
                        break;
                    case DocumentReferenceTypes.BranchTransfer:
                        // Reduce from source branch
                        productInventory.CostPrice = transRepository.GetCostPrice(item.ProductSKUMapID, item.Batch.Value, costSetting);
                        productInventory.BranchID = (long)item.BranchID;
                        isSuccess = transRepository.DecreaseProductInventory(productInventory);

                        // Add to destination branch
                        productInventory.BranchID = (long)item.ToBranchID;
                        isSuccess = transRepository.IncreaseProductInventory(productInventory);
                        productInventory.CostPrice = transRepository.GetCostPrice(item.ProductSKUMapID, item.Batch.Value, costSetting);
                        break;
                    case DocumentReferenceTypes.SalesReturn:
                        {
                            productInventory.isActive = 0; //temporary
                            productInventory.Quantity = item.Quantity * (item.Fraction ?? 1);
                            productInventory.OriginalQty = item.Quantity;
                            isSuccess = transRepository.IncreaseProductInventory(productInventory);
                            productInventory.CostPrice = transRepository.GetCostPrice(item.TransactioHeadID, item.ProductSKUMapID);
                            //Digital production serial key should be back
                            if (isSuccess)
                                isSuccess = transRepository.UpdateProduceSerialKeyStatus(item.SerialKeys, false);
                        }
                        break;
                    case DocumentReferenceTypes.PurchaseReturn:
                        productInventory.Quantity = item.Quantity * (item.Fraction ?? 1);
                        productInventory.OriginalQty = item.Quantity;
                        isSuccess = transRepository.DecreaseProductInventory(productInventory);
                        productInventory.CostPrice = item.CostPrice;
                        break;
                    case DocumentReferenceTypes.GoodsReceivedNotes:
                        productInventory.Quantity = item.Quantity * (item.Fraction ?? 1);
                        productInventory.OriginalQty = item.Quantity;
                        productInventory.CostPrice = item.CostPrice;
                        isSuccess = transRepository.IncreaseProductInventory(productInventory);
                        break;
                    case DocumentReferenceTypes.BundleUnWrap:
                        isSuccess = transRepository.DecreaseProductInventory(productInventory);
                        productInventory.CostPrice = transRepository.GetCostPrice(item.ProductSKUMapID, item.Batch.Value, costSetting);
                        break;
                    case DocumentReferenceTypes.BundleWrap:
                        isSuccess = transRepository.IncreaseProductInventory(productInventory);
                        productInventory.CostPrice = item.CostPrice;
                        var productDetails = new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(item.ProductSKUMapID), companyID: (int)item.CompanyID);

                        // create entries in product-inventory-serial-maps only for below condition
                        if (isSuccess && productDetails.IsNotNull() && productDetails.IsSerialNumber == true && (productDetails.IsSerailNumberAutoGenerated == true || productDetails.IsSerialNumberOnPurchase == true))
                        {
                            // Add items to ProductInventorySerialMap
                            var productInventorySerialMaps = new List<ProductInventorySerialMap>();
                            var vouchers = new List<VoucherMasterDTO>();
                            var productSerialMaps = new ProductDetailBL().GetProductSerialMaps(item.TransactioDetailID, item.ProductSKUMapID, Convert.ToInt32(item.Quantity));
                            for (int counter = 0; counter < item.Quantity; counter++)
                            {
                                var serialMap = new ProductInventorySerialMap();
                                serialMap.ProductSKUMapID = item.ProductSKUMapID;
                                serialMap.Batch = item.Batch.Value;
                                serialMap.CompanyID = item.CompanyID.HasValue ? item.CompanyID.Value : _callContext.CompanyID.Value;
                                serialMap.BranchID = Convert.ToInt64(item.BranchID);
                                serialMap.SerialNo = (productSerialMaps.IsNotNull() && productSerialMaps.Count > 0) ? productSerialMaps.First().SerialNo : ""; // get serial number from productSerialMaps
                                serialMap.Used = false;
                                serialMap.CreatedDate = DateTime.Now;

                                if (_callContext != null)
                                    serialMap.CreatedBy = Convert.ToInt32(_callContext.UserId);

                                if (productSerialMaps.Count > 0)
                                    productSerialMaps.RemoveAt(0); // remove product serialmap from list

                                productInventorySerialMaps.Add(serialMap);
                                var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                                var product = new ProductDetailRepository().GetProduct(productDetails.ProductIID);
                                if (productDetails.IsSerailNumberAutoGenerated == true)
                                {
                                    decimal voucherAmount = 0;

                                    // Get voucher amount from ProductPriceListSKUMaps
                                    var priceSettingList = new PriceSettingsBL(null).GetProductPriceListForSKU(item.ProductSKUMapID, item.CompanyID.HasValue ? item.CompanyID.Value : _callContext.IsNotNull() ? _callContext.CompanyID.Value : default(int));
                                    // Blink Pricelist(default)
                                    var defaultPriceListID = 1; // How to get default pricelist for sku if more than one available

                                    if (priceSettingList.IsNotNull() && priceSettingList.Count > 0)
                                    {
                                        if (priceSettingList.Any(p => p.ProductPriceListID == defaultPriceListID))
                                        {
                                            voucherAmount = Convert.ToDecimal(priceSettingList.Where(p => p.ProductPriceListID == defaultPriceListID).Single().Price);
                                        }
                                        else
                                        {
                                            voucherAmount = Convert.ToDecimal(priceSettingList.First().Price);
                                        }
                                    }

                                    // Add to voucher dto list
                                    if (ProductTypes.Physical != (Framework.Enums.ProductTypes)(product.ProductTypeID))
                                    {
                                        vouchers.Add(new VoucherMasterDTO()
                                        {
                                            VoucherNo = StringCipher.Decrypt(serialMap.SerialNo, hash),
                                            VoucherType = Convert.ToString((int)VoucherTypes.Marketing),
                                            VoucherAmount = voucherAmount,
                                            StatusID = Convert.ToInt16(Framework.Enums.VoucherStatus.Active),
                                            IsSharable = true,
                                        });
                                    }
                                }

                            }

                            isSuccess = new ProductDetailRepository().UpdateProductInventorySerialMaps(productInventorySerialMaps, false);

                            if (isSuccess)
                            {

                            }
                        }

                        break;
                }
            }
            return dtos;
        }

        public List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dtos)
        {
            foreach (var item in dtos)
            {
                item.Batch = item.Batch.HasValue ? item.Batch.Value : new TransactionRepository().GetNextBatch(item.ProductSKUMapID, item.BranchID);

                var productInventory = new ProductInventory()
                {
                    CompanyID = item.CompanyID.HasValue ? item.CompanyID.Value : _callContext.CompanyID.Value,
                    BranchID = item.BranchID,
                    ProductSKUMapID = item.ProductSKUMapID,
                    Quantity = item.Quantity,
                    Batch = item.Batch.Value,
                    CostPrice = item.CostPrice,
                    UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : default(int) : default(int),
                    UpdatedDate = DateTime.Now
                };

                new TransactionRepository().UpdateProductInventory(productInventory);
            }

            return dtos;
        }

        public bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dtos)
        {
            bool isSuccess = false;
            var getSalesInvoic = new SettingRepository().GetSettingDetail("Transaction_Doc_Type_SalesInvoice").SettingValue;
            var getPurchaseReturnDoc = new SettingRepository().GetSettingDetail("Transaction_Doc_Type_PurchaseReturn").SettingValue;

            int? saleInvoiceDocTypeID = int.Parse(getSalesInvoic);
            int? purchaseReturnDocTypeID = int.Parse(getPurchaseReturnDoc);

            var settingDat = new SettingRepository().GetSettingDetail("DOCUMENTTYPE_FOCSALES").SettingValue;
            var focDocTypeID = int.Parse(settingDat);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var docTypes = dbContext.DocumentTypes.AsNoTracking().ToList();
                var grnDocTypeID = docTypes.Find(x => x.TransactionNoPrefix == "GRN-").DocumentTypeID;
                var wrapDocTypeID = docTypes.Find(x => x.TransactionNoPrefix == "BNWR-").DocumentTypeID;
                var unWrapDocTypeID = docTypes.Find(x => x.TransactionNoPrefix == "BNUWR-").DocumentTypeID;

                var check = dtos.FirstOrDefault();

                var referenceHeadID = dbContext.TransactionHeads.Where(x => x.HeadIID == check.HeadID).AsNoTracking().FirstOrDefault().ReferenceHeadID;

                var docTypeID = referenceHeadID != null ? dbContext.TransactionHeads.Where(r => r.HeadIID == referenceHeadID).AsNoTracking().FirstOrDefault().DocumentTypeID : null;

                bool isGRNentry = docTypeID == grnDocTypeID ? true : false;

                foreach (var item in dtos)
                {
                    var invetoryTransaction = new InvetoryTransaction();

                    invetoryTransaction.DocumentTypeID = item.DocumentTypeID;
                    invetoryTransaction.TransactionNo = item.TransactionNo;
                    invetoryTransaction.TransactionDate = item.TransactionDate == null ? DateTime.Now : Convert.ToDateTime(item.TransactionDate);
                    invetoryTransaction.Description = item.Description;

                    invetoryTransaction.HeadID = item.HeadID;
                    invetoryTransaction.BatchID = item.BatchID;
                    invetoryTransaction.BranchID = item.BranchID;
                    invetoryTransaction.CompanyID = item.CompanyID.HasValue ? item.CompanyID : _callContext.CompanyID;


                    // in DB FK_InvetoryTransactions_ProductSKUMaps mapping between  InvetoryTransaction.ProductID & TransactionDetail.ProductSKUMapID 
                    invetoryTransaction.ProductSKUMapID = item.ProductSKUMapID;
                    invetoryTransaction.UnitID = item.UnitID;


                    if (saleInvoiceDocTypeID == item.DocumentTypeID || purchaseReturnDocTypeID == item.DocumentTypeID || unWrapDocTypeID == item.DocumentTypeID)
                    {
                        item.Quantity = -item.Quantity;
                    }

                    invetoryTransaction.Quantity = item.Quantity;
                    invetoryTransaction.Amount = item.DocumentTypeID == focDocTypeID ? 0 : item.Amount;
                    invetoryTransaction.ExchangeRate = item.ExchangeRate;
                    invetoryTransaction.LandingCost = item.LandingCost;
                    invetoryTransaction.LastCostPrice = item.LastCostPrice;
                    invetoryTransaction.Fraction = item.Fraction;
                    invetoryTransaction.Cost = item.DocumentTypeID == focDocTypeID ? item.LastCostPrice : item.LandingCost;
                    invetoryTransaction.OriginalQty = item.Quantity * (item.Fraction ?? 0);
                    invetoryTransaction.Rate = item.DocumentTypeID == focDocTypeID ? 0 : item.Rate;
                    // call the repo 

                    //Except GRN entry against purchase invoice
                    if (isGRNentry == false)
                    {
                        isSuccess = new TransactionRepository().UpdateInvetoryTransactions(invetoryTransaction);
                        if (isSuccess)
                        {
                            string type = null;
                            if (wrapDocTypeID == invetoryTransaction.DocumentTypeID)
                            {
                                type = "wrap";
                            }
                            if (unWrapDocTypeID == invetoryTransaction.DocumentTypeID)
                            {
                                type = "unWrap";
                            }
                            if (type != null)
                            {
                                isSuccess = new TransactionRepository().InsertWrapToInventoryTransactions(invetoryTransaction, type);
                            }
                        }
                    }

                    else
                    {
                        isSuccess = new TransactionRepository().updateAgainstGRNInvetoryTransactions(invetoryTransaction, referenceHeadID);
                        //isSuccess = new TransactionRepository().updateProductInventoryCostPrice(invetoryTransaction, referenceHeadID);
                    }

                }

                if (!isSuccess)
                {
                    return isSuccess = false;
                }

                return isSuccess;
            }
        }

        public bool UpdateTransactionHead(TransactionHeadDTO dto)
        {
            var transactionHead = new TransactionHead();
            transactionHead.HeadIID = dto.HeadIID;
            transactionHead.TransactionStatusID = dto.TransactionStatusID;
            transactionHead.Description = dto.Description;
            transactionHead.Reference = dto.Reference;
            transactionHead.UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : default(int) : default(int);
            transactionHead.DocumentStatusID = dto.DocumentStatusID;
            return new TransactionRepository().UpdateTransactionHead(transactionHead);
        }

        public ProductInventoryDTO GetProductInventoryDetail(ProductInventoryDTO dto)
        {
            ProductInventory productInventory = new ProductInventory();
            productInventory.ProductSKUMapID = dto.ProductSKUMapID;

            // get the ProductInventory
            productInventory = new TransactionRepository().GetProductInventoryDetail(productInventory);

            // convert from ProductInventory to ProductInventoryDTO
            dto.ProductSKUMapID = productInventory.ProductSKUMapID;
            dto.Quantity = productInventory.Quantity;
            dto.CostPrice = productInventory.CostPrice;

            return dto;
        }

        public decimal CheckAvailibility(long branchID, long productSKUMapID)
        {
            return new TransactionRepository().CheckAvailibility(branchID, productSKUMapID);
        }

        public bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions)
        {
            return new TransactionRepository().CheckAvailibility(branchID, transactions);
        }

        public bool CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions)
        {
            return new TransactionRepository().CheckAvailibilityBundleItem(branchID, transactions);
        }

        public TransactionHeadDTO GetTransactionDetail(long headId)
        {
            return ToTransactionHeadDTO(new TransactionRepository().GetTransactionDetail(headId));
        }

        public TransactionDTO SaveTransactions(TransactionDTO transaction)
        {
            var settingDat = new SettingRepository().GetSettingDetail("DOCUMENTTYPE_FOCSALES").SettingValue;
            var docTypeID = int.Parse(settingDat);

            long? oldDocumentStatus = (int?)null;
            var transactionDTO = new TransactionDTO();

            if (transaction.TransactionHead.DocumentReferenceTypeID != null)
            {
                switch (transaction.TransactionHead.DocumentReferenceTypeID.Value)
                {
                    case Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder:
                    case Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice:
                    case Services.Contracts.Enums.DocumentReferenceTypes.PurchaseReturn:
                    case Services.Contracts.Enums.DocumentReferenceTypes.GoodsReceivedNotes:
                    case Services.Contracts.Enums.DocumentReferenceTypes.GoodsDeliveryNotes:
                    case Services.Contracts.Enums.DocumentReferenceTypes.ServiceEntry:
                    case Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice:
                    case Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder:
                    case Services.Contracts.Enums.DocumentReferenceTypes.SalesReturn:
                    case Services.Contracts.Enums.DocumentReferenceTypes.FOCSales:

                        var monthlyClosingDate = new MonthlyClosingRepository().GetMonthlyClosingDate();
                        var transactionDate = transactionDTO?.TransactionHead?.TransactionDate;
                        if (monthlyClosingDate != null && monthlyClosingDate.Value.Year > 1900 && transactionDate != null && transactionDate.Value.Date >= monthlyClosingDate.Value.Date)
                        {
                            transaction.TransactionHead.IsError = true;
                            transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T007;
                            transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T007;
                            transaction.TransactionHead.IsTransactionCompleted = false;
                            return transaction;
                        }
                        break;

                    default:
                        break;
                }
            }
            transaction.IgnoreEntitlementCheck = true;
            // calulate the Entitlement amount and transaction detail amount
            if (!(transaction.IgnoreEntitlementCheck.HasValue && transaction.IgnoreEntitlementCheck == true))
            {
                var IsPaymentMatched = transaction.TransactionHead.DocumentTypeID != docTypeID ? CalculateEntitlement(transaction) : false;

                if (IsPaymentMatched && transaction.TransactionHead.DocumentStatusID != (int)Services.Contracts.Enums.DocumentStatuses.Draft)
                {
                    transaction.TransactionHead.IsError = true;
                    transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T003;
                    transaction.ErrorCode = ErrorCodes.Transaction.T003;
                    return transaction;
                }
            }
            var transactionHead = new TransactionHead();
            // output
            var productInventorySerialMaps = new List<ProductInventorySerialMap>();
            List<ProductInventorySerialMap> productInventorySerialMapsDigi = null;

            if (transaction.IsNotNull())
            {
                if (transaction.TransactionHead.TransactionDate.HasValue)
                {
                    if (transaction.TransactionHead.HeadIID == 0 && transaction.TransactionHead.TransactionDate.Value.Date < DateTime.Now.Date)
                    {
                        throw new Exception("Date must be greater than or equal to today's date");
                    }

                    if (transaction.TransactionHead.DueDate.HasValue && transaction.TransactionHead.DocumentReferenceTypeID != Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice)
                    {
                        if (transaction.TransactionHead.TransactionDate.Value.Date > transaction.TransactionHead.DueDate.Value.Date)
                        {
                            throw new Exception("Please select the Due/Valid date properly");
                        }
                    }
                }

                bool isUpdateTransactionNo = true;

                /*checking if document type is changed or not 
                if it is changed then we have to update the Transaction Number with the last generated number*/
                if (transaction.TransactionHead.HeadIID > 0)
                {
                    var dbDTO = new TransactionRepository().GetTransaction(transaction.TransactionHead.HeadIID);
                    oldDocumentStatus = dbDTO.DocumentStatusID;

                    isUpdateTransactionNo = dbDTO.DocumentTypeID != transaction.TransactionHead.DocumentTypeID ? true : false;

                    //Check for document status

                    // once TransactionStatus completed we should not allow any operation on any transaction..
                    if (
                        dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.IntitiateReprecess ||
                        dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete ||
                        dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess ||
                        dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled ||
                        dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Confirmed)
                    {
                        if (!(dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete
                            && transaction.TransactionHead.DocumentStatusID == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled))
                        {
                            transaction.TransactionHead.IsError = true;
                            transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T002;
                            transaction.ErrorCode = ErrorCodes.Transaction.T002;
                            transaction.TransactionHead.IsTransactionCompleted = true;
                            return transaction;
                        }
                    }
                }

                var ignoreInventoryCheck = false;

                if (!transaction.TransactionHead.DocumentTypeID.HasValue)
                {
                    transaction.TransactionHead.IsError = true;
                    transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T006;
                    transaction.ErrorCode = ErrorCodes.Transaction.T006;
                    transaction.TransactionHead.IsTransactionCompleted = true;
                    return transaction;
                }

                var documentDetail = new ReferenceDataRepository().GetDocumentType(transaction.TransactionHead.DocumentTypeID.Value);
                ignoreInventoryCheck = documentDetail.IgnoreInventoryCheck.HasValue ? documentDetail.IgnoreInventoryCheck.Value : false;

                if (!transaction.TransactionHead.DocumentReferenceTypeID.HasValue)
                {
                    transaction.TransactionHead.DocumentReferenceTypeID = (Eduegate.Services.Contracts.Enums.DocumentReferenceTypes)documentDetail.ReferenceTypeID;
                }

                //validate the validalble quanitty
                if (transaction.TransactionHead.DocumentReferenceTypeID.Value == Services.Contracts.Enums.DocumentReferenceTypes.BranchTransfer ||
                    transaction.TransactionHead.DocumentReferenceTypeID.Value == Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice ||
                    transaction.TransactionHead.DocumentReferenceTypeID.Value == Services.Contracts.Enums.DocumentReferenceTypes.FOCSales)
                {
                    if (!ignoreInventoryCheck)
                    {
                        foreach (var detail in transaction.TransactionDetails)
                        {
                            var skuInventory = new ProductCatalogRepository().GetProductSKUInventoryDetail(
                                transaction.TransactionHead.CompanyID.HasValue ? transaction.TransactionHead.CompanyID.Value : _callContext.IsNotNull() ? _callContext.CompanyID.Value : 0,
                                detail.ProductSKUMapID.Value, transaction.TransactionHead.BranchID.HasValue ? transaction.TransactionHead.BranchID.Value : 0);

                            if (skuInventory.IsNull() || !skuInventory.Quantity.HasValue || detail.Quantity > skuInventory.Quantity.Value)
                            {
                                transaction.TransactionHead.IsError = true;
                                transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T005;
                                transaction.ErrorCode = ErrorCodes.Transaction.T005;
                                transaction.TransactionHead.IsTransactionCompleted = true;

                                if (transaction.TransactionHead.DocumentStatusID != (int)Services.Contracts.Enums.DocumentStatuses.Draft)
                                {
                                    return transaction;
                                }
                            }
                        }
                    }
                }

                if (isUpdateTransactionNo)
                {
                    // If transaction is SO created from order change request
                    var parameters = new List<KeyValueParameterDTO>();

                    if (transaction.TransactionHead.ReferenceHeadID.IsNotNull())
                    {
                        // This will give change request or Parent Transaction
                        var Ptransaction = GetTransaction((long)transaction.TransactionHead.ReferenceHeadID);
                        var transactionID = Ptransaction.TransactionHead.HeadIID;
                        // Check if parent is change request 
                        if (Ptransaction.TransactionHead.ReferenceHeadID.IsNotNull() && Ptransaction.TransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.OrderChangeRequest.ToString())
                        {
                            // this will give parent SO of change request
                            var parenttransactiondetail = GetTransaction((long)Ptransaction.TransactionHead.ReferenceHeadID);
                            transactionID = (long)parenttransactiondetail.TransactionHead.ReferenceHeadID;
                        }
                        var parenttransactioncartid = new ShoppingCartBL(_callContext).GetCartDetailByHeadID(transactionID);

                        if (parenttransactioncartid.IsNotNullOrEmpty() && Ptransaction.TransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder.ToString()
                            || Ptransaction.TransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder.ToString())
                            parameters.Add(new KeyValueParameterDTO() { ParameterName = "CARTID", ParameterValue = parenttransactioncartid });
                        else
                            parameters = null;
                    }
                    else
                    {
                        parameters = null;
                    }

                    transaction.TransactionHead.TransactionNo = new MutualBL(_callContext).GetNextTransactionNumber(Convert.ToInt32(transaction.TransactionHead.DocumentTypeID), parameters);
                }

                if (transaction.TransactionHead.IsNotNull())
                {
                    transactionHead = TransactionHeadMapper.Mapper(_callContext).ToEntity(transaction.TransactionHead);
                }

                transactionHead.CompanyID = transactionHead.CompanyID.HasValue ? transactionHead.CompanyID : (_callContext.IsNotNull() ?
                    (_callContext.CompanyID.HasValue && _callContext.CompanyID != default(int) ? _callContext.CompanyID : (int?)null) : null);

                if (transaction.OrderContactMaps.IsNotNull() && transaction.OrderContactMaps.Count > 0)
                {
                    foreach (OrderContactMapDTO ocmDTO in transaction.OrderContactMaps)
                    {
                        OrderContactMap orderContactMap = OrderContactMapMapper.Mapper(_callContext).ToEntity(ocmDTO);
                        orderContactMap.OrderID = orderContactMap.OrderID > 0 ? orderContactMap.OrderID : transaction.TransactionHead.HeadIID;

                        transactionHead.OrderContactMaps.Add(orderContactMap);
                    }
                }

                switch (transaction.TransactionHead.DocumentReferenceTypeID.Value)
                {
                    case Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice:
                        {
                            if (transaction.TransactionHead.ReferenceHeadID.IsNotNull() && transaction.TransactionHead.ReferenceHeadID > 0)
                            {
                                var parentTransaction = GetTransactionDetail(Convert.ToInt64(transaction.TransactionHead.ReferenceHeadID));
                                var childTransactions = GetChildTransactions(Convert.ToInt64(transaction.TransactionHead.ReferenceHeadID));


                                // Loop on current transaction's items here
                                foreach (var currentItem in transaction.TransactionDetails)
                                {
                                    //decimal storedQuantity = 0;

                                    //if (childTransactions.Any())
                                    //{
                                    //    var dChild = childTransactions.Where(w => w.TransactionDetails.Any(x => x.ProductSKUMapID == currentItem.ProductSKUMapID)).SelectMany(w => w.TransactionDetails);
                                    //    if (dChild.Any())
                                    //        storedQuantity = dChild.Sum(w => w.Quantity) ?? 0;
                                    //}
                                    // loop on children Heads
                                    //foreach (var childHead in childTransactions)
                                    //{
                                    //    if (currentItem.HeadID != childHead.HeadIID)
                                    //    {
                                    //        var childItem = childHead.TransactionDetails.Where(t => t.ProductSKUMapID == currentItem.ProductSKUMapID).FirstOrDefault();
                                    //        storedQuantity += (childItem != null && childItem.Quantity.HasValue ? childItem.Quantity.Value : 0);
                                    //    }
                                    //}

                                    // stored quantity + current quantity > Total quantity mentioned in PO then reject current order
                                    //if ((storedQuantity + currentItem.Quantity) > (Convert.ToDecimal(parentTransaction.TransactionDetails.Where(t => t.ProductSKUMapID == currentItem.ProductSKUMapID).FirstOrDefault().Quantity)))
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
                                    bool isProductSerialNumberAutoGenerated = Convert.ToBoolean(new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(currentItem.ProductSKUMapID)).IsSerailNumberAutoGenerated);

                                    if (isProductSerialNumberAutoGenerated == true && (currentItem.SKUDetails.IsNull()
                                         || currentItem.SKUDetails.Count == 0 || currentItem.SKUDetails.First().SerialNo.IsNullOrEmpty()))
                                    {
                                        // If autogenerated, generate and create serial map collection
                                        currentItem.SKUDetails = new List<ProductSerialMapDTO>();
                                        var quantityCounter = currentItem.Quantity;
                                        while (Convert.ToInt32(quantityCounter--) > 0)
                                        {
                                            currentItem.SKUDetails.Add(new ProductSerialMapDTO()
                                            {
                                                SerialNo = currentItem.SerialNumber.IsNotNull() ? currentItem.SerialNumber : GetUniqueAutoGeneratedSerialNumber(),
                                                DetailID = currentItem.DetailIID,
                                                ProductSKUMapID = Convert.ToInt64(currentItem.ProductSKUMapID),
                                            });
                                        }
                                    }
                                    else
                                    {
                                        //var productType = new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(transaction.TransactionDetails.First().ProductSKUMapID)).ProductTypeID;
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
                                    }

                                    // Remove empty skudetail if any
                                    if (currentItem.SKUDetails.IsNotNull() && currentItem.SKUDetails.Count > 0)
                                    {
                                        currentItem.SKUDetails.RemoveAll(s => s.ProductSKUMapID == 0);
                                    }

                                    transactionHead.TransactionDetails.Add(TransactionDetailsMapper.Mapper(_callContext).ToEntity(currentItem));

                                    //}
                                }

                            }
                            else
                            {
                                transactionHead.TransactionDetails = transaction.TransactionDetails.Select(x => TransactionDetailsMapper.Mapper(_callContext).ToEntity(x)).ToList();
                            }

                            var hashCode = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE");
                            string hash = hashCode == null ? string.Empty : hashCode.SettingValue;

                            // Check for duplicate serialnumber in database
                            foreach (var line in transaction.TransactionDetails)
                            {

                                // Skip checking for autogenerated serials as we did it while generating
                                //if (!Convert.ToBoolean(new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(line.ProductSKUMapID)).IsSerailNumberAutoGenerated) && line.IsSerialNumberOnPurchase)

                                var lineSkudetail = new ProductBL(_callContext).GetProductSKUInventoryDetail(Convert.ToInt64(line.ProductSKUMapID));

                                if (lineSkudetail != null)
                                {
                                    if (!Convert.ToBoolean(lineSkudetail.IsSerailNumberAutoGenerated) && Convert.ToBoolean(lineSkudetail.IsSerialNumberOnPurchase))
                                    {
                                        foreach (var skuDetail in line.SKUDetails)
                                        {
                                            if (ProductSerialCountCheck(hash, Convert.ToInt64(line.ProductSKUMapID), skuDetail.SerialNo, skuDetail.ProductSerialID))
                                            {
                                                line.IsError = true;
                                                skuDetail.IsError = true;
                                                skuDetail.ErrorMessage = ErrorCodes.PurchaseInvoice.SI003;
                                                transaction.ErrorCode = ErrorCodes.PurchaseInvoice.SI003;
                                                return transaction;
                                            }
                                        }
                                    }
                                }

                                // Delete allocation for branches if not allocated(if allocation quantity for that branch < 1)
                                if (line.TransactionAllocations.IsNotNull() && line.TransactionAllocations.Count > 0)
                                {
                                    line.TransactionAllocations.RemoveAll(a => a.Quantity < 1);
                                }


                            }
                            // Check for duplicate serialnumber

                            //Check if user has rights to write serial key ##start
                            if (transaction.TransactionDetails.Any(a => (Framework.Enums.ProductTypes?)(new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(a.ProductSKUMapID)).ProductTypeID) == ProductTypes.Digital))
                            {
                                var serialKeyClaim = new SettingBL().GetSettingValue<long>(Eduegate.Framework.Helper.Constants.WRITESERIALKEYCLAIM);
                                var hasClaim = new SecurityRepository().HasClaimAccess(serialKeyClaim, _callContext.LoginID.Value);

                                if (!hasClaim)
                                {
                                    transaction.TransactionDetails.Where(a => (Framework.Enums.ProductTypes)(new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(a.ProductSKUMapID)).ProductTypeID) == ProductTypes.Digital).First().IsError = true;
                                    transaction.ErrorCode = ErrorCodes.PurchaseInvoice.SI004;
                                    return transaction;
                                }
                            }
                        }
                        break;
                    case Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice:
                        {
                            var productType = new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(transaction.TransactionDetails.First().ProductSKUMapID)).ProductTypeID;

                            if (productType != null && (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital)
                            {
                                var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                                productInventorySerialMapsDigi = new List<ProductInventorySerialMap>();
                                var serialKeys = new List<string>();

                                foreach (var item in transaction.TransactionDetails)
                                {

                                    // To create SI for digital product pick serial Numbers and save with transaction details
                                    var piSerialMaps = new TransactionRepository().GetUnUsedSerialKey(item.ProductSKUMapID.Value, transaction.TransactionHead.CompanyID.Value, serialKeys);

                                    // Considering quantity will be 1 always for digital products, set this as used and add it to a ilst to update later
                                    if (piSerialMaps != null)
                                    {
                                        serialKeys.Add(piSerialMaps.SerialNo);
                                        item.SerialNumber = StringCipher.Decrypt(piSerialMaps.SerialNo, hash);
                                        piSerialMaps.Used = true;
                                        productInventorySerialMapsDigi.Add(piSerialMaps);
                                    }

                                    var detailMap = TransactionDetailsMapper.Mapper(_callContext).ToEntity(item);
                                    detailMap.ProductSerialMaps = null;//serial key is allocated to row itself so mapping is not required.
                                    transactionHead.TransactionDetails.Add(detailMap);
                                }
                            }
                            else
                            {
                                // if product is physical it must have SerialNumber for every item if IsSerialNumber checked for SKU
                                var isSerialNumberEmpty = false;
                                foreach (var detail in transaction.TransactionDetails)
                                {
                                    var skuMap = new ProductDetailBL().GetProductSKUDetails(detail.ProductSKUMapID.Value);
                                    // How many config a SKU can have?
                                    if (skuMap.ProductInventorySKUConfigMaps.IsNotNull() && skuMap.ProductInventorySKUConfigMaps.Count > 0 && Convert.ToBoolean(skuMap.ProductInventorySKUConfigMaps[0].ProductInventoryConfig.IsSerialNumber))
                                    {
                                        foreach (var serialMap in detail.SKUDetails)
                                        {
                                            if (serialMap.SerialNo.IsNullOrEmpty())
                                            {
                                                isSerialNumberEmpty = true;
                                                serialMap.IsError = true;
                                                detail.IsError = true;
                                                serialMap.ErrorMessage = ErrorCodes.Transaction.T004;
                                                transaction.TransactionHead.IsError = true;
                                                transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T004;
                                                transaction.ErrorCode = ErrorCodes.Transaction.T004;
                                                return transaction;
                                            }
                                        }

                                    }
                                }

                                // If serial number empty return
                                if (isSerialNumberEmpty)
                                {
                                    return transaction;
                                }

                                // normally map dto to entity
                                transactionHead.TransactionDetails = transaction.TransactionDetails.Select(x => TransactionDetailsMapper.Mapper(_callContext).ToEntity(x)).ToList();
                            }
                        }
                        break;
                    default:
                        transactionHead.TransactionDetails = transaction.TransactionDetails.Select(x => TransactionDetailsMapper.Mapper(_callContext).ToEntity(x)).ToList();
                        break;
                }

                if (transaction.ShipmentDetails.IsNotNull())
                {
                    transactionHead.TransactionShipments.Add(TransactionShipmentMapper.Mapper(_callContext).ToEntity(transaction.ShipmentDetails));
                }

                if (transaction.TransactionHead.TaxDetails.IsNotNull())
                {
                    transactionHead.TaxTransactions = TaxTransactionMapper.Mapper(_callContext).ToEntity(transaction.TransactionHead.TaxDetails, transaction.TransactionHead.HeadIID);
                }
            }

            //save workflow if it's attached only while creating

            transactionHead.WorkflowTransactionHeadMaps = GenerateWorkflows(transactionHead);
            SetTransactionStatus(transaction, transactionHead);

            if (transaction.TransactionHead.IsError)
            {
                return transaction;
            }

            // Passing entity model data to repository
            transactionHead = new TransactionRepository().SaveTransactions(transactionHead, productInventorySerialMapsDigi);

            if (transaction.TransactionHead.HeadIID == 0)
            {
                Logging.LoggingHelper.TransactionCreated(_callContext, transactionHead);

                if (transactionHead.WorkflowTransactionHeadMaps.Count > 0)
                {
                    //get he workflow details
                    var workflow = new WorkflowRepository().GetWorkflowLog(transactionHead.HeadIID);
                    Logging.LoggingHelper.WorkflowCreated(_callContext, workflow, transactionHead);
                }
            }
            else
            {
                if (oldDocumentStatus != transactionHead.DocumentStatusID)
                {
                    Logging.LoggingHelper.TransactionStatusChanged(_callContext, transactionHead);
                }
                else
                {
                    Logging.LoggingHelper.TransactionModified(_callContext, transactionHead);
                }
            }

            // Create dto variable for OrderCOntactMap
            var dtoOrderContactMap = new OrderContactMapDTO();

            if (transactionHead.IsNotNull())
            {
                // Update LastTransactionNo in [mutual].[DocumentTypes] table
                //DocumentType entity = new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(transactionHead.DocumentTypeID), transactionHead.TransactionNo);
                var parameters = new List<KeyValueParameterDTO>();

                if (transactionHead.SupplierID.HasValue)
                {
                    parameters.Add(new KeyValueParameterDTO() { ParameterName = "SupplierID", ParameterValue = transactionHead.SupplierID.Value.ToString() });
                }

                if (transactionHead.CustomerID.HasValue)
                {
                    parameters.Add(new KeyValueParameterDTO() { ParameterName = "CustomerID", ParameterValue = transactionHead.CustomerID.Value.ToString() });
                }

                if (transactionHead.StudentID.HasValue)
                {
                    parameters.Add(new KeyValueParameterDTO() { ParameterName = "StudentID", ParameterValue = transactionHead.StudentID.Value.ToString() });
                }

                if (transaction.TransactionHead.HeadIID == 0)
                {
                    transactionHead.TransactionNo = new MutualBL(_callContext)
                        .GetAndSaveNextTransactionNumber(transactionHead.DocumentTypeID.Value);
                }
                // Save OrderContact Map
                if (transaction.OrderContactMap.IsNotNull())
                {
                    transaction.OrderContactMap.OrderID = transactionHead.HeadIID;
                    dtoOrderContactMap = new OrderBL(_callContext).SaveOrderContactMap(transaction.OrderContactMap);
                }
            }

            if (transactionHead.IsNotNull())
            {
                transactionDTO = new TransactionBL(_callContext).GetTransaction(transactionHead.HeadIID);
            }

            if (dtoOrderContactMap.IsNotNull())
            {
                transactionDTO.OrderContactMap = new OrderContactMapDTO();
                transactionDTO.OrderContactMap = dtoOrderContactMap;
            }

            if (transaction.TransactionHead.SupplierList != null)
            {
                transactionDTO.TransactionHead.SupplierList = transaction.TransactionHead.SupplierList;
            }
            if (transaction.TransactionHead.PurchaseRequests != null)
            {
                transactionDTO.TransactionHead.PurchaseRequests = transaction.TransactionHead.PurchaseRequests;
            }
            if (transaction.TransactionHead.Tender != null)
            {
                transactionDTO.TransactionHead.Tender = transaction.TransactionHead.Tender;
                transactionDTO.TransactionHead.TenderID = transaction.TransactionHead.TenderID;
            }

            //process the transaction if it's in the same process    
            return transactionDTO;
        }

        private void SetTransactionStatus(TransactionDTO transaction, TransactionHead transactionHead)
        {
            // Set Transaction Status as per Document Status 
            if (transaction.TransactionHead.DocumentStatusID.IsNull())
            {
                // If Null then Draft
                transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Hold;
            }
            else
            {
                // Get Document Statues by document reference type
                var documentStatus = new MutualRepository().GetDocumentReferenceStatusMap(Convert.ToInt32(transaction.TransactionHead.DocumentStatusID));

                if (documentStatus.IsNull())
                    documentStatus = new DocumentReferenceStatusMap() { DocumentStatusID = 1 };

                switch (documentStatus.DocumentStatusID)
                {
                    case (long)Services.Contracts.Enums.DocumentStatuses.Approved:
                        // New
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Approved;
                        transactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.New;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Cancelled:
                        // Cancelled
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Cancelled;
                        transactionHead.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Completed:
                        // Completed
                        transaction.TransactionHead.IsError = true;
                        transaction.TransactionHead.ErrorCode = ErrorCodes.Transaction.T001;
                        transaction.ErrorCode = ErrorCodes.Transaction.T001;
                        //return transaction;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Submitted:
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Submitted;
                        transactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.New;
                        break;
                    case (long)Services.Contracts.Enums.DocumentStatuses.Draft:
                    default:
                        // New
                        transactionHead.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                        transactionHead.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Hold;
                        break;
                }
            }
        }

        public List<WorkflowTransactionHeadMap> GenerateWorkflows(TransactionHead head)
        {
            var workflowMaps = new List<WorkflowTransactionHeadMap>();
            //if the workflow exists
            var workflow = new WorkflowRepository().GetWorkflowByDocumentType(head.DocumentTypeID.Value);

            if (workflow != null)
            {
                //workflowMaps.WorkflowTransactionHeadMaps = new List<WorkflowTransactionHeadMap>();
                var workflowHead = new WorkflowTransactionHeadMap()
                {
                    TransactionHeadID = head.HeadIID,
                    WorkflowID = workflow.WorkflowIID,
                    WorkflowStatusID = 1,
                    WorkflowTransactionHeadRuleMaps = new List<WorkflowTransactionHeadRuleMap>()
                };

                var totalAmount = head.TransactionHeadEntitlementMaps.Sum(a => a.Amount);
                var rules = new List<WorkflowRule>();

                switch (workflow.WorkflowFiled.ColumnName)
                {
                    case "Amount":
                        //greater than check
                        rules = workflow.WorkflowRules.Where(a => totalAmount >= decimal.Parse(a.Value1)
                        && a.ConditionID == 2).ToList();

                        if (rules.Count == 0)
                        {
                            //between check
                            rules = workflow.WorkflowRules.Where(a => decimal.Parse(a.Value1) >= totalAmount &&
                            decimal.Parse(a.Value1) <= totalAmount && a.ConditionID == 1).ToList();
                        }

                        break;
                }

                foreach (var ruleMap in rules)
                {
                    var rule = new WorkflowTransactionHeadRuleMap()
                    {
                        WorkflowStatusID = 1,
                        WorkflowRuleID = ruleMap.WorkflowRuleIID,
                        WorkflowConditionID = ruleMap.ConditionID,
                        Remarks = "Workflow assigned by the system."
                    };

                    foreach (var condition in ruleMap.WorkflowRuleConditions)
                    {
                        condition.WorkflowTransactionRuleApproverMaps = new List<WorkflowTransactionRuleApproverMap>();
                        rule.WorkflowConditionID = condition.ConditionID;

                        foreach (var approver in condition.WorkflowRuleApprovers)
                        {
                            rule.WorkflowTransactionRuleApproverMaps.Add(new WorkflowTransactionRuleApproverMap()
                            {
                                EmployeeID = approver.EmployeeID,
                                WorkflowConditionID = condition.ConditionID
                            });
                        }
                    }

                    workflowHead.WorkflowTransactionHeadRuleMaps.Add(rule);
                }

                workflowMaps.Add(workflowHead);
            }

            return workflowMaps;
        }

        List<ProductSerialMap> ProductSerialMapList(string hash, long ProductSKUMapID, string SerialNo)
        {
            var productType = new ProductDetailBL().GetProductBySKUID(ProductSKUMapID).ProductTypeID;
            var isDigitalProduct = (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital;
            return new ProductDetailBL().GetProductSerialMaps(isDigitalProduct ? StringCipher.Encrypt(SerialNo, hash) : SerialNo);
        }

        public bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        {
            List<ProductSerialMap> ProductSerialList = ProductSerialMapList(hash, ProductSKUMapID, SerialNo);
            if ((ProductSerialID > 0 && ProductSerialList.Count > 0 && ProductSerialID != ProductSerialList.First().ProductSerialIID) || ((ProductSerialID.IsNull() || ProductSerialID == 0) && ProductSerialList.Count > 0)) // Existing serial map can have only 1 row with same serial number
            {
                return true;
            }
            return false;
        }


        public TransactionDTO GetTransaction(long headIID, bool partialCalulation = false, bool checkClaims = false)
        {
            var transaction = new TransactionRepository().GetTransaction(headIID);
            return transactionEntityToDto(headIID, partialCalulation, transaction);
        }

        private TransactionDTO transactionEntityToDto(long headIID, bool partialCalulation, TransactionHead transaction)
        {
            // get the TransactionHeadEntitlementMaps by headid
            var transactionDTO = new TransactionDTO();
            var removableItems = new List<TransactionDetail>();

            // Get its Children
            var childTransactions = GetChildTransactions(Convert.ToInt64(headIID));
            // Check if partialCalculations to be considered
            if (partialCalulation)
            {

                // Loop on current transaction's items here
                foreach (var currentItem in transaction.TransactionDetails)
                {
                    // loop on children Heads
                    foreach (var childHead in childTransactions)
                    {
                        var sku = childHead.TransactionDetails.Where(t => t.ProductSKUMapID == currentItem.ProductSKUMapID).FirstOrDefault();

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
                transaction.TransactionDetails = transaction.TransactionDetails.Except(removableItems).ToList();

            }

            if (transaction != null)
            {
                transactionDTO.TransactionDetails = new List<TransactionDetailDTO>();
                transactionDTO.TransactionHead = TransactionHeadMapper.Mapper(_callContext).ToDTO(transaction);

                if (transaction.TransactionHeadEntitlementMaps != null && transaction.TransactionHeadEntitlementMaps.Count > 0)
                {
                    // Map TransactionHeadEntitlementMap using mapper
                    transactionDTO.TransactionHead.TransactionHeadEntitlementMaps = transaction.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapMapper.Mapper(_callContext).ToDTO(x)).ToList();

                    foreach (var entitlement in transactionDTO.TransactionHead.TransactionHeadEntitlementMaps)
                    {

                        if (transactionDTO.TransactionHead.DocumentReferenceType.IsNotNull() && transactionDTO.TransactionHead.DocumentReferenceType == Services.Contracts.Enums.DocumentReferenceTypes.OrderChangeRequest.ToString())
                        {
                            var parentTransaction = new TransactionRepository().GetTransaction((long)transaction.ReferenceHeadID);
                            setVoucherCodeByClaim(parentTransaction, entitlement);
                        }
                        else
                        {// ... if it is voucher
                            setVoucherCodeByClaim(transaction, entitlement);
                        }
                    }
                }

                if (transactionDTO.TransactionHead.BranchID.IsNotNull() && transactionDTO.TransactionHead.BranchID != default(long))
                    transactionDTO.TransactionHead.BranchName = new ReferenceDataRepository().GetBranch((long)transactionDTO.TransactionHead.BranchID, false).BranchName;

                if (transactionDTO.TransactionHead.ToBranchID.IsNotNull() && transactionDTO.TransactionHead.ToBranchID != default(long))
                    transactionDTO.TransactionHead.ToBranchName = new ReferenceDataRepository().GetBranch((long)transactionDTO.TransactionHead.ToBranchID, false).BranchName;

                if (transaction.TransactionDetails != null && transaction.TransactionDetails.Count > 0)
                {
                    if (_callContext.IsNotNull() && _callContext.CompanyID > 0)
                    {
                        transactionDTO.TransactionDetails = transaction.TransactionDetails.Select(x => TransactionDetailsMapper.Mapper(_callContext).ToDTO(x)).ToList();
                    }
                    else
                    {
                        transactionDTO.TransactionDetails = transaction.TransactionDetails.Select(x => TransactionDetailsMapper.Mapper(_callContext).ToDTO(x, transaction.CompanyID.Value)).ToList();
                    }

                }

                //to get all PI's for PO HeadIID
                var documentReferenceType = new TransactionRepository().GetDocumentReferenceTypeByHeadId(headIID);

                // validate it is PO
                if (documentReferenceType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                {
                    string invoiceStatus = string.Empty;
                    if (childTransactions.Count == 0)
                    {
                        transactionDTO.TransactionHead.InvoiceStatus = string.Empty;
                    }
                    if (childTransactions.IsNotNull() && childTransactions.Count > 0)
                    {
                        var isComplete = childTransactions.Any(x => x.TransactionStatusID != (int)Eduegate.Framework.Enums.TransactionStatus.Complete);
                        if (isComplete)
                        {
                            transactionDTO.TransactionHead.InvoiceStatus = "Partial";
                        }
                        else
                        {
                            transactionDTO.TransactionHead.InvoiceStatus = "Completed";
                        }
                    }
                }
                // Shipment details
                if (transaction.TransactionShipments != null)
                {
                    transactionDTO.ShipmentDetails = transaction.TransactionShipments.Select(x => TransactionShipmentMapper.Mapper(_callContext).ToDTO(x)).FirstOrDefault();
                }

                // Order contact details
                if (transaction.OrderContactMaps.IsNotNull() && transaction.OrderContactMaps.Count > 0)
                {
                    transactionDTO.OrderContactMaps = new List<OrderContactMapDTO>();

                    foreach (var ocmEntity in transaction.OrderContactMaps)
                    {
                        OrderContactMapDTO ocmDTO = OrderContactMapMapper.Mapper(_callContext).ToDTO(ocmEntity);
                        transactionDTO.OrderContactMaps.Add(ocmDTO);
                    }

                    //SO, SR, SRR
                    transactionDTO.OrderContactMap = OrderContactMapMapper.Mapper(_callContext).ToDTO(transaction.OrderContactMaps.FirstOrDefault());
                }
                var childTransaction = GetChildTransaction(Convert.ToInt64(headIID));
                if (childTransaction.IsNotNull() && childTransaction.Count > 0)
                {
                    foreach (var thead in childTransaction)
                    {
                        transaction.TransactionHead2 = thead;
                        transactionDTO.TransactionHeads = new List<TransactionHeadDTO>();
                        TransactionHeadDTO transDTO = TransactionHeadMapper.Mapper(_callContext).ToDTO(transaction.TransactionHead2);
                        transactionDTO.TransactionHeads.Add(transDTO);
                    }
                }

                if (transaction.TaxTransactions != null && transaction.TaxTransactions.Count > 0)
                {
                    // Map TransactionHeadEntitlementMap using mapper
                    transactionDTO.TransactionHead.TaxDetails = transaction.TaxTransactions.Select(x => TaxTransactionMapper.Mapper(_callContext).ToDTO(x)).ToList();
                }

                //For RFQ
                if (transaction.RFQSupplierRequestMapHeads.Count > 0)
                {
                    var getMapData = new TransactionRepository().GetSuppliersAndPurchaseReqByHeadID(headIID);

                    if (getMapData != null)
                    {
                        transactionDTO.TransactionHead.SupplierList = getMapData.SupplierList;
                        transactionDTO.TransactionHead.PurchaseRequests = getMapData.PurchaseRequests;
                        transactionDTO.TransactionHead.Tender = getMapData.TenderID.HasValue ? getMapData.Tender : null;
                        transactionDTO.TransactionHead.TenderID = getMapData.TenderID;
                    }
                }
            }

            return transactionDTO;
        }

        private void setVoucherCodeByClaim(TransactionHead transaction, TransactionHeadEntitlementMapDTO entitlement)
        {
            if (entitlement.EntitlementID == 12) // 12 for voucher
            {
                // ... get voucher
                var voucher = (transaction.TransactionHeadShoppingCartMaps.Select(y => y.ShoppingCart.ShoppingCartVoucherMaps.Select(x => x.Voucher).FirstOrDefault())).FirstOrDefault();

                //var voucherDto = VoucherBL.FromEntity(voucher);

                var settingRepository = new SettingRepository();
                var securityRepository = new SecurityRepository();

                var hasReadVoucherCode = false;
                try
                {
                    hasReadVoucherCode = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READVOUCHERCODE).SettingValue), _callContext.LoginID.Value);
                }
                catch { }

                var hasReadPartialVoucherCode = false;
                try
                {
                    hasReadPartialVoucherCode = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READPARTIALVOUCHERCODE).SettingValue), _callContext.LoginID.Value);
                }
                catch { }

                var visibleVoucherCode = string.Empty;// voucherDto.VoucherNo;

                if (hasReadPartialVoucherCode)
                {
                    var length = visibleVoucherCode.Length;

                    if (length <= 4)
                    {
                        visibleVoucherCode = new String('x', length);
                    }
                    else
                    {
                        visibleVoucherCode = new String('x', length - 4) + visibleVoucherCode.Substring(length - 4);
                    }
                }

                // ... then map
                entitlement.VoucherCode = visibleVoucherCode;
            }
        }

        public List<TransactionAllocationDTO> GetTransactionAllocations(long transactionDetailID)
        {
            //var transactionAllocationList= new TransactionRepository().GetTransactionAllocations(transactionDetailID);
            return (new TransactionRepository().GetTransactionAllocations(transactionDetailID)).Select(t => TransactionAllocationMapper.Mapper().ToDTO(t)).ToList();
        }

        public List<KeyValueDTO> GetProductInventorySerialMaps(long productSKUMapID, string searchText, int pageSize, bool serialKeyUsed = false, bool checkClaims = false)
        {

            var settingRepository = new SettingRepository();
            var securityRepository = new SecurityRepository();
            var hasFullReadClaim = false;
            try
            {
                hasFullReadClaim = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM).SettingValue), _callContext.LoginID.Value);
            }
            catch (Exception) { }

            var hasPartialReadClaim = false;
            try
            {
                hasPartialReadClaim = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READPARTIALSERIALKEYCLAIM).SettingValue), _callContext.LoginID.Value);
            }
            catch (Exception) { }
            var keyValues = new List<KeyValueDTO>();
            var serialMaps = new TransactionRepository().GetProductInventorySerialMaps(productSKUMapID, searchText, pageSize, serialKeyUsed);
            if (serialMaps != null && serialMaps.Count > 0)
            {
                var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                foreach (var serial in serialMaps)
                {
                    try
                    {
                        serial.SerialNo = StringCipher.Decrypt(serial.SerialNo, hash);
                    }
                    catch (Exception) { }
                    if (checkClaims && hasFullReadClaim)
                        keyValues.Add(new KeyValueDTO() { Key = serial.SerialNo, Value = serial.SerialNo });
                    else if (checkClaims && hasPartialReadClaim)
                    {
                        var length = serial.SerialNo.Length;
                        var visibleSerialKey = "";
                        if (length <= 4)
                        {
                            visibleSerialKey = new String('x', length);
                        }
                        else
                        {
                            visibleSerialKey = new String('x', length - 4) + serial.SerialNo.Substring(length - 4);
                        }
                        keyValues.Add(new KeyValueDTO() { Key = visibleSerialKey, Value = visibleSerialKey });
                    }
                    //else
                    //    keyValues.Add(new KeyValueDTO() { Key = visibleSerialKey, Value = visibleSerialKey });

                }
            }

            return keyValues;
        }

        public TransactionSummaryDetailDTO GetTransactionDetails(string docuementTypeID, DateTime dateFrom, DateTime dateTo)
        {
            List<int?> documentIds = docuementTypeID.Split(',')
                .Select(x =>
                {
                    int value;
                    return int.TryParse(x, out value) ? value : (int?)null;
                })
                .ToList();
            var detail = new TransactionRepository().GetTransactionDetails(documentIds, dateFrom, dateTo);
            return detail == null ? null : new TransactionSummaryDetailDTO() { TransactionTypeName = detail.TransactionTypeName, Amount = detail.Amount, TransactionCount = detail.TransactionCount };
        }

        public TransactionSummaryDetailDTO GetSupplierTransactionDetails(long loginID, string docuementTypeID, DateTime dateFrom, DateTime dateTo)
        {
            List<int?> documentIds = docuementTypeID.Split(',')
                .Select(x =>
                {
                    int value;
                    return int.TryParse(x, out value) ? value : (int?)null;
                })
                .ToList();
            var detail = new TransactionRepository().GetSupplierTransactionDetails(loginID, documentIds, dateFrom, dateTo);
            return detail == null ? null : new TransactionSummaryDetailDTO() { TransactionTypeName = detail.TransactionTypeName, Amount = detail.Amount, TransactionCount = detail.TransactionCount };
        }

        public List<TransactionHead> GetChildTransactions(long headID)
        {
            return new TransactionRepository().GetChildTransactions(headID);

        }

        public List<TransactionHead> GetChildTransaction(long headID)
        {
            return new TransactionRepository().GetChildTransaction(headID);

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
            var entityTypeEntitlements = new TransactionRepository().GetTransactionEntitlementByHeadId(headId);

            // convert from entityTypeEntitlementsto dto
            if (entityTypeEntitlements.IsNotNull() && entityTypeEntitlements.Count > 0)
            {
                entityTypeEntitlements.ForEach(x =>
                {
                    keyValueDtos.Add(new KeyValueDTO
                    {
                        Key = x.Item2.ToString(),
                        Value = x.Item1,
                    });
                });
            }
            return keyValueDtos;
        }

        // this will return id and entitlment
        public List<KeyValueDTO> GetEntitlementsByHeadIds(long headId)
        {
            List<KeyValueDTO> keyValueDtos = new List<KeyValueDTO>();
            // get entityTypeEntitlements
            var entityTypeEntitlements = new TransactionRepository().GetEntitlementsByHeadId(headId);

            // convert from entityTypeEntitlementsto dto
            if (entityTypeEntitlements.IsNotNull() && entityTypeEntitlements.Count > 0)
            {
                entityTypeEntitlements.ForEach(x =>
                {
                    keyValueDtos.Add(new KeyValueDTO
                    {
                        Key = x.EntitlementID.ToString(),
                        Value = x.EntitlementName,
                    });
                });
            }
            return keyValueDtos;
        }

        public DigitalLimitDTO DigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, long companyID)
        {
            decimal checkAmount = new CustomerBL(_callContext).CustomerVerificatonCheck(customerID) == true ? Convert.ToDecimal(new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.CUSVERIFIEDAMTLIMIT, companyID).SettingValue) : Convert.ToDecimal(new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.CUSBLOCKEDAMTLIMIT, companyID).SettingValue);
            var cartAmount = new TransactionRepository().DigitalAmountCustomerCheck(customerID, referenceType, (int)companyID);
            var totalAmount = cartAmount + cartDigitalAmount;
            return new DigitalLimitDTO()
            {
                IsAllowed = totalAmount <= checkAmount ? true : false,
                AmountLimit = cartDigitalAmount - (totalAmount - checkAmount)
            };

        }

        /// <summary>
        /// get TransactionDetail By Job Entry Head Id
        /// </summary>
        /// <param name="jobEntryHeadId"></param>
        /// <param name="partialCalulation"></param>
        /// <returns></returns>
        public TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false)
        {
            TransactionHead transaction = new TransactionRepository().GetTransactionByJobEntryHeadId(jobEntryHeadId);

            if (transaction.IsNotNull() && transaction.HeadIID > 0)
            {
                return transactionEntityToDto(transaction.HeadIID, partialCalulation, transaction);
            }
            else
            {
                return null;
            }
        }

        public bool HasDigitalProduct(long headID)
        {
            return new TransactionRepository().HasDigitalProduct(headID);
        }

        public List<TransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey, bool IsDigital)
        {
            var transactions = new TransactionRepository().GetAllTransactionsBySerialKey(serialKey);
            if (IsDigital == true)
            {
                var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                serialKey = StringCipher.Encrypt(serialKey, hash);
                var reftransactions = new TransactionRepository().GetAllTransactionsBySerialKey(serialKey);
                transactions.AddRange(reftransactions);
            }
            List<long> heads = new List<long>();
            var references = transactions.Where(x => x.ReferenceHeadID.HasValue).Select(x => (long)x.ReferenceHeadID).ToList();
            heads.AddRange(references);
            transactions.AddRange(new TransactionRepository().GetParentTransactions(heads.IsNotNull() ? heads : null));
            return TransactionHeadMapper.Mapper(_callContext).TodTO(transactions);
        }

        public bool CancelTransaction(long headID)
        {
            var head = new TransactionRepository().GetTransaction(headID);

            switch ((DocumentReferenceTypes)head.DocumentType.ReferenceTypeID)
            {
                case DocumentReferenceTypes.SalesOrder:
                    return new TransactionRepository().CancelSalesOrderTransaction(headID);
                case DocumentReferenceTypes.SalesInvoice:
                    return new TransactionRepository().CancelSalesInvoiceTransaction(headID);
                case DocumentReferenceTypes.SalesQuote:
                    return new TransactionRepository().CancelSalesQuotationTransaction(headID);
                case DocumentReferenceTypes.PurchaseOrder:
                    return new TransactionRepository().CancelPurchaseOrderTransaction(headID);
                case DocumentReferenceTypes.PurchaseInvoice:
                    return new TransactionRepository().CancelPurchaseInvoiceTransaction(headID);
                case DocumentReferenceTypes.PurchaseQuote:
                    return new TransactionRepository().CancelPurchaseQuoteTransaction(headID);
                case DocumentReferenceTypes.PurchaseTender:
                    return new TransactionRepository().CancelPurchaseTenderTransaction(headID);
            }

            return false;
        }

        public bool CalculateEntitlement(TransactionDTO transaction)
        {
            decimal detailSum = 0;
            decimal entitlementSum = 0;


            if (transaction.TransactionHead.TransactionHeadEntitlementMaps.IsNotNull() && transaction.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
            {
                foreach (var entitlement in transaction.TransactionHead.TransactionHeadEntitlementMaps)
                {
                    entitlementSum += entitlement.Amount.HasValue ? entitlement.Amount.Value : default(decimal);
                }
            }

            if (transaction.TransactionDetails.IsNotNull() && transaction.TransactionDetails.Count > 0)
            {

                foreach (var detail in transaction.TransactionDetails)
                {
                    detailSum += Convert.ToDecimal(detail.UnitPrice * detail.Quantity);
                }

                detailSum += transaction.TransactionHead.DeliveryCharge.IsNull() ? 0 : transaction.TransactionHead.DeliveryCharge.Value;
                detailSum -= transaction.TransactionHead.DiscountAmount.IsNull() ? 0 : transaction.TransactionHead.DiscountAmount.Value;
            }

            if (transaction.TransactionHead.TaxDetails.IsNotNull() && transaction.TransactionHead.TaxDetails.Count > 0)
            {
                foreach (var tax in transaction.TransactionHead.TaxDetails)
                {
                    detailSum += Convert.ToDecimal(tax.ExclusiveTaxAmount);
                }
            }

            if (entitlementSum != detailSum)
            {
                var diffference = detailSum - entitlementSum;
                // If entitlement is Visa/Master consider x.010 difference
                if (transaction.TransactionHead.TransactionHeadEntitlementMaps.Any(e => e.EntitlementID == 16) && diffference >= 0)
                {
                    return false;
                }
                else if (transaction.TransactionHead.TransactionHeadEntitlementMaps.Any(e => e.EntitlementID == 7) && (diffference >= 0 && diffference <= (decimal)0.010))
                {
                    return false;
                }

                return true;
            }
            else
                return false;
        }

        public OrderDetailDTO GetDeliveryDetails(long Id)
        {
            var order = new TransactionRepository().GetDeliveryDetails(Id);
            if (order.IsNotNull())
            {
                var orderDTO = new OrderDetailDTO();
                orderDTO = TransactionHeadMapper.Mapper(_callContext).FromEntityToDTO(order);
                return orderDTO;
            }
            else
            {
                return null;
            }
        }

        public OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO dto)
        {
            var order = new TransactionHead();

            order = TransactionHeadMapper.Mapper(_callContext).FromDTOToEntity(dto);
            if (dto.IsNotNull())
            {
                var orderDetail = new TransactionRepository().SaveTransactions(order);
            }
            return null;
        }

        public List<DeliverySettingDTO> GetDeliveryOptions()
        {
            List<DeliveryTypes1> entities = new TransactionRepository().GetDeliveryOptions();
            return DeliveryOptionsMapper.Mapper(_callContext).ToDTO(entities);
        }

        public bool IsChangeRequestDetailProcessed(long transactionDetailID)
        {
            return new TransactionRepository().IsChangeRequestDetailProcessed(transactionDetailID);
        }

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

        public List<OrderDeliveryDisplayHeadMap> GetOrderDeliveryTextByHeadID(long headID)
        {
            return new TransactionRepository().GetOrderDeliveryTextByHeadID(headID);
        }

        public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        {
            var ProductBundle = new List<ProductBundle>();
            var productBundleDTO = new List<ProductBundleDTO>();
            // get the Product Bundle Item Details
            ProductBundle = new TransactionRepository().GetProductBundleItemDetail(productSKUMapID);

            // convert from TransactionDetail to TransactionDetailDTO
            foreach (ProductBundle detail in ProductBundle)
            {
                productBundleDTO.Add(new ProductBundleDTO()
                {
                    FromProductID = detail.FromProductID,
                    Quantity = detail.Quantity,
                    FromProductSKUMapID = detail.FromProductSKUMapID,
                    CostPrice = detail.CostPrice,

                });
            }

            return productBundleDTO;
        }


        public StockVerificationDTO SaveStockVerificationDatas(StockVerificationDTO stockDatas)
        {
            var dat = stockDatas;

            using (var dbContext = new dbEduegateERPContext())
            {
                var docType = dbContext.DocumentTypes.Where(s => s.TransactionTypeName == "Physical Stock Entry").AsNoTracking().FirstOrDefault();
                var incrLastNo = docType.LastTransactionNo + 1;

                if (dat.HeadIID != 0 && dat.CurrentStatusID == (int)Services.Contracts.Enums.DocumentStatuses.Submitted)
                {
                    throw new Exception("Can't edit already submitted transaction");
                }

                if (dat.HeadIID != 0)
                {
                    //edit case delete all related map details and re-entry
                    var getMapDatas = dbContext.StockCompareDetails.Where(x => x.HeadID == dat.HeadIID).ToList();
                    if (getMapDatas.Count > 0)
                    {
                        dbContext.StockCompareDetails.RemoveRange(getMapDatas);
                        dbContext.SaveChanges();
                    }
                }

                if (stockDatas != null)
                {
                    var entity = new StockCompareHead()
                    {
                        HeadIID = dat.HeadIID,
                        BranchID = dat.BranchID,
                        PostedDate = dat.PostedDate,
                        TransactionNo = dat.HeadIID == 0 ? docType.TransactionNoPrefix + incrLastNo : dat.TransactionNo,
                        TransactionDate = dat.TransactionDate,
                        EmployeeID = dat.EmployeeID,
                        SchoolID = (byte?)_callContext.SchoolID,
                        AcademicYearID = _callContext.AcademicYearID,
                        PostedComments = dat.PostedComments,
                        TransactionStatusID = dat.TransactionStatusID,
                        CreatedBy = (int?)(dat.HeadIID != 0 ? dat.CreatedBy : _callContext.LoginID),
                        CreatedDate = dat.HeadIID != 0 ? dat.CreatedDate : DateTime.Now,
                        UpdatedBy = dat.HeadIID != 0 ? (int?)_callContext.LoginID : dat.UpdatedBy,
                        UpdatedDate = dat.HeadIID != 0 ? DateTime.Now : dat.UpdatedDate,
                    };

                    dbContext.Add(entity);

                    if (entity.HeadIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        //increment Doc Transaction No
                        docType.LastTransactionNo = incrLastNo;
                        dbContext.Entry(docType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }

                    //TODO : not have reference with CompareHead Table , so using another context for save
                    foreach (var listDatas in stockDatas.StockVerificationMap)
                    {
                        if (listDatas.PhysicalQuantity != null)
                        {
                            var getPrdctID = dbContext.ProductSKUMaps.Where(p => p.ProductSKUMapIID == listDatas.ProductSKUMapID).AsNoTracking().FirstOrDefault();

                            var mapEntity = new StockCompareDetail()
                            {
                                HeadID = entity.HeadIID,
                                //DetailIID = listDatas.DetailIID,
                                ProductID = getPrdctID != null ? getPrdctID.ProductID : null,
                                ProductSKUMapID = listDatas.ProductSKUMapID,
                                PhysicalQuantity = listDatas.PhysicalQuantity,
                                Remark = listDatas.Remark,
                                CreatedBy = listDatas.CreatedBy != null ? listDatas.CreatedBy : _callContext.LoginID,
                                CreatedDate = listDatas.CreatedDate != null ? listDatas.CreatedDate : DateTime.Now,
                                UpdatedBy = _callContext.LoginID,
                                UpdatedDate = DateTime.Now,
                                ActualQuantity = (decimal?)listDatas.AvailableQuantity,
                                DifferQuantity = listDatas.PhysicalQuantity - listDatas.AvailableQuantity,
                            };

                            dbContext.StockCompareDetails.Add(mapEntity);
                            dbContext.SaveChanges();
                        }
                    }

                    return GetStockVerificationDatas(entity.HeadIID);
                }
                else
                {
                    return null;
                }
            }
        }

        public StockVerificationDTO GetStockVerificationDatas(long headIID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var returnData = new StockVerificationDTO();
            // get Stock verification details by headIID
            using (var dbContext = new dbEduegateERPContext())
            {
                var getStockData = dbContext.StockCompareHeads.Where(x => x.HeadIID == headIID).AsNoTracking().FirstOrDefault();
                var employee = dbContext.Employees.Where(e => e.EmployeeIID == getStockData.EmployeeID).AsNoTracking().FirstOrDefault();
                var branchName = dbContext.Branches.Where(b => b.BranchIID == getStockData.BranchID).AsNoTracking().FirstOrDefault();
                var docTypes = dbContext.DocumentStatuses.Where(b => b.DocumentStatusID == getStockData.TransactionStatusID).AsNoTracking().FirstOrDefault();

                returnData.BranchID = getStockData.BranchID;
                returnData.HeadIID = getStockData.HeadIID;
                returnData.EmployeeID = getStockData.EmployeeID;
                returnData.TransactionDate = getStockData.TransactionDate;
                returnData.TransactionNo = getStockData.TransactionNo;
                returnData.TransactionStatusID = getStockData.TransactionStatusID;
                returnData.CurrentStatusID = getStockData.TransactionStatusID;
                returnData.PostedComments = getStockData.PostedComments != null ? getStockData.PostedComments : null;
                returnData.PostedDate = getStockData.PostedDate;
                returnData.SchoolID = getStockData.SchoolID;
                returnData.AcademicYearID = getStockData.AcademicYearID;
                returnData.CreatedBy = getStockData.CreatedBy;
                returnData.CreatedDate = getStockData.CreatedDate;

                returnData.Employee = new KeyValueDTO()
                {
                    Value = employee.EmployeeCode + " - " + employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                    Key = employee.EmployeeIID.ToString()
                };

                returnData.Branch = new KeyValueDTO()
                {
                    Value = branchName.BranchName,
                    Key = branchName.BranchIID.ToString()
                };

                returnData.TransactionStatus = new KeyValueDTO()
                {
                    Value = docTypes?.StatusName,
                    Key = docTypes?.DocumentStatusID.ToString()
                };

                returnData.PhysicalStockVerfiedBy = employee.EmployeeCode + " - " + employee.FirstName + " " + employee.MiddleName + " " + employee.LastName;

                var getMapDatas = dbContext.StockCompareDetails.Where(x => x.HeadID == headIID).OrderBy(s => s.DetailIID).ToList();

                foreach (var listDat in getMapDatas)
                {
                    var prdctItem = dbContext.ProductSKUMaps.
                        Where(p => p.ProductSKUMapIID == listDat.ProductSKUMapID)
                        .Include(p => p.Product)
                        .Include(p => p.ProductInventories)
                        .AsNoTracking().FirstOrDefault();

                    returnData.StockVerificationMap.Add(new StockVerificationMapDTO()
                    {
                        HeadID = listDat.HeadID,
                        DetailIID = listDat.DetailIID,
                        ProductID = listDat.ProductID,
                        PhysicalQuantity = listDat.PhysicalQuantity,
                        Remark = listDat.Remark != null ? listDat.Remark : null,
                        CreatedBy = listDat.CreatedBy,
                        CreatedDate = listDat.CreatedDate,
                        AvailableQuantity = listDat.ActualQuantity,
                        ProductSKUMapID = listDat.ProductSKUMapID,
                        BookStock = prdctItem.ProductInventories
                        .Where(s => s.ProductSKUMapID == listDat.ProductSKUMapID && s.BranchID == branchName.BranchIID)
                        .AsEnumerable()
                        .Sum(d => d.Quantity),
                        //DifferQuantity = listDat.PhysicalQuantity - listDat.ActualQuantity,
                        ProductSKU = new KeyValueDTO()
                        {
                            Value = prdctItem != null ? prdctItem.Product.ProductName + " - " + prdctItem?.ProductSKUCode : null,
                            Key = prdctItem != null ? prdctItem.ProductSKUMapIID.ToString() : null,
                        },
                        Description = prdctItem != null ? prdctItem.Product.ProductName : null,
                    });
                }
                return returnData;
            }
        }



        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetails(string documentTypeID, int pageNumber, int pageSize,
        string customerID = null, CallContext context = null,
        long orderID = default(long), bool withCurrencyConversion = true)
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var HistoryDetails = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();
            Services.Contracts.OrderHistory.OrderHistoryDTO orderHistory = null;
            decimal ConversionRate = 1;
            var userDetail = new UserDTO();

            if (context.IsNull())
            {
                context = new CallContext();
                context.CurrencyCode = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CurrencyCode");
            }

            // get the converted price 
            ConversionRate = new ReferenceDataRepository().GetCurrencyPrice(context);
            if (!withCurrencyConversion)
                ConversionRate = 1;

            if (orderID == default(long) && customerID.IsNullOrEmpty())
                userDetail = null;

            // Get order detail
            var userHistoryList = new Eduegate.Domain.Repository.TransactionRepository().GetOrderHistoryItemDetails(documentTypeID,
                Convert.ToString(customerID), pageNumber, pageSize,
                orderID, this._callContext.CompanyID.Value);


            if (userHistoryList.IsNotNull() && userHistoryList.Count > 0)
            {
                foreach (var historyHeader in userHistoryList)
                {
                    orderHistory = new Services.Contracts.OrderHistory.OrderHistoryDTO();
                    orderHistory.OrderDetails = new List<Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO>();
                    decimal subTotalAmount = 0;
                    orderHistory.TransactionDate = historyHeader.TransactionDate;
                    orderHistory.TransactionNo = historyHeader.TransactionNo;
                    orderHistory.TransactionOrderIID = historyHeader.TransactionOrderIID;
                    orderHistory.DeliveryText = new ShoppingCartBL(null).DeliveryTypeText(historyHeader.DeliveryTypeID, 1);
                    if (historyHeader.OrderDetails.IsNotNull() && historyHeader.OrderDetails.Count > 0)
                    {
                        foreach (var historyDetail in historyHeader.OrderDetails)
                        {
                            orderHistory.OrderDetails.Add(new Eduegate.Services.Contracts.OrderHistory.OrderDetailDTO()
                            {
                                TransactionIID = historyDetail.TransactionIID,
                                ProductID = historyDetail.ProductID,
                                ProductName = historyDetail.ProductName,
                                ProductImageUrl = historyDetail.ProductImageUrl,
                                ProductSKUMapID = historyDetail.ProductSKUMapID,
                                Quantity = historyDetail.Quantity,
                                UnitID = historyDetail.UnitID,
                                DiscountPercentage = historyDetail.DiscountPercentage,
                                UnitPrice = historyDetail.UnitPrice * ConversionRate,
                                Amount = historyDetail.Amount * ConversionRate, // price*quantity saved in Amount fiels in detail
                                ExchangeRate = historyDetail.ExchangeRate,
                                Properties = shoppingCartRepository.GetPropertiesBySKU(Convert.ToInt64(historyDetail.ProductSKUMapID)),
                                Categories = shoppingCartRepository.GetCategoriesBySKU(Convert.ToInt64(historyDetail.ProductSKUMapID)),
                                ProductUrl = string.Format("productID={0}&productName={1}&skuID={2}", Convert.ToString(historyDetail.ProductID), Convert.ToString(historyDetail.ProductName), Convert.ToString(historyDetail.ProductSKUMapID)),
                                SerialNumber = historyDetail.SerialNumber,
                                ActualQuantity = historyDetail.ActualQuantity
                            });

                            subTotalAmount = subTotalAmount + Convert.ToDecimal(historyDetail.Amount * ConversionRate);
                        }
                    }
                    orderHistory.Currency = _callContext.CurrencyCode;
                    orderHistory.DiscountAmount = historyHeader.DiscountAmount;
                    orderHistory.SubTotal = subTotalAmount;
                    orderHistory.Total = subTotalAmount + (orderHistory.DiscountAmount.HasValue ? orderHistory.DiscountAmount.Value : 0);
                    orderHistory.UserDetail = userDetail;
                    HistoryDetails.Add(orderHistory);
                }
            }

            return HistoryDetails;
        }



        #region OLD code commented -- Get ProductList by BranchID  for StockVerification screen
        //public List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID)
        //{
        //    var returnData = new List<StockVerificationMapDTO>();

        //    using (var dbContext = new dbEduegateERPContext())
        //    {
        //        var prductItems = dbContext.ProductSKUMaps.Where(s => s.ProductID != null && s.ProductInventories.Any(x => x.BranchID == branchID)).ToList();

        //        foreach(var item in prductItems)
        //        {
        //            if (!returnData.Any(x => x.ProductSKUMapID == item.ProductSKUMapIID))
        //            {
        //                var getQuantity = dbContext.ProductInventories.Where(u => u.ProductSKUMapID == item.ProductSKUMapIID && u.BranchID == branchID).ToList();

        //                returnData.Add(new StockVerificationMapDTO()
        //                {
        //                    ProductID = item.ProductID != null ? item.ProductID : null,
        //                    ProductSKUMapID = item.ProductSKUMapIID,
        //                    AvailableQuantity = getQuantity.Sum(s => s.Quantity),
        //                    ProductSKU = new KeyValueDTO()
        //                    {
        //                        Value = item != null ? item.Product.ProductName + " - " + item?.ProductSKUCode : null,
        //                        Key = item != null ? item.ProductSKUMapIID.ToString() : null,
        //                    },
        //                    SKUID = new KeyValueDTO()
        //                    {
        //                        Value = item != null ? item.Product.ProductName + " - " + item?.ProductSKUCode : null,
        //                        Key = item != null ? item.ProductSKUMapIID.ToString() : null,
        //                    },
        //                    Description = item != null ? item.Product?.ProductDescription != null ? item.Product?.ProductDescription : item?.SKUName : null,
        //                });
        //            }
        //        }

        //        return returnData;
        //    }
        //}
        #endregion


        //Procedure Function for get Products for stockverification data 
        public List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID, DateTime date)
        {
            var listData = new List<StockVerificationMapDTO>();
            using (var dbContext = new dbEduegateERPContext())
            {
                string message = string.Empty;
                SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                _sBuilder.ConnectTimeout = 30; // Set Timedout
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                {
                    try { conn.Open(); }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_GET_PRODUCTSKU_BY_BRANCHID_DATE]", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@BranchID", SqlDbType.VarChar));
                        adapter.SelectCommand.Parameters["@BranchID"].Value = branchID.ToString();

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime));
                        adapter.SelectCommand.Parameters["@Date"].Value = date;

                        DataSet dt = new DataSet();
                        adapter.Fill(dt);
                        DataTable dataTable = null;

                        if (dt.Tables.Count > 0)
                        {
                            if (dt.Tables[0].Rows.Count > 0)
                            {
                                dataTable = dt.Tables[0];
                            }
                        }

                        if (dataTable != null)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                listData.Add(new StockVerificationMapDTO()
                                {
                                    ProductSKUMapID = (long?)row["ProductSKUMapIID"],
                                    ProductID = (long?)row["ProductID"],
                                    AvailableQuantity = (decimal?)row["Quantity"],
                                    ProductSKU = new KeyValueDTO()
                                    {
                                        Value = (string)row["ProductSKUValue"],
                                        Key = row["ProductSKUKey"].ToString(),
                                    },
                                    SKUID = new KeyValueDTO()
                                    {
                                        Value = (string)row["ProductSKUValue"],
                                        Key = row["ProductSKUKey"].ToString(),
                                    },
                                    Description = (string)row["Description"],
                                });
                            }
                        }
                        return listData;
                    }
                }

            }

        }

        //Get Counts for Dashbord BoilerPlates
        public TransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID)
        {
            var returnData = new TransactionSummaryDetailDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var transactions = dbContext.TransactionDetails.Where(x => x.TransactionHead.DocumentTypeID == docTypeID && x.TransactionHead.SchoolID == _callContext.SchoolID).AsNoTracking().ToList();

                var dailyAmount = transactions.Where(y => y.TransactionHead.TransactionDate.Value.Date == DateTime.Now.Date).Sum(s => s.Amount);
                var monthlyAmount = transactions.Where(y => y.TransactionHead.TransactionDate.Value.Month == DateTime.Now.Month && y.TransactionHead.TransactionDate.Value.Year == DateTime.Now.Year).Sum(s => s.Amount);
                var yearlyAmount = transactions.Where(y => y.TransactionHead.TransactionDate.Value.Year == DateTime.Now.Year).Sum(s => s.Amount);

                returnData.YearlyAmount = yearlyAmount;
                returnData.MonthlyAmount = monthlyAmount;
                returnData.DailyAmount = dailyAmount;

                return returnData;
            }
        }


        #region for PurchaseQuatation

        public List<TransactionDetailDTO> GetProductsByPurchaseRequestID(List<long> request_IDs)
        {
            var transaction = new TransactionRepository().GetProductsByPurchaseRequestID(request_IDs);
            return transaction;
        }

        public string SaveRFQMappingData(TransactionDTO dto)
        {
            var transaction = new TransactionRepository().SaveRFQMappingData(dto);
            return null;
        }

        #endregion

        //Update Quotation from supplier - Vendor Portal
        public long UpdateQuotation(TransactionDTO dto)
        {
            var returnIID = new TransactionRepository().UpdateQuotation(dto);
            return returnIID;
        }

        //Quotation Compare screen
        public List<TransactionDetailDTO> FillQuotationItemList(List<long> quotation_IDs)
        {
            var dataList = new TransactionRepository().FillQuotationItemList(quotation_IDs);
            return dataList;
        }
        
        public List<TransactionDetailDTO> FillBidItemList(string bidApprovalIID)
        {
            var dataList = new TransactionRepository().FillBidItemList(bidApprovalIID);
            return dataList;
        }

        public List<KeyValueDTO> FillQuotationsByRFQ(string rfqHeadIID)
        {
            var data = new TransactionRepository().FillQuotationsByRFQ(rfqHeadIID);
            return data;
        } 
        
        public List<KeyValueDTO> FillBidLookUpByRFQ(string rfqHeadIID)
        {
            var data = new TransactionRepository().FillBidLookUpByRFQ(rfqHeadIID);
            return data;
        }

        public TransactionDTO GetQuotationListByTenderID(long? tenderIID)
        {
            var result = new TransactionDTO();

            var quotationIDs = new List<long>();

            var dto = new TenderMasterMapper().entityToDTO((long)tenderIID);

            result.TransactionHead = new TransactionHeadDTO()
            {
                TenderID = dto.TenderIID,
                TenderName = dto.Name,
                TenderDescription = dto.Description,
            };

            quotationIDs = new TransactionRepository().GetQuotationIDsByTenderID(tenderIID);

            result.TransactionDetails = new TransactionRepository().FillQuotationItemList(quotationIDs);

            return result;
        }


        public TransactionDTO SaveBidApprovalItemList(TransactionDTO dto)
        {
            var saveData = new TransactionRepository().SaveBidApprovalItemList(dto);

            return saveData;
        }


        public string SendTransactionMailToSupplier(TransactionDTO dto,string purposeMsg)
        {
            string classCode = string.Empty;

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            var defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
            var loginLink = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_VENDOR_LOGIN_LINK");

            foreach (var sup in dto.TransactionHead.SupplierList)
            {
                var supplierDetails = new SupplierRepository().GetSupplier(long.Parse(sup.Key.ToString()));

                var emailTemplate = EmailTemplateMapper.Mapper(_callContext).GetEmailTemplateDetails(EmailTemplates.TransactionMailToSupplier.ToString());

                var emailSubject = string.Empty;
                var emailBody = string.Empty;
                if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
                {
                    emailSubject = emailTemplate.Subject;

                    emailBody = emailTemplate.EmailTemplate;

                    emailBody = emailBody.Replace("{TransactionTypeName}", dto.TransactionHead.DocumentTypeName);
                    emailBody = emailBody.Replace("{SupplierName}", supplierDetails.FirstName);
                    emailBody = emailBody.Replace("{TransactionNo}", dto.TransactionHead.TransactionNo);
                    emailBody = emailBody.Replace("{PurposeMsg}", purposeMsg);
                    emailBody = emailBody.Replace("{LoginLink}", loginLink);

                    emailSubject = emailSubject.Replace("{TransactionTypeName}", dto.TransactionHead.DocumentTypeName);
                    emailSubject = emailSubject.Replace("{TransactionNo}", dto.TransactionHead.TransactionNo);
                }

                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).PopulateBody(supplierDetails.SupplierEmail, emailBody);

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(supplierDetails.SupplierEmail, emailSubject, mailMessage, Services.Contracts.Enums.EmailTypes.VendorPortal, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(defaultMail, emailSubject, mailMessage, Services.Contracts.Enums.EmailTypes.VendorPortal, mailParameters);
                    }
                }
            }

            return null;
        }

        public string MailQTSubmission(long headIID)
        {
            var transDTO = new TransactionRepository().GetTransactionDTO(headIID);

            var qtSubmitStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSACTION_DOC_STS_ID_SUBMITTED");
            if (transDTO.TransactionHead.DocumentStatusID == long.Parse(qtSubmitStatusID))
            {
                var toEmpList = new List<Eduegate.Services.Contracts.Payroll.EmployeeDTO>();

                toEmpList = new TransactionRepository().GetQTSubmissionEmployeeMaiIDs(headIID);

                if(toEmpList != null)
                {
                    SendQTSubmissionMail(transDTO, toEmpList);
                    SendQTSubmissionPushNotification(transDTO, toEmpList);
                }
            }

            return null;
        }

        public string SendQTSubmissionMail(TransactionDTO dto, List<Eduegate.Services.Contracts.Payroll.EmployeeDTO> toEmpList)
        {
            string classCode = string.Empty;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            var defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            foreach (var emp in toEmpList)
            {
                var emailTemplate = EmailTemplateMapper.Mapper(_callContext).GetEmailTemplateDetails(EmailTemplates.QuotationSubmission.ToString());

                var emailSubject = string.Empty;
                var emailBody = string.Empty;
                if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
                {
                    emailSubject = emailTemplate.Subject;

                    emailBody = emailTemplate.EmailTemplate;

                    emailBody = emailBody.Replace("{RFQNumber}", dto.TransactionHead.ReferenceTransactionNo);
                    emailBody = emailBody.Replace("{EmployeeName}", emp.EmployeeName);
                    emailBody = emailBody.Replace("{TransactionNo}", dto.TransactionHead.TransactionNo);
                    emailBody = emailBody.Replace("{SubmittedBy}", dto.TransactionHead.SupplierName);
                    emailBody = emailBody.Replace("{DateOfSubmission}", DateTime.Now.ToString(dateFormat));

                    emailSubject = emailSubject.Replace("{RFQNumber}", dto.TransactionHead.ReferenceTransactionNo);
                }

                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).PopulateBody(emp.WorkEmail, emailBody);

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(emp.WorkEmail, emailSubject, mailMessage, Services.Contracts.Enums.EmailTypes.VendorPortal, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_callContext).SendMail(defaultMail, emailSubject, mailMessage, Services.Contracts.Enums.EmailTypes.VendorPortal, mailParameters);
                    }
                }
            }

            return null;
        }

        #region push notification
        public string SendQTSubmissionPushNotification(TransactionDTO dto, List<Eduegate.Services.Contracts.Payroll.EmployeeDTO> toEmpList)
        {
            foreach (var emp in toEmpList)
            {
                long fromLoginID = dto.TransactionHead?.SupplierLogiID != null ? (long)dto.TransactionHead?.SupplierLogiID : 2 ;
                var settings = Eduegate.Domain.Mappers.Notification.Helpers.NotificationSetting.GetEmployeeAppSettings();
                var title = "Quotation Submission Against RFQ Number : " + dto.TransactionHead.ReferenceTransactionNo;
                var message = "A quotation has been successfully submitted in response to the RFQ number : " + dto.TransactionHead.ReferenceTransactionNo + ". Please review the quotation at your earliest convenience.";
                long toLoginID = (long)emp.LoginID;

                if (toLoginID != 0)
                {
                    PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                }
            }

            return null;
        }
        #endregion
    }
}
