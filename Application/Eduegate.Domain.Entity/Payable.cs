namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Payables")]
    public partial class Payable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payable()
        {
            JobsEntryHeadPayableMaps = new HashSet<JobsEntryHeadPayableMap>();
            Payables1 = new HashSet<Payable>();
        }

        [Key]
        public long PayableIID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        public string TransactionNumber { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }

        public long? SerialNumber { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? ReferencePayablesID { get; set; }

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
        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payable> Payables1 { get; set; }

        public virtual Payable Payable1 { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}
