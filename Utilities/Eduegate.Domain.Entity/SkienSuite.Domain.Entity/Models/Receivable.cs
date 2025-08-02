using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Receivable
    {
        public Receivable()
        {
            this.JobsEntryHeadReceivableMaps = new List<JobsEntryHeadReceivableMap>();
            this.Receivables1 = new List<Receivable>();
        }

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
        public Nullable<System.DateTime> AccountPostingDate { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual ICollection<Receivable> Receivables1 { get; set; }
        public virtual Receivable Receivable1 { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}
