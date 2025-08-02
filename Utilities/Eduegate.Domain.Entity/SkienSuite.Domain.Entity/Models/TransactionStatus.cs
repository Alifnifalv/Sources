using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
            this.AssetTransactionHeads = new List<AssetTransactionHead>();
            this.TransactionHeads = new List<TransactionHead>();
        }

        public byte TransactionStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
