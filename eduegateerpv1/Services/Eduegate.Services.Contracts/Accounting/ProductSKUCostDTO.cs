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
    public class ProductSKUCostDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProductSKUCostDTO()
        {
            ProductSKUCultureInfo = new List<ProductSKUCultureDataDTO>();
        }

        [DataMember]
        public long ProductID { get; set; }
        [DataMember]
        public string ProductSKUCode { get; set; }
        [DataMember]
        public Nullable<int> Sequence { get; set; }
        [DataMember]
        public Nullable<decimal> ProductPrice { get; set; }
        [DataMember]
        public string PartNumber { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string SKU { get; set; }
        [DataMember]
        public long ProductSKUMapID { get; set; }
        [DataMember]
        public List<SKUImageMapDTO> ImageMaps { get; set; }
        [DataMember]
        public Nullable<byte> StatusID { get; set; }
        
        [DataMember]
        public bool isDefaultSKU { get; set; }

        [DataMember]
        public ProductInventoryConfigDTO ProductInventoryConfigDTOs { get; set; }

        [DataMember]
        public List<ProductVideoMapDTO> ProductVideoMaps { get; set; }

        [DataMember]
        public List<PropertyDTO> Properties { get; set; }

        [DataMember]
        public bool? IsHiddenFromList { get; set; }

        [DataMember]
        public bool HideSKU { get; set; }

        [DataMember]
        public string SkuName { get; set; }

        [DataMember]
        public string VariantsMap { get; set; }
     
        [DataMember]
        public Nullable<long> SeoMetadataID { get; set; }

        [DataMember]
        public List<ProductInventorySKUConfigMapDTO> ProductInventorySKUConfigMaps { get; set; }

        [DataMember]
        public List<ProductSKUCultureDataDTO> ProductSKUCultureInfo { get; set; }

        [DataMember]
        public decimal AvailableQuantity { get; set; }

        [DataMember]
        public decimal CurrentAvgCost { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal NewAvgCost { get; set; }


    }
}
