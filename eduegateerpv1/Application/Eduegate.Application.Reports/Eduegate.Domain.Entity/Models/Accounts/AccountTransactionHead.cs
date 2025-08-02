using Eduegate.Domain.Entity.Models.Inventory;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountTransactionHead
    {
        public AccountTransactionHead()
        {
            this.AccountTransactionDetails = new List<AccountTransactionDetail>();
            this.AccountTransactionHeadAccountMaps = new List<AccountTransactionHeadAccountMap>();
            this.AccountTaxTransactions = new List<AccountTaxTransaction>();
        }

        public long AccountTransactionHeadIID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string TransactionNumber { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public string Remarks { get; set; }
        public string Reference { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<decimal> AdvanceAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<long> ReceiptsID { get; set; }
        public Nullable<long> PaymentsID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        public long? BranchID { get; set; }
        public long? CompanyID { get; set; }
        public string ExternalReference1 { get; set; }
        public string ExternalReference2 { get; set; }
        public string ExternalReference3 { get; set; }
        //public decimal? ReferenceRate { get; set; }
        //public decimal? ReferenceQuatity { get; set; }

        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}
