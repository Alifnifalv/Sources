using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductPriceCategoryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListCategoryMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public string PriceDescription { get; set; } 
        [DataMember]
        public Nullable<long> CategoryID { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPrice { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Price { get; set; }
    }
}
