using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Payable
    {
        public Payable()
        {
            this.JobsEntryHeadPayableMaps = new List<JobsEntryHeadPayableMap>();
            this.Payables1 = new List<Payable>();
            this.TransactionHeadPayablesMaps = new List<TransactionHeadPayablesMap>();
        }

        public long PayableIID { get; set; }
        public int? DocumentTypeID { get; set; }
        public string TransactionNumber { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<long> SerialNumber { get; set; }
        public string Description { get; set; }
        public Nullable<long> ReferencePayablesID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<System.DateTime> AccountPostingDate { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<bool> DebitOrCredit { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual ICollection<Payable> Payables1 { get; set; }
        public virtual Payable Payable1 { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
    }
}
