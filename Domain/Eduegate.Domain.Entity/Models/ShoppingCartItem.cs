using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ShoppingCartItems", Schema = "inventory")]
    public partial class ShoppingCartItem
    {
        [Key]
        public long ShoppingCartItemID { get; set; }
        public Nullable<long> ShoppingCartID { get; set; }
        //public long ShoppingCartID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public Nullable<Int32> DeliveryTypeID { get; set; }
        public Nullable<Int32> DeliveryDays { get; set; }

        public Nullable<decimal> NetWeight { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public Nullable<Int64> BranchID { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<decimal> ProductDiscountPrice { get; set; }
        public Nullable<Int32> DisplayRange { get; set; }
    }
}
