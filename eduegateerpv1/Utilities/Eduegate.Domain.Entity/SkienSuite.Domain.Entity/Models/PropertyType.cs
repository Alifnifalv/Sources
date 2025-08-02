using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            this.ProductPropertyMaps = new List<ProductPropertyMap>();
            this.Properties = new List<Property>();
            this.Properties1 = new List<Property>();
        }

        public byte PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Property> Properties1 { get; set; }
    }
}
