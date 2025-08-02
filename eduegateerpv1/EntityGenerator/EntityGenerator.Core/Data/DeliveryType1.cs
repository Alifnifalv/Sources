using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypes", Schema = "inventory")]
    public partial class DeliveryType1
    {
        public DeliveryType1()
        {
            CustomerGroupDeliveryTypeMaps = new HashSet<CustomerGroupDeliveryTypeMap>();
            DeliveryCharges = new HashSet<DeliveryCharge>();
            DeliveryTypeAllowedAreaMaps = new HashSet<DeliveryTypeAllowedAreaMap>();
            DeliveryTypeAllowedCountryMaps = new HashSet<DeliveryTypeAllowedCountryMap>();
            DeliveryTypeAllowedZoneMaps = new HashSet<DeliveryTypeAllowedZoneMap>();
            DeliveryTypeCultureDatas = new HashSet<DeliveryTypeCultureData>();
            DeliveryTypeCutOffSlots = new HashSet<DeliveryTypeCutOffSlot>();
            DeliveryTypeGeoMaps = new HashSet<DeliveryTypeGeoMap>();
            DeliveryTypeTimeSlotMaps = new HashSet<DeliveryTypeTimeSlotMap>();
            OrderDeliveryHolidayHeads = new HashSet<OrderDeliveryHolidayHead>();
            PaymentExceptionByZoneDeliveries = new HashSet<PaymentExceptionByZoneDelivery>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductTypeDeliveryTypeMaps = new HashSet<ProductTypeDeliveryTypeMap>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public int DeliveryTypeID { get; set; }
        [StringLength(100)]
        public string DeliveryTypeName { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int? Priority { get; set; }
        public byte? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? Days { get; set; }
        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }
        public bool? IsLocal { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DistanceEndRange { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DistanceStartRange { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndCutOffTime { get; set; }
        public bool? HasTimeSlot { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumCapAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartCutOffTime { get; set; }
        public long? DeliveryTypeCategoryID { get; set; }

        [ForeignKey("DeliveryTypeCategoryID")]
        [InverseProperty("DeliveryType1")]
        public virtual Category DeliveryTypeCategory { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("DeliveryType1")]
        public virtual DeliveryTypeStatus Status { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeCultureData> DeliveryTypeCultureDatas { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeCutOffSlot> DeliveryTypeCutOffSlots { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeGeoMap> DeliveryTypeGeoMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<DeliveryTypeTimeSlotMap> DeliveryTypeTimeSlotMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<PaymentExceptionByZoneDelivery> PaymentExceptionByZoneDeliveries { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        [InverseProperty("DeliveryType")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
