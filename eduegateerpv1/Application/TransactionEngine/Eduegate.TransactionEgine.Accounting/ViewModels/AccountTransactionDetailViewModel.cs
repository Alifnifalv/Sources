using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class AccountTransactionDetailViewModel
    {
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal ReferenceQuantity { get; set; }
        public decimal ReferenceRate { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public Nullable<int> CurrencyID { get; set; }

        //[DataMember]
        //public AccountDTO Account { get; set; }
        public double? InvoiceAmount { get; set; }
        public string InvoiceNumber { get; set; }
        public double? PaidAmount { get; set; }
        public Nullable<DateTime> PaymentDueDate { get; set; }
        public double? ReturnAmount { get; set; }
        public long? ReceivableID { get; set; }
        public long? PayableID { get; set; }

        public KeyValueViewModel CostCenter { get; set; }
        public double? ExchangeRate { get; set; }
        public KeyValueViewModel Account { get; set; }
        public KeyValueViewModel SKUID { get; set; }
        public Nullable<long> ProductSKUId { get; set; }
        public string SKU { get; set; }

        public Nullable<decimal> AvailableQuantity { get; set; }
        public Nullable<decimal> CurrentAvgCost { get; set; }
        public Nullable<decimal> NewAvgCost { get; set; }
        public string ProductSKUCode { get; set; }
        //public KeyValueDTO AccountType { get; set; }
        public Nullable<int> AccountTypeID { get; set; }
        public double? UnPaidAmount { get; set; }
        public KeyValueViewModel Currency { get; set; }
        public string JobMissionNumber { get; set; }
        public string Description { get; set; }
        public int? TaxTemplateID { get; set; }
        public decimal? TaxPercentage { get; set; }
        public long? ReferenceReceiptID { get; set; }
        public long? ReferencePaymentID { get; set; }
        public string ExternalReference1 { get; set; }
        public string ExternalReference2 { get; set; }
        public string ExternalReference3 { get; set; }

        public static AccountTransactionDetailViewModel ToVM(AccountTransactionDetailsDTO dto)
        {
            Mapper<AccountTransactionDetailsDTO, AccountTransactionDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<AccountTransactionDetailsDTO, AccountTransactionDetailViewModel>.Map(dto);
        }

        public static AccountTransactionDetailsDTO ToDTO(AccountTransactionDetailViewModel dto)
        {
            Mapper<AccountTransactionDetailViewModel, AccountTransactionDetailsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            return Mapper<AccountTransactionDetailViewModel, AccountTransactionDetailsDTO>.Map(dto);
        }
    }
}
