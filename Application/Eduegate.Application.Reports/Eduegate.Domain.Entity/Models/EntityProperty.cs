using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public class EntityProperty
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
