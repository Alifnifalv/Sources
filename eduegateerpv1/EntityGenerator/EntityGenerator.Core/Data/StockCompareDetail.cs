using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StockCompareDetails", Schema = "inventory")]
    [Index("HeadID", Name = "IDX_StockCompareDetails_HeadID_ProductID__ProductSKUMapID__PhysicalQuantity__ActualQuantity__Remark")]
    public partial class StockCompareDetail
    {
        [Key]
        public long DetailIID { get; set; }
        public long? HeadID { get; set; }
        public long? ReferenceHeadID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Fraction { get; set; }
        public long? UnitGroupID { get; set; }
        public long? UnitID { get; set; }
        [Column(TypeName = "money")]
        public decimal? PhysicalUnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? PhysicalQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal? PhysicalAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? ActualUnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? ActualQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal? ActualAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? DifferUnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? DifferQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal? DifferAmount { get; set; }
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
        [StringLength(200)]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WarrantyStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WarrantyEndDate { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(100)]
        public string BarCode { get; set; }
        public long? CartItemID { get; set; }
        public int? ProductOptionID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LastCostPrice { get; set; }
        public long? TransactionHeadID { get; set; }
    }
}
