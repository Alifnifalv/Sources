using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public short StockNotificationStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<StockNotification> StockNotifications { get; set; }
    }
}
