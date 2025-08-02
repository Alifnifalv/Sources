using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;

namespace Eduegate.Domain.Entity.Accounts.Models.Inventory
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
        }

        [Key]
        public byte TransactionStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }

        public virtual ICollection<Payable> Payables { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }
    }
}