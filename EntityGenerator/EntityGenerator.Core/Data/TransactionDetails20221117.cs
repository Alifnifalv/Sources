using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TransactionDetails20221117", Schema = "inventory")]
    public partial class TransactionDetails20221117
    {
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
        [Column(TypeName = "decimal(18, 3)")]
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
    }
}
