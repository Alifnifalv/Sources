using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class AccountingBase
    {
        public const string GL_COD_RECEIVABLES_CODE = "GL_COD_RECEIVABLES";
        public const string GL_VISA_MASTERCARD_CODE = "GL_VISA_MASTERCARD";
        public const string GL_PAYPAL_CODE = "GL_PAYPAL";
        public const string GL_ORDER_IN_PROCESS_CODE = "GL_ORDER_IN_PROCESS";
        public const string GL_REFUND_VOUCHER_CODE = "GL_REFUND_VOUCHER";
        public const string GL_REFUND_IN_PROCESS_CODE = "GL_REFUND_IN_PROCESS";
        public const string GL_COGS_CODE = "GL_COGS";
        public const string GL_INVENTORY_CODE = "GL_INVENTORY";
        public const string GL_CASH_CODE = "GL_CASH";
        public const string GL_CUSTOMER_ACCOUNT_CODE = "GL_CUSTOMER_ACCOUNT";
        public const string GL_REFUND_VOUCHER_INCOME_CODE = "GL_REFUND_VOUCHER_INCOME";
        public const string GL_PURCHASE_CODE = "GL_PURCHASE";
        public const string GL_PURCHASE_ADDITIONAL_CODE = "GL_PURCHASE_ADDITIONAL";
        public const string GL_SUSPENDED_CODE = "GL_SUSPENDED";
        public const string GL_SALES_DISCOUNT_CODE = "GL_SALES_DISCOUNT";
        public const string GL_DRIVER_CASH_COLLECTION_CODE = "GL_DRIVER_CASH_COLLECTION";
        public const string GL_MARKETING_VOUCHER_CODE = "GL_MARKETING_VOUCHER";
        public const string GL_VOUCHER_EXPENSE_CODE = "GL_VOUCHER_EXPENSE";
        public const string GL_PROFIT_LOSS_CODE = "GL_PROFIT_LOSS";
        public const string GL_PURCHASERETURN_CODE = "GL_PURCHASE_RETURN";
        public const string GL_SALES_CODE = "GL_SALES";
        public const string GL_VATACC_CODE = "GL_VAT";
        public const string GL_SALES_WITH_VATACC_CODE = "GL_SALESWITHVAT";

        public const bool CONST_DEBIT = true;
        public const bool CONST_CREDIT = false;

        public const string ENTITLEMENT_MARKETPLACE = "Market Place";
        public const string ENTITLEMENT_CREDIT = "Credit";
        public const string ENTITLEMENT_CONSIGNMENT = "Consignment";
        public const string ENTITLEMENT_CASH = "Cash";
        public const string ENTITLEMENT_DIGITALCARDS = "Digital cards";
        public const string ENTITLEMENT_KNET = "Knet";
        public const string ENTITLEMENT_VISAMASTERCARD = "Visa-Mastercard";
        public const string ENTITLEMENT_PAYPAL = "Paypal";
        public const string ENTITLEMENT_WALLET = "Wallet";
        public const string ENTITLEMENT_COD = "COD";
        public const string ENTITLEMENT_PDC = "PDC";
        public const string ENTITLEMENT_VOUCHER = "Voucher";
        public const string ENTITLEMENT_VOUCHER_EXPIRES = "VoucherExpires";
        public const string ENTITLEMENT_CREDITCARD = "Credit Card";
        public const string SETTING_CREDITCARDID = "CREDITCARD_ID";
        public const string SETTING_DOCUMENT_ONLINESTORE = "DOCUMENTTYPE_ONLINESTORE";

        public void MergeAccountTransactions(List<ViewModels.AccountTransactionViewModel> vm, ViewModels.TransactionHeadViewModel transaction
                            , long? AccountID, decimal? Amount,
                            bool DebitOrCredit, string Description,
                            decimal? inclusiveTaxAmount = null, decimal? exclusiveTaxAmount = null, decimal? discountAmount = null, bool allZeroAmount = false)
        {
            if (!AccountID.HasValue || AccountID.Value == 0) return;

            if (allZeroAmount)
            {
                if (!Amount.HasValue || Amount.Value == 0) return;
            }

            var accountTransactionViewModel = new ViewModels.AccountTransactionViewModel
            {
                DocumentTypeID = transaction.DocumentTypeID,
                TransactionDate = transaction.TransactionDate,
                TransactionNumber = transaction.TransactionNo,
                AccountID = AccountID,
                DebitOrCredit = DebitOrCredit,
                Amount = Amount,
                TransactionHeadID = transaction.HeadIID,
                Description = Description,
                TransactionType = 1,
                TransactionHeadAccountMap = new TransactionHeadAccountMapViewModel
                {
                    TransactionHeadID = transaction.HeadIID,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                InclusiveTaxAmount = inclusiveTaxAmount,
                //DiscountAmount = discountAmount,
                DiscountAmount = transaction.DiscountAmount,
            };

            if (AccountID.HasValue)
            {
                vm.Add(accountTransactionViewModel);
            }
        }

        public AccountTransactionViewModel Create_Asset_TransactionHeadViewModel(long? AccountID, decimal? Amount,
                                                                                bool DebitOrCredit, long TransactionHeadID, string Description)
        {
            var assetTransactionHeadViewModel = new ViewModels.AccountTransactionViewModel
            {
                AccountID = AccountID,
                DebitOrCredit = DebitOrCredit,
                Amount = Amount,
                TransactionHeadID = TransactionHeadID,
                Description = Description,
                TransactionType = 2
            };

            return assetTransactionHeadViewModel;
        }

        public AccountTransactionViewModel Create_Accounts_TransactionHeadViewModel(long? AccountID, decimal? Amount,
                                                                                    bool DebitOrCredit, long TransactionHeadID, string Description, bool allowZero = false)
        {

            if (!allowZero)
            {
                if (!Amount.HasValue || Amount.Value == 0) return null;
            }

            var accountsTransactionHeadViewModel = new ViewModels.AccountTransactionViewModel
            {
                AccountID = AccountID,
                DebitOrCredit = DebitOrCredit,
                Amount = Amount,
                TransactionHeadID = TransactionHeadID,
                Description = Description,
                TransactionType = 3
            };

            return accountsTransactionHeadViewModel;
        }

        public AccountTransactionViewModel Create_MissionJob_TransactionHeadViewModel(long? AccountID, decimal? Amount,
                                                                                      bool DebitOrCredit, long TransactionHeadID, string Description)
        {
            var accountsTransactionHeadViewModel = new ViewModels.AccountTransactionViewModel
            {
                AccountID = AccountID,
                DebitOrCredit = DebitOrCredit,
                Amount = Amount,
                TransactionHeadID = TransactionHeadID,
                Description = Description,
                TransactionType = 4
            };

            return accountsTransactionHeadViewModel;
        }

        public decimal GetCostCenterPrice(TransactionHeadViewModel transaction)
        {
            var transactionHead = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetInvetoryTransactionsByTransactionHeadID(transaction.HeadIID);
            return Convert.ToDecimal(transactionHead.Sum(a => a.Cost));
        }

        public List<TransactionHeadEntitlementMapDTO> GetTransactionHeadEntitlementMaps(long transactionHeadIID)
        {
            var transactionHeadEntitlementMapList = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetTransactionEntitlementByHeadId(transactionHeadIID);
            return transactionHeadEntitlementMapList;
        }
        public List<AdditionalExpensesTransactionsMapDTO> GetAdditionalExpensesTransactionsMap(long transactionHeadIID)
        {
            var additionalExpensesTransactionsMapDTOList = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAdditionalExpensesTransactions(null, 0, transactionHeadIID);
            return additionalExpensesTransactionsMapDTOList;
        }

        public string GetSettingByCode(string SettingCode)
        {
            string glAccountId = null;
            var settingDTO = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetSettingDetail(SettingCode);
            if (settingDTO != null)
            {
                glAccountId = settingDTO.SettingValue;
            }
            return glAccountId;
        }

        public long? GetGLAccountId(string accountingCode)
        {
            long? accountID = null;
            var account = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetGLAccountByCode(accountingCode);

            if (account != null)
            {
                accountID = Convert.ToInt64(account.AccountID);
            }

            return accountID;
        }

        public long? GetCustomerGLAccountId(long CustomerID, int? EntitlementID)
        {
            long? customerGLAccountId = null;
            var customerAccountMap = new ClientFactory.ClientFactory().GetCustomerAccountMap(CustomerID, EntitlementID.Value);
            if (customerAccountMap != null && customerAccountMap.AccountID != null)
            {
                customerGLAccountId = Convert.ToInt64(customerAccountMap.AccountID);
            }
            return customerGLAccountId;
        }

        public long? GetSupplierGLAccountId(long SupplierID, int EntitlementID)
        {
            long? supplierGLAccountId = null;
            var supplierAccountMapsEntiy = new ClientFactory.ClientFactory().GetSupplierAccountMap(SupplierID, EntitlementID);
            if (supplierAccountMapsEntiy != null && supplierAccountMapsEntiy.AccountID != 0)
            {
                supplierGLAccountId = Convert.ToInt64(supplierAccountMapsEntiy.AccountID);
            }
            return supplierGLAccountId;
        }

        public Eduegate.Services.Contracts.Enums.VoucherTypes GetVoucherTypeId(long TransactionHeadID)
        {
            var shoppingCart = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetCartDetailByHeadID(TransactionHeadID);

            if (shoppingCart != null)
            {
                var cartDetails = new ClientFactory.ClientFactory().GetCartDetailbyIID(long.Parse(shoppingCart));

                if (cartDetails != null)
                {
                    var cartVoucherMap = new ClientFactory.ClientFactory().GetCartDetailbyIID(long.Parse(shoppingCart));

                    if (cartVoucherMap != null && cartVoucherMap.VoucherID.HasValue)
                    {
                        var voucher = new ClientFactory.ClientFactory().GetVoucher(cartVoucherMap.VoucherID.Value);

                        if (voucher != null)
                        {
                            return (Eduegate.Services.Contracts.Enums.VoucherTypes)voucher.VoucherTypeID;
                        }
                    }
                }
            }

            return Eduegate.Services.Contracts.Enums.VoucherTypes.Free;
        }

        public decimal GetDiscountAmount(TransactionHeadViewModel transaction)
        {
            return transaction.DiscountAmount != null ? Convert.ToDecimal(transaction.DiscountAmount) : 0;
        }

        public bool UpdateAccountTransactionProcessStatus(long TransactionHeadID, Eduegate.Framework.Enums.TransactionStatus statusID)
        {
            var transactionHeadDTO = new Services.Contracts.Catalog.TransactionHeadDTO { HeadIID = TransactionHeadID, TransactionStatusID = (byte)statusID };
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().UpdateAccountTransactionProcessStatus(transactionHeadDTO);
        }

        public bool AccountTransactionSync(long accountTransactionHeadIID, long referenceID, int type)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AccountTransactionSync(accountTransactionHeadIID, referenceID, type);
        }
        public bool AccountTransMerge(long accountTransactionHeadIID, long referenceID, DateTime transDate, int type)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AccountTransMerge(accountTransactionHeadIID, referenceID, transDate, type);
        }
        public long? AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int type)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AutoReceiptAccountTransactionSync(accountTransactionHeadIID, referenceID, type);
        }
        public string AdditionalExpensesTransactionsMap(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID, short documentStatus)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AdditionalExpensesTransactionsMap(additionalExpenseData, accountTransactionHeadIID, referenceID, documentStatus);
        }
        public List<AdditionalExpensesTransactionsMapDTO> GetAdditionalExpensesTransactions(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAdditionalExpensesTransactions(additionalExpenseData, accountTransactionHeadIID, referenceID);
        }
        public decimal GetPurchaseSuspendedAmount(TransactionHeadViewModel transaction)
        {
            decimal PurchaseSuspendedAmount = 0;
            return PurchaseSuspendedAmount;
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO assetTransactionHead, Eduegate.Framework.Enums.TransactionStatus transactionStatus, DocumentStatuses documentStatus)
        {
            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().UpdateAssetTransactionHead(
                new AssetTransactionHeadDTO()
                {
                    HeadIID = assetTransactionHead.HeadIID,
                    ProcessingStatusID = (byte)transactionStatus,
                    DocumentStatusID = (short)documentStatus
                });
        }

        public bool AddAccountTransactionReceivablesMaps(List<AccountTransactionReceivablesMapDTO> AccountTransactionReceivablesMapDTOs)
        {
            new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactionReceivablesMaps(AccountTransactionReceivablesMapDTOs);
            return true;
        }
        public bool AddReceivable(ViewModels.AccountTransactionHeadViewModel transHead, long? AccountId, decimal? totalPaidAmount, bool debitOrCredit, bool isAccount = false, bool isAddMaptable = true)
        {
            var receivableEntityList = new List<ReceivableDTO>();
            decimal totalPrice = 0;
            var receivableEntity = new ReceivableDTO();
            receivableEntity.TransactionDate = transHead.TransactionDate;
            receivableEntity.DueDate = transHead.DueDate;
            receivableEntity.DocumentStatusID = 1;
            receivableEntity.AccountID = AccountId;
            receivableEntity.DocumentTypeID = transHead.DocumentTypeID;
            receivableEntity.TransactionNumber = transHead.TransactionNumber;
            receivableEntity.DebitOrCredit = debitOrCredit;
            receivableEntity.DiscountAmount = transHead.DiscountAmount;
            var accountTransactionReceivablesMaps = new List<AccountTransactionReceivablesMapDTO>();

            foreach (var transactionDetails in transHead.AccountTransactionDetails)
            {
                totalPrice = totalPrice + Convert.ToDecimal(transactionDetails.Amount);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);
                
            }

            if (transHead.TaxDetails != null)
            {
                foreach (var tax in transHead.TaxDetails)
                {
                    if (tax.Amount.HasValue)
                    {
                        totalPrice = totalPrice + tax.Amount.Value;
                    }
                }
            }
            if (totalPaidAmount != 0 && totalPrice == 0)
                receivableEntity.Amount = totalPaidAmount;
            else
                receivableEntity.Amount = totalPrice;
            //receivableEntity.PaidAmount = Convert.ToDecimal(transHead.AmountPaid);
            receivableEntity.TransactionHeadReceivablesMaps = new List<TransactionHeadReceivablesMapDTO>();
            receivableEntity.AccountTransactionReceivablesMaps = new List<AccountTransactionReceivablesMapDTO>();            
            
            receivableEntityList.Add(receivableEntity);

            new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddReceivables(receivableEntityList);
            return true;
        }

        public bool AddInvetoryReceivable(ViewModels.TransactionHeadViewModel transHead, long? AccountId, decimal totalPaidAmount, bool debitOrCredit)
        {
            var receivableEntityList = new List<ReceivableDTO>();
            decimal totalPrice = 0;
            var receivableEntity = new ReceivableDTO();
            receivableEntity.TransactionDate = transHead.TransactionDate;
            receivableEntity.DueDate = transHead.DueDate;
            receivableEntity.DocumentStatusID = 1;
            receivableEntity.AccountID = AccountId;
            receivableEntity.DocumentTypeID = transHead.DocumentTypeID;
            receivableEntity.DebitOrCredit = debitOrCredit;
            receivableEntity.TransactionNumber = transHead.TransactionNo;
            receivableEntity.Description = transHead.Description;
            receivableEntity.DiscountAmount = transHead.DiscountAmount;

            foreach (var transactionDetails in transHead.TransactionDetails)
            {
                //totalPrice = totalPrice + Convert.ToDecimal(transactionDetails.Quantity * transactionDetails.UnitPrice);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);
                totalPrice = totalPrice + Convert.ToDecimal(transactionDetails.Amount);
            }

            if (transHead.TaxDetails != null)
            {
                foreach (var tax in transHead.TaxDetails)
                {
                    totalPrice = totalPrice + tax.Amount.Value;
                }
            }

            receivableEntity.Amount = totalPrice;
            receivableEntity.PaidAmount = totalPaidAmount;
            receivableEntity.TransactionHeadReceivablesMaps = new List<TransactionHeadReceivablesMapDTO>() { new TransactionHeadReceivablesMapDTO { HeadID = transHead.HeadIID } };
            receivableEntityList.Add(receivableEntity);

            new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddReceivables(receivableEntityList);
            return true;
        }

        public bool AddInvetoryPayable(ViewModels.TransactionHeadViewModel InventoryTransactionHead, long? AccountId, bool debitOrCredit)
        {

            var payablesEntityList = new List<PayableDTO>();

            //???????????????????? Check whether need to convert the amount using Exchange Rate
            decimal totalPrice = 0;
            var payableEntity = new PayableDTO();
            payableEntity.TransactionDate = InventoryTransactionHead.TransactionDate;
            payableEntity.DueDate = InventoryTransactionHead.DueDate;
            payableEntity.DocumentStatusID = 1;
            payableEntity.AccountID = AccountId;
            payableEntity.PaidAmount = 0;
            payableEntity.DebitOrCredit = debitOrCredit;
            payableEntity.DiscountAmount = InventoryTransactionHead.DiscountAmount;
            payableEntity.TransactionNumber = InventoryTransactionHead.TransactionNo;
            payableEntity.Description = InventoryTransactionHead.Description;

            foreach (var transactionDetails in InventoryTransactionHead.TransactionDetails)
            {
                //totalPrice = totalPrice + Convert.ToDecimal(transactionDetails.Quantity * transactionDetails.UnitPrice);// * InventoryTransactionHead.ExchangeRate ==null?1: InventoryTransactionHead.ExchangeRate);
                totalPrice = totalPrice + Convert.ToDecimal(transactionDetails.Amount);
            }

            payableEntity.Amount = totalPrice;
            payableEntity.TransactionHeadPayablesMaps = new List<TransactionHeadPayablesMapDTO>() { new TransactionHeadPayablesMapDTO { HeadID = InventoryTransactionHead.HeadIID } };
            payablesEntityList.Add(payableEntity);

            new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddPayables(payablesEntityList);
            return true;
        }

        public bool ClearPostedData(long accountingHeadID)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().ClearPostedData(accountingHeadID);
        }
    }

}
