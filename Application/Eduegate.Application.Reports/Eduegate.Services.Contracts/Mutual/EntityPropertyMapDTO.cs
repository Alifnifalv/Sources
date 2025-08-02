using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class EntityPropertyMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EntityPropertyMapID { get; set; }
        [DataMember]
        public Nullable<int> EntityTypeID { get; set; }
        [DataMember]
        public Nullable<int> EntityPropertyTypeID { get; set; }
        [DataMember]
        public Nullable<long> EntityPropertyID { get; set; }
        [DataMember]
        public Nullable<long> ReferenceID { get; set; }
        [DataMember]
        public Nullable<short> Sequence { get; set; }
        [DataMember]
        public string Value1 { get; set; }
        [DataMember]
        public string Value2 { get; set; }

        [DataMember]
        public string EntityProperty { get; set; }

    }


    public class EntityPropertyMapMapper
    {
        public static EntityPropertyMapDTO ToDto(EntityPropertyMap entity, string entityProperty = null)
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


        public static EntityPropertyMap ToEntity(EntityPropertyMapDTO dto)
        {
            if (dto != null)
            {
                return new EntityPropertyMap()
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
                return new EntityPropertyMap();
            }
        }

        public static EntityPropertyMapDTO ToDto(EntityPropertyMap entity)
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
