namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.OperationTypes")]
    public partial class OperationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OperationType()
        {
            EntityChangeTrackers = new HashSet<EntityChangeTracker>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OperationTypeID { get; set; }

        [StringLength(50)]
        public string OperationName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
