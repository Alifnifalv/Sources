using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            this.Comments = new List<Comment>();
            this.EntityTypeEntitlements = new List<EntityTypeEntitlement>();
            this.EntityTypePaymentMethodMaps = new List<EntityTypePaymentMethodMap>();
            this.EntityTypeRelationMaps = new List<EntityTypeRelationMap>();
            this.EntityTypeRelationMaps1 = new List<EntityTypeRelationMap>();
        }

        public short EntityTypeID { get; set; }
        public string EntityName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        public virtual ICollection<EntityTypeRelationMap> EntityTypeRelationMaps { get; set; }
        public virtual ICollection<EntityTypeRelationMap> EntityTypeRelationMaps1 { get; set; }
    }
}
