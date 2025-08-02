using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
            BidApprovalHeads = new HashSet<BidApprovalHead>();
            BranchDocumentTypeMaps = new HashSet<BranchDocumentTypeMap>();
            DocumentDepartmentMaps = new HashSet<DocumentDepartmentMap>();
            DocumentTypeSettings = new HashSet<DocumentTypeSetting>();
            DocumentTypeTransactionNumbers = new HashSet<DocumentTypeTransactionNumber>();
            DocumentTypeTypeMapDocumentTypeMaps = new HashSet<DocumentTypeTypeMap>();
            DocumentTypeTypeMapDocumentTypes = new HashSet<DocumentTypeTypeMap>();
            EmployeeESBProvisionHeads = new HashSet<EmployeeESBProvisionHead>();
            EmployeeLSProvisionHeads = new HashSet<EmployeeLSProvisionHead>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            JobEntryHeadDocumentTypes = new HashSet<JobEntryHead>();
            JobEntryHeadReferenceDocumentTypes = new HashSet<JobEntryHead>();
            LoanTypes = new HashSet<LoanType>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
            Tickets = new HashSet<Ticket>();
            TransactionHeads = new HashSet<TransactionHead>();
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
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? TransactionSequenceType { get; set; }
        public int? TaxTemplateID { get; set; }
        public bool? IgnoreInventoryCheck { get; set; }
        public long? WorkflowID { get; set; }
        public bool? IsExternal { get; set; }

        [ForeignKey("ReferenceTypeID")]
        [InverseProperty("DocumentTypes")]
        public virtual DocumentReferenceType ReferenceType { get; set; }
        [ForeignKey("TaxTemplateID")]
        [InverseProperty("DocumentTypes")]
        public virtual TaxTemplate TaxTemplate { get; set; }
        [ForeignKey("WorkflowID")]
        [InverseProperty("DocumentTypes")]
        public virtual Workflow Workflow { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<BidApprovalHead> BidApprovalHeads { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<DocumentDepartmentMap> DocumentDepartmentMaps { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<DocumentTypeSetting> DocumentTypeSettings { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }
        [InverseProperty("DocumentTypeMap")]
        public virtual ICollection<DocumentTypeTypeMap> DocumentTypeTypeMapDocumentTypeMaps { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<DocumentTypeTypeMap> DocumentTypeTypeMapDocumentTypes { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<EmployeeESBProvisionHead> EmployeeESBProvisionHeads { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<EmployeeLSProvisionHead> EmployeeLSProvisionHeads { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<JobEntryHead> JobEntryHeadDocumentTypes { get; set; }
        [InverseProperty("ReferenceDocumentType")]
        public virtual ICollection<JobEntryHead> JobEntryHeadReferenceDocumentTypes { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<LoanType> LoanTypes { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<Payable> Payables { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<Receivable> Receivables { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
