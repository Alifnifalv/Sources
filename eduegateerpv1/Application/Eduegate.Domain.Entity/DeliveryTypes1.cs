namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypes")]
    public partial class DeliveryTypes1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryTypes1()
        {
            DeliveryTypeGeoMaps = new HashSet<DeliveryTypeGeoMap>();
            OrderDeliveryHolidayHeads = new HashSet<OrderDeliveryHolidayHead>();
            CustomerGroupDeliveryTypeMaps = new HashSet<CustomerGroupDeliveryTypeMap>();
            DeliveryCharges = new HashSet<DeliveryCharge>();
            DeliveryTypeAllowedAreaMaps = new HashSet<DeliveryTypeAllowedAreaMap>();
            DeliveryTypeAllowedCountryMaps = new HashSet<DeliveryTypeAllowedCountryMap>();
            DeliveryTypeAllowedZoneMaps = new HashSet<DeliveryTypeAllowedZoneMap>();
            DeliveryTypeCultureDatas = new HashSet<DeliveryTypeCultureData>();
            DeliveryTypeCutOffSlots = new HashSet<DeliveryTypeCutOffSlot>();
            DeliveryTypeTimeSlotMaps = new HashSet<DeliveryTypeTimeSlotMap>();
            PaymentExceptionByZoneDeliveries = new HashSet<PaymentExceptionByZoneDelivery>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductTypeDeliveryTypeMaps = new HashSet<ProductTypeDeliveryTypeMap>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeliveryTypeID { get; set; }

        [StringLength(100)]
        public string DeliveryTypeName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? Priority { get; set; }

        public byte? StatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? Days { get; set; }

        public int? CompanyID { get; set; }

        public int? SiteID { get; set; }

        public bool? IsLocal { get; set; }

        public decimal? DistanceEndRange { get; set; }

        public decimal? DistanceStartRange { get; set; }

        public DateTime? EndCutOffTime { get; set; }

        public bool? HasTimeSlot { get; set; }

        public decimal? MinimumCapAmount { get; set; }

        public DateTime? StartCutOffTime { get; set; }

        public long? DeliveryTypeCategoryID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeGeoMap> DeliveryTypeGeoMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCultureData> DeliveryTypeCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCutOffSlot> DeliveryTypeCutOffSlots { get; set; }

        public virtual DeliveryTypeStatus DeliveryTypeStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeTimeSlotMap> DeliveryTypeTimeSlotMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentExceptionByZoneDelivery> PaymentExceptionByZoneDeliveries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
