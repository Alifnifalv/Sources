using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCart", Schema = "catalog")]
    public partial class ShoppingCart
    {
        [Key]
        public long ShoppingCartItemID { get; set; }
        public long? SkuID { get; set; }
        public int? Quantity { get; set; }
        [StringLength(10)]
        public string Price { get; set; }
        public byte? DeliveryID { get; set; }
    }
}
