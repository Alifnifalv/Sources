using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Notifications
{
    public class EmailNotificationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmailNotificationDTO() {
            this.IsReprocess = false;
        }

        [DataMember]
        public long NotificationQueueID { get; set; }
        [DataMember]
        public long NotificationParentID { get; set; }
        [DataMember]
        public long EmailMetaDataIID { get; set; }
        [DataMember]
        public string ToEmailID { get; set; }
        [DataMember]
        public EmailNotificationTypes EmailNotificationType { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string FromEmailID { get; set; }
        [DataMember]
        public string ToCCEmailID { get; set; }
        [DataMember]
        public string ToBCCEmailID { get; set; }
        [DataMember]
        public List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO> AdditionalParameters { get; set; }
        [DataMember]
        public string EmailData {get;set;}
        [DataMember]
        public bool IsReprocess { get; set; }
    }
}
