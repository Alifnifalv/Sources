using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public partial class StockNotificationDTO
    {
        [DataMember]
        public long NotifyStockID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public Nullable<long> LoginID { get; set; }
        [DataMember]
        public Nullable<short> NotficationStatusID { get; set; }
    }
}
