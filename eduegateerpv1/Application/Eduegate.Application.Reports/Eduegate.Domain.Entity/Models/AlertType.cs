using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AlertType
    {
        public AlertType()
        {
            this.NotificationAlerts = new List<NotificationAlert>();
        }
        public int AlertTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
    }
}
