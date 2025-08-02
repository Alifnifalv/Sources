using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class RVInvoiceAllocationViewModel : BaseMasterViewModel
    {
        public RVInvoiceAllocationViewModel()
        {
            DetailViewModel = new List<RVInvoiceAllocationDetailViewModel>() { new RVInvoiceAllocationDetailViewModel() };
            MasterViewModel = new RVInvoiceAllocationMasterViewModel();
        }

        public List<RVInvoiceAllocationDetailViewModel> DetailViewModel { get; set; }
        public RVInvoiceAllocationMasterViewModel MasterViewModel { get; set; }

        public override string AsDTOString(BaseMasterDTO dto)
        {
            return base.AsDTOString(dto);
        }

        public override BaseMasterDTO ToDTO()
        {
            var dto = new RVInvoiceAllocationDTO();
            decimal amountTotalPaid = 0;

            foreach (var detail in this.DetailViewModel)
            {
                if (detail.Amount.HasValue && detail.Amount.Value != 0)
                {
                    dto.Receipts.Add(new ReceivableDTO()
                    {
                        ReceivableIID = detail.ReceivableIID,
                        Amount = decimal.Parse(detail.Amount.Value.ToString()),
                        Description = detail.ReferenceNumber,
                        DocumentReferenceTypeID = detail.DocumentReferenceTypeID,
                    });

                    amountTotalPaid += decimal.Parse(detail.Amount.Value.ToString());
                }
            }

            dto.Receipts.Add(new ReceivableDTO()
            {
                ReceivableIID = MasterViewModel.ReceivableIID,
                Amount = MasterViewModel.Amount,
                Description = MasterViewModel.Remarks,
                DocumentReferenceTypeID = MasterViewModel.DocumentReferenceTypeID,
            });

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountTransactionHeadDTO>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var headDto = dto as AccountTransactionHeadDTO;
            var RVInvoiceAllocationVM = new RVInvoiceAllocationViewModel();
            RVInvoiceAllocationVM.MasterViewModel = new RVInvoiceAllocationMasterViewModel()
            {
            };

            RVInvoiceAllocationVM.DetailViewModel = new List<RVInvoiceAllocationDetailViewModel>();

            foreach (var detailDTO in headDto.AccountTransactionDetails)
            {
                RVInvoiceAllocationVM.DetailViewModel.Add(new RVInvoiceAllocationDetailViewModel()
                {
                });
            }

            return RVInvoiceAllocationVM;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RVInvoiceAllocationViewModel>(jsonString);
        }

        public static RVInvoiceAllocationViewModel ToVM(ReceivableDTO receipt, List<ReceivableDTO> pendingReceipts)
        {
            return ToVM(new List<ReceivableDTO>() { receipt }, pendingReceipts);
        }

        public static RVInvoiceAllocationViewModel ToVM(List<ReceivableDTO> receipts, List<ReceivableDTO> pendingReceipts)
        {
            var vm = new RVInvoiceAllocationViewModel();
            vm.MasterViewModel = new RVInvoiceAllocationMasterViewModel()
            {
                DocumentType = receipts[0].DocumentTypeName, 
                DocumentReferenceTypeID = receipts[0].DocumentReferenceTypeID,
                TransactionNumber = receipts[0].TransactionNumber,
                CustomerName = receipts[0].Account.Value,
                //DetailAccount =new KeyValueViewModel() {  Key = receipts[0].Account.Key, Value= receipts[0].Account.Value},
                Amount = receipts[0].Amount.Value,
                ReceivableIID = receipts[0].ReceivableIID
            };

            vm.DetailViewModel = new List<RVInvoiceAllocationDetailViewModel>();

            if (pendingReceipts != null)
            {
                foreach (var pendingReceipt in pendingReceipts)
                {
                    var paidAmount = pendingReceipt.PaidAmount.HasValue ? double.Parse(pendingReceipt.PaidAmount.ToString()) : 0;
                    vm.DetailViewModel.Add(new RVInvoiceAllocationDetailViewModel()
                    {
                        ReceivableIID = pendingReceipt.ReceivableIID,
                        Amount = 0,
                        AccountID = pendingReceipt.AccountID,
                        InvoiceAmount = double.Parse(pendingReceipt.Amount.ToString()),
                        PaidAmount = paidAmount,
                        InvoiceNumber = pendingReceipt.TransactionNumber,
                        ReferenceNumber = pendingReceipt.Description,
                        DocumentTypeName = pendingReceipt.DocumentTypeName,
                        //CurrencyName = pendingReceipt.CurrencyName,
                        //ExchangeRate = double.Parse(pendingReceipt.ExchangeRate.ToString()),
                        BalanceAmount = double.Parse(pendingReceipt.Amount.ToString()) - paidAmount,
                        DocumentReferenceTypeID = pendingReceipt.DocumentReferenceTypeID,
                    });
                }
            }

            return vm;
        }
    }
}
