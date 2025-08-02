using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Synchronizer
{
    [DataContract]
    public class EntityChangeTrackerDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EntityChangeTrackerIID { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.Synchronizer.Entities Entity { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.Synchronizer.OperationTypes OperationType { get; set; }
        [DataMember]
        public long ProcessedID { get; set; }
        [DataMember]
        public List<string> ProcessedFields { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses TrackerStatus { get; set; }
    }
}
