using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class EntitlementPriceListMapDTO
    {
        [DataMember]
        public Nullable<byte> EntitlementID { get; set; }
        [DataMember]
        public string EntitlementName { get; set; }
        [DataMember]
        public string PriceListName { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListIID { get; set; }
        [DataMember]
        public long ProductPriceListCustomerMapIID { get; set; }
        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [DataMember]
        public string TimeStamps { get; set; }
    }
}
