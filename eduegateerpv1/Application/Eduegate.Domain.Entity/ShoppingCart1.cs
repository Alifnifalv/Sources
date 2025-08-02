namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCarts")]
    public partial class ShoppingCart1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingCart1()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
            ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            ShoppingCartVoucherMaps = new HashSet<ShoppingCartVoucherMap>();
            ShoppingCartWeekDayMaps = new HashSet<ShoppingCartWeekDayMap>();
            TransactionHeadShoppingCartMaps = new HashSet<TransactionHeadShoppingCartMap>();
        }

        [Key]
        public long ShoppingCartIID { get; set; }

        [Required]
        [StringLength(100)]
        public string CartID { get; set; }

        public int? CartStatusID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(20)]
        public string PaymentMethod { get; set; }

        public long? ShippingAddressID { get; set; }

        public long? BillingAddressID { get; set; }

        public bool? IsInventoryBlocked { get; set; }

        public DateTime? InventoryBlockedDateTime { get; set; }

        public long? BlockedHeadID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public short? PaymentGateWayID { get; set; }

        [StringLength(50)]
        public string CurrencyCode { get; set; }

        public int? CompanyID { get; set; }

        public int? SiteID { get; set; }

        public long? CreatedBy { get; set; }

        public bool? IsInternational { get; set; }

        public long? BranchID { get; set; }

        public int? CashChangeID { get; set; }

        public long? CustomerID { get; set; }

        public int? DeliveryDays { get; set; }

        public int? DeliveryTimeslotID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int? DisplayRange { get; set; }

        [StringLength(10)]
        public string LanguageCode { get; set; }

        public long? LoginID { get; set; }

        public decimal? LoyaltyAmount { get; set; }

        public decimal? RadeemPoint { get; set; }

        public DateTime? ScheduledDateTime { get; set; }

        public decimal? VoucherAmount { get; set; }

        public decimal? WalletAmount { get; set; }

        public decimal? ActualDeliveryCharge { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public string OrderNotes { get; set; }

        public long? StudentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public short? SubscriptionTypeID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DeliveryDaysCount { get; set; }

        public short? DayID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual Student Student { get; set; }

        public virtual SubscriptionType SubscriptionType { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartWeekDayMap> ShoppingCartWeekDayMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
    }
}
