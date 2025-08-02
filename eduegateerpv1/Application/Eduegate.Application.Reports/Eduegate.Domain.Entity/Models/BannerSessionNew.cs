using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerSessionNew
    {
        public long BannerID { get; set; }
        public string SessionID { get; set; }
        public long UniqueID { get; set; }
        public long LoopID { get; set; }
        public System.DateTime BannerTime { get; set; }
    }
}
