using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Culture
    {
        public Culture()
        {
            this.PermissionCultureDatas = new List<PermissionCultureData>();
            this.RoleCultureDatas = new List<RoleCultureData>();
            this.BrandCultureDatas = new List<BrandCultureData>();
            this.CategoryCultureDatas = new List<CategoryCultureData>();
            this.ProductCultureDatas = new List<ProductCultureData>();
            this.ProductFamilyCultureDatas = new List<ProductFamilyCultureData>();
            this.ProductFamilyTypes = new List<ProductFamilyType>();
            this.ProductInventoryConfigCultureDatas = new List<ProductInventoryConfigCultureData>();
            this.ProductSKUCultureDatas = new List<ProductSKUCultureData>();
            this.PropertyCultureDatas = new List<PropertyCultureData>();
            this.SalesRelationshipTypes = new List<SalesRelationshipType>();
            this.CustomerJustAsks = new List<CustomerJustAsk>();
            this.CustomerSupportTickets = new List<CustomerSupportTicket>();
            this.NewsletterSubscriptions = new List<NewsletterSubscription>();
            this.PageBoilerplateMapParameterCultureDatas = new List<PageBoilerplateMapParameterCultureData>();
            this.OrderDeliveryDisplayHeadMaps = new List<OrderDeliveryDisplayHeadMap>();
            this.DeliveryTypeTimeSlotMapsCultures = new List<DeliveryTypeTimeSlotMapsCulture>();
            this.CultureDatas = new List<CultureData>();
            this.Languages = new List<Language>();
            this.SeoMetadataCultureDatas = new List<SeoMetadataCultureData>();
            this.StatusesCultureDatas = new List<StatusesCultureData>();
        }

        public byte CultureID { get; set; }
        public string CultureCode { get; set; }
        public string CultureName { get; set; }
        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }
        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }
        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }
        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }
        public virtual ICollection<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        public virtual ICollection<ProductFamilyType> ProductFamilyTypes { get; set; }
        public virtual ICollection<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }
        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }
        public virtual ICollection<SalesRelationshipType> SalesRelationshipTypes { get; set; }
        public virtual ICollection<CustomerJustAsk> CustomerJustAsks { get; set; }
        public virtual ICollection<CustomerSupportTicket> CustomerSupportTickets { get; set; }
        public virtual ICollection<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
        public virtual ICollection<CultureData> CultureDatas { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
        public virtual ICollection<StatusesCultureData> StatusesCultureDatas { get; set; }
    }
}
