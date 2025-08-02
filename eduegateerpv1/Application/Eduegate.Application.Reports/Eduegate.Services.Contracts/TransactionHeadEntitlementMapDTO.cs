using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
    }
}
