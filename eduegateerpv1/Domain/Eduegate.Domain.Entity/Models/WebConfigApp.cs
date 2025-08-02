using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class WebConfigApp
    {
        public short ParamID { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
        public Nullable<short> RefUserID { get; set; }
        public Nullable<System.DateTime> Updatedon { get; set; }
    }
}
