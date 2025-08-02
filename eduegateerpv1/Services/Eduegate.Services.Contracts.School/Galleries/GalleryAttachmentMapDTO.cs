using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Galleries
{
    [DataContract]
    public class GalleryAttachmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long GalleryAttachmentMapIID { get; set; }

        [DataMember]
        public long? GalleryID { get; set; }

        [DataMember]
        public long? AttachmentContentID { get; set; }

        [DataMember]
        public string AttachmentName { get; set; }
    }
}