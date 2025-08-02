using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCarts", Schema = "inventory")]
    public partial class ShoppingCart1
    {
        public ShoppingCart1()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
            ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            ShoppingCartWeekDayMaps = new HashSet<ShoppingCartWeekDayMap>();
            TransactionHeadShoppingCartMaps = new HashSet<TransactionHeadShoppingCartMap>();
        }

        [Key]
        public long ShoppingCartIID { get; set; }
        [Required]
        [StringLength(100)]
        public string CartID { get; set; }
        public int? CartStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(20)]
        public string PaymentMethod { get; set; }
        public long? ShippingAddressID { get; set; }
        public long? BillingAddressID { get; set; }
        public bool? IsInventoryBlocked { get; set; }
        [Column(TypeName = "datetime")]
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
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LoyaltyAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RadeemPoint { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ScheduledDateTime { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? VoucherAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? WalletAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualDeliveryCharge { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge { get; set; }
        public string OrderNotes { get; set; }
        public long? StudentID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public short? SubscriptionTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? DeliveryDaysCount { get; set; }
        public short? DayID { get; set; }
        public byte? DeliveryOrderTypeID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ShoppingCart1")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("BlockedHeadID")]
        [InverseProperty("ShoppingCart1")]
        public virtual TransactionHead BlockedHead { get; set; }
        [ForeignKey("DeliveryOrderTypeID")]
        [InverseProperty("ShoppingCart1")]
        public virtual DeliveryOrderType DeliveryOrderType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ShoppingCart1")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("ShoppingCart1")]
        public virtual Student Student { get; set; }
        [ForeignKey("SubscriptionTypeID")]
        [InverseProperty("ShoppingCart1")]
        public virtual SubscriptionType SubscriptionType { get; set; }
        [InverseProperty("ShoppingCart")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }
        [InverseProperty("ShoppingCart")]
        public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }
        [InverseProperty("ShoppingCart")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        [InverseProperty("ShoppingCart")]
        public virtual ICollection<ShoppingCartWeekDayMap> ShoppingCartWeekDayMaps { get; set; }
        [InverseProperty("ShoppingCart")]
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
    }
}
