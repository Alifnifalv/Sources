using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class JVViewModel : BaseMasterViewModel
    {
        public JVViewModel()
        {
            DetailViewModel = new List<JVDetailViewModel>() { new JVDetailViewModel() };
            MasterViewModel = new JVMasterViewModel();
        }

        public List<JVDetailViewModel> DetailViewModel { get; set; }
        public JVMasterViewModel MasterViewModel { get; set; }



        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(JVViewModel viewModel)
        {
            JVMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<JVDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

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

            foreach (JVDetailViewModel detailVMItem in DeatilViewModelList)
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
                detailDTO.AccountTypeID =  detailVMItem.AccountTypes != null ? !string.IsNullOrEmpty(detailVMItem.AccountTypes.Key) ? Convert.ToInt32(detailVMItem.AccountTypes.Key) : (int?)null : (int?)null;


                if (detailVMItem.AccountTypes.Key == "1")
                {
                    detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                }
                else
                {
                    detailDTO.AccountID = detailVMItem.VendorCustomerAccounts != null ? !string.IsNullOrEmpty(detailVMItem.VendorCustomerAccounts.Key) ? Convert.ToInt32(detailVMItem.VendorCustomerAccounts.Key) : (int?)null : (int?)null;
                }
                
                detailDTO.CostCenterID = detailVMItem.CostCenter != null ? !string.IsNullOrEmpty(detailVMItem.CostCenter.Key) ? Convert.ToInt32(detailVMItem.CostCenter.Key) : (int?)null : (int?)null;


                if (detailVMItem.Credit > 0)
                {
                    detailDTO.Amount = Convert.ToDecimal(detailVMItem.Credit);
                }
                else
                {
                    detailDTO.Amount = -1 * Convert.ToDecimal(detailVMItem.Debit);
                }

                headDTO.AccountTransactionDetails.Add(detailDTO);
            }
            return headDTO;
        }

        public JVViewModel ToJVViewModel(AccountTransactionHeadDTO headDTO)
        {
            var jvViewModel = new JVViewModel();
            jvViewModel.MasterViewModel = new JVMasterViewModel();
            jvViewModel.DetailViewModel = new List<JVDetailViewModel>();
            jvViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            jvViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            jvViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.ToString();
            jvViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            jvViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            jvViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            jvViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            jvViewModel.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            jvViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            jvViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            jvViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            jvViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            jvViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            jvViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            jvViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            jvViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            jvViewModel.MasterViewModel.TransactionNumber = headDTO.TransactionNumber;
            jvViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;

            if (headDTO.DocumentStatus != null)
                jvViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                jvViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                jvViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                jvViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                jvViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                jvViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                jvViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                jvViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (AccountTransactionDetailsDTO detailDTOItem in headDTO.AccountTransactionDetails)
            {
                JVDetailViewModel detailViewModel = new JVDetailViewModel();

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
                //detailViewModel.DueDate = detailDTOItem.PaymentDue;
                detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                detailViewModel.Remarks = detailDTOItem.Remarks;
                detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                detailViewModel.ReceivableID = detailDTOItem.ReceivableID;

                if (detailDTOItem.CostCenter != null)
                    detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                if (detailDTOItem.AccountTypeID != null)
                {
                    if (detailDTOItem.AccountTypeID == 1)
                    {
                        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "1", Value = "ChartOfAccounts" };
                    }
                    else
                    {
                        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "2", Value = "VendorCustomerAccounts" };
                    }
                }

                if(detailDTOItem.AccountTypeID ==1)
                {
                    if (detailDTOItem.Account != null)
                        detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                }
                else
                {
                    if (detailDTOItem.Account != null)
                        detailViewModel.VendorCustomerAccounts = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                }

                

                if (detailViewModel.Amount > 0)
                {
                    detailViewModel.Credit = detailViewModel.Amount;
                    detailViewModel.CreditTotal =  (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                }
                else
                {
                    detailViewModel.Debit = -1 * detailViewModel.Amount;
                    detailViewModel.DebitTotal =  (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                }

                jvViewModel.DetailViewModel.Add(detailViewModel);
            }
            return jvViewModel;
        }


    }
}

