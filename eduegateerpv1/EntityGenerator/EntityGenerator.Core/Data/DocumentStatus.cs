using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentStatuses", Schema = "mutual")]
    public partial class DocumentStatus
    {
        public DocumentStatus()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            BidApprovalHeads = new HashSet<BidApprovalHead>();
            DocumentReferenceStatusMaps = new HashSet<DocumentReferenceStatusMap>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
        }

        [Key]
        public long DocumentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public bool? IsEditable { get; set; }

        [InverseProperty("DocumentStatus")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        [InverseProperty("DocumentStatus")]
        public virtual ICollection<BidApprovalHead> BidApprovalHeads { get; set; }
        [InverseProperty("DocumentStatus")]
        public virtual ICollection<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        [InverseProperty("DocumentStatus")]
        public virtual ICollection<Payable> Payables { get; set; }
        [InverseProperty("DocumentStatus")]
        public virtual ICollection<Receivable> Receivables { get; set; }
    }
}
