using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerSideAngle
    {
        public int BannerSideAngleID { get; set; }
        public string BannerSideAngleName { get; set; }
        public string BannerSideAngleFile { get; set; }
        public string BannerSideAnglePosition { get; set; }
        public string BannerSideAngleUrl { get; set; }
        public string BannerSideAngleTarget { get; set; }
        public bool BannerSideAngleStatus { get; set; }
        public string BannerSideAngleNameAr { get; set; }
        public string Lang { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
