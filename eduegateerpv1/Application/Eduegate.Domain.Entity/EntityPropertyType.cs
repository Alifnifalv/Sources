namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntityPropertyTypes")]
    public partial class EntityPropertyType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntityPropertyType()
        {
            EntityProperties = new HashSet<EntityProperty>();
            EntityTypePaymentMethodMaps = new HashSet<EntityTypePaymentMethodMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EntityPropertyTypeID { get; set; }

        [StringLength(150)]
        public string EntityPropertyTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityProperty> EntityProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
    }
}
