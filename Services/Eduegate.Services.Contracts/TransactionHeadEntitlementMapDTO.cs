using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class TransactionHeadEntitlementMapDTO
    {
        [DataMember]
        public long TransactionHeadEntitlementMapID { get; set; }

        [DataMember]
        public long TransactionHeadID { get; set; }

        [DataMember]
        public byte EntitlementID { get; set; }

        [DataMember]
        public string EntitlementName { get; set; }

        [DataMember]
        public Nullable<decimal> Amount { get; set; }

        [DataMember]
        public string VoucherCode { get; set; }

        [DataMember]
        public string ReferenceNo { get; set; }

        [DataMember]
        public long? PaymentTrackID { get; set; }

        [DataMember]
        public string PaymentTransactionNumber { get; set; }
    }
}