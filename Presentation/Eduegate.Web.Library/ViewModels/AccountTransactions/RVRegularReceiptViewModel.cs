using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Web.Library.ViewModels.Distributions;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class RVRegularReceiptViewModel : BaseMasterViewModel
    {
        public RVRegularReceiptViewModel()
        {
            DetailViewModel = new List<RVRegularReceiptDetailViewModel>() { new RVRegularReceiptDetailViewModel() };
            MasterViewModel = new RVRegularReceiptMasterViewModel();
        }

        public List<RVRegularReceiptDetailViewModel> DetailViewModel { get; set; }
        public RVRegularReceiptMasterViewModel MasterViewModel { get; set; }

        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(RVRegularReceiptViewModel viewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            RVRegularReceiptMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<RVRegularReceiptDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            var headDTO = new AccountTransactionHeadDTO();

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
            headDTO.ChequeDate = masterViewModel.ChequeDateString == null ? (DateTime?) null : DateTime.ParseExact(masterViewModel.ChequeDateString, dateFormat, CultureInfo.InvariantCulture);
            headDTO.BranchID = long.Parse(masterViewModel.Branch);

            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (RVRegularReceiptDetailViewModel detailVMItem in DeatilViewModelList)
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

                detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                detailDTO.CostCenterID = detailVMItem.CostCenter != null ? !string.IsNullOrEmpty(detailVMItem.CostCenter.Key) ? Convert.ToInt32(detailVMItem.CostCenter.Key) : (int?)null : (int?)null;
                detailDTO.SubLedgerID = detailVMItem.AccountSubLedgers != null ? !string.IsNullOrEmpty(detailVMItem.AccountSubLedgers.Key) ? Convert.ToInt32(detailVMItem.AccountSubLedgers.Key) : (int?)null : (int?)null;
                detailDTO.BudgetID = detailVMItem.Budget != null ? !string.IsNullOrEmpty(detailVMItem.Budget.Key) ? Convert.ToInt32(detailVMItem.Budget.Key) : (int?)null : (int?)null;

                //if (detailVMItem.AccountTypes.Key == "1")
                //{
                //    detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                //}
                //else
                //{
                //    detailDTO.AccountID = detailVMItem.VendorCustomerAccounts != null ? !string.IsNullOrEmpty(detailVMItem.VendorCustomerAccounts.Key) ? Convert.ToInt32(detailVMItem.VendorCustomerAccounts.Key) : (int?)null : (int?)null;
                //}
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

        public RVRegularReceiptViewModel ToRVRegularReceiptViewModel(AccountTransactionHeadDTO headDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var rvRegularReceiptViewModel = new RVRegularReceiptViewModel();
            rvRegularReceiptViewModel.MasterViewModel = new RVRegularReceiptMasterViewModel();
            rvRegularReceiptViewModel.DetailViewModel = new List<RVRegularReceiptDetailViewModel>();
            rvRegularReceiptViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            rvRegularReceiptViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            rvRegularReceiptViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            rvRegularReceiptViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            rvRegularReceiptViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            rvRegularReceiptViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            rvRegularReceiptViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            rvRegularReceiptViewModel.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            rvRegularReceiptViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            rvRegularReceiptViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            rvRegularReceiptViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            rvRegularReceiptViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            rvRegularReceiptViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            rvRegularReceiptViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            rvRegularReceiptViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            rvRegularReceiptViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            rvRegularReceiptViewModel.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            rvRegularReceiptViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;
            rvRegularReceiptViewModel.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;
            rvRegularReceiptViewModel.MasterViewModel.ChequeNumber = headDTO.ChequeNumber;
            rvRegularReceiptViewModel.MasterViewModel.ChequeDateString = headDTO.ChequeDate.HasValue ? headDTO.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            rvRegularReceiptViewModel.MasterViewModel.Branch = headDTO.BranchID.ToString();
            rvRegularReceiptViewModel.MasterViewModel.ErrorCode = headDTO.ErrorCode;
            rvRegularReceiptViewModel.MasterViewModel.IsError = headDTO.IsError;
            rvRegularReceiptViewModel.ErrorCode = headDTO.ErrorCode;
            rvRegularReceiptViewModel.IsError = headDTO.IsError;
            if (headDTO.DocumentStatus != null)
                rvRegularReceiptViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                rvRegularReceiptViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                rvRegularReceiptViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                rvRegularReceiptViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                rvRegularReceiptViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                rvRegularReceiptViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                rvRegularReceiptViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                rvRegularReceiptViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new RVRegularReceiptDetailViewModel();

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

                if (detailDTOItem.CostCenter != null)
                    detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                if (detailDTOItem.Account != null)
                    detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };

                if (detailDTOItem.AccountGroup != null)
                    detailViewModel.AccountGroup = new KeyValueViewModel() { Key = detailDTOItem.AccountGroup.Key, Value = detailDTOItem.AccountGroup.Value };

                if (detailDTOItem.SubLedger != null)
                    detailViewModel.AccountSubLedgers = new KeyValueViewModel() { Key = detailDTOItem.SubLedger.Key, Value = detailDTOItem.SubLedger.Value };


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
                //else
                //{
                //    if(detailDTOItem.Account != null)
                //    {
                //        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "1", Value = "ChartOfAccounts" };
                //    }
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



                rvRegularReceiptViewModel.DetailViewModel.Add(detailViewModel);
            }
            return rvRegularReceiptViewModel;
        }
    }
}

