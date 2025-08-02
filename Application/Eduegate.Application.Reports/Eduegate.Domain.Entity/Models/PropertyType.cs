using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            this.ProductPropertyMaps = new List<ProductPropertyMap>();
            this.Properties = new List<Property>();
            this.PropertyTypePropertyMaps = new List<PropertyTypePropertyMap>();
            this.ProductFamilyPropertyTypeMaps = new List<ProductFamilyPropertyTypeMap>();
        }
         
        public byte PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PropertyTypePropertyMap> PropertyTypePropertyMaps { get; set; }
        public virtual ICollection<ProductFamilyPropertyTypeMap> ProductFamilyPropertyTypeMaps { get; set; }
    }
}
