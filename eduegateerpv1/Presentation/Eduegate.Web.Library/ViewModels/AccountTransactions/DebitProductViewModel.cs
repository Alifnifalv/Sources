using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class DebitProductViewModel : BaseMasterViewModel
    {
        public DebitProductViewModel()
        {
            DetailViewModel = new List<DebitProductDetailViewModel>();
            MasterViewModel = new DebitProductMasterViewModel();
        }

        public List<DebitProductDetailViewModel> DetailViewModel { get; set; }
        public DebitProductMasterViewModel MasterViewModel { get; set; }


        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(DebitProductViewModel viewModel)
        {
            DebitProductMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<DebitProductDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

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

            foreach (DebitProductDetailViewModel detailVMItem in DeatilViewModelList)
            {
                AccountTransactionDetailsDTO detailDTO = new AccountTransactionDetailsDTO();

                detailDTO.ProductSKUId = detailVMItem.SKUID != null ? !string.IsNullOrEmpty(detailVMItem.SKUID.Key) ? Convert.ToInt32(detailVMItem.SKUID.Key) : (long?)null : (long?)null;
                detailDTO.AccountID = headDTO.AccountID;
                detailDTO.AccountTransactionDetailIID = detailVMItem.AccountTransactionDetailIID;
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.AccountTransactionHeadID = detailVMItem.AccountTransactionHeadID;
                detailDTO.Amount = Convert.ToDecimal(detailVMItem.Amount);
                detailDTO.CostCenterID = detailVMItem.CostCenterID;
                detailDTO.AvailableQuantity = detailVMItem.AvailableQuantity;
                detailDTO.Remarks = detailVMItem.Remarks;
                detailDTO.TimeStamps = detailVMItem.TimeStamps;
                detailDTO.CurrentAvgCost = detailVMItem.CurrentAvgCost;
                detailDTO.NewAvgCost = detailVMItem.NewAvgCost;
                detailDTO.ProductSKUCode = detailVMItem.ProductSKUCode;
                headDTO.AccountTransactionDetails.Add(detailDTO);
            }
            return headDTO;
        }

        public DebitProductViewModel ToDebitProductViewModel(AccountTransactionHeadDTO headDTO)
        {
            var debitproductViewModel = new DebitProductViewModel();
           debitproductViewModel.MasterViewModel = new DebitProductMasterViewModel();
           debitproductViewModel.DetailViewModel = new List<DebitProductDetailViewModel>();
           debitproductViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
           debitproductViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
           debitproductViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.ToString();
           debitproductViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
           debitproductViewModel.MasterViewModel.AccountID = headDTO.AccountID;
           debitproductViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
           debitproductViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
           debitproductViewModel.MasterViewModel.Amount = Convert.ToDouble(headDTO.AdvanceAmount);
           debitproductViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
           debitproductViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
           debitproductViewModel.MasterViewModel.Remarks = headDTO.Remarks;
           debitproductViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
           debitproductViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
           debitproductViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
           debitproductViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
           debitproductViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
           debitproductViewModel.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            debitproductViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;

            if (headDTO.DocumentStatus != null)
                debitproductViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                debitproductViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                debitproductViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                debitproductViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                debitproductViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                debitproductViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                debitproductViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                debitproductViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (AccountTransactionDetailsDTO detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new DebitProductDetailViewModel();

                if (detailDTOItem.SKUID != null)
                    detailViewModel.SKUID = new KeyValueViewModel() { Key = detailDTOItem.SKUID.Key, Value = detailDTOItem.SKUID.Value };
            
                detailViewModel.Amount = Convert.ToDouble(detailDTOItem.Amount);
                detailViewModel.AccountID = detailDTOItem.AccountID;
                detailViewModel.AccountTransactionDetailIID = detailDTOItem.AccountTransactionDetailIID;
                detailViewModel.CostCenterID = detailDTOItem.CostCenterID;
                detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                detailViewModel.AvailableQuantity = detailDTOItem.AvailableQuantity;
                detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                detailViewModel.CurrentAvgCost = detailDTOItem.CurrentAvgCost;
                detailViewModel.Remarks = detailDTOItem.Remarks;
                detailViewModel.NewAvgCost = detailDTOItem.NewAvgCost;
                detailViewModel.ProductSKUCode = detailDTOItem.ProductSKUCode;
                debitproductViewModel.DetailViewModel.Add(detailViewModel);
            }
            return debitproductViewModel;
        }


    }
}

