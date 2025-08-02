using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerGroupDeliverySetting
    {
        public long CustomerGroupIID { get; set; }
        public string GroupName { get; set; }
        public Nullable<decimal> PointLimit { get; set; }
    }
}
