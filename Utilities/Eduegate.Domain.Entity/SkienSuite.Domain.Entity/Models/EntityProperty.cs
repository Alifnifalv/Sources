using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityProperty
    {
        public long EntityPropertyIID { get; set; }
        public Nullable<int> EntityPropertyTypeID { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDescription { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual EntityPropertyType EntityPropertyType { get; set; }
    }
}
