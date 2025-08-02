using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccountTransactions", Schema = "account")]
    [Index("AccountID", Name = "INDX_ACC_TRN_ACCOUNTID")]
    [Index("TransactionDate", Name = "INDX_ACC_TRN_TRNDATE")]
    public partial class AccountTransaction
    {
        public AccountTransaction()
        {
            AccountTransactionHeadAccountMaps = new HashSet<AccountTransactionHeadAccountMap>();
            AssetTransactionHeadAccountMaps = new HashSet<AssetTransactionHeadAccountMap>();
            TransactionHeadAccountMaps = new HashSet<TransactionHeadAccountMap>();
        }

        [Key]
        public long TransactionIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
        public long? AccountID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InclusiveTaxAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExclusiveTaxAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal? DiscountPercentage { get; set; }
        public bool? DebitOrCredit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? BudgetID { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("AccountTransactions")]
        public virtual Account Account { get; set; }
        [ForeignKey("BudgetID")]
        [InverseProperty("AccountTransactions")]
        public virtual Budget Budget { get; set; }
        [ForeignKey("CostCenterID")]
        [InverseProperty("AccountTransactions")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("AccountTransactions")]
        public virtual DocumentType DocumentType { get; set; }
        [InverseProperty("AccountTransaction")]
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        [InverseProperty("AccountTransaction")]
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
        [InverseProperty("AccountTransaction")]
        public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
    }
}
