using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public enum ScheduleTypes
    {
        [EnumMember]
        Pick,
        [EnumMember]
        Drop
    }
}
