using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductItunesLog
    {
        public int LogID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string RequestPage { get; set; }
        public Nullable<System.DateTime> RequestOn { get; set; }
    }
}
