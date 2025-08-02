using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ExpressDeliverySlotsHoliday
    {
        public int TimeID { get; set; }
        public System.DateTime TimeFrom { get; set; }
        public System.DateTime TimeTo { get; set; }
    }
}
