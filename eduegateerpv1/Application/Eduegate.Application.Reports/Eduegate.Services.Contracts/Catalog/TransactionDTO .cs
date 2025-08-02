using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class TransactionDTO
    {
        [DataMember]
        public TransactionHeadDTO TransactionHead { get; set; }

        [DataMember]
        public List<TransactionDetailDTO> TransactionDetails { get; set; }

       
        [DataMember]
        public List<TransactionHeadDTO> TransactionHeads { get; set; }

        [DataMember]
        public ShipmentDetailDTO ShipmentDetails { get; set; }

        [DataMember]
        public OrderContactMapDTO OrderContactMap { get; set; }

        [DataMember]
        public List<OrderContactMapDTO> OrderContactMaps { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public bool? IgnoreEntitlementCheck { get; set; }

        [DataMember]
        public TransactionHeadEntitlementMapDTO TransactionHeadEntitlementMap { get; set; }
    }
}
