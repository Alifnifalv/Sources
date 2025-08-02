using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccountTransactionHeads", Schema = "account")]
    [Index("DocumentTypeID", Name = "IDX_AccountTransactionHeads_DocumentTypeID_")]
    public partial class AccountTransactionHead
    {
        public AccountTransactionHead()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AccountTransactionHeadAccountMaps = new HashSet<AccountTransactionHeadAccountMap>();
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeCollectionMonthlySplits = new HashSet<FeeCollectionMonthlySplit>();
            FeeCollections = new HashSet<FeeCollection>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            FinalSettlementMonthlySplits = new HashSet<FinalSettlementMonthlySplit>();
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            RefundMonthlySplits = new HashSet<RefundMonthlySplit>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
        }

        [Key]
        public long AccountTransactionHeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? PaymentModeID { get; set; }
        public long? AccountID { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [StringLength(100)]
        public string Reference { get; set; }
        public bool? IsPrePaid { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AdvanceAmount { get; set; }
        public int? CostCenterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AmountPaid { get; set; }
        public long? DocumentStatusID { get; set; }
        public byte? TransactionStatusID { get; set; }
        public long? BranchID { get; set; }
        public long? CompanyID { get; set; }
        public long? ReceiptsID { get; set; }
        public long? PaymentsID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference2 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference3 { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal? DiscountPercentage { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ChequeNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChequeDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("AccountTransactionHeads")]
        public virtual Account Account { get; set; }
        [ForeignKey("BranchID")]
        [InverseProperty("AccountTransactionHeads")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DocumentStatusID")]
        [InverseProperty("AccountTransactionHeads")]
        public virtual DocumentStatus DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("AccountTransactionHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("TransactionStatusID")]
        [InverseProperty("AccountTransactionHeads")]
        public virtual TransactionStatus TransactionStatus { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<FinalSettlementMonthlySplit> FinalSettlementMonthlySplits { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<RefundMonthlySplit> RefundMonthlySplits { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
    }
}
