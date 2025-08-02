using Eduegate.Domain.Entity.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            Attachments = new HashSet<Attachment>();
            Comments = new HashSet<Comment>();
            EntityTypeEntitlements = new HashSet<EntityTypeEntitlement>();
            EntityTypePaymentMethodMaps = new HashSet<EntityTypePaymentMethodMap>();
            EntityTypeRelationMaps = new HashSet<EntityTypeRelationMap>();
            EntityTypeRelationMaps1 = new HashSet<EntityTypeRelationMap>();
            Workflows = new HashSet<Workflow>();
        }

        public int EntityTypeID { get; set; }

        public string EntityName { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }

        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }

        public virtual ICollection<EntityTypeRelationMap> EntityTypeRelationMaps { get; set; }

        public virtual ICollection<EntityTypeRelationMap> EntityTypeRelationMaps1 { get; set; }

        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}
