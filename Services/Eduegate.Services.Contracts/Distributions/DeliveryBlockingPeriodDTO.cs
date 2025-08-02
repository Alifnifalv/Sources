using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class DeliveryBlockingPeriodDTO
    {
        [DataMember]
        public List<string> BlockedDates { get; set; }
        [DataMember]
        public List<TimeInterval> Times { get; set; }
        [DataMember]
        public string MaxLimitDate { get; set; }
    }

    [DataContract]
    public class TimeInterval
    {
        [DataMember]
        public int StartHour { get; set; }

        [DataMember]
        public int EndHour { get; set; }
    }
}
