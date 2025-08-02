using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class VisitorAttachmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public VisitorAttachmentMapDTO()
        {

        }

        [DataMember]
        public long VisitorAttachmentMapIID { get; set; }

        [DataMember]
        public long? VisitorID { get; set; }

        [DataMember]
        public long? QIDFrontAttachmentID { get; set; }

        [DataMember]
        public long? QIDBackAttachmentID { get; set; }

        [DataMember]
        public long? PassportFrontAttachmentID { get; set; }

        [DataMember]
        public long? PassportBackAttachmentID { get; set; }

        [DataMember]
        public long? VisitorProfileID { get; set; }

    }
}