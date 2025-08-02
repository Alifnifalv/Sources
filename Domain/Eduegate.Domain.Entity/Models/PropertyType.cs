using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PropertyTypes", Schema = "catalog")]
    public partial class PropertyType
    {
        public PropertyType()
        {
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            Properties = new HashSet<Property>();
            //PropertiesNavigation = new HashSet<Property>();
        }

        [Key]
        public byte PropertyTypeID { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        //public virtual ICollection<Property> PropertiesNavigation { get; set; }
    }
}
