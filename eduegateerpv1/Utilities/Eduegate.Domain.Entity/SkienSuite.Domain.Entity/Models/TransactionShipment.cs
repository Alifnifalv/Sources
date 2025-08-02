using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionShipment
    {
        public long TransactionShipmentIID { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public Nullable<long> SupplierIDFrom { get; set; }
        public Nullable<long> SupplierIDTo { get; set; }
        public string ShipmentReference { get; set; }
        public string FreightCarrier { get; set; }
        public Nullable<short> ClearanceTypeID { get; set; }
        public string AirWayBillNo { get; set; }
        public Nullable<decimal> FreightCharges { get; set; }
        public Nullable<decimal> BrokerCharges { get; set; }
        public Nullable<decimal> AdditionalCharges { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<int> NoOfBoxes { get; set; }
        public string BrokerAccount { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Supplier Supplier1 { get; set; }
    }
}
