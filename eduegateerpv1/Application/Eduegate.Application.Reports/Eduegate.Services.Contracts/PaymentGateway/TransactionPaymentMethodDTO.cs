using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.PaymentGateway
{
    [DataContract]
    public class TransactionPaymentMethodDTO
    {
        [DataMember]
        public long PaymentMapIID { get; set; }
        [DataMember]
        public long HeadID { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.PaymentMethodTypes PaymentMethodID { get; set; }
        [DataMember]
        public string PaymentMethodName { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public Nullable<DateTime> PaymentDate { get; set; }
    }
}
