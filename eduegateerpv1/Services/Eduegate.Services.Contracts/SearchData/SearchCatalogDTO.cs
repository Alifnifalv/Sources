using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class SearchCatalogDTO
    {
        [DataMember]
        public long ProductID { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string BrandNameEn { get; set; }

        [DataMember]
        public string ProductThumbnail { get; set; }

        [DataMember]
        public string ProductListingImage { get; set; }

        [DataMember]
        public string ProductColor { get; set; }

        [DataMember]
        public long BrandID { get; set; }

        [DataMember]
        public string BrandCode { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public short BrandPosition { get; set; }

        [DataMember]
        public decimal ProductPrice { get; set; }

        [DataMember]
        public decimal ProductDiscountPrice { get; set; }

        [DataMember]
        public string ProductCategoryAll { get; set; }

        [DataMember]
        public long ProductWeight { get; set; }

        [DataMember]
        public int ProductAvailableQuantity { get; set; }

        [DataMember]
        public short DeliveryDays { get; set; }

        [DataMember]
        public int? ProductSoldQty { get; set; }

        [DataMember]
        public int NewArrival { get; set; }

        [DataMember]
        public string ProductMadeIn { get; set; }

        [DataMember]
        public string ProductModel { get; set; }

        [DataMember]
        public string ProductWarranty { get; set; }

        [DataMember]
        public bool QuantityDiscount { get; set; }

        [DataMember]
        public bool DisableListing { get; set; }

        [DataMember]
        public bool ProductActive { get; set; }
        [DataMember]
        public bool ProductActiveAr { get; set; }

        [DataMember]
        public string ProductKeywordsEn { get; set; }

        [DataMember]
        public string BrandKeywordsEn { get; set; }

        [DataMember]
        public string ProductNameAr { get; set; }

        [DataMember]
        public string BrandNameAr { get; set; }
        [DataMember]
        public long SKUID { get; set; }
        [DataMember]

        public string CurrencyCode { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public string ProductLargeImage { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public long? CategoryID { get; set; }

        [DataMember]
        public string AdditionalInfo1 { get; set; }

        [DataMember]
        public string AdditionalInfo2 { get; set; }

        [DataMember]
        public string ProductDescription { get; set; }

        [DataMember]
        public string AlertMessage { get; set; }

        [DataMember]
        public bool IsWishList { get; set; }
    }
}
