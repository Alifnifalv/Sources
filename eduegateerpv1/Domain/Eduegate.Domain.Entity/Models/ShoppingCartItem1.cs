using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ShoppingCartItems", Schema = "inventory")]
    public partial class ShoppingCartItem1
    {
        [Key]
        public long ShoppingCartItemID { get; set; }
        public Nullable<long> ShoppingCartID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int? DeliveryDays { get; set; }
        public int? DisplayRange { get; set; }
    }
}
