using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderDetailsLoyaltyPoint
    {
        public long OrderDetailsLoyaltyPointsID { get; set; }
        public long RefOrderID { get; set; }
        public int RefProductID { get; set; }
        public short ProductQuantity { get; set; }
        public short Points { get; set; }
        public virtual OrderMaster OrderMaster { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}
