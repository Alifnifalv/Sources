using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCart
    {
        public long ShoppingCartItemID { get; set; }
        public Nullable<long> SkuID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Price { get; set; }
        public Nullable<byte> DeliveryID { get; set; }
    }
}
