using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryBannerMasterMap
    {
        public int MapID { get; set; }
        public Nullable<int> RefBannerID { get; set; }
        public string MapArea { get; set; }
        public string MapTitle { get; set; }
        public string MapLink { get; set; }
        public string MapTarget { get; set; }
    }
}
