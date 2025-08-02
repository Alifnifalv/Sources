using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class DeliveryTypeTimeSlotDTO : BaseMasterDTO
    {
        [DataMember]
        public long DeliveryTypeTimeSlotMapIID { get; set; }
        [DataMember]
        public System.Nullable<int> DeliveryTypeID { get; set; }
        [DataMember]
        public System.Nullable<System.TimeSpan> TimeFrom { get; set; }
        [DataMember]
        public System.Nullable<System.TimeSpan> TimeTo { get; set; }
        [DataMember]
        public System.Nullable<bool> IsCutOff { get; set; }
        [DataMember]
        public System.Nullable<byte> CutOffDays { get; set; }
        [DataMember]
        public System.Nullable<System.TimeSpan> CutOffTime { get; set; }
        [DataMember]
        public System.Nullable<byte> CutOffHour { get; set; }
        [DataMember]
        public string CutOffDisplayText { get; set; }
        [DataMember]
        public int? NoOfCutOffOrder { get; set; }
        [DataMember]
        public string SlotName { get; set; }
    }
}
