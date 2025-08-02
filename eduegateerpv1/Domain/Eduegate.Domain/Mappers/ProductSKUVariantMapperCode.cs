using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.ProductDetail;

namespace Eduegate.Domain.Mappers
{
    public partial class ProductSKUVariantMapperCode : IDTOEntityMapper<ProductSKUVariantDTO, ProductSKuVariantsCode>
    {
        private CallContext _context;

        public static ProductSKUVariantMapperCode Mapper(CallContext context)
        {
            var mapper = new ProductSKUVariantMapperCode();
            mapper._context = context;
            return mapper;
        }
        public ProductSKUVariantDTO ToDTO(ProductSKuVariantsCode entity)
        {
            if (entity != null)
            {
                return new ProductSKUVariantDTO
                {
                    ProductIID = entity.ProductIID,
                    ProductSKUMapIID = entity.ProductSKUMapIID,
                    PropertyName = entity.PropertyName,
                    PropertyTypeName = entity.PropertyTypeName,
                    ProductCode  = entity.ProductCode
                };
            }
            else { return null; }
        }

        public ProductSKuVariantsCode ToEntity(ProductSKUVariantDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
