using Eduegate.Framework.Enums;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class AccountTransactionHeadViewModel
    {
        public long AccountTransactionHeadIID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string TransactionNumber { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<long> DetailAccountID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? Amount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public List<AccountTransactionDetailViewModel> AccountTransactionDetails { get; set; }
        public KeyValueViewModel TransactionStatus { get; set; }
        public KeyValueViewModel DocumentStatus { get; set; }
        public KeyValueViewModel DocumentType { get; set; }
        public KeyValueViewModel PaymentModes { get; set; }
        public KeyValueViewModel Currency { get; set; }
        public KeyValueViewModel CostCenter { get; set; }
        public KeyValueViewModel Account { get; set; }
        public KeyValueViewModel DetailAccount { get; set; }
        public List<TaxDetailViewModel> TaxDetails { get; set; }

        public static AccountTransactionHeadViewModel ToVM(AccountTransactionHeadDTO dto)
        {
            Mapper<AccountTransactionHeadDTO, AccountTransactionHeadViewModel>.CreateMap();
            Mapper<AccountTransactionDetailsDTO, AccountTransactionDetailViewModel>.CreateMap();
            Mapper<TaxDetailsDTO, TaxDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<AccountTransactionHeadDTO, AccountTransactionHeadViewModel>.Map(dto);
        }

        public static AccountTransactionHeadDTO ToDTO(AccountTransactionHeadViewModel dto)
        {
            Mapper<AccountTransactionHeadViewModel, AccountTransactionHeadDTO>.CreateMap();
            Mapper<AccountTransactionDetailViewModel, AccountTransactionDetailsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            return Mapper<AccountTransactionHeadViewModel, AccountTransactionHeadDTO>.Map(dto);
        }
    }
}
