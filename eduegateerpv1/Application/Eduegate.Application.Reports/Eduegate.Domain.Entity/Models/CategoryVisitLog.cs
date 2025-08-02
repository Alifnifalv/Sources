using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryVisitLog
    {
        public int LogID { get; set; }
        public long CategoryID { get; set; }
        public System.DateTime VisitOn { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
        public Nullable<long> CustomerID { get; set; }
    }
}
