using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class ExpenseEntryViewModel : BaseMasterViewModel
    {
        public ExpenseEntryViewModel()
        {
            DetailViewModel = new List<ExpenseEntryDetailViewModel>() { new ExpenseEntryDetailViewModel() };
            MasterViewModel = new ExpenseEntryHeadViewModel();
        }

        public List<ExpenseEntryDetailViewModel> DetailViewModel { get; set; }
        public ExpenseEntryHeadViewModel MasterViewModel { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AccountTransactionHeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExpenseEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var headDTO = dto as AccountTransactionHeadDTO;
            var vm = new ExpenseEntryViewModel();
            vm.MasterViewModel = new ExpenseEntryHeadViewModel();
            vm.DetailViewModel = new List<ExpenseEntryDetailViewModel>();
            vm.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            vm.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            vm.MasterViewModel.TransactionDate = headDTO.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.MasterViewModel.CostCenterID = headDTO.CostCenterID;
            vm.MasterViewModel.AccountID = headDTO.AccountID;
            vm.MasterViewModel.TransactionHeadIID = headDTO.AccountTransactionHeadIID;
            vm.MasterViewModel.AccountTransactionHeadIID = headDTO.AccountTransactionHeadIID;
            vm.MasterViewModel.AdvanceAmount = Convert.ToDouble(headDTO.AdvanceAmount);
            vm.MasterViewModel.Amount = Convert.ToDouble(headDTO.AdvanceAmount);
            vm.MasterViewModel.AmountPaid = Convert.ToDouble(headDTO.AmountPaid);
            vm.MasterViewModel.CurrencyID = headDTO.CurrencyID;
            vm.MasterViewModel.Remarks = headDTO.Remarks;
            vm.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            vm.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            vm.MasterViewModel.DetailAccountID = headDTO.DetailAccountID;
            vm.MasterViewModel.PaymentModeID = headDTO.PaymentModeID;
            vm.MasterViewModel.TimeStamps = headDTO.TimeStamps;
            vm.MasterViewModel.TransactionNo = headDTO.TransactionNumber;
            vm.MasterViewModel.TransactionStatusID = headDTO.TransactionStatusID;

            vm.MasterViewModel.Discount = headDTO.DiscountAmount;
            vm.MasterViewModel.DiscountPercentage = headDTO.DiscountPercentage;

            vm.MasterViewModel.CompanyID = headDTO.CompanyID;
            vm.MasterViewModel.Branch = headDTO.BranchID.ToString();

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

            if (headDTO.TaxDetails != null)
            {
                vm.MasterViewModel.TaxDetails = new Inventory.TaxDetailsViewModel();

                foreach (var tax in headDTO.TaxDetails)
                {
                    vm.MasterViewModel.TaxDetails.Taxes.Add(new Inventory.TaxViewModel()
                    {
                        //AccountID = tax.AccountID,
                        TaxAmount = tax.Amount,
                        TaxPercentage = tax.Percentage,
                        TaxID = tax.TaxID,
                        TaxName = tax.TaxName,
                        TaxTemplateID = tax.TaxTemplateID,
                        TaxTemplateItemID = tax.TaxTemplateItemID,
                        TaxTypeID = tax.TaxTypeID
                    });
                }
            }

            foreach (var detailDTOItem in headDTO.AccountTransactionDetails)
            {
                if (detailDTOItem.Account == null)
                {
                    continue;
                }

                var detailViewModel = new ExpenseEntryDetailViewModel();

                //detailViewModel.Amount = (decimal)detailDTOItem.Amount;
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
                detailViewModel.DebitAmount = Convert.ToDouble(detailDTOItem.Amount);
                detailViewModel.TaxTemplate = detailDTOItem.TaxTemplateID.ToString();
                detailViewModel.TaxTemplateID = detailDTOItem.TaxTemplateID;
                detailViewModel.TaxPercentage = detailDTOItem.TaxPercentage;

                if (detailDTOItem.CostCenter != null)
                    detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                if (detailDTOItem.Account != null)
                {
                    detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };
                    detailViewModel.Description = detailDTOItem.Account.Value;
                }

                vm.DetailViewModel.Add(detailViewModel);
            }
            
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            ExpenseEntryHeadViewModel masterViewModel = MasterViewModel;
            List<ExpenseEntryDetailViewModel> DeatilViewModelList = DetailViewModel;

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
            headDTO.BranchID = masterViewModel.Branch != null ? long.Parse(masterViewModel.Branch) : (long?)null;
            headDTO.CompanyID = masterViewModel.CompanyID == null || masterViewModel.CompanyID == 0 ? null : masterViewModel.CompanyID;

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

            headDTO.DiscountPercentage = masterViewModel.DiscountPercentage;
            headDTO.DiscountAmount = masterViewModel.Discount;

            headDTO.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();

            if (masterViewModel.TaxDetails != null &&  masterViewModel.TaxDetails.Taxes != null)
            {
                headDTO.TaxDetails = new List<TaxDetailsDTO>();

                foreach (var tax in masterViewModel.TaxDetails.Taxes)
                {
                    headDTO.TaxDetails.Add(new TaxDetailsDTO()
                    {
                        //AccountID = tax.AccountID,
                        Amount = tax.TaxAmount,
                        Percentage = tax.TaxPercentage,
                        TaxID = tax.TaxID,
                        TaxName = tax.TaxName,
                        TaxTemplateID = tax.TaxTemplateID,
                        TaxTemplateItemID = tax.TaxTemplateItemID,
                        TaxTypeID = tax.TaxTypeID
                    });
                }
            }

            foreach (var detailVMItem in DeatilViewModelList)
            {
                if (detailVMItem.Account == null)
                    continue;

                var detailDTO = new AccountTransactionDetailsDTO();
                detailDTO.Remarks = detailVMItem.Remarks;
                //detailDTO.AccountID = detailVMItem.AccountID;
                detailDTO.AccountTransactionDetailIID = detailVMItem.AccountTransactionDetailIID;
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.AccountTransactionHeadID = detailVMItem.AccountTransactionHeadID;
                //detailDTO.Amount = detailVMItem.Amount;
                // detailDTO.CostCenterID = detailVMItem.CostCenterID;
                detailDTO.ReferenceNumber = detailVMItem.ReferenceNumber;
                detailDTO.Remarks = detailVMItem.Remarks;
                detailDTO.TimeStamps = detailVMItem.TimeStamps;
                detailDTO.ReceivableID = detailVMItem.ReceivableID;

                detailDTO.PaidAmount = Convert.ToDecimal(detailVMItem.PaidAmount);
                // detailDTO.PaymentDueDate = Convert.ToDateTime(detailVMItem.PaymentDueDate);
                detailDTO.ReturnAmount = Convert.ToDecimal(detailVMItem.ReturnAmount);
                detailDTO.InvoiceAmount = Convert.ToDecimal(detailVMItem.InvoiceAmount);
                detailDTO.Amount = Convert.ToDecimal(detailVMItem.DebitAmount);

                detailDTO.AccountID = detailVMItem.Account != null ? !string.IsNullOrEmpty(detailVMItem.Account.Key) ? Convert.ToInt32(detailVMItem.Account.Key) : (int?)null : (int?)null;
                detailDTO.CostCenterID = detailVMItem.CostCenter != null ? !string.IsNullOrEmpty(detailVMItem.CostCenter.Key) ? Convert.ToInt32(detailVMItem.CostCenter.Key) : (int?)null : (int?)null;

                detailDTO.TaxTemplateID = string.IsNullOrEmpty(detailVMItem.TaxTemplate) ? (int?)null : int.Parse(detailVMItem.TaxTemplate);
                detailDTO.TaxPercentage = detailVMItem.TaxPercentage;

                headDTO.AccountTransactionDetails.Add(detailDTO);
            }

            return headDTO;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountTransactionHeadDTO>(jsonString);
        }
    }
}
