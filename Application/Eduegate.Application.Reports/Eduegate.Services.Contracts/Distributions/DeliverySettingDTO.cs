using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class DeliverySettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int DeliveryTypeID { get; set; }
        [DataMember]
        public string DeliveryTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<int> Priority { get; set; }
        [DataMember]
        public Nullable<byte> StatusID { get; set; }
        [DataMember]
        public Nullable<int> Days { get; set; }
        [DataMember]
        public List<DeliveryTimeSlotDTO> TimeSlots { get; set; }
        [DataMember]
        public List<CustomerGroupDeliveryChargeDTO> CustomerGroupDeliveryCharges { get; set; }
        [DataMember]
        public List<AreaDeliveryChargeDTO> AreaCharges { get; set; }
        [DataMember]
        public List<ProductSKUDeliveryTypeChargeDTO> ProductSKUCharges { get; set; }
        [DataMember]
        public bool? HasTimeSlot { get; set; }

        [DataMember]
        public decimal? DistanceStartRange { get; set; }
        [DataMember]
        public decimal? DistanceEndRange { get; set; }
        [DataMember]
        public bool NotAvailable { get; set; }

        [DataMember]
        public string WarningMessage { get; set; }

        [DataMember]
        public string TooltipMessage { get; set; }
    }
}
