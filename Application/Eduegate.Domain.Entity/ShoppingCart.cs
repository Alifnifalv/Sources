namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ShoppingCart")]
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
