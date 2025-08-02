using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            this.TransactionHeads = new List<TransactionHead>();
            this.OrderTrackings = new List<OrderTracking>();
            this.AssetTransactionHeads = new List<AssetTransactionHead>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
        }

        public byte TransactionStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<OrderTracking> OrderTrackings { get; set; }
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

    }
}
