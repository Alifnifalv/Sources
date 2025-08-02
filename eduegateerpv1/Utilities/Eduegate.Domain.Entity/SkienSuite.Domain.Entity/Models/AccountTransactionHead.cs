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
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<decimal> AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}
