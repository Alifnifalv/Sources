using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models.Mutual
{
    [Table("Customers", Schema = "mutual")]
    public partial class Customer
    {
        public Customer()
        {
            //Appointments = new HashSet<Appointment>();
            //Contacts = new HashSet<Contact>();
            //CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            //CustomerCards = new HashSet<CustomerCard>();
            //CustomerProductReferences = new HashSet<CustomerProductReference>();
            //CustomerRedeemPoints = new HashSet<CustomerRedeemPoint>();
            //CustomerSettings = new HashSet<CustomerSetting>();
            //CustomerSupplierMaps = new HashSet<CustomerSupplierMap>();
            //PaymentDetailsPayPals = new HashSet<PaymentDetailsPayPal>();
            //PaymentDetailsTheForts = new HashSet<PaymentDetailsTheFort>();
            //ProductPriceListCustomerMaps = new HashSet<ProductPriceListCustomerMap>();
            //ReferFriendTokens = new HashSet<ReferFriendToken>();
            //ReferralReferredCustomers = new HashSet<Referral>();
            //ReferralReferrerCustomers = new HashSet<Referral>();
            //SalesPromotionLogs = new HashSet<SalesPromotionLog>();
            //SalesPromotions = new HashSet<SalesPromotion>();
            //SegmentCustomerMaps = new HashSet<SegmentCustomerMap>();
            //ShareHolders = new HashSet<ShareHolder>();
            Tickets = new HashSet<Ticket>();
            //TransactionHeads = new HashSet<TransactionHead>();
            //Vouchers = new HashSet<Voucher>();
            //WalletTransactions = new HashSet<WalletTransaction>();
            //WishLists = new HashSet<WishList>();
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
        [Column( TypeName = "date")]
        public DateTime? CRExpiryDate { get; set; }
        [StringLength(100)]
        public string PassportNumber { get; set; }
        [Column("ParentCustomerID")]
        public long? ParentCustomerId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
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

        //public virtual Branch DefaultBranch { get; set; }
        public virtual CustomerGroup Group { get; set; }
        public virtual Login Login { get; set; }
        //public virtual CustomerStatus Status { get; set; }
        //public virtual ICollection<Appointment> Appointments { get; set; }
        //public virtual ICollection<Contact> Contacts { get; set; }
        //public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        //public virtual ICollection<CustomerCard> CustomerCards { get; set; }
        //public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }
        //public virtual ICollection<CustomerRedeemPoint> CustomerRedeemPoints { get; set; }
        //public virtual ICollection<CustomerSetting> CustomerSettings { get; set; }
        //public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        //public virtual ICollection<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }
        //public virtual ICollection<PaymentDetailsTheFort> PaymentDetailsTheForts { get; set; }
        //public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        //public virtual ICollection<ReferFriendToken> ReferFriendTokens { get; set; }
        //public virtual ICollection<Referral> ReferralReferredCustomers { get; set; }
        //public virtual ICollection<Referral> ReferralReferrerCustomers { get; set; }
        //public virtual ICollection<SalesPromotionLog> SalesPromotionLogs { get; set; }
        //public virtual ICollection<SalesPromotion> SalesPromotions { get; set; }
        //public virtual ICollection<SegmentCustomerMap> SegmentCustomerMaps { get; set; }
        //public virtual ICollection<ShareHolder> ShareHolders { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        //public virtual ICollection<Voucher> Vouchers { get; set; }
        //public virtual ICollection<WalletTransaction> WalletTransactions { get; set; }
        //public virtual ICollection<WishList> WishLists { get; set; }
    }
}