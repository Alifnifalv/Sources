using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerMaster
    {
        public BannerMaster()
        {
            this.BannerLogs = new List<BannerLog>();
            this.BannerPages = new List<BannerPage1>();
            this.BannerPages1 = new List<BannerPage>();
        }

        public int BannerID { get; set; }
        public string BannerName { get; set; }
        public string BannerFile { get; set; }
        public string BannerType { get; set; }
        public byte Frequency { get; set; }
        public string Position { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        public bool Active { get; set; }
        public string BannerNameAr { get; set; }
        public virtual ICollection<BannerLog> BannerLogs { get; set; }
        public virtual ICollection<BannerPage1> BannerPages { get; set; }
        public virtual ICollection<BannerPage> BannerPages1 { get; set; }
    }
}
