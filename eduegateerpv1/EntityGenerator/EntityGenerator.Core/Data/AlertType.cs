using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AlertTypes", Schema = "notification")]
    public partial class AlertType
    {
        public AlertType()
        {
            NotificationAlerts = new HashSet<NotificationAlert>();
        }

        [Key]
        public int AlertTypeID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [InverseProperty("AlertType")]
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
