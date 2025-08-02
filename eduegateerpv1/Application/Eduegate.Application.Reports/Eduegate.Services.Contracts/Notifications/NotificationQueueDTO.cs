using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class NotificationQueueDTO
    {
        [DataMember]
        public long EmailNotificationQueueID { get; set; }
        [DataMember]
        public NotificationTypes NotificationTypeID { get; set; }
        [DataMember]
        public bool IsReprocess { get; set; }
    }
}
