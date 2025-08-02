using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OrderDeliveryHolidayDetails", Schema = "distribution")]
    public partial class OrderDeliveryHolidayDetail
    {
        [Key]
        public long OrderDeliveryHolidayDetailsIID { get; set; }
        public long? OrderDeliveryHolidayHeadID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? HolidayDate { get; set; }

        [ForeignKey("OrderDeliveryHolidayHeadID")]
        [InverseProperty("OrderDeliveryHolidayDetails")]
        public virtual OrderDeliveryHolidayHead OrderDeliveryHolidayHead { get; set; }
    }
}
