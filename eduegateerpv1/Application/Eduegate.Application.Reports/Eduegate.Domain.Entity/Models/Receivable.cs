using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{

    [Table("account.Receivables")]
    public partial class Receivable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Receivable()
        {
            JobsEntryHeadReceivableMaps = new HashSet<JobsEntryHeadReceivableMap>();
            Receivables1 = new HashSet<Receivable>();
            TransactionHeadReceivablesMaps = new HashSet<TransactionHeadReceivablesMap>();
        }

        [Key]
        public long ReceivableIID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        public string TransactionNumber { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }

        public long? SerialNumber { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? ReferenceReceivablesID { get; set; }

        public long? DocumentStatusID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public DateTime? AccountPostingDate { get; set; }

        public decimal? ExchangeRate { get; set; }

        public int? CurrencyID { get; set; }

        public byte? TransactionStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? DebitOrCredit { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receivable> Receivables1 { get; set; }

        public virtual Receivable Receivable1 { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
    }
}
