using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class PushNotificationDTO : BaseMasterDTO
    {
        public PushNotificationDTO()
        {
            MessageSendTo = new List<KeyValueDTO>();
            MyInboxNotificationList = new List<PushNotificationDTO>();
        }

        [DataMember]
        public long NotificationAlertIID { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public int? PushNotificationUserID { get; set; }

        [DataMember]
        public string NotificationType { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string MessageSendType { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public long? FromLoginID { get; set; }

        [DataMember]
        public long? ToLoginID { get; set; }

        [DataMember]
        public DateTime? NotificationDate { get; set; }

        [DataMember]
        public int? AlertStatusID { get; set; }

        [DataMember]
        public long? ReferenceID { get; set; }

        [DataMember]
        public int? AlertActionTypeID { get; set; }

        [DataMember]
        public string ActionType { get; set; }

        [DataMember]
        public string ActionValue { get; set; }

        [DataMember]
        public string ImageURL { get; set; }

        [DataMember]
        public bool? IsEmail { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string PushNotificationUser { get; set; }

        [DataMember]
        public List<KeyValueDTO> MessageSendTo { get; set; }

        //for Dashboard
        [DataMember]
        public string NotificationDateString { get; set; }

        [DataMember]
        public long MyInboxCount { get; set; }

        [DataMember]
        public long CircularCount { get; set; }


        [DataMember]
        public List<PushNotificationDTO> MyInboxNotificationList { get; set; }
    }
}