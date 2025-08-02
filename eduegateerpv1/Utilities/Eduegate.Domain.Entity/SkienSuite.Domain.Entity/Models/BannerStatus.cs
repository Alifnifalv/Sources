using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerStatus
    {
        public BannerStatus()
        {
            this.Banners = new List<Banner>();
        }

        public int BannerStatusID { get; set; }
        public string BannerStatusName { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}
