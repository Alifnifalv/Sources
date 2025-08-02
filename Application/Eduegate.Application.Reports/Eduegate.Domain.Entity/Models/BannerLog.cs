using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerLog
    {
        public int BannerLogID { get; set; }
        public Nullable<int> BannerID { get; set; }
        public string BannerAction { get; set; }
        public string IPAddress { get; set; }
        public string IPLocation { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public virtual BannerMaster BannerMaster { get; set; }
    }
}
