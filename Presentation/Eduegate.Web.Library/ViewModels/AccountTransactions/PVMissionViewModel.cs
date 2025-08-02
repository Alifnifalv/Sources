using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class PVMissionViewModel : BaseMasterViewModel
    {
        public PVMissionViewModel()
        {
            DetailViewModel = new List<PVMissionDetailViewModel>();
            MasterViewModel = new PVMissionMasterViewModel();
        }

        public List<PVMissionDetailViewModel> DetailViewModel { get; set; }
        public PVMissionMasterViewModel MasterViewModel { get; set; }

        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(PVMissionViewModel viewModel)
        {
            var masterViewModel = viewModel.MasterViewModel;
            var DeatilViewModelList = viewModel.DetailViewModel;

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
            headDTO.TransactionDate = Convert.ToDateTime(masterViewModel.TransactionDate.ToString());
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

            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (var detailVMItem in DeatilViewModelList)
            {
                var detailDTO = new AccountTransactionDetailsDTO();

                detailDTO.AccountID = detailVMItem.AccountID;
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
                detailDTO.ReceivableID = detailVMItem.ReceivableID;
                
                detailDTO.PaidAmount = Convert.ToDecimal(detailVMItem.PaidAmount);
                detailDTO.PaymentDueDate = detailVMItem.PaymentDueDate != null ? Convert.ToDateTime(detailVMItem.PaymentDueDate) : DateTime.Now;
                detailDTO.ReturnAmount = Convert.ToDecimal(detailVMItem.ReturnAmount);
                detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);
                detailDTO.InvoiceNumber = detailVMItem.InvoiceNumber;
                detailDTO.CurrencyID = detailVMItem.CurrencyID;
                detailDTO.UnPaidAmount = Convert.ToDecimal(detailVMItem.UnPaidAmount);

                headDTO.AccountTransactionDetails.Add(detailDTO);

            }
            return headDTO;
        }

        public PVMissionViewModel ToPVMissionViewModel(AccountTransactionHeadDTO headDTO)
        {
            var pvMissionViewModel = new PVMissionViewModel();
            pvMissionViewModel.MasterViewModel = new PVMissionMasterViewModel();
            pvMissionViewModel.DetailViewModel = new List<PVMissionDetailViewModel>();
            pvMissionViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            pvMissionViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            pvMissionViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.ToString();
            pvMissionViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            pvMissionViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            pvMissionViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            pvMissionViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            pvMissionViewModel.MasterViewModel.Amount = Convert.ToDouble(headDTO.AdvanceAmount);
            pvMissionViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            pvMissionViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            pvMissionViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            pvMissionViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            pvMissionViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            pvMissionViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            pvMissionViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            pvMissionViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            pvMissionViewModel.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            pvMissionViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;

            if (headDTO.DocumentStatus != null)
                pvMissionViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                pvMissionViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                pvMissionViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                pvMissionViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                pvMissionViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                pvMissionViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                pvMissionViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                pvMissionViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new PVMissionDetailViewModel();

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
                detailViewModel.InvoiceNumber = detailDTOItem.InvoiceNumber;
                detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                detailViewModel.Remarks = detailDTOItem.Remarks;
                detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                detailViewModel.ReceivableID = detailDTOItem.ReceivableID;
                detailViewModel.CurrencyID = detailDTOItem.CurrencyID;
                detailViewModel.PaymentDueDate = detailDTOItem.PaymentDueDate.ToString();
                detailViewModel.UnPaidAmount = Convert.ToDouble(detailDTOItem.UnPaidAmount);
                detailViewModel.CurrencyName = detailDTOItem.Currency.Value;

                pvMissionViewModel.DetailViewModel.Add(detailViewModel);
            }
            return pvMissionViewModel;
        }


    }
}

