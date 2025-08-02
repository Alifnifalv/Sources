using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.ProductDetail;

namespace Eduegate.Domain.Mappers
{
    public partial class ProductSKUVariantMapper : IDTOEntityMapper<ProductSKUVariantDTO, ProductSKUVariant>
    {
        private CallContext _context;

        public static ProductSKUVariantMapper Mapper(CallContext context)
        {
            var mapper = new ProductSKUVariantMapper();
            mapper._context = context;
            return mapper;
        }
        public ProductSKUVariantDTO ToDTO(ProductSKUVariant entity)
        {
            if (entity !=null)
            {
            return new ProductSKUVariantDTO
            {
                ProductIID = entity.ProductIID,
                ProductSKUMapIID = entity.ProductSKUMapIID,
                PropertyName = entity.PropertyName,
                PropertyTypeName = entity.PropertyTypeName
                
            };
            }
            else { return null; }
        }

        public ProductSKUVariant ToEntity(ProductSKUVariantDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
