using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "NotificationStatuses")]
    public enum NotificationStatuses
    {
        [EnumMember]
        New = 1,
        [EnumMember]
        InProcess = 2,
        [EnumMember]
        Failed = 3,
        [EnumMember]
        Completed = 4,
        [EnumMember]
        Reprocess = 5,
    }

    [DataContract(Name = "StockNotificationStatuses")]
    public enum StockNotificationStatuses
    {
        [EnumMember]
        Subscribed = 1,
        [EnumMember]
        Notified = 2
    }

    // this will return Notification is inserted or updated
    [DataContract(Name = "SubscriptionStatus")]
    public enum SubscriptionStatus
    {
        [EnumMember]
        Inserted = 1,
        [EnumMember]
        Updated = 2,
        [EnumMember]
        Failed = 3
    }
}
