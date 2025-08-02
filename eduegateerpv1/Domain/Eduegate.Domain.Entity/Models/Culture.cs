using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Cultures", Schema = "mutual")]
    public partial class Culture
    {
        public Culture()
        {
            this.PermissionCultureDatas = new List<PermissionCultureData>();
            this.RoleCultureDatas = new List<RoleCultureData>();
            this.BrandCultureDatas = new List<BrandCultureData>();
            this.Categories = new List<Category>();
            this.CategoryCultureDatas = new List<CategoryCultureData>();
            this.ProductCultureDatas = new List<ProductCultureData>();
            this.ProductFamilyCultureDatas = new List<ProductFamilyCultureData>();
            this.ProductFamilyTypes = new List<ProductFamilyType>();
            this.ProductSKUCultureDatas = new List<ProductSKUCultureData>();
            this.ProductPriceListCultureDatas = new List<ProductPriceListCultureData>();
            this.PropertyCultureDatas = new List<PropertyCultureData>();
            this.SalesRelationshipTypes = new List<SalesRelationshipType>();
            this.MenuLinkCultureDatas = new List<MenuLinkCultureData>();
            this.CultureDatas = new List<CultureData>();
            this.StatusesCultureDatas = new List<StatusesCultureData>();
            this.SeoMetadataCultureDatas = new List<SeoMetadataCultureData>();
            this.ProductInventoryConfigCultureDatas = new List<ProductInventoryConfigCultureData>();
            this.PageBoilerplateMapParameterCultureDatas = new List<PageBoilerplateMapParameterCultureData>();
            this.CultureDatas = new List<CultureData>();
            this.OrderDeliveryDisplayHeadMaps = new List<OrderDeliveryDisplayHeadMap>();
            this.BranchCultureDatas = new List<BranchCultureData>();
            ViewColumnCultureDatas = new HashSet<ViewColumnCultureData>();
            FilterColumnCultureDatas = new HashSet<FilterColumnCultureData>();
            DeliveryTypeCultureDatas = new HashSet<DeliveryTypeCultureData>();
            ViewCultureDatas = new HashSet<ViewCultureData>();
            ScreenMetadataCultureDatas = new HashSet<ScreenMetadataCultureData>();

        }

        [Key]
        public byte CultureID { get; set; }

        public string CultureCode { get; set; }

        public string CultureName { get; set; }

        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }

        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }

        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }

        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }

        public virtual ICollection<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }

        public virtual ICollection<ProductFamilyType> ProductFamilyTypes { get; set; }

        public virtual ICollection<ProductPriceListCultureData> ProductPriceListCultureDatas { get; set; }

        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }

        public virtual ICollection<SalesRelationshipType> SalesRelationshipTypes { get; set; }

        public virtual ICollection<CustomerJustAsk> CustomerJustAsks { get; set; }

        public virtual ICollection<CustomerSupportTicket> CustomerSupportTickets { get; set; }

        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }

        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }

        public virtual ICollection<CultureData> CultureDatas { get; set; }

        public virtual ICollection<StatusesCultureData> StatusesCultureDatas { get; set; }

        public virtual ICollection<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }

        public virtual ICollection<NewsletterSubscription> NewsletterSubscriptions { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }

        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }

        public virtual ICollection<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }

        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }

        public virtual ICollection<BranchCultureData> BranchCultureDatas { get; set; }

        public virtual ICollection<ViewColumnCultureData> ViewColumnCultureDatas { get; set; }

        public virtual ICollection<FilterColumnCultureData> FilterColumnCultureDatas { get; set; }

        public virtual ICollection<DeliveryTypeCultureData> DeliveryTypeCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewCultureData> ViewCultureDatas { get; set; }
    }
}