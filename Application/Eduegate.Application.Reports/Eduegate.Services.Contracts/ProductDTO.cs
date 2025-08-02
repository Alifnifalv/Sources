using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public long ProductID { get; set; }
        [DataMember]
        public long SKUID { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.ProductStatuses ProductStatus { get; set; }
        [DataMember]
        public List<string> CategoryIDs { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string ProductGroup { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string DiscountedPrice { get; set; }
        [DataMember]
        public decimal DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<byte> SortOrder { get; set; }
        [DataMember]
        public string SKU { get; set; }
        [DataMember]
        public string ImageFile { get; set; }
        [DataMember]
        public Nullable<byte> Sequence { get; set; }
        [DataMember]
        public long CategoryIID { get; set; }
        [DataMember]
        public long ProductCount { get; set; }
        [DataMember]
        public string ProductPrice { get; set; }
        [DataMember]
        public long BrandIID { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public int BrandPosition { get; set; }
        [DataMember]
        public string Descirption { get; set; }
        [DataMember]
        public string LogoFile { get; set; }
        [DataMember]
        public bool HasStock { get; set; }
        [DataMember]
        public string ProductCategoryAll { get; set; }
        [DataMember]
        public string ProductThumbnail { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public string BrandNameEn { get; set; }
        public bool? DisableListing { get; set; }
        [DataMember]
        public bool? ProductActive { get; set; }
        [DataMember]
        public bool? ProductActiveAr { get; set; }
        [DataMember]
        public string ProductColor { get; set; }
        [DataMember]
        public decimal ProductDiscountPrice { get; set; }
        [DataMember]
        public string ProductListingImage { get; set; }
        [DataMember]
        public string ProductMadeIn { get; set; }
        [DataMember]
        public string ProductModel { get; set; }
        [DataMember]
        public string ProductWarranty { get; set; }
        [DataMember]
        public long? ProductWeight { get; set; }
        [DataMember]
        public int ProductAvailableQuantity { get; set; }
        [DataMember]
        public bool? QuantityDiscount { get; set; }
        [DataMember]
        public int? ProductSoldQty { get; set; }
        [DataMember]
        public int? DeliveryDays { get; set; }
        [DataMember]
        public int NewArrival { get; set; }

        [DataMember]
        public string BrandKeyWords { get; set; }

        [DataMember]
        public string ProductKeyWordsEn { get; set; }

        [DataMember]
        public string ProductNameAr { get; set; }

        [DataMember]
        public string BrandNameAr { get; set; }

        [DataMember]
        public string BrandKeyWordsAr { get; set; }

        [DataMember]
        public string ProductKeyWordsAr { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public List<string> SKUTags { get; set; }

        [DataMember]
        public List<string> CategoryTags { get; set; }
        [DataMember]
        public string ProductPartNo { get; set; }

        [DataMember]
        public int? DeliveryRange { get; set; }
        [DataMember]
        public string ProductCreatedDate { get; set; }
        [DataMember]
        public List<string> DeliveryTypesList { get; set; }
        [DataMember]
        public List<string> SKUProperties { get; set; }
        [DataMember]
        public List<string> PropertyType { get; set; }
        [DataMember]
        public List<string> ProductFamily { get; set; }
        [DataMember]
        public string ProductNameCulture { get; set; }

        [DataMember]
        public long? PurchaseUnitID { get; set; }

        [DataMember]
        public long? SellingUnitID { get; set; }

        [DataMember]
        public long? PurchaseUnitGroupID { get; set; }

        [DataMember]
        public long? SellingUnitGroupID { get; set; }

        [DataMember]
        public long? CartItemID { get; set; }

        [DataMember]
        public int? DeliveryTypeTimeSlotMapID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
    public static class ProductMapper
    {
        public static ProductDTO ToProductDTOMap(Product obj)
        {
            return new ProductDTO()
            {
                ProductID = obj.ProductIID,
                ProductName = obj.ProductName,
            };
        }
    }
}
