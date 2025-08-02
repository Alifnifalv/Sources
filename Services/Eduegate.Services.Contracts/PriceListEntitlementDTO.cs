using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class PriceListEntitlementDTO
    {
        [DataMember]
        public List<EntitlementPriceListMapDTO> EntitlementPriceListMaps { get; set; }
    }
}
