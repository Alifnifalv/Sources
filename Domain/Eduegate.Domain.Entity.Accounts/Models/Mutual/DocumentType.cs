using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Jobs;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("DocumentTypes", Schema = "mutual")]
    public partial class DocumentType
    {
        public DocumentType()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            AccountTransactions = new HashSet<AccountTransaction>();
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            AssetTransactionHeads = new HashSet<AssetTransactionHead>();
            JobEntryHeadDocumentTypes = new HashSet<JobEntryHead>();
            JobEntryHeadReferenceDocumentTypes = new HashSet<JobEntryHead>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
        }

        [Key]
        public int DocumentTypeID { get; set; }

        public int? ReferenceTypeID { get; set; }

        [StringLength(50)]
        public string TransactionTypeName { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string System { get; set; }

        [StringLength(50)]
        public string TransactionNoPrefix { get; set; }

        public long? LastTransactionNo { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? TransactionSequenceType { get; set; }

        public int? TaxTemplateID { get; set; }

        public bool? IgnoreInventoryCheck { get; set; }

        public long? WorkflowID { get; set; }

        public bool? IsExternal { get; set; }

        public virtual DocumentReferenceType ReferenceType { get; set; }

        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }

        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeadDocumentTypes { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeadReferenceDocumentTypes { get; set; }

        public virtual ICollection<Payable> Payables { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }
    }
}
