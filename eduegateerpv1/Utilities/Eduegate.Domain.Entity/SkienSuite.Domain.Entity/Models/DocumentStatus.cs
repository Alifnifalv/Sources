using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentStatus
    {
        public DocumentStatus()
        {
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
            this.DocumentReferenceStatusMaps = new List<DocumentReferenceStatusMap>();
        }

        public long DocumentStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
    }
}
