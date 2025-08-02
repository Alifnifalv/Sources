using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartSaveforLater
    {
        public long SaveforLaterID { get; set; }
        public Nullable<int> RefProductID { get; set; }
        public Nullable<int> RefCustomerID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
