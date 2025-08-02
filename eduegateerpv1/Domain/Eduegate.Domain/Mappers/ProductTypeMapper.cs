using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    class ProductTypeMapper : IDTOEntityMapper<ProductTypeDTO, ProductType>
    {

        public static ProductTypeMapper Mapper()
        {
            var mapper = new ProductTypeMapper();
            return mapper;
        }

        public ProductTypeDTO ToDTO(ProductType entity)
        {
            if (entity != null)
            {
                return new ProductTypeDTO()
                {
                    
                    ProductTypeID = entity.ProductTypeID,
                    ProductTypeName = entity.ProductTypeName,
                };
            }
            else
                return new ProductTypeDTO();
        }

        public ProductType ToEntity(ProductTypeDTO dto)
        {
            if (dto != null)
            {
                return new ProductType()
                {
                   
                    ProductTypeID = dto.ProductTypeID,
                    ProductTypeName = dto.ProductTypeName,
                };
            }
            else
                return new ProductType();
        }
    }
}
