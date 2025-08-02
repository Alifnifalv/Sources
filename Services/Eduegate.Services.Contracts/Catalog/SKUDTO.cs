using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class SKUDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SKUDTO()
        {
            ProductSKUCultureInfo = new List<ProductSKUCultureDataDTO>();
            ProductBundle = new List<ProductBundleDTO>();
            Brand = new KeyValueDTO();
            Unit = new KeyValueDTO();
            ProductFamily = new KeyValueDTO();
            Categories = new List<KeyValueDTO>();
            Rack = new List<KeyValueDTO>();
            PurchasingUnit = new KeyValueDTO();
            SellingUnit = new KeyValueDTO();
            GLAccount = new KeyValueDTO();
            ProductImageUrls = new List<DowloadFileDTO>();
            ImageMaps = new List<SKUImageMapDTO>();
            Allergies = new List<KeyValueDTO>();

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
        public Nullable<decimal> CostPrice { get; set; }
        [DataMember]
        public Nullable<decimal> SellingPrice { get; set; }
        [DataMember]
        public Nullable<int> Quantity { get; set; }

        [DataMember]
        public Nullable<long> SeoMetadataID { get; set; }

        [DataMember]
        public List<ProductInventorySKUConfigMapDTO> ProductInventorySKUConfigMaps { get; set; }

        [DataMember]
        public List<ProductSKUCultureDataDTO> ProductSKUCultureInfo { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public List<ProductPriceSettingDTO> ProductPrices { get; set; }
        
        [DataMember]
        public List<ProductBundleDTO> ProductBundle { get; set; }

        [DataMember]
        public int? TaxTempleteID { get; set; }

        [DataMember]
        public KeyValueDTO Unit { get; set; }

        [DataMember]
        public long? ProductFamilyID { get; set; }

        [DataMember]
        public List<KeyValueDTO> Categories { get; set; }

        [DataMember]
        public List<KeyValueDTO> Allergies { get; set; }

        [DataMember]
        public List<KeyValueDTO> Rack { get; set; }

        [DataMember]
        public KeyValueDTO Brand { get; set; }

        [DataMember]
        public KeyValueDTO ProductFamily { get; set; }
        
        [DataMember]
        public long? UnitID { get; set; }

        [DataMember]
        public long? BrandID { get; set; }
        [DataMember]
        public long? CategoriesID { get; set; }

        [DataMember]
        public long RackID { get; set; }

        [DataMember]
        public long? ProductTypeID { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public KeyValueDTO PurchasingUnit { get; set; }

        [DataMember]
        public KeyValueDTO SellingUnit { get; set; }

        [DataMember]
        public long? PurchaseUnitID { get; set; }

        [DataMember]
        public long? SellingUnitID { get; set; }

        [DataMember]
        public string SellingUnitGroup { get; set; }

        [DataMember]
        public string PurchaseUnitGroup { get; set; }

        [DataMember]
        public long? SellingUnitGroupID { get; set; }

        [DataMember]
        public long? PurchaseUnitGroupID { get; set; }

        [DataMember]
        public string ProductImageUrl { get; set; }

        [DataMember]
        public List<DowloadFileDTO> ProductImageUrls { get; set; }

        [DataMember]
        public string ProductImageUploadFile { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public long? GLAccountID { get; set; }
      
        [DataMember]
        public KeyValueDTO GLAccount { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public decimal? Calorie { get; set; }

        [DataMember]
        public decimal? Weight { get; set; }

    }
}
