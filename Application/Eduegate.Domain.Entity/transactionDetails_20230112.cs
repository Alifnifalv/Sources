namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.transactionDetails_20230112")]
    public partial class transactionDetails_20230112
    {
        [Key]
        public long DetailIID { get; set; }

        public long? HeadID { get; set; }

        public long? ProductID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public decimal? Quantity { get; set; }

        public long? UnitID { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Amount { get; set; }

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

        public int? Action { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public decimal? TaxAmount1 { get; set; }

        public decimal? TaxAmount2 { get; set; }

        public int? TaxTemplateID { get; set; }

        public decimal? TaxPercentage { get; set; }

        public bool? HasTaxInclusive { get; set; }

        public decimal? InclusiveTaxAmount { get; set; }

        public decimal? ExclusiveTaxAmount { get; set; }

        public DateTime? WarrantyStartDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        public int? CostCenterID { get; set; }

        [StringLength(100)]
        public string BarCode { get; set; }

        public decimal? DiscountAmount { get; set; }

        public long? CartItemID { get; set; }

        public decimal? ActualUnitPrice { get; set; }

        public int? ProductOptionID { get; set; }

        public decimal? ActualQuantity { get; set; }

        public decimal? ActualAmount { get; set; }

        public decimal? LandingCost { get; set; }

        public decimal? LastCostPrice { get; set; }

        public decimal? Fraction { get; set; }

        public decimal? ForeignAmount { get; set; }

        public decimal? ForeignRate { get; set; }

        public long? UnitGroupID { get; set; }
    }
}
