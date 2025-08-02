namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.OrderDeliveryHolidayDetails")]
    public partial class OrderDeliveryHolidayDetail
    {
        [Key]
        public long OrderDeliveryHolidayDetailsIID { get; set; }

        public long? OrderDeliveryHolidayHeadID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HolidayDate { get; set; }

        public virtual OrderDeliveryHolidayHead OrderDeliveryHolidayHead { get; set; }
    }
}
