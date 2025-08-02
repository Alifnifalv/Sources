namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.DeliveryTypeMaster")]
    public partial class DeliveryTypeMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryTypeMaster()
        {
            DeliveryTypeCategoryMasters = new HashSet<DeliveryTypeCategoryMaster>();
        }

        [Key]
        public int DeliveryTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCategoryMaster> DeliveryTypeCategoryMasters { get; set; }
    }
}
