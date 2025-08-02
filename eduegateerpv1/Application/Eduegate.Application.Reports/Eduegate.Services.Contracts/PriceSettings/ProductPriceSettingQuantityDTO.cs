using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.PriceSettings
{
    [DataContract]
    public class ProductPriceSettingQuantityDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListProductQuantityMapIID { get; set; }

        [DataMember]
        public Nullable<long> ProductPriceListProductMapID { get; set; }

        [DataMember]
        public Nullable<long> ProductID { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public Nullable<decimal> Discount { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
    }
}
