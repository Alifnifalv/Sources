using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "CustomerGroups")]
    public enum CustomerGroups
    {
        [Description("Blue Member")]
        [EnumMember]
        BlueMember = 1,

        [Description("Silver Member")]
        [EnumMember]
        SilverMember = 2,

        [Description("Gold Member")]
        [EnumMember]
        GoldMember = 3,

        [Description("Platinum Member")]
        [EnumMember]
        PlatinumMember = 4,

        [Description("Diamond Member")]
        [EnumMember]
        DiamondMember = 5,

        [Description("Diamond + Member")]
        [EnumMember]
        DiamondsMember = 6
    }
}
