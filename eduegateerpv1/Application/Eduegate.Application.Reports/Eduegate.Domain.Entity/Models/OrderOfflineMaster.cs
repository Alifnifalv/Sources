using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderOfflineMaster
    {
        public long OrderOfflineID { get; set; }
        public string OfflineInvoiceNo { get; set; }
        public byte OfflineDeliveryMethod { get; set; }
        public Nullable<long> RefOfflineCustomerID { get; set; }
        public string OrderOfflineStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDatetime { get; set; }
        public Nullable<int> UpdatedByID { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
    }
}
