using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "Contact Status")]
    public enum ContactStatus
    {
        [Description("Active")]
        [EnumMember]
        Active = 1,

        [Description("InActive")]
        [EnumMember]
        InActive = 2
    }
}
