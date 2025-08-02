using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Receivables", Schema = "account")]
    public partial class Receivable
    {
        public Receivable()
        {
            AccountTransactionReceivablesMaps = new HashSet<AccountTransactionReceivablesMap>();
            InverseReferenceReceivables = new HashSet<Receivable>();
            JobsEntryHeadReceivableMaps = new HashSet<JobsEntryHeadReceivableMap>();
            TransactionHeadReceivablesMaps = new HashSet<TransactionHeadReceivablesMap>();
        }

        [Key]
        public long ReceivableIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        public long? SerialNumber { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? ReferenceReceivablesID { get; set; }
        public long? DocumentStatusID { get; set; }
        public long? AccountID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AccountPostingDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        public int? CurrencyID { get; set; }
        public byte? TransactionStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? DebitOrCredit { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("Receivables")]
        public virtual Account Account { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("Receivables")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("DocumentStatusID")]
        [InverseProperty("Receivables")]
        public virtual DocumentStatus DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("Receivables")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("ReferenceReceivablesID")]
        [InverseProperty("InverseReferenceReceivables")]
        public virtual Receivable ReferenceReceivables { get; set; }
        [ForeignKey("TransactionStatusID")]
        [InverseProperty("Receivables")]
        public virtual TransactionStatus TransactionStatus { get; set; }
        [InverseProperty("Receivable")]
        public virtual ICollection<AccountTransactionReceivablesMap> AccountTransactionReceivablesMaps { get; set; }
        [InverseProperty("ReferenceReceivables")]
        public virtual ICollection<Receivable> InverseReferenceReceivables { get; set; }
        [InverseProperty("Receivable")]
        public virtual ICollection<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
        [InverseProperty("Receivable")]
        public virtual ICollection<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
    }
}
