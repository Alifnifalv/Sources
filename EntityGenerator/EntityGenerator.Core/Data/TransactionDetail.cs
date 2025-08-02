using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionDetails", Schema = "inventory")]
    [Index("ProductSKUMapID", Name = "IDX_TransactionDetails_ProductSKUMapID_")]
    [Index("HeadID", Name = "idx_TransactionDetailsHeadID")]
    [Index("HeadID", Name = "idx_TransactionDetailsHeadIDInclProdIDAmount")]
    [Index("HeadID", Name = "idx_TransactionDetailsHeadIDInclProductID")]
    [Index("HeadID", Name = "idx_TransactionDetailsHeadIDInclSKUPrice")]
    [Index("HeadID", Name = "idx_TransactionDetailsHeadIDInclUnitpriceAmt")]
    [Index("ProductID", Name = "idx_TransactionDetailsProductID")]
    public partial class TransactionDetail
    {
        public TransactionDetail()
        {
            InverseParentDetail = new HashSet<TransactionDetail>();
            ProductSerialMaps = new HashSet<ProductSerialMap>();
            TransactionAllocations = new HashSet<TransactionAllocation>();
        }

        [Key]
        public long DetailIID { get; set; }
        public long? HeadID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        public long? UnitID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? ExchangeRate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WarrantyDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Unicode(false)]
        public string SerialNumber { get; set; }
        public long? ParentDetailID { get; set; }
        public int? Action { get; set; }
        [StringLength(200)]
        public string Remark { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount1 { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount2 { get; set; }
        public int? TaxTemplateID { get; set; }
        [Column(TypeName = "decimal(8, 3)")]
        public decimal? TaxPercentage { get; set; }
        public bool? HasTaxInclusive { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InclusiveTaxAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExclusiveTaxAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WarrantyStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WarrantyEndDate { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(100)]
        public string BarCode { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        public long? CartItemID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualUnitPrice { get; set; }
        public int? ProductOptionID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LandingCost { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastCostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fraction { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ForeignAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ForeignRate { get; set; }
        public long? UnitGroupID { get; set; }

        [ForeignKey("CostCenterID")]
        [InverseProperty("TransactionDetails")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("TransactionDetails")]
        public virtual TransactionHead Head { get; set; }
        [ForeignKey("ParentDetailID")]
        [InverseProperty("InverseParentDetail")]
        public virtual TransactionDetail ParentDetail { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("TransactionDetails")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("TransactionDetails")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("UnitID")]
        [InverseProperty("TransactionDetails")]
        public virtual Unit Unit { get; set; }
        [InverseProperty("ParentDetail")]
        public virtual ICollection<TransactionDetail> InverseParentDetail { get; set; }
        [InverseProperty("Detail")]
        public virtual ICollection<ProductSerialMap> ProductSerialMaps { get; set; }
        [InverseProperty("TrasactionDetail")]
        public virtual ICollection<TransactionAllocation> TransactionAllocations { get; set; }
    }
}
