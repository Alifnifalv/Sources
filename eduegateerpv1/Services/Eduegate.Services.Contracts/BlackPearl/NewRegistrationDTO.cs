using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class NewRegistrationDTO
    {
        [DataMember]
        public decimal RegistrationIID { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string ConfirmPassword { get; set; }

        [DataMember]
        public string TitleID { get; set; }

        [DataMember]
        public string TitleName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string AddressName { get; set; }

        [DataMember]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public Nullable<long> CountryIID { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string PassportNumber { get; set; }

        [DataMember]
        public Nullable<long> CivilIdNumber { get; set; }

        [DataMember]
        public string PassportIssueCountryID { get; set; }

        [DataMember]
        public string PassportIssueCountryName { get; set; }

        [DataMember]
        public string TelephoneCode { get; set; }

        [DataMember]
        public Nullable<decimal> TelephoneNumber { get; set; }

        [DataMember]
        public bool IsDifferentBillingAddress { get; set; }

        [DataMember]
        public bool IsTermsAndConditionsAggreed { get; set; }

        [DataMember]
        public bool IsSubscribeNewsLetter { get; set; }

        [DataMember]
        public bool IsResidentOfKuwaith { get; set; }

        [DataMember]
        public bool IsNotResidentOfKuwaith { get; set; }

        [DataMember]
        public NewRegistrationDTO BillingAddress { get; set; }

        [DataMember]
        public NewRegistrationDTO ShippingAddress { get; set; }
    }
}
