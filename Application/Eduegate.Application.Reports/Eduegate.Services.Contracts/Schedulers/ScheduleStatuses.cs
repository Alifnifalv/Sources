using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public enum ScheduleStatuses
    {
        [EnumMember]
        NotStarted = 1,
        [EnumMember]
        Started = 1,
        [EnumMember]
        Completed = 2
    }
}
