using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Logging
{
    [DataContract]
    public enum ActionTypes
    {
        [Description("Created")]
        [EnumMember]
        Created = 1,
        [Description("Modifeid")]
        [EnumMember]
        Modifeid = 2,
        [Description("Deleted")]
        [EnumMember]
        Deleted = 3,
        [Description("Approved")]
        [EnumMember]
        Approved = 4,
    }
}
