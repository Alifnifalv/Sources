using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartGiftItem
    {
        public int SCGistItemID { get; set; }
        public string CustomerSessionID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public long RefProductID { get; set; }
        public string FreindName { get; set; }
        public string FreindEmail { get; set; }
        public string EmailMessage { get; set; }
    }
}
