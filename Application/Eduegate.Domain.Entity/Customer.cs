namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Customers")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            CustomerProductReferences = new HashSet<CustomerProductReference>();
            ProductPriceListCustomerMaps = new HashSet<ProductPriceListCustomerMap>();
            Tickets = new HashSet<Ticket>();
            ShareHolders = new HashSet<ShareHolder>();
            TransactionHeads = new HashSet<TransactionHead>();
            Vouchers = new HashSet<Voucher>();
            WishLists = new HashSet<WishList>();
            ReferFriendTokens = new HashSet<ReferFriendToken>();
            Referrals = new HashSet<Referral>();
            Referrals1 = new HashSet<Referral>();
            SalesPromotionLogs = new HashSet<SalesPromotionLog>();
            SalesPromotions = new HashSet<SalesPromotion>();
            SegmentCustomerMaps = new HashSet<SegmentCustomerMap>();
            CustomerCards = new HashSet<CustomerCard>();
            Appointments = new HashSet<Appointment>();
            CustomerSettings = new HashSet<CustomerSetting>();
            CustomerSupplierMaps = new HashSet<CustomerSupplierMap>();
            PaymentDetailsPayPals = new HashSet<PaymentDetailsPayPal>();
            PaymentDetailsTheForts = new HashSet<PaymentDetailsTheFort>();
        }

        [Key]
        public long CustomerIID { get; set; }

        public long? LoginID { get; set; }

        public long? TitleID { get; set; }

        public long? GroupID { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string MiddleName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public bool? IsOfflineCustomer { get; set; }

        public bool? IsDifferentBillingAddress { get; set; }

        public bool? IsTermsAndConditions { get; set; }

        public bool? IsSubscribeForNewsLetter { get; set; }

        public byte? StatusID { get; set; }

        [StringLength(100)]
        public string CivilIDNumber { get; set; }

        public long? PassportIssueCountryID { get; set; }

        [StringLength(50)]
        public string CustomerCR { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CRExpiryDate { get; set; }

        [StringLength(100)]
        public string PassportNumber { get; set; }

        public long? ParentCustomerID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? HowKnowOptionID { get; set; }

        [StringLength(200)]
        public string HowKnowText { get; set; }

        public long? CountryID { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        public long? ProductManagerID { get; set; }

        public bool? IsMigrated { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(50)]
        public string CustomerCode { get; set; }

        [StringLength(200)]
        public string CustomerEmail { get; set; }

        [StringLength(50)]
        public string ShareHolderID { get; set; }

        [StringLength(1000)]
        public string CustomerAddress { get; set; }

        [StringLength(100)]
        public string AddressLatitude { get; set; }

        [StringLength(100)]
        public string AddressLongitude { get; set; }

        public byte? GenderID { get; set; }

        public long? DefaultBranchID { get; set; }

        public long? DefaultStudentID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }

        public virtual Login Login { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShareHolder> ShareHolders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voucher> Vouchers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishList> WishLists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReferFriendToken> ReferFriendTokens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Referral> Referrals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Referral> Referrals1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesPromotionLog> SalesPromotionLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesPromotion> SalesPromotions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SegmentCustomerMap> SegmentCustomerMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCard> CustomerCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual Student Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSetting> CustomerSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetailsTheFort> PaymentDetailsTheForts { get; set; }
    }
}
