using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Notifications
{
    public class MailNotificationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int PortNumber { get; set; }

        [DataMember]
        public bool IsUseDefaultCredentials { get; set; }

        [DataMember]
        public bool IsEnableSSL { get; set; }

        [DataMember]
        public bool IsMailContainsAttachment { get; set; }

        [DataMember]
        public string MailToAddress { get; set; }

        [DataMember]
        public string MailSubject { get; set; }

        [DataMember]
        public string MailMessage { get; set; }
    }
}