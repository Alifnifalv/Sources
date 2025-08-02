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
    public class ProductQuantityDiscountMapper : IDTOEntityMapper<ProductQuantityDiscountDTO, ProductQuantityDiscount>
    {
        private CallContext _context;

        public static ProductQuantityDiscountMapper Mapper(CallContext context)
        {
            var mapper = new ProductQuantityDiscountMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductQuantityDiscountDTO ToDTO(ProductQuantityDiscount entity)
        {
            if (entity != null)
            {
                return new ProductQuantityDiscountDTO()
                {
                    DiscountPercentage = entity.DiscountPercentage,
                    QtyPrice = entity.QtyPrice,
                    Quantity = entity.Quantity,
                    Currency = _context.CurrencyCode
                };
            }
            else
            {
                return new ProductQuantityDiscountDTO();
            }
        }

        public ProductQuantityDiscount ToEntity(ProductQuantityDiscountDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
