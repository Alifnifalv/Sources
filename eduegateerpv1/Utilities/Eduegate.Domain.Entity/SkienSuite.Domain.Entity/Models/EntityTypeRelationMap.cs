using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityTypeRelationMap
    {
        public long EntityTypeRelationMapsIID { get; set; }
        public Nullable<short> FromEntityTypeID { get; set; }
        public Nullable<short> ToEntityTypeID { get; set; }
        public Nullable<long> FromRelationID { get; set; }
        public Nullable<long> ToRelationID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual EntityType EntityType { get; set; }
        public virtual EntityType EntityType1 { get; set; }
    }
}
