using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartItemLog
    {
        public int LogID { get; set; }
        public string CustomerSessionID { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
    }
}
