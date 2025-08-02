using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierPurchaseOrder
    {
        public int SupplierPurchaseOrderID { get; set; }
        public int RefOrderID { get; set; }
        public int RefProductID { get; set; }
        public Nullable<short> RefSupplierID { get; set; }
        public Nullable<short> RefProductManagerID { get; set; }
        public short OrderQty { get; set; }
        public Nullable<short> CancelReturnQty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool EmailSend { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
