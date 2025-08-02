using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class WishListDTO
    {
        [DataMember]
        public long ProductIID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public long SKUID { get; set; }


        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        public string ProductSubCategory { get; set; }

        [DataMember]
        public decimal DesignerIID { get; set; }

        [DataMember]
        public string DesignerName { get; set; }

        [DataMember]
        public string ProductPrice { get; set; }

        [DataMember]
        public string ProductImageUrl { get; set; }

        [DataMember]
        public Nullable<decimal> SellingQuantityLimit { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public string ProductDiscountedPrice { get; set; }
        [DataMember]
        public string Currency { get; set; }
    }
}
