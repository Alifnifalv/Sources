using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerMasterNumberInvalid
    {
        public long CustomerID { get; set; }
        public string CustomerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public bool Promotion { get; set; }
        public System.DateTime RegisteredOn { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationCountry { get; set; }
        public Nullable<int> HowKnowOption { get; set; }
        public string HowKnowText { get; set; }
        public string CountryCode { get; set; }
        public string CountryPhoneCode { get; set; }
    }
}
