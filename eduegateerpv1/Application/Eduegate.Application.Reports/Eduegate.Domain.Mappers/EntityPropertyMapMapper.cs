using Eduegate.Services.Contracts.Mutual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers
{
    public class EntityPropertyMapMapper
    {
        public static EntityPropertyMapDTO ToDto(Entity.Models.EntityPropertyMap entity, string entityProperty = null)
        {
            if (entity != null)
            {
                return new EntityPropertyMapDTO()
                {
                    EntityPropertyMapID = entity.EntityPropertyMapIID,
                    EntityTypeID = entity.EntityTypeID,
                    EntityPropertyTypeID = entity.EntityPropertyTypeID,
                    EntityPropertyID = entity.EntityPropertyID,
                    ReferenceID = entity.ReferenceID,
                    Sequence = entity.Sequence,
                    Value1 = entity.Value1,
                    Value2 = entity.Value2,
                    EntityProperty = entityProperty
                };
            }
            else
            {
                return new EntityPropertyMapDTO();
            }
        }


        public static Entity.Models.EntityPropertyMap ToEntity(EntityPropertyMapDTO dto)
        {
            if (dto != null)
            {
                return new Entity.Models.EntityPropertyMap()
                {
                    EntityPropertyMapIID = dto.EntityPropertyMapID,
                    EntityTypeID = dto.EntityTypeID,
                    EntityPropertyTypeID = dto.EntityPropertyTypeID,
                    EntityPropertyID = dto.EntityPropertyID,
                    ReferenceID = dto.ReferenceID,
                    Sequence = dto.Sequence,
                    Value1 = dto.Value1,
                    Value2 = dto.Value2,
                };
            }
            else
            {
                return new Entity.Models.EntityPropertyMap();
            }
        }

        public static EntityPropertyMapDTO ToDto(Entity.Models.EntityPropertyMap entity)
        {
            if (entity != null)
            {
                return new EntityPropertyMapDTO()
                {
                    EntityPropertyMapID = entity.EntityPropertyMapIID,
                    EntityTypeID = entity.EntityTypeID,
                    EntityPropertyTypeID = entity.EntityPropertyTypeID,
                    EntityPropertyID = entity.EntityPropertyID,
                    ReferenceID = entity.ReferenceID,
                    Sequence = entity.Sequence,
                    Value1 = entity.Value1,
                    Value2 = entity.Value2,
                };
            }
            else
            {
                return new EntityPropertyMapDTO();
            }
        }
    }
}
