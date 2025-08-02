
using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class DeliveryTypeCutOffSlotDTO : BaseMasterDTO
    {
        [DataMember]
        public int DeliveryTypeCutOffSlotID { get; set; }
        [DataMember]
        public int? DeliveryTypeID { get; set; }
        [DataMember]
        public byte? OccurrenceTypeID { get; set; }
        [DataMember]
        public byte? OccuranceDayID { get; set; }
        [DataMember]
        public DateTime? OccuranceDate { get; set; }
        [DataMember]
        public long? TimeSlotID { get; set; }
        [DataMember]
        public DateTime? TimeFromValue { get; set; }
        [DataMember]
        public DateTime? TimeToValue { get; set; }
        [DataMember]
        public string TimeFrom { get; set; }
        [DataMember]
        public string TimeTo { get; set; }
        [DataMember]
        public string WarningMessage { get; set; }
        [DataMember]
        public string TooltipMessage { get; set; }
    }
}
