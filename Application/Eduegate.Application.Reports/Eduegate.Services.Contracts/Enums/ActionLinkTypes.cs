using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "ActionLinkTypes")]
    public enum ActionLinkTypes
    {
        [EnumMember]
        Brand = 1,
        [EnumMember]
        Category = 2,
        [EnumMember]
        External_Link = 3,
        [EnumMember]
        Product_Detail = 4,
        [EnumMember]
        Product_Search = 5,
        [EnumMember]
        Deal = 6

    }
}
