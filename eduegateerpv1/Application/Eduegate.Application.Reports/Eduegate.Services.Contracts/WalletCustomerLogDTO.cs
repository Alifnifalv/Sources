using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class WalletCustomerLogDTO
    {
        [DataMember]
        public long LogId { get; set; }
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public Nullable<long> CustomerId { get; set; }
        [DataMember]
        public Nullable<DateTime> CreatedDateTime { get; set; }
        [DataMember]
        public string CustomerSessionId { get; set; }
    }
}
