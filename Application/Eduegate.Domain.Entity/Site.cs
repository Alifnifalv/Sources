namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.Sites")]
    public partial class Site
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Site()
        {
            MenuLinkCategoryMaps = new HashSet<MenuLinkCategoryMap>();
            Pages = new HashSet<Page>();
            SiteCountryMaps = new HashSet<SiteCountryMap>();
            Notifies = new HashSet<Notify>();
            OrderDeliveryHolidayHeads = new HashSet<OrderDeliveryHolidayHead>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SiteID { get; set; }

        [StringLength(50)]
        public string SiteName { get; set; }

        public long? MasterPageID { get; set; }

        public long? HomePageID { get; set; }

        public int? CompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page> Pages { get; set; }

        public virtual Page Page { get; set; }

        public virtual Page Page1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notify> Notifies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }

        public virtual Company Company { get; set; }
    }
}
