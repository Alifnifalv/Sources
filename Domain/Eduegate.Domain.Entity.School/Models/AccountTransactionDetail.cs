namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AccountTransactionDetails", Schema = "account")]
    public partial class AccountTransactionDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountTransactionDetail()
        {
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
        }

        [Key]
        public long AccountTransactionDetailIID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public long? AccountID { get; set; }

        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public decimal? ReferenceRate { get; set; }

        public decimal? ReferenceQuantity { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public int? CostCenterID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public decimal? InvoiceAmount { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? ReturnAmount { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public long? ProductSKUId { get; set; }

        public decimal? AvailableQuantity { get; set; }

        public decimal? CurrentAvgCost { get; set; }

        public decimal? NewAvgCost { get; set; }

        public int? AccountType { get; set; }

        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        public decimal? UnPaidAmount { get; set; }

        [StringLength(50)]
        public string JobMissionNumber { get; set; }

        public int? TaxTemplateID { get; set; }

        public decimal? TaxPercentage { get; set; }

        public long? ReferenceReceiptID { get; set; }

        public long? ReferencePaymentID { get; set; }

        [StringLength(50)]
        public string ExternalReference1 { get; set; }

        [StringLength(50)]
        public string ExternalReference2 { get; set; }

        public decimal? TaxAmount { get; set; }

        [StringLength(50)]
        public string ExternalReference3 { get; set; }

        public int? BudgetID { get; set; }

        public decimal? DiscountAmount { get; set; }

        public virtual Account Account { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual Budget Budget { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
    }
}
