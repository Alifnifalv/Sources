using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Notifications
{
    [DataContract]
    public class SendPushNotificationDTO
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string DeviceToken { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}