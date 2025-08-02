using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderDeliveryHolidayHead
    {
        public OrderDeliveryHolidayHead()
        {
            this.OrderDeliveryHolidayDetails = new List<OrderDeliveryHolidayDetail>();
        }

        public long OrderDeliveryHolidayHeadIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<byte> WeekEndDayID1 { get; set; }
        public Nullable<byte> WeekEndDayID2 { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<OrderDeliveryHolidayDetail> OrderDeliveryHolidayDetails { get; set; }
        public virtual Company Company { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
