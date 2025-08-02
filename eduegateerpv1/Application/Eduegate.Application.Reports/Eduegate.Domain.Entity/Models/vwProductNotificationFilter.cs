using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductNotificationFilter
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductPartNo { get; set; }
        public string RequestedOn { get; set; }
    }
}
