using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class StockNotification
    {
        [Key]
        public long NotifyStockIID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public string EmailID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<short> NotficationStatusID { get; set; }
        public virtual Login Login { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual StockNotificationStatus StockNotificationStatus { get; set; }
    }
}
