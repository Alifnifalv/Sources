using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "Systems")]
    public enum Systems
    {
        [EnumMember]
        Stock = 1,
        [EnumMember]
        Account = 2,
        [EnumMember]
        Payroll = 3,
        [EnumMember]
        Warehouse = 4,
        [EnumMember]
        Support = 5,
        [EnumMember]
        Distribution = 6,
    }
}
