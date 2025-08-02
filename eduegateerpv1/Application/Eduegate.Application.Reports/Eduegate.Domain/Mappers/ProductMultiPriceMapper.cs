using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class ProductMultiPriceMapper : IDTOEntityMapper<ProductMultiPriceDTO, ProductMultiPrice>
    {
        private CallContext _context;

        public static ProductMultiPriceMapper Mapper(CallContext context)
        {
            var mapper = new ProductMultiPriceMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductMultiPriceDTO ToDTO(ProductMultiPrice entity)
        {
            if (entity != null)
            {
                return new ProductMultiPriceDTO()
                {
                    GroupID = entity.GroupID,
                    GroupName = entity.GroupName,
                    isSelected = entity.isSelected,
                    MultipriceValue = entity.MultipriceValue,
                    Currency = _context.CurrencyCode,
                };
            }
            else
            {
                return new ProductMultiPriceDTO();
            }
        }

        public ProductMultiPrice ToEntity(ProductMultiPriceDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
