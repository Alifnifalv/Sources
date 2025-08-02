using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Notifications
{
    [DataContract(Name = "SMSNotificationTypes")]
    public enum SMSNotificationTypes
    {
        [Description("All")]
        [EnumMember]
        All = 0,
        [Description("Order Delivery For Digital")]
        [EnumMember]
        OrderDeliveryForDigital = 1,
    }
}
