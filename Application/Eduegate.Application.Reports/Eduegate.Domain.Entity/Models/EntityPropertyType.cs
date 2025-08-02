using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityPropertyType
    {
        public EntityPropertyType()
        {
            this.EntityProperties = new List<EntityProperty>();
            this.EntityTypePaymentMethodMaps = new List<EntityTypePaymentMethodMap>();
        }

        public int EntityPropertyTypeID { get; set; }
        public string EntityPropertyTypeName { get; set; }
        public virtual ICollection<EntityProperty> EntityProperties { get; set; }
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
    }
}
