using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Web.Library.ViewModels;
namespace Eduegate.Web.Library.Accounts.AccountTransactions
{
    public class GeneralInvoiceViewModel : BaseMasterViewModel
    {
        public GeneralInvoiceViewModel()
        {
            DetailViewModel = new List<GeneralInvoiceDetailViewModel>() { new GeneralInvoiceDetailViewModel() };
            MasterViewModel = new GeneralInvoiceMasterViewModel();
        }

        public List<GeneralInvoiceDetailViewModel> DetailViewModel { get; set; }
        public GeneralInvoiceMasterViewModel MasterViewModel { get; set; }
        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(GeneralInvoiceViewModel viewModel)
        {
            GeneralInvoiceMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<GeneralInvoiceDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            AccountTransactionHeadDTO headDTO = new AccountTransactionHeadDTO();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

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
            //headDTO.TransactionDate = Convert.ToDateTime(masterViewModel.TransactionDate.ToString());
            headDTO.TransactionDate = DateTime.ParseExact(masterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
            headDTO.Remarks = masterViewModel.Remarks;
            headDTO.UpdatedBy = masterViewModel.UpdatedBy;
            headDTO.UpdatedDate = masterViewModel.UpdatedDate;
            headDTO.AccountTransactionHeadIID = masterViewModel.AccountTransactionHeadIID;
            headDTO.TransactionNumber = masterViewModel.TransactionNo;
            headDTO.TimeStamps = masterViewModel.TimeStamps;
            headDTO.IsPrePaid = masterViewModel.IsPrePaid;
            headDTO.ExchangeRate = Convert.ToDecimal(masterViewModel.ExchangeRate);
            headDTO.AmountPaid = Convert.ToDecimal(masterViewModel.AmountPaid);
            headDTO.AdvanceAmount = Convert.ToDecimal(masterViewModel.AdvanceAmount);
            headDTO.BranchID = long.Parse(masterViewModel.Branch);
            headDTO.Reference = masterViewModel.Reference;
            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (var detailVMItem in DeatilViewModelList)
            {
                AccountTransactionDetailsDTO detailDTO = new AccountTransactionDetailsDTO();


                detailDTO.AccountTransactionDetailIID = detailVMItem.AccountTransactionDetailIID;
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.AccountTransactionHeadID = detailVMItem.AccountTransactionHeadID;
                detailDTO.Amount = Convert.ToDecimal(detailVMItem.Amount);
                detailDTO.ReferenceNumber = detailVMItem.ReferenceNumber;
                detailDTO.Remarks = detailVMItem.Remarks;
                detailDTO.TimeStamps = detailVMItem.TimeStamps;
                detailDTO.ReceivableID = detailVMItem.ReceivableID;
                detailDTO.SubLedgerID = detailVMItem.SubLedgerID;

                detailDTO.PaidAmount = Convert.ToDecimal(detailVMItem.PaidAmount);
                detailDTO.ReturnAmount = Convert.ToDecimal(detailVMItem.ReturnAmount);
                detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);

                //detailDTO.AccountTypeID = detailVMItem.AccountTypes != null ? !string.IsNullOrEmpty(detailVMItem.AccountTypes.Key) ? Convert.ToInt32(detailVMItem.AccountTypes.Key) : (int?)null : (int?)null;
                detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                detailDTO.CostCenterID = detailVMItem.CostCenter != null ? !string.IsNullOrEmpty(detailVMItem.CostCenter.Key) ? Convert.ToInt32(detailVMItem.CostCenter.Key) : (int?)null : (int?)null;
                detailDTO.SubLedgerID = detailVMItem.AccountSubLedgers != null ? !string.IsNullOrEmpty(detailVMItem.AccountSubLedgers.Key) ? Convert.ToInt32(detailVMItem.AccountSubLedgers.Key) : (int?)null : (int?)null;
                detailDTO.BudgetID = detailVMItem.Budget != null ? !string.IsNullOrEmpty(detailVMItem.Budget.Key) ? Convert.ToInt32(detailVMItem.Budget.Key) : (int?)null : (int?)null;


                //if (detailVMItem.AccountTypes.Key == "1")
                //{
                //detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
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

        public GeneralInvoiceViewModel ToGeneralInvoiceViewModel(AccountTransactionHeadDTO headDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var GeneralInvoiceViewModel = new GeneralInvoiceViewModel();
            GeneralInvoiceViewModel.MasterViewModel = new GeneralInvoiceMasterViewModel();
            GeneralInvoiceViewModel.DetailViewModel = new List<GeneralInvoiceDetailViewModel>();
            GeneralInvoiceViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            GeneralInvoiceViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            GeneralInvoiceViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentTypeID.ToString(), Value = headDTO.DocumentType?.Value };
            GeneralInvoiceViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            GeneralInvoiceViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            GeneralInvoiceViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            GeneralInvoiceViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            GeneralInvoiceViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            GeneralInvoiceViewModel.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            GeneralInvoiceViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            GeneralInvoiceViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            GeneralInvoiceViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            GeneralInvoiceViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            GeneralInvoiceViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            GeneralInvoiceViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            GeneralInvoiceViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            GeneralInvoiceViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            GeneralInvoiceViewModel.MasterViewModel.TransactionNo = headDTO.TransactionNumber;
            GeneralInvoiceViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;
            GeneralInvoiceViewModel.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;
            GeneralInvoiceViewModel.MasterViewModel.Branch = headDTO.BranchID.ToString();
            GeneralInvoiceViewModel.MasterViewModel.Reference = headDTO.Reference;
            GeneralInvoiceViewModel.MasterViewModel.ErrorCode = headDTO.ErrorCode;
            GeneralInvoiceViewModel.MasterViewModel.IsError = headDTO.IsError;
            GeneralInvoiceViewModel.ErrorCode = headDTO.ErrorCode;
            GeneralInvoiceViewModel.IsError = headDTO.IsError;
            if (headDTO.DocumentStatus != null)
                GeneralInvoiceViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                GeneralInvoiceViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                GeneralInvoiceViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                GeneralInvoiceViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                GeneralInvoiceViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                GeneralInvoiceViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                GeneralInvoiceViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                GeneralInvoiceViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new GeneralInvoiceDetailViewModel();

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
                detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                detailViewModel.Remarks = detailDTOItem.Remarks;
                detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                if (detailDTOItem.ReceivableID.HasValue)
                    detailViewModel.ReceivableID = detailDTOItem.ReceivableID;
                else
                    detailViewModel.ReceivableID = detailDTOItem.ReferenceReceiptID;
                //detailViewModel.SubLedgerID = detailDTOItem.SubLedgerID;

                if (detailDTOItem.CostCenter != null)
                    detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

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

                //if (detailDTOItem.AccountTypeID == 1)
                //{
                if (detailDTOItem.Account != null)
                    detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                //}
                //else
                //{
                //    if (detailDTOItem.Account != null)
                //        detailViewModel.VendorCustomerAccounts = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                //}

                if (detailDTOItem.AccountGroup != null)
                    detailViewModel.AccountGroup = new KeyValueViewModel() { Key = detailDTOItem.AccountGroup.Key, Value = detailDTOItem.AccountGroup.Value };

                if (detailDTOItem.SubLedger != null)
                    detailViewModel.AccountSubLedgers = new KeyValueViewModel() { Key = detailDTOItem.SubLedger.Key, Value = detailDTOItem.SubLedger.Value };


                if (detailViewModel.Amount > 0)
                {
                    detailViewModel.Credit = 0;
                    detailViewModel.Debit = detailViewModel.Amount;
                    detailViewModel.DebitTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                }
                else
                {
                    detailViewModel.Debit = 0;
                    detailViewModel.Credit = -1 * detailViewModel.Amount;
                    detailViewModel.CreditTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                }

                detailViewModel.BudgetID = detailDTOItem.BudgetID;

                if (detailDTOItem.Budget != null)
                    detailViewModel.Budget = new KeyValueViewModel() { Key = detailDTOItem.Budget.Key, Value = detailDTOItem.Budget.Value };


                GeneralInvoiceViewModel.DetailViewModel.Add(detailViewModel);
            }
            return GeneralInvoiceViewModel;
        }


    }
}

