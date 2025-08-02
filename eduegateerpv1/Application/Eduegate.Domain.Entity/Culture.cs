namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Cultures")]
    public partial class Culture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Culture()
        {
            PermissionCultureDatas = new HashSet<PermissionCultureData>();
            RoleCultureDatas = new HashSet<RoleCultureData>();
            BrandCultureDatas = new HashSet<BrandCultureData>();
            CategoryCultureDatas = new HashSet<CategoryCultureData>();
            ProductCultureDatas = new HashSet<ProductCultureData>();
            ProductFamilyCultureDatas = new HashSet<ProductFamilyCultureData>();
            ProductFamilyTypes = new HashSet<ProductFamilyType>();
            ProductInventoryConfigCultureDatas = new HashSet<ProductInventoryConfigCultureData>();
            ProductOptionCultureDatas = new HashSet<ProductOptionCultureData>();
            ProductSKUCultureDatas = new HashSet<ProductSKUCultureData>();
            PropertyCultureDatas = new HashSet<PropertyCultureData>();
            SalesRelationshipTypes = new HashSet<SalesRelationshipType>();
            CustomerJustAsks = new HashSet<CustomerJustAsk>();
            CustomerSupportTickets = new HashSet<CustomerSupportTicket>();
            NewsletterSubscriptions = new HashSet<NewsletterSubscription>();
            PageBoilerplateMapParameterCultureDatas = new HashSet<PageBoilerplateMapParameterCultureData>();
            OrderDeliveryDisplayHeadMaps = new HashSet<OrderDeliveryDisplayHeadMap>();
            CashChangeCultureDatas = new HashSet<CashChangeCultureData>();
            DeliveryTypeCultureDatas = new HashSet<DeliveryTypeCultureData>();
            DeliveryTypeCutOffSlotCultureDatas = new HashSet<DeliveryTypeCutOffSlotCultureData>();
            DeliveryTypeTimeSlotMapsCultures = new HashSet<DeliveryTypeTimeSlotMapsCulture>();
            ShoppingCartActivityLogCultreDatas = new HashSet<ShoppingCartActivityLogCultreData>();
            AreaCultureDatas = new HashSet<AreaCultureData>();
            BranchCultureDatas = new HashSet<BranchCultureData>();
            CultureDatas = new HashSet<CultureData>();
            FilterColumnCultureDatas = new HashSet<FilterColumnCultureData>();
            MenuLinkCultureDatas = new HashSet<MenuLinkCultureData>();
            PaymentMethodCultureDatas = new HashSet<PaymentMethodCultureData>();
            ScreenMetadataCultureDatas = new HashSet<ScreenMetadataCultureData>();
            SeoMetadataCultureDatas = new HashSet<SeoMetadataCultureData>();
            StatusesCultureDatas = new HashSet<StatusesCultureData>();
            ViewColumnCultureDatas = new HashSet<ViewColumnCultureData>();
            ViewCultureDatas = new HashSet<ViewCultureData>();
        }

        public byte CultureID { get; set; }

        [StringLength(50)]
        public string CultureCode { get; set; }

        [StringLength(100)]
        public string CultureName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductFamilyType> ProductFamilyTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOptionCultureData> ProductOptionCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesRelationshipType> SalesRelationshipTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerJustAsk> CustomerJustAsks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSupportTicket> CustomerSupportTickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsletterSubscription> NewsletterSubscriptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashChangeCultureData> CashChangeCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCultureData> DeliveryTypeCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCutOffSlotCultureData> DeliveryTypeCutOffSlotCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartActivityLogCultreData> ShoppingCartActivityLogCultreDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AreaCultureData> AreaCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BranchCultureData> BranchCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CultureData> CultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumnCultureData> FilterColumnCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentMethodCultureData> PaymentMethodCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StatusesCultureData> StatusesCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewColumnCultureData> ViewColumnCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewCultureData> ViewCultureDatas { get; set; }
    }
}
