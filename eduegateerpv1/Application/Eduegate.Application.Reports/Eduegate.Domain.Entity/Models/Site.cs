using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Site
    {
        public Site()
        {
            this.Pages = new List<Page>();
            this.SiteCountryMaps = new List<SiteCountryMap>();
            this.PaymentMethodSiteMaps = new List<PaymentMethodSiteMap>();
            this.Notifies = new List<Notify>();
            this.ProductSKUSiteMaps = new List<ProductSKUSiteMap>();
        }

        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public Nullable<long> MasterPageID { get; set; }
        public Nullable<long> HomePageID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual Page Page { get; set; }
        public virtual Page Page1 { get; set; }
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }
        public virtual ICollection<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }
        public virtual Company Company { get; set; }
        public virtual PaymentGroup PaymentGroup { get; set; }
        //public virtual Login Login { get; set; }
        public virtual ICollection<Notify> Notifies { get; set; }
        public virtual ICollection<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }
    }
}
