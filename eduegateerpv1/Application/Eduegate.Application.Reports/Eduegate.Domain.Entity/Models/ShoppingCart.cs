using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            //ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
            //ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            ShoppingCartVoucherMaps = new HashSet<ShoppingCartVoucherMap>();
            TransactionHeadShoppingCartMaps = new HashSet<TransactionHeadShoppingCartMap>();
            ShoppingCartWeekDayMaps = new HashSet<ShoppingCartWeekDayMap>();
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

        public decimal? DeliveryCharge { get; set; }

        public decimal? ActualDeliveryCharge { get; set; }

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

        [StringLength(1)]
        public string OrderNotes { get; set; }

        public long? StudentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public short? SubscriptionTypeID { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DeliveryDaysCount { get; set; }

        

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }

        public virtual Student Student { get; set; }

        //public virtual Day Day { get; set; }

        public virtual SubscriptionType SubscriptionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartWeekDayMap> ShoppingCartWeekDayMaps { get; set; }
    }
}
