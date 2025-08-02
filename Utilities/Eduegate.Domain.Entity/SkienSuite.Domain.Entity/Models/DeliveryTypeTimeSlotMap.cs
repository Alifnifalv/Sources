using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypeTimeSlotMap
    {
        public DeliveryTypeTimeSlotMap()
        {
            this.DeliveryTypeTimeSlotMapsCultures = new List<DeliveryTypeTimeSlotMapsCulture>();
        }

        public long DeliveryTypeTimeSlotMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<System.TimeSpan> TimeFrom { get; set; }
        public Nullable<System.TimeSpan> TimeTo { get; set; }
        public Nullable<bool> IsCutOff { get; set; }
        public Nullable<byte> CutOffDays { get; set; }
        public Nullable<System.TimeSpan> CutOffTime { get; set; }
        public Nullable<byte> CutOffHour { get; set; }
        public string CutOffDisplayText { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
    }
}
