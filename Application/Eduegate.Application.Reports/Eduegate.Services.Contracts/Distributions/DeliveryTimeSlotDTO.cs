using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class DeliveryTimeSlotDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long DeliveryTypeTimeSlotMapIID { get; set; }

        [DataMember]
        public  string TimeFrom { get; set; }

        [DataMember]
        public string TimeTo { get; set; }

        [DataMember]
        public Nullable<bool> IsCutOff { get; set; }

        [DataMember]
        public Nullable<short> CutOffDays { get; set; }

        [DataMember]
        public string CutOffTime { get; set; }

        [DataMember]
        public string CutOffHour { get; set; }

        [DataMember]
        public string CutOffDisplayText { get; set; }

        [DataMember]
        public string SlotName { get; set; }

        [DataMember]
        public int? NoOfCutOffOrder { get; set; }

        [DataMember]
        public int? DeliveryTypeID { get; set; }

        [DataMember]
        public System.Nullable<long> BranchID { get; set; }

        [DataMember]
        public TimeSpan? TimeFromSpan { get; set; }

        [DataMember]
        public TimeSpan? TimeToSpan { get; set; }
    }
}