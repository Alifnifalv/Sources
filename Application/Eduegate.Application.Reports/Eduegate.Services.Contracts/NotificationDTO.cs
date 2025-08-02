using System.Runtime.Serialization;
using Eduegate.Services.Contracts.Enums;
using System;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class NotificationDTO
    {
        [DataMember]
        public long? NotificationQueueIID { get; set; }
        [DataMember]
        public string NotificationTypeName { get; set; }
        [DataMember]
        public NotificationStatuses NotificationStatus { get; set; }
        [DataMember]
        public string NotificationStatusName { get {return Convert.ToString(NotificationStatus);} set { } }
        [DataMember]
        public string NotificationStatusID { get { return Convert.ToString((int)NotificationStatus); } set { } }
        
        [DataMember]
        public string ToEmailID { get; set; }
        [DataMember]
        public string FromEmailID { get; set; }
        [DataMember]
        public Nullable<bool> IsReprocess { get; set; } 
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public Nullable<long> NotificationProcessingID { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public string Emaildata { get; set; }
    }
}
