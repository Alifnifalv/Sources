using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartItem
    {
        public long ShoppingCartItemID { get; set; }
        public Nullable<long> ShoppingCartID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> DeliveryDays { get; set; }
        public Nullable<decimal> NetWeight { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<decimal> ProductDiscountPrice { get; set; }
        public Nullable<int> DisplayRange { get; set; }
        public virtual ShoppingCart1 ShoppingCart { get; set; }
    }
}
