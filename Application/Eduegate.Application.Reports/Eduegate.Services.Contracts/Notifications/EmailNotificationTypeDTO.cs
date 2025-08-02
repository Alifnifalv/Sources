using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class EmailNotificationTypeDTO
    {
        [DataMember] public int EmailNotificationTypeID { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string EmailTemplateFilePath { get; set; }
        [DataMember] public string EmailSubject { get; set; }
        [DataMember] public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember] public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember] public byte[] TimeStamp { get; set; }
        [DataMember] public string CreatedBy { get; set; }
        [DataMember] public string ModifiedBy { get; set; }
        [DataMember]
        public string ToBCCEmailID {get;set;}
        [DataMember] 
        public string ToCCEmailID { get; set; }

    }
}
