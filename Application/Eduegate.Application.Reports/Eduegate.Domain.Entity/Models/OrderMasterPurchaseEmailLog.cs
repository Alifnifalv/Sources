using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMasterPurchaseEmailLog
    {
        public long PurchaseEmailLogID { get; set; }
        public long RefOrderID { get; set; }
        public string Action { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> SupplierID { get; set; }
    }
}
