using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class MailBoxDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long mailBoxID { get; set; }
        [DataMember]
        public long? fromID { get; set; }
        [DataMember]
        public long? toID { get; set; }
        [DataMember]
        public string mailSubject { get; set; }
        [DataMember]
        public string mailBody { get; set; }

        [DataMember]
        public string mailFolder { get; set; }
        [DataMember]
        public bool? viewStatus { get; set; }
        [DataMember]
        public bool? fromDelete { get; set; }
        [DataMember]
        public bool? toDelete { get; set; }

        [DataMember]
        public DateTime? onDate { get; set; }

        [DataMember]
        public string SendTo { get; set; }
    }
}
