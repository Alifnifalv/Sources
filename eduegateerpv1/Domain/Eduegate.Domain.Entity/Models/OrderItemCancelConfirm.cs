using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderItemCancelConfirm
    {
        [Key]
        public long RefOrderID { get; set; }
        public long RefOrderItemID { get; set; }
        public int Quantity { get; set; }
        public Nullable<bool> UpdateInventory { get; set; }
    }
}
