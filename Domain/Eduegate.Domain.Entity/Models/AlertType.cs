using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AlertTypes", Schema = "notification")]
    public partial class AlertType
    {
        public AlertType()
        {
            this.NotificationAlerts = new List<NotificationAlert>();
        }

        [Key]
        public int AlertTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
