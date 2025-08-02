using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierPurchaseOrderMaster
    {
        public int SupplierPurchaseOrderMasterID { get; set; }
        public int RefSupplierID { get; set; }
        public long RefOrderID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool EmailSend { get; set; }
        public Nullable<System.DateTime> EmailSendOn { get; set; }
        public bool CancelEmailSend { get; set; }
        public Nullable<System.DateTime> CancelEmailSendOn { get; set; }
        public string Status { get; set; }
    }
}
