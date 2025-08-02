using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    public class ProductPriceListTypeMapper : IDTOEntityMapper<ProductPriceListTypeDTO, ProductPriceListType>
    {
        public static ProductPriceListTypeMapper Mapper { get { return new ProductPriceListTypeMapper(); } }

        public ProductPriceListTypeDTO ToDTO(ProductPriceListType entity)
        {
            if (entity != null)
            {
                return new ProductPriceListTypeDTO()
                {
                    ProductPriceListTypeID = entity.ProductPriceListTypeID,
                    PriceListTypeName = entity.PriceListTypeName,
                    Description = entity.Description,
                };
            }
            else
            {
                return new ProductPriceListTypeDTO();
            }
        }


        public ProductPriceListType ToEntity(ProductPriceListTypeDTO dto)
        {
            if (dto != null)
            {
                return new ProductPriceListType()
                {
                    ProductPriceListTypeID = dto.ProductPriceListTypeID,
                    PriceListTypeName = dto.PriceListTypeName,
                    Description = dto.Description,
                };
            }
            else
            {
                return new ProductPriceListType();
            }
        }

    }
}
