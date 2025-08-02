using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class CartItemQuantityDTO
    {
        [DataMember]
        public string SKUID { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }
}
