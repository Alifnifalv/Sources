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
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Repository.Accounts;

namespace Eduegate.Domain
{
    public class AccountingTransactionBL
    {
        private CallContext _callContext;

        public AccountingTransactionBL(CallContext context)
        {
            _callContext = context;
        }

        public CustomerAccountMapsDTO GetCustomerAccountMap(long CustomerID, int EntitlementID)
        {
            var customerAccountMap = new AccountTransactionRepository().GetCustomerAccountMap(CustomerID, EntitlementID);
            if (customerAccountMap != null)
            {
                return new CustomerAccountMapsDTO { CustomerAccountMapIID = customerAccountMap.CustomerAccountMapIID, CustomerID = customerAccountMap.CustomerID, AccountID = customerAccountMap.AccountID };
            }
            return null;

        }

        public SupplierAccountEntitlmentMapsDTO GetSupplierAccountMap(long SupplierID, int EntitlementID)
        {
            var supplierAccountMap = new AccountTransactionRepository().GetSupplierAccountMap(SupplierID, EntitlementID);
            if (supplierAccountMap != null)
            {
                return new SupplierAccountEntitlmentMapsDTO
                {
                    AccountID = (long)supplierAccountMap.AccountID,
                    SupplierID = (long)supplierAccountMap.SupplierID,
                    SupplierAccountMapIID = supplierAccountMap.SupplierAccountMapIID
                };
            }
            return null;
        }

        public List<ProductInventoryDTO> GetProductInventory(long skuID)
        {
            List<ProductInventoryDTO> ProductInventoryDTOList = new List<ProductInventoryDTO>();
            var ProductInventoryList = new ProductDetailRepository().GetProductInventory(skuID);
            foreach (ProductInventory item in ProductInventoryList)
            {
                ProductInventoryDTOList.Add(new ProductInventoryDTO { ProductSKUMapID = item.ProductSKUMapID, CostPrice = item.CostPrice });
            }
            return ProductInventoryDTOList;

        }

        public bool DeleteAccountTransactions(List<long> headIds)
        {
            return new AccountTransactionRepository().DeleteAccountTransactions(headIds);
        }

        public bool AddAccountTransactions(List<AccountTransactionsDTO> AccountTransactionsDTOList)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var accountTransactions = new List<AccountTransaction>();
                var dtoItem = AccountTransactionsDTOList.FirstOrDefault();

                var docTypes = dbContext.DocumentTypes.ToList();
                var grnDocTypeID = docTypes.Find(x => x.TransactionNoPrefix == "GRN-").DocumentTypeID;

                foreach (var transaction in AccountTransactionsDTOList)
                {
                    if (grnDocTypeID != transaction.DocumentTypeID)
                    {
                        var accountTransaction = new AccountTransaction
                        {
                            AccountID = transaction.AccountID,
                            Amount = transaction.Amount,
                            ExclusiveTaxAmount = transaction.ExclusiveTaxAmount,
                            InclusiveTaxAmount = transaction.InclusiveTaxAmount,
                            DiscountAmount = transaction.DiscountAmount,
                            DiscountPercentage = transaction.DiscountPercentage,
                            DebitOrCredit = transaction.DebitOrCredit,
                            Description = transaction.Description,
                            CreatedDate = DateTime.Now,
                            DocumentTypeID = transaction.DocumentTypeID,
                            CostCenterID = transaction.CostCenterID,
                            BudgetID= transaction.BudgetID,                            
                            TransactionDate = transaction.TransactionDate,
                            TransactionNumber = transaction.TransactionNumber,
                            CreatedBy = transaction.CreatedBy
                        };

                        if (transaction.TransactionHeadAccountMap != null)
                        {
                            accountTransaction.TransactionHeadAccountMaps.Add(
                                new TransactionHeadAccountMap
                                {
                                    TransactionHeadID = transaction.TransactionHeadAccountMap.TransactionHeadID,
                                    CreatedDate = transaction.TransactionHeadAccountMap.CreatedDate,
                                    UpdatedDate = transaction.TransactionHeadAccountMap.UpdatedDate
                                });
                        }

                        accountTransactions.Add(accountTransaction);
                    }
                }

                bool blnOperationStatus = accountTransactions.Count > 0 ? new AccountTransactionRepository().AddAccountTransactions(accountTransactions) : false;
                if (blnOperationStatus)
                {
                    long headId = (long)dtoItem.TransactionHeadID;
                    switch (dtoItem.TransactionType)
                    {
                        case 2:
                            blnOperationStatus = new AccountTransactionRepository()
                                .Update_AssetTransaction_ProcessStatus(headId, (int)Eduegate.Framework.Enums.TransactionStatus.Complete);
                            break;
                        case 3:
                        case 1:
                            blnOperationStatus = new AccountTransactionRepository()
                                .Update_AccountingTransaction_ProcessStatus(headId, (int)Eduegate.Framework.Enums.TransactionStatus.Complete);
                            break;
                        case 4:
                            blnOperationStatus = new AccountTransactionRepository()
                                .Update_MissionJob_ProcessStatus(headId, 4);//DB Table jobs.JobOperationStatuses - 4- Completed
                            break;
                    }
                }

