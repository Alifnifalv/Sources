using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class NotificationEmailTemplateDTO : BaseMasterDTO
    {
        public NotificationEmailTemplateDTO()
        {
        }

        [DataMember]
        public int EmailTemplateID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string TemplateName { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string EmailTemplate { get; set; }
    }
}