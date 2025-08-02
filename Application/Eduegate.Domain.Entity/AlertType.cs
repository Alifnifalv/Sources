namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.AlertTypes")]
    public partial class AlertType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlertType()
        {
            NotificationAlerts = new HashSet<NotificationAlert>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlertTypeID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
