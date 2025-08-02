using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SiteCountryMap
    {
        public int SiteCountryMapIID { get; set; }
        public int SiteID { get; set; }
        public int CountryID { get; set; }
        public Nullable<bool> IsLocal { get; set; }
        public virtual Country Country { get; set; }
        public virtual Site Site { get; set; }
    }
}
