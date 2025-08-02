using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public partial class ProductFeatureDTO
    {
        [DataMember]
        public long ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public long BrandID { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public long ProductSKUMapId { get; set; }

        [DataMember]
        public string ProductSKUCode { get; set; }

        [DataMember]
        public string ProductPrice { get; set; }

        [DataMember]
        public string DiscountedPrice { get; set; }

        [DataMember]
        public Nullable<decimal> PricePercentage { get; set; }

        [DataMember]
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public Nullable<decimal> MinimumQuanityInCart { get; set; }
        [DataMember]
        public Nullable<decimal> MaximumQuantityInCart { get; set; }
        
            


        [DataMember]
        public List<ProductSKUDTO> ProductSKUDTOList { get; set; }

        [DataMember]
        public List<ImageDTO> ImageList { get; set; }

        [DataMember]
        public List<PropertyTypeDTO> PropertyTypeList { get; set; }

        [DataMember]
        public List<PropertyDTO> PropertyList { get; set; }
    }


    public static class ProductFeatureMapper
    {
        public static ProductFeatureDTO ToProductFeatureDTOMap(Product obj)
        {
            return new ProductFeatureDTO()
            {
                ProductID = obj.ProductIID,
                ProductName = obj.ProductName,


            };
        }
    }
}
