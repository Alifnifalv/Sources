using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwOrderItem
    {
        [Key]
        public long RefOrderID { get; set; }
        public int Quantity { get; set; }
        public int cancelqty { get; set; }
        public int retqty { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public int RefOrderProductID { get; set; }
        public long OrderItemID { get; set; }
        public Nullable<int> ReturnApproveQty { get; set; }
    }
}
