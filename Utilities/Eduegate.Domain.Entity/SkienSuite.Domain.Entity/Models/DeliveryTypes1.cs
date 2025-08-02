using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryTypes1
    {
        public DeliveryTypes1()
        {
            this.OrderDeliveryHolidayHeads = new List<OrderDeliveryHolidayHead>();
            this.CustomerGroupDeliveryTypeMaps = new List<CustomerGroupDeliveryTypeMap>();
            this.DeliveryCharges = new List<DeliveryCharge>();
            this.DeliveryTypeAllowedAreaMaps = new List<DeliveryTypeAllowedAreaMap>();
            this.DeliveryTypeAllowedCountryMaps = new List<DeliveryTypeAllowedCountryMap>();
            this.DeliveryTypeAllowedZoneMaps = new List<DeliveryTypeAllowedZoneMap>();
            this.DeliveryTypeTimeSlotMaps = new List<DeliveryTypeTimeSlotMap>();
            this.ProductDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            this.ProductTypeDeliveryTypeMaps = new List<ProductTypeDeliveryTypeMap>();
            this.TransactionHeads = new List<TransactionHead>();
        }

        public int DeliveryTypeID { get; set; }
        public string DeliveryTypeName { get; set; }
        public string Description { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> Days { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<bool> IsLocal { get; set; }
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual ICollection<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        public virtual DeliveryTypeStatus DeliveryTypeStatus { get; set; }
        public virtual ICollection<DeliveryTypeTimeSlotMap> DeliveryTypeTimeSlotMaps { get; set; }
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
