using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "TenderType")]
    public enum TenderType
    {
        [EnumMember]
        Cash = 0,
        [EnumMember]
        ForeignCurrency = 1,
        [EnumMember]
        Cards = 2,
        [EnumMember]
        Cheque = 3,
        [EnumMember]
        Online = 4,
        [EnumMember]
        DirectTransfer = 5,
        [EnumMember]
        Vouchers = 6,
        [EnumMember]
        Others = 7
    }
}
