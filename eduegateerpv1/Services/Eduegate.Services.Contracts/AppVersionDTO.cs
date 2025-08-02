using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class AppVersionDTO
    {
        [DataMember]        
        public string RedirectUrl { get; set; }
        [DataMember]
        public bool IsUpdated { get; set; }
        [DataMember]
        public bool IsMajor { get; set; }
        [DataMember]
        public string UpdateMessage { get; set; }
        [DataMember]
        public string CustomImageURL { get; set; }
    }
}
