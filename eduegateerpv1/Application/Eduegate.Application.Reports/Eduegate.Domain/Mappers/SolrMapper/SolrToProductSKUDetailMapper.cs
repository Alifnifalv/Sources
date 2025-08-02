using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Domain.Mappers.SolrMapper
{
    public class SolrToProductSKUDetailMapper
    {
        private CallContext _context;

        public static SolrToProductSKUDetailMapper Mapper(CallContext context)
        {
            var mapper = new SolrToProductSKUDetailMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductSKUDetailDTO ToDTO(SearchCatalogDTO searchcatalogDTO)
        {
            return new ProductSKUDetailDTO()
            {
                ProductID = searchcatalogDTO.ProductID,
                //ProductPartNo = searchcatalogDTO,
                ProductName = searchcatalogDTO.ProductName,
                ProductPrice = searchcatalogDTO.ProductPrice,
                SKUID = searchcatalogDTO.SKUID,
                SKUName = searchcatalogDTO.ProductName,
                ProductCode = searchcatalogDTO.ProductCode,
                //BrandName = entity.BrandName,
                //BrandCode = entity.BrandCode
            };
        }

        public List<ProductSKUDetailDTO> ToDTOList(SearchResultDTO searchresultDTO)
        {
            var list = new List<ProductSKUDetailDTO>();
            foreach (var catalog in searchresultDTO.Catalogs)
            {
                list.Add(ToDTO(catalog));
            }
            return list;
        }

    }
}
