using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TimeSlotMaster
    {
        public int TimeSlotID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<byte> Cutoff { get; set; }
        public Nullable<bool> Monday { get; set; }
        public Nullable<bool> Tuesday { get; set; }
        public Nullable<bool> Wednesday { get; set; }
        public Nullable<bool> Thursday { get; set; }
        public Nullable<bool> Friday { get; set; }
        public Nullable<bool> Saturday { get; set; }
        public Nullable<bool> Sunday { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
    }
}
