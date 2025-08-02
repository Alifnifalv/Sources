using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Supports

{
    [DataContract]
    public class CustomerFeedbackDTO 
    {
        [DataMember]
        public long CustomerFeedbackIID { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public byte? FeedbackTypeID { get; set; }

        [DataMember]
        public string FeedbackType { get; set; }
    }
}
