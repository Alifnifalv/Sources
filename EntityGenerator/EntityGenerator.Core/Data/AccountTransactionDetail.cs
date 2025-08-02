using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccountTransactionDetails", Schema = "account")]
    [Index("AccountTransactionHeadID", Name = "idx_AccountTransactionDetailsAccountTransactionHeadID")]
    public partial class AccountTransactionDetail
    {
        public AccountTransactionDetail()
        {
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
        }

        [Key]
        public long AccountTransactionDetailIID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public long? AccountID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceNumber { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ReferenceRate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ReferenceQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(2000)]
        public string Remarks { get; set; }
        public int? CostCenterID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InvoiceAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ReturnAmount { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDueDate { get; set; }
        public long? ProductSKUId { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AvailableQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CurrentAvgCost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NewAvgCost { get; set; }
        public int? AccountType { get; set; }
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? UnPaidAmount { get; set; }
        [StringLength(50)]
        public string JobMissionNumber { get; set; }
        public int? TaxTemplateID { get; set; }
        [Column(TypeName = "decimal(8, 3)")]
        public decimal? TaxPercentage { get; set; }
        public long? ReferenceReceiptID { get; set; }
        public long? ReferencePaymentID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference2 { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference3 { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        public long? SubLedgerID { get; set; }
        public int? BudgetID { get; set; }
        public long? DepartmentID { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("AccountTransactionDetails")]
        public virtual Account Account { get; set; }
        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("AccountTransactionDetails")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("BudgetID")]
        [InverseProperty("AccountTransactionDetails")]
        public virtual Budget Budget { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("AccountTransactionDetails")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("ProductSKUId")]
        [InverseProperty("AccountTransactionDetails")]
        public virtual ProductSKUMap ProductSKU { get; set; }
        [ForeignKey("SubLedgerID")]
        [InverseProperty("AccountTransactionDetails")]
        public virtual Accounts_SubLedger SubLedger { get; set; }
        [InverseProperty("AccountTransactionDetail")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
    }
}
