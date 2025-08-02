using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
   public class RecomendedProductDTO
    {
        [DataMember]
        public long ProductID { get; set; }

        [DataMember]
        public long SkuID { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal ProductPrice { get; set; }

        [DataMember]
        public decimal ProductDiscountPrice { get; set; }

        [DataMember]
        public string ProductListingImage { get; set; }

        [DataMember]
        public int ProductAvailableQuantity { get; set; }

        [DataMember]
        public int ProductListingQuantity { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

        [DataMember]
        public bool? ProductActive { get; set; }

        [DataMember]
        public string Currency { get; set; }
    }
}
