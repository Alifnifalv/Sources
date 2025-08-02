using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Circulars
{
    [DataContract]
    public  class CircularAttachmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
      
        [DataMember]
        public long CircularAttachmentMapIID { get; set; }

        [DataMember]
        public long? CircularID { get; set; }

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

    }
}
