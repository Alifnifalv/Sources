namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AlertTypes", Schema = "notification")]
    public partial class AlertType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlertType()
        {
            NotificationAlerts = new HashSet<NotificationAlert>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlertTypeID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
