using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("ProductPriceListSKUMaps_20230210", Schema = "catalog")]
    public partial class ProductPriceListSKUMaps_20230210
    {
        public long ProductPriceListItemMapIID { get; set; }
        public int? CompanyID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? ProductSKUID { get; set; }
        public long? UnitGroundID { get; set; }
        public long? CustomerGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SellingQuantityLimit { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public byte? SortOrder { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PricePercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Cost { get; set; }
        public bool? IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public int? CurrencyID { get; set; }
    }
}
