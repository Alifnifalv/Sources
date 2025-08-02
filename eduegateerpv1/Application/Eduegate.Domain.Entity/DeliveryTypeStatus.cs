namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypeStatuses")]
    public partial class DeliveryTypeStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryTypeStatus()
        {
            DeliveryTypes1 = new HashSet<DeliveryTypes1>();
        }

        public byte DeliveryTypeStatusID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypes1> DeliveryTypes1 { get; set; }
    }
}
