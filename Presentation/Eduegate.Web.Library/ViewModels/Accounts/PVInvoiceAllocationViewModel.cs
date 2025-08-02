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
    public class PVInvoiceAllocationViewModel : BaseMasterViewModel
    {
        public PVInvoiceAllocationViewModel()
        {
            DetailViewModel = new List<PVInvoiceAllocationDetailViewModel>() { new PVInvoiceAllocationDetailViewModel() };
            MasterViewModel = new PVInvoiceAllocationMasterViewModel();
        }

        public List<PVInvoiceAllocationDetailViewModel> DetailViewModel { get; set; }
        public PVInvoiceAllocationMasterViewModel MasterViewModel { get; set; }

        public override string AsDTOString(BaseMasterDTO dto)
        {
            return base.AsDTOString(dto);
        }

        public override BaseMasterDTO ToDTO()
        {
            return null;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountTransactionHeadDTO>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            return null;
        }

        public static PVInvoiceAllocationViewModel ToVM(List<PayableDTO> payments, List<PayableDTO> pendingPayments)
        {
            var vm = new PVInvoiceAllocationViewModel();
            vm.MasterViewModel = new PVInvoiceAllocationMasterViewModel()
            {
                DocumentType = payments[0].DocumentTypeName,
                TransactionNumber = payments[0].TransactionNumber,
                CustomerName = payments[0].Account.Value,
                Amount = payments[0].Amount.Value,
            };

            vm.DetailViewModel = new List<PVInvoiceAllocationDetailViewModel>();

            foreach (var pendingReceipt in pendingPayments)
            {
                vm.DetailViewModel.Add(new PVInvoiceAllocationDetailViewModel()
                {
                    Amount = double.Parse(pendingReceipt.Amount.ToString()),
                    AccountID = pendingReceipt.AccountID,
                    InvoiceAmount = double.Parse(pendingReceipt.Amount.ToString()),
                    PaidAmount = double.Parse(pendingReceipt.PaidAmount.ToString()),
                    InvoiceNumber = pendingReceipt.InvoiceNumber,
                    ReferenceNumber = pendingReceipt.Description,
                    //CurrencyName = pendingReceipt.CurrencyName,
                    //ExchangeRate = double.Parse(pendingReceipt.ExchangeRate.ToString()),
                });
            }

            return vm;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PVInvoiceAllocationViewModel>(jsonString);
        }
    }
}
