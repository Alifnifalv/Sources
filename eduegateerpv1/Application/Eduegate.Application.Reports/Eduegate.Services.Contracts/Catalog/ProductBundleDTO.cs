using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductBundleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProductBundleDTO()
        {
            FromProduct = new KeyValueDTO();
        }
        [DataMember]
        public long BundleIID { get; set; }

        [DataMember]
        public long? FromProductID { get; set; }

        [DataMember]
        public long? ToProductID { get; set; }

        [DataMember]
        public long? FromProductSKUMapID { get; set; }

        [DataMember]
        public long? ToProductSKUMapID { get; set; }

        [DataMember]
        public decimal? Quantity { get; set; }

        [DataMember]
        public decimal? SellingPrice { get; set; }

        [DataMember]
        public decimal? CostPrice { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public decimal? AvailableQuantity { get; set; }

        [DataMember]
        public KeyValueDTO FromProduct { get; set; }
    }
}
