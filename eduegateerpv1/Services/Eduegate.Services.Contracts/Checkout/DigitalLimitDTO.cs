using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Checkout
{
    [DataContract]
    public class DigitalLimitDTO
    {
        [DataMember]
        public bool IsAllowed { get; set; }
        [DataMember]
        public decimal AmountLimit { get; set; }
    }
}
