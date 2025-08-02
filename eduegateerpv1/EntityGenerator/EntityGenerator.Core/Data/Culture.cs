using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Cultures", Schema = "mutual")]
    public partial class Culture
    {
        public Culture()
        {
            AreaCultureDatas = new HashSet<AreaCultureData>();
            BranchCultureDatas = new HashSet<BranchCultureData>();
            BrandCultureDatas = new HashSet<BrandCultureData>();
            CashChangeCultureDatas = new HashSet<CashChangeCultureData>();
            CategoryCultureDatas = new HashSet<CategoryCultureData>();
            CultureDatas = new HashSet<CultureData>();
            CustomerJustAsks = new HashSet<CustomerJustAsk>();
            CustomerSupportTickets = new HashSet<CustomerSupportTicket>();
            DeliveryTypeCultureDatas = new HashSet<DeliveryTypeCultureData>();
            DeliveryTypeCutOffSlotCultureDatas = new HashSet<DeliveryTypeCutOffSlotCultureData>();
            DeliveryTypeTimeSlotMapsCultures = new HashSet<DeliveryTypeTimeSlotMapsCulture>();
            FilterColumnCultureDatas = new HashSet<FilterColumnCultureData>();
            MenuLinkCultureDatas = new HashSet<MenuLinkCultureData>();
            NewsletterSubscriptions = new HashSet<NewsletterSubscription>();
            OrderDeliveryDisplayHeadMaps = new HashSet<OrderDeliveryDisplayHeadMap>();
            PageBoilerplateMapParameterCultureDatas = new HashSet<PageBoilerplateMapParameterCultureData>();
            PaymentMethodCultureDatas = new HashSet<PaymentMethodCultureData>();
            PermissionCultureDatas = new HashSet<PermissionCultureData>();
            ProductCultureDatas = new HashSet<ProductCultureData>();
            ProductFamilyCultureDatas = new HashSet<ProductFamilyCultureData>();
            ProductFamilyTypes = new HashSet<ProductFamilyType>();
            ProductInventoryConfigCultureDatas = new HashSet<ProductInventoryConfigCultureData>();
            ProductOptionCultureDatas = new HashSet<ProductOptionCultureData>();
            ProductSKUCultureDatas = new HashSet<ProductSKUCultureData>();
            PropertyCultureDatas = new HashSet<PropertyCultureData>();
            RoleCultureDatas = new HashSet<RoleCultureData>();
            SalesRelationshipTypes = new HashSet<SalesRelationshipType>();
            ScreenMetadataCultureDatas = new HashSet<ScreenMetadataCultureData>();
            SeoMetadataCultureDatas = new HashSet<SeoMetadataCultureData>();
            ShoppingCartActivityLogCultreDatas = new HashSet<ShoppingCartActivityLogCultreData>();
            StatusesCultureDatas = new HashSet<StatusesCultureData>();
            ViewColumnCultureDatas = new HashSet<ViewColumnCultureData>();
            ViewCultureDatas = new HashSet<ViewCultureData>();
        }

        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CultureCode { get; set; }
        [StringLength(100)]
        public string CultureName { get; set; }

        [InverseProperty("Culture")]
        public virtual ICollection<AreaCultureData> AreaCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<BranchCultureData> BranchCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<BrandCultureData> BrandCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<CashChangeCultureData> CashChangeCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<CategoryCultureData> CategoryCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<CultureData> CultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<CustomerJustAsk> CustomerJustAsks { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<CustomerSupportTicket> CustomerSupportTickets { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<DeliveryTypeCultureData> DeliveryTypeCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<DeliveryTypeCutOffSlotCultureData> DeliveryTypeCutOffSlotCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<FilterColumnCultureData> FilterColumnCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<PaymentMethodCultureData> PaymentMethodCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ProductCultureData> ProductCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ProductFamilyType> ProductFamilyTypes { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ProductOptionCultureData> ProductOptionCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<SalesRelationshipType> SalesRelationshipTypes { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
        [InverseProperty("Cultre")]
        public virtual ICollection<ShoppingCartActivityLogCultreData> ShoppingCartActivityLogCultreDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<StatusesCultureData> StatusesCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ViewColumnCultureData> ViewColumnCultureDatas { get; set; }
        [InverseProperty("Culture")]
        public virtual ICollection<ViewCultureData> ViewCultureDatas { get; set; }
    }
}
