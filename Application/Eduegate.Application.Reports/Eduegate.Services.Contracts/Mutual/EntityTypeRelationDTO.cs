using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class EntityTypeRelationDTO
    {
        [DataMember]
        public EntityTypes FromEntityTypes { get; set; }
        [DataMember]
        public EntityTypes ToEntityTypes { get; set; }
        [DataMember]
        public long FromRelaionID { get; set; }
        [DataMember]
        public List<long> ToRelaionIDs { get; set; }
    }


    //public class EntityTypeRelationMapper
    //{
    //    public EntityTypeRelationDTO ToDto(EntityTypeRelation entity)
    //    { }
    //}
}
