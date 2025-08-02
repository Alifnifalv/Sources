using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class PlaceOrderDTO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public decimal TelephoneNumber { get; set; }

        [DataMember]
        public DateTime DeliveryDate { get; set; }

        [DataMember]
        public string ShippingMethod { get; set; }

        [DataMember]
        public AddressBookDTO ShippingAddress { get; set; }

        [DataMember]
        public AddressBookDTO BillingAddress { get; set; }

        [DataMember]
        public List<Eduegate.Services.Contracts.Catalog.ProductDetailDTO> ProductDetails { get; set; }

        [DataMember]
        public decimal SubTotal { get; set; }

        [DataMember]
        public decimal DeliveryCharge { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public bool IsCreditCard { get; set; }

        [DataMember]
        public bool IsKNET { get; set; }

        [DataMember]
        public bool IsCashOnDelivery { get; set; }
    }
}
