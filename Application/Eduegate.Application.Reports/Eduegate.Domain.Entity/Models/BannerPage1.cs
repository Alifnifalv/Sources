using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerPage1
    {
        public long BannerPagesID { get; set; }
        public int BannerID { get; set; }
        public int DisplayID { get; set; }
        public string Language { get; set; }
        public string DisplayOn { get; set; }
        public virtual BannerMaster BannerMaster { get; set; }
    }
}
