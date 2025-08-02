using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WebConfig
    {
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public Nullable<System.DateTime> Updatedon { get; set; }
    }
}
