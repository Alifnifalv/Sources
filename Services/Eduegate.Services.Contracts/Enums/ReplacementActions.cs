using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum ReplacementActions
    {
        [EnumMember]
        NoAction = 1,
        [EnumMember]
        Cancellation = 2,
        [EnumMember]
        Return = 3,
        [EnumMember]
        Replace = 4,
        [EnumMember]
        Exchange = 5,
        [EnumMember]
        Repair = 6,
    }
}
