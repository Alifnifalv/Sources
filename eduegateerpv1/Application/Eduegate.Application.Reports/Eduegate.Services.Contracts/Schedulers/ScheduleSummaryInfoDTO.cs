using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class ScheduleSummaryInfoDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public DateTime ActualTime { get; set; }

        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public int NumberOfPickups { get; set; }

        [DataMember]
        public int NumberOfDrops { get; set; }

        [DataMember]
        public int NumberOfPickupsCompleted { get; set; }

        [DataMember]
        public int NumberOfDropsCompleted { get; set; }

        [DataMember]
        public List<SchedulerSummaryDetailDTO> SchedulerSummaryDetails { get; set; }
    }
}
