using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderStatusMaster
    {
        public OrderStatusMaster()
        {
            this.OrderMasters = new List<OrderMaster>();
        }

        [Key]
        public byte OrderStatusID { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public string StatusTextAr { get; set; }
        public virtual ICollection<OrderMaster> OrderMasters { get; set; }
    }
}
