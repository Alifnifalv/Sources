using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SpecialOfferBannerMaster
    {
        public int BannerID { get; set; }
        public string BannerName { get; set; }
        public string ThumbFile { get; set; }
        public string BannerFile { get; set; }
        public Nullable<short> Position { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        public bool Active { get; set; }
        public Nullable<bool> UseMap { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string BannerNameAr { get; set; }
        public string LinkAr { get; set; }
        public string Lang { get; set; }
    }
}
