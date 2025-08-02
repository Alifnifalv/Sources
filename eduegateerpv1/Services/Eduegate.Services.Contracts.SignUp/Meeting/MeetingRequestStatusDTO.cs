using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.Meeting
{
    [DataContract]
    public class MeetingRequestStatusDTO : BaseMasterDTO
    {
        public MeetingRequestStatusDTO()
        {
        }

        [DataMember]
        public byte MeetingRequestStatusID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string RequestStatusName { get; set; }

        [DataMember]
        public int? StatusOrder { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}