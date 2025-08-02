using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class MyAccountDTO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public decimal TelephoneNumber { get; set; }

        [DataMember]
        public AddressDTO ShippingAddress { get; set; }

        [DataMember]
        public AddressDTO BillingAddress { get; set; }

        [DataMember]
        public AddressDTO BreadCrumbModel { get; set; }
    }
}
