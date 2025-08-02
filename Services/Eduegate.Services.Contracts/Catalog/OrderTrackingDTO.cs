using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class OrderTrackingDTO
    {
        [DataMember]
        public long OrderTrackingIID { get; set; }
        [DataMember]
        public long OrderID { get; set; }
        [DataMember]
        public Enums.TransactionStatus TransactionStatus { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<DateTime> StatusDate { get; set; }
        [DataMember]
        public Nullable<long> CreatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> CreatedDate{ get; set; }
    }
}
