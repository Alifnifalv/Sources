namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AlertStatuses", Schema = "notification")]
    public partial class AlertStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlertStatus()
        {
            NotificationAlerts = new HashSet<NotificationAlert>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlertStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
