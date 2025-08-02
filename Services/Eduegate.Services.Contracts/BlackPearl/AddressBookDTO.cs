using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class AddressBookDTO
    {
        [DataMember]
        public decimal AddressBookIID { get; set; }

        [DataMember]
        public string AddressName { get; set; }

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
        public string TelephoneCode { get; set; }

        [DataMember]
        public Nullable<decimal> TelephoneNumber { get; set; }

        [DataMember]
        public bool IsDefaultShippingAddress { get; set; }

        [DataMember]
        public bool IsDefaultBillingAddress { get; set; }
    }
}
