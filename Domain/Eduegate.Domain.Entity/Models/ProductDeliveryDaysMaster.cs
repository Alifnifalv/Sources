using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDeliveryDaysMaster
    {
        [Key]
        public int DeliveryDaysID { get; set; }
        public int DeliveryDays { get; set; }
    }
}
