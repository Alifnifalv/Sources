using System;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    public class PropertyTypeMapper : IDTOEntityMapper<PropertyTypeDTO, PropertyType>
    {

        private CallContext _context;
        public static PropertyTypeMapper Mapper(CallContext context)
        {
            var mapper = new PropertyTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public PropertyTypeDTO ToDTO(PropertyType entity)
        {
            if (entity != null)
            {
                return new PropertyTypeDTO()
                {
                    PropertyTypeID = entity.PropertyTypeID,
                    PropertyTypeName = entity.PropertyTypeName,
                    PropertyList = entity.ProductPropertyMaps.Select(x => PropertyMapper.Mapper(_context).ToDTO(new ProductDetailRepository().GetProperty(x.PropertyID))).ToList(),
                };
            }
            else return new PropertyTypeDTO();
        }

        public PropertyType ToEntity(PropertyTypeDTO dto)
        {
            return new PropertyType()
            {
                //CultureID = Convert.ToByte(dto.CultureID),
                PropertyTypeID = dto.PropertyTypeID,
                PropertyTypeName = dto.PropertyTypeName,
                ProductPropertyMaps = dto.PropertyList.Select(x => new ProductPropertyMap() { PropertyID = x.PropertyIID, PropertyTypeID = dto.PropertyTypeID }).ToList()
            };

        }


    }
}