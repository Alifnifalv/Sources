using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ProductDetail
{
    [DataContract]
    public class ProductSKUDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductID { get; set; }

        [DataMember]
        public string ProductPartNo { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public decimal ProductPrice { get; set; }

        [DataMember]
        public decimal ProductDiscountPrice { get; set; }

        [DataMember]
        public long ProductAvailableQuantity { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public long SKUID { get; set; }

        [DataMember]
        public long ProductListingQuantity { get; set; }

        [DataMember]
        public string ProductListingImage { get; set; }
        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string ProductThumbnailImage { get; set; }


        [DataMember]
        public long BrandID { get; set; }

        [DataMember]
        public string SKUName { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string BrandCode { get; set; }

        [DataMember]

        public string ServerCurrentTime { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public bool IsWishList { get; set; }

        [DataMember]
        public decimal? Calorie { get; set; }

        [DataMember]
        public decimal? Weight { get; set; }

        [DataMember]
        public List<KeyValueDTO> Allergies { get; set; }

    }
}
