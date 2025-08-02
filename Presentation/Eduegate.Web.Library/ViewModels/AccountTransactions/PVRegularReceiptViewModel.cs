using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class PVRegularPaymentViewModel : BaseMasterViewModel
    {
        public PVRegularPaymentViewModel()
        {
            DetailViewModel = new List<PVRegularPaymentDetailViewModel>() { new PVRegularPaymentDetailViewModel() };
            MasterViewModel = new PVRegularPaymentMasterViewModel();
        }

        public List<PVRegularPaymentDetailViewModel> DetailViewModel { get; set; }
        public PVRegularPaymentMasterViewModel MasterViewModel { get; set; }



        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(PVRegularPaymentViewModel viewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            PVRegularPaymentMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<PVRegularPaymentDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            AccountTransactionHeadDTO headDTO = new AccountTransactionHeadDTO();

            headDTO.DocumentStatusID = masterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(masterViewModel.DocumentStatus.Key) ? Convert.ToInt32(masterViewModel.DocumentStatus.Key) : (long?)null : (long?)null;
            headDTO.DocumentTypeID = masterViewModel.DocumentType != null ? !string.IsNullOrEmpty(masterViewModel.DocumentType.Key) ? Convert.ToInt32(masterViewModel.DocumentType.Key) : (int?)null : (int?)null;
            headDTO.AccountID = masterViewModel.Account != null ? !string.IsNullOrEmpty(masterViewModel.Account.Key) ? Convert.ToInt32(masterViewModel.Account.Key) : (int?)null : (int?)null;
            headDTO.PaymentModeID = masterViewModel.PaymentModes != null ? !string.IsNullOrEmpty(masterViewModel.PaymentModes.Key) ? Convert.ToInt16(masterViewModel.PaymentModes.Key) : (short?)null : (short?)null;
            headDTO.CurrencyID = masterViewModel.Currency != null ? !string.IsNullOrEmpty(masterViewModel.Currency.Key) ? Convert.ToInt32(masterViewModel.Currency.Key) : (int?)null : (int?)null;
            headDTO.CostCenterID = masterViewModel.CostCenter != null ? !string.IsNullOrEmpty(masterViewModel.CostCenter.Key) ? Convert.ToInt32(masterViewModel.CostCenter.Key) : (int?)null : (int?)null;
            headDTO.DetailAccountID = masterViewModel.DetailAccount != null ? !string.IsNullOrEmpty(masterViewModel.DetailAccount.Key) ? Convert.ToInt32(masterViewModel.DetailAccount.Key) : (int?)null : (int?)null;
            headDTO.TransactionStatusID = masterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(masterViewModel.TransactionStatus.Key) ? Convert.ToByte(masterViewModel.TransactionStatus.Key) : (Byte?)null : (Byte?)null;

            headDTO.CreatedBy = masterViewModel.CreatedBy;
            headDTO.CreatedDate = masterViewModel.CreatedDate;
            headDTO.TransactionDate = DateTime.ParseExact(masterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
            headDTO.Remarks = masterViewModel.Remarks;
            headDTO.UpdatedBy = masterViewModel.UpdatedBy;
            headDTO.UpdatedDate = masterViewModel.UpdatedDate;
            headDTO.AccountTransactionHeadIID = masterViewModel.AccountTransactionHeadIID;
            headDTO.TransactionNumber = masterViewModel.TransactionNumber;
            headDTO.TimeStamps = masterViewModel.TimeStamps;
            headDTO.IsPrePaid = masterViewModel.IsPrePaid;
            headDTO.ExchangeRate = Convert.ToDecimal(masterViewModel.ExchangeRate);
            headDTO.AmountPaid = Convert.ToDecimal(masterViewModel.AmountPaid);
            headDTO.AdvanceAmount = Convert.ToDecimal(masterViewModel.AdvanceAmount);
            headDTO.ChequeNumber = masterViewModel.ChequeNumber;
            headDTO.ChequeDate = string.IsNullOrEmpty(masterViewModel.ChequeDateString) ? (DateTime?)null : DateTime.ParseExact(masterViewModel.ChequeDateString, dateFormat, CultureInfo.InvariantCulture);
            headDTO.BranchID = long.Parse(masterViewModel.Branch);

            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (PVRegularPaymentDetailViewModel detailVMItem in DeatilViewModelList)
            {
                var detailDTO = new AccountTransactionDetailsDTO();

                //detailDTO.AccountID = detailVMItem.AccountID;
                detailDTO.AccountTransactionDetailIID = detailVMItem.AccountTransactionDetailIID;
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.AccountTransactionHeadID = detailVMItem.AccountTransactionHeadID;
                detailDTO.Amount = Convert.ToDecimal(detailVMItem.Amount);
                // detailDTO.CostCenterID = detailVMItem.CostCenterID;
                detailDTO.ReferenceNumber = detailVMItem.ReferenceNumber;
                detailDTO.Remarks = detailVMItem.Remarks;
                detailDTO.TimeStamps = detailVMItem.TimeStamps;
                detailDTO.ReceivableID = detailVMItem.ReceivableID;

                detailDTO.PaidAmount = Convert.ToDecimal(detailVMItem.PaidAmount);
                // detailDTO.PaymentDueDate = Convert.ToDateTime(detailVMItem.PaymentDueDate);
                detailDTO.ReturnAmount = Convert.ToDecimal(detailVMItem.ReturnAmount);
                detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);
                detailDTO.AccountTypeID = detailVMItem.AccountTypes != null ? !string.IsNullOrEmpty(detailVMItem.AccountTypes.Key) ? Convert.ToInt32(detailVMItem.AccountTypes.Key) : (int?)null : (int?)null;
                detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;


                //if (detailVMItem.AccountTypes.Key == "1")
                //{
                //    detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                //}
                //else
                //{
                //    detailDTO.AccountID = detailVMItem.VendorCustomerAccounts != null ? !string.IsNullOrEmpty(detailVMItem.VendorCustomerAccounts.Key) ? Convert.ToInt32(detailVMItem.VendorCustomerAccounts.Key) : (int?)null : (int?)null;
                //}

                detailDTO.CostCenterID = detailVMItem.CostCenter != null ? !string.IsNullOrEmpty(detailVMItem.CostCenter.Key) ? Convert.ToInt32(detailVMItem.CostCenter.Key) : (int?)null : (int?)null;
                detailDTO.SubLedgerID = detailVMItem.AccountSubLedgers != null ? !string.IsNullOrEmpty(detailVMItem.AccountSubLedgers.Key) ? Convert.ToInt32(detailVMItem.AccountSubLedgers.Key) : (int?)null : (int?)null;
                detailDTO.BudgetID = detailVMItem.Budget != null ? !string.IsNullOrEmpty(detailVMItem.Budget.Key) ? Convert.ToInt32(detailVMItem.Budget.Key) : (int?)null : (int?)null;


                if (detailVMItem.Credit > 0)
                {
                    detailDTO.Amount = -1 * Convert.ToDecimal(detailVMItem.Credit);
                }
                else
                {
                    detailDTO.Amount = Convert.ToDecimal(detailVMItem.Debit);
                }

                headDTO.AccountTransactionDetails.Add(detailDTO);
            }
            return headDTO;
        }

        public PVRegularPaymentViewModel ToPVRegularPaymentViewModel(AccountTransactionHeadDTO headDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            PVRegularPaymentViewModel pvRegularReceiptViewModel = new PVRegularPaymentViewModel();
            pvRegularReceiptViewModel.MasterViewModel = new PVRegularPaymentMasterViewModel();
            pvRegularReceiptViewModel.DetailViewModel = new List<PVRegularPaymentDetailViewModel>();
            pvRegularReceiptViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            pvRegularReceiptViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            pvRegularReceiptViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            pvRegularReceiptViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            pvRegularReceiptViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            pvRegularReceiptViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            pvRegularReceiptViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            pvRegularReceiptViewModel.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            pvRegularReceiptViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            pvRegularReceiptViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            pvRegularReceiptViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            pvRegularReceiptViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            pvRegularReceiptViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            pvRegularReceiptViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            pvRegularReceiptViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            pvRegularReceiptViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            pvRegularReceiptViewModel.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            pvRegularReceiptViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;
            pvRegularReceiptViewModel.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;
            pvRegularReceiptViewModel.MasterViewModel.ChequeNumber = headDTO.ChequeNumber;
            pvRegularReceiptViewModel.MasterViewModel.ErrorCode = headDTO.ErrorCode;
            pvRegularReceiptViewModel.MasterViewModel.IsError = headDTO.IsError;
            pvRegularReceiptViewModel.ErrorCode = headDTO.ErrorCode;
            pvRegularReceiptViewModel.IsError = headDTO.IsError;
            pvRegularReceiptViewModel.MasterViewModel.ChequeDateString = headDTO.ChequeDate.HasValue ? headDTO.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            pvRegularReceiptViewModel.MasterViewModel.Branch = headDTO.BranchID.ToString();

            if (headDTO.DocumentStatus != null)
                pvRegularReceiptViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                pvRegularReceiptViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                pvRegularReceiptViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                pvRegularReceiptViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                pvRegularReceiptViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                pvRegularReceiptViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                pvRegularReceiptViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                pvRegularReceiptViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new PVRegularPaymentDetailViewModel();

                detailViewModel.Amount = detailDTOItem.Amount;
                detailViewModel.AccountID = detailDTOItem.AccountID;
                detailViewModel.AccountTransactionDetailIID = detailDTOItem.AccountTransactionDetailIID;
                detailViewModel.CostCenterID = detailDTOItem.CostCenterID;
                detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                detailViewModel.InvoiceAmount = Convert.ToDouble(detailDTOItem.InvoiceAmount);
                detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                detailViewModel.InvoiceNumber = detailDTOItem.InvoiceNumber;
                detailViewModel.PaidAmount = Convert.ToDouble(detailDTOItem.PaidAmount);
                //detailViewModel.DueDate = detailDTOItem.PaymentDue;
                detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                detailViewModel.Remarks = detailDTOItem.Remarks;
                detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                detailViewModel.ReceivableID = detailDTOItem.ReceivableID;
                detailViewModel.SubLedgerID = detailDTOItem.SubLedgerID;

                if (detailDTOItem.SubLedger != null)
                    detailViewModel.AccountSubLedgers = new KeyValueViewModel() { Key = detailDTOItem.SubLedger.Key, Value = detailDTOItem.SubLedger.Value };

                if (detailDTOItem.CostCenter != null)
                    detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                if (detailDTOItem.Account != null)
                    detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };

                if (detailDTOItem.AccountGroup != null)
                    detailViewModel.AccountGroup = new KeyValueViewModel() { Key = detailDTOItem.AccountGroup.Key, Value = detailDTOItem.AccountGroup.Value };


                if (detailDTOItem.Budget != null)
                    detailViewModel.Budget = new KeyValueViewModel() { Key = detailDTOItem.Budget.Key, Value = detailDTOItem.Budget.Value };
                //if (detailDTOItem.AccountTypeID != null)
                //{
                //    if (detailDTOItem.AccountTypeID == 1)
                //    {
                //        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "1", Value = "ChartOfAccounts" };
                //    }
                //    else
                //    {
                //        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "2", Value = "VendorCustomerAccounts" };
                //    }
                //}

                //if(detailDTOItem.AccountTypeID ==1)
                //{
                //    if (detailDTOItem.Account != null)
                //        detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                //}
                //else
                //{
                //    if (detailDTOItem.Account != null)
                //        detailViewModel.VendorCustomerAccounts = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                //}



                if (detailViewModel.Amount > 0)
                {

                    detailViewModel.Debit = detailViewModel.Amount;
                    detailViewModel.DebitTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                }
                else
                {
                    detailViewModel.Credit = -1 * detailViewModel.Amount;
                    detailViewModel.CreditTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                }

                pvRegularReceiptViewModel.DetailViewModel.Add(detailViewModel);
            }
            return pvRegularReceiptViewModel;
        }


    }
}

