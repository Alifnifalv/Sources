using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class JournalEntryViewModel : BaseMasterViewModel
    {
        public JournalEntryViewModel()
        {
            MasterViewModel = new JournalMasterViewModel();
            DetailViewModel = new List<JournalDetailViewModel>() { new JournalDetailViewModel() };
        }

        public JournalMasterViewModel MasterViewModel { get; set; }
        public List<JournalDetailViewModel> DetailViewModel { get; set; }

        public static JournalEntryViewModel FromDTO(AccountTransactionHeadDTO headDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = new JournalEntryViewModel();
            vm.MasterViewModel = new JournalMasterViewModel();
            vm.DetailViewModel = new List<JournalDetailViewModel>();
            vm.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            vm.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            vm.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            vm.MasterViewModel.AccountID = headDTO.AccountID;
            vm.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            vm.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            vm.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            vm.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            vm.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            vm.MasterViewModel.Remarks = headDTO.Remarks;
            vm.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            vm.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            vm.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            vm.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            vm.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            vm.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            vm.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;
            vm.MasterViewModel.ErrorCode= headDTO.ErrorCode;
            vm.MasterViewModel.IsError = headDTO.IsError;
            vm.ErrorCode = headDTO.ErrorCode;
            vm.IsError = headDTO.IsError;
            vm.MasterViewModel.Discount = headDTO.DiscountAmount;
            vm.MasterViewModel.DiscountPercentage = headDTO.DiscountPercentage;

            vm.MasterViewModel.CompanyID = headDTO.CompanyID;

            vm.MasterViewModel.Branch = headDTO.BranchID.ToString();

            vm.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;

            if (headDTO.DocumentStatus != null)
                vm.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                vm.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                vm.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                vm.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                vm.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                vm.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                vm.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                vm.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new JournalDetailViewModel();

                detailViewModel.Amount = Convert.ToDecimal(detailDTOItem.Amount);
                detailViewModel.AccountID = detailDTOItem.AccountID.HasValue ? detailDTOItem.AccountID : null;
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
                detailViewModel.Narration = detailDTOItem.Remarks;
                detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                detailViewModel.ReceivableID = detailDTOItem.ReceivableID;
                detailViewModel.SubLedgerID = detailDTOItem.SubLedgerID;
                detailViewModel.BudgetID = detailDTOItem.BudgetID;

                if (detailViewModel.Amount > 0)
                {
                    detailViewModel.Debit = Convert.ToDecimal(detailViewModel.Amount);
                    detailViewModel.DebitTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                }
                else
                {
                    detailViewModel.Credit = Convert.ToDecimal(-1 * detailViewModel.Amount);
                    detailViewModel.CreditTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                }

                if (detailDTOItem.CostCenter != null)
                    detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                if (detailDTOItem.Budget != null)
                    detailViewModel.Budget = new KeyValueViewModel() { Key = detailDTOItem.Budget.Key, Value = detailDTOItem.Budget.Value };

                if (detailDTOItem.Account != null)
                { 
                    detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                    detailViewModel.Description = detailDTOItem.Account.Value;
                }
                if (detailDTOItem.SubLedger != null)
                    detailViewModel.AccountSubLedgers = new KeyValueViewModel() { Key = detailDTOItem.SubLedger.Key, Value = detailDTOItem.SubLedger.Value };

                if (detailDTOItem.AccountGroup != null)
                    detailViewModel.AccountGroup = new KeyValueViewModel() { Key = detailDTOItem.AccountGroup.Key, Value = detailDTOItem.AccountGroup.Value };

                vm.DetailViewModel.Add(detailViewModel);
            }
            return vm;
        }

        public static AccountTransactionHeadDTO ToDTO(JournalEntryViewModel viewModel)
        {
            JournalMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<JournalDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var headDTO = new AccountTransactionHeadDTO();

            headDTO.DocumentStatusID = masterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(masterViewModel.DocumentStatus.Key) ? Convert.ToInt32(masterViewModel.DocumentStatus.Key) : (long?)null : (long?)null;
            headDTO.DocumentTypeID = masterViewModel.DocumentType != null ? !string.IsNullOrEmpty(masterViewModel.DocumentType.Key) ? Convert.ToInt32(masterViewModel.DocumentType.Key) : (int?)null : (int?)null;
            headDTO.AccountID = masterViewModel.Account != null ? !string.IsNullOrEmpty(masterViewModel.Account.Key) ? Convert.ToInt32(masterViewModel.Account.Key) : (int?)null : (int?)null;
            headDTO.PaymentModeID = masterViewModel.PaymentModes != null ? !string.IsNullOrEmpty(masterViewModel.PaymentModes.Key) ? Convert.ToInt16(masterViewModel.PaymentModes.Key) : (short?)null : (short?)null;
            headDTO.CurrencyID = masterViewModel.Currency != null ? !string.IsNullOrEmpty(masterViewModel.Currency.Key) ? Convert.ToInt32(masterViewModel.Currency.Key) : (int?)null : (int?)null;
            headDTO.CostCenterID = masterViewModel.CostCenter != null ? !string.IsNullOrEmpty(masterViewModel.CostCenter.Key) ? Convert.ToInt32(masterViewModel.CostCenter.Key) : (int?)null : (int?)null;
            headDTO.DetailAccountID = masterViewModel.DetailAccount != null ? !string.IsNullOrEmpty(masterViewModel.DetailAccount.Key) ? Convert.ToInt32(masterViewModel.DetailAccount.Key) : (int?)null : (int?)null;
            headDTO.TransactionStatusID = masterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(masterViewModel.TransactionStatus.Key) ? Convert.ToByte(masterViewModel.TransactionStatus.Key) : (Byte?)null : (Byte?)null;
            headDTO.CostCenterID = masterViewModel.AccountSubLedgers != null ? !string.IsNullOrEmpty(masterViewModel.AccountSubLedgers.Key) ? Convert.ToInt32(masterViewModel.AccountSubLedgers.Key) : (int?)null : (int?)null;
            headDTO.CostCenterID = masterViewModel.AccountSubLedgers != null ? !string.IsNullOrEmpty(masterViewModel.AccountSubLedgers.Key) ? Convert.ToInt32(masterViewModel.AccountSubLedgers.Key) : (int?)null : (int?)null;

            headDTO.CreatedBy = masterViewModel.CreatedBy;
            headDTO.CreatedDate = masterViewModel.CreatedDate;
            //headDTO.TransactionDate = Convert.ToDateTime(masterViewModel.TransactionDate.ToString());
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
            
            headDTO.CompanyID = masterViewModel.CompanyID;

            headDTO.BranchID = long.Parse(masterViewModel.Branch);

            headDTO.DiscountPercentage = masterViewModel.DiscountPercentage;
            headDTO.DiscountAmount = masterViewModel.Discount;

            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (JournalDetailViewModel detailVMItem in DeatilViewModelList)
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
                detailDTO.Remarks = detailVMItem.Narration;
                detailDTO.TimeStamps = detailVMItem.TimeStamps;
                detailDTO.ReceivableID = detailVMItem.ReceivableID;
                detailDTO.SubLedgerID = detailVMItem.AccountSubLedgers != null ? !string.IsNullOrEmpty(detailVMItem.AccountSubLedgers.Key) ? Convert.ToInt32(detailVMItem.AccountSubLedgers.Key) : (int?)null : (int?)null;

                detailDTO.PaidAmount = Convert.ToDecimal(detailVMItem.PaidAmount);
                // detailDTO.PaymentDueDate = Convert.ToDateTime(detailVMItem.PaymentDueDate);
                detailDTO.ReturnAmount = Convert.ToDecimal(detailVMItem.ReturnAmount);
                detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);

                detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                detailDTO.CostCenterID = detailVMItem.CostCenter != null ? !string.IsNullOrEmpty(detailVMItem.CostCenter.Key) ? Convert.ToInt32(detailVMItem.CostCenter.Key) : (int?)null : (int?)null;
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
    }
}
