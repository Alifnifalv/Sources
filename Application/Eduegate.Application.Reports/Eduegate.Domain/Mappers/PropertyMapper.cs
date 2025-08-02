using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class PropertyMapper : IDTOEntityMapper<PropertyDTO, Property>
    {

        private CallContext _context;
        public static PropertyMapper Mapper(CallContext context)
        {
            var mapper = new PropertyMapper();
            mapper._context = context;
            return mapper;
        }

        public PropertyDTO ToDTO(Property entity)
        {
            if (entity != null)
            {
                return new PropertyDTO()
                {
                    PropertyIID = entity.PropertyIID,
                    PropertyName = entity.PropertyName,
                    PropertyTypeID = entity.PropertyTypeID,
                    PropertyDescription = entity.PropertyDescription,
                    DefaultValue = entity.DefaultValue,
                    IsUnqiue = entity.IsUnqiue,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else return new PropertyDTO();
        }

        public Property ToEntity(PropertyDTO dto)
        {
            if (dto != null)
            {
                return new Property()
                {
                    PropertyIID = dto.PropertyIID,
                    PropertyName = dto.PropertyName,
                    PropertyTypeID = dto.PropertyTypeID,
                    PropertyDescription = dto.PropertyDescription,
                    DefaultValue = dto.DefaultValue,
                    IsUnqiue = dto.IsUnqiue,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = (int)_context.LoginID,
                    CreatedDate = dto.PropertyIID == 0 ? DateTime.Now : dto.CreatedDate,
                    CreatedBy = dto.PropertyIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps)

                };
            }
            else
            {
                return new Property();
            }
        }
    }
}
