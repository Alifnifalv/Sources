using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderGiftItem
    {
        [Key]
        public int OrderGiftItemID { get; set; }
        public Nullable<long> RefOrderID { get; set; }
        public long RefProductID { get; set; }
        public string FreindName { get; set; }
        public string FreindEmail { get; set; }
        public string EmailMessage { get; set; }
    }
}
