using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerMasterNew
    {
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
    }
}
