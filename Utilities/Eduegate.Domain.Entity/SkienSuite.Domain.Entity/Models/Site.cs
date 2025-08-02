using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Site
    {
        public Site()
        {
            this.Pages = new List<Page>();
            this.SiteCountryMaps = new List<SiteCountryMap>();
            this.Notifies = new List<Notify>();
            this.OrderDeliveryHolidayHeads = new List<OrderDeliveryHolidayHead>();
        }

        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public Nullable<long> MasterPageID { get; set; }
        public Nullable<long> HomePageID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual Page Page { get; set; }
        public virtual Page Page1 { get; set; }
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }
        public virtual ICollection<Notify> Notifies { get; set; }
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        public virtual Company Company { get; set; }
    }
}
