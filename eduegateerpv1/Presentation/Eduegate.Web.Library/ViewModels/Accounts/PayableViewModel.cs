using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class PayableViewModel : BaseMasterViewModel
    {
        public long PayableIID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<long> SerialNumber { get; set; }
        public string Description { get; set; }
        public Nullable<long> ReferencePayablesID { get; set; }
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
        public KeyValueDTO Payables1 { get; set; }
        public KeyValueDTO TransactionStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public string CurrencyName { get; set; }
        public List<TransactionHeadPayablesMapDTO> TransactionHeadPayablesMaps { get; set; }

        public List<PayableViewModel> ToVM(List<PayableDTO> dtos)
        {
            var vms = new List<PayableViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(ToVM(dto));
            }

            return vms;
        }

        public PayableViewModel ToVM(PayableDTO dto)
        {
            Mapper<PayableDTO, PayableViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<PayableDTO, PayableViewModel>.Map(dto);
        }
    }
}
