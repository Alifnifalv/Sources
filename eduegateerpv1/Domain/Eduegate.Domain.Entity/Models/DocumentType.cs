using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DocumentTypes", Schema = "mutual")]
    public partial class DocumentType
    {
        public DocumentType()
        {
            this.Tickets = new List<Ticket>();
            this.BranchDocumentTypeMaps = new List<BranchDocumentTypeMap>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.TransactionHeads = new List<TransactionHead>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.JobEntryHeads1 = new List<JobEntryHead>();
            this.AssetTransactionHeads = new List<AssetTransactionHead>();
            this.DocumentTypeType = new List<DocumentTypeType>();
            this.DocumentTypeType2 = new List<DocumentTypeType>();
            this.DocumentTypeTransactionNumbers = new List<DocumentTypeTransactionNumber>();
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.AccountTransactions = new List<AccountTransaction>();
            BidApprovalHeads = new HashSet<BidApprovalHead>();
        }

        [Key]
        public int DocumentTypeID { get; set; }
        public Nullable<int> ReferenceTypeID { get; set; }
        public string TransactionTypeName { get; set; }
        public string System { get; set; }
        public string TransactionNoPrefix { get; set; }
        public Nullable<long> LastTransactionNo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<int> TaxTemplateID { get; set; }
        //public byte[] TimeStamps { get; set; } 
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> TransactionSequenceType { get; set; }
        public Nullable<bool> IgnoreInventoryCheck { get; set; }
        public Nullable<long> WorkflowID { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads1 { get; set; }
        public virtual DocumentReferenceType DocumentReferenceType { get; set; }
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual ICollection<DocumentTypeType> DocumentTypeType { get; set; }
        public virtual ICollection<DocumentTypeType> DocumentTypeType2 { get; set; }
        public virtual ICollection<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual Workflows.Workflow Workflow { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual ICollection<BidApprovalHead> BidApprovalHeads { get; set; }

    }
}
    