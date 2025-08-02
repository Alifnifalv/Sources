using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("InvetoryTransactions", Schema = "inventory")]
    [Index("HeadID", Name = "IDX_InvetoryTransactions_HeadID_")]
    [Index("ProductSKUMapID", "HeadID", Name = "IDX_InvetoryTransactions_ProductSKUMapID__HeadID_")]
    [Index("DocumentTypeID", Name = "idx_InvetoryTransactionsDocumentTypeID")]
    public partial class InvetoryTransaction
    {
        public InvetoryTransaction()
        {
            InverseLinkDocument = new HashSet<InvetoryTransaction>();
        }

        [Key]
        public long InventoryTransactionIID { get; set; }
        public int? SerialNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(30)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public long? ProductSKUMapID { get; set; }
        public long? BatchID { get; set; }
        public long? AccountID { get; set; }
        public long? UnitID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Cost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        public long? LinkDocumentID { get; set; }
        public long? BranchID { get; set; }
        public int? CompanyID { get; set; }
        public long? HeadID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LandingCost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastCostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fraction { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OriginalQty { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual Company Company { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual TransactionHead Head { get; set; }
        [ForeignKey("LinkDocumentID")]
        [InverseProperty("InverseLinkDocument")]
        public virtual InvetoryTransaction LinkDocument { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("UnitID")]
        [InverseProperty("InvetoryTransactions")]
        public virtual Unit Unit { get; set; }
        [InverseProperty("LinkDocument")]
        public virtual ICollection<InvetoryTransaction> InverseLinkDocument { get; set; }
    }
}
