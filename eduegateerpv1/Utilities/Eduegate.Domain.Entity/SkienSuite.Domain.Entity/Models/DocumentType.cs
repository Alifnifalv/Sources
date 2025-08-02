using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.AssetTransactionHeads = new List<AssetTransactionHead>();
            this.BranchDocumentTypeMaps = new List<BranchDocumentTypeMap>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.TransactionHeads = new List<TransactionHead>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.JobEntryHeads1 = new List<JobEntryHead>();
            this.DocumentTypeTransactionNumbers = new List<DocumentTypeTransactionNumber>();
            this.DocumentTypeTypeMaps = new List<DocumentTypeTypeMap>();
            this.DocumentTypeTypeMaps1 = new List<DocumentTypeTypeMap>();
        }

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
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> TransactionSequenceType { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads1 { get; set; }
        public virtual DocumentReferenceType DocumentReferenceType { get; set; }
        public virtual ICollection<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }
        public virtual ICollection<DocumentTypeTypeMap> DocumentTypeTypeMaps { get; set; }
        public virtual ICollection<DocumentTypeTypeMap> DocumentTypeTypeMaps1 { get; set; }
    }
}
