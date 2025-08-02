using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerMaster
    {
        public CustomerMaster()
        {
            this.AddressMasters = new List<AddressMaster>();
            this.CustomerLogs = new List<CustomerLog>();
            this.PaymentDetails = new List<PaymentDetail>();
            this.PaymentDetailsMasterVisas = new List<PaymentDetailsMasterVisa>();
            //this.PaymentDetailsPayPals = new List<PaymentDetailsPayPal>();
            this.VoucherClaims = new List<VoucherClaim>();
            this.WalletTransactionDetails = new List<WalletTransactionDetail>();
        }

        [Key]
        public long CustomerID { get; set; }
        public string CustomerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public bool Promotion { get; set; }
        public bool Active { get; set; }
        public byte Status { get; set; }
        public System.DateTime RegisteredOn { get; set; }
        public Nullable<System.DateTime> ConfirmedOn { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationCountry { get; set; }
        public string ConfirmationIP { get; set; }
        public string ConfirmationCountry { get; set; }
        public Nullable<int> EmailSend { get; set; }
        public Nullable<int> EmailBounced { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public Nullable<long> RefCountryID { get; set; }
        public Nullable<long> RefAreaID { get; set; }
        public Nullable<int> TotalLoyaltyPoints { get; set; }
        public Nullable<int> CategorizationPoints { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public Nullable<int> HowKnowOption { get; set; }
        public string HowKnowText { get; set; }
        public string CountryCode { get; set; }
        public string CountryPhoneCode { get; set; }
        public Nullable<bool> MobilePinRequired { get; set; }
        public string MobilePin { get; set; }
        public Nullable<byte> MobilePinSendCount { get; set; }
        public Nullable<bool> VerifiedBuyer { get; set; }
        public string NewsletterCategory { get; set; }
        public string DigitalProductClass { get; set; }
        public string CustomerLang { get; set; }
        public string Jadda { get; set; }
        public string Telephone2 { get; set; }
        
        //commented as it wasn't in table
        //public string RegisteredFrom { get; set; }
        public virtual ICollection<AddressMaster> AddressMasters { get; set; }
        public virtual ICollection<CustomerLog> CustomerLogs { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<PaymentDetailsMasterVisa> PaymentDetailsMasterVisas { get; set; }
        //public virtual ICollection<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }
        public virtual ICollection<VoucherClaim> VoucherClaims { get; set; }
        public virtual ICollection<WalletTransactionDetail> WalletTransactionDetails { get; set; }
    }
}
