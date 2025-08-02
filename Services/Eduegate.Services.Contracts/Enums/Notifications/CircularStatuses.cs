using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Notifications
{
    [DataContract(Name = "CircularStatuses")]
    public enum CircularStatuses
    {
        [EnumMember]
        Draft = 1,
        [EnumMember]
        Publish = 2,
    }
}
