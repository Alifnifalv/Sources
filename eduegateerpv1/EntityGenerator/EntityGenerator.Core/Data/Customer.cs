using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Customers", Schema = "mutual")]
    [Index("LoginID", Name = "IDX_Customers_LoginID_")]
    [Index("StatusID", Name = "IDX_Customers_StatusID_")]
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
            CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            CustomerCards = new HashSet<CustomerCard>();
            CustomerProductReferences = new HashSet<CustomerProductReference>();
            CustomerSettings = new HashSet<CustomerSetting>();
            CustomerSupplierMaps = new HashSet<CustomerSupplierMap>();
            PaymentDetailsPayPals = new HashSet<PaymentDetailsPayPal>();
            PaymentDetailsTheForts = new HashSet<PaymentDetailsTheFort>();
            ProductPriceListCustomerMaps = new HashSet<ProductPriceListCustomerMap>();
            ReferFriendTokens = new HashSet<ReferFriendToken>();
            ReferralReferredCustomers = new HashSet<Referral>();
            ReferralReferrerCustomers = new HashSet<Referral>();
            SalesPromotionLogs = new HashSet<SalesPromotionLog>();
            SalesPromotions = new HashSet<SalesPromotion>();
            SegmentCustomerMaps = new HashSet<SegmentCustomerMap>();
            ShareHolders = new HashSet<ShareHolder>();
            Tickets = new HashSet<Ticket>();
            TransactionHeads = new HashSet<TransactionHead>();
            Vouchers = new HashSet<Voucher>();
            WishLists = new HashSet<WishList>();
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? HowKnowOptionID { get; set; }
        [StringLength(200)]
        public string HowKnowText { get; set; }
        public long? CountryID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Telephone { get; set; }
        public long? ProductManagerID { get; set; }
        public bool? IsMigrated { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        public string CustomerCode { get; set; }
        [StringLength(200)]
        public string CustomerEmail { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ShareHolderID { get; set; }
        [StringLength(1000)]
        public string CustomerAddress { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AddressLatitude { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AddressLongitude { get; set; }
        public byte? GenderID { get; set; }
        public long? DefaultBranchID { get; set; }
        public long? DefaultStudentID { get; set; }

        [ForeignKey("DefaultStudentID")]
        [InverseProperty("Customers")]
        public virtual Student DefaultStudent { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("Customers")]
        public virtual Login Login { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerCard> CustomerCards { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerSetting> CustomerSettings { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        [InverseProperty("RefCustomer")]
        public virtual ICollection<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<PaymentDetailsTheFort> PaymentDetailsTheForts { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<ReferFriendToken> ReferFriendTokens { get; set; }
        [InverseProperty("ReferredCustomer")]
        public virtual ICollection<Referral> ReferralReferredCustomers { get; set; }
        [InverseProperty("ReferrerCustomer")]
        public virtual ICollection<Referral> ReferralReferrerCustomers { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<SalesPromotionLog> SalesPromotionLogs { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<SalesPromotion> SalesPromotions { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<SegmentCustomerMap> SegmentCustomerMaps { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<ShareHolder> ShareHolders { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Voucher> Vouchers { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
