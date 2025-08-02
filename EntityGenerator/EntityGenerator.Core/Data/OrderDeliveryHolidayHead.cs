using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OrderDeliveryHolidayHead", Schema = "distribution")]
    public partial class OrderDeliveryHolidayHead
    {
        public OrderDeliveryHolidayHead()
        {
            OrderDeliveryHolidayDetails = new HashSet<OrderDeliveryHolidayDetail>();
        }

        [Key]
        public long OrderDeliveryHolidayHeadIID { get; set; }
        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }
        public int? Year { get; set; }
        public int? DeliveryTypeID { get; set; }
        public byte? WeekEndDayID1 { get; set; }
        public byte? WeekEndDayID2 { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("OrderDeliveryHolidayHeads")]
        public virtual Company Company { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("OrderDeliveryHolidayHeads")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("SiteID")]
        [InverseProperty("OrderDeliveryHolidayHeads")]
        public virtual Site Site { get; set; }
        [InverseProperty("OrderDeliveryHolidayHead")]
        public virtual ICollection<OrderDeliveryHolidayDetail> OrderDeliveryHolidayDetails { get; set; }
    }
}
