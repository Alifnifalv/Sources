using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Customers", Schema = "mutual")]
    public partial class Customer
    {
        public Customer()
        {
            this.CustomerAccountMaps = new List<CustomerAccountMap>();
            this.CustomerProductReferences = new List<CustomerProductReference>();
            this.ProductPriceListCustomerMaps = new List<ProductPriceListCustomerMap>();
            this.Tickets = new List<Ticket>();
            this.TransactionHeads = new List<TransactionHead>();
            this.Vouchers = new List<Voucher>();
            this.WishLists = new List<WishList>();
            this.Contacts = new List<Contact>();
            this.CustomerSettings = new List<CustomerSetting>();
            this.PaymentDetailsPayPals = new List<PaymentDetailsPayPal>();
            this.CustomerSupplierMaps = new List<CustomerSupplierMap>();
            this.PaymentDetailsTheForts = new List<PaymentDetailsTheFort>();
            //this.CustomerAccounts = new List<CustomerAccountMap>();
            this.CustomerCards = new HashSet<CustomerCard>();
        }

        [Key]
        public long CustomerIID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> TitleID { get; set; }
        public Nullable<long> GroupID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<bool> IsOfflineCustomer { get; set; }
        public Nullable<bool> IsDifferentBillingAddress { get; set; }
        public Nullable<bool> IsTermsAndConditions { get; set; }
        public Nullable<bool> IsSubscribeForNewsLetter { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string CivilIDNumber { get; set; }
        public Nullable<long> PassportIssueCountryID { get; set; }
        public string CustomerCR { get; set; }
        public Nullable<System.DateTime> CRExpiryDate { get; set; }
        public string PassportNumber { get; set; }
        public Nullable<long> ParentCustomerID { get; set; }
        public Nullable<long>ProductManagerID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public Nullable<long> HowKnowOptionID { get; set; }
        public string HowKnowText { get; set; }
        public Nullable<long> CountryID { get; set; }
        public string Telephone { get; set; }
        public Nullable<bool> IsMigrated { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerCode { get; set; }
        public long? DefaultBranchID { get; set; }
        public long? DefaultStudentID { get; set; }
        public string ShareHolderID { get; set; }
        public string CustomerAddress { get; set; }
        public string AddressLatitude { get; set; }
        public string AddressLongitude { get; set; }
        public byte? GenderID { get; set; }

        public virtual ICollection<CustomerProductReference> CustomerProductReferences { get; set; }
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        //public virtual KnowHowOption KnowHowOption { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CustomerSetting> CustomerSettings { get; set; }
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        public virtual Login Login { get; set; }
        //public virtual Employee Employee { get; set; } 
        //public virtual CustomerGroup CustomerGroup { get; set; }
        //public virtual CustomerStatus CustomerStatus { get; set; }
        public virtual ICollection<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }
        public virtual ICollection<PaymentDetailsTheFort> PaymentDetailsTheForts { get; set; }
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCard> CustomerCards { get; set; }
    }
}
