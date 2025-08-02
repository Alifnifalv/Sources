using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Accounting;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
   public  class AccountTransactionDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long AccountTransactionDetailIID { get; set; }
        [DataMember]
        public Nullable<long> AccountTransactionHeadID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public string ReferenceNumber { get; set; }
        [DataMember]
        public decimal? ReferenceQuantity { get; set; }
        [DataMember]
        public decimal? ReferenceRate { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public decimal? TaxAmount { get; set; }
        [DataMember]
        public decimal? DiscountAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Nullable<int> CostCenterID { get; set; }
        [DataMember]
        public Nullable<long> DepartmentID { get; set; }

        [DataMember]
        public Nullable<int> CurrencyID { get; set; }

        //[DataMember]
        //public AccountDTO Account { get; set; }
        [DataMember]
        public decimal? InvoiceAmount { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public decimal? PaidAmount { get; set; }
        [DataMember]
        public Nullable<DateTime> PaymentDueDate { get; set; }
        [DataMember]
        public decimal? ReturnAmount { get; set; }
        [DataMember]
        public long? ReceivableID { get; set; }
        [DataMember]
        public long? PayableID { get; set; }

        [DataMember]
        public KeyValueDTO CostCenter { get; set; }

        [DataMember]
        public KeyValueDTO Department { get; set; }
        [DataMember]
        public decimal? ExchangeRate { get; set; }
        [DataMember]
        public KeyValueDTO Account { get; set; }
        [DataMember]
        public KeyValueDTO SKUID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUId { get; set; }
        [DataMember]
        public string SKU { get; set; }

        [DataMember]
        public Nullable<decimal> AvailableQuantity { get; set; }
        [DataMember]
        public Nullable<decimal> CurrentAvgCost { get; set; }
        [DataMember]
        public Nullable<decimal> NewAvgCost { get; set; }
        [DataMember]
        public string ProductSKUCode { get; set; }
        //[DataMember]
        //public KeyValueDTO AccountType { get; set; }
        [DataMember]
        public Nullable<int> AccountTypeID { get; set; }
        [DataMember]
        public decimal? UnPaidAmount { get; set; }
        [DataMember]
        public KeyValueDTO Currency { get; set; }
        [DataMember]
        public string JobMissionNumber { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? TaxTemplateID { get; set; }
        [DataMember]
        public decimal? TaxPercentage { get; set; }
        [DataMember]
        public long? ReferenceReceiptID { get; set; }
        [DataMember]
        public long? ReferencePaymentID { get; set; }
        [DataMember]
        public string ExternalReference1 { get; set; }
        [DataMember]
        public string ExternalReference2 { get; set; }
        [DataMember]
        public string ExternalReference3 { get; set; }

        [DataMember]
        public long? AccountGroupID { get; set; }

        [DataMember]
        public KeyValueDTO AccountGroup { get; set; }

        [DataMember]
        public long? SubLedgerID { get; set; }

        [DataMember]
        public KeyValueDTO SubLedger { get; set; }

        [DataMember]
        public int? BudgetID { get; set; }

        [DataMember]
        public KeyValueDTO Budget { get; set; }
    }
}
