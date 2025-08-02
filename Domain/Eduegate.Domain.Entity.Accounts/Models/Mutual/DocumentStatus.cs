using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("DocumentStatuses", Schema = "mutual")]
    public partial class DocumentStatus
    {
        public DocumentStatus()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            DocumentReferenceStatusMaps = new HashSet<DocumentReferenceStatusMap>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
        }

        [Key]
        public long DocumentStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        public bool? IsEditable { get; set; }

        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }

        public virtual ICollection<Payable> Payables { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }
    }
}