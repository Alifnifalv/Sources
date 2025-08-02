using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "NewsTypes")]
    public enum NewsTypes
    {
        [EnumMember]
        Ours = 1,
        [EnumMember]
        Media = 2,
        [EnumMember]
        Featured = 3,
        [EnumMember]
        All = 0,
    }
}
