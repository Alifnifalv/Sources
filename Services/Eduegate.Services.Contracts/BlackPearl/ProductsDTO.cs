using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class ProductsDTO
    {
        [DataMember]
        public decimal ProductCount { get; set; }

        [DataMember]
        public List<ProductDetailDTO> Products { get; set; }
    }
}
