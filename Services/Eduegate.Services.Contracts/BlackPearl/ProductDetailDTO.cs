using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class ProductDetailDTO
    {
        [DataMember]
        public decimal ProductIID { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductImageUrl { get; set; }

        [DataMember]
        public string Metal { get; set; }

        [DataMember]
        public string Stone { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string PriceUnit { get; set; }

        [DataMember]
        public decimal OrderedQuantity { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public string Size { get; set; }

        [DataMember]
        public decimal AvailableQuantity { get; set; }

        [DataMember]
        public decimal AllowedQuantity { get; set; }

    }
}
