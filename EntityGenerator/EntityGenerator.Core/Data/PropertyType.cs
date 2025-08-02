using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PropertyTypes", Schema = "catalog")]
    public partial class PropertyType
    {
        public PropertyType()
        {
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            Properties = new HashSet<Property>();
            PropertiesNavigation = new HashSet<Property>();
        }

        [Key]
        public byte PropertyTypeID { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }

        [InverseProperty("PropertyType")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        [InverseProperty("PropertyType")]
        public virtual ICollection<Property> Properties { get; set; }

        [ForeignKey("PropertyTypeID")]
        [InverseProperty("PropertyTypes")]
        public virtual ICollection<Property> PropertiesNavigation { get; set; }
    }
}
