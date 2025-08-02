using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Payables", Schema = "account")]
    public partial class Payable
    {
        public Payable()
        {
            InverseReferencePayables = new HashSet<Payable>();
            JobsEntryHeadPayableMaps = new HashSet<JobsEntryHeadPayableMap>();
        }

        [Key]
        public long PayableIID { get; set; }
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
        public long? ReferencePayablesID { get; set; }
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
        [InverseProperty("Payables")]
        public virtual Account Account { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("Payables")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("DocumentStatusID")]
        [InverseProperty("Payables")]
        public virtual DocumentStatus DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("Payables")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("ReferencePayablesID")]
        [InverseProperty("InverseReferencePayables")]
        public virtual Payable ReferencePayables { get; set; }
        [ForeignKey("TransactionStatusID")]
        [InverseProperty("Payables")]
        public virtual TransactionStatus TransactionStatus { get; set; }
        [InverseProperty("ReferencePayables")]
        public virtual ICollection<Payable> InverseReferencePayables { get; set; }
        [InverseProperty("Payable")]
        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
    }
}
