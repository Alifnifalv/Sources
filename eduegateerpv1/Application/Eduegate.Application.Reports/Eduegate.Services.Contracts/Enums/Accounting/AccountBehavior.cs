using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Accounting
{
    [DataContract]
    public enum AccountBehavior
    {
        [EnumMember]
        DebitSide = 1,
        [EnumMember]
        CreditSide = 2,
        [EnumMember]
        Both = 3
    }
}
