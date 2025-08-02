using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class BrandPriceMapper : IDTOEntityMapper<BrandPriceDTO, ProductPriceListBrandMap>
    {
        public BrandPriceDTO ToDTO(ProductPriceListBrandMap entity)
        {
            throw new NotImplementedException();
        }

        public ProductPriceListBrandMap ToEntity(BrandPriceDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
