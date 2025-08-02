using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ExpressDeliverySlotsHoliday
    {
        [Key]
        public int TimeID { get; set; }
        public System.DateTime TimeFrom { get; set; }
        public System.DateTime TimeTo { get; set; }
    }
}
