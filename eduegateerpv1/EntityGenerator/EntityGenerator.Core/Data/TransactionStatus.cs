using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionStatuses", Schema = "inventory")]
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            AssetTransactionHeads = new HashSet<AssetTransactionHead>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public byte TransactionStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("TransactionStatus")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        [InverseProperty("ProcessingStatus")]
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        [InverseProperty("TransactionStatus")]
        public virtual ICollection<Payable> Payables { get; set; }
        [InverseProperty("TransactionStatus")]
        public virtual ICollection<Receivable> Receivables { get; set; }
        [InverseProperty("TransactionStatus")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
