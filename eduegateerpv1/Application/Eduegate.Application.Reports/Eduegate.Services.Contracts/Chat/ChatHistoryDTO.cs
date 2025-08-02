using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Chat
{
    [DataContract]
    public class ChatHistoryDTO
    {
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string CommunicationMessage { get; set; }
        [DataMember]
        public long MessageIID { get; set; }
        [DataMember]
        public Nullable<long> SenderUserID { get; set; }
        [DataMember]
        public Nullable<long> ReceiverUserID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> Date { get; set; }
        [DataMember]
        public string UserStatus { get; set; }
        [DataMember]
        public int UserStatusIID { get; set; }
    }
}
