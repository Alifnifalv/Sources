namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.OrderDeliveryHolidayHead")]
    public partial class OrderDeliveryHolidayHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public virtual Site Site { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDeliveryHolidayDetail> OrderDeliveryHolidayDetails { get; set; }

        public virtual Company Company { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
