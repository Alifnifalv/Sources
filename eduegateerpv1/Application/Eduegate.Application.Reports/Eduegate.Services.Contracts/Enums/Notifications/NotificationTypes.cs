using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "NotificationTypes")]
    public enum NotificationTypes
    {
        [Description("All")]
        [EnumMember]
        All = 0,
        [Description("Alerts")]
        [EnumMember]
        Alerts = 1,
        [Description("Email")]
        [EnumMember]
        Email = 2,
        [Description("Messaging")]
        [EnumMember]
        Messaging = 3,
        [Description("Events")]
        [EnumMember]
        Events = 4,
        [Description("ContactUs")]
        [EnumMember]
        ContactUs = 5,
    }
}
