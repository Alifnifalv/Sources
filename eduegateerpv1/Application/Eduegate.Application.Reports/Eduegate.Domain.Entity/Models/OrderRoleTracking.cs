using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderRoleTracking
    {
        public long OrderTrackingIID { get; set; }
        public long TransactionHeadID { get; set; }
        public string DevicePlatform { get; set; }
        public string DeviceVersion { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
