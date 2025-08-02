using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderDeliveryHolidayDetail
    {
        public long OrderDeliveryHolidayDetailsIID { get; set; }
        public Nullable<long> OrderDeliveryHolidayHeadID { get; set; }
        public Nullable<System.DateTime> HolidayDate { get; set; }
        public virtual OrderDeliveryHolidayHead OrderDeliveryHolidayHead { get; set; }
    }
}
