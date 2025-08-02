using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DeliveryCheckDTO
    {
        [DataMember]
        public long SKUID { get; set; }
        [DataMember]
        public long DeliveryCount { get; set; }
    }
}
