using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerPage
    {
        public int BannerPageID { get; set; }
        public int BannerID { get; set; }
        public int PageID { get; set; }
        public virtual BannerMaster BannerMaster { get; set; }
    }
}
