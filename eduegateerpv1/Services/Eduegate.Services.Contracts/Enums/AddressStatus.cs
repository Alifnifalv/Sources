using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    public enum AddressStatus
    {
        [Description("Active Banners")]
        [EnumMember]
        Active = 1,
        [Description("In Active Banners")]
        [EnumMember]
        InActive = 2
    }
}
