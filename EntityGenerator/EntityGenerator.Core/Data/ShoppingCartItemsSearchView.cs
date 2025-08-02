using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ShoppingCartItemsSearchView
    {
        public long ShoppingCartIID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductDiscountPrice { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? DeliveryDays { get; set; }
        [StringLength(1000)]
        public string SKUName { get; set; }
        [StringLength(50)]
        public string BarCode { get; set; }
        [StringLength(50)]
        public string PartNo { get; set; }
    }
}
