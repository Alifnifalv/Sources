using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceEmployeeMap
    {
        public long ServiceEmployeeMapIID { get; set; }
        public Nullable<long> ServiceID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Service Service { get; set; }
    }
}
