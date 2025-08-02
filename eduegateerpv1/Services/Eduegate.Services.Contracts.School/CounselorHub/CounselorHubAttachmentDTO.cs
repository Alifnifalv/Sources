using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.CounselorHub
{
    [DataContract]
    public class CounselorHubAttachmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long CounselorHubAttachmentMapIID { get; set; }

        [DataMember]
        public long? CounselorHubID { get; set; }

        [DataMember]
        public long? AttachmentReferenceID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string AttachmentName { get; set; }

        [DataMember]
        [StringLength(1000)]
        public string AttachmentDescription { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public long? StudentID { get; set; }


    }
}
