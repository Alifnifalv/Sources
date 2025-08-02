using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityTypes", Schema = "mutual")]
    public partial class EntityType
    {
        public EntityType()
        {
            Attachments = new HashSet<Attachment>();
            Comments = new HashSet<Comment>();
            EntityTypeEntitlements = new HashSet<EntityTypeEntitlement>();
            EntityTypePaymentMethodMaps = new HashSet<EntityTypePaymentMethodMap>();
            EntityTypeRelationMapFromEntityTypes = new HashSet<EntityTypeRelationMap>();
            EntityTypeRelationMapToEntityTypes = new HashSet<EntityTypeRelationMap>();
            Workflows = new HashSet<Workflow>();
        }

        [Key]
        public int EntityTypeID { get; set; }
        [StringLength(150)]
        public string EntityName { get; set; }

        [InverseProperty("EntityType")]
        public virtual ICollection<Attachment> Attachments { get; set; }
        [InverseProperty("EntityType")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("EntityType")]
        public virtual ICollection<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }
        [InverseProperty("EntityType")]
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        [InverseProperty("FromEntityType")]
        public virtual ICollection<EntityTypeRelationMap> EntityTypeRelationMapFromEntityTypes { get; set; }
        [InverseProperty("ToEntityType")]
        public virtual ICollection<EntityTypeRelationMap> EntityTypeRelationMapToEntityTypes { get; set; }
        [InverseProperty("LinkedEntityType")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}
