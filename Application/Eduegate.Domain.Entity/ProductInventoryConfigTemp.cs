namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductInventoryConfigTemp")]
    public partial class ProductInventoryConfigTemp
    {
        [Key]
        public long ProductInventoryConfigIID { get; set; }

        public long? ProductID { get; set; }

        public decimal? NotifyQuantity { get; set; }

        public decimal? MinimumQuantity { get; set; }

        public decimal? MaximumQuantity { get; set; }

        public decimal? MinimumQuanityInCart { get; set; }

        public decimal? MaximumQuantityInCart { get; set; }

        public bool? IsQuntityUseDecimals { get; set; }

        public byte? BackOrderTypeID { get; set; }

        public byte? IsStockAvailabiltiyID { get; set; }

        [StringLength(50)]
        public string ProductWarranty { get; set; }

        public bool? IsSerialNumber { get; set; }

        public bool? IsSerialRequiredForPurchase { get; set; }

        public short? DeliveryMethod { get; set; }

        public decimal? ProductWeight { get; set; }

        public decimal? ProductLength { get; set; }

        public decimal? ProductWidth { get; set; }

        public decimal? ProductHeight { get; set; }

        public decimal? DimensionalWeight { get; set; }

        public short? PackingTypeID { get; set; }

        public bool? IsMarketPlace { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HSCode { get; set; }

        public byte? DeliveryDays { get; set; }

        public decimal? ProductCost { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
