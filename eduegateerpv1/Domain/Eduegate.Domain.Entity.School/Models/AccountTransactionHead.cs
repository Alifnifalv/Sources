namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AccountTransactionHeads", Schema = "account")]
    public partial class AccountTransactionHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountTransactionHead()
        {
            //AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            //AccountTransactionHeadAccountMaps = new HashSet<AccountTransactionHeadAccountMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeCollectionMonthlySplits = new HashSet<FeeCollectionMonthlySplit>();
            FeeCollections = new HashSet<FeeCollection>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            FinalSettlementMonthlySplits = new HashSet<FinalSettlementMonthlySplit>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            RefundMonthlySplits = new HashSet<RefundMonthlySplit>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
        }

        [Key]
        public long AccountTransactionHeadIID { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(50)]
        public string TransactionNumber { get; set; }

        public int? DocumentTypeID { get; set; }

        public int? PaymentModeID { get; set; }

        public long? AccountID { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [StringLength(100)]
        public string Reference { get; set; }

        public bool? IsPrePaid { get; set; }

        public decimal? AdvanceAmount { get; set; }

        public int? CostCenterID { get; set; }

        public decimal? AmountPaid { get; set; }

        public long? DocumentStatusID { get; set; }

        public byte? TransactionStatusID { get; set; }

        public long? BranchID { get; set; }

        public long? CompanyID { get; set; }

        public long? ReceiptsID { get; set; }

        public long? PaymentsID { get; set; }

        [StringLength(50)]
        public string ExternalReference1 { get; set; }

        [StringLength(50)]
        public string ExternalReference2 { get; set; }

        [StringLength(50)]
        public string ExternalReference3 { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        [StringLength(30)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }

        //public virtual Branch Branch { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefundMonthlySplit> RefundMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
    }
}
