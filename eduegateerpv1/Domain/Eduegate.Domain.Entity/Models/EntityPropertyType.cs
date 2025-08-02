using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntityPropertyTypes", Schema = "mutual")]
    public partial class EntityPropertyType
    {
        public EntityPropertyType()
        {
            this.EntityProperties = new List<EntityProperty>();
            this.EntityTypePaymentMethodMaps = new List<EntityTypePaymentMethodMap>();
        }

        [Key]
        public int EntityPropertyTypeID { get; set; }
        public string EntityPropertyTypeName { get; set; }
        public virtual ICollection<EntityProperty> EntityProperties { get; set; }
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
    }
}
