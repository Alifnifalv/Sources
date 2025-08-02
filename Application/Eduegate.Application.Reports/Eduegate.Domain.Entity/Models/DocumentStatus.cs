using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentStatus
    {
        public DocumentStatus()
        {
            this.DocumentReferenceStatusMaps = new List<DocumentReferenceStatusMap>();
            this.AssetTransactionHeads = new List<AssetTransactionHead>();
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
        }

        public long DocumentStatusID { get; set; }
        public string StatusName { get; set; }
        public Nullable<bool> IsEditable { get; set; }
        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }

    }
}
