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
    public enum ActionStatuses
    {
        [Description("None")]
        [EnumMember]
        None = 1,
        [Description("Success")]
        [EnumMember]
        Success = 2,
        [Description("Failed")]
        [EnumMember]
        Failed = 3
    }
}
