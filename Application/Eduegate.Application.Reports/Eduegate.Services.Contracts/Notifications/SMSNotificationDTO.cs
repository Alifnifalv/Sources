using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Notifications;

namespace Eduegate.Services.Contracts.Notifications
{
    public class SMSNotificationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SMSNotificationDTO()
        {
            this.IsReprocess = false;
        }

        [DataMember]
        public long NotificationQueueID { get; set; }
        [DataMember]
        public long NotificationParentID { get; set; }
        [DataMember]
        public long SMSMetaDataIID { get; set; }
        [DataMember]
        public string ToMobileNumbers { get; set; }
        [DataMember]
        public SMSNotificationTypes NotificationType { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        [DataMember]
        public string FromMobileNumber { get; set; }
        [DataMember]
        public string ToCCMobileNumber { get; set; }
        [DataMember]
        public string ToBCCMobileNumber { get; set; }
        [DataMember]
        public List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO> AdditionalParameters { get; set; }
        [DataMember]
        public string Content {get;set;}
        [DataMember]
        public bool IsReprocess { get; set; }
    }
}
