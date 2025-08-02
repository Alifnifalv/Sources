using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TimeSlotOverRider
    {
        public int OverrideID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> Cutoff { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<bool> Exclude { get; set; }
        public virtual SupplierMaster SupplierMaster { get; set; }
    }
}
