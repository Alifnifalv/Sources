using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerType
    {
        public BannerType()
        {
            this.Banners = new List<Banner>();
        }

        public int BannerTypeID { get; set; }
        public string BannerTypeName { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}
