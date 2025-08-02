using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class CheckOutDTO
    {
        [DataMember]
        public decimal ShippingAddressIID { get; set; }

        [DataMember]
        public string ShippingAddressName { get; set; }

        [DataMember]
        public decimal BillingAddressIID { get; set; }

        [DataMember]
        public string BillingAddressName { get; set; }

        [DataMember]
        public AddressBookDTO ShippingAddress { get; set; }

        [DataMember]
        public AddressBookDTO BillingAddress { get; set; }

    }
}
