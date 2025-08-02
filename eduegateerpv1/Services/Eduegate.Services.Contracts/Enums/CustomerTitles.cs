using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "CustomerTitles")]
    public enum CustomerTitles
    {
        [Description("Mr.")]
        [EnumMember]
        Mr = 1,

        [Description("Mrs.")]
        [EnumMember]
        Mrs = 2,

        [Description("Ms")]
        [EnumMember]
        Ms = 3,

        [Description("Miss")]
        [EnumMember]
        Miss = 4,

        [Description("Master")]
        [EnumMember]
        Master = 5,
    }
}
