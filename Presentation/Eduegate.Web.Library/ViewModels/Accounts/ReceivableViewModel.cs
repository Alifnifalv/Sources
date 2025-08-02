using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class ReceivableViewModel : BaseMasterViewModel
    {
        public long ReceivableIID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<long> SerialNumber { get; set; }
        public string Description { get; set; }
        public Nullable<long> ReferenceReceivablesID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<System.DateTime> AccountPostingDate { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public KeyValueDTO Account { get; set; }
        public KeyValueDTO Currency { get; set; }
        public KeyValueDTO DocumentStatus { get; set; }
        public KeyValueDTO Receivable1 { get; set; }
        public KeyValueDTO TransactionStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public string CurrencyName { get; set; }
        public List<TransactionHeadReceivablesMapDTO> TransactionHeadReceivablesMaps { get; set; }

        public List<ReceivableViewModel> ToVM(List<ReceivableDTO> dtos)
        {
            var vms = new List<ReceivableViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(ToVM(dto));
            }

            return vms;
        }

        public ReceivableViewModel ToVM(ReceivableDTO dto)
        {
            Mapper<ReceivableDTO, ReceivableViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<ReceivableDTO, ReceivableViewModel>.Map(dto);
        }
    }
}
