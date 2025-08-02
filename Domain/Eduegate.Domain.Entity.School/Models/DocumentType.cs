namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DocumentTypes", Schema = "mutual")]
    public partial class DocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentType()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            //AccountTransactions = new HashSet<AccountTransaction>();
            //Payables = new HashSet<Payable>();
            //Receivables = new HashSet<Receivable>();
            //AssetTransactionHeads = new HashSet<AssetTransactionHead>();
            //Tickets = new HashSet<Ticket>();
            //BranchDocumentTypeMaps = new HashSet<BranchDocumentTypeMap>();
            //InvetoryTransactions = new HashSet<InvetoryTransaction>();
            //TransactionHeads = new HashSet<TransactionHead>();
            //JobEntryHeads = new HashSet<JobEntryHead>();
            //JobEntryHeads1 = new HashSet<JobEntryHead>();
            //DocumentTypeTransactionNumbers = new HashSet<DocumentTypeTransactionNumber>();
            //DocumentTypeTypeMaps = new HashSet<DocumentTypeTypeMap>();
            //DocumentTypeTypeMaps1 = new HashSet<DocumentTypeTypeMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentTypeID { get; set; }

        public int? ReferenceTypeID { get; set; }

        [StringLength(50)]
        public string TransactionTypeName { get; set; }

        [StringLength(20)]
        public string System { get; set; }

        [StringLength(50)]
        public string TransactionNoPrefix { get; set; }

        public long? LastTransactionNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? TransactionSequenceType { get; set; }

        public int? TaxTemplateID { get; set; }

        public bool? IgnoreInventoryCheck { get; set; }

        public long? WorkflowID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Payable> Payables { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Receivable> Receivables { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Tickets { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        //public virtual TaxTemplate TaxTemplate { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<JobEntryHead> JobEntryHeads1 { get; set; }

        //public virtual DocumentReferenceType DocumentReferenceType { get; set; }

        public virtual Workflow Workflow { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DocumentTypeTypeMap> DocumentTypeTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DocumentTypeTypeMap> DocumentTypeTypeMaps1 { get; set; }
    }
}
