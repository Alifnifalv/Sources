using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMasterCoutureEmailLog
    {
        public long CoutureEmailLogID { get; set; }
        public long RefOrderID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
