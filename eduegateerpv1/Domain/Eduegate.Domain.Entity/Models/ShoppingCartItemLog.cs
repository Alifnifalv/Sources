using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartItemLog
    {
        [Key]
        public int LogID { get; set; }
        public string CustomerSessionID { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
    }
}
