using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class SchedulerSummaryDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ScheduleID { get; set; }
        [DataMember]
        public ScheduleTypes ScheduleType { get; set; }
        [DataMember]
        public ScheduleStatuses ScheduleStatus { get; set; }
    }
}
