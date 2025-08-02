using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierPurchaseOrderDetail
    {
        public int SupplierPurchaseOrderDetailsID { get; set; }
        public int RefSupplierPurchaseOrderMasterID { get; set; }
        public long RefOrderItemID { get; set; }
        public int RefProductID { get; set; }
        public short Quantity { get; set; }
        public Nullable<short> CancelQty { get; set; }
        public decimal CostPrice { get; set; }
        public string Status { get; set; }
        public Nullable<short> ProductManagerID { get; set; }
    }
}
