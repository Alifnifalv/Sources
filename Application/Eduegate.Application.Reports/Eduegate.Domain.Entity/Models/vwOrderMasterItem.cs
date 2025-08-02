using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwOrderMasterItem
    {
        public long OrderItemID { get; set; }
        public int RefOrderProductID { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemAmount { get; set; }
        public bool ItemCanceled { get; set; }
        public long OrderID { get; set; }
        public long RefCustomerID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string SessionID { get; set; }
        public Nullable<int> ReturnApproveQty { get; set; }
        public decimal ProductDiscountPrice { get; set; }
    }
}
