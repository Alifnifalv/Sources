using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Sites", Schema = "cms")]
    public partial class Site
    {
        public Site()
        {
            MenuLinkCategoryMaps = new HashSet<MenuLinkCategoryMap>();
            Notifies = new HashSet<Notify>();
            OrderDeliveryHolidayHeads = new HashSet<OrderDeliveryHolidayHead>();
            Pages = new HashSet<Page>();
            SiteCountryMaps = new HashSet<SiteCountryMap>();
        }

        [Key]
        public int SiteID { get; set; }
        [StringLength(50)]
        public string SiteName { get; set; }
        public long? MasterPageID { get; set; }
        public long? HomePageID { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("Sites")]
        public virtual Company Company { get; set; }
        [ForeignKey("HomePageID")]
        [InverseProperty("SiteHomePages")]
        public virtual Page HomePage { get; set; }
        [ForeignKey("MasterPageID")]
        [InverseProperty("SiteMasterPages")]
        public virtual Page MasterPage { get; set; }
        [InverseProperty("Site")]
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        [InverseProperty("Site")]
        public virtual ICollection<Notify> Notifies { get; set; }
        [InverseProperty("Site")]
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        [InverseProperty("Site")]
        public virtual ICollection<Page> Pages { get; set; }
        [InverseProperty("Site")]
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }
    }
}
