namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.StockCompareDetails")]
    public partial class StockCompareDetail
    {
        [Key]
        public long DetailIID { get; set; }

        public long? HeadID { get; set; }

        public long? ReferenceHeadID { get; set; }

        public long? ProductID { get; set; }

        public long? ProductSKUMapID { get; set; }

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

        public decimal? ExchangeRate { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public string SerialNumber { get; set; }

        public long? ParentDetailID { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public DateTime? WarrantyStartDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        public int? CostCenterID { get; set; }

        [StringLength(100)]
        public string BarCode { get; set; }

        public long? CartItemID { get; set; }

        public int? ProductOptionID { get; set; }

        public decimal? LastCostPrice { get; set; }

        public long? TransactionHeadID { get; set; }
    }
}
