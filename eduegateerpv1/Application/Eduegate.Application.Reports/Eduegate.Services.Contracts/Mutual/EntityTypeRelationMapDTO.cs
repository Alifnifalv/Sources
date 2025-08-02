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
    public class EntityTypeRelationMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EntityTypeRelationMapsID { get; set; }
        [DataMember]
        public Nullable<int> FromEntityTypeID { get; set; }
        [DataMember]
        public Nullable<int> ToEntityTypeID { get; set; }
        [DataMember]
        public Nullable<long> FromRelationID { get; set; }
        [DataMember]
        public Nullable<long> ToRelationID { get; set; }
    }


    public class EntityTypeRelationMapMapper
    {
        public static EntityTypeRelationMapDTO ToDto(EntityTypeRelationMap entity)
        {
            if (entity != null)
            {
                return new EntityTypeRelationMapDTO(){
                    EntityTypeRelationMapsID = entity.EntityTypeRelationMapsIID,
                    FromEntityTypeID = entity.FromEntityTypeID,
                    ToEntityTypeID = entity.ToEntityTypeID,
                    ToRelationID = entity.ToRelationID,
                };
            }
            else return new EntityTypeRelationMapDTO();
        }


        public static EntityTypeRelationMap ToEntity(EntityTypeRelationMapDTO dto)
        {
            if (dto != null)
            {
                return new EntityTypeRelationMap()
                {
                    EntityTypeRelationMapsIID = dto.EntityTypeRelationMapsID,
                    FromEntityTypeID = dto.FromEntityTypeID,
                    ToEntityTypeID = dto.ToEntityTypeID,
                    ToRelationID = dto.ToRelationID,
                };
            }
            else return new EntityTypeRelationMap();
        }

    }


}