                return blnOperationStatus;
            }
        }

        public VoucherMasterDTO GetVoucher(long TransactionHeadID)
        {
            string strShoppingCartID = new ShoppingCartBL(_callContext).GetCartDetailByHeadID(TransactionHeadID);
            long ShoppingCartID = 0;
            long VoucherID = 0;
            if (strShoppingCartID != string.Empty)
            {
                ShoppingCartID = Convert.ToInt64(strShoppingCartID);
                var cartDetails = new ShoppingCartRepository().GetCartDetailbyIID(ShoppingCartID);

                var ShoppingCartVoucherMap = new ShoppingCartRepository().GetVoucherMap(ShoppingCartID);
                if (ShoppingCartVoucherMap != null)
                {
                    VoucherID = Convert.ToInt64(ShoppingCartVoucherMap.VoucherID);
                    var Voucher = new ShoppingCartRepository().GetVoucher(VoucherID);


                    return new VoucherMasterDTO
                    {
                        VoucherID = Voucher.VoucherIID,
                        CustomerID = Voucher.CustomerID,
                        VoucherNo = Voucher.VoucherNo,
                        VoucherTypeID = (Eduegate.Services.Contracts.Enums.VoucherTypes)(Voucher.VoucherTypeID)
                    };
                }
            }

            return null;

        }

        public bool UpdateAccountTransactionProcessStatus(TransactionHeadDTO dto)
        {
            var TransactionHeadDBModel = new TransactionHead { HeadIID = dto.HeadIID, TransactionStatusID = dto.TransactionStatusID };
            return new AccountTransactionRepository().UpdateAccountTransactionProcessStatus(TransactionHeadDBModel);
        }

        public List<InvetoryTransactionDTO> GetInvetoryTransactionsByTransactionHeadID(long TransactionHeadID)
        {
            var InvetoryTransactionList = new AccountTransactionRepository().GetInvetoryTransactionsByTransactionHeadID(TransactionHeadID);
            if (InvetoryTransactionList != null)
            {
                Mapper<InvetoryTransaction, InvetoryTransactionDTO>.CreateMap();
                return Mapper<List<InvetoryTransaction>, List<InvetoryTransactionDTO>>.Map(InvetoryTransactionList);
            }
            return null;
        }

        public List<AccountDTO> GetAutoGeneratedAccounts(long AccountID, int noOfChildAccounts, string Entity, string Entitlment)
        {
            List<AccountDTO> childAccountList = new List<AccountDTO>();
            var defaultAccount = new AccountTransactionRepository().SetAccountChildLastID(AccountID, 1);
            var rootAccount = new AccountDTO();
            //childAccount.AccountBehavoirID = baseAccount.AccountBehavoirID;
            rootAccount.AccountID = defaultAccount.AccountID;
            rootAccount.AccountName = defaultAccount.AccountName;
            rootAccount.Alias = defaultAccount.Alias;

            rootAccount.AccountBehavior = (Services.Contracts.Enums.Accounting.AccountBehavior)defaultAccount.AccountBehavoir.AccountBehavoirID;
            rootAccount.AccountGroup = new AccountGroupDTO() { AccountGroupID = defaultAccount.Group.GroupID };
            rootAccount.ParentAccount = new AccountDTO() { AccountID = defaultAccount.Account1 != null ? defaultAccount.Account1.AccountID : 0 };

            childAccountList.Add(rootAccount);
            return childAccountList;
        }

        public List<EntitlementMapDTO> GetEntitlementsByHeadId(long headId)
        {
            var dtos = new List<EntitlementMapDTO>();
            // get entityTypeEntitlements
            var entityTypeEntitlements = new AccountTransactionRepository().GetTransactionEntitlementByHeadId(headId);

            foreach (var entity in entityTypeEntitlements)
            {
                var dto = new EntitlementMapDTO
                {
                    EntitlementID = entity.EntitlementID,
                    EntitlementName = entity.EntityTypeEntitlement.EntitlementName,
                    EntitlementAmount = entity.Amount
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public AccountTransactionHeadDTO SaveAccountTransactionHead(AccountTransactionHeadDTO headDto)
        {
            long? oldDocumentStatus = (int?)null;

            var monthlyClosingDate = new MonthlyClosingRepository().GetMonthlyClosingDate();
            if (monthlyClosingDate != null && monthlyClosingDate.Value.Year > 1900 && headDto.TransactionDate.Value.Date >= monthlyClosingDate.Value.Date)
            {
                headDto.IsError = true;
                headDto.ErrorCode = ErrorCodes.Transaction.T007;
                headDto.ErrorCode = ErrorCodes.Transaction.T007;
                headDto.IsTransactionCompleted = false;
                return headDto;
            }

            if (headDto.AccountID == null && headDto.PaymentModes != null)
            {
                switch (headDto.PaymentModes.Value.ToUpper())
                {
                    case "CASH":
                        var settingDetail = new SettingRepository().GetSettingDetail("GL_CASH");
                        var glAccountByCode = new AccountTransactionRepository().GetGLAccountByCode(settingDetail.SettingValue);
                        headDto.AccountID = glAccountByCode.AccountID;
                        break;
                }
            }
            if (headDto.AccountTransactionHeadIID > 0)
            {
                bool isUpdateTransactionNo = true;
                var dbDTO = new AccountTransactionRepository().GetTransaction(headDto.AccountTransactionHeadIID);
                oldDocumentStatus = dbDTO.DocumentStatusID;

                isUpdateTransactionNo = dbDTO.DocumentTypeID != headDto.DocumentTypeID ? true : false;

                //Check for document status

                // once TransactionStatus completed we should not allow any operation on any transaction..
                if (
                    dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.IntitiateReprecess ||
                    dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete ||
                    dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess ||
                    dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled)
                {
                    if (!(dbDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete
                        && headDto.DocumentStatusID == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled))
                    {
                        headDto.IsError = true;
                        headDto.ErrorCode = ErrorCodes.Transaction.T002;
                        headDto.ErrorCode = ErrorCodes.Transaction.T002;
                        headDto.IsTransactionCompleted = true;
                        return headDto;
                    }
                }
            }

            var newEntity = Mappers.Accounts.AccountTransactionHeadMapper.Mapper(_callContext).ToEntity(headDto);
            newEntity.TransactionNumber = new MutualBL(_callContext)
                .GetNextTransactionNumberByMonthYear(new TransactionNumberDTO()
                {
                    DocumentTypeID = newEntity.DocumentTypeID.Value,
                    Month = newEntity.TransactionDate.Value.Month,
                    Year = newEntity.TransactionDate.Value.Year,
                    PaymentMode = newEntity.PaymentModeID.ToString()
                });

            var entity = new AccountTransactionRepository().SaveAccountTransactionHead(newEntity);

            if (entity != null)
            {
                new AccountTransactionRepository().AccountTransactionSync(entity.AccountTransactionHeadIID, 0, int.Parse(_callContext.LoginID.Value.ToString()), 3);
                foreach (var accountDetail in headDto.AccountTransactionDetails)
                {
                    if (accountDetail.ReceivableID != null && accountDetail.ReceivableID != 0)
                    {
                        var receivablesID = accountDetail.ReceivableID;
                        bool blnreturn = new AccountTransactionRepository().UpdateReceivablesPaidAmount(receivablesID, Convert.ToDecimal(accountDetail.Amount));
                    }

                    if (accountDetail.PayableID != null && accountDetail.PayableID != 0)
                    {
                        var payableID = accountDetail.PayableID;
                        bool blnreturnPayable = new AccountTransactionRepository().UpdatePayablesPaidAmount(payableID, Convert.ToDecimal(accountDetail.Amount));
                    }
                }

                //if debit note against product
                if (headDto.DocumentTypeID == (int)Eduegate.Framework.Enums.DocumentReferenceTypes.DebitNoteProduct)
                {
                    //foreach (AccountTransactionDetailsDTO accountDetail in headDto.AccountTransactionDetails)
                    //{
                    //    bool blnreturn = new AccountTransactionRepository().UpdateProductAvgCost((long)accountDetail.ProductSKUId, (decimal)accountDetail.NewAvgCost);
                    //}
                }

                return GetAccountTransactionHeadById(entity.AccountTransactionHeadIID);
            }

            return headDto;
        }

        public AccountTransactionHeadDTO GetAccountTransactionHeadById(long HeadID)
        {
            var accountTransactionHeadEntity = new AccountTransactionRepository().GetAccountTransactionHeadById(HeadID);
            return Mappers.Accounts.AccountTransactionHeadMapper.Mapper(_callContext).ToDTO(accountTransactionHeadEntity);
        }

        public bool AccountTransactionSync(long accountTransactionHeadIID, long referenceID, int type)
        {
            int loginID = _callContext != null && _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : 0;
            new AccountTransactionRepository().AccountTransactionSync(accountTransactionHeadIID, referenceID, loginID, type);
            return true;
        }
        public bool AccountTransMerge(long accountTransactionHeadIID, long referenceID, DateTime transDate, int type)
        {
            int loginID = _callContext != null && _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : 0;
            new AccountTransactionRepository().AccountTransMerge( referenceID, transDate, loginID, type);
            return true;
        }
        
        public long? AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int type)
        {
            int loginID = _callContext != null && _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : 0;
            return new AccountTransactionRepository().AutoReceiptAccountTransactionSync(accountTransactionHeadIID, referenceID, loginID, type);

        }
        public string AdditionalExpensesTransactionsMap(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID, short documentStatus)
        {
            int loginID = _callContext != null && _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : 0;
            return new AccountTransactionRepository().AdditionalExpensesTransactionsMap(additionalExpenseData, accountTransactionHeadIID, referenceID, loginID, documentStatus);

        }
        public List<AdditionalExpensesTransactionsMapDTO> GetAdditionalExpensesTransactions(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID)
        {
            int loginID = _callContext != null && _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : 0;
            return new AccountTransactionRepository().GetAdditionalExpensesTransactions(additionalExpenseData, accountTransactionHeadIID, referenceID);

        }

      

        public List<ReceivableDTO> GetReceivablesByAccountId(long AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes docType)
        {
            List<Receivable> receivablesEntityList = null;

            if (docType == Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts)
            {
                receivablesEntityList = new AccountTransactionRepository().GetMissionReceivablesByAccountId(AccountID);
            }
            else if (docType == Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts)
            {
                receivablesEntityList = new AccountTransactionRepository().GetInventoryReceivablesByAccountId(AccountID);
            }

            List<ReceivableDTO> receivablesDTOs = null;

            if (receivablesEntityList != null && receivablesEntityList.Count() > 0)
            {
                receivablesDTOs = ToReceivableDTO(receivablesEntityList, docType);
            }

            return receivablesDTOs;
        }

        public List<ReceivableDTO> ToReceivableDTO(List<Receivable> entities, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes docType)
        {
            var dtoList = new List<ReceivableDTO>();

            foreach (var entity in entities)
            {
                var dto = new ReceivableDTO();

                if (docType == Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts)
                {
                    if (entity.JobsEntryHeadReceivableMaps != null && entity.JobsEntryHeadReceivableMaps.Count != 0)
                    {
                        #region JobsEntryHeadReceivableMaps
                        JobsEntryHeadReceivableMap MissionJobsEntryHeadReceivableMap = entity.JobsEntryHeadReceivableMaps.FirstOrDefault();
                        if (MissionJobsEntryHeadReceivableMap != null) //Mission Job
                        {
                            JobEntryHead MissionJobEntryHead = MissionJobsEntryHeadReceivableMap.JobEntryHead;
                            JobEntryDetail MissionJobEntryDeatil = MissionJobsEntryHeadReceivableMap.JobEntryDetail;
                            if (MissionJobEntryDeatil != null)
                            {
                                JobEntryHead WareHousePackJobEntryHead = MissionJobEntryDeatil.JobEntryHead1;
                                if (WareHousePackJobEntryHead != null)
                                {
                                    //Head Details
                                    TransactionHead InventoryTransactionHead = WareHousePackJobEntryHead.TransactionHead;
                                    if (InventoryTransactionHead != null)
                                    {
                                        dto.InvoiceNumber = InventoryTransactionHead.TransactionNo;
                                        dto.CurrencyName = InventoryTransactionHead.Currency.Name;
                                        dto.CurrencyID = InventoryTransactionHead.Currency.CurrencyID;
                                        dto.ExchangeRate = InventoryTransactionHead.ExchangeRate;
                                        dto.DueDate = InventoryTransactionHead.DueDate;
                                        dto.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = InventoryTransactionHead.Currency.CurrencyID.ToString(), Value = InventoryTransactionHead.Currency.Name };

                                        //Sales retun
                                        if (InventoryTransactionHead.TransactionHead1 != null)
                                        {
                                            List<TransactionHead> SalesReturnTransactionHeadList = InventoryTransactionHead.TransactionHead1.ToList();
                                            if (SalesReturnTransactionHeadList != null)
                                            {
                                                //?? Exchange rate
                                                dto.ReturnAmount = SalesReturnTransactionHeadList
                                                    .Where(doctype => doctype.DocumentType.DocumentReferenceType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesReturn)
                                                    .Sum(x => x.TransactionDetails.Sum(sm => sm.UnitPrice * sm.Quantity));

                                            }

                                        }
                                    }
                                    //Invoice Amount
                                    //???????????????????? Check whether need to convert the amount using Exchange Rate
                                    decimal TotalPrice = 0;
                                    foreach (JobEntryDetail WareHousePackJobEntryDetail in WareHousePackJobEntryHead.JobEntryDetails)
                                    {
                                        TotalPrice = TotalPrice + Convert.ToDecimal(WareHousePackJobEntryDetail.Quantity * WareHousePackJobEntryDetail.UnitPrice == null ? 1 : WareHousePackJobEntryDetail.UnitPrice);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);                                       
                                    }
                                    dto.InvoiceAmount = TotalPrice;
                                }
                            }
                        }
                        #endregion JobsEntryHeadReceivableMaps
                    }
                }
                else if (docType == Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts)
                {
                    if (entity.TransactionHeadReceivablesMaps != null)
                    {
                        #region TransactionHeadReceivablesMaps
                        TransactionHeadReceivablesMap transactionHeadReceivablesMap = entity.TransactionHeadReceivablesMaps.FirstOrDefault();
                        //TransactionHead Details
                        TransactionHead transactionHead = null;
                        if (transactionHeadReceivablesMap != null)
                        {
                            transactionHead = transactionHeadReceivablesMap.TransactionHead;
                        }
                        if (transactionHead != null)
                        {
                            dto.InvoiceNumber = transactionHead.TransactionNo;
                            dto.CurrencyName = transactionHead.Currency.Name;
                            dto.CurrencyID = transactionHead.Currency.CurrencyID;
                            dto.ExchangeRate = transactionHead.ExchangeRate;
                            dto.DueDate = transactionHead.DueDate;
                            dto.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = transactionHead.Currency.CurrencyID.ToString(), Value = transactionHead.Currency.Name };


                            //Sales Return
                            if (transactionHead.TransactionHead1 != null)
                            {
                                List<TransactionHead> SalesReturnTransactionHeadList = transactionHead.TransactionHead1.ToList();
                                if (SalesReturnTransactionHeadList != null)
                                {

                                    dto.ReturnAmount = SalesReturnTransactionHeadList
                                        .Where(doctype => doctype.DocumentType.DocumentReferenceType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesReturn)
                                        .Sum(x => x.TransactionDetails.Sum(sm => sm.UnitPrice * sm.Quantity));
                                }
                                //Invoice Amount
                                decimal TotalPrice = 0;
                                foreach (TransactionDetail transactionDetails in transactionHead.TransactionDetails)
                                {
                                    TotalPrice = TotalPrice + Convert.ToDecimal(transactionDetails.Quantity * transactionDetails.UnitPrice);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);                                       
                                }
                                dto.InvoiceAmount = TotalPrice;
                            }
                        }

                        #endregion
                    }
                }
                dto.ReturnAmount = dto.ReturnAmount == null ? 0 : dto.ReturnAmount;
                if ((entity.Amount - dto.ReturnAmount - entity.PaidAmount) > 0)// Only if Pending amount is there
                {
                    if (entity.Account != null)
                    {
                        dto.Account = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.AccountID.ToString(), Value = entity.Account.AccountName };
                    }
                    if (entity.TransactionStatus != null)
                    {
                        dto.TransactionStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.TransactionStatusID.ToString(), Value = entity.TransactionStatus.Description };
                    }
                    if (entity.DocumentStatus != null)
                    {
                        dto.DocumentStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName };
                    }
                    //if (entity.Currency != null)
                    //{
                    //    dto.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.CurrencyID.ToString(), Value = entity.Currency.Name };
                    //}

                    dto.ReceivableIID = entity.ReceivableIID;
                    dto.TransactionDate = entity.TransactionDate;
                    //dto.DueDate = entity.DueDate;
                    dto.SerialNumber = entity.SerialNumber;
                    dto.Description = entity.Description;
                    dto.ReferenceReceivablesID = entity.ReferenceReceivablesID;
                    dto.Amount = entity.Amount;
                    dto.PaidAmount = entity.PaidAmount;
                    dto.AccountPostingDate = entity.AccountPostingDate;
                    dto.DiscountAmount = entity.DiscountAmount;
                    dto.TransactionNumber = entity.TransactionNumber;
                    //dto.JobMissionNumber = entity.JobMissionNumber;
                    //dto.ExchangeRate = entity.ExchangeRate;

                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }

        public long AddMissionJobReceivable(JobEntryHead jobEntryHead)
        {
            var MissionJobEntryHead = new AccountTransactionRepository().GetMissionJobEntryHead(jobEntryHead.JobEntryHeadIID);
            var receivableEntityList = new List<Receivable>();
            if (MissionJobEntryHead != null)
            {
                long driverGlAccountId = 2;
                var driverAccount = new AccountTransactionRepository().GetEmployeeAccount(Convert.ToInt64(MissionJobEntryHead.EmployeeID));

                if (driverAccount != null)
                {
                    driverGlAccountId = driverAccount.AccountID.Value;
                }

                foreach (JobEntryDetail missionJobEntryDetail in MissionJobEntryHead.JobEntryDetails)
                {
                    //???????????????????? Check whether need to convert the amount using Exchange Rate
                    decimal totalPrice = 0;
                    Receivable receivableEntity = new Receivable();
                    receivableEntity.TransactionDate = MissionJobEntryHead.CreatedDate;
                    receivableEntity.DocumentStatusID = 1;
                    receivableEntity.AccountID = driverGlAccountId;

                    receivableEntity.PaidAmount = 0;
                    var WareHousePackJobEntryHead = missionJobEntryDetail.JobEntryHead1;

                    if (WareHousePackJobEntryHead != null)
                    {
                        var InventoryTransactionHead = WareHousePackJobEntryHead.TransactionHead;
                        if (InventoryTransactionHead != null)
                        {
                            foreach (var transactionDetail in InventoryTransactionHead.TransactionDetails)
                            {
                                totalPrice = totalPrice + Convert.ToDecimal(transactionDetail.Quantity * transactionDetail.UnitPrice);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);
                            }

                            receivableEntity.TransactionHeadReceivablesMaps.Add(new TransactionHeadReceivablesMap { HeadID = InventoryTransactionHead.HeadIID });
                        }
                    }

                    receivableEntity.Amount = totalPrice;
                    //receivableEntity.JobMissionNumber = MissionJobEntryHead.JobNumber;
                    receivableEntity.JobsEntryHeadReceivableMaps.Add(new JobsEntryHeadReceivableMap { JobEntryHeadID = jobEntryHead.JobEntryHeadIID, JobEntryDetailID = missionJobEntryDetail.JobEntryDetailIID });
                    receivableEntityList.Add(receivableEntity);
                }
            }

            receivableEntityList = new AccountTransactionRepository().AddReceivable(receivableEntityList);
            return 1;
        }

        public long RevertFailedMissionJobReceivable(JobEntryHead jobEntryHead)
        {
            List<Receivable> receivableEntityList = new List<Receivable>();
            JobEntryHead FailedMissionJobEntryHead = new AccountTransactionRepository().GetMissionJobEntryHead(jobEntryHead.JobEntryHeadIID);
            if (FailedMissionJobEntryHead != null)
            {
                TransactionHead InventoryTransactionHead = FailedMissionJobEntryHead.TransactionHead;
                if (InventoryTransactionHead != null)
                {
                    Receivable receivable = new AccountTransactionRepository().GetInventoryReceivable_ByTransHeadId(InventoryTransactionHead.HeadIID);
                    if (receivable != null)
                    {
                        bool blnReturn = new AccountTransactionRepository().UpdateReceivablesPaidAmount(receivable.ReceivableIID, Convert.ToDecimal(receivable.Amount));
                    }
                }
            }
            return 1;
        }
        public long RevertSalesReturnInReceivable(long SalesReturn_TransactionHeadID)
        {
            //Get the Sales invoice related to the SalesReturn
            TransactionHead salesTransHead = new AccountTransactionRepository().GetParentTransactionHeadByTransHeadID(SalesReturn_TransactionHeadID);
            long Sales_TransactionHeadID = 0;
            if (salesTransHead != null)
            {
                Sales_TransactionHeadID = salesTransHead.HeadIID;
            }
            Receivable receivable = new AccountTransactionRepository().GetInventoryReceivable_ByTransHeadId(Sales_TransactionHeadID);
            if (receivable != null)
            {
                //bool blnReturn = new AccountTransactionRepository().UpdateReceivablesPaidAmount(receivable.ReceivableIID, Convert.ToDecimal(receivable.Amount));
            }
            return 1;
        }

        public bool AddReceivables(List<ReceivableDTO> dtos)
        {
            var receivableEntityList = new List<Receivable>();

            foreach (ReceivableDTO dto in dtos)
            {
                var receivableEntity = new Receivable();
                var InventoryTransactionHead = new TransactionHead();
                receivableEntity.TransactionDate = dto.TransactionDate;
                receivableEntity.DueDate = dto.DueDate;
                receivableEntity.DocumentStatusID = dto.DocumentStatusID;
                receivableEntity.AccountID = dto.AccountID;
                receivableEntity.PaidAmount = dto.PaidAmount;
                receivableEntity.Amount = dto.Amount;
                receivableEntity.DocumentTypeID = dto.DocumentTypeID;
                receivableEntity.TransactionNumber = dto.TransactionNumber;
                receivableEntity.DiscountAmount = dto.DiscountAmount;
                receivableEntity.DebitOrCredit = dto.DebitOrCredit;
                receivableEntity.TransactionHeadReceivablesMaps = new List<TransactionHeadReceivablesMap>() { new TransactionHeadReceivablesMap { HeadID = dto.TransactionHeadReceivablesMaps.FirstOrDefault().HeadID } };
                receivableEntityList.Add(receivableEntity);
            }

            receivableEntityList = new AccountTransactionRepository().AddReceivable(receivableEntityList);
            return true;
        }

        public List<JobEntryHeadAccountingDTO> GetAllMissionJobEntryHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            var jobEntryHeadEntity = new List<JobEntryHead>();
            jobEntryHeadEntity = new AccountTransactionRepository().GetAllMissionJobEntryHeads(referenceTypes, transactionStatus);

            var _JobEntryHeadAccountingDTOs = new List<JobEntryHeadAccountingDTO>();
            // convert TransactionDetail into TransactionDetailDTO
            foreach (var jobEntryHead in jobEntryHeadEntity)
            {
                var jobEntryHeadAccountingDTO = new JobEntryHeadAccountingDTO();
                long DriverGlAccountId = 2;
                var DriverAccount = new AccountTransactionRepository().GetEmployeeAccount(Convert.ToInt64(jobEntryHead.EmployeeID));

                if (DriverAccount != null)
                {
                    DriverGlAccountId = DriverAccount.AccountID.Value;
                }

                jobEntryHeadAccountingDTO.EmployeeID = jobEntryHead.EmployeeID;
                jobEntryHeadAccountingDTO.AccountID = DriverGlAccountId;
                jobEntryHeadAccountingDTO.JobEntryHeadIID = jobEntryHead.JobEntryHeadIID;

                var transactionHeadDTO = new TransactionHeadDTO();
                var itemTransactionHead = jobEntryHead.TransactionHead;
                transactionHeadDTO.HeadIID = itemTransactionHead.HeadIID;
                transactionHeadDTO.DocumentTypeID = itemTransactionHead.DocumentTypeID;
                transactionHeadDTO.TransactionNo = itemTransactionHead.TransactionNo;
                transactionHeadDTO.Description = itemTransactionHead.Description;
                transactionHeadDTO.Reference = itemTransactionHead.Reference;
                transactionHeadDTO.CustomerID = itemTransactionHead.CustomerID;
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

                transactionHeadDTO.TransactionDetails = new List<TransactionDetailDTO>();

                foreach (var item in itemTransactionHead.TransactionDetails)
                {
                    var transactionDetailDTO = new TransactionDetailDTO();

                    // Get the eldest batch for item
                    var productinventories = new TransactionBL(_callContext).GetProductInventories(Convert.ToInt64(item.ProductSKUMapID), Convert.ToInt64(itemTransactionHead.BranchID));
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
                jobEntryHeadAccountingDTO.TransactionHeadDTO = transactionHeadDTO;
                // add in the TransactionHeadDTO list
                _JobEntryHeadAccountingDTOs.Add(jobEntryHeadAccountingDTO);
            }

            return _JobEntryHeadAccountingDTOs;
        }

        public bool AddPayables(List<PayableDTO> dtos)
        {
            var payableEntityList = new List<Payable>();

            foreach (PayableDTO dto in dtos)
            {
                var payableEntity = new Payable();
                var InventoryTransactionHead = new TransactionHead();
                payableEntity.TransactionDate = dto.TransactionDate;
                payableEntity.DueDate = dto.DueDate;
                payableEntity.DocumentStatusID = dto.DocumentStatusID;
                payableEntity.AccountID = dto.AccountID;
                payableEntity.PaidAmount = dto.PaidAmount;
                payableEntity.Amount = dto.Amount;
                payableEntity.DebitOrCredit = dto.DebitOrCredit;
                payableEntity.DiscountAmount = dto.DiscountAmount;
                payableEntity.Description = dto.Description;
                payableEntity.TransactionNumber = dto.TransactionNumber;
                payableEntity.TransactionHeadPayablesMaps = new List<TransactionHeadPayablesMap>() { new TransactionHeadPayablesMap { HeadID = dto.TransactionHeadPayablesMaps.FirstOrDefault().HeadID } };
                payableEntityList.Add(payableEntity);
            }

            payableEntityList = new AccountTransactionRepository().AddPayables(payableEntityList);
            return true;
        }


        public List<PayableDTO> GetPayablesByAccountId(long AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes docType)
        {
            List<Payable> payablesEntityList = null;

            if (docType == Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments)
            {
                payablesEntityList = new AccountTransactionRepository().GetInventoryPayablesByAccountId(AccountID);
            }

            List<PayableDTO> payableDTOs = null;

            if (payablesEntityList != null && payablesEntityList.Count() > 0)
            {
                payableDTOs = ToPayableDTO(payablesEntityList, docType);
            }

            return payableDTOs;
        }




        public List<PayableDTO> ToPayableDTO(List<Payable> entities, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes docType)
        {
            var dtoList = new List<PayableDTO>();

            foreach (var entity in entities)
            {
                var dto = new PayableDTO();

                dto.DebitOrCredit = entity.DebitOrCredit;

                if (docType == Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments)
                {
                    if (entity.TransactionHeadPayablesMaps != null)
                    {
                        #region TransactionHeadPayableMaps
                        TransactionHeadPayablesMap transactionHeadPayableMap = entity.TransactionHeadPayablesMaps.FirstOrDefault();
                        //TransactionHead Details
                        TransactionHead transactionHead = null;
                        if (transactionHeadPayableMap != null)
                        {
                            transactionHead = transactionHeadPayableMap.TransactionHead;
                        }
                        if (transactionHead != null)
                        {
                            dto.InvoiceNumber = transactionHead.TransactionNo;
                            dto.CurrencyName = transactionHead.Currency.Name;
                            dto.CurrencyID = transactionHead.Currency.CurrencyID;
                            dto.ExchangeRate = transactionHead.ExchangeRate;
                            dto.DueDate = transactionHead.DueDate;
                            dto.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = transactionHead.Currency.CurrencyID.ToString(), Value = transactionHead.Currency.Name };

                            //Sales Return
                            if (transactionHead.TransactionHead1 != null)
                            {
                                List<TransactionHead> SalesReturnTransactionHeadList = transactionHead.TransactionHead1.ToList();
                                if (SalesReturnTransactionHeadList != null)
                                {

                                    dto.ReturnAmount = SalesReturnTransactionHeadList
                                        .Where(doctype => doctype.DocumentType.DocumentReferenceType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseReturn)
                                        .Sum(x => x.TransactionDetails.Sum(sm => sm.UnitPrice * sm.Quantity));
                                }
                                //Invoice Amount
                                decimal TotalPrice = 0;
                                foreach (TransactionDetail transactionDetails in transactionHead.TransactionDetails)
                                {
                                    TotalPrice = TotalPrice + Convert.ToDecimal(transactionDetails.Quantity * transactionDetails.UnitPrice);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);                                       
                                }
                                dto.InvoiceAmount = TotalPrice;
                            }
                        }

                        #endregion
                    }
                }
                dto.ReturnAmount = dto.ReturnAmount == null ? 0 : dto.ReturnAmount;
                if ((entity.Amount - dto.ReturnAmount - entity.PaidAmount) > 0)// Only if Pending amount is there
                {
                    if (entity.Account != null)
                    {
                        dto.Account = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.AccountID.ToString(), Value = entity.Account.AccountName };
                    }
                    if (entity.TransactionStatus != null)
                    {
                        dto.TransactionStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.TransactionStatusID.ToString(), Value = entity.TransactionStatus.Description };
                    }
                    if (entity.DocumentStatus != null)
                    {
                        dto.DocumentStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName };
                    }
                    //if (entity.Currency != null)
                    //{
                    //    dto.Currency = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.CurrencyID.ToString(), Value = entity.Currency.Name };
                    //}

                    dto.PayableIID = entity.PayableIID;
                    dto.TransactionDate = entity.TransactionDate;
                    //dto.DueDate = entity.DueDate;
                    dto.SerialNumber = entity.SerialNumber;
                    dto.Description = entity.Description;
                    dto.ReferencePayablesID = entity.ReferencePayablesID;
                    dto.Amount = entity.Amount;
                    dto.PaidAmount = entity.PaidAmount;
                    dto.AccountPostingDate = entity.AccountPostingDate;
                    dto.DiscountAmount = entity.DiscountAmount;
                    //dto.ExchangeRate = entity.ExchangeRate;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public List<AccountTransactionHeadDTO> GetAccountTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            var accountTransactionHeadEntity = new AccountTransactionRepository().GetAccountTransactionHeads(referenceTypes, transactionStatus);
            var DTOList = new List<AccountTransactionHeadDTO>();
            var mapper = Mappers.Accounts.AccountTransactionHeadMapper.Mapper(_callContext);

            foreach (AccountTransactionHead head in accountTransactionHeadEntity)
            {
                DTOList.Add(mapper.ToDTO(head));
            }

            return DTOList;
        }

        public List<ProductSKUCostDTO> GetProductSKUMapByID(long ProductSKUMapIID)
        {
            List<ProductSKUMap> ProductSKUMapList = null;
            ProductSKUMapList = new AccountTransactionRepository().GetProductSKUMapByID(ProductSKUMapIID);

            List<ProductSKUCostDTO> SKUDTOs = null;

            if (ProductSKUMapList != null && ProductSKUMapList.Count() > 0)
            {
                SKUDTOs = ToProductSKUDTO(ProductSKUMapList);
            }

            return SKUDTOs;
        }


        public List<ProductSKUCostDTO> ToProductSKUDTO(List<ProductSKUMap> entities)
        {
            var dtoList = new List<ProductSKUCostDTO>();

            foreach (var entity in entities)
            {
                var dto = new ProductSKUCostDTO();

                dto.AvailableQuantity = 100;
                dto.CurrentAvgCost = 50;
                dto.ProductSKUCode = entity.ProductSKUCode;
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public List<KeyValueDTO> GetVendorCustomerAccounts(string searchText)
        {
            List<KeyValueDTO> dtos = new List<KeyValueDTO>();
            var customerAccountMapList = new AccountTransactionRepository().GetCustomersAccount(searchText);

            if (customerAccountMapList != null)
            {
                foreach (var map in customerAccountMapList)
                {
                    dtos.Add(new KeyValueDTO
                    {
                        Key = Convert.ToString(map.AccountID),
                        Value = map.AccountName + map.AccountCode
                    });
                }
            }

            var SuppliersAccountList = new AccountTransactionRepository().GetSuppliersAccount(searchText);

            if (SuppliersAccountList != null)
            {
                foreach (var map in SuppliersAccountList)
                {
                    if (map.Account != null)
                    {
                        dtos.Add(new KeyValueDTO
                        {
                            Key = Convert.ToString(map.AccountID),
                            Value = map.Account.AccountName
                        });
                    }
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> Get_AllSuppliers_Accounts(string searchText)
        {
            var dtos = new List<KeyValueDTO>();

            var SuppliersAccountList = new AccountTransactionRepository().GetSuppliersAccount(searchText);

            if (SuppliersAccountList != null)
            {
                foreach (var map in SuppliersAccountList)
                {
                    if (map.Account != null)
                    {
                        dtos.Add(new KeyValueDTO
                        {
                            Key = Convert.ToString(map.AccountID),
                            Value = map.Account.AccountName
                        });
                    }
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> Get_AllCustomers_Accounts(string searchText)
        {
            var dtos = new List<KeyValueDTO>();
            var customerAccountMapList = new AccountTransactionRepository().GetCustomersAccount(searchText);

            if (customerAccountMapList != null)
            {
                foreach (var map in customerAccountMapList)
                {
                    dtos.Add(new KeyValueDTO
                    {
                        Key = Convert.ToString(map.AccountID),
                        Value = map.AccountName + "-" + (string.IsNullOrEmpty(map.AccountCode) ? string.Empty : map.AccountCode)
                    });
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> Get_AllDrivers_Accounts(string searchText)
        {
            var dtos = new List<KeyValueDTO>();
            var employeeAccountMapList = new AccountTransactionRepository().Get_AllDrivers_Accounts(searchText);

            if (employeeAccountMapList != null)
            {
                foreach (var map in employeeAccountMapList)
                {
                    if (map.Account != null)
                    {
                        dtos.Add(new KeyValueDTO
                        {
                            Key = Convert.ToString(map.Account.AccountID),
                            Value = map.Account.AccountName
                        });
                    }
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> GetChildAccounts_ByParentAccountId(string searchText, long ParentAccountId)
        {
            var dtos = new List<KeyValueDTO>();
            var accounts = new AccountTransactionRepository().GetChildAccounts_ByParentAccountId(ParentAccountId);
            accounts = accounts.Where(x => (x.AccountName.Contains(searchText) || searchText == null || searchText == "")).ToList();

            if (accounts != null)
            {
                foreach (var acc in accounts)
                {
                    dtos.Add(new KeyValueDTO
                    {
                        Key = Convert.ToString(acc.AccountID),
                        Value = acc.AccountName
                    });
                }
            }

            return dtos;
        }

        public List<KeyValueDTO> GetGLAccounts(string searchText)
        {
            /* This function will return all the Accounts except Supplier, Customer, Driver account*/
            var dtos = new List<KeyValueDTO>();
            var accountsList = new AccountTransactionRepository().GetGLAccounts(searchText);

            if (accountsList != null)
            {
                foreach (var acnt in accountsList)
                {
                    dtos.Add(new KeyValueDTO
                    {
                        Key = Convert.ToString(acnt.AccountID),
                        Value = acnt.AccountName
                    });

                }
            }

            return dtos;
        }

        public List<KeyValueDTO> GetPaymentModes()
        {
            var dtos = new List<KeyValueDTO>();
            var paymentModesList = new AccountTransactionRepository().GetPaymentModes();

            if (paymentModesList != null)
            {
                foreach (var mode in paymentModesList)
                {
                    dtos.Add(new KeyValueDTO
                    {
                        Key = Convert.ToString(mode.PaymentModeID),
                        Value = mode.PaymentModeName
                    });

                }
            }
            return dtos;
        }

        public List<ReceivableDTO> GetCustomerPendingInvoices(long customerID)
        {
            var entity = new AccountTransactionRepository().GetCustomerPendingInvoices(customerID);
            var DTOList = new List<ReceivableDTO>();
            var mapper = Mappers.Accounts.ReceivableMapper.Mapper(_callContext);

            foreach (var head in entity)
            {
                DTOList.Add(mapper.ToDTO(head));
            }

            return DTOList;
        }

        public List<PayableDTO> GetSupplierPendingInvoices(long suplierID)
        {
            var entities = new AccountTransactionRepository().GetSupplierPendingInvoices(suplierID);
            var DTOList = new List<PayableDTO>();
            var mapper = Mappers.Accounts.PayableMapper.Mapper(_callContext);

            foreach (var head in entities)
            {
                DTOList.Add(mapper.ToDTO(head));
            }

            return DTOList;
        }

        public AccountDTO GetGLAccountByCode(string code)
        {
            var account = new AccountTransactionRepository().GetGLAccountByCode(code);
            return Mappers.Accounts.AccountMapper.Mapper(_callContext).ToDTO(account);
        }

        public List<TransactionHeadEntitlementMapDTO> GetTransactionEntitlementByHeadId(long headId)
        {
            var maps = new AccountTransactionRepository().GetTransactionEntitlementByHeadId(headId);
            return Mappers.TransactionHeadEntitlementMapMapper.Mapper(_callContext).ToDTO(maps);
        }

        public List<ReceivableDTO> GetReceivables(List<long> receivableIds)
        {
            var receivables = new AccountTransactionRepository().GetReceivables(receivableIds);
            var receivableDTO = new List<ReceivableDTO>();

            foreach (var receivable in receivables)
            {
                receivableDTO.Add(Mappers.Accounts.ReceivableMapper.Mapper(_callContext).ToDTO(receivable));
            }

            return receivableDTO;
        }

        public List<PayableDTO> GetPayables(List<long> payableIds)
        {
            var payables = new AccountTransactionRepository().GetPayables(payableIds);
            var payableDTO = new List<PayableDTO>();

            foreach (var payable in payables)
            {
                payableDTO.Add(Mappers.Accounts.PayableMapper.Mapper(_callContext).ToDTO(payable));
            }

            return payableDTO;
        }

        public List<ReceivableDTO> SaveReceivables(List<ReceivableDTO> receivables)
        {
            var entities = new List<Receivable>();

            foreach (var receivable in receivables)
            {
                entities.Add(Mappers.Accounts.ReceivableMapper.Mapper(_callContext).ToEntity(receivable));
            }

            new AccountTransactionRepository().SaveReceivables(entities);
            return receivables;
        }

        public List<PayableDTO> SavePayables(List<PayableDTO> payables)
        {
            var entities = new List<Payable>();

            foreach (var payable in payables)
            {
                entities.Add(Mappers.Accounts.PayableMapper.Mapper(_callContext).ToEntity(payable));
            }

            new AccountTransactionRepository().SavePayables(entities);
            return payables;
        }

        public bool ClearPostedData(long accountingHeadID)
        {
            new AccountTransactionRepository().ClearPostedData(accountingHeadID);
            return true;
        }

        public ReceivableDTO GetAllocatedReceivables(long receivableId)
        {
            var receivable = new AccountTransactionRepository().GetReceivables(new List<long>() { receivableId });
            var mapper = Mappers.Accounts.ReceivableMapper.Mapper(_callContext);
            var dto = mapper.ToDTO(receivable.FirstOrDefault());
            var allocatedRec = new AccountTransactionRepository().GetAllocatedReceivable(receivableId);

            foreach (var rec in allocatedRec)
            {
                dto.ReferenceReceivables.Add(mapper.ToDTO(rec));
            }

            return dto;
        }
    }
}
