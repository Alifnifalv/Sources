using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "CustomerStatus")]
    public enum CustomerStatus
    {
        [Description("Active customers")]
        [EnumMember]
        Active = 1,
        [Description("In Active customers")]
        [EnumMember]
        InActive = 2,

        [Description("Cancelled customers")]
        [EnumMember]
        Cancelled = 4,

        [Description("Delete customers")]
        [EnumMember]
        Deleted = 18
    }
}
