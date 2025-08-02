using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.ProductDetail;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductSKUDetailViewModel : BaseMasterViewModel
    {

        public long ProductID { get; set; }

        public string ProductPartNo { get; set; }

        public string ProductName { get; set; }

        public string BrandName { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductDiscountPrice { get; set; }

        public long ProductAvailableQuantity { get; set; }

        public string Currency { get; set; }

        public long SkuID { get; set; }

        public long ProductListingQuantity { get; set; }

        public string ProductListingImage { get; set; }

        public string ProductThumbnailImage { get; set; }
        public static ProductSKUDetailViewModel ToVM(ProductSKUDetailDTO dto)
        {
            Mapper<ProductSKUDetailDTO, ProductSKUDetailViewModel>.CreateMap();
            var mapper = Mapper<ProductSKUDetailDTO, ProductSKUDetailViewModel>.Map(dto);

            return mapper;

        }
    }
}
