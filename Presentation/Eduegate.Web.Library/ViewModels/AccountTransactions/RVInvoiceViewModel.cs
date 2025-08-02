using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class RVInvoiceViewModel : BaseMasterViewModel
    {
        public RVInvoiceViewModel()
        {
            DetailViewModel = new List<RVInvoiceDetailViewModel>() { new RVInvoiceDetailViewModel() };
            MasterViewModel = new RVInvoiceMasterViewModel();
        }

        public List<RVInvoiceDetailViewModel> DetailViewModel { get; set; }
        public RVInvoiceMasterViewModel MasterViewModel { get; set; }


        public AccountTransactionHeadDTO ToAccountTransactionHeadDTO(RVInvoiceViewModel viewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            RVInvoiceMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<RVInvoiceDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            AccountTransactionHeadDTO headDTO = new AccountTransactionHeadDTO();

            headDTO.DocumentStatusID = masterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(masterViewModel.DocumentStatus.Key) ? Convert.ToInt32(masterViewModel.DocumentStatus.Key) : (long?)null : (long?)null;
            headDTO.DocumentTypeID = masterViewModel.DocumentType != null ? !string.IsNullOrEmpty(masterViewModel.DocumentType.Key) ? Convert.ToInt32(masterViewModel.DocumentType.Key) : (int?)null : (int?)null;
            //headDTO.AccountID = masterViewModel.Account != null ? !string.IsNullOrEmpty(masterViewModel.Account.Key) ? Convert.ToInt32(masterViewModel.Account.Key) : (int?)null : (int?)null;
            headDTO.AccountID = masterViewModel.DetailAccount != null ? !string.IsNullOrEmpty(masterViewModel.DetailAccount.Key) ? Convert.ToInt32(masterViewModel.DetailAccount.Key) : (int?)null : (int?)null;

            headDTO.PaymentModeID = masterViewModel.PaymentModes != null ? !string.IsNullOrEmpty(masterViewModel.PaymentModes.Key) ? Convert.ToInt16(masterViewModel.PaymentModes.Key) : (short?)null : (short?)null;
            headDTO.CurrencyID = masterViewModel.Currency != null ? !string.IsNullOrEmpty(masterViewModel.Currency.Key) ? Convert.ToInt32(masterViewModel.Currency.Key) : (int?)null : (int?)null;
            headDTO.CostCenterID = masterViewModel.CostCenter != null ? !string.IsNullOrEmpty(masterViewModel.CostCenter.Key) ? Convert.ToInt32(masterViewModel.CostCenter.Key) : (int?)null : (int?)null;
            headDTO.DetailAccountID = masterViewModel.DetailAccount != null ? !string.IsNullOrEmpty(masterViewModel.DetailAccount.Key) ? Convert.ToInt32(masterViewModel.DetailAccount.Key) : (int?)null : (int?)null;
            headDTO.TransactionStatusID = masterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(masterViewModel.TransactionStatus.Key) ? Convert.ToByte(masterViewModel.TransactionStatus.Key) : (Byte?)null : (Byte?)null;

            headDTO.PaymentModes = masterViewModel.PaymentModes != null ? new KeyValueDTO() { Key = masterViewModel.PaymentModes.Key, Value = masterViewModel.PaymentModes.Value } : null;
            headDTO.ChequeNumber = masterViewModel.ChequeNumber;
            headDTO.ChequeDate = masterViewModel.ChequeDateString == null ? (DateTime?)null : DateTime.ParseExact(masterViewModel.ChequeDateString, dateFormat, CultureInfo.InvariantCulture);

            headDTO.BranchID = long.Parse(masterViewModel.Branch);
            headDTO.CreatedBy = masterViewModel.CreatedBy;
            headDTO.CreatedDate = masterViewModel.CreatedDate;
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
            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            foreach (var detailVMItem in DeatilViewModelList)
            {
                if (detailVMItem.IsRowSelected ==true && detailVMItem.Amount != 0 && !string.IsNullOrEmpty(detailVMItem.InvoiceNumber))
                {
                    AccountTransactionDetailsDTO detailDTO = new AccountTransactionDetailsDTO();
                    detailDTO.AccountID = headDTO.DetailAccountID;
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
                    //detailDTO.ReturnAmount = detailVMItem.ReturnAmount;
                    detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);
                    detailDTO.InvoiceNumber = detailVMItem.InvoiceNumber;
                    detailDTO.ReceivableID = detailVMItem.ReceivableID;
                    detailDTO.ReferencePaymentID = detailVMItem.ReferencePaymentID;
                    detailDTO.ReferenceReceiptID = detailVMItem.ReferenceReceiptID;

                    //detailDTO.ExchangeRate = Convert.ToDecimal(detailVMItem.ExchangeRate);
                    detailDTO.CurrencyID = detailVMItem.CurrencyID;
                    //detailDTO.UnPaidAmount = detailVMItem.UnPaidAmount;
                    headDTO.AccountTransactionDetails.Add(detailDTO);
                }
            }
            return headDTO;
        }

        public List<RVInvoiceViewModel> ToRVInvoiceViewModel(List<AccountTransactionHeadDTO> headDTOs)
        {
            var dtos = new List<RVInvoiceViewModel>();

            foreach (var dto in headDTOs)
            {
                dtos.Add(ToRVInvoiceViewModel(dto));
            }

            return dtos;
        }

        public RVInvoiceViewModel ToRVInvoiceViewModel(AccountTransactionHeadDTO headDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var rvInvoiceViewModel = new RVInvoiceViewModel();
            rvInvoiceViewModel.MasterViewModel = new RVInvoiceMasterViewModel();
            rvInvoiceViewModel.DetailViewModel = new List<RVInvoiceDetailViewModel>();

            rvInvoiceViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            rvInvoiceViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            rvInvoiceViewModel.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            rvInvoiceViewModel.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            rvInvoiceViewModel.MasterViewModel.AccountID = headDTO.AccountID;
            rvInvoiceViewModel.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            rvInvoiceViewModel.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            rvInvoiceViewModel.MasterViewModel.Amount = headDTO.AdvanceAmount != null ? Convert.ToDecimal(headDTO.AdvanceAmount) : 0;
            rvInvoiceViewModel.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            rvInvoiceViewModel.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            rvInvoiceViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            rvInvoiceViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            rvInvoiceViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            rvInvoiceViewModel.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            rvInvoiceViewModel.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            rvInvoiceViewModel.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            rvInvoiceViewModel.MasterViewModel.TransactionNo = headDTO.TransactionNumber;
            rvInvoiceViewModel.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;
            rvInvoiceViewModel.MasterViewModel.ExchangeRate = Convert.ToDouble(headDTO.ExchangeRate);
            rvInvoiceViewModel.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;
            rvInvoiceViewModel.MasterViewModel.ChequeNumber = headDTO.ChequeNumber;
            rvInvoiceViewModel.MasterViewModel.ChequeDateString = headDTO.ChequeDate.HasValue ? headDTO.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            rvInvoiceViewModel.MasterViewModel.Branch = headDTO.BranchID.ToString();
            rvInvoiceViewModel.MasterViewModel.ErrorCode = headDTO.ErrorCode;
            rvInvoiceViewModel.MasterViewModel.IsError = headDTO.IsError;
            rvInvoiceViewModel.ErrorCode = headDTO.ErrorCode;
            rvInvoiceViewModel.IsError = headDTO.IsError;
            if (headDTO.DocumentStatus != null)
                rvInvoiceViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                rvInvoiceViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                rvInvoiceViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            if (headDTO.Account != null)
                rvInvoiceViewModel.MasterViewModel.Account = new KeyValueViewModel() { Key = headDTO.Account.Key, Value = headDTO.Account.Value };
            if (headDTO.CostCenter != null)
                rvInvoiceViewModel.MasterViewModel.CostCenter = new KeyValueViewModel() { Key = headDTO.CostCenter.Key, Value = headDTO.CostCenter.Value };
            if (headDTO.Currency != null)
                rvInvoiceViewModel.MasterViewModel.Currency = new KeyValueViewModel() { Key = headDTO.Currency.Key, Value = headDTO.Currency.Value };
            if (headDTO.DetailAccount != null)
                rvInvoiceViewModel.MasterViewModel.DetailAccount = new KeyValueViewModel() { Key = headDTO.DetailAccount.Key, Value = headDTO.DetailAccount.Value };
            if (headDTO.PaymentModes != null)
                rvInvoiceViewModel.MasterViewModel.PaymentModes = new KeyValueViewModel() { Key = headDTO.PaymentModes.Key, Value = headDTO.PaymentModes.Value };

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                var detailViewModel = new RVInvoiceDetailViewModel();
                detailViewModel.IsRowSelected =true; 
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
                detailViewModel.ReceivableID = detailDTOItem.ReceivableID;
                detailViewModel.ReferencePaymentID = detailDTOItem.ReferencePaymentID;
                detailViewModel.ReferenceReceiptID = detailDTOItem.ReferenceReceiptID;

                //detailViewModel.ReturnAmount = detailDTOItem.ReturnAmount;
                //detailViewModel.UnPaidAmount = detailDTOItem.UnPaidAmount;

                detailViewModel.CurrencyID = detailDTOItem.CurrencyID;
                //if (detailDTOItem.Currency != null)
                //{
                //    detailViewModel.CurrencyName = detailDTOItem.Currency.Value;
                //}

                //detailViewModel.ExchangeRate = Convert.ToDouble(detailDTOItem.ExchangeRate);

                rvInvoiceViewModel.DetailViewModel.Add(detailViewModel);
            }
            return rvInvoiceViewModel;
        }


    }
}

