using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityPropertyTypes", Schema = "mutual")]
    public partial class EntityPropertyType
    {
        public EntityPropertyType()
        {
            EntityProperties = new HashSet<EntityProperty>();
            EntityTypePaymentMethodMaps = new HashSet<EntityTypePaymentMethodMap>();
        }

        [Key]
        public int EntityPropertyTypeID { get; set; }
        [StringLength(150)]
        public string EntityPropertyTypeName { get; set; }

        [InverseProperty("EntityPropertyType")]
        public virtual ICollection<EntityProperty> EntityProperties { get; set; }
        [InverseProperty("EntityPropertyType")]
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
    }
}
