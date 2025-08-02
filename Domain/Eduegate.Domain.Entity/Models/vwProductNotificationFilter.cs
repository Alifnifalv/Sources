using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductNotificationFilter
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductPartNo { get; set; }
        public string RequestedOn { get; set; }
    }
}
