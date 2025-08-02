using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "Systems")]    
    public enum VoucherTypes
    {
        [EnumMember]
        Marketing = 1,
        [EnumMember]
        AmountCapture = 2,
        [EnumMember]
        Refund = 3,
        [EnumMember]
        Loyalty = 4,
        [EnumMember]
        Free = 5,
    }
}
