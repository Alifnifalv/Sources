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
    public class NotificationDTO
    {
        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public string AccountActivationLink { get; set; }

        [DataMember]
        public string ResetPasswordLink { get; set; }

        [DataMember]
        public string LoginLink { get; set; }

        [DataMember]
        public NotificationTypes NotificationType { get; set; }
    }
}
