using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Synchronizer
{
    [DataContract(Name = "TrackerStatuses")]
    public enum TrackerStatuses
    {
        [Description("New")]
        [EnumMember]
        New = 1,
        [Description("InProcess")]
        [EnumMember]
        InProcess = 2,
        [Description("Completed")]
        [EnumMember]
        Completed = 3,
        [Description("Failed")]
        [EnumMember]
        Failed = 4
    }
}
