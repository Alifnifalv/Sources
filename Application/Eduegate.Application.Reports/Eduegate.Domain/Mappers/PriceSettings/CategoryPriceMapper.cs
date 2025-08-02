using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class CategoryPriceMapper : IDTOEntityMapper<CategoryPriceDTO, ProductPriceListCategoryMap>
    {
        public CategoryPriceDTO ToDTO(ProductPriceListCategoryMap entity)
        {
            throw new NotImplementedException();
        }

        public ProductPriceListCategoryMap ToEntity(CategoryPriceDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
