using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypeTimeSlotMapsCulture
    {
        public byte CultureID { get; set; }
        public long DeliveryTypeTimeSlotMapID { get; set; }
        public string CutOffDisplayText { get; set; }
        public virtual DeliveryTypeTimeSlotMap DeliveryTypeTimeSlotMap { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
