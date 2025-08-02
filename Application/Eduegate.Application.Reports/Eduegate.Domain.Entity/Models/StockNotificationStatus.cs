using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class StockNotificationStatus
    {
        public StockNotificationStatus()
        {
            this.StockNotifications = new List<StockNotification>();
        }

        public short StockNotificationStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<StockNotification> StockNotifications { get; set; }
    }
}
