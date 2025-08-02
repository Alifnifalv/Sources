using System;
using System.Collections.Generic;
using System.Globalization;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class PVInvoiceViewModel : BaseMasterViewModel
    {
        public PVInvoiceViewModel()
        {
            DetailViewModel = new List<PVInvoiceDetailViewModel>() { new PVInvoiceDetailViewModel() };
            MasterViewModel = new PVInvoiceMasterViewModel();
        }

        public List<PVInvoiceDetailViewModel> DetailViewModel { get; set; }
        public PVInvoiceMasterViewModel MasterViewModel { get; set; }


        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(PVInvoiceViewModel viewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            PVInvoiceMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<PVInvoiceDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            AccountTransactionHeadDTO headDTO = new AccountTransactionHeadDTO();

            headDTO.DocumentStatusID = masterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(masterViewModel.DocumentStatus.Key) ? Convert.ToInt32(masterViewModel.DocumentStatus.Key) : (long?)null : (long?)null;
            headDTO.DocumentTypeID = masterViewModel.DocumentType != null ? !string.IsNullOrEmpty(masterViewModel.DocumentType.Key) ? Convert.ToInt32(masterViewModel.DocumentType.Key) : (int?)null : (int?)null;
            headDTO.AccountID = masterViewModel.Account != null ? !string.IsNullOrEmpty(masterViewModel.Account.Key) ? Convert.ToInt32(masterViewModel.Account.Key) : (int?)null : (int?)null;
            headDTO.PaymentModeID = masterViewModel.PaymentModes != null ? !string.IsNullOrEmpty(masterViewModel.PaymentModes.Key) ? Convert.ToInt16(masterViewModel.PaymentModes.Key) : (short?)null : (short?)null;
            headDTO.CurrencyID = masterViewModel.Currency != null ? !string.IsNullOrEmpty(masterViewModel.Currency.Key) ? Convert.ToInt32(masterViewModel.Currency.Key) : (int?)null : (int?)null;
            headDTO.CostCenterID = masterViewModel.CostCenter != null ? !string.IsNullOrEmpty(masterViewModel.CostCenter.Key) ? Convert.ToInt32(masterViewModel.CostCenter.Key) : (int?)null : (int?)null;
            headDTO.DetailAccountID = masterViewModel.DetailAccount != null ? !string.IsNullOrEmpty(masterViewModel.DetailAccount.Key) ? Convert.ToInt32(masterViewModel.DetailAccount.Key) : (int?)null : (int?)null;
            headDTO.TransactionStatusID = masterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(masterViewModel.TransactionStatus.Key) ? Convert.ToByte(masterViewModel.TransactionStatus.Key) : (Byte?)null : (Byte?)null;

            headDTO.BranchID = long.Parse(masterViewModel.Branch);
            headDTO.CreatedBy = masterViewModel.CreatedBy;
            headDTO.CreatedDate = masterViewModel.CreatedDate;
            headDTO.TransactionDate = DateTime.ParseExact(masterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture); ;
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
            headDTO.ChequeDate = masterViewModel.ChequeDateString == null ? (DateTime?)null : DateTime.ParseExact(masterViewModel.ChequeDateString, dateFormat, CultureInfo.InvariantCulture);

            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (PVInvoiceDetailViewModel detailVMItem in DeatilViewModelList)
            {
                var detailDTO = new AccountTransactionDetailsDTO();


                detailDTO.AccountID = headDTO.AccountID;
                detailDTO.AccountTransactionDetailIID = detailVMItem.AccountTransactionDetailIID;
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.AccountTransactionHeadID = detailVMItem.AccountTransactionHeadID;
                detailDTO.Amount = Convert.ToDecimal(detailVMItem.Amount);
                detailDTO.CostCenterID = detailVMItem.CostCenterID;
                detailDTO.ReferenceNumber = detailVMItem.ReferenceNumber;
                detailDTO.Remarks = detailVMItem.Remarks;
                detailDTO.TimeStamps = detailVMItem.TimeStamps;
                detailDTO.PaidAmount = Convert.ToDecimal(detailVMItem.PaidAmount);
                detailDTO.PaymentDueDate = detailVMItem.PaymentDueDate != null ? DateTime.ParseExact(detailVMItem.PaymentDueDate, dateFormat, CultureInfo.InvariantCulture) : DateTime.Now;
                detailDTO.ReturnAmount = Convert.ToDecimal(detailVMItem.ReturnAmount);
                detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);
                detailDTO.InvoiceNumber = detailVMItem.InvoiceNumber;
                detailDTO.UnPaidAmount = Convert.ToDecimal(detailVMItem.UnPaidAmount);
                //detailDTO.ExchangeRate = Convert.ToDecimal(detailVMItem.ExchangeRate);
                detailDTO.PayableID = detailVMItem.PayableID;
                detailDTO.CurrencyID = detailVMItem.CurrencyID;

                headDTO.AccountTransactionDetails.Add(detailDTO);
            }
            return headDTO;
        }

        public PVInvoiceViewModel ToPVInvoiceViewModel(AccountTransactionHeadDTO headDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var pvInvoiceViewModel = new PVInvoiceViewModel();
            pvInvoiceViewModel.MasterViewModel = new PVInvoiceMasterViewModel();
            pvInvoiceViewModel.DetailViewModel = new List<PVInvoiceDetailViewModel>();
            pvInvoiceViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            pvInvoiceViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            pvInvoiceViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            pvInvoiceViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            pvInvoiceViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            pvInvoiceViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            pvInvoiceViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            pvInvoiceViewModel.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            pvInvoiceViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            pvInvoiceViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            pvInvoiceViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            pvInvoiceViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            pvInvoiceViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            pvInvoiceViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            pvInvoiceViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            pvInvoiceViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            pvInvoiceViewModel.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            pvInvoiceViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;
            pvInvoiceViewModel.MasterViewModel.ExchangeRate = Convert.ToDouble(headDTO.ExchangeRate);
            pvInvoiceViewModel.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;          
            pvInvoiceViewModel.MasterViewModel.ErrorCode = headDTO.ErrorCode;
            pvInvoiceViewModel.MasterViewModel.IsError = headDTO.IsError;
            pvInvoiceViewModel.ErrorCode = headDTO.ErrorCode;
            pvInvoiceViewModel.IsError = headDTO.IsError;
            pvInvoiceViewModel.MasterViewModel.ChequeNumber = headDTO.ChequeNumber;
            pvInvoiceViewModel.MasterViewModel.ChequeDateString = headDTO.ChequeDate.HasValue ? headDTO.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            pvInvoiceViewModel.MasterViewModel.Branch = headDTO.BranchID.ToString();

            if (headDTO.DocumentStatus != null)
                pvInvoiceViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                pvInvoiceViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                pvInvoiceViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                pvInvoiceViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                pvInvoiceViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                pvInvoiceViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                pvInvoiceViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                pvInvoiceViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (AccountTransactionDetailsDTO detailDTOItem in headDTO.AccountTransactionDetails)
            {
                PVInvoiceDetailViewModel detailViewModel = new PVInvoiceDetailViewModel();

                detailViewModel.Amount = Convert.ToDouble(detailDTOItem.Amount);
                detailViewModel.AccountID = detailDTOItem.AccountID;
                detailViewModel.AccountTransactionDetailIID = detailDTOItem.AccountTransactionDetailIID;
                detailViewModel.CostCenterID = detailDTOItem.CostCenterID;
                detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                detailViewModel.InvoiceAmount = Convert.ToDouble(detailDTOItem.InvoiceAmount);
                detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                detailViewModel.InvoiceNumber = detailDTOItem.InvoiceNumber;
                detailViewModel.PaidAmount = Convert.ToDouble(detailDTOItem.PaidAmount);
                detailViewModel.PaymentDueDate = detailDTOItem.PaymentDueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
                detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                detailViewModel.Remarks = detailDTOItem.Remarks;
                detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                //detailViewModel.ExchangeRate = Convert.ToDouble(detailDTOItem.ExchangeRate);
                detailViewModel.UnPaidAmount = Convert.ToDouble(detailDTOItem.UnPaidAmount);
                detailViewModel.PayableID = detailDTOItem.PayableID;
                detailViewModel.CurrencyID = detailDTOItem.CurrencyID;
                //if (detailDTOItem.Currency != null)
                //{
                //    detailViewModel.CurrencyName = detailDTOItem.Currency.Value;
                //}

                pvInvoiceViewModel.DetailViewModel.Add(detailViewModel);
            }
            return pvInvoiceViewModel;
        }


    }
}

