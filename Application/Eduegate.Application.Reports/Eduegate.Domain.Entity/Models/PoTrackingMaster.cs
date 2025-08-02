using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PoTrackingMaster
    {
        public int PoTrackingMasterID { get; set; }
        public string PoNumber { get; set; }
        public string SupplierName { get; set; }
        public Nullable<decimal> PoValue { get; set; }
        public string Details { get; set; }
        public byte RefPoTrackingWorkflowNo { get; set; }
        public string PoTrackingStatus { get; set; }
        public byte RefPoTrackingWorkflowID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public Nullable<short> ProductManagerID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<byte> RefCurrencyID { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
    }
}
