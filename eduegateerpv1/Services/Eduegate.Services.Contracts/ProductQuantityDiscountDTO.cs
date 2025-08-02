using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
     public partial class ProductQuantityDiscountDTO
    {
        [DataMember]
        public string DiscountPercentage { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]

        public string QtyPrice { get; set; }
        [DataMember]

        public string Currency { get; set; }
    }
}
