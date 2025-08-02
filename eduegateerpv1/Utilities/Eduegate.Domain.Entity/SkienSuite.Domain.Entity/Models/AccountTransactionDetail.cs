using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountTransactionDetail
    {
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public string ReferenceNumber { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public virtual Account Account { get; set; }
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
    }
}
