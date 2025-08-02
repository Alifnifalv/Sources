using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ZoneSearchView
    {
        public short ZoneID { get; set; }
        public string ZoneName { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
