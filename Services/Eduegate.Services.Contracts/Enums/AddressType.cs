using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum AddressType
    {
        [EnumMember]
        All = 0,
        [EnumMember]
        Billing = 1,
        [EnumMember]
        Shipping = 2
    }
}
