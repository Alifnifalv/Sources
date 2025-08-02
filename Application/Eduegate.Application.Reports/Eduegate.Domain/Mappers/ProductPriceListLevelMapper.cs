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
    public class ProductPriceListLevelMapper : IDTOEntityMapper<ProductPriceListLevelDTO, ProductPriceListLevel>
    {
        public static ProductPriceListLevelMapper Mapper { get { return new ProductPriceListLevelMapper(); } }

        public ProductPriceListLevelDTO ToDTO(ProductPriceListLevel entity)
        {
            if (entity != null)
            {
                return new ProductPriceListLevelDTO()
                {
                    ProductPriceListLevelID = entity.ProductPriceListLevelID,
                    Name = entity.Name,
                    Description = entity.Description,
                };
            }
            else
            {
                return new ProductPriceListLevelDTO();
            }
        }


        public ProductPriceListLevel ToEntity(ProductPriceListLevelDTO dto)
        {
            if (dto != null)
            {
                return new ProductPriceListLevel()
                {
                    ProductPriceListLevelID = dto.ProductPriceListLevelID,
                    Name = dto.Name,
                    Description = dto.Description,
                };
            }
            else
            {
                return new ProductPriceListLevel();
            }
        }
    }
}
