using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductInventoryConfigTemp", Schema = "catalog")]
    public partial class ProductInventoryConfigTemp
    {
        [Key]
        public long ProductInventoryConfigIID { get; set; }
        public long? ProductID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NotifyQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumQuanityInCart { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumQuantityInCart { get; set; }
        public bool? IsQuntityUseDecimals { get; set; }
        public byte? BackOrderTypeID { get; set; }
        public byte? IsStockAvailabiltiyID { get; set; }
        [StringLength(50)]
        public string ProductWarranty { get; set; }
        public bool? IsSerialNumber { get; set; }
        public bool? IsSerialRequiredForPurchase { get; set; }
        public short? DeliveryMethod { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductWeight { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductLength { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductWidth { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductHeight { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DimensionalWeight { get; set; }
        public short? PackingTypeID { get; set; }
        public bool? IsMarketPlace { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? HSCode { get; set; }
        public byte? DeliveryDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductCost { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
