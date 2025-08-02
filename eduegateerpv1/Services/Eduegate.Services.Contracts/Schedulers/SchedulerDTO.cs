using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Schedulers;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class SchedulerDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EntitySchedulerIID { get; set; }
        [DataMember]
        public SchedulerTypes SchedulerType { get; set; }
        [DataMember]
        public SchedulerEntityTypes SchedulerEntityType { get; set; }
        [DataMember]
        public string EntityID { get; set; }
        [DataMember]
        public string EntityValue { get; set; }
    }
}
